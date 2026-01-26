using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Ventas;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Ventas;
using aDVanceERP.Modulos.Venta.Interfaces;

using DVanceERP.Modulos.Venta.Vistas;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    public class PresentadorGestionVentas : PresentadorVistaGestion<PresentadorTuplaVenta, IVistaGestionVentas, IVistaTuplaVenta, Core.Modelos.Modulos.Ventas.Venta, RepoVenta, FiltroBusquedaVenta> {
        public PresentadorGestionVentas(IVistaGestionVentas vista) : base(vista) {
            RegistrarEntidad += OnRegistrarVenta;
            EditarEntidad += OnEditarVenta;

            AgregadorEventos.Suscribir("MostrarVistaGestionVentas", OnMostrarVistaGestionVentas);
            AgregadorEventos.Suscribir("ActivarDesactivarVenta", OnActivarDesactivarVenta);
        }

        private void OnRegistrarVenta(object? sender, EventArgs e) {
            AgregadorEventos.Publicar("MostrarVistaRegistroVenta", string.Empty);
        }

        private void OnEditarVenta(object? sender, Core.Modelos.Modulos.Ventas.Venta e) {
            AgregadorEventos.Publicar("MostrarVistaEdicionVenta", AgregadorEventos.SerializarPayload(e));
        }

        private void OnMostrarVistaGestionVentas(string obj) {
            Vista.CargarFiltrosBusqueda(UtilesBusquedaVenta.FiltroBusquedaVenta);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        private void OnActivarDesactivarVenta(string obj) {
            var idVentaSeleccionado = _tuplasEntidades.FirstOrDefault(t => t.EstadoSeleccion)?.Vista.Id ?? 0;

            if (idVentaSeleccionado != 0) {
                var estado = RepoVenta.Instancia.HabilitarDeshabilitarVenta(idVentaSeleccionado);

                ActualizarResultadosBusqueda();

                CentroNotificaciones.Mostrar($"La venta ha sido {(estado ? "habilitada" : "deshabilitada")} satisfactoriamente.", TipoNotificacion.Info);
            }
        }

        protected override PresentadorTuplaVenta ObtenerValoresTupla(Core.Modelos.Modulos.Ventas.Venta entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaVenta(new VistaTuplaVenta(), entidad);
            
            presentadorTupla.Vista.Id = entidad.Id;
            
            presentadorTupla.Vista.Activo = entidad.Activo;

            return presentadorTupla;
        }
    }
}