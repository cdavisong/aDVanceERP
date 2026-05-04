using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Inventario;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Vistas;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    internal class PresentadorGestionUnidadesMedida : PresentadorVistaGestion<PresentadorTuplaUnidadMedida, IVistaGestionUnidadesMedida, IVistaTuplaUnidadMedida, UnidadMedida, RepoUnidadMedida, FiltroBusquedaUnidadMedida> {
        public PresentadorGestionUnidadesMedida(IVistaGestionUnidadesMedida vista) : base(vista) {
            vista.RegistrarEntidad += OnRegistrarUnidadMedida;

            AgregadorEventos.Suscribir<EventoMostrarVistaGestionUnidadesMedida>(OnMostrarVistaGestionUnidadesMedida);
        }

        private void OnMostrarVistaGestionUnidadesMedida(EventoMostrarVistaGestionUnidadesMedida e) {
            CargarDatosComunes();
            
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        private void CargarDatosComunes() {
            Vista.CargarFiltrosBusqueda([.. EnumExt.ObtenerNombresDescripciones<FiltroBusquedaUnidadMedida>()]);
        }

        private void OnRegistrarUnidadMedida(object? sender, EventArgs e) {
            AgregadorEventos.Publicar(new EventoMostrarVistaRegistroUnidadMedida());
        }

        protected override PresentadorTuplaUnidadMedida ObtenerValoresTupla(UnidadMedida entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaUnidadMedida(new VistaTuplaUnidadMedida(), entidad);

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.Nombre = entidad.Nombre;
            presentadorTupla.Vista.Abreviatura = entidad.Abreviatura;
            presentadorTupla.Vista.Descripcion = !string.IsNullOrEmpty(entidad.Descripcion) ? entidad.Descripcion : "No disponible";

            return presentadorTupla;
        }

        public override void Dispose() {
            Vista.RegistrarEntidad += OnRegistrarUnidadMedida;

            AgregadorEventos.Desuscribir<EventoMostrarVistaGestionUnidadesMedida>(OnMostrarVistaGestionUnidadesMedida);

            base.Dispose();
        }
    }
}