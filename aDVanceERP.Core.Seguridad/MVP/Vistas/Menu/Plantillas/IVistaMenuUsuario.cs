using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Seguridad.MVP.Vistas.Menu.Plantillas
{
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
