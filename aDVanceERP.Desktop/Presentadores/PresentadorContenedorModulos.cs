using aDVanceERP.Core.Extension.Controladores;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Desktop.Vistas;

using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;

namespace aDVanceERP.Desktop.Presentadores {
    public partial class PresentadorContenedorModulos : IPresentadorVistaContenedorModulos<IVistaContenedorModulos> {
        private readonly GestorModulosExtensibles _gestorModulos = new GestorModulosExtensibles();
        private PresentadorContenedorExtensiones _extensiones = null!;

        public PresentadorContenedorModulos(IVistaPrincipal vistaPrincipal, IVistaContenedorModulos vistaModulos) {
            VistaPrincipal = vistaPrincipal;
            Vista = vistaModulos;

            // Inicializar presentadores
            _extensiones = new PresentadorContenedorExtensiones(new VistaContenedorExtensiones());

            // Adicionar vistas al panel central
            Vista.PanelCentral.Registrar(_extensiones.Vista);
        }

        internal void CargarModulosExtension(IPresentadorVistaPrincipal<IVistaPrincipal> presentadorVistaPrincipal) {
            _gestorModulos.CargarModulos(presentadorVistaPrincipal);
        }

        public string[] ObtenerNombresModulosExtensionCargados() {
            return [.. _gestorModulos.ObtenerModulosExtension().Select(me => me.Nombre)];
        }

        public void AdicionarBotonAccesoModulo(Guna2CircleButton btnModulo) {
            Vista.PanelMenuLateral.SuspendLayout();

            CustomizableEdges customizableEdges = new CustomizableEdges();

            // Propiedades comunes
            btnModulo.Animated = true;
            btnModulo.Cursor = Cursors.Hand;
            btnModulo.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnModulo.CheckedState.FillColor = Color.PeachPuff;
            btnModulo.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnModulo.CustomImages.ImageSize = new Size(24, 24);
            btnModulo.FillColor = Color.White;
            btnModulo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnModulo.ForeColor = Color.White;
            btnModulo.ImageSize = new Size(24, 24);
            btnModulo.ShadowDecoration.CustomizableEdges = customizableEdges;
            btnModulo.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnModulo.Size = new Size(44, 44);
            btnModulo.TabIndex = Vista.PanelMenuLateral.Controls.Count + 1;

            // Agregar al panel lateral
            Vista.PanelMenuLateral.Controls.Add(btnModulo);

            Vista.PanelMenuLateral.ResumeLayout(false);
        }

        public IVistaPrincipal VistaPrincipal { get; }

        public IVistaContenedorModulos Vista { get; }

        public void Dispose() {
            _gestorModulos.ApagarModulos();

            Vista.Dispose();
        }
    }
}