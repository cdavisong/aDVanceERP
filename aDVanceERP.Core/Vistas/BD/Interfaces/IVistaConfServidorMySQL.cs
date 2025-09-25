using aDVanceERP.Core.Modelos.BD;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Vistas.BD.Interfaces {
    public interface IVistaConfServidorMySQL : IVistaBase {
        string? NombreDireccionServidor { get; set; }
        string? NombreBaseDatos { get; set; }
        string? NombreUsuario { get; set; }
        string? Password { get; }
        bool RecordarConfiguracion { get; set; }

        event EventHandler<ConfiguracionBaseDatos>? AlmacenarConfiguracion;
        event EventHandler? ConfiguracionCargada;
    }
}