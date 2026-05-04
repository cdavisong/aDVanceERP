using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Interfaces {
    public interface IVistaTuplaCuentaUsuario : IVistaTupla {
        long Id { get; set; }
        string NombrePersona { get; set; }
        string NombreUsuario { get; set; }
        string? Email { get; set; }
        string NombreRol { get; set; }
        bool Administrador { get; set; }
        bool Aprobado { get; set; }
        bool Estado { get; set; }

        event EventHandler? AprobarCuentaUsuario;
    }
}