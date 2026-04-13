using aDVanceERP.Core.Eventos;
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
            RegistrarEntidad += OnRegistrarUnidadMedida;
            EditarEntidad += OnEditarUnidadMedida;

            AgregadorEventos.Suscribir("MostrarVistaGestionUnidadesMedida", OnMostrarVistaGestionUnidadesMedida);
        }

        private void OnRegistrarUnidadMedida(object? sender, EventArgs e) {
            AgregadorEventos.Publicar("MostrarVistaRegistroUnidadMedida", string.Empty);
        }

        private void OnEditarUnidadMedida(object? sender, UnidadMedida e) {
            AgregadorEventos.Publicar("MostrarVistaEdicionUnidadMedida", AgregadorEventos.SerializarPayload(e));
        }

        private void OnMostrarVistaGestionUnidadesMedida(string obj) {
            Vista.CargarFiltrosBusqueda([.. EnumExt.ObtenerNombresDescripciones<FiltroBusquedaUnidadMedida>()]);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        protected override PresentadorTuplaUnidadMedida ObtenerValoresTupla(UnidadMedida entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaUnidadMedida(new VistaTuplaUnidadMedida(), entidad);

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.Nombre = entidad.Nombre;
            presentadorTupla.Vista.Abreviatura = entidad.Abreviatura;
            presentadorTupla.Vista.Descripcion = entidad.Descripcion ?? "No hay descripción disponible";

            return presentadorTupla;
        }
    }
}