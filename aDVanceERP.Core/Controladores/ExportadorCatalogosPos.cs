using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Monedas;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace aDVanceERP.Core.Controladores {

    /// <summary>
    /// Genera el archivo JSON de catálogo que aDVance POS Mobile necesita.
    /// Incluye productos con stock > 0 y monedas activas con tasas del día.
    ///
    /// PATRÓN: este exportador recibe los datos ya cargados por el presenter.
    /// No instancia repositorios — sólo transforma y serializa.
    ///
    /// Uso típico:
    ///   var exportador = new ExportadorCatalogosPos(@"exports\pos\");
    ///   var monedas = ConstruirMonedasDto(); // helper del presenter
    ///   exportador.ExportarCatalogo(productos, inventario, almacen, unidades, monedas, presentaciones);
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

        /// <summary>Ruta de salida del catálogo generado.</summary>
        public string RutaCatalogo => Path.Combine(_carpetaDestino, "catalogo.json");

        // ══════════════════════════════════════════════════════
        //  EXPORTACIÓN
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Exporta el catálogo completo con los datos proporcionados por el presenter.
        /// </summary>
        /// <param name="productos">Productos activos con stock disponible en el almacén.</param>
        /// <param name="inventarioAlmacen">
        ///   Registros de inventario del almacén — se usa para obtener StockDisponible por producto.
        /// </param>
        /// <param name="almacen">Almacén de origen (para la sección meta del catálogo).</param>
        /// <param name="unidades">Todas las unidades de medida activas.</param>
        /// <param name="monedas">
        ///   Monedas activas con tasa ya resuelta. Ver <see cref="MonedaCatalogoDto"/>.
        /// </param>
        /// <param name="presentacionesPorProducto">
        ///   Presentaciones activas indexadas por IdProducto.
        ///   Puede ser null o vacío si ningún producto tiene presentaciones.
        /// </param>
        public void ExportarCatalogo(
            IEnumerable<Producto>                        productos,
            IEnumerable<Inventario>                      inventarioAlmacen,
            Almacen                                      almacen,
            IEnumerable<UnidadMedida>                    unidades,
            IEnumerable<MonedaCatalogoDto>               monedas,
            Dictionary<long, List<PresentacionProducto>>? presentacionesPorProducto = null) {

            var inventarioLista  = inventarioAlmacen.ToList();
            var unidadesDict     = unidades.ToDictionary(u => u.Id, u => u.Abreviatura);
            var presentaciones   = presentacionesPorProducto
                                   ?? new Dictionary<long, List<PresentacionProducto>>();

            var catalogo = new CatalogoPosDto {
                Meta = new CatalogoMetaPosDto {
                    Version       = "1.0",
                    GeneradoEn    = DateTime.Now,
                    Aplicacion    = "aDVance ERP",
                    IdAlmacen     = almacen.Id,
                    NombreAlmacen = almacen.Nombre
                },
                Productos = productos.Select(p => new ProductoPosDto {
                    Id                       = p.Id,
                    Nombre                   = p.Nombre,
                    Codigo                   = p.Codigo ?? string.Empty,
                    Descripcion              = p.Descripcion ?? string.Empty,
                    Categoria                = p.Categoria.ToString(),
                    PrecioVentaBase          = p.PrecioVentaBase,
                    ImpuestoVentaPorcentaje  = p.ImpuestoVentaPorcentaje,
                    StockDisponible          = inventarioLista
                                               .FirstOrDefault(i => i.IdProducto == p.Id)
                                               ?.Cantidad ?? 0,
                    UnidadMedida             = unidadesDict.TryGetValue(p.IdUnidadMedida, out var abr)
                                               ? abr : "u",
                    Presentaciones           = presentaciones.TryGetValue(p.Id, out var prs)
                                               ? prs.Select(pr => new PresentacionPosDto {
                                                     Id           = pr.Id,
                                                     Cantidad     = pr.Cantidad,
                                                     PrecioVenta  = pr.PrecioVenta,
                                                     Activo       = pr.Activo,
                                                     UnidadMedida = unidadesDict.TryGetValue(
                                                                        pr.IdUnidadMedida, out var abrPr)
                                                                    ? abrPr : "u"
                                                 }).ToList()
                                               : new List<PresentacionPosDto>()
                }).ToList(),
                Monedas = monedas.Select(m => new MonedaPosDto {
                    Id               = m.Id,
                    Codigo           = m.Codigo,
                    Nombre           = m.Nombre,
                    Simbolo          = m.Simbolo,
                    EsBase           = m.EsBase,
                    TasaHoy          = m.TasaHoy,
                    AplicaEfectivo   = m.AplicaEfectivo,
                    PrecisionDecimal = m.PrecisionDecimal
                }).ToList()
            };

            var json = JsonSerializer.Serialize(catalogo, _opcionesJson);
            File.WriteAllText(RutaCatalogo, json, System.Text.Encoding.UTF8);
        }
    }

    // ══════════════════════════════════════════════════════════
    //  DTO de entrada para monedas
    //  (el presenter construye este DTO con tasa ya resuelta)
    // ══════════════════════════════════════════════════════════

    /// <summary>
    /// Moneda con su tasa de cambio vigente ya resuelta.
    /// El presenter construye esta colección antes de llamar a ExportarCatalogo.
    /// </summary>
    public record MonedaCatalogoDto(
        long    Id,
        string  Codigo,
        string  Nombre,
        string  Simbolo,
        bool    EsBase,
        decimal TasaHoy,
        bool    AplicaEfectivo,
        int     PrecisionDecimal);

    // ══════════════════════════════════════════════════════════
    //  DTOs de serialización JSON — coinciden con CatalogoJson.cs del POS
    //  Son file-scoped porque solo este exportador los serializa.
    // ══════════════════════════════════════════════════════════

    file class CatalogoPosDto {
        [JsonPropertyName("meta")]      public CatalogoMetaPosDto  Meta      { get; set; } = new();
        [JsonPropertyName("productos")] public List<ProductoPosDto> Productos { get; set; } = new();
        [JsonPropertyName("monedas")]   public List<MonedaPosDto>   Monedas   { get; set; } = new();
    }

    file class CatalogoMetaPosDto {
        [JsonPropertyName("version")]       public string   Version       { get; set; } = "1.0";
        [JsonPropertyName("generadoEn")]    public DateTime GeneradoEn    { get; set; }
        [JsonPropertyName("aplicacion")]    public string   Aplicacion    { get; set; } = string.Empty;
        [JsonPropertyName("idAlmacen")]     public long     IdAlmacen     { get; set; }
        [JsonPropertyName("nombreAlmacen")] public string   NombreAlmacen { get; set; } = string.Empty;
    }

    file class ProductoPosDto {
        [JsonPropertyName("id")]                      public long                   Id                      { get; set; }
        [JsonPropertyName("nombre")]                  public string                 Nombre                  { get; set; } = string.Empty;
        [JsonPropertyName("codigo")]                  public string                 Codigo                  { get; set; } = string.Empty;
        [JsonPropertyName("descripcion")]             public string                 Descripcion             { get; set; } = string.Empty;
        [JsonPropertyName("categoria")]               public string                 Categoria               { get; set; } = string.Empty;
        [JsonPropertyName("precioVentaBase")]         public decimal                PrecioVentaBase         { get; set; }
        [JsonPropertyName("impuestoVentaPorcentaje")] public decimal                ImpuestoVentaPorcentaje { get; set; }
        [JsonPropertyName("stockDisponible")]         public decimal                StockDisponible         { get; set; }
        [JsonPropertyName("unidadMedida")]            public string                 UnidadMedida            { get; set; } = string.Empty;
        [JsonPropertyName("presentaciones")]          public List<PresentacionPosDto> Presentaciones        { get; set; } = new();
    }

    file class PresentacionPosDto {
        [JsonPropertyName("id")]           public long    Id          { get; set; }
        [JsonPropertyName("cantidad")]     public decimal Cantidad    { get; set; }
        [JsonPropertyName("precioVenta")]  public decimal PrecioVenta { get; set; }
        [JsonPropertyName("activo")]       public bool    Activo      { get; set; }
        [JsonPropertyName("unidadMedida")] public string  UnidadMedida { get; set; } = string.Empty;
    }

    file class MonedaPosDto {
        [JsonPropertyName("id")]               public long    Id               { get; set; }
        [JsonPropertyName("codigo")]           public string  Codigo           { get; set; } = string.Empty;
        [JsonPropertyName("nombre")]           public string  Nombre           { get; set; } = string.Empty;
        [JsonPropertyName("simbolo")]          public string  Simbolo          { get; set; } = string.Empty;
        [JsonPropertyName("esBase")]           public bool    EsBase           { get; set; }
        [JsonPropertyName("tasaHoy")]          public decimal TasaHoy          { get; set; } = 1m;
        [JsonPropertyName("aplicaEfectivo")]   public bool    AplicaEfectivo   { get; set; } = true;
        [JsonPropertyName("precisionDecimal")] public int     PrecisionDecimal  { get; set; } = 2;
    }
}
