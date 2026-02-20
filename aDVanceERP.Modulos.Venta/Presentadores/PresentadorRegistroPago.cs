using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    internal class PresentadorRegistroPago : PresentadorVistaRegistro<IVistaRegistroPago, Pago, RepoPago, FiltroBusquedaPago> {
        public PresentadorRegistroPago(IVistaRegistroPago vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroPago", OnMostrarVistaRegistroPago);
            AgregadorEventos.Suscribir("MostrarVistaEdicionPago", OnMostrarVistaEdicionPago);
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

            return new Pago() { 
                Id = 0,
                IdVenta = venta.Id,
                MetodoPago = Vista.MetodoPago,
                MontoPagado = Vista.MontoPagado,
                FechaPagoCliente = Vista.FechaPagoCliente,
                FechaConfirmacionPago = Vista.EstadoPendiente ? DateTime.MinValue : DateTime.Now,
                EstadoPago = Vista.EstadoPendiente ? EstadoPagoEnum.Pendiente : EstadoPagoEnum.Confirmado
            };
        }

        protected override void RegistroEdicionAuxiliar(RepoPago repositorio, long id) {
            if (Vista.MetodoPago == MetodoPagoEnum.TransferenciaBancaria) {
                var repoDetallePagoTransferencia = RepoDetallePagoTransferencia.Instancia;

                // Registrar detalles del la transferencia bancaria
                var detallePagoTransferencia = new DetallePagoTransferencia() {
                    Id = 0,
                    IdPago = id,
                    NumeroConfirmacion = Vista.NumeroConfirmacion,
                    NumeroTransaccion = Vista.NumeroTransaccion,
                    MontoTransferencia = Vista.MontoPagado
                };

                repoDetallePagoTransferencia.Adicionar(detallePagoTransferencia);
            }

            // Verificar si el pago actual satisface la venta y completar la misma
            var repoVenta = RepoVenta.Instancia;
            var venta = repoVenta.Buscar(FiltroBusquedaVenta.Id, Entidad?.IdVenta.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;

            if (repoVenta.VentaEstaPagadaCompletamente(venta.Id))
                repoVenta.CambiarEstadoVenta(venta.Id, EstadoVentaEnum.Completada);

            // Actualizar el método de pago principal de la venta
            repoVenta.ActualizarMetodoPagoPrincipal(venta.Id);
        }
    }
}
