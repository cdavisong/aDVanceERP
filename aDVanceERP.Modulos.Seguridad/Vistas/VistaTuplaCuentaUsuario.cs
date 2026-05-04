using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Vistas {
    public partial class VistaTuplaCuentaUsuario : Form, IVistaTuplaCuentaUsuario {
        private bool _administrador;
        private bool _aprobado;

        public VistaTuplaCuentaUsuario() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaCuentaUsuario);

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

        public string NombrePersona {
            get => fieldNombre.Text;
            set {
                fieldNombre.Text = value;
                fieldNombre.AjusteAutomaticoMargenTexto();
            }
        }

        public string NombreUsuario {
            get => fieldNombreUsuario.Text;
            set => fieldNombreUsuario.Text = value;
        }

        public string? Email {
            get => fieldEmail.Text;
            set { 
                fieldEmail.Text = value;
                fieldEmail.AjusteAutomaticoMargenTexto();
            }
        }

        public string NombreRol {
            get => fieldRol.Text;
            set => fieldRol.Text = value;
        }

        public bool Administrador { 
            get => _administrador; 
            set {
                _administrador = value; 

                fieldEsAdmin.Text = value ? "Sí" : "No";
            }
        }

        public bool Aprobado { 
            get => _aprobado; 
            set {
                _aprobado = value; 

                fieldAprobado.Text = value ? "Sí" : "No";
                btnAprobarCuentaUsuario.Enabled = !value;
            }
        }

        public bool Estado {
            get => fieldEstado.Text.Equals("Activo");
            set {
                var (colorFondo, colorFuente) = ObtenerColorEstado(value);

                fieldEstado.Text = value ? "Activo" : "Inactivo";
                fieldEstado.DisabledState.BorderColor = colorFondo;
                fieldEstado.DisabledState.FillColor = colorFondo;
                fieldEstado.DisabledState.ForeColor = colorFuente;
            }
        }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;
        public event EventHandler? AprobarCuentaUsuario;

        public void Inicializar() {
            // Eventos
            btnEditar.Click += delegate(object? sender, EventArgs e) { 
                EditarDatosTupla?.Invoke(this, e); 
            };
            btnEliminar.Click += delegate(object? sender, EventArgs e) { 
                EliminarDatosTupla?.Invoke(this, e); 
            };
            btnAprobarCuentaUsuario.Click += delegate(object? sender, EventArgs e) { 
                AprobarCuentaUsuario?.Invoke(this, e); 
            };
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

        private (Color colorFondo, Color colorFuente) ObtenerColorEstado(bool estado) {
            return estado
                ? (Color.FromArgb(232, 245, 233), Color.FromArgb(46, 125, 50))  // Verde
                : (Color.FromArgb(252, 228, 236), Color.FromArgb(198, 40, 40)); // Rojo
        }
    }
}