using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Presentadores.Comun.Interfaces;

public interface IPresentadorVistaModulos<Vm> : IPresentadorVistaBase<Vm>
     where Vm : class, IVistaModulos {
}
