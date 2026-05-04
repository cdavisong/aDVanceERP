using aDVanceERP.Core.Modelos.Modulos.Seguridad;

namespace aDVanceERP.Core.Eventos.Modulos.Seguridad {
    public class EventoUsuarioAutenticado {
        public CuentaUsuario CuentaUsuario { get; set; } = null!;
    }
}
