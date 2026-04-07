using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario {
    public class RepoPrecioPresentacion : RepoEntidadBaseDatos<PrecioPresentacion, FiltroBusquedaPrecioPresentacion> {

        public RepoPrecioPresentacion()
            : base("adv__precio_presentacion", "id_precio_presentacion") { }

        protected override string GenerarComandoAdicionar(
            PrecioPresentacion entidad,
            out Dictionary<string, object> parametros,
            params IEntidadBaseDatos[] entidadesExtra) {

            const string consulta = """
                INSERT INTO adv__precio_presentacion (
                    id_producto,
                    id_unidad_medida,
                    cantidad,
                    precio_venta,
                    activo
                ) VALUES (
                    @id_producto,
                    @id_unidad_medida,
                    @cantidad,
                    @precio_venta,
                    @activo
                );
                """;

            parametros = new Dictionary<string, object> {
                { "@id_producto",      entidad.IdProducto     },
                { "@id_unidad_medida", entidad.IdUnidadMedida },
                { "@cantidad",         entidad.Cantidad       },
                { "@precio_venta",     entidad.PrecioVenta    },
                { "@activo",           entidad.Activo ? 1 : 0 }
            };

            return consulta;
        }

        // ── EDITAR ───────────────────────────────────────────

        protected override string GenerarComandoEditar(
            PrecioPresentacion entidad,
            out Dictionary<string, object> parametros,
            params IEntidadBaseDatos[] entidadesExtra) {

            const string consulta = """
                UPDATE adv__precio_presentacion
                SET
                    id_unidad_medida = @id_unidad_medida,
                    cantidad         = @cantidad,
                    precio_venta     = @precio_venta,
                    activo           = @activo
                WHERE id_precio_presentacion = @id;
                """;

            parametros = new Dictionary<string, object> {
                { "@id",               entidad.Id             },
                { "@id_unidad_medida", entidad.IdUnidadMedida },
                { "@cantidad",         entidad.Cantidad       },
                { "@precio_venta",     entidad.PrecioVenta    },
                { "@activo",           entidad.Activo ? 1 : 0 }
            };

            return consulta;
        }

        // ── ELIMINAR ─────────────────────────────────────────

        protected override string GenerarComandoEliminar(
            long id,
            out Dictionary<string, object> parametros) {

            const string consulta = """
                DELETE FROM adv__precio_presentacion
                WHERE id_precio_presentacion = @id;
                """;

            parametros = new Dictionary<string, object> {
                { "@id", id }
            };

            return consulta;
        }

        // ── OBTENER / BUSCAR ─────────────────────────────────

        protected override string GenerarComandoObtener(
            FiltroBusquedaPrecioPresentacion filtroBusqueda,
            out Dictionary<string, object> parametros,
            params string[] criteriosBusqueda) {

            var criterio = criteriosBusqueda.Length > 0
                ? criteriosBusqueda[0]
                : string.Empty;

            const string baseQuery = """
                SELECT pp.*, p.nombre AS nombre_producto, p.precio_venta_base
                FROM adv__precio_presentacion pp
                LEFT JOIN adv__producto p ON pp.id_producto = p.id_producto
                """;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaPrecioPresentacion.Id => $"""
                    {baseQuery}
                    WHERE pp.id_precio_presentacion = @id;
                    """,

                FiltroBusquedaPrecioPresentacion.IdProducto => $"""
                    {baseQuery}
                    WHERE pp.id_producto = @id_producto
                    ORDER BY pp.cantidad ASC;
                    """,

                FiltroBusquedaPrecioPresentacion.SoloActivos => $"""
                    {baseQuery}
                    WHERE pp.id_producto = @id_producto
                      AND pp.activo = 1
                    ORDER BY pp.cantidad ASC;
                    """,

                _ => $"""
                    {baseQuery}
                    ORDER BY pp.id_producto, pp.cantidad ASC;
                    """
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaPrecioPresentacion.Id => new Dictionary<string, object> {
                    { "@id", Convert.ToInt64(string.IsNullOrEmpty(criterio) ? "0" : criterio) }
                },
                FiltroBusquedaPrecioPresentacion.IdProducto or
                FiltroBusquedaPrecioPresentacion.SoloActivos => new Dictionary<string, object> {
                    { "@id_producto", Convert.ToInt64(string.IsNullOrEmpty(criterio) ? "0" : criterio) }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        // ── MAPEADOR ─────────────────────────────────────────

        protected override (PrecioPresentacion, List<IEntidadBaseDatos>) MapearEntidad(
            MySqlDataReader lector) {

            var presentacion = new PrecioPresentacion(
                id: Convert.ToInt64(lector["id_precio_presentacion"]),
                idProducto: Convert.ToInt64(lector["id_producto"]),
                idUnidadMedida: Convert.ToInt64(lector["id_unidad_medida"]),
                cantidad: Convert.ToDecimal(lector["cantidad"], CultureInfo.InvariantCulture),
                precioVenta: Convert.ToDecimal(lector["precio_venta"], CultureInfo.InvariantCulture),
                activo: Convert.ToBoolean(lector["activo"])
            );

            // Si el JOIN con adv__producto está en la consulta, disponemos del producto base
            var entidadesExtra = new List<IEntidadBaseDatos>();

            if (lector.VisibleFieldCount > 5) {
                entidadesExtra.Add(new Producto {
                    Nombre = Convert.ToString(lector["nombre_producto"]) ?? string.Empty,
                    PrecioVentaBase = lector["precio_venta_base"] != DBNull.Value
                        ? Convert.ToDecimal(lector["precio_venta_base"], CultureInfo.InvariantCulture)
                        : 0m
                });
            }

            return (presentacion, entidadesExtra);
        }

        // ── SINGLETON ────────────────────────────────────────

        public static RepoPrecioPresentacion Instancia { get; } = new RepoPrecioPresentacion();

        // ── UTILIDADES ───────────────────────────────────────

        /// <summary>
        /// Devuelve todas las presentaciones activas de un producto,
        /// ordenadas de menor a mayor cantidad.
        /// Es el método principal que usarán el presentador de ventas y el POS.
        /// </summary>
        public List<PrecioPresentacion> ObtenerActivasPorProducto(long idProducto) {
            var (_, resultados) = Buscar(
                FiltroBusquedaPrecioPresentacion.SoloActivos,
                idProducto.ToString());

            return resultados
                .Select(r => r.entidadBase)
                .OrderBy(p => p.Cantidad)
                .ToList();
        }

        /// <summary>
        /// Activa o desactiva una presentación sin eliminarla.
        /// </summary>
        public bool CambiarEstado(long id, bool nuevoEstado) {
            const string consulta = """
                UPDATE adv__precio_presentacion
                SET activo = @activo
                WHERE id_precio_presentacion = @id;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id",     id },
                { "@activo", nuevoEstado ? 1 : 0 }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        /// <summary>
        /// Verifica si ya existe una presentación con la misma cantidad
        /// para el mismo producto (evita duplicados).
        /// </summary>
        public bool ExistePresentacionConCantidad(long idProducto, decimal cantidad, long excluirId = 0) {
            const string consulta = """
                SELECT COUNT(*)
                FROM adv__precio_presentacion
                WHERE id_producto = @id_producto
                  AND cantidad    = @cantidad
                  AND id_precio_presentacion <> @excluir_id;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_producto",  idProducto  },
                { "@cantidad",     cantidad     },
                { "@excluir_id",   excluirId    }
            };

            return ContextoBaseDatos.EjecutarConsultaEscalar<int>(consulta, parametros) > 0;
        }

        /// <summary>
        /// Elimina todas las presentaciones de un producto.
        /// Útil cuando se elimina el producto completo.
        /// </summary>
        public bool EliminarPorProducto(long idProducto) {
            const string consulta = """
                DELETE FROM adv__precio_presentacion
                WHERE id_producto = @id_producto;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_producto", idProducto }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) >= 0;
        }
    }
}
