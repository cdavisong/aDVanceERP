using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.BD;
using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Venta {
    public class RepoDetallePagoTransferencia : RepoEntidadBaseDatos<DetallePagoTransferencia, FiltroBusquedaDetalleTransferencia> {
        public RepoDetallePagoTransferencia() : base("adv__detalle_pago_transferencia", "id_detalle_pago_transferencia") {
        }

        protected override string GenerarComandoAdicionar(DetallePagoTransferencia entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
                INSERT INTO adv__detalle_pago_transferencia (
                    id_pago,
                    numero_confirmacion,
                    numero_transaccion,
                    monto_transferencia
                ) VALUES (
                    @id_pago,
                    @numero_confirmacion,
                    @numero_transaccion,
                    @monto_transferencia
                );
                """;

            parametros = new Dictionary<string, object> {
                { "@id_pago", entidad.IdPago },
                { "@numero_confirmacion", entidad.NumeroConfirmacion },
                { "@numero_transaccion", entidad.NumeroTransaccion },
                { "@monto_transferencia", entidad.MontoTransferencia.ToString(CultureInfo.InvariantCulture) }
            };

            return comando;
        }

        protected override string GenerarComandoEditar(DetallePagoTransferencia entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
                UPDATE adv__detalle_pago_transferencia 
                SET 
                    id_pago = @id_pago,
                    numero_confirmacion = @numero_confirmacion,
                    numero_transaccion = @numero_transaccion,
                    monto_transferencia = @monto_transferencia
                WHERE id_detalle_pago_transferencia = @id_detalle
                """;

            parametros = new Dictionary<string, object> {
                { "@id_detalle", entidad.Id },
                { "@id_pago", entidad.IdPago },
                { "@numero_confirmacion", entidad.NumeroConfirmacion },
                { "@numero_transaccion", entidad.NumeroTransaccion },
                { "@monto_transferencia", entidad.MontoTransferencia.ToString(CultureInfo.InvariantCulture) }
            };

            return comando;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var comando = $"""
                DELETE FROM adv__detalle_pago_transferencia 
                WHERE id_detalle_pago_transferencia = @id_detalle
                """;

            parametros = new Dictionary<string, object> {
                { "@id_detalle", id }
            };

            return comando;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaDetalleTransferencia filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;

            var consultaComun = $"""
                SELECT dpt.*, p.metodo_pago, p.estado_pago, p.monto_pagado
                FROM adv__detalle_pago_transferencia dpt
                LEFT JOIN adv__pago p ON dpt.id_pago = p.id_pago
                """;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaDetalleTransferencia.PorPago => $"""
                    {consultaComun}
                    WHERE dpt.id_pago = @id_pago
                    """,
                FiltroBusquedaDetalleTransferencia.PorNumeroConfirmacion => $"""
                    {consultaComun}
                    WHERE dpt.numero_confirmacion = @numero_confirmacion
                    """,
                FiltroBusquedaDetalleTransferencia.PorNumeroTransaccion => $"""
                    {consultaComun}
                    WHERE dpt.numero_transaccion = @numero_transaccion
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaDetalleTransferencia.PorPago => new Dictionary<string, object> {
                    { "@id_pago", long.Parse(criterio) }
                },
                FiltroBusquedaDetalleTransferencia.PorNumeroConfirmacion => new Dictionary<string, object> {
                    { "@numero_confirmacion", criterio }
                },
                FiltroBusquedaDetalleTransferencia.PorNumeroTransaccion => new Dictionary<string, object> {
                    { "@numero_transaccion", criterio }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (DetallePagoTransferencia, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var detalle = new DetallePagoTransferencia {
                Id = Convert.ToInt64(lector["id_detalle_pago_transferencia"]),
                IdPago = Convert.ToInt64(lector["id_pago"]),
                NumeroConfirmacion = Convert.ToString(lector["numero_confirmacion"]) ?? "",
                NumeroTransaccion = Convert.ToString(lector["numero_transaccion"]) ?? "",
                MontoTransferencia = Convert.ToDecimal(lector["monto_transferencia"], CultureInfo.InvariantCulture)
            };

            var entidadesExtra = new List<IEntidadBaseDatos>();

            if (lector.VisibleFieldCount > 5) {
                entidadesExtra.Add(new Pago {
                    MetodoPago = Enum.Parse<MetodoPagoEnum>(Convert.ToString(lector["metodo_pago"]) ?? "Efectivo"),
                    EstadoPago = Enum.Parse<EstadoPagoEnum>(Convert.ToString(lector["estado_pago"]) ?? "Pendiente"),
                    MontoPagado = Convert.ToDecimal(lector["monto_pagado"], CultureInfo.InvariantCulture)
                });
            }

            return (detalle, entidadesExtra);
        }

        #region STATIC

        public static RepoDetallePagoTransferencia Instancia { get; } = new RepoDetallePagoTransferencia();

        #endregion

        #region UTILES

        public DetallePagoTransferencia? ObtenerPorPago(long idPago) {
            var consulta = $"""
                SELECT * FROM adv__detalle_pago_transferencia
                WHERE id_pago = @id_pago
                LIMIT 1;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_pago", idPago }
            };

            var resultados = ContextoBaseDatos.EjecutarConsulta(
                consulta,
                parametros,
                (reader) => {
                    var (detalle, entidadesExtra) = MapearEntidad(reader);
                    return (detalle, entidadesExtra);
                }
            ).FirstOrDefault();

            return resultados.entidadBase;
        }

        public bool ExisteNumeroConfirmacion(string numeroConfirmacion) {
            var consulta = $"""
                SELECT COUNT(*) 
                FROM adv__detalle_pago_transferencia
                WHERE numero_confirmacion = @numero_confirmacion;
                """;

            var parametros = new Dictionary<string, object> {
                { "@numero_confirmacion", numeroConfirmacion }
            };

            var resultado = ContextoBaseDatos.EjecutarConsultaEscalar<int>(consulta, parametros);
            return resultado > 0;
        }

        public bool ExisteNumeroTransaccion(string numeroTransaccion) {
            var consulta = $"""
                SELECT COUNT(*) 
                FROM adv__detalle_pago_transferencia
                WHERE numero_transaccion = @numero_transaccion;
                """;

            var parametros = new Dictionary<string, object> {
                { "@numero_transaccion", numeroTransaccion }
            };

            var resultado = ContextoBaseDatos.EjecutarConsultaEscalar<int>(consulta, parametros);
            return resultado > 0;
        }

        #endregion
    }
}