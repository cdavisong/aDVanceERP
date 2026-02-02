using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Vistas {
    public partial class VistaTuplaProveedor : Form, IVistaTuplaProveedor {
        public VistaTuplaProveedor() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaProveedor);

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

        public string CodigoProveedor {
            get => fieldCodigo.Text;
            set => fieldCodigo.Text = value;
        }

        public string RazonSocial {
            get => fieldRazonSocial.Text;
            set {
                fieldRazonSocial.Text = value;
                fieldRazonSocial.Margin = fieldRazonSocial.AjusteAutomaticoMargenTexto();
            }
        }

        public string Telefonos {
            get => fieldTelefonos.Text;
            set {
                fieldTelefonos.Text = value;
                fieldTelefonos.Margin = fieldTelefonos.AjusteAutomaticoMargenTexto();
            }
        }

        public string Direccion {
            get => fieldDireccion.Text;
            set {
                fieldDireccion.Text = value;
                fieldDireccion.Margin = fieldDireccion.AjusteAutomaticoMargenTexto();
            }
        }

        public string NombreRepresentante {
            get => fieldNombreRepresentante.Text;
            set {
                fieldNombreRepresentante.Text = value;
                fieldNombreRepresentante.Margin = fieldNombreRepresentante.AjusteAutomaticoMargenTexto();
            }
        }

        public bool Activo {
            get => fieldEstado.Text.Equals("Activo");
            set {
                fieldEstado.Text = value ? "Activo" : "Inactivo";
                fieldEstado.ForeColor = value ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
            }
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