using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Repositorios.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Presentadores.Comun.Interfaces;

public interface IPresentadorVistaGestion<Vg, Re, En, Fb> : IPresentadorVistaBase<Vg>, IDisposable
    where Vg : class, IVistaContenedor, IGestorEntidades, IBuscadorEntidades<Fb>, INavegadorTuplasEntidades
    where Re : class, IRepoEntidadBaseDatos<En, Fb>, new()
    where En : class, IEntidadBaseDatos, new()
    where Fb : Enum
{
    Re Repositorio { get; }
    Fb FiltroBusqueda { get; }
    string? CriterioBusqueda { get; }

    event EventHandler? EditarEntidad;

    void Buscar(Fb filtroBusqueda, string? criterioBusqueda);
    void ActualizarResultadosBusqueda();
}