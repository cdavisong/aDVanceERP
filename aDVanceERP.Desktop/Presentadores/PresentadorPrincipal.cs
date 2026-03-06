using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Desktop.Properties;
using aDVanceERP.Desktop.Vistas;

using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;

namespace aDVanceERP.Desktop.Presentadores {
    public partial class PresentadorPrincipal : IPresentadorVistaPrincipal<IVistaPrincipal> {
        private readonly VistaCargaDatos _cargaDatos;

        public PresentadorPrincipal() {
            _cargaDatos = new VistaCargaDatos();

            Vista = new VistaPrincipal();
            Seguridad = new PresentadorContenedorSeguridad(Vista, new VistaContenedorSeguridad());
            Modulos = new PresentadorContenedorModulos(Vista, new VistaContenedorModulos());

            // Adicionar vistas al panel central
            Vista.PanelCentral.Registrar(Seguridad.Vista);
            Vista.PanelCentral.Registrar(Modulos.Vista);

            // Eventos de la vista principal
            ((Form) Vista).Shown += OnVistaPrincipalMostrada;

            // Eventos de seguridad
            AgregadorEventos.Suscribir("EventoUsuarioAutenticado", OnUsuarioAutenticado);
            AgregadorEventos.Suscribir("EventoSesionCerrada", OnSesionCerrada);

            // Adicionar vistas comunes
            InicializarVistasComunes();
        }

        public IVistaPrincipal Vista { get; }

        public IPresentadorVistaContenedorSeguridad<IVistaContenedorSeguridad> Seguridad { get; }

        public IPresentadorVistaContenedorModulos<IVistaContenedorModulos> Modulos { get; }

        private void OnVistaPrincipalMostrada(object? sender, EventArgs e) {
            Vista.BarraTitulo.OcultarTodos();
            Vista.BarraEstado.OcultarTodos();
            Vista.ModificarVisibilidadBotonesBarraTitulo(false);
            Vista.PanelCentral.Mostrar(nameof(VistaContenedorSeguridad));

            Seguridad.ConfiguracionBaseDatos.ConfiguracionCargada += async (s, args) => {
                await Task.Run(() => {
                    // Cargar módulos extensiones de la aplicación
                    (Vista as Control)?.Invoke(() => {
                        _cargaDatos.TextoProgreso = "Cargando módulos y extensiones de la aplicación...";
                        _cargaDatos.Mostrar();

                        ((PresentadorContenedorModulos) Modulos).CargarModulosExtension(this);
                    });
                }).ContinueWith(t => {
                    if (t.IsFaulted) {
                        (Vista as Control)?.Invoke(() => {
                            CentroNotificaciones.MostrarNotificacion(
                                $"Error al cargar los módulos: {t.Exception?.InnerException?.Message}",
                                TipoNotificacionEnum.Error);
                        });
                    }

                    (Vista as Control)?.Invoke(() => {
                        // Verificar si existe el módulo de seguridad, en caso contrario pasar a la vista inicial directamente
                        if (!Modulos.ObtenerNombresModulosExtensionCargados().Any(m => m.Equals("MOD_SEGURIDAD")))
                            AgregadorEventos.Publicar("EventoUsuarioAutenticado", string.Empty);

                        _cargaDatos.Ocultar();
                    });
                });
            };
        }

        private void OnUsuarioAutenticado(string obj) {
            Vista.ModificarVisibilidadBotonesBarraTitulo(true);
            Vista.PanelCentral.Ocultar(nameof(VistaContenedorSeguridad));
            Vista.PanelCentral.Restaurar(nameof(VistaContenedorModulos));
            Vista.PanelCentral.Mostrar(nameof(VistaContenedorModulos));

            Modulos.Vista.MensajePortada = Resources.MensajePortada
                .Replace("[version]", $"{Program.Version}-beta")
                .Replace("[user]", ContextoSeguridad.UsuarioAutenticado?.Nombre ?? "invitado");
        }

        private void OnSesionCerrada(string obj) {
            Vista.ModificarVisibilidadBotonesBarraTitulo(false);
            Vista.PanelCentral.Ocultar(nameof(VistaContenedorModulos));
            Vista.PanelCentral.Restaurar(nameof(VistaContenedorSeguridad));
            Vista.PanelCentral.Mostrar(nameof(VistaContenedorSeguridad));

            AgregadorEventos.Publicar("MostrarVistaAutenticacionUsuario", string.Empty);
        }

        private void InicializarVistasComunes() {
            // Módulos
            // Ubicaciones
            Modulos.Vista.PanelCentral.Registrar(new VistaGestionUbicaciones());
        }

        public void AdicionarBotonBarraTitulo(Guna2Button btnTitulo) {
            Vista.BotonesTitulo.SuspendLayout();

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
            btnTitulo.ShadowDecoration.CustomizableEdges = customizableEdges;
            btnTitulo.Size = new Size(50, 50);
            btnTitulo.TabIndex = Vista.BotonesTitulo.Controls.Count + 1;

            Vista.BotonesTitulo.Controls.Add(btnTitulo);
            Vista.BotonesTitulo.ResumeLayout(false);
        }

        public void Dispose() {
            Modulos.Dispose();
            Seguridad.Dispose();
            Vista.Dispose();
        }
    }
}