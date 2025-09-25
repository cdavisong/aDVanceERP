using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Extension.Interfaces;

public interface IModuloExtension {
    string Nombre { get; }
    string Descripcion { get; }
    Version Version { get; }

    void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> principal);
    void Apagar();
}
