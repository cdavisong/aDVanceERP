using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Inventario;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    internal class PresentadorRegistroClasificacion : PresentadorVistaRegistro<IVistaRegistroClasificacion, ClasificacionProducto, RepoClasificacionProducto, FiltroBusquedaClasificacionProducto> {
        public PresentadorRegistroClasificacion(IVistaRegistroClasificacion vista) : base(vista) {
            AgregadorEventos.Suscribir<EventoMostrarVistaRegistroClasificacionProducto>(OnMostrarVistaRegistroClasificacion);
            AgregadorEventos.Suscribir<EventoMostrarVistaEdicionClasificacion>(OnMostrarVistaEdicionClasificacion);
        }

        private void OnMostrarVistaRegistroClasificacion(EventoMostrarVistaRegistroClasificacionProducto e) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();        
            Vista.Mostrar();
        }

        private void OnMostrarVistaEdicionClasificacion(EventoMostrarVistaEdicionClasificacion e) {
            Vista.ModoEdicion = true;
            Vista.Restaurar();

            PopularVistaDesdeEntidad(e.ClasificacionProducto);

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

        protected override async void RegistroEdicionAuxiliar(RepoClasificacionProducto repositorio, long id) {
            if (!Vista.ModoEdicion)
                AgregadorEventos.Publicar(new EventoClasificacionProductoRegistrada() {
                    ClasificacionProducto = Entidad!
                });
        }

        protected override bool EntidadCorrecta() {
            var nombreRepetido = !Vista.ModoEdicion && RepoClasificacionProducto.Instancia.Buscar(FiltroBusquedaClasificacionProducto.Nombre, Vista.Nombre).cantidad > 0;
            var nombreOk = !string.IsNullOrEmpty(Vista.Nombre) && !nombreRepetido;

            if (nombreRepetido)
                CentroNotificaciones.MostrarNotificacion("Ye existe un tipo o clasificación con el mismo nombre registrado en el sistema, los nombres deben ser únicos.", TipoNotificacionEnum.Advertencia);
            if (!nombreOk)
                CentroNotificaciones.MostrarNotificacion("El campo de nombre es obligatorio, por favor, corrija los datos entrados", TipoNotificacionEnum.Advertencia);

            return nombreOk;
        }

        public override void Dispose() {
            AgregadorEventos.Suscribir<EventoMostrarVistaRegistroClasificacionProducto>(OnMostrarVistaRegistroClasificacion);
            AgregadorEventos.Suscribir<EventoMostrarVistaEdicionClasificacion>(OnMostrarVistaEdicionClasificacion);

            base.Dispose();
        }
    }
}