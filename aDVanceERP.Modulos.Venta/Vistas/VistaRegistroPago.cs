using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaRegistroPago : Form, IVistaRegistroPago {
        private bool _modoEdicion = false;

        public VistaRegistroPago() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroPago);

            Inicializar();
            InicializarPaisesPrefijos();
        }

        private void InicializarPaisesPrefijos() {
            fieldPaises.Items.Clear();
            fieldPaises.Items.AddRange(PrefijosInternacionales.ObtenerPaises());
            fieldPaises.StartIndex = 53;
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                _modoEdicion = value;

                fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
                btnRegistrarActualizar.Text = value ? "Actualizar el pago" : "Registrar el pago";
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

        public string NumeroFacturaVenta {
            get => fieldNumeroFactura.SelectedItem?.ToString() ?? string.Empty;
            set {
                if (fieldNumeroFactura.Items.Contains(value)) {
                    fieldNumeroFactura.SelectedItem = value;
                } else {
                    fieldNumeroFactura.SelectedIndex = -1;
                }
            }
        }

        public DateTime FechaPagoCliente {
            get => fieldFechaPago.Value;
            set => fieldFechaPago.Value = value;
        }

        public MetodoPagoEnum MetodoPago {
            get => (MetodoPagoEnum) fieldMetodoPago.SelectedIndex;
            set => fieldMetodoPago.SelectedItem = value.ObtenerDisplayName();
        }

        public decimal MontoPagado {
            get => decimal.TryParse(fieldMonto.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            set => fieldMonto.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public bool EstadoPendiente {
            get => fieldEstadoPendiente.Checked;
            set => fieldEstadoPendiente.Checked = value;
        }

        public string NumeroConfirmacion {
            get => $"{fieldPrefijoInternacional.Text} {fieldNumeroConfirmacion.Text}";
            set => fieldNumeroConfirmacion.Text = value;
        }

        public string NumeroTransaccion {
            get => fieldNumeroTransaccion.Text;
            set => fieldNumeroTransaccion.Text = value;
        }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            fieldMetodoPago.SelectedIndexChanged += delegate {
                separador1.Visible = MetodoPago == MetodoPagoEnum.TransferenciaBancaria;
                fieldTituloDatosTransferencia.Visible = MetodoPago == MetodoPagoEnum.TransferenciaBancaria;
                layoutTitulos2.Visible = MetodoPago == MetodoPagoEnum.TransferenciaBancaria;
                layoutDatos2.Visible = MetodoPago == MetodoPagoEnum.TransferenciaBancaria;
            };
            fieldPaises.SelectedIndexChanged += delegate {
                fieldPrefijoInternacional.Text = $"{PrefijosInternacionales.ObtenerPrefijo(fieldPaises.Text)}";
                fieldNumeroConfirmacion.IconLeft = PrefijosInternacionales.ObtenerFlag(fieldPaises.Text);
            };
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
            FechaPagoCliente = DateTime.Now;
            MetodoPago = MetodoPagoEnum.Efectivo;
            EstadoPendiente = false;
            NumeroConfirmacion = string.Empty;
            NumeroTransaccion = string.Empty;

            fieldNumeroFactura.SelectedIndex = -1;
            fieldMonto.Text = string.Empty;
            fieldPaises.SelectedIndex = 53;
            fieldNumeroConfirmacion.Text = string.Empty;
        }

        public void Cerrar() {
            Dispose();
        }

        public void CargarFacturasVentasPendientes(string[] numerosFacturasPendientes) {
            fieldNumeroFactura.Items.Clear();
            fieldNumeroFactura.Items.AddRange(numerosFacturasPendientes);
            fieldNumeroFactura.SelectedIndex = -1;
        }

        public void CargarMetodosPago(string[] metodosPago) {
            fieldMetodoPago.Items.Clear();
            fieldMetodoPago.Items.AddRange(metodosPago);
            fieldMetodoPago.SelectedIndex = 0;
        }
    }
}
