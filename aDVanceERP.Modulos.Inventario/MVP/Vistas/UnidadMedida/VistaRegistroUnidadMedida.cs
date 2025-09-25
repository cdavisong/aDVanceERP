using aDVanceERP.Modulos.Inventario.MVP.Vistas.UnidadMedida.Plantillas;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.UnidadMedida {
    public partial class VistaRegistroUnidadMedida : Form, IVistaRegistroUnidadMedida {
        private bool _modoEdicion;

        public VistaRegistroUnidadMedida() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroUnidadMedida);

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

        public string NombreUnidadMedida {
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

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
                btnRegistrar.Text = value ? "Actualizar unidad de medida" : "Registrar unidad de medida";
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
            NombreVista = string.Empty;
            Abreviatura = string.Empty;
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
