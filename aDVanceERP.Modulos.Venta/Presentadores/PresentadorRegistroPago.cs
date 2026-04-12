using aDVanceERP.Core.Eventos;
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

            foreach (MetodoPagoEnum metodo in Enum.GetValues(typeof(MetodoPagoEnum)))
                 metodosPago.Add(metodo.ObtenerDisplayName());            

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

            foreach (MetodoPagoEnum metodo in Enum.GetValues(typeof(MetodoPagoEnum)))
                metodosPago.Add(metodo.ObtenerDisplayName());

            Vista.CargarFacturasVentasPendientes([.. RepoVenta.Instancia.ObtenerVentasPendientesDePago().Select(v => v.NumeroFacturaTicket)]);
            Vista.CargarMetodosPago([.. metodosPago]);

            PopularVistaDesdeEntidad(pago);

            Vista.Mostrar();
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
                FechaConfirmacionPago = Vista.EstadoPendiente ? DateTime.MinValue : DateTime.Now,
                EstadoPago = Vista.EstadoPendiente ? EstadoPagoEnum.Pendiente : EstadoPagoEnum.Confirmado
            };
        }

        protected override void RegistroEdicionAuxiliar(RepoPago repositorio, long id) {
            if (Vista.MetodoPago == MetodoPagoEnum.TransferenciaBancaria) {
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
                    CanalPago = (CanalPagoCajaEnum) ((int) Entidad.MetodoPago),
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
        }
    }
}
