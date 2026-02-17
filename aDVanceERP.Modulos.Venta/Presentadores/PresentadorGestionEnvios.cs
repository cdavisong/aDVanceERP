using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;
using aDVanceERP.Modulos.Venta.Vistas;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    internal class PresentadorGestionEnvios : PresentadorVistaGestion<PresentadorTuplaEnvio, IVistaGestionEnvios, IVistaTuplaEnvio, SeguimientoEntrega, RepoSeguimientoEntrega, FiltroBusquedaSeguimientoEntrega> {
        private List<VentaPendientePago> _ventasPendientesPago = new List<VentaPendientePago>();

        public PresentadorGestionEnvios(IVistaGestionEnvios vista) : base(vista) {
            RegistrarEntidad += OnRegistrarEnvio;
            EditarEntidad += OnEditarEnvio;

            AgregadorEventos.Suscribir("MostrarVistaGestionEnvios", OnMostrarVistaGestionEnvios);
        }

        private void OnRegistrarEnvio(object? sender, EventArgs e) {
            _ventasPendientesPago = RepoVenta.Instancia.ObtenerVentasPendientesDePago();

            if (_ventasPendientesPago.Count == 0) {
                CentroNotificaciones.MostrarNotificacion("No es posible registrar un nuevo envío puesto que no existen ventas pendientes en el sistema.", TipoNotificacion.Advertencia);
                return;
            }

            AgregadorEventos.Publicar("MostrarVistaRegistroEnvio", string.Empty);
        }

        private void OnEditarEnvio(object? sender, SeguimientoEntrega e) {
            _ventasPendientesPago = RepoVenta.Instancia.ObtenerVentasPendientesDePago();

            AgregadorEventos.Publicar("MostrarVistaEdicionEnvio", AgregadorEventos.SerializarPayload(e));
        }

        private void OnMostrarVistaGestionEnvios(string obj) {
            Vista.CargarFiltrosBusqueda(UtilesBusquedaSeguimientoEntrega.FiltroBusquedaSeguimientoEntrega);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        protected override PresentadorTuplaEnvio ObtenerValoresTupla(SeguimientoEntrega entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaEnvio(new VistaTuplaEnvio(), entidad);
            var venta = RepoVenta.Instancia.ObtenerPorId(entidad.IdVenta);
            var mensajero = RepoMensajero.Instancia.ObtenerPorId(entidad.IdMensajero);
            var persona = RepoPersona.Instancia.ObtenerPorId(mensajero?.IdPersona ?? 0);

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.IdVenta = entidad.IdVenta;
            presentadorTupla.Vista.NumeroFacturaVenta = venta?.NumeroFacturaTicket ?? "-";
            presentadorTupla.Vista.IdMensajero = entidad.IdMensajero;
            presentadorTupla.Vista.NombreMensajero = persona != null ? $"{persona.NombreCompleto}" : "-";
            presentadorTupla.Vista.TipoEnvio = entidad.TipoEnvio;
            presentadorTupla.Vista.FechaAsignacion = entidad.FechaAsignacion ?? DateTime.MinValue;
            presentadorTupla.Vista.FechaEntregaRealizada = entidad.FechaEntregaRealizada ?? DateTime.MinValue;
            presentadorTupla.Vista.ObservacionesEntrega = entidad.ObservacionesEntrega;
            presentadorTupla.Vista.MontoCobradoAlCliente = entidad.MontoCobradoAlCliente;
            presentadorTupla.Vista.EstadoEntrega = entidad.EstadoEntrega;

            return presentadorTupla;
        }
    }
}
