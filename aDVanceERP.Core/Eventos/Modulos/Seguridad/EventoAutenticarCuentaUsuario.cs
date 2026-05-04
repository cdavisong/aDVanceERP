using System.Security;

namespace aDVanceERP.Core.Eventos.Modulos.Seguridad {
    public class EventoAutenticarCuentaUsuario {
        public string NombreUsuario { get; set; } = string.Empty;
        public SecureString Password { get; set; } = null!;
    }
}
