using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaTuplaAlmacen : Form, IVistaTuplaAlmacen {
        public VistaTuplaAlmacen() {
            InitializeComponent();
            Inicializar();
        }

        public string NombreVista {
            get => $"{Name}{Id}";
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

        public Color ColorFondoTupla {
            get => layoutVista.BackColor;
            set => layoutVista.BackColor = value;
        }

        public bool EstadoSeleccion { get; set; }

        public long Id {
            get => Convert.ToInt64(fieldId.Text);
            set => fieldId.Text = value.ToString();
        }

        public string NombreAlmacen {
            get => fieldNombre.Text;
            set => fieldNombre.Text = value;
        }

        public string Tipo { 
            get => fieldTipo.Text;
            set => fieldTipo.Text = value;
        }

        public string Direccion {
            get => fieldDireccion.Text;
            set {
                fieldDireccion.Text = value;
                fieldDireccion.Margin = fieldDireccion.AjusteAutomaticoMargenTexto();
            }
        }

        public string Descripcion {
            get => fieldDescripcion.Text;
            set {
                fieldDescripcion.Text = value;
                fieldDescripcion.Margin = fieldDescripcion.AjusteAutomaticoMargenTexto();
            }
        }

        public bool Estado { 
            get => fieldEstado.Text.Equals("Activo");
            set {
                var (colorFondo, colorFuente) = ObtenerColorEstado(value);

                fieldEstado.Text = value ? "● Activo" : "X Inactivo";
                fieldEstado.DisabledState.BorderColor = colorFondo;
                fieldEstado.DisabledState.FillColor = colorFondo;
                fieldEstado.DisabledState.ForeColor = colorFuente;
            }
        }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;

        public event EventHandler<(long Id, FormatoDocumento Formato)>? ExportarDocumentoInventario;

        public void Inicializar() {
            // Eventos
            btnExportarDocumentoInventario.Click += delegate { btnExportarDocumentoInventario.ContextMenuStrip?.Show(btnExportarDocumentoInventario, new Point(0, 40)); };
            btnExportarPdf.Click += delegate { ExportarDocumentoInventario?.Invoke(this, (Id, FormatoDocumento.PDF)); };
            btnExportarXlsx.Click += delegate { ExportarDocumentoInventario?.Invoke(this, (Id, FormatoDocumento.Excel)); };
            btnEditar.Click += delegate (object? sender, EventArgs e) { EditarDatosTupla?.Invoke(Id, e); };
            btnEliminar.Click += delegate (object? sender, EventArgs e) { EliminarDatosTupla?.Invoke(Id, e); };
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        private (Color colorFondo, Color colorFuente) ObtenerColorEstado(bool estado) {
            return estado
                ? (Color.FromArgb(232, 245, 233), Color.FromArgb(46, 125, 50))  // Verde
                : (Color.FromArgb(252, 228, 236), Color.FromArgb(198, 40, 40)); // Rojo
        }

        public void Restaurar() {
            ColorFondoTupla = BackColor;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }
    }
}