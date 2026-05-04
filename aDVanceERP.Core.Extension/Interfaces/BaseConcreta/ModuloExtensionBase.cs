
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Extension.Interfaces.BaseConcreta {
    public abstract class ModuloExtensionBase : IModuloExtension {
        protected IPresentadorVistaPrincipal<IVistaPrincipal> _principal;

        protected ModuloExtensionBase() {
            // Inicialización nula de referencias
            _principal = null!;

            // Inicialización de propiedades
            Version = new Version(1, 0, 0, 0);
        }

        public ModuloSistemaEnum Nombre { get; protected set; }
        public Version Version { get; protected set; }

        public virtual void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> principal) {
            _principal = principal;

            InicializarVistas();
            InicializarEventos();

            Application.DoEvents();
        }

        protected abstract void InicializarVistas();

        protected abstract void InicializarEventos();

        public abstract void Apagar();
    }
}