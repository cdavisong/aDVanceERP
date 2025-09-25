using aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja.Plantillas;

using System.Globalization;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja {
    public partial class VistaRegistroMovimientoCaja : Form, IVIstaRegistroMovimientoCaja {
        private bool _modoEdicion;
        private DateTime _fechaMovimiento;

        public VistaRegistroMovimientoCaja() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroMovimientoCaja);

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

        public DateTime Fecha {
            get => _fechaMovimiento;
            set {
                _fechaMovimiento = value;

                fieldSubtitulo.Text = ModoEdicion ?
                    $"Detalles de movimiento con fecha {value:yyyy-MM-dd}" :
                    $"Movimiento de efectivo en caja con fecha {value:yyyy-MM-dd}";
            }
        }

        public decimal Monto {
            get => decimal.TryParse(fieldMonto.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var total)
                ? total
                : 0;
            set => fieldMonto.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public string TipoMovimiento {
            get => fieldTipoMovimiento.Text;
            set => fieldTipoMovimiento.Text = value;
        }

        public string Concepto {
            get => fieldConcepto.Text;
            set => fieldConcepto.Text = value;
        }

        public string? Observaciones {
            get => fieldObservaciones.Text;
            set => fieldObservaciones.Text = value;
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                btnRegistrar.Text = value ? "Actualizar movimiento" : "Registrar movimiento";
                _modoEdicion = value;
            }
        }


        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            // Configuración de la ventana
            if (!ModoEdicion)
                Fecha = DateTime.Now;

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
            Monto = 0;
            TipoMovimiento = string.Empty;
            Concepto = string.Empty;
            Observaciones = string.Empty;
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
