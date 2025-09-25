using aDVanceERP.Modulos.Inventario.MVP.Vistas.TipoMateriaPrima.Plantillas;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.TipoMateriaPrima {
    public partial class VistaRegistroTipoMateriaPrima : Form, IVistaRegistroTipoMateriaPrima {
        private bool _modoEdicion;

        public VistaRegistroTipoMateriaPrima() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroTipoMateriaPrima);

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

        public string NombreTipoMateriaPrima {
            get => fieldNombre.Text;
            set => fieldNombre.Text = value;
        }

        public string? Descripcion {
            get => fieldDescripcion.Text;
            set => fieldDescripcion.Text = value;
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
                btnRegistrar.Text = value ? "Actualizar tipo de producto" : "Registrar tipo de producto";
                _modoEdicion = value;
            }
        }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;
        

        public void Inicializar() {
            // Eventos
            btnCerrar.Click += delegate (object? sender, EventArgs args) {
                Close();
            };
            btnRegistrar.Click += delegate (object? sender, EventArgs args) {
                if (ModoEdicion)
                    EditarEntidad?.Invoke(sender, args);
                else
                    RegistrarEntidad?.Invoke(sender, args);
            };
            btnSalir.Click += delegate (object? sender, EventArgs args) {
                Close();
            };
        }

        public void Mostrar() {
            BringToFront();
            ShowDialog();
        }

        public void Restaurar() {
            NombreTipoMateriaPrima = string.Empty;
            Descripcion = string.Empty;
            ModoEdicion = false;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }
    }
}
