using aDVanceERP.Core.Vistas.Comun.Interfaces;

using Guna.UI2.WinForms;

namespace aDVanceERP.Core.Presentadores.Comun.Interfaces {
    public interface IPresentadorVistaPrincipal<Vp> : IPresentadorVistaBase<Vp>
         where Vp : class, IVistaPrincipal {
        IPresentadorVistaSeguridad<IVistaSeguridad> Seguridad { get; }
        IPresentadorVistaModulos<IVistaModulos> Modulos { get; }

        void AdicionarBotonBarraTitulo(Guna2Button btnTitulo);
    }
}
