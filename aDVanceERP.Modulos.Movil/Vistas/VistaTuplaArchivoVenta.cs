using aDVanceERP.Modulos.Movil.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Movil.Vistas {
    public partial class VistaTuplaArchivoVenta : Form, IVistaTuplaArchivoVenta {
        public VistaTuplaArchivoVenta() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaArchivoVenta);

            Inicializar();
        }

        public string NombreVista {
            get => $"{Name}{NombreArchivo}";
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

        public string NombreArchivo {
            get => fieldNombreArchivo.Text;
            set => fieldNombreArchivo.Text = value;
        }

        public DateTime Fecha {
            get => fieldFecha.Text.Equals("-")
                        ? DateTime.MinValue
                        : DateTime.ParseExact(fieldFecha.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            set => fieldFecha.Text = value.Equals(DateTime.MinValue)
                ? "-"
                : value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public string TamannoArchivo {
            get => fieldTamannoArchivo.Text;
            set => fieldTamannoArchivo.Text = value;
        }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;
        public event EventHandler<string>? ImportarArchivo;

        public void Inicializar() {
            // Eventos
            btnImportar.Click += delegate (object? sender, EventArgs e) { ImportarArchivo?.Invoke(this, NombreArchivo); };
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
            Dispose();
        }
    }
}