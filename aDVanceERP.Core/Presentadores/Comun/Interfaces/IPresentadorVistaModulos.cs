using aDVanceERP.Core.Vistas.Comun.Interfaces;

using Guna.UI2.WinForms;

namespace aDVanceERP.Core.Presentadores.Comun.Interfaces {
    public interface IPresentadorVistaContenedorModulos<Vm> : IPresentadorVistaBase<Vm>
         where Vm : class, IVistaContenedorModulos {
        IVistaPrincipal VistaPrincipal { get; }

        string[] ObtenerNombresModulosExtensionCargados();
        void AdicionarBotonAccesoModulo(Guna2CircleButton btnModulo);
    }
}
