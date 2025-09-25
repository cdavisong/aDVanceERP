
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Extension.Interfaces.BaseConcreta;

public abstract class ModuloExtensionBase : IModuloExtension {
    protected IPresentadorVistaPrincipal<IVistaPrincipal> _principal;

    protected ModuloExtensionBase() {
        // Inicialización nula de referencias
        _principal = null!;

        // Inicialización de propiedades
        Nombre = string.Empty;
        Descripcion = string.Empty;
        Version = new Version(1, 0, 0, 0);
    }

    public string Nombre { get; protected set; }
    public string Descripcion { get; protected set; }
    public Version Version { get; protected set; }

    public virtual void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> principal) {
        _principal = principal;

        InicializarVistas();
    }

    protected abstract void InicializarVistas();

    public abstract void Apagar();
}