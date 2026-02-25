using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Modelos.Modulos.Inventario;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace aDVanceERP.Core.Controladores {

    /// <summary>
    /// Genera los 5 archivos JSON de catálogo que aDVance Stock Mobile
    /// necesita para operar. Cada método recibe la lista del repositorio
    /// correspondiente y escribe el JSON en la ruta indicada.
    ///
    /// Uso típico:
    ///   var exportador = new ExportadorCatalogosStock(@"exports\stock\");
    ///   exportador.ExportarTodo(productos, proveedores, unidades, clasificaciones, almacenes);
    ///   controladorAdb.FlujoPreparacion(exportador.RutaProductos, ...);
    /// </summary>
    public class ExportadorCatalogosStock {

        private readonly string _carpetaDestino;

        private static readonly JsonSerializerOptions _opcionesJson = new() {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ExportadorCatalogosStock(string carpetaDestino) {
            _carpetaDestino = carpetaDestino;

            if (!Directory.Exists(carpetaDestino))
                Directory.CreateDirectory(_carpetaDestino);
        }

        // ── Rutas de salida (para pasarlas al ControladorArchivosAndroidStock) ──

        public string RutaProductos => Path.Combine(_carpetaDestino, "catalogo_productos.json");
        public string RutaProveedores => Path.Combine(_carpetaDestino, "catalogo_proveedores.json");
        public string RutaUnidades => Path.Combine(_carpetaDestino, "catalogo_unidades.json");
        public string RutaClasificaciones => Path.Combine(_carpetaDestino, "catalogo_clasificaciones.json");
        public string RutaAlmacenes => Path.Combine(_carpetaDestino, "catalogo_almacenes.json");

        // ══════════════════════════════════════════════════════
        //  EXPORTACIÓN COMPLETA
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Exporta los 5 catálogos de una sola vez.
        /// Pasa las listas tal como las devuelve tu repositorio.
        /// El parámetro proveedores es de tipo object porque aún no
        /// tenemos la clase Proveedor — ver sobrecarga más abajo.
        /// </summary>
        public void ExportarTodo(
            IEnumerable<Producto> productos,
            IEnumerable<Proveedor> proveedores,
            IEnumerable<UnidadMedida> unidades,
            IEnumerable<ClasificacionProducto> clasificaciones,
            IEnumerable<Almacen> almacenes) {

            ExportarProductos(productos);
            ExportarProveedores(proveedores);
            ExportarUnidades(unidades);
            ExportarClasificaciones(clasificaciones);
            ExportarAlmacenes(almacenes);
        }

        // ══════════════════════════════════════════════════════
        //  EXPORTACIÓN INDIVIDUAL
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Exporta la lista de productos existentes.
        /// La app mobile solo necesita Id, Codigo y Nombre para detectar
        /// si un código escaneado ya existe en el ERP.
        /// </summary>
        public void ExportarProductos(IEnumerable<Producto> productos) {
            var dtos = productos
                .Where(p => p.Activo)           // solo productos activos
                .Select(p => new ProductoMobileDto {
                    Id = p.Id,
                    Codigo = p.Codigo ?? "",
                    Nombre = p.Nombre
                });

            Serializar(dtos, RutaProductos);
        }

        /// <summary>
        /// Exporta proveedores. Adáptalo a tu clase Proveedor cuando la tengas.
        /// La firma usa (long id, string codigo, string razonSocial) para
        /// no depender de la clase concreta aquí.
        /// </summary>
        public void ExportarProveedores(
            IEnumerable<(long Id, string Codigo, string RazonSocial)> proveedores) {

            var dtos = proveedores.Select(p => new ProveedorMobileDto {
                Id = p.Id,
                Codigo = p.Codigo,
                RazonSocial = p.RazonSocial
            });

            Serializar(dtos, RutaProveedores);
        }

        public void ExportarProveedores(IEnumerable<Proveedor> proveedores) {
            var dtos = proveedores
                .Where(p => p.Activo)
                .Select(p => new ProveedorMobileDto {
                    Id = p.Id,
                    Codigo = p.CodigoProveedor,
                    RazonSocial = p.RazonSocial
                });
            Serializar(dtos, RutaProveedores);
        }

        public void ExportarUnidades(IEnumerable<UnidadMedida> unidades) {
            var dtos = unidades.Select(u => new UnidadMedidaMobileDto {
                Id = u.Id,
                Nombre = u.Nombre,
                Abreviatura = u.Abreviatura
            });

            Serializar(dtos, RutaUnidades);
        }

        public void ExportarClasificaciones(IEnumerable<ClasificacionProducto> clasificaciones) {
            var dtos = clasificaciones.Select(c => new ClasificacionMobileDto {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion
            });

            Serializar(dtos, RutaClasificaciones);
        }

        public void ExportarAlmacenes(IEnumerable<Almacen> almacenes) {
            var dtos = almacenes
                .Where(a => a.Estado)           // solo almacenes activos
                .Select(a => new AlmacenMobileDto {
                    Id = a.Id,
                    Nombre = a.Nombre,
                    // Almacen no tiene Codigo — usamos "ALM-{Id}" como referencia legible
                    Codigo = $"ALM-{a.Id:D3}"
                });

            Serializar(dtos, RutaAlmacenes);
        }

        // ══════════════════════════════════════════════════════
        //  PRIVADOS
        // ══════════════════════════════════════════════════════

        private void Serializar<T>(IEnumerable<T> datos, string ruta) {
            var json = JsonSerializer.Serialize(datos.ToList(), _opcionesJson);
            File.WriteAllText(ruta, json, System.Text.Encoding.UTF8);
        }
    }

    // ══════════════════════════════════════════════════════════
    //  DTOs de serialización — solo los campos que necesita la app
    //  Estas clases son internas al exportador, no reemplazan tus modelos
    // ══════════════════════════════════════════════════════════

    file class ProductoMobileDto {
        [JsonPropertyName("id")] public long Id { get; set; }
        [JsonPropertyName("codigo")] public string Codigo { get; set; } = "";
        [JsonPropertyName("nombre")] public string Nombre { get; set; } = "";
    }

    file class ProveedorMobileDto {
        [JsonPropertyName("id")] public long Id { get; set; }
        [JsonPropertyName("codigo")] public string Codigo { get; set; } = "";
        [JsonPropertyName("razonSocial")] public string RazonSocial { get; set; } = "";
    }

    file class UnidadMedidaMobileDto {
        [JsonPropertyName("id")] public long Id { get; set; }
        [JsonPropertyName("nombre")] public string Nombre { get; set; } = "";
        [JsonPropertyName("abreviatura")] public string Abreviatura { get; set; } = "";
    }

    file class ClasificacionMobileDto {
        [JsonPropertyName("id")] public long Id { get; set; }
        [JsonPropertyName("nombre")] public string Nombre { get; set; } = "";
        [JsonPropertyName("descripcion")] public string Descripcion { get; set; } = "";
    }

    file class AlmacenMobileDto {
        [JsonPropertyName("id")] public long Id { get; set; }
        [JsonPropertyName("nombre")] public string Nombre { get; set; } = "";
        [JsonPropertyName("codigo")] public string Codigo { get; set; } = "";
    }
}