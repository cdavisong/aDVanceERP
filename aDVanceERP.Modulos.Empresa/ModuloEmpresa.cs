using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Extension.Interfaces.BaseConcreta;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Empresa.Properties;

using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Empresa {
    internal class ModuloEmpresa : ModuloExtensionBase {
        private Guna2Button _btnAccesoModulo = new Guna2Button();

        public ModuloEmpresa() {
            Nombre = "MOD_EMPRESA";
            Descripcion = "Proporciona funcionalidades de gestión de empresas.";
            Version = new Version(1, 0, 0, 0);
        }

        public override void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> principal) {
            // Botón de acceso al módulo
            _btnAccesoModulo.Name = "btnAccesoModuloEmpresa";
            _btnAccesoModulo.CustomImages.Image = Resources.enterprisesB_24px;
            _btnAccesoModulo.Click += delegate {
                AgregadorEventos.Publicar("MostrarVistaMenuEmpresa", string.Empty);
            };

            base.Inicializar(principal);
        }

        protected override void InicializarVistas() {
            // Agregar botón de acceso al módulo
            _principal.AdicionarBotonBarraTitulo(_btnAccesoModulo);
        }

        public override void Apagar() {
            throw new NotImplementedException();
        }
    }
}
