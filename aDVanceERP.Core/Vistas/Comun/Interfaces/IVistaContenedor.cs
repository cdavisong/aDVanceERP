using aDVanceERP.Core.Repositorios.Comun;

namespace aDVanceERP.Core.Vistas.Comun.Interfaces;

public interface IVistaContenedor : IVistaBase {
    int TuplasMaximasContenedor { get; }

    RepoVistaBase PanelCentral { get; }
}