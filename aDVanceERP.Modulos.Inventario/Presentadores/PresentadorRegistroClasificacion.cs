using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    internal class PresentadorRegistroClasificacion : PresentadorVistaRegistro<IVistaRegistroClasificacion, ClasificacionProducto, RepoClasificacionProducto, FiltroBusquedaClasificacionProducto> {
        public PresentadorRegistroClasificacion(IVistaRegistroClasificacion vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroClasificacion", OnMostrarVistaRegistroClasificacion);
            AgregadorEventos.Suscribir("MostrarVistaEdicionClasificacion", OnMostrarVistaEdicionClasificacion);
        }

        private void OnMostrarVistaRegistroClasificacion(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();
        
            Vista.Mostrar();
        }

        private void OnMostrarVistaEdicionClasificacion(string obj) {
            Vista.ModoEdicion = true;
            Vista.Restaurar();

            if (string.IsNullOrEmpty(obj))
                return;

            var almacen = AgregadorEventos.DeserializarPayload<ClasificacionProducto>(obj);

            if (almacen == null)
                return;

            PopularVistaDesdeEntidad(almacen);

            Vista.Mostrar();
        }

        public override void PopularVistaDesdeEntidad(ClasificacionProducto entidad) {
            base.PopularVistaDesdeEntidad(entidad);

            Vista.Nombre = entidad.Nombre;
            Vista.Descripcion = entidad.Descripcion;
        }

        protected override ClasificacionProducto? ObtenerEntidadDesdeVista() {
            return new ClasificacionProducto {
                Id = _entidad?.Id ?? 0,
                Nombre = Vista.Nombre,
                Descripcion = Vista.Descripcion
            }; 
        }

        protected override bool EntidadCorrecta() {
            var nombreRepetido = !Vista.ModoEdicion && RepoClasificacionProducto.Instancia.Buscar(FiltroBusquedaClasificacionProducto.Nombre, Vista.Nombre).cantidad > 0;
            var nombreOk = !string.IsNullOrEmpty(Vista.Nombre) && !nombreRepetido;

            if (nombreRepetido)
                CentroNotificaciones.MostrarNotificacion("Ye existe un tipo o clasificación con el mismo nombre registrado en el sistema, los nombres deben ser únicos.", TipoNotificacion.Advertencia);
            if (!nombreOk)
                CentroNotificaciones.MostrarNotificacion("El campo de nombre es obligatorio, por favor, corrija los datos entrados", TipoNotificacion.Advertencia);

            return nombreOk;
        }
    }
}