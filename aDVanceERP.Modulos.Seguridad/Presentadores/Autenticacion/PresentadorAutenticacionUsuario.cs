using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Mensajes.MVP.Modelos;
using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Vistas.Autenticacion.Plantillas;

namespace aDVanceERP.Modulos.Seguridad.Presentadores.Autenticacion;

public class PresentadorAutenticacionUsuario : PresentadorVistaBase<IVistaAutenticacionUsuario> {
    public PresentadorAutenticacionUsuario(IVistaAutenticacionUsuario vista) : base(vista) {
        AgregadorEventos.Suscribir("MostrarVistaAutenticacionUsuario", OnMostrarVistaAutenticacionUsuario);
        AgregadorEventos.Suscribir("EventoAutenticarCuentaUsuario", OnAutenticarCuentaUsuario);
    }

    private void OnMostrarVistaAutenticacionUsuario(string obj) {
        Vista.Restaurar();
        Vista.Mostrar();
    }

    private void OnAutenticarCuentaUsuario(string obj) {
        if (string.IsNullOrEmpty(Vista.NombreUsuario) || Vista.Password.Length == 0) {
            CentroNotificaciones.Mostrar(
                "Debe especificar un usuario y contraseña para autenticarse en el sistema. Por favor, rellene los campos correctamente.",
                TipoNotificacion.Advertencia);

            return;
        }

        try {
            using (var datosUsuario = new RepoCuentaUsuario()) {
                var usuario = datosUsuario.Buscar(FiltroBusquedaCuentaUsuario.Nombre, Vista.NombreUsuario).entidades.FirstOrDefault();

                if (usuario == null) {
                    CentroNotificaciones.Mostrar(
                        "El usuario especificado no existe en la base de datos o no se ha registrado aún en el sistema, verifique los datos entrados.",
                        TipoNotificacion.Advertencia);

                    return;
                }

                if (Vista.Password.VerificarPassword(usuario.PasswordHash, usuario.PasswordSalt)) {
                    if (usuario.Aprobado) {
                        var rolUsuario = RepoRolUsuario.Instancia.ObtenerPorId(usuario.IdRolUsuario);
                        var permisosRolUsuario = RepoPermisoRolUsuario.Instancia.Buscar(FiltroBusquedaPermisoRolUsuario.IdRolUsuario, rolUsuario?.Id.ToString()).entidades;
                        
                        ContextoSeguridad.UsuarioAutenticado = usuario;
                        ContextoSeguridad.PermisosUsuario = permisosRolUsuario.Select(p => p.NombrePermiso).ToArray();

                        AgregadorEventos.Publicar("EventoUsuarioAutenticado", AgregadorEventos.SerializarPayload(usuario));
                    } else AgregadorEventos.Publicar("MostrarVistaAprobacionUsuario", AgregadorEventos.SerializarPayload(usuario));
                } else {
                    CentroNotificaciones.Mostrar(
                        "La contraseña especificada es incorrecta para el usuario especificado, verifique los datos entrados.",
                        TipoNotificacion.Advertencia);
                }
            }
        } catch (ExcepcionConexionServidorMySQL e) {
            CentroNotificaciones.Mostrar(e.Message, TipoNotificacion.Error);
        }
    }

    public override void Dispose() {
        AgregadorEventos.Desuscribir("MostrarVistaAutenticacionUsuario", OnMostrarVistaAutenticacionUsuario);
        AgregadorEventos.Desuscribir("EventoAutenticarCuentaUsuario", OnAutenticarCuentaUsuario);
    }
}