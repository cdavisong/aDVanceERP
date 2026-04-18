using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Monedas;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Monedas;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace aDVanceERP.Core.Controladores {

    /// <summary>
    /// Genera el archivo JSON de catálogo que aDVance POS Mobile necesita.
    /// Incluye productos con stock > 0 y monedas activas con tasas del día.
    /// 
    /// Uso típico:
    ///   var exportador = new ExportadorCatalogosPos(@"exports\pos\");
    ///   exportador.ExportarCatalogoCompleto(idAlmacen);
    ///   controladorAdb.PushCatalogo(exportador.RutaCatalogo);
    /// </summary>
    public class ExportadorCatalogosPos {

        private readonly string _carpetaDestino;

        private static readonly JsonSerializerOptions _opcionesJson = new() {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ExportadorCatalogosPos(string carpetaDestino) {
            _carpetaDestino = carpetaDestino;

            if (!Directory.Exists(carpetaDestino))
                Directory.CreateDirectory(_carpetaDestino);
        }

        // ── Ruta de salida ──
        public string RutaCatalogo => Path.Combine(_carpetaDestino, "catalogo.json");

        // ══════════════════════════════════════════════════════
        //  EXPORTACIÓN COMPLETA
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Exporta el catálogo completo de un almacén: productos + monedas.
        /// </summary>
        public void ExportarCatalogoCompleto(long idAlmacen) {
            var repoInventario = RepoInventario.Instancia;
            var repoProducto = RepoProducto.Instancia;
            var repoPresentaciones = RepoPresentacionProducto.Instancia;            
            var repoUnidadMedida = RepoUnidadMedida.Instancia;
            var repoMoneda = RepoMoneda.Instancia;
            var repoTasaCambio = RepoTasaCambio.Instancia;

            // Obtener almacén
            var almacen = RepoAlmacen.Instancia.ObtenerPorId(idAlmacen);

            if (almacen == null)
                throw new InvalidOperationException($"Almacén {idAlmacen} no encontrado.");

            // Obtener inventario del almacén y productos con stock > 0
            var inventarioAlmacen = repoInventario
                .Buscar(FiltroBusquedaInventario.IdAlmacen, idAlmacen.ToString())
                .resultadosBusqueda
                .Select(r => r.entidadBase as Inventario)
                .Where(i => i.Cantidad > 0)
                .ToList();

            var productos = new List<Producto>();
            
            foreach(var item in inventarioAlmacen) {
                var producto = repoProducto.ObtenerPorId(item.IdProducto);

                if (producto != null && producto.Activo)
                    productos.Add(producto);
            }

            // Obtener monedas activas con tasas
            var monedaBase = repoMoneda.ObtenerMonedaBase();
            var monedasActivas = repoMoneda.ObtenerActivas();

            var monedasDto = monedasActivas.Select(m => {
                var tasa = 1m;
                var aplicaEfectivo = true;

                if (!m.EsBase) {
                    var tasaCambio = repoTasaCambio
                        .Buscar(FiltroBusquedaTasaCambio.VigenteHoy, monedaBase.Id.ToString(), m.Id.ToString())
                        .resultadosBusqueda.FirstOrDefault()
                        .entidadBase as TasaCambio;

                    tasa = tasaCambio?.Tasa ?? 1m;
                    aplicaEfectivo = tasaCambio?.AplicaEfectivo ?? true;
                }

                return new MonedaPosDto {
                    Id = m.Id,
                    Codigo = m.Codigo,
                    Nombre = m.Nombre,
                    Simbolo = m.Simbolo,
                    EsBase = m.EsBase,
                    TasaHoy = tasa,
                    AplicaEfectivo = aplicaEfectivo,
                    PrecisionDecimal = m.PrecisionDecimal
                };
            }).ToList();

            // Serializar
            var catalogo = new CatalogoPosDto {
                Meta = new CatalogoMetaPosDto {
                    Version = "1.0",
                    GeneradoEn = DateTime.Now,
                    Aplicacion = "aDVance ERP",
                    IdAlmacen = idAlmacen,
                    NombreAlmacen = almacen.Nombre
                },
                Productos = productos.Select(p => new ProductoPosDto {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Codigo = p.Codigo ?? "",
                    Descripcion = p.Descripcion ?? "",
                    Categoria = p.Categoria.ToString(),
                    PrecioVentaBase = p.PrecioVentaBase,
                    ImpuestoVentaPorcentaje = p.ImpuestoVentaPorcentaje,
                    StockDisponible = inventarioAlmacen.FirstOrDefault(i => i.IdProducto == p.Id)?.Cantidad ?? 0,
                    UnidadMedida = repoUnidadMedida.ObtenerPorId(p.IdUnidadMedida)?.Abreviatura ?? "u",
                    Presentaciones = repoPresentaciones
                        .Buscar(FiltroBusquedaPresentacionProducto.IdProducto, p.Id.ToString())
                        .resultadosBusqueda
                        .Select(r => r.entidadBase as PresentacionProducto)
                        .Where(pr => pr != null && pr.Activo)
                        .Select(pr => new PresentacionPosDto {
                            Id = pr!.Id,
                            Cantidad = pr.Cantidad,
                            PrecioVenta = pr.PrecioVenta,
                            Activo = pr.Activo,
                            UnidadMedida = repoUnidadMedida.ObtenerPorId(pr.IdUnidadMedida)?.Abreviatura ?? "u"
                        }).ToList()
                }).ToList(),
                Monedas = monedasDto
            };

            Serializar(catalogo, RutaCatalogo);
        }

        // ══════════════════════════════════════════════════════
        //  PRIVADOS
        // ══════════════════════════════════════════════════════

        private void Serializar<T>(T datos, string ruta) {
            var json = JsonSerializer.Serialize(datos, _opcionesJson);
            File.WriteAllText(ruta, json, System.Text.Encoding.UTF8);
        }
    }

    // ══════════════════════════════════════════════════════════
    //  DTOs de serialización — coinciden con CatalogoJson.cs del POS
    // ══════════════════════════════════════════════════════════

    file class CatalogoPosDto {
        [JsonPropertyName("meta")] public CatalogoMetaPosDto Meta { get; set; } = new();
        [JsonPropertyName("productos")] public List<ProductoPosDto> Productos { get; set; } = new();
        [JsonPropertyName("monedas")] public List<MonedaPosDto> Monedas { get; set; } = new();
    }

    file class CatalogoMetaPosDto {
        [JsonPropertyName("version")] public string Version { get; set; } = "1.0";
        [JsonPropertyName("generadoEn")] public DateTime GeneradoEn { get; set; }
        [JsonPropertyName("aplicacion")] public string Aplicacion { get; set; } = string.Empty;
        [JsonPropertyName("idAlmacen")] public long IdAlmacen { get; set; }
        [JsonPropertyName("nombreAlmacen")] public string NombreAlmacen { get; set; } = string.Empty;
    }

    file class ProductoPosDto {
        [JsonPropertyName("id")] public long Id { get; set; }
        [JsonPropertyName("nombre")] public string Nombre { get; set; } = string.Empty;
        [JsonPropertyName("codigo")] public string Codigo { get; set; } = string.Empty;
        [JsonPropertyName("descripcion")] public string Descripcion { get; set; } = string.Empty;
        [JsonPropertyName("categoria")] public string Categoria { get; set; } = string.Empty;
        [JsonPropertyName("precioVentaBase")] public decimal PrecioVentaBase { get; set; }
        [JsonPropertyName("impuestoVentaPorcentaje")] public decimal ImpuestoVentaPorcentaje { get; set; }
        [JsonPropertyName("stockDisponible")] public decimal StockDisponible { get; set; }
        [JsonPropertyName("unidadMedida")] public string UnidadMedida { get; set; } = string.Empty;
        [JsonPropertyName("presentaciones")] public List<PresentacionPosDto> Presentaciones { get; set; } = new();
    }

    file class PresentacionPosDto {
        [JsonPropertyName("id")] public long Id { get; set; }
        [JsonPropertyName("cantidad")] public decimal Cantidad { get; set; }
        [JsonPropertyName("precioVenta")] public decimal PrecioVenta { get; set; }
        [JsonPropertyName("activo")] public bool Activo { get; set; }
        [JsonPropertyName("unidadMedida")] public string UnidadMedida { get; set; } = string.Empty;
    }

    file class MonedaPosDto {
        [JsonPropertyName("id")] public long Id { get; set; }
        [JsonPropertyName("codigo")] public string Codigo { get; set; } = string.Empty;
        [JsonPropertyName("nombre")] public string Nombre { get; set; } = string.Empty;
        [JsonPropertyName("simbolo")] public string Simbolo { get; set; } = string.Empty;
        [JsonPropertyName("esBase")] public bool EsBase { get; set; }
        [JsonPropertyName("tasaHoy")] public decimal TasaHoy { get; set; } = 1m;
        [JsonPropertyName("aplicaEfectivo")] public bool AplicaEfectivo { get; set; } = true;
        [JsonPropertyName("precisionDecimal")] public int PrecisionDecimal { get; set; } = 2;
    }
}