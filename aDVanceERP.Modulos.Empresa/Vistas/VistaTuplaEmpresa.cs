using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Modulos.Empresa.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Empresa.Vistas {
    public partial class VistaTuplaEmpresa : Form, IVistaTuplaEmpresa {
        public VistaTuplaEmpresa() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaEmpresa);

            Inicializar();
        }

        public string NombreVista {
            get => $"{Id:0000}{Name}";
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

        public string Nombre { 
            get => fieldNombreComercial.Text;
            set {
                fieldNombreComercial.Text = value;
                fieldNombreComercial.Margin = fieldNombreComercial.AjusteAutomaticoMargenTexto();
            }
        }

        public string RazonSocial { get; set; }

        public string? Rif { 
            get => fieldRif.Text;
            set => fieldRif.Text = value ?? string.Empty;
        }

        public string? Direccion { 
            get => fieldDireccion.Text;
            set {
                fieldDireccion.Text = value;
                fieldDireccion.Margin = fieldDireccion.AjusteAutomaticoMargenTexto();
            }
        }

        public string? Telefono { 
            get => fieldTelefono.Text;
            set => fieldTelefono.Text = value ?? string.Empty;
        }

        public string? Email { 
            get => fieldEmail.Text;
            set {
                fieldEmail.Text = value;
                fieldEmail.Margin = fieldEmail.AjusteAutomaticoMargenTexto();
            }
        }

        public DateTime FechaRegistro {
            get => fieldFechaRegistro.Text.Equals("-")
                    ? DateTime.MinValue
                    : DateTime.ParseExact(fieldFechaRegistro.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            set => fieldFechaRegistro.Text = value.Equals(DateTime.MinValue)
                ? "-"
                : value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;

        public void Inicializar() {
            // Eventos
            btnEditar.Click += delegate (object? sender, EventArgs e) { EditarDatosTupla?.Invoke(this, e); };
            btnEliminar.Click += delegate (object? sender, EventArgs e) { EliminarDatosTupla?.Invoke(this, e); };
        }

        public void Mostrar() {
            BringToFront();
            Show();
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