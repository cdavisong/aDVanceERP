using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Presentadores.Comun.Interfaces;

public interface IPresentadorVistaPrincipal<Vp> : IPresentadorVistaBase<Vp>
     where Vp : class, IVistaPrincipal {
    IPresentadorVistaSeguridad<IVistaSeguridad> Seguridad { get; }
    IPresentadorVistaModulos<IVistaModulos> Modulos { get; }
}
