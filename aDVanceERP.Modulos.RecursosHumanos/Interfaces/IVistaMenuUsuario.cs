using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Interfaces {
    public interface IVistaMenuUsuario : IVistaBase {
        string? NombreUsuario { get; set; }
        Image? LogotipoEmpresa { get; set; }
        string? NombreEmpresa { get; set; }
        string? CorreoElectronico { get; set; }
        long IdEmpresa { get; }

        event EventHandler? CerrarSesion;
        event EventHandler? ConfigurarEmpresa;
    }
}
