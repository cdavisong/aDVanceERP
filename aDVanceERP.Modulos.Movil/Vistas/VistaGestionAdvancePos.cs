using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Modulos.Movil.Interfaces;
using aDVanceERP.Modulos.Movil.Properties;

namespace aDVanceERP.Modulos.Movil.Vistas {
    public partial class VistaGestionAdvancePos : Form, IVistaGestionAdvancePos {
        private bool _dispositivoConectado;
        private bool _appInstalada;
        private bool _catalogoExisteEnDispositivo;
        private DateTime? _fechaActualizacionCatalogo;
        private bool _mostrarBotonEnviarCatalogo;
        private bool _mostrarBotonEliminarCatalogo;
        private bool _mostrarBotonImportarVentas;

        public VistaGestionAdvancePos() {
            InitializeComponent();

            NombreVista = nameof(VistaGestionAdvancePos);

            Inicializar();
        }

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
                fieldEstadoInstalcionApp.Text = value ? "     App detectada en el dispositivo" : "     App no detectada en el dispositivo";
            }
        }

        public bool CatalogoExisteEnDispositivo {
            get => _catalogoExisteEnDispositivo;
            set {
                _catalogoExisteEnDispositivo = value;
                fieldEstadoCatalogoDispositivo.Image = value ? Resources.okG_16px : Resources.noR_16px;
                fieldEstadoCatalogoDispositivo.ForeColor = value ? Color.SeaGreen : Color.Firebrick;
                fieldEstadoCatalogoDispositivo.Text = value
                    ? "     Catálogo disponible"
                    : "     Sin catálogo en el dispositivo";
            }
        }

        public DateTime? FechaActualizacionCatalogo {
            get => _fechaActualizacionCatalogo;
            set {
                _fechaActualizacionCatalogo = value;

                if (value == null) {
                    fieldUltimaActualizacionCatalogo.Text = "Desconocida";
                    fieldUltimaActualizacionCatalogo.ForeColor = Color.Gray;
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

                fieldUltimaActualizacionCatalogo.Text = texto;
                fieldUltimaActualizacionCatalogo.ForeColor = Color.Black;
            }
        }

        public Almacen? Almacen {
            get => fieldAlmacen.SelectedItem as Almacen;
            set => fieldAlmacen.SelectedItem = value;
        }

        public bool MostrarBotonEnviarCatalogo {
            get => _mostrarBotonEnviarCatalogo;
            set {
                _mostrarBotonEnviarCatalogo = value;
                btnEnviarCatalogo.Visible = value;
            }
        }

        public bool MostrarBotonEliminarCatalogo {
            get => _mostrarBotonEliminarCatalogo;
            set {
                _mostrarBotonEliminarCatalogo = value;
                btnEliminarCatalogo.Visible = value;
            }
        }

        public int ArchivosDisponiblesDispositivo {
            get => int.Parse(fieldArchivosDisponiblesDispositivo.Text.Split(' ')[0]);
            set => fieldArchivosDisponiblesDispositivo.Text = $"{value} archivo{(value > 1 ? "s" : string.Empty)}";
        }

        public bool MostrarBotonImportarVentas {
            get => _mostrarBotonImportarVentas;
            set {
                _mostrarBotonImportarVentas = value;
                btnImportarVentas.Visible = value;
                btnImportarTodasLasVentas.Visible = value;
            }
        }

        public event EventHandler? VerificarConexion;
        public event EventHandler? EnviarCatalogo;
        public event EventHandler? EliminarCatalogo;
        public event EventHandler? ImportarVentas;
        public event EventHandler? ImportarTodasLasVentas;

        public void Inicializar() {
            // Eventos
            btnVerificarConexion.Click += delegate { VerificarConexion?.Invoke(this, EventArgs.Empty); };
            btnEnviarCatalogo.Click += delegate { EnviarCatalogo?.Invoke(Almacen?.Id ?? 0, EventArgs.Empty); };
            btnEliminarCatalogo.Click += delegate { EliminarCatalogo?.Invoke(this, EventArgs.Empty); };
            btnImportarVentas.Click += delegate { ImportarVentas?.Invoke(this, EventArgs.Empty); };
            btnImportarTodasLasVentas.Click += delegate { ImportarTodasLasVentas?.Invoke(this, EventArgs.Empty); };
        }

        public void ActualizarArchivosVenta(List<(string fileName, DateTime fecha, double tamanoKb)> archivosVenta) {
            LimpiarArchivosVenta();

            if (archivosVenta == null || archivosVenta.Count == 0)
                return;

            var indice = 0;

            foreach (var archivoVenta in archivosVenta) {
                var tuplaArchivoVenta = new VistaTuplaArchivoVenta() {
                    NombreArchivo = archivoVenta.fileName,
                    Fecha = archivoVenta.fecha,
                    TamannoArchivo = $"{archivoVenta.tamanoKb} KB",
                    TopLevel = false,
                    Location = new Point(0, indice++ * 42),
                    Size = new Size(panelArchivosVenta.Width - 20, 42),
                    Visible = true
                };

                tuplaArchivoVenta.ImportarArchivo += OnImportarArchivoVenta;

                panelArchivosVenta.Controls.Add(tuplaArchivoVenta);
            }
        }

        private void OnImportarArchivoVenta(object? sender, string nombreArchivo) {
            ImportarVentas?.Invoke(nombreArchivo, EventArgs.Empty);
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            // ...
        }

        private void LimpiarArchivosVenta() {
            var tuplas = panelArchivosVenta.Controls
                .OfType<VistaTuplaArchivoVenta>()
                .ToList();

            foreach (var tupla in tuplas) {
                tupla.ImportarArchivo -= OnImportarArchivoVenta;
                tupla.Cerrar();
            }

            panelArchivosVenta.Controls.Clear();
        }

        public void CargarAlmacenes(Almacen[] almacenes) {
            fieldAlmacen.Items.Clear();
            fieldAlmacen.Items.AddRange(almacenes);

            if (fieldAlmacen.Items.Count > 0)
                fieldAlmacen.SelectedIndex = 0;
        }
    }
}