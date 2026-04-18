using System;

namespace aDVanceERP.Core.Modelos.Modulos.Seguridad {
    /// <summary>
    /// Argumentos para eventos de sesión
    /// </summary>
    public class EventArgsSesion : EventArgs {
        public CuentaUsuario Usuario { get; set; }
        public DateTime FechaEvento { get; set; }

        public EventArgsSesion(CuentaUsuario usuario) {
            Usuario = usuario;
            FechaEvento = DateTime.Now;
        }
    }

    /// <summary>
    /// Manejador de eventos de sesión
    /// </summary>
    public static class EventosSesion {
        public static event EventHandler<EventArgsSesion> UsuarioInicioSesion;
        public static event EventHandler<EventArgsSesion> UsuarioCerroSesion;
        public static event EventHandler<EventArgsSesion> SesionExpirada;

        public static void OnUsuarioInicioSesion(CuentaUsuario usuario) {
            UsuarioInicioSesion?.Invoke(null, new EventArgsSesion(usuario));
        }

        public static void OnUsuarioCerroSesion(CuentaUsuario usuario) {
            UsuarioCerroSesion?.Invoke(null, new EventArgsSesion(usuario));
        }

        public static void OnSesionExpirada(CuentaUsuario usuario) {
            SesionExpirada?.Invoke(null, new EventArgsSesion(usuario));
        }
    }
}