using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Helpers.Comun;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Presentadores;

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
            CentroNotificaciones.Mostrar(
                "Debe especificar un usuario y contraseña para registrarse en el sistema. Por favor, rellene los campos correctamente.",
                TipoNotificacion.Advertencia);

            return null;
        }

        // Obtener los datos de la vista
        var passwordSeguro = SecureStringHelper.HashPassword(Vista.Password);
        var usuario = new Core.Modelos.Modulos.Seguridad.CuentaUsuario(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.NombreUsuario,
            passwordSeguro.hash,
            passwordSeguro.salt,
            0
        );

        try {
            var repoCuentaUsuario = new RepoCuentaUsuario();
            var repoRolUsuario = new RepoRolUsuario();

            if (repoCuentaUsuario.Cantidad() == 0) {
                var rolAdministrador = repoRolUsuario.Buscar(FiltroBusquedaRolUsuario.Nombre, "Administrador").resultadosBusqueda.FirstOrDefault().entidadBase;

                if (rolAdministrador != null) 
                    usuario.IdRolUsuario = rolAdministrador.Id;
                else { 
                    rolAdministrador = new RolUsuario(0, "Administrador"); 
                    rolAdministrador.Id = repoRolUsuario.Adicionar(rolAdministrador);
                    usuario.IdRolUsuario = rolAdministrador.Id;
                }

                usuario.Aprobado = true;
                usuario.Administrador = true;
                usuario.Id = repoCuentaUsuario.Adicionar(usuario);

                AgregadorEventos.Publicar("MostrarVistaAutenticacionUsuario", AgregadorEventos.SerializarPayload(usuario));

                return null;
            }
        } catch (ExcepcionConexionServidorMySQL e) {
            CentroNotificaciones.Mostrar(e.Message, TipoNotificacion.Error);
        }

        AgregadorEventos.Publicar("MostrarVistaAprobacionUsuario", AgregadorEventos.SerializarPayload(usuario));

        return usuario;
    }
}