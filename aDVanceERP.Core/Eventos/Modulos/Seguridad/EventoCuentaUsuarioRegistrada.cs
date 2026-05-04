using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;

namespace aDVanceERP.Core.Eventos.Modulos.Seguridad {
    public class EventoCuentaUsuarioRegistrada {
        public CuentaUsuario CuentaUsuario { get; set; } = null!;
        public Persona Persona { get; set; } = null!;
        public CorreoContacto CorreoContacto { get; set; } = null!;
    }
}
