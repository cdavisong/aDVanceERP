using aDVanceERP.Core.Repositorios.Comun;

namespace aDVanceERP.Core.Vistas.Comun.Interfaces;

public interface IVistaSeguridad : IVistaBase {
    RepoVistaBase PanelCentral { get; }
}