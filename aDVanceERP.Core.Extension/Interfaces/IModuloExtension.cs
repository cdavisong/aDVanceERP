using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Extension.Interfaces {
    public interface IModuloExtension {
        ModuloSistemaEnum Nombre { get; }
        Version Version { get; }

        void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> principal);
        void Apagar();
    }
}
