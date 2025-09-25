using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Mensajes.MVP.Modelos;
using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Seguridad.MVP.Modelos;
using aDVanceERP.Core.Seguridad.MVP.Modelos.Repositorios;
using aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion.Plantillas;
using aDVanceERP.Core.Seguridad.Utiles;

namespace aDVanceERP.Core.Seguridad.MVP.Presentadores;

public class PresentadorAutenticacionUsuario : PresentadorVistaBase<IVistaAutenticacionUsuario> {
    public PresentadorAutenticacionUsuario(IVistaAutenticacionUsuario vista) : base(vista) {
        vista.Autenticar += OnAutenticarUsuario;
        vista.RegistrarCuenta += OnRegistrarCuenta; 
    }

    public event EventHandler? UsuarioAutenticado;
    public event EventHandler? MostrarVistaRegistroCuentaUsuario;

   private void OnAutenticarUsuario(object? sender, EventArgs args) {
        if (string.IsNullOrEmpty(Vista.NombreUsuario) || Vista.Password.Length == 0) {
            CentroNotificaciones.Mostrar(
                "Debe especificar un usuario y contraseña para autenticarse en el sistema. Por favor, rellene los campos correctamente.",
                TipoNotificacion.Advertencia);

            return;
        }

        try {
            using (var datosUsuario = new RepoCuentaUsuario()) {
                var usuario = datosUsuario.Buscar(FiltroBusquedaCuentaUsuario.Nombre, Vista.NombreUsuario).resultados.FirstOrDefault();

                if (usuario == null) {
                    CentroNotificaciones.Mostrar(
                        "El usuario especificado no existe en la base de datos o no se ha registrado aún en el sistema, verifique los datos entrados.",
                        TipoNotificacion.Advertencia);

                    return;
                }

                if (UtilesPassword.VerificarPassword(Vista.Password, usuario.PasswordHash, usuario.PasswordSalt)) {
                    UtilesCuentaUsuario.UsuarioAutenticado = usuario;
                    UtilesCuentaUsuario.PermisosUsuario = UtilesRolUsuario.ObtenerPermisosDeRol(usuario.IdRolUsuario);

                    UsuarioAutenticado?.Invoke(usuario, args);
                }
                else {
                    CentroNotificaciones.Mostrar(
                        "La contraseña especificada es incorrecta para el usuario especificado, verifique los datos entrados.",
                        TipoNotificacion.Advertencia);
                }
            }
        }
        catch (ExcepcionConexionServidorMySQL e) {
            CentroNotificaciones.Mostrar(e.Message, TipoNotificacion.Error);
        }
    }

    private void OnRegistrarCuenta(object? sender, EventArgs e) {
        MostrarVistaRegistroCuentaUsuario?.Invoke("register-user", e);
    }

    public override void Dispose() {
        Vista.Autenticar -= OnAutenticarUsuario;
        Vista.RegistrarCuenta -= OnRegistrarCuenta;
    }
}