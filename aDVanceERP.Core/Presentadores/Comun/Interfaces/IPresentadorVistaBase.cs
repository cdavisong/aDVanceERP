using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Presentadores.Comun.Interfaces;

public interface IPresentadorVistaBase<Vb> : IDisposable
    where Vb : class, IVistaBase {
    Vb Vista { get; }
}