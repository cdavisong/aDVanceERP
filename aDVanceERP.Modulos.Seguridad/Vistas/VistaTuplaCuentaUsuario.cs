using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Vistas {
    public partial class VistaTuplaCuentaUsuario : Form, IVistaTuplaCuentaUsuario {
        public VistaTuplaCuentaUsuario() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaCuentaUsuario);

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

        public string Id {
            get => fieldId.Text;
            set => fieldId.Text = value;
        }

        public string NombreUsuario {
            get => fieldNombreUsuario.Text;
            set => fieldNombreUsuario.Text = value;
        }

        public string NombreRolUsuario {
            get => fieldNombreRolUsuario.Text;
            set {
                fieldNombreRolUsuario.Text = value;

                if (value != null && value.Equals("Administrador")) {
                    btnEditar.Enabled = false;
                    btnEliminar.Enabled = false;
                }
            }
        }

        public string EstadoCuentaUsuario {
            get => fieldEstadoCuentaUsuario.Text;
            set => fieldEstadoCuentaUsuario.Text = value;
        }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;
    
        public void Inicializar() {
            // Eventos
            btnEditar.Click += delegate(object? sender, EventArgs e) { EditarDatosTupla?.Invoke(this, e); };
            btnEliminar.Click += delegate(object? sender, EventArgs e) { EliminarDatosTupla?.Invoke(this, e); };
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