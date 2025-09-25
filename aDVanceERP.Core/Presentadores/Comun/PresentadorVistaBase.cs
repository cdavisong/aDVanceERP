using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Presentadores.Comun;

public abstract class PresentadorVistaBase<Vb> : IPresentadorVistaBase<Vb>
    where Vb : class, IVistaBase {
    private readonly Vb _vista;

    protected PresentadorVistaBase(Vb vista) {
        _vista = vista ?? throw new ArgumentNullException(nameof(vista));
    }

    public Vb Vista => _vista;

    public abstract void Dispose();
}