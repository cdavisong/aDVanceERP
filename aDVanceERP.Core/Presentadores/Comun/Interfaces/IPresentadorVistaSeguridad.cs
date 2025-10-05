using aDVanceERP.Core.Vistas.BD;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Presentadores.Comun.Interfaces;

public interface IPresentadorVistaSeguridad<Vs> : IPresentadorVistaBase<Vs>
     where Vs : class, IVistaSeguridad {
    VistaConfiguracionBaseDatos ConfiguracionBaseDatos { get; }
}