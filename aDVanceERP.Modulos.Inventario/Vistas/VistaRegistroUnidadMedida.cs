using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaRegistroUnidadMedida : Form, IVistaRegistroUnidadMedida {
        private bool _modoEdicion = false;

        public VistaRegistroUnidadMedida() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroUnidadMedida);

            Inicializar();
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                _modoEdicion = value;

                fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
                btnRegistrarActualizar.Text = value ? "Actualizar unidad de medida" : "Registrar unidad de medida";
            }
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
        public string Nombre { 
            get => fieldNombre.Text;
            set => fieldNombre.Text = value;
        }

        public string Abreviatura {
            get => fieldAbreviatura.Text;
            set => fieldAbreviatura.Text = value;
        }

        public string? Descripcion {
            get => fieldDescripcion.Text;
            set => fieldDescripcion.Text = value;
        }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            btnRegistrarActualizar.Click += delegate (object? sender, EventArgs args) {
                if (ModoEdicion)
                    EditarEntidad?.Invoke(sender, args);
                else
                    RegistrarEntidad?.Invoke(sender, args);
            };
            btnSalir.Click += delegate (object? sender, EventArgs args) { Ocultar(); };
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {
            Nombre = string.Empty;
            Abreviatura = string.Empty;
            Descripcion = string.Empty;
        }

        public void Cerrar() {
            Dispose();
        }
    }
}
