using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaRegistroEnvio : Form, IVistaRegistroEnvio {
        private bool _modoEdicion = false;

        public VistaRegistroEnvio() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroEnvio);

            Inicializar();
            InicializarPaisesPrefijos();
        }

        private void InicializarPaisesPrefijos() {
            fieldPaisesCliente.Items.Clear();
            fieldPaisesCliente.Items.AddRange(PrefijosInternacionales.ObtenerPaises());
            fieldPaisesCliente.StartIndex = 53;
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                _modoEdicion = value;

                fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
                btnRegistrarActualizar.Text = value ? "Actualizar el envío" : "Registrar el envío";
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
            
        }

        public void Cerrar() {
            Dispose();
        }

        public void CargarFacturasVentasPendientes(string[] numerosFacturasPendientes) {
            fieldNumeroFactura.Items.Clear();
            fieldNumeroFactura.Items.AddRange(numerosFacturasPendientes);
            fieldNumeroFactura.SelectedIndex = -1;
        }
    }
}
