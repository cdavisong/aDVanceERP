using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Interfaces {
    public interface IVistaTuplaCuentaUsuario : IVistaTupla {
        string Id { get; set; }
        string NombreUsuario { get; set; }
        string EstadoCuentaUsuario { get; set; }
    }
}