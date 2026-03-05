using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Compra;
using aDVanceERP.Modulos.Compra.Interfaces;
using aDVanceERP.Modulos.Compra.Vistas;

namespace aDVanceERP.Modulos.Compra.Presentadores {
    internal class PresentadorGestionPagos : PresentadorVistaGestion<PresentadorTuplaPago, IVistaGestionPagos, IVistaTuplaPago, Pago, RepoPago, FiltroBusquedaPago> {
        //private List<CompraPendientePago> _comprasPendientesPago = new List<CompraPendientePago>();

        public PresentadorGestionPagos(IVistaGestionPagos vista) : base(vista) {
            RegistrarEntidad += OnRegistrarPago;
            EditarEntidad += OnEditarPago;

            AgregadorEventos.Suscribir("MostrarVistaGestionPagosCompra", OnMostrarVistaGestionPagosCompra);
        }

        private void OnRegistrarPago(object? sender, EventArgs e) {
            //_comprasPendientesPago = RepoCompra.Instancia.ObtenerComprasPendientesDePago();

            //if (_comprasPendientesPago.Count == 0) {
            //    CentroNotificaciones.MostrarNotificacion("No es posible registrar un nuevo pago puesto que no existen compras pendientes por pago en el sistema.", TipoNotificacion.Advertencia);
            //    return;
            //}

            AgregadorEventos.Publicar("MostrarVistaRegistroPagoCompra", string.Empty);
        }

        private void OnEditarPago(object? sender, Pago e) {
            //_comprasPendientesPago = RepoCompra.Instancia.ObtenerComprasPendientesDePago();

            AgregadorEventos.Publicar("MostrarVistaEdicionPagoCompra", AgregadorEventos.SerializarPayload(e));
        }

        private void OnMostrarVistaGestionPagosCompra(string obj) {
            Vista.CargarFiltrosBusqueda(UtilesBusquedaPago.FiltroBusquedaPagoCompra);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        public override void ActualizarResultadosBusqueda() {
            if (FiltroBusqueda == FiltroBusquedaPago.Todos && (CriteriosBusqueda == null || CriteriosBusqueda.Length == 0))
                CriteriosBusqueda = [DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Today.ToString("yyyy-MM-dd"), string.Empty, "Compra"];            

            base.ActualizarResultadosBusqueda();
        }

        protected override PresentadorTuplaPago ObtenerValoresTupla(Pago entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaPago(new VistaTuplaPago(), entidad);
            var compra = RepoCompra.Instancia.ObtenerPorId(entidad.IdCompra);
            var detallePagoTransferencia = RepoDetallePagoTransferencia.Instancia.Buscar(FiltroBusquedaDetalleTransferencia.PorPago, entidad.Id.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.IdCompra = entidad.IdCompra;
            presentadorTupla.Vista.NumeroSolicitudCompra = compra?.IdSolicitudCompra.ToString() ?? "-";
            presentadorTupla.Vista.MetodoPago = entidad.MetodoPago;
            presentadorTupla.Vista.NumeroTelefonoRemitente = detallePagoTransferencia?.NumeroTelefonoConfirmacion ?? string.Empty;
            presentadorTupla.Vista.NumeroTransaccion = detallePagoTransferencia?.NumeroTransaccion ?? string.Empty;
            presentadorTupla.Vista.MontoPagado = entidad.MontoPagado;
            presentadorTupla.Vista.FechaPagoProveedor = entidad.FechaPago ?? DateTime.MinValue;
            presentadorTupla.Vista.FechaConfirmacionPago = entidad.FechaConfirmacionPago ?? DateTime.MinValue;
            presentadorTupla.Vista.EstadoPago = entidad.EstadoPago;

            return presentadorTupla;
        }
    }
}
