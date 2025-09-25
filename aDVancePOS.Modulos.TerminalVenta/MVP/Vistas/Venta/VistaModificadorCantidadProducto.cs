using aDVancePOS.Modulos.TerminalVenta.MVP.Vistas.Venta.Plantillas;

using Guna.UI2.WinForms;
using System.Globalization;

namespace aDVancePOS.Modulos.TerminalVenta.MVP.Vistas.Venta {
    public partial class VistaModificadorCantidadProducto : Form, IVistaModificadorCantidadProducto {
        public VistaModificadorCantidadProducto() {
            InitializeComponent();
            Inicializar();
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

        public decimal CantidadProducto {
            get => decimal.TryParse(fieldCantidad.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var cantidad) ? cantidad : 1;
            set => fieldCantidad.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public event EventHandler? Salir;

        public void Inicializar() {
            // Eventos
            fieldCantidad.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode is >= Keys.A and <= Keys.Z) {
                    args.SuppressKeyPress = true;

                    return;
                }

                switch (args.KeyCode) {
                    case Keys.Enter or Keys.Escape:
                        if (args.KeyCode == Keys.Escape)
                            CantidadProducto = 1;

                        args.SuppressKeyPress = true;

                        Salir?.Invoke(sender, args);

                        Cerrar();
                        break;
                    case Keys.Back or Keys.Delete:
                        AdicionarCaracterTeclado(btnEliminar);

                        args.SuppressKeyPress = true;
                        break;
                }
            };
            btnNumero0.Click += delegate {
                AdicionarCaracterTeclado(btnNumero0);
            };
            btnNumero1.Click += delegate {
                AdicionarCaracterTeclado(btnNumero1);
            };
            btnNumero2.Click += delegate {
                AdicionarCaracterTeclado(btnNumero2);
            };
            btnNumero3.Click += delegate {
                AdicionarCaracterTeclado(btnNumero3);
            };
            btnNumero4.Click += delegate {
                AdicionarCaracterTeclado(btnNumero4);
            };
            btnNumero5.Click += delegate {
                AdicionarCaracterTeclado(btnNumero5);
            };
            btnNumero6.Click += delegate {
                AdicionarCaracterTeclado(btnNumero6);
            };
            btnNumero7.Click += delegate {
                AdicionarCaracterTeclado(btnNumero7);
            };
            btnNumero8.Click += delegate {
                AdicionarCaracterTeclado(btnNumero8);
            };
            btnNumero9.Click += delegate {
                AdicionarCaracterTeclado(btnNumero9);
            };
            btnEliminar.Click += delegate {
                AdicionarCaracterTeclado(btnEliminar);
            };
            btnEscape.Click += delegate (object? sender, EventArgs args) {
                Salir?.Invoke(sender, args);

                Cerrar();
            };
            btnIntro.Click += delegate (object? sender, EventArgs args) {
                Salir?.Invoke(sender, args);

                Cerrar();
            };

            fieldCantidad.Focus();
        }

        private void AdicionarCaracterTeclado(Guna2Button btnCaracter) {
            var textoCantidad = fieldCantidad.Text;

            if (textoCantidad.Length == 0)
                textoCantidad = "1";

            if (textoCantidad.Where(char.IsDigit).Count() <= 11)
                fieldCantidad.Text += btnCaracter.Text;

            switch (btnCaracter.Name) {
                case "btnEliminar":
                    fieldCantidad.Text = textoCantidad[..^1];
                    break;
            }

            // Enfocar el campo cantidad
            fieldCantidad.Focus();

            // Colocar el cursor al final
            fieldCantidad.SelectionStart = fieldCantidad.Text.Length;
            fieldCantidad.SelectionLength = 0; // Sin texto seleccionado
        }

        public void Mostrar() {
            fieldCantidad.Focus();

            Habilitada = true;
            BringToFront();
            ShowDialog();
        }

        public void Restaurar() {
            CantidadProducto = 1;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }
    }
}
