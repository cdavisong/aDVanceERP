using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Mensajes.MVP.Modelos;
using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Seguridad.MVP.Modelos;
using aDVanceERP.Core.Seguridad.MVP.Modelos.Repositorios;
using aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion.Plantillas;
using aDVanceERP.Core.Seguridad.Utiles;

namespace aDVanceERP.Core.Seguridad.MVP.Presentadores;

public class PresentadorRegistroUsuario : PresentadorVistaRegistro<IVistaRegistroUsuario, CuentaUsuario,
    RepoCuentaUsuario, FiltroBusquedaCuentaUsuario> {
    public PresentadorRegistroUsuario(IVistaRegistroUsuario vista) : base(vista) {
        vista.RegistrarEntidad += delegate(object? sender, EventArgs args) {
            UsuarioRegistrado?.Invoke(sender, args); 
        };
        vista.AutenticarUsuario += delegate(object? sender, EventArgs args) {
            MostrarVistaAutenticacionUsuario?.Invoke("autenticate-user", args);
        };
    }

    public event EventHandler? UsuarioRegistrado;
    public event EventHandler? MostrarVistaAutenticacionUsuario;

    public override void PopularVistaDesdeEntidad(CuentaUsuario objeto) {
        throw new NotImplementedException();
    }

    protected override void RegistroAuxiliar(RepoCuentaUsuario datosCuentaUsuario, long id) {
        UsuarioRegistrado?.Invoke("register-user", EventArgs.Empty);
    }

    protected override CuentaUsuario? ObtenerEntidadDesdeVista() {
        try {
            if (UtilesCuentaUsuario.EsTablaCuentasUsuarioVacia().Result) {
                if (Vista.Password != null)
                    UtilesCuentaUsuario.CrearUsuarioAdministrador(Vista.NombreUsuario, Vista.Password);

                UsuarioRegistrado?.Invoke("register-admin", EventArgs.Empty);

                return null;
            }
        }
        catch (ExcepcionConexionServidorMySQL e) {
            CentroNotificaciones.Mostrar(e.Message, TipoNotificacion.Error);
        }

        var passwordSeguro = UtilesPassword.HashPassword(Vista.Password);

        return new CuentaUsuario(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.NombreUsuario,
            passwordSeguro.hash,
            passwordSeguro.salt,
            0
        );
    }
}