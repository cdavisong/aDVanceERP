using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    internal class PresentadorRegistroUnidadMedida : PresentadorVistaRegistro<IVistaRegistroUnidadMedida, UnidadMedida, RepoUnidadMedida, FiltroBusquedaUnidadMedida> {
        public PresentadorRegistroUnidadMedida(IVistaRegistroUnidadMedida vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroUnidadMedida", OnMostrarVistaRegistroUnidadMedida);
            AgregadorEventos.Suscribir("MostrarVistaEdicionUnidadMedida", OnMostrarVistaEdicionUnidadMedida);
        }

        private void OnMostrarVistaRegistroUnidadMedida(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();        
            Vista.Mostrar();
        }

        private void OnMostrarVistaEdicionUnidadMedida(string obj) {
            Vista.ModoEdicion = true;
            Vista.Restaurar();

            if (string.IsNullOrEmpty(obj))
                return;

            var unidadMedida = AgregadorEventos.DeserializarPayload<UnidadMedida>(obj);

            if (unidadMedida == null)
                return;

            PopularVistaDesdeEntidad(unidadMedida);

            Vista.Mostrar();
        }

        public override void PopularVistaDesdeEntidad(UnidadMedida entidad) {
            base.PopularVistaDesdeEntidad(entidad);

            Vista.Nombre = entidad.Nombre;
            Vista.Abreviatura = entidad.Abreviatura;
            Vista.Descripcion = entidad.Descripcion;
        }

        protected override UnidadMedida? ObtenerEntidadDesdeVista() {
            return new UnidadMedida {
                Id = _entidad?.Id ?? 0,
                Nombre = Vista.Nombre,
                Abreviatura = Vista.Abreviatura,
                Descripcion = Vista.Descripcion
            }; 
        }

        protected override bool EntidadCorrecta() {
            var nombreRepetido = !Vista.ModoEdicion && RepoUnidadMedida.Instancia.Buscar(FiltroBusquedaUnidadMedida.Nombre, Vista.Nombre).cantidad > 0;
            var nombreOk = !string.IsNullOrEmpty(Vista.Nombre) && !nombreRepetido;

            if (nombreRepetido)
                CentroNotificaciones.MostrarNotificacion("Ye existe una unidad de medida con el mismo nombre registrado en el sistema, los nombres deben ser únicos.", TipoNotificacionEnum.Advertencia);
            if (!nombreOk)
                CentroNotificaciones.MostrarNotificacion("El campo de nombre es obligatorio, por favor, corrija los datos entrados", TipoNotificacionEnum.Advertencia);

            return nombreOk;
        }
    }
}