using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Compra;
using aDVanceERP.Modulos.Compra.Interfaces;

namespace aDVanceERP.Modulos.Compra.Presentadores {
    internal class PresentadorRegistroPago : PresentadorVistaRegistro<IVistaRegistroPago, Pago, RepoPago, FiltroBusquedaPago> {
        public PresentadorRegistroPago(IVistaRegistroPago vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroPagoCompra", OnMostrarVistaRegistroPago);
            AgregadorEventos.Suscribir("MostrarVistaEdicionPagoCompra", OnMostrarVistaEdicionPago);
        }

        private void OnMostrarVistaRegistroPago(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();

            // Carga inicial de datos
            var metodosPago = new List<string>();

            foreach (MetodoPagoEnum metodo in Enum.GetValues(typeof(MetodoPagoEnum)))
                 metodosPago.Add(metodo.ObtenerDisplayName());            

            //Vista.CargarSolicitudesComprasPendientes([.. RepoCompra.Instancia.ObtenerComprasPendientesDePago().Select(v => v.NumeroFacturaTicket)]);
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

            //Vista.CargarSolicitudesComprasPendientes([.. RepoCompra.Instancia.ObtenerComprasPendientesDePago().Select(v => v.NumeroFacturaTicket)]);
            Vista.CargarMetodosPago([.. metodosPago]);

            PopularVistaDesdeEntidad(pago);

            Vista.Mostrar();
        }

        protected override Pago? ObtenerEntidadDesdeVista() {
            var compra = RepoCompra.Instancia.Buscar(FiltroBusquedaCompra.IdSolicitudCompra, Vista.NumeroSolicitudCompra).resultadosBusqueda.FirstOrDefault().entidadBase;

            return new Pago() { 
                Id = 0,
                IdCompra = compra.Id,
                MetodoPago = Vista.MetodoPago,
                MontoPagado = Vista.MontoPagado,
                FechaPago = Vista.FechaPagoProveedor,
                FechaConfirmacionPago = DateTime.Now,
                EstadoPago = EstadoPagoEnum.Confirmado
            };
        }

        protected override void RegistroEdicionAuxiliar(RepoPago repositorio, long id) {
            if (Vista.MetodoPago == MetodoPagoEnum.TransferenciaBancaria) {
                var repoDetallePagoTransferencia = RepoDetallePagoTransferencia.Instancia;

                // Registrar detalles del la transferencia bancaria
                var detallePagoTransferencia = new DetallePagoTransferencia() {
                    Id = 0,
                    IdPago = id,
                    NumeroTelefonoConfirmacion = Vista.NumeroTelefonoConfirmacion,
                    NumeroTransaccion = Vista.NumeroTransaccion,
                    MontoTransferencia = Vista.MontoPagado
                };

                repoDetallePagoTransferencia.Adicionar(detallePagoTransferencia);
            }

            // Verificar si el pago actual satisface la compra y completar la misma
            var repoCompra = RepoCompra.Instancia;
            var compra = repoCompra.Buscar(FiltroBusquedaCompra.Id, Entidad?.IdCompra.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;

            //if (repoCompra.CompraEstaPagadaCompletamente(compra.Id))
            //    repoCompra.CambiarEstadoCompra(compra.Id, EstadoCompraEnum.Completada);

            // Actualizar el método de pago principal de la compra
            //repoCompra.ActualizarMetodoPagoPrincipal(compra.Id);
        }
    }
}
