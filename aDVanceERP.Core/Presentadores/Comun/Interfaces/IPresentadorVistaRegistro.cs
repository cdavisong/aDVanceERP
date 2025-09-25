using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Repositorios.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Presentadores.Comun.Interfaces;

public interface IPresentadorVistaRegistro<Vr, Re, En, Fb> : IPresentadorVistaBase<Vr>
    where Vr : class, IVistaRegistro
    where Re : class, IRepoEntidadBaseDatos<En, Fb>, new()
    where En : class, IEntidadBaseDatos, new()
    where Fb : Enum
{
    En? Entidad { get; }
    Re Repositorio { get; }

    event EventHandler? EntidadRegistradaActualizada;
    event EventHandler? Salir;
}