using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.BD;
using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Venta {
    public class RepoDetalleVentaProducto : RepoEntidadBaseDatos<DetalleVentaProducto, FiltroBusquedaDetalleVenta> {
        public RepoDetalleVentaProducto() : base("adv__detalle_venta_producto", "id_detalle_venta_producto") {
        }

        protected override string GenerarComandoAdicionar(DetalleVentaProducto entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
                INSERT INTO adv__detalle_venta_producto (
                    id_venta,
                    id_producto,
                    cantidad,
                    precio_compra_vigente,
                    precio_venta_unitario,
                    descuento_item
                ) VALUES (
                    @id_venta,
                    @id_producto,
                    @cantidad,
                    @precio_compra_vigente,
                    @precio_venta_unitario,
                    @descuento_item
                );
                """;

            parametros = new Dictionary<string, object> {
                { "@id_venta", entidad.IdVenta },
                { "@id_producto", entidad.IdProducto },
                { "@cantidad", entidad.Cantidad },
                { "@precio_compra_vigente", entidad.PrecioCompraVigente },
                { "@precio_venta_unitario", entidad.PrecioVentaUnitario },
                { "@descuento_item", entidad.DescuentoItem }
            };

            return comando;
        }

        protected override string GenerarComandoEditar(DetalleVentaProducto entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
                UPDATE adv__detalle_venta_producto 
                SET 
                    id_venta = @id_venta,
                    id_producto = @id_producto,
                    cantidad = @cantidad,
                    precio_compra_vigente = @precio_compra_vigente,
                    precio_venta_unitario = @precio_venta_unitario,
                    descuento_item = @descuento_item
                WHERE id_detalle_venta_producto = @id_detalle
                """;

            parametros = new Dictionary<string, object> {
                { "@id_detalle", entidad.Id },
                { "@id_venta", entidad.IdVenta },
                { "@id_producto", entidad.IdProducto },
                { "@cantidad", entidad.Cantidad },
                { "@precio_compra_vigente", entidad.PrecioCompraVigente },
                { "@precio_venta_unitario", entidad.PrecioVentaUnitario },
                { "@descuento_item", entidad.DescuentoItem }
            };

            return comando;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var comando = $"""
                DELETE FROM adv__detalle_venta_producto 
                WHERE id_detalle_venta_producto = @id_detalle
                """;

            parametros = new Dictionary<string, object> {
                { "@id_detalle", id }
            };

            return comando;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaDetalleVenta filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;

            var consultaComun = $"""
                SELECT dvp.*, p.nombre as nombre_producto, p.codigo as codigo_producto
                FROM adv__detalle_venta_producto dvp
                LEFT JOIN adv__producto p ON dvp.id_producto = p.id_producto
                """;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaDetalleVenta.PorVenta => $"""
                    {consultaComun}
                    WHERE dvp.id_venta = @id_venta
                    """,
                FiltroBusquedaDetalleVenta.PorProducto => $"""
                    {consultaComun}
                    WHERE dvp.id_producto = @id_producto
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaDetalleVenta.PorVenta => new Dictionary<string, object> {
                    { "@id_venta", long.Parse(criterio) }
                },
                FiltroBusquedaDetalleVenta.PorProducto => new Dictionary<string, object> {
                    { "@id_producto", long.Parse(criterio) }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (DetalleVentaProducto, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var detalle = new DetalleVentaProducto {
                Id = Convert.ToInt64(lector["id_detalle_venta_producto"]),
                IdVenta = Convert.ToInt64(lector["id_venta"]),
                IdProducto = Convert.ToInt64(lector["id_producto"]),
                Cantidad = Convert.ToDecimal(lector["cantidad"], CultureInfo.InvariantCulture),
                PrecioCompraVigente = Convert.ToDecimal(lector["precio_compra_vigente"], CultureInfo.InvariantCulture),
                PrecioVentaUnitario = Convert.ToDecimal(lector["precio_venta_unitario"], CultureInfo.InvariantCulture),
                DescuentoItem = Convert.ToDecimal(lector["descuento_item"], CultureInfo.InvariantCulture),
                Subtotal = Convert.ToDecimal(lector["subtotal"], CultureInfo.InvariantCulture)
            };

            var entidadesExtra = new List<IEntidadBaseDatos>();

            if (lector.VisibleFieldCount > 8) {
                entidadesExtra.Add(new Producto {
                    Nombre = Convert.ToString(lector["nombre_producto"]) ?? "N/A",
                    Codigo = Convert.ToString(lector["codigo_producto"]) ?? "N/A"
                });
            }

            return (detalle, entidadesExtra);
        }

        #region STATIC
        public static RepoDetalleVentaProducto Instancia { get; } = new RepoDetalleVentaProducto();
        #endregion

        #region UTILES

        public bool EliminarDetallesPorVenta(long idVenta) {
            var consulta = $"""
                DELETE FROM adv__detalle_venta_producto
                WHERE id_venta = @id_venta;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_venta", idVenta }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        public decimal ObtenerTotalVenta(long idVenta) {
            var consulta = $"""
                SELECT COALESCE(SUM(subtotal), 0) as total
                FROM adv__detalle_venta_producto
                WHERE id_venta = @id_venta
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_venta", idVenta }
            };

            var resultado = ContextoBaseDatos.EjecutarConsultaEscalar<decimal>(consulta, parametros);
            return resultado;
        }

        public List<DetalleVentaProducto> ObtenerDetallesConProducto(long idVenta) {
            var consulta = $"""
                SELECT dvp.*, p.nombre as nombre_producto, p.codigo as codigo_producto, p.categoria
                FROM adv__detalle_venta_producto dvp
                INNER JOIN adv__producto p ON dvp.id_producto = p.id_producto
                WHERE dvp.id_venta = @id_venta
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_venta", idVenta }
            };

            var detalles = new List<DetalleVentaProducto>();
            var resultados = ContextoBaseDatos.EjecutarConsulta(
                consulta,
                parametros,
                (reader) => {
                    var (detalle, entidadesExtra) = MapearEntidad(reader);
                    return (detalle, entidadesExtra);
                }
            );

            foreach (var (detalle, _) in resultados) {
                detalles.Add(detalle);
            }

            return detalles;
        }

        public decimal ObtenerCantidadVendidaPorProducto(long idProducto, DateTime? fechaInicio = null, DateTime? fechaFin = null) {
            var consulta = $"""
                SELECT COALESCE(SUM(dvp.cantidad), 0) as total_vendido
                FROM adv__detalle_venta_producto dvp
                INNER JOIN adv__venta v ON dvp.id_venta = v.id_venta
                WHERE dvp.id_producto = @id_producto
                AND v.estado_venta IN ('Completada', 'Entregada')
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_producto", idProducto }
            };

            if (fechaInicio.HasValue) {
                consulta += " AND v.fecha_venta >= @fecha_inicio";
                parametros.Add("@fecha_inicio", fechaInicio.Value.ToString("yyyy-MM-dd 00:00:00"));
            }

            if (fechaFin.HasValue) {
                consulta += " AND v.fecha_venta <= @fecha_fin";
                parametros.Add("@fecha_fin", fechaFin.Value.ToString("yyyy-MM-dd 23:59:59"));
            }

            var resultado = ContextoBaseDatos.EjecutarConsultaEscalar<decimal>(consulta, parametros);

            return resultado;
        }

        #endregion
    }
}