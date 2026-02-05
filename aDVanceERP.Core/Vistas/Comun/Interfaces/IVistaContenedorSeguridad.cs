using aDVanceERP.Core.Repositorios.Comun;

namespace aDVanceERP.Core.Vistas.Comun.Interfaces {
    public interface IVistaContenedorSeguridad : IVistaBase {
        RepoVistaBase PanelCentral { get; }
    }
}