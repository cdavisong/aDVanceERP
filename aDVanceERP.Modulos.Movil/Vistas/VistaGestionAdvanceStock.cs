using aDVanceERP.Modulos.Movil.Interfaces;
using aDVanceERP.Modulos.Movil.Properties;

namespace aDVanceERP.Modulos.Movil.Vistas {
    public partial class VistaGestionAdvanceStock : Form, IVistaGestionAdvanceStock {

        // ── Backing fields ────────────────────────────────────────────────────
        private bool _dispositivoConectado;
        private bool _appInstalada;
        private bool _mostrarBotonEnviarCatalogos;
        private bool _mostrarBotonEliminarCatalogos;
        private bool _mostrarBotonImportarSesiones;
        private bool _catalogosExistenEnDispositivo;
        private DateTime? _fechaActualizacionCatalogos;

        public VistaGestionAdvanceStock() {
            InitializeComponent();

            NombreVista = nameof(VistaGestionAdvanceStock);

            Inicializar();
        }

        // ── IVistaBase ────────────────────────────────────────────────────────

        public string NombreVista {
            get => Name;
            private set => Name = value;
        }

        public bool Habilitada {
            get => Enabled;
            set => Enabled = value;
        }

        public Point Coordenadas {
            get => Location;
            set => Location = value;
        }

        public Size Dimensiones {
            get => Size;
            set => Size = value;
        }

        public bool DispositivoConectado {
            get => _dispositivoConectado;
            set {
                _dispositivoConectado = value;
                fieldEstadoDispositivoAdb.ForeColor = value ? Color.SeaGreen : Color.Firebrick;
                fieldEstadoDispositivoAdb.Image = value ? Resources.okG_16px : Resources.noR_16px;
                fieldEstadoDispositivoAdb.Text = value ? "     Conectado y autorizado" : "     Desconectado";
            }
        }

        public bool AppInstalada {
            get => _appInstalada;
            set {
                _appInstalada = value;
                fieldEstadoInstalcionApp.ForeColor = value ? Color.SeaGreen : Color.Firebrick;
                fieldEstadoInstalcionApp.Image = value ? Resources.okG_16px : Resources.noR_16px;
                fieldEstadoInstalcionApp.Text = value
                    ? "     App detectada en el dispositivo"
                    : "     App no detectada en el dispositivo";
            }
        }

        public bool CatalogosExistenEnDispositivo {
            get => _catalogosExistenEnDispositivo;
            set {
                _catalogosExistenEnDispositivo = value;
                fieldEstadoCatalogoDispositivo.Image = value ? Resources.okG_16px : Resources.noR_16px;
                fieldEstadoCatalogoDispositivo.ForeColor = value ? Color.SeaGreen : Color.Firebrick;
                fieldEstadoCatalogoDispositivo.Text = value
                    ? "     Catálogos disponibles"
                    : "     Sin catálogos en el dispositivo";
            }
        }

        public DateTime? FechaActualizacionCatalogos {
            get => _fechaActualizacionCatalogos;
            set {
                _fechaActualizacionCatalogos = value;

                if (value == null) {
                    fieldUltimaActualizacionCatalogos.Text = "Desconocida";
                    fieldUltimaActualizacionCatalogos.ForeColor = Color.Gray;
                    return;
                }

                var antiguedad = DateTime.Now - value.Value;
                string texto;

                if (antiguedad.TotalMinutes < 2)
                    texto = "Hace un momento";
                else if (antiguedad.TotalHours < 1)
                    texto = $"Hace {(int) antiguedad.TotalMinutes} min";
                else if (antiguedad.TotalDays < 1)
                    texto = $"Hoy a las {value.Value:HH:mm}";
                else if (antiguedad.TotalDays < 2)
                    texto = $"Ayer a las {value.Value:HH:mm}";
                else
                    texto = value.Value.ToString("dd/MM/yyyy HH:mm");

                fieldUltimaActualizacionCatalogos.Text = texto;
                fieldUltimaActualizacionCatalogos.ForeColor = Color.Black;
            }
        }

        public bool MostrarBotonEnviarCatalogos {
            get => _mostrarBotonEnviarCatalogos;
            set {
                _mostrarBotonEnviarCatalogos = value;
                btnEnviarCatalogos.Visible = value;
            }
        }

        public bool MostrarBotonEliminarCatalogos {
            get => _mostrarBotonEliminarCatalogos;
            set {
                _mostrarBotonEliminarCatalogos = value;
                btnEliminarCatalogos.Visible = value;
            }
        }

        public bool MostrarBotonImportarSesiones {
            get => _mostrarBotonImportarSesiones;
            set {
                _mostrarBotonImportarSesiones = value;
                btnImportarSesiones.Visible = value;
            }
        }

        public int ArchivosDisponiblesDispositivo {
            get => int.TryParse(
                fieldArchivosDisponiblesDispositivo.Text.Split(' ')[0], out int n) ? n : 0;
            set => fieldArchivosDisponiblesDispositivo.Text =
                $"{value} sesión{(value != 1 ? "es" : string.Empty)}";
        }

        public void ActualizarArchivosSesion(
            List<(string fileName, DateTime fechaHora, double tamanoKb)> archivos) {

            LimpiarArchivosSesion();

            if (archivos == null || archivos.Count == 0)
                return;

            var indice = 0;

            foreach (var archivo in archivos) {
                var tupla = new VistaTuplaArchivoVenta {
                    NombreArchivo = archivo.fileName,
                    Fecha = archivo.fechaHora,
                    TamannoArchivo = $"{archivo.tamanoKb} KB",
                    TopLevel = false,
                    Location = new Point(0, indice++ * 42),
                    Size = new Size(panelArchivosSesion.Width - 20, 42),
                    Visible = true
                };

                tupla.ImportarArchivo += OnImportarArchivoSesion;

                panelArchivosSesion.Controls.Add(tupla);
            }
        }

        public event EventHandler? VerificarConexion;
        public event EventHandler? EnviarCatalogos;
        public event EventHandler? EliminarCatalogos;
        public event EventHandler? ImportarSesiones;

        public void Inicializar() {
            btnVerificarConexion.Click += delegate { VerificarConexion?.Invoke(this, EventArgs.Empty); };
            btnEnviarCatalogos.Click += delegate { EnviarCatalogos?.Invoke(this, EventArgs.Empty); };
            btnEliminarCatalogos.Click += delegate { EliminarCatalogos?.Invoke(this, EventArgs.Empty); };
            btnImportarSesiones.Click += delegate { ImportarSesiones?.Invoke(this, EventArgs.Empty); };
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() { }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() { }

        /// <summary>
        /// Relay del evento ImportarArchivo de la tupla hacia ImportarSesiones
        /// del presentador, pasando el nombre del archivo como sender para que
        /// el presentador pueda importar sólo esa sesión específica.
        /// </summary>
        private void OnImportarArchivoSesion(object? sender, string nombreArchivo) {
            ImportarSesiones?.Invoke(nombreArchivo, EventArgs.Empty);
        }

        private void LimpiarArchivosSesion() {
            var tuplas = panelArchivosSesion.Controls
                .OfType<VistaTuplaArchivoVenta>()
                .ToList();

            foreach (var tupla in tuplas) {
                tupla.ImportarArchivo -= OnImportarArchivoSesion;
                tupla.Cerrar();
            }

            panelArchivosSesion.Controls.Clear();
        }
    }
}