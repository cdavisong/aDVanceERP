using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Venta {
    public class RepoPedido : RepoEntidadBaseDatos<Pedido, FiltroBusquedaPedido> {
        public RepoPedido() : base("adv__pedido", "id_pedido") {
        }

        protected override string GenerarComandoAdicionar(Pedido entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
                INSERT INTO adv__pedido (
                    codigo,
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
                    @codigo,
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
                { "@codigo", entidad.Codigo },
                { "@id_empleado_vendedor", entidad.IdEmpleadoVendedor.HasValue ? entidad.IdEmpleadoVendedor.Value : DBNull.Value },
                { "@fecha_pedido", entidad.FechaPedido.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@fecha_entrega_solicitada", entidad.FechaEntregaSolicitada.HasValue ? entidad.FechaEntregaSolicitada.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value },
                { "@direccion_entrega", entidad.DireccionEntrega },
                { "@total_pedido", entidad.TotalPedido },
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
                    codigo = @codigo,
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
                { "@codigo", entidad.Codigo },
                { "@id_cliente", entidad.IdCliente },
                { "@id_empleado_vendedor", entidad.IdEmpleadoVendedor.HasValue ? entidad.IdEmpleadoVendedor.Value : DBNull.Value },
                { "@fecha_pedido", entidad.FechaPedido.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@fecha_entrega_solicitada", entidad.FechaEntregaSolicitada.HasValue ? entidad.FechaEntregaSolicitada.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value },
                { "@direccion_entrega", entidad.DireccionEntrega },
                { "@total_pedido", entidad.TotalPedido },
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
            var fechaDesde = criteriosBusqueda.Length == 3 ? criteriosBusqueda[0] : DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var fechaHasta = criteriosBusqueda.Length == 3 ? criteriosBusqueda[1] : DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var criterio = criteriosBusqueda.Length == 3 ? criteriosBusqueda[2] : criteriosBusqueda .Length > 0 ? criteriosBusqueda[0] : string.Empty;

            var consultaComun = $"""
                SELECT p.*, c.nombre_completo as nombre_cliente
                FROM adv__pedido p
                LEFT JOIN adv__cliente cl ON p.id_cliente = cl.id_cliente
                LEFT JOIN adv__persona c ON cl.id_persona = c.id_persona
                WHERE p.activo = @activo 
                { (criteriosBusqueda.Length == 3 ? "AND p.fecha_pedido >= @fecha_desde AND p.fecha_pedido <= @fecha_hasta" : string.Empty) }
                """;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaPedido.Id => $"""
                    {consultaComun}
                    AND p.id_pedido = @id_pedido
                    """,
                FiltroBusquedaPedido.Codigo => $"""
                    {consultaComun}
                    AND p.codigo = @codigo
                    """,
                FiltroBusquedaPedido.IdCliente => $"""
                    {consultaComun}
                    AND p.id_cliente = @id_cliente
                    """,
                FiltroBusquedaPedido.Estado => $"""
                    {consultaComun}
                    AND p.estado_pedido = @estado_pedido
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaPedido.Id => new Dictionary<string, object> {
                    { "@id_pedido", long.Parse(criterio) },
                    { "@activo", !filtroBusqueda.ToString().Equals("Inactivos", StringComparison.OrdinalIgnoreCase) },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                },
                FiltroBusquedaPedido.Codigo => new Dictionary<string, object> {
                    { "@codigo", criterio },
                    { "@activo", !filtroBusqueda.ToString().Equals("Inactivos", StringComparison.OrdinalIgnoreCase) },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                },
                FiltroBusquedaPedido.IdCliente => new Dictionary<string, object> {
                    { "@id_cliente", long.Parse(criterio) },
                    { "@activo", !filtroBusqueda.ToString().Equals("Inactivos", StringComparison.OrdinalIgnoreCase) },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                },
                FiltroBusquedaPedido.Estado => new Dictionary<string, object> {
                    { "@estado_pedido", criterio },
                    { "@activo", !filtroBusqueda.ToString().Equals("Inactivos", StringComparison.OrdinalIgnoreCase) },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                },
                _ => new Dictionary<string, object> {
                    { "@activo", !filtroBusqueda.ToString().Equals("Inactivos", StringComparison.OrdinalIgnoreCase) },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                }
            };

            return consulta;
        }

        protected override (Pedido, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var pedido = new Pedido {
                Id = Convert.ToInt64(lector["id_pedido"]),
                Codigo = Convert.ToString(lector["codigo"]) ?? "00000000000000",
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

        public bool HabilitarDeshabilitarPedido(long id) {
            var consulta = $"""
                UPDATE adv__pedido
                SET activo = NOT activo
                WHERE id_pedido = @IdPedido;
                """;
            var parametros = new Dictionary<string, object> {
                { "@IdPedido", id }
            };

            ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);

            consulta = $"""
                SELECT activo
                FROM adv__pedido
                WHERE id_pedido = @IdPedido;
                """;

            return ContextoBaseDatos.EjecutarConsultaEscalar<bool>(consulta, parametros);
        }

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

        public string[] ObtenerCodigosPedidosConfirmados() {
            var consulta = $"""
                SELECT codigo
                FROM adv__pedido
                WHERE estado_pedido = @estado_pedido;
                """;
            var parametros = new Dictionary<string, object> {
                { "@estado_pedido", EstadoPedidoEnum.Confirmado.ToString() }
            };

            return ContextoBaseDatos.EjecutarConsulta(consulta, parametros, MapearCodigoPedido).Select(result => result.entidadBase).ToArray() ?? [];
        }

        private (string, List<IEntidadBaseDatos>) MapearCodigoPedido(MySqlDataReader lector) {
            return (Convert.ToString(lector["codigo"]) ?? string.Empty, []);
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
            var consulta = """
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

        public (Dictionary<int, (decimal disponible, decimal comprometido, decimal solicitado, string nombre, string codigo)> disponibilidad, bool todosDisponibles) VerificarDisponibilidadPedidoCompleta(long idPedido, long idAlmacenVenta) {
            var consulta = """
                -- Obtener productos del pedido con sus cantidades
                WITH productos_pedido AS (
                    SELECT 
                        dpp.id_producto,
                        dpp.cantidad_solicitada,
                        p.nombre,
                        p.codigo,
                        um.abreviatura as unidad_medida
                    FROM adv__detalle_pedido_producto dpp
                    INNER JOIN adv__producto p ON dpp.id_producto = p.id_producto
                    LEFT JOIN adv__unidad_medida um ON p.id_unidad_medida = um.id_unidad_medida
                    WHERE dpp.id_pedido = @idPedido
                    AND p.activo = 1
                )
                -- Combinar con disponibilidad en inventario
                SELECT 
                    pp.id_producto,
                    pp.cantidad_solicitada,
                    pp.nombre,
                    pp.codigo,
                    pp.unidad_medida,
                    COALESCE(i.cantidad, 0) as disponible_inventario,
                    COALESCE((
                        SELECT SUM(dpp2.cantidad_solicitada)
                        FROM adv__detalle_pedido_producto dpp2
                        INNER JOIN adv__pedido p2 ON dpp2.id_pedido = p2.id_pedido
                        WHERE dpp2.id_producto = pp.id_producto
                        AND p2.estado_pedido IN ('Pendiente', 'Confirmado', 'Preparando')
                        AND p2.activo = 1
                        AND p2.id_pedido != @idPedido  -- Excluir este pedido
                    ), 0) as comprometido_otros_pedidos
                FROM productos_pedido pp
                LEFT JOIN adv__inventario i ON pp.id_producto = i.id_producto 
                    AND i.id_almacen = @idAlmacenVenta
                """;

            var parametros = new Dictionary<string, object> {
                { "@idPedido", idPedido },
                { "@idAlmacenVenta", idAlmacenVenta }
            };

            var resultados = new Dictionary<int, (decimal disponible, decimal comprometido, decimal solicitado, string nombre, string codigo)>();
            bool todosDisponibles = true;

            using (var connection = ContextoBaseDatos.ObtenerConexionOptimizada()) {
                connection.Open();

                using (var command = new MySqlCommand(consulta, connection)) {
                    foreach (var param in parametros)
                        command.Parameters.AddWithValue(param.Key, param.Value);

                    using (var lector = command.ExecuteReader()) {
                        while (lector.Read()) {
                            var idProducto = Convert.ToInt32(lector["id_producto"]);
                            var cantidadSolicitada = Convert.ToDecimal(lector["cantidad_solicitada"]);
                            var disponible = Convert.ToDecimal(lector["disponible_inventario"]);
                            var comprometido = Convert.ToDecimal(lector["comprometido_otros_pedidos"]);
                            var nombre = Convert.ToString(lector["nombre"]) ?? "N/A";
                            var codigo = Convert.ToString(lector["codigo"]) ?? "00000000000000";

                            var stockReal = disponible - comprometido;

                            resultados[idProducto] = (disponible, comprometido, cantidadSolicitada, nombre, codigo);

                            if (cantidadSolicitada > stockReal)
                                todosDisponibles = false;
                        }
                    }
                }
            }

            return (resultados, todosDisponibles);
        }

        #endregion
    }
}