
using aDVanceERP.Core.Infraestructura.Globales;
using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaRegistroPagoTransferencia : Form {
        public VistaRegistroPagoTransferencia() {
            InitializeComponent();
            Inicializar();
            InicializarPaisesPrefijos();
        }

        public string NumeroConfirmacion {
            get => $"{fieldPrefijoInternacional.Text} {fieldNumeroConfirmacion.Text}";
            set => fieldNumeroConfirmacion.Text = value;
        }

        public string NumeroTransaccion {
            get => fieldNumeroTransaccion.Text;
            set => fieldNumeroTransaccion.Text = value;
        }

        public decimal MontoPagado {
            get => decimal.TryParse(fieldMonto.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            set => fieldMonto.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public bool EstadoPendiente {
            get => fieldEstadoPendiente.Checked;
            set => fieldEstadoPendiente.Checked = value;
        }

        private void Inicializar() {
            fieldPaises.SelectedIndexChanged += delegate {
                fieldPrefijoInternacional.Text = $"{PrefijosInternacionales.ObtenerPrefijo(fieldPaises.Text)}";
                fieldNumeroConfirmacion.IconLeft = PrefijosInternacionales.ObtenerFlag(fieldPaises.Text);
            };
            btnRegistrarPago.Click += delegate (object? sender, EventArgs args) {
                DialogResult = DialogResult.OK;
                Close();
            };
            btnNoCancel.Click += delegate (object? sender, EventArgs args) {
                DialogResult = DialogResult.Cancel;
                Close();
            };
            btnCerrar.Click += delegate (object? sender, EventArgs args) {
                DialogResult = DialogResult.Cancel;
                Close();
            };
        }

        private void InicializarPaisesPrefijos() {
            fieldPaises.Items.Clear();
            fieldPaises.Items.AddRange(PrefijosInternacionales.ObtenerPaises());
            fieldPaises.StartIndex = 53;
        }
    }
}
