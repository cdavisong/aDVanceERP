using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.Modulos.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Monedas;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaRegistroPago : Form, IVistaRegistroPago {
        private bool _modoEdicion = false;
        private Core.Modelos.Modulos.Venta.Venta? _venta = null!;
        private decimal _montoPendiente;

        public VistaRegistroPago() {
            InitializeComponent();

            NombreVista = $"{nameof(VistaRegistroPago)}Venta";

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

        public Core.Modelos.Modulos.Venta.Venta? Venta {
            get => _venta;
            set {
                _venta = value;

                if (value == null) {
                    panelDatosVenta.Visible = false;
                    layoutVista.RowStyles[4].Height = 0;
                    layoutVista.RowStyles[5].Height = 0;
                    fieldNumeroFacturaBanner.Text = string.Empty;
                } else {
                    panelDatosVenta.Visible = true;
                    layoutVista.RowStyles[4].Height = 70;
                    layoutVista.RowStyles[5].Height = 20;

                    ActualizarInformacionVentaSeleccionada(value);
                }
            }
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

        public CanalPagoEnum MetodoPago {
            get => (CanalPagoEnum) fieldCanalPago.SelectedIndex;
            set => fieldCanalPago.SelectedItem = value.ObtenerNombreDescripcion();
        }

        public decimal MontoPagado {
            get => decimal.TryParse(fieldMonto.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            set => fieldMonto.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public decimal MontoPendiente { 
            get => _montoPendiente; 
            set {
                _montoPendiente = value; 

                fieldMontoPendiente.Text = $"{RepoMoneda.Instancia.ObtenerMonedaBase().Simbolo} {value.ToString("N2", CultureInfo.InvariantCulture)}";
            } 
        }

        public string NumeroTelefonoRemitente {
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
            fieldNumeroFactura.SelectedIndexChanged += delegate {
                Venta = RepoVenta.Instancia.Buscar(FiltroBusquedaVenta.NumeroFactura, NumeroFacturaVenta).resultadosBusqueda.Select(v => v.entidadBase).FirstOrDefault();

                if (fieldNumeroFactura.SelectedIndex == -1 || Venta == null) {
                    fieldMonto.Text = string.Empty;
                    MontoPendiente = 0;
                    return;
                }

                // Calcular saldo pendiente de la venta
                var estadoPago = RepoVenta.Instancia.VerificarEstadoPagoVenta(Venta.Id);
                fieldMonto.Text = string.Empty;
                MontoPendiente = estadoPago.Saldo;

            };
            fieldCanalPago.SelectedIndexChanged += delegate {
                separador1.Visible = MetodoPago == CanalPagoEnum.TransferenciaBancaria;
                layoutTitulos2.Visible = MetodoPago == CanalPagoEnum.TransferenciaBancaria;
                layoutDatos2.Visible = MetodoPago == CanalPagoEnum.TransferenciaBancaria;
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

        private void ActualizarInformacionVentaSeleccionada(Core.Modelos.Modulos.Venta.Venta venta) {
            var montoAcumulado = RepoPago.Instancia.ObtenerTotalPagadoPorVenta(venta.Id);
            var canalPagoPrincipal = RepoVenta.Instancia.DeterminarMetodoPagoPrincipal(venta.Id) ?? CanalPagoEnum.NA;
            var (colorFondo, colorFuente) = ObtenerColorCanal(canalPagoPrincipal);

            // Banner de venta
            fieldNumeroFacturaBanner.Text = venta.NumeroFacturaTicket;
            fieldMontoAcumuladoBanner.Text = $"{RepoMoneda.Instancia.ObtenerMonedaBase().Simbolo} {montoAcumulado.ToString("N2", CultureInfo.InvariantCulture)}";
            fieldFechaVentaBanner.Text = venta.FechaVenta.ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture);
            fieldCanalPagoPrincipalBanner.Text = canalPagoPrincipal.ObtenerNombreDescripcion().Nombre ?? "N/A";
            fieldCanalPagoPrincipalBanner.ForeColor = colorFuente;
            fieldCanalPagoPrincipalBanner.FillColor = colorFondo;
            fieldOperadorBanner.Text = ContextoSeguridad.UsuarioAutenticado!.Nombre;
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {
            Venta = null;
            FechaPagoCliente = DateTime.Now;
            MetodoPago = CanalPagoEnum.Efectivo;
            NumeroTelefonoRemitente = string.Empty;
            NumeroTransaccion = string.Empty;

            fieldNumeroFactura.SelectedIndex = -1;
            fieldMonto.Text = string.Empty;
            fieldPaises.SelectedIndex = 53;
            fieldNumeroTelefonoRemitente.Text = string.Empty;
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
            fieldCanalPago.Items.Clear();
            fieldCanalPago.Items.AddRange(metodosPago);
            fieldCanalPago.SelectedIndex = 0;
        }

        private(Color colorFondo, Color colorFuente) ObtenerColorCanal(CanalPagoEnum estado) {
            return estado switch {
                CanalPagoEnum.Efectivo => (Color.FromArgb(255, 243, 224), Color.FromArgb(230, 81, 0)),          // Ambar
                CanalPagoEnum.TransferenciaBancaria => (Color.FromArgb(227, 242, 253), Color.FromArgb(21, 101, 196)),   // Azul
                CanalPagoEnum.Mixto => (Color.FromArgb(232, 245, 233), Color.FromArgb(46, 125, 50)),            // Verde
                _ => (Color.FromArgb(240, 240, 240), Color.FromArgb(136, 136, 136))
            };
        }
    }
}
