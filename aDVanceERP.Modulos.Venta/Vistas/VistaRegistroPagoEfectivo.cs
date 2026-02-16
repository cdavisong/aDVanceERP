using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaRegistroPagoEfectivo : Form {
        public VistaRegistroPagoEfectivo() {
            InitializeComponent();
            Inicializar();
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
    }
}
