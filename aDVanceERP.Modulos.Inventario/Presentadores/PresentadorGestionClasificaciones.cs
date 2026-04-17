using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Vistas;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    internal class PresentadorGestionClasificaciones : PresentadorVistaGestion<PresentadorTuplaClasificacion, IVistaGestionClasificaciones, IVistaTuplaClasificacion, ClasificacionProducto, RepoClasificacionProducto, FiltroBusquedaClasificacionProducto> {
        public PresentadorGestionClasificaciones(IVistaGestionClasificaciones vista) : base(vista) {
            vista.RegistrarEntidad += OnRegistrarClasificacion;

            AgregadorEventos.Suscribir("MostrarVistaGestionClasificaciones", OnMostrarVistaGestionClasificaciones);
        }

        private void OnMostrarVistaGestionClasificaciones(string obj) {
            CargarDatosComunes();
            
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        private void CargarDatosComunes() {
            Vista.CargarFiltrosBusqueda([.. EnumExt.ObtenerNombresDescripciones<FiltroBusquedaClasificacionProducto>()]);
        }

        private void OnRegistrarClasificacion(object? sender, EventArgs e) {
            AgregadorEventos.Publicar("MostrarVistaRegistroClasificacion", string.Empty);
        }

        protected override PresentadorTuplaClasificacion ObtenerValoresTupla(ClasificacionProducto entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaClasificacion(new VistaTuplaClasificacion(), entidad);

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.Nombre = entidad.Nombre;
            presentadorTupla.Vista.Descripcion = string.IsNullOrEmpty(entidad.Descripcion) ? "No disponible" : entidad.Descripcion;

            return presentadorTupla;
        }

        public override void Dispose() {
            Vista.RegistrarEntidad -= OnRegistrarClasificacion;

            AgregadorEventos.Desuscribir("MostrarVistaGestionClasificaciones", OnMostrarVistaGestionClasificaciones);

            base.Dispose();
        }
    }
}