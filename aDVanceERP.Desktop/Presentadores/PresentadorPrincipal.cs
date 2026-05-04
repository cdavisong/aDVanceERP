using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Desktop.Vistas;

using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;

namespace aDVanceERP.Desktop.Presentadores {
    public partial class PresentadorPrincipal : IPresentadorVistaPrincipal<IVistaPrincipal> {
        public PresentadorPrincipal() {
            Inicializar();
            InicializarVistas();
            InicializarEventos();
        }

        private void Inicializar() {
            // Vista principal
            Vista = new VistaPrincipal();

            // Contendor de seguridad
            Seguridad = new PresentadorContenedorSeguridad(this, new VistaContenedorSeguridad());

            // Contenedor de módulos
            Modulos = new PresentadorContenedorModulos(Vista, new VistaContenedorModulos());
        }

        private void InicializarVistas() {
            // Contendor de seguridad
            Vista.PanelCentral.Registrar(Seguridad.Vista);

            // Contenedor de módulos
            Vista.PanelCentral.Registrar(Modulos.Vista);
        }

        private void InicializarEventos() {
            // Vista principal
            ((Form)Vista).Shown += OnVistaPrincipalMostrada;

            // Contenedor de seguridad
            AgregadorEventos.Suscribir<EventoUsuarioAutenticado>(OnUsuarioAutenticado);
            AgregadorEventos.Suscribir<EventoSesionCerrada>(OnSesionCerrada);
        }

        public IVistaPrincipal Vista { get; private set; } = null!;

        public IPresentadorVistaContenedorSeguridad<IVistaContenedorSeguridad> Seguridad { get; private set; } = null!;

        public IPresentadorVistaContenedorModulos<IVistaContenedorModulos> Modulos { get; private set; } = null!;

        private void OnVistaPrincipalMostrada(object? sender, EventArgs e) {
            Vista.BarraTitulo.OcultarTodos();
            Vista.BarraEstado.OcultarTodos();
            Vista.ModificarVisibilidadBotonesBarraTitulo(false);

            AgregadorEventos.Publicar(new EventoMostrarVistaContenedorSeguridad());
        }

        private void OnUsuarioAutenticado(EventoUsuarioAutenticado e) {
            Vista.ModificarVisibilidadBotonesBarraTitulo(true);
            Vista.PanelCentral.Ocultar(nameof(VistaContenedorSeguridad));
            Vista.PanelCentral.Restaurar(nameof(VistaContenedorModulos));
            Vista.PanelCentral.Mostrar(nameof(VistaContenedorModulos));

            var persona = RepoPersona.Instancia.ObtenerPorId(ContextoSeguridad.UsuarioAutenticado?.IdPersona ?? 0);
            var nombreConApellidos = persona?.NombreCompleto.Split(' ').Length > 0;
            var nombreUsuario = ContextoSeguridad.UsuarioAutenticado?.Nombre ?? "invitado";
            var nombrePersona = persona?.NombreCompleto.Split(' ').FirstOrDefault() ?? nombreUsuario;

            Modulos.Vista.ActualizarPortadaInicio($"{Program.Version}-beta", nombrePersona);
        }

        private void OnSesionCerrada(EventoSesionCerrada e) {
            Vista.ModificarVisibilidadBotonesBarraTitulo(false);
            Vista.PanelCentral.Ocultar(nameof(VistaContenedorModulos));
            Vista.PanelCentral.Restaurar(nameof(VistaContenedorSeguridad));
            Vista.PanelCentral.Mostrar(nameof(VistaContenedorSeguridad));

            AgregadorEventos.Publicar(new EventoMostrarVistaAutenticacionCuentaUsuario());
        }

        public void AdicionarBotonBarraTitulo(Guna2Button btnTitulo) {
            CustomizableEdges customizableEdges = new CustomizableEdges();

            btnTitulo.Animated = true;
            btnTitulo.BorderRadius = 5;
            btnTitulo.Cursor = Cursors.Hand;
            btnTitulo.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnTitulo.CustomImages.ImageSize = new Size(20, 20);
            btnTitulo.CustomizableEdges = customizableEdges;
            btnTitulo.FillColor = Color.WhiteSmoke;
            btnTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnTitulo.ForeColor = Color.White;
            btnTitulo.ImageSize = new Size(20, 20);
            btnTitulo.Margin = new Padding(1);
            btnTitulo.ShadowDecoration.CustomizableEdges = customizableEdges;
            btnTitulo.Size = new Size(50, 50);
            btnTitulo.TabIndex = Vista.BotonesTitulo.Controls.Count + 1;

            Vista.BotonesTitulo.Controls.Add(btnTitulo);
        }

        public void Dispose() {
            Modulos.Dispose();
            Seguridad.Dispose();
            Vista.Dispose();
        }
    }
}