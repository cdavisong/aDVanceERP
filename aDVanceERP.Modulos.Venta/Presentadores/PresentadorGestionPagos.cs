using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;
using aDVanceERP.Modulos.Venta.Vistas;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    internal class PresentadorGestionPagos : PresentadorVistaGestion<PresentadorTuplaPago, IVistaGestionPagos, IVistaTuplaPago, Pago, RepoPago, FiltroBusquedaPago> {
        private List<VentaPendientePago> _ventasPendientesPago = new List<VentaPendientePago>();

        public PresentadorGestionPagos(IVistaGestionPagos vista) : base(vista) {
            RegistrarEntidad += OnRegistrarPago;
            EditarEntidad += OnEditarPago;

            AgregadorEventos.Suscribir("MostrarVistaGestionPagos", OnMostrarVistaGestionPagos);
        }

        private void OnRegistrarPago(object? sender, EventArgs e) {
            _ventasPendientesPago = RepoVenta.Instancia.ObtenerVentasPendientesDePago();

            if (_ventasPendientesPago.Count == 0) {
                CentroNotificaciones.MostrarNotificacion("No es posible registrar un nuevo pago puesto que no existen ventas pendientes por pago en el sistema.", TipoNotificacion.Advertencia);
                return;
            }

            AgregadorEventos.Publicar("MostrarVistaRegistroPago", string.Empty);
        }

        private void OnEditarPago(object? sender, Pago e) {
            _ventasPendientesPago = RepoVenta.Instancia.ObtenerVentasPendientesDePago();

            AgregadorEventos.Publicar("MostrarVistaEdicionPago", AgregadorEventos.SerializarPayload(e));
        }

        private void OnMostrarVistaGestionPagos(string obj) {
            Vista.CargarFiltrosBusqueda(UtilesBusquedaPago.FiltroBusquedaPago);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        protected override PresentadorTuplaPago ObtenerValoresTupla(Pago entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaPago(new VistaTuplaPago(), entidad);
            var venta = RepoVenta.Instancia.ObtenerPorId(entidad.IdVenta);
            var detallePagoTransferencia = RepoDetallePagoTransferencia.Instancia.Buscar(FiltroBusquedaDetalleTransferencia.PorPago, entidad.Id.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.IdVenta = entidad.IdVenta;
            presentadorTupla.Vista.NumeroFacturaVenta = venta?.NumeroFacturaTicket ?? "-";
            presentadorTupla.Vista.MetodoPago = entidad.MetodoPago;
            presentadorTupla.Vista.NumeroConfirmacion = detallePagoTransferencia?.NumeroConfirmacion ?? string.Empty;
            presentadorTupla.Vista.NumeroTransaccion = detallePagoTransferencia?.NumeroTransaccion ?? string.Empty;
            presentadorTupla.Vista.MontoPagado = entidad.MontoPagado;
            presentadorTupla.Vista.FechaPagoCliente = entidad.FechaPagoCliente ?? DateTime.MinValue;
            presentadorTupla.Vista.FechaConfirmacionPago = entidad.FechaConfirmacionPago ?? DateTime.MinValue;
            presentadorTupla.Vista.EstadoPago = entidad.EstadoPago;

            return presentadorTupla;
        }
    }
}
