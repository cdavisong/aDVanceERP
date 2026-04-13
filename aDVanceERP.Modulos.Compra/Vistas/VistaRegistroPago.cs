using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Repositorios.Modulos.Compra;
using aDVanceERP.Modulos.Compra.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Compra.Vistas {
    public partial class VistaRegistroPago : Form, IVistaRegistroPago {
        private bool _modoEdicion = false;

        public VistaRegistroPago() {
            InitializeComponent();

            NombreVista = $"{nameof(VistaRegistroPago)}Compra";

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

        public string CodigoCompra {
            get => fieldCodigoCompra.SelectedItem?.ToString() ?? string.Empty;
            set {
                if (fieldCodigoCompra.Items.Contains(value)) {
                    fieldCodigoCompra.SelectedItem = value;
                } else {
                    fieldCodigoCompra.SelectedIndex = -1;
                }
            }
        }

        public DateTime FechaPagoProveedor {
            get => fieldFechaPago.Value;
            set => fieldFechaPago.Value = value;
        }

        public MetodoPagoEnum MetodoPago {
            get => (MetodoPagoEnum) fieldMetodoPago.SelectedIndex;
            set => fieldMetodoPago.SelectedItem = value.ObtenerNombreDescripcion();
        }

        public decimal MontoPagado {
            get => decimal.TryParse(fieldMonto.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            set => fieldMonto.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public string NumeroTelefonoConfirmacion {
            get => $"{fieldPrefijoInternacional.Text} {fieldNumeroTelefonoRemitente.Text}";
            set => fieldNumeroTelefonoRemitente.Text = value;
        }

        public string NumeroTransaccion {
            get => fieldNumeroTransaccion.Text;
            set => fieldNumeroTransaccion.Text = value;
        }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            fieldCodigoCompra.SelectedIndexChanged += delegate {
                var compra = RepoCompra.Instancia.Buscar(FiltroBusquedaCompra.Codigo, CodigoCompra).resultadosBusqueda.Select(c => c.entidadBase).FirstOrDefault();

                if (fieldCodigoCompra.SelectedIndex == -1 || compra == null) {
                    fieldMonto.Text = string.Empty;
                    return;
                }

                MontoPagado = compra.TotalCompra;
            };
            fieldMetodoPago.SelectedIndexChanged += delegate {
                separador1.Visible = MetodoPago == MetodoPagoEnum.TransferenciaBancaria;
                layoutTitulos2.Visible = MetodoPago == MetodoPagoEnum.TransferenciaBancaria;
                layoutDatos2.Visible = MetodoPago == MetodoPagoEnum.TransferenciaBancaria;
            };
            fieldPaises.SelectedIndexChanged += delegate {
                fieldPrefijoInternacional.Text = $"{PrefijosInternacionales.ObtenerPrefijo(fieldPaises.Text)}";
                fieldNumeroTelefonoRemitente.IconLeft = PrefijosInternacionales.ObtenerFlag(fieldPaises.Text);
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
            FechaPagoProveedor = DateTime.Now;
            MetodoPago = MetodoPagoEnum.Efectivo;
            NumeroTelefonoConfirmacion = string.Empty;
            NumeroTransaccion = string.Empty;

            fieldCodigoCompra.SelectedIndex = -1;
            fieldMonto.Text = string.Empty;
            fieldPaises.SelectedIndex = 53;
            fieldNumeroTelefonoRemitente.Text = string.Empty;
        }

        public void Cerrar() {
            Dispose();
        }

        public void CargarSolicitudesComprasPendientes(string[] numerosSolicitudesPendientes) {
            fieldCodigoCompra.Items.Clear();
            fieldCodigoCompra.Items.AddRange(numerosSolicitudesPendientes);
            fieldCodigoCompra.SelectedIndex = -1;
        }

        public void CargarMetodosPago(string[] metodosPago) {
            fieldMetodoPago.Items.Clear();
            fieldMetodoPago.Items.AddRange(metodosPago);
            fieldMetodoPago.SelectedIndex = 0;
        }
    }
}
