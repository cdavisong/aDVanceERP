using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Extension.Interfaces.BaseConcreta;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Servicios.Properties;

using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Servicios {
    internal class ModuloServicios : ModuloExtensionBase {
        private Guna2CircleButton _btnAccesoModulo = new Guna2CircleButton();

        public ModuloServicios() {
            Nombre = "MOD_SERVICIOS";
            NombreAmigable = "Servicios";
            Descripcion = "Proporciona funcionalidades de gestión de servicios.";
            Version = new Version(1, 0, 0, 0);
        }

        public override void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> principal) {
            // Botón de acceso al módulo
            _btnAccesoModulo.Name = "btnAccesoModuloEmpresa";
            _btnAccesoModulo.CustomImages.Image = Resources.serviceB_24px;
            _btnAccesoModulo.TabIndex = 4;
            _btnAccesoModulo.Click += delegate {
                AgregadorEventos.Publicar("EventoCambioModulo", string.Empty);
                AgregadorEventos.Publicar("EventoCambioMenu", string.Empty);
                AgregadorEventos.Publicar("MostrarVistaMenuServicios", string.Empty);
            };

            // Contenedor de módulos

            base.Inicializar(principal);
        }

        protected override void InicializarVistas() {
            // Agregar botón de acceso al módulo
            _principal.Modulos.AdicionarBotonAccesoModulo(_btnAccesoModulo, "Servicios");

            // Contenedor de módulos
        }

        public override void Apagar() {
            throw new NotImplementedException();
        }
    }
}
