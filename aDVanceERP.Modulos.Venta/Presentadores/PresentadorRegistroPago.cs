using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Extension.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Caja;
using aDVanceERP.Core.Repositorios.Modulos.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    internal class PresentadorRegistroPago : PresentadorVistaRegistro<IVistaRegistroPago, Pago, RepoPago, FiltroBusquedaPago> {
        public PresentadorRegistroPago(IVistaRegistroPago vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroPagoVenta", OnMostrarVistaRegistroPago);
            AgregadorEventos.Suscribir("MostrarVistaEdicionPagoVenta", OnMostrarVistaEdicionPago);
        }

        private void OnMostrarVistaRegistroPago(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();

            // Carga inicial de datos
            var metodosPago = new List<string>();

            foreach (CanalPagoEnum metodo in Enum.GetValues(typeof(CanalPagoEnum)))
                 metodosPago.Add(metodo.ObtenerNombreDescripcion().Nombre);            

            Vista.CargarFacturasVentasPendientes([.. RepoVenta.Instancia.ObtenerVentasPendientesDePago().Select(v => v.NumeroFacturaTicket)]);
            Vista.CargarMetodosPago([.. metodosPago]);

            Vista.Mostrar();
        }

        private void OnMostrarVistaEdicionPago(string obj) {
            Vista.ModoEdicion = true;
            Vista.Restaurar();

            if (string.IsNullOrEmpty(obj))
                return;

            var pago = AgregadorEventos.DeserializarPayload<Pago>(obj);

            if (pago == null)
                return;

            var metodosPago = new List<string>();

            foreach (CanalPagoEnum metodo in Enum.GetValues(typeof(CanalPagoEnum)))
                metodosPago.Add(metodo.ObtenerNombreDescripcion().Nombre);

            Vista.CargarFacturasVentasPendientes([.. RepoVenta.Instancia.ObtenerVentasPendientesDePago().Select(v => v.NumeroFacturaTicket)]);
            Vista.CargarMetodosPago([.. metodosPago]);

            PopularVistaDesdeEntidad(pago);

            Vista.Mostrar();
        }

        public override void PopularVistaDesdeEntidad(Pago pago) {
            Vista.NumeroFacturaVenta = RepoVenta.Instancia
                .ObtenerPorId(pago.IdVenta)?.NumeroFacturaTicket ?? string.Empty;

            Vista.FechaPagoCliente = pago.FechaPago ?? DateTime.Now;
            Vista.MetodoPago = pago.MetodoPago;
            Vista.MontoPagado = pago.MontoPagado;

            if (pago.MetodoPago == CanalPagoEnum.TransferenciaBancaria) {
                var detalle = RepoDetallePagoTransferencia.Instancia
                    .Buscar(FiltroBusquedaDetalleTransferencia.PorPago, pago.Id.ToString())
                    .resultadosBusqueda.FirstOrDefault().entidadBase;

                if (detalle != null) {
                    Vista.NumeroTelefonoRemitente = detalle.NumeroTelefonoConfirmacion;
                    Vista.NumeroTransaccion = detalle.NumeroTransaccion;
                }
            }
        }

        protected override Pago? ObtenerEntidadDesdeVista() {
            var venta = RepoVenta.Instancia.Buscar(FiltroBusquedaVenta.NumeroFactura, Vista.NumeroFacturaVenta).resultadosBusqueda.FirstOrDefault().entidadBase;

            // Validar que el monto no exceda el saldo pendiente (evitar sobrepago)
            var estadoPago = RepoVenta.Instancia.VerificarEstadoPagoVenta(venta.Id);

            if (Vista.MontoPagado > estadoPago.Saldo && !Vista.ModoEdicion) {
                CentroNotificaciones.MostrarNotificacion(
                    $"El monto del pago ({Vista.MontoPagado:C2}) excede el saldo pendiente de {estadoPago.Saldo:C2}.",
                    TipoNotificacionEnum.Advertencia);
                return null;
            }

            return new Pago() { 
                Id = 0,
                IdVenta = venta.Id,
                MetodoPago = Vista.MetodoPago,
                MontoPagado = Vista.MontoPagado,
                FechaPago = Vista.FechaPagoCliente,
                FechaConfirmacionPago = DateTime.Now,
                EstadoPago = EstadoPagoEnum.Confirmado
            };
        }

        protected override bool EntidadCorrecta() {
            if (string.IsNullOrEmpty(Vista.NumeroFacturaVenta)) {
                CentroNotificaciones.MostrarNotificacion(
                    "Debe seleccionar una factura para registrar el pago.",
                    TipoNotificacionEnum.Advertencia);
                return false;
            }

            if (Vista.MontoPagado <= 0) {
                CentroNotificaciones.MostrarNotificacion(
                    "El monto del pago debe ser mayor a cero.",
                    TipoNotificacionEnum.Advertencia);
                return false;
            }

            if (Vista.MetodoPago == CanalPagoEnum.TransferenciaBancaria) {
                if (string.IsNullOrWhiteSpace(Vista.NumeroTransaccion)) {
                    CentroNotificaciones.MostrarNotificacion(
                        "Debe ingresar el número de transacción para pagos por transferencia.",
                        TipoNotificacionEnum.Advertencia);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(Vista.NumeroTelefonoRemitente)) {
                    CentroNotificaciones.MostrarNotificacion(
                        "Debe ingresar el número de teléfono del remitente.",
                        TipoNotificacionEnum.Advertencia);
                    return false;
                }
            }

            return true;
        }

        protected override void RegistroEdicionAuxiliar(RepoPago repositorio, long id) {
            if (Vista.MetodoPago == CanalPagoEnum.TransferenciaBancaria) {
                var repoDetallePagoTransferencia = RepoDetallePagoTransferencia.Instancia;

                // Validar que el número de transacción no esté duplicado
                if (!string.IsNullOrEmpty(Vista.NumeroTransaccion) && repoDetallePagoTransferencia.ExisteNumeroTransaccion(Vista.NumeroTransaccion)) {
                    CentroNotificaciones.MostrarNotificacion(
                        $"El número de transacción '{Vista.NumeroTransaccion}' ya ha sido registrado anteriormente. Verifique si el pago ya fue ingresado.",
                        TipoNotificacionEnum.Advertencia);
                    return;
                }

                // Registrar detalles del la transferencia bancaria
                var detallePagoTransferencia = new DetallePagoTransferencia() {
                    Id = 0,
                    IdPago = id,
                    NumeroTelefonoConfirmacion = Vista.NumeroTelefonoRemitente,
                    NumeroTransaccion = Vista.NumeroTransaccion,
                    MontoTransferencia = Vista.MontoPagado
                };

                repoDetallePagoTransferencia.Adicionar(detallePagoTransferencia);
            }

            // Verificar si el pago actual satisface la venta y completar la misma
            var repoVenta = RepoVenta.Instancia;
            var repoCaja = RepoCajaTurno.Instancia;
            var repoMovimientoCaja = RepoCajaMovimiento.Instancia;
            var venta = repoVenta.Buscar(FiltroBusquedaVenta.Id, Entidad?.IdVenta.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;
            var turno = repoCaja.ObtenerTurnoAbierto(venta.IdAlmacen);

            // Registrar pago de venta en caja automáticamente
            if (ContextoModulos.NombresModulosCargados.Exists(nm => nm.Equals("MOD_CAJA"))
                && turno != null) {
                var movimiento = new CajaMovimiento() {
                    Id = 0,
                    IdTurno = turno.Id,
                    Tipo = TipoMovimientoCajaEnum.Venta,
                    CanalPago = Entidad.MetodoPago switch {
                        CanalPagoEnum.Efectivo => CanalPagoCajaEnum.Efectivo,
                        CanalPagoEnum.TransferenciaBancaria => CanalPagoCajaEnum.Transferencia,
                        CanalPagoEnum.Mixto => CanalPagoCajaEnum.Mixto,
                        _ => CanalPagoCajaEnum.NA
                    },
                    IdVenta = venta.Id,
                    Monto = Entidad.MontoPagado,
                    Descripcion = $"Pago de factura {venta.NumeroFacturaTicket}",
                    IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                    FechaMovimiento = Entidad.FechaConfirmacionPago ?? DateTime.Now,
                };

                repoMovimientoCaja.Adicionar(movimiento);
            }

            if (repoVenta.VentaEstaPagadaCompletamente(venta.Id))
                repoVenta.CambiarEstadoVenta(venta.Id, EstadoVentaEnum.Completada);

            // Actualizar el método de pago principal de la venta
            repoVenta.ActualizarMetodoPagoPrincipal(venta.Id);

            // Notificar a la gestión de pagos para que refresque su lista
            AgregadorEventos.Publicar("PagoVentaRegistrado", string.Empty);
        }
    }
}
