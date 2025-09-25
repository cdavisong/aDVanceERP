using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Presentadores.Comun.Interfaces;

public interface IPresentadorVistaSeguridad<Vs> : IPresentadorVistaBase<Vs>
     where Vs : class, IVistaSeguridad {
}