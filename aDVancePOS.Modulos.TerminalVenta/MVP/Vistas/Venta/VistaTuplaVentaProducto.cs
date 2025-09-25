using System.Globalization;

using aDVancePOS.Modulos.TerminalVenta.MVP.Vistas.Venta.Plantillas;

namespace aDVancePOS.Modulos.TerminalVenta.MVP.Vistas.Venta;

public partial class VistaTuplaVentaProducto : Form, IVistaTuplaVentaProducto {
    private bool _enabled = true;

    public VistaTuplaVentaProducto() {
        InitializeComponent();
        Inicializar();
    }

    public bool Habilitada {
        get => _enabled;
        set {
            _enabled = value;
            fieldPrecio.ReadOnly = !value;
            btnEliminar.Enabled = value;
        }
    }

    public Point Coordenadas {
        get => Location;
        set => Location = value;
    }

    public Size Dimensiones {
        get => Size;
        set => Size = value;
    }

    public string IdProducto {
        get => fieldId.Text;
        set => fieldId.Text = value;
    }

    public string NombreProducto {
        get => fieldNombreProducto.Text;
        set {
            fieldNombreProducto.Text = value;
            fieldNombreProducto.Margin = new Padding(1, value?.Length > 30 ? 10 : 1, 1, 1);
        }
    }

    public string PrecioVentaFinal {
        get => fieldPrecio.Text;
        set => fieldPrecio.Text = value;
    }

    public string Cantidad {
        get => fieldCantidad.Text;
        set => fieldCantidad.Text = value;
    }

    public string Subtotal {
        get => fieldSubtotal.Text;
        set => fieldSubtotal.Text = value;
    }

    public Color ColorFondoTupla {
        get => layoutVista.BackColor;
        set => layoutVista.BackColor = value;
    }

    public event EventHandler? PrecioVentaModificado;
    public event EventHandler? TuplaSeleccionada;
    public event EventHandler? EditarDatosTupla;
    public event EventHandler? EliminarDatosTupla;
    public event EventHandler? Salir;

    public void Inicializar() {
        // Eventos
        fieldNombreProducto.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldPrecio.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldPrecio.LostFocus += delegate { FormatearMontoModificado(); };
        fieldPrecio.KeyDown += delegate (object? sender, KeyEventArgs args) {
            if (args.KeyCode != Keys.Enter)
                return;

            FormatearMontoModificado();

            args.SuppressKeyPress = true;
        };
        fieldCantidad.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };

        btnEliminar.Click += delegate (object? sender, EventArgs e) {
            EliminarDatosTupla?.Invoke(new[] { IdProducto, NombreProducto, PrecioVentaFinal, Cantidad }, e);
        };
    }

    public void Mostrar() {
        BringToFront();
        Show();
    }

    public void Restaurar() {
        ColorFondoTupla = BackColor;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }

    private void FormatearMontoModificado() {
        if (!decimal.TryParse(PrecioVentaFinal, NumberStyles.Any, CultureInfo.InvariantCulture, out var monto))
            return;

        PrecioVentaFinal = monto.ToString("N2", CultureInfo.InvariantCulture);
        PrecioVentaModificado?.Invoke(this,
            EventArgs.Empty); // Dispara el evento para notificar que se ha modificado el monto
    }
}

