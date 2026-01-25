using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Ventas;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Ventas {
    public class RepoPedido : RepoEntidadBaseDatos<Pedido, FiltroBusquedaPedido> {
        public RepoPedido() : base("adv__pedido", "id_pedido") {
        }

        protected override string GenerarComandoAdicionar(Pedido entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
                INSERT INTO adv__pedido (
                    id_cliente,
                    id_empleado_vendedor,
                    fecha_pedido,
                    fecha_entrega_solicitada,
                    direccion_entrega,
                    total_pedido,
                    estado_pedido,
                    observaciones_pedido,
                    activo
                ) VALUES (
                    @id_cliente,
                    @id_empleado_vendedor,
                    @fecha_pedido,
                    @fecha_entrega_solicitada,
                    @direccion_entrega,
                    @total_pedido,
                    @estado_pedido,
                    @observaciones_pedido,
                    @activo
                );
                """;

            parametros = new Dictionary<string, object> {
                { "@id_cliente", entidad.IdCliente },
                { "@id_empleado_vendedor", entidad.IdEmpleadoVendedor.HasValue ? entidad.IdEmpleadoVendedor.Value : DBNull.Value },
                { "@fecha_pedido", entidad.FechaPedido.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@fecha_entrega_solicitada", entidad.FechaEntregaSolicitada.HasValue ? entidad.FechaEntregaSolicitada.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value },
                { "@direccion_entrega", entidad.DireccionEntrega },
                { "@total_pedido", entidad.TotalPedido.ToString(CultureInfo.InvariantCulture) },
                { "@estado_pedido", entidad.EstadoPedido.ToString() },
                { "@observaciones_pedido", entidad.ObservacionesPedido },
                { "@activo", entidad.Activo }
            };

            return comando;
        }

        protected override string GenerarComandoEditar(Pedido entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
                UPDATE adv__pedido 
                SET 
                    id_cliente = @id_cliente,
                    id_empleado_vendedor = @id_empleado_vendedor,
                    fecha_pedido = @fecha_pedido,
                    fecha_entrega_solicitada = @fecha_entrega_solicitada,
                    direccion_entrega = @direccion_entrega,
                    total_pedido = @total_pedido,
                    estado_pedido = @estado_pedido,
                    observaciones_pedido = @observaciones_pedido,
                    activo = @activo
                WHERE id_pedido = @id_pedido
                """;

            parametros = new Dictionary<string, object> {
                { "@id_pedido", entidad.Id },
                { "@id_cliente", entidad.IdCliente },
                { "@id_empleado_vendedor", entidad.IdEmpleadoVendedor.HasValue ? entidad.IdEmpleadoVendedor.Value : DBNull.Value },
                { "@fecha_pedido", entidad.FechaPedido.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@fecha_entrega_solicitada", entidad.FechaEntregaSolicitada.HasValue ? entidad.FechaEntregaSolicitada.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value },
                { "@direccion_entrega", entidad.DireccionEntrega },
                { "@total_pedido", entidad.TotalPedido.ToString(CultureInfo.InvariantCulture) },
                { "@estado_pedido", entidad.EstadoPedido.ToString() },
                { "@observaciones_pedido", entidad.ObservacionesPedido },
                { "@activo", entidad.Activo }
            };

            return comando;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var comando = $"""
                DELETE FROM adv__pedido 
                WHERE id_pedido = @id_pedido
                """;

            parametros = new Dictionary<string, object> {
                { "@id_pedido", id }
            };

            return comando;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaPedido filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var segundoCriterio = criteriosBusqueda.Length > 1 ? criteriosBusqueda[1] : string.Empty;

            var consultaComun = $"""
                SELECT p.*, c.nombre_completo as nombre_cliente
                FROM adv__pedido p
                LEFT JOIN adv__cliente cl ON p.id_cliente = cl.id_cliente
                LEFT JOIN adv__persona c ON cl.id_persona = c.id_persona
                WHERE p.activo = 1
                """;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaPedido.Id => $"""
                    {consultaComun}
                    AND p.id_pedido = @id_pedido
                    """,
                FiltroBusquedaPedido.IdCliente => $"""
                    {consultaComun}
                    AND p.id_cliente = @id_cliente
                    """,
                FiltroBusquedaPedido.FechaDesde => $"""
                    {consultaComun}
                    AND p.fecha_pedido >= @fecha_desde
                    AND p.fecha_pedido <= @fecha_hasta
                    """,
                FiltroBusquedaPedido.Estado => $"""
                    {consultaComun}
                    AND p.estado_pedido = @estado_pedido
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaPedido.Id => new Dictionary<string, object> {
                    { "@id_pedido", long.Parse(criterio) }
                },
                FiltroBusquedaPedido.IdCliente => new Dictionary<string, object> {
                    { "@id_cliente", long.Parse(criterio) }
                },
                FiltroBusquedaPedido.FechaDesde => new Dictionary<string, object> {
                    { "@fecha_desde", DateTime.Parse(criterio).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(segundoCriterio).ToString("yyyy-MM-dd 23:59:59") }
                },
                FiltroBusquedaPedido.Estado => new Dictionary<string, object> {
                    { "@estado_pedido", criterio }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (Pedido, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var pedido = new Pedido {
                Id = Convert.ToInt64(lector["id_pedido"]),
                IdCliente = Convert.ToInt64(lector["id_cliente"]),
                IdEmpleadoVendedor = lector["id_empleado_vendedor"] != DBNull.Value ? Convert.ToInt64(lector["id_empleado_vendedor"]) : null,
                FechaPedido = Convert.ToDateTime(lector["fecha_pedido"]),
                FechaEntregaSolicitada = lector["fecha_entrega_solicitada"] != DBNull.Value ? Convert.ToDateTime(lector["fecha_entrega_solicitada"]) : null,
                DireccionEntrega = lector["direccion_entrega"] != DBNull.Value ? Convert.ToString(lector["direccion_entrega"]) : null,
                TotalPedido = Convert.ToDecimal(lector["total_pedido"], CultureInfo.InvariantCulture),
                EstadoPedido = Enum.Parse<EstadoPedidoEnum>(Convert.ToString(lector["estado_pedido"]) ?? "Pendiente"),
                ObservacionesPedido = lector["observaciones_pedido"] != DBNull.Value ? Convert.ToString(lector["observaciones_pedido"]) : null,
                Activo = Convert.ToBoolean(lector["activo"])
            };

            var entidadesExtra = new List<IEntidadBaseDatos>();

            // Solo obtenemos el nombre del cliente de la BD
            if (lector.VisibleFieldCount > 9) {
                entidadesExtra.Add(new Persona {
                    NombreCompleto = Convert.ToString(lector["nombre_cliente"]) ?? "N/A"
                });
            }

            return (pedido, entidadesExtra);
        }

        #region STATIC
        public static RepoPedido Instancia { get; } = new RepoPedido();
        #endregion

        #region UTILES

        public bool CambiarEstadoPedido(long idPedido, EstadoPedidoEnum nuevoEstado) {
            var consulta = $"""
                UPDATE adv__pedido
                SET estado_pedido = @nuevo_estado
                WHERE id_pedido = @id_pedido;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_pedido", idPedido },
                { "@nuevo_estado", nuevoEstado.ToString() }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        public List<DetallePedidoProducto> ObtenerDetallesPedido(long idPedido) {
            var consulta = $"""
                SELECT dpp.*, pr.nombre as nombre_producto, pr.codigo as codigo_producto
                FROM adv__detalle_pedido_producto dpp
                LEFT JOIN adv__producto pr ON dpp.id_producto = pr.id_producto
                WHERE dpp.id_pedido = @id_pedido
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_pedido", idPedido }
            };

            var detalles = new List<DetallePedidoProducto>();
            var resultados = ContextoBaseDatos.EjecutarConsulta(
                consulta,
                parametros,
                MapearEntidadDetallePedidoProducto
            );

            foreach (var (detalle, _) in resultados) {
                detalles.Add(detalle);
            }

            return detalles;
        }

        private (DetallePedidoProducto, List<IEntidadBaseDatos>) MapearEntidadDetallePedidoProducto(MySqlDataReader lector) {
            var detalle = new DetallePedidoProducto {
                Id = Convert.ToInt64(lector["id_detalle_pedido_producto"]),
                IdPedido = Convert.ToInt64(lector["id_pedido"]),
                IdProducto = Convert.ToInt64(lector["id_producto"]),
                CantidadSolicitada = Convert.ToDecimal(lector["cantidad_solicitada"], CultureInfo.InvariantCulture),
                PrecioVentaReferencia = Convert.ToDecimal(lector["precio_venta_referencia"], CultureInfo.InvariantCulture),
                Subtotal = Convert.ToDecimal(lector["subtotal"], CultureInfo.InvariantCulture)
            };

            var entidadesExtra = new List<IEntidadBaseDatos>();

            if (lector.VisibleFieldCount > 6) {
                entidadesExtra.Add(new Producto {
                    Nombre = Convert.ToString(lector["nombre_producto"]) ?? "N/A",
                    Codigo = Convert.ToString(lector["codigo_producto"]) ?? "N/A"
                });
            }

            return (detalle, entidadesExtra);
        }

        public decimal CalcularTotalPedido(long idPedido) {
            var consulta = $"""
                SELECT COALESCE(SUM(subtotal), 0) as total
                FROM adv__detalle_pedido_producto
                WHERE id_pedido = @id_pedido
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_pedido", idPedido }
            };

            var resultado = ContextoBaseDatos.EjecutarConsultaEscalar<decimal>(consulta, parametros);
            return resultado;
        }

        #endregion
    }
}