using aDVanceERP.Core.Repositorios.Comun;

namespace aDVanceERP.Core.Vistas.Comun.Interfaces;

public interface IVistaContenedorMenu : IVistaContenedor {
    RepoVistaBase Menus { get; }
}