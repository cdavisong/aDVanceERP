using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaRegistroAlmacen : Form, IVistaRegistroAlmacen {
        private bool _modoEdicion = false;

        public VistaRegistroAlmacen() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroAlmacen);

            Inicializar();
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                _modoEdicion = value;

                fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
                btnRegistrarActualizar.Text = value ? "Actualizar el almacén" : "Registrar el almacén";
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
        public string NombreAlmacen { 
            get => fieldNombre.Text;
            set => fieldNombre.Text = value;
        }

        public string? Direccion { 
            get => fieldDireccion.Text;
            set => fieldDireccion.Text = value;
        }

        public TipoAlmacenEnum Tipo { 
            get => (TipoAlmacenEnum) fieldTipo.SelectedIndex;
            set => fieldTipo.SelectedItem = value;
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
            NombreAlmacen = string.Empty;
            Direccion = string.Empty;
            Tipo = TipoAlmacenEnum.Secundario;
            Descripcion = string.Empty;
        }

        public void Cerrar() {
            Dispose();
        }
    }
}
