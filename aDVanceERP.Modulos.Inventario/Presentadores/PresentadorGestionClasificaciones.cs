using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Vistas;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    internal class PresentadorGestionClasificaciones : PresentadorVistaGestion<PresentadorTuplaClasificacion, IVistaGestionClasificaciones, IVistaTuplaClasificacion, ClasificacionProducto, RepoClasificacionProducto, FiltroBusquedaClasificacionProducto> {
        public PresentadorGestionClasificaciones(IVistaGestionClasificaciones vista) : base(vista) {
            RegistrarEntidad += OnRegistrarClasificacion;
            EditarEntidad += OnEditarClasificacion;

            AgregadorEventos.Suscribir("MostrarVistaGestionClasificaciones", OnMostrarVistaGestionClasificaciones);
        }

        private void OnRegistrarClasificacion(object? sender, EventArgs e) {
            AgregadorEventos.Publicar("MostrarVistaRegistroClasificacion", string.Empty);
        }

        private void OnEditarClasificacion(object? sender, ClasificacionProducto e) {
            AgregadorEventos.Publicar("MostrarVistaEdicionClasificacion", AgregadorEventos.SerializarPayload(e));
        }

        private void OnMostrarVistaGestionClasificaciones(string obj) {
            Vista.CargarFiltrosBusqueda(UtilesBusquedaClasificacionProducto.FiltroBusquedaTiposProducto);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        protected override PresentadorTuplaClasificacion ObtenerValoresTupla(ClasificacionProducto entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaClasificacion(new VistaTuplaClasificacion(), entidad);

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.Nombre = entidad.Nombre;
            presentadorTupla.Vista.Descripcion = entidad.Descripcion ?? "No hay descripción disponible";

            return presentadorTupla;
        }
    }
}