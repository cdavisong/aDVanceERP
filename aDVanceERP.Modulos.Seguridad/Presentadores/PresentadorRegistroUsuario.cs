using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Helpers.Comun;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Presentadores {
    public class PresentadorRegistroUsuario : PresentadorVistaRegistro<IVistaRegistroUsuario, Core.Modelos.Modulos.Seguridad.CuentaUsuario, RepoCuentaUsuario, FiltroBusquedaCuentaUsuario> {
        public PresentadorRegistroUsuario(IVistaRegistroUsuario vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroUsuario", OnMostrarVistaRegistroUsuario);
        }

        private void OnMostrarVistaRegistroUsuario(string obj) {
            Vista.ModoEdicion = false;

            Vista.Restaurar();
            Vista.Mostrar();
        }

        public override void PopularVistaDesdeEntidad(Core.Modelos.Modulos.Seguridad.CuentaUsuario objeto) { }

        protected override Core.Modelos.Modulos.Seguridad.CuentaUsuario? ObtenerEntidadDesdeVista() {
            if (string.IsNullOrEmpty(Vista.NombreUsuario) || Vista.Password.Length == 0) {
                CentroNotificaciones.MostrarNotificacion(
                    "Debe especificar un usuario y contraseña para registrarse en el sistema. Por favor, rellene los campos correctamente.",
                    TipoNotificacion.Advertencia);

                return null;
            }

            // Obtener los datos de la vista
            var passwordSeguro = SecureStringHelper.HashPassword(Vista.Password);
            var usuario = new CuentaUsuario(
                Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
                Vista.NombreUsuario,
                passwordSeguro.hash,
                passwordSeguro.salt
            );

            try {
                var repoCuentaUsuario = new RepoCuentaUsuario();
                
                if (repoCuentaUsuario.Cantidad() == 0) {
                    usuario.Aprobado = true;
                    usuario.Administrador = true;
                    usuario.Id = repoCuentaUsuario.Adicionar(usuario);

                    AgregadorEventos.Publicar("MostrarVistaAutenticacionUsuario", AgregadorEventos.SerializarPayload(usuario));

                    return null;
                }
            } catch (ExcepcionConexionServidorMySQL e) {
                CentroNotificaciones.MostrarNotificacion(e.Message, TipoNotificacion.Error);
            }

            AgregadorEventos.Publicar("MostrarVistaAprobacionUsuario", AgregadorEventos.SerializarPayload(usuario));

            return usuario;
        }
    }
}