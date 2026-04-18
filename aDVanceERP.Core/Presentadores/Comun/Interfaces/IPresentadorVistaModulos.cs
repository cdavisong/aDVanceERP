using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

using Guna.UI2.WinForms;

namespace aDVanceERP.Core.Presentadores.Comun.Interfaces {
    public interface IPresentadorVistaContenedorModulos<Vm> : IPresentadorVistaBase<Vm>
         where Vm : class, IVistaContenedorModulos {
        IVistaPrincipal VistaPrincipal { get; }

        ModuloSistemaEnum[] ObtenerNombresModulosExtensionCargados();

        void AdicionarBotonAccesoModulo(Guna2CircleButton btnModulo, string nombreModulo);
    }
}
