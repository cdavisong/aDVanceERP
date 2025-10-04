using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Extension.Controladores;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

using aDVanceERP.Desktop.Presentadores.ContenedorModulos;
using aDVanceERP.Desktop.Presentadores.ContenedorSeguridad;
using aDVanceERP.Desktop.Vistas.Modulos;
using aDVanceERP.Desktop.Vistas.Principal;
using aDVanceERP.Desktop.Vistas.Seguridad;
using aDVanceERP.Desktop.Properties;
using aDVanceERP.Core.Infraestructura.Globales;
using Guna.UI2.WinForms.Suite;
using Guna.UI2.WinForms;

namespace aDVanceERP.Desktop.Presentadores.Principal;

public partial class PresentadorPrincipal : IPresentadorVistaPrincipal<IVistaPrincipal> {
    private readonly GestorModulosExtensibles _gestorModulos = new GestorModulosExtensibles();

    public PresentadorPrincipal() {
        Vista = new VistaPrincipal();
        Seguridad = new PresentadorSeguridad(Vista, new VistaSeguridad());
        Modulos = new PresentadorModulos(Vista, new VistaModulos());

        // Adicionar vistas al panel central
        Vista.PanelCentral.Registrar(Seguridad.Vista);
        Vista.PanelCentral.Registrar(Modulos.Vista);

        // Eventos de la vista principal
        ((Form) Vista).Shown += OnVistaPrincipalMostrada;

        // Eventos de seguridad
        AgregadorEventos.Suscribir("EventoUsuarioAutenticado", OnUsuarioAutenticado);
        AgregadorEventos.Suscribir("EventoSesionCerrada", OnSesionCerrada);

        // Cargar módulos extensiones de la aplicación
        CargarModulosExtension();
    }

    public IVistaPrincipal Vista { get; }

    public IPresentadorVistaSeguridad<IVistaSeguridad> Seguridad { get; }

    public IPresentadorVistaModulos<IVistaModulos> Modulos { get; }

    private void CargarModulosExtension() {
        _gestorModulos.CargarModulos(this);
    }

    private void OnVistaPrincipalMostrada(object? sender, EventArgs e) {
        Vista.BarraTitulo.OcultarTodos();
        Vista.ModificarVisibilidadBotonesBarraTitulo(false);
        Vista.PanelCentral.Mostrar(nameof(VistaSeguridad));
        Vista.BarraEstado.OcultarTodos();

        // Verificar si existe el módulo de seguridad, en caso contrario pasar a la vista inicial directamente
        if (_gestorModulos.ObtenerModulosExtension().FirstOrDefault(m => m.Nombre.Equals("MOD_SEGURIDAD")) == null)
            AgregadorEventos.Publicar("EventoUsuarioAutenticado", string.Empty);
    }

    private void OnUsuarioAutenticado(string obj) {
        Vista.ModificarVisibilidadBotonesBarraTitulo(true);
        Vista.PanelCentral.Ocultar(nameof(VistaSeguridad));
        Vista.PanelCentral.Restaurar(nameof(VistaModulos));
        Vista.PanelCentral.Mostrar(nameof(VistaModulos));

        Modulos.Vista.MensajePortada = Resources.MensajePortada
            .Replace("[version]", $"{Program.Version}-beta")
            .Replace("[user]", ContextoSeguridad.UsuarioAutenticado?.Nombre ?? "invitado");
    }

    private void OnSesionCerrada(string obj) {
        Vista.ModificarVisibilidadBotonesBarraTitulo(false);
        Vista.PanelCentral.Ocultar(nameof(VistaModulos));
        Vista.PanelCentral.Restaurar(nameof(VistaSeguridad));
        Vista.PanelCentral.Mostrar(nameof(VistaSeguridad));

        AgregadorEventos.Publicar("MostrarVistaAutenticacionUsuario", string.Empty);
    }

    public void AdicionarBotonBarraTitulo(Guna2Button btnTitulo) {
        Vista.BotonesTitulo.SuspendLayout();

        CustomizableEdges customizableEdges = new CustomizableEdges();

        btnTitulo.Animated = true;
        btnTitulo.Cursor = Cursors.Hand;
        btnTitulo.CustomImages.ImageAlign = HorizontalAlignment.Center;
        btnTitulo.CustomImages.ImageSize = new Size(20, 20);
        btnTitulo.CustomizableEdges = customizableEdges;
        btnTitulo.FillColor = Color.WhiteSmoke;
        btnTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        btnTitulo.ForeColor = Color.White;
        btnTitulo.ImageSize = new Size(20, 20);
        btnTitulo.ShadowDecoration.CustomizableEdges = customizableEdges;
        btnTitulo.Size = new Size(50, 50);
        btnTitulo.TabIndex = Vista.BotonesTitulo.Controls.Count + 1;

        Vista.BotonesTitulo.Controls.Add(btnTitulo);
        Vista.BotonesTitulo.ResumeLayout(false);
    }

    public void Dispose() {
        _gestorModulos.ApagarModulos();

        Modulos.Dispose();
        Seguridad.Dispose();
        Vista.Dispose();
    }
}