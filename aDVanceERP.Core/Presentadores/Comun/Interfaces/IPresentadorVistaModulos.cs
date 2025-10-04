using aDVanceERP.Core.Vistas.Comun.Interfaces;

using Guna.UI2.WinForms;

namespace aDVanceERP.Core.Presentadores.Comun.Interfaces;

public interface IPresentadorVistaModulos<Vm> : IPresentadorVistaBase<Vm>
     where Vm : class, IVistaModulos {
    IVistaPrincipal VistaPrincipal { get; }

    void AdicionarBotonAccesoModulo(Guna2CircleButton btnModulo);
}
