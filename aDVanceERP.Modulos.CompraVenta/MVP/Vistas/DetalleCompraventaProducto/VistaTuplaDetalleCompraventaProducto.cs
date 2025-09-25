using System.Globalization;

using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.DetalleCompraventaProducto.Plantillas;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.DetalleCompraventaProducto; 

public partial class VistaTuplaDetalleCompraventaProducto : Form, IVistaTuplaDetalleCompraventaProducto {
    private bool _enabled = true;

    public VistaTuplaDetalleCompraventaProducto() {
        InitializeComponent();

        NombreVista = nameof(VistaTuplaDetalleCompraventaProducto);

        Inicializar();
    }

    public string NombreVista {
        get => $"{Name}{Indice}";
        private set => Name = value;
    }

    public bool Habilitada {
        get => _enabled;
        set {
            _enabled = value;
            fieldPrecioCompraventa.ReadOnly = !value;
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

    public int Indice { get; set; }

    public string IdProducto { get; set; }

    public string NombreProducto {
        get => fieldNombreProducto.Text;
        set {
            fieldNombreProducto.Text = value;
            fieldNombreProducto.Margin = new Padding(1, value?.Length > 20 ? 10 : 1, 1, 1);
        }
    }

    public string PrecioCompraventaFinal {
        get => fieldPrecioCompraventa.Text;
        set => fieldPrecioCompraventa.Text = value;
    }

    public string Cantidad {
        get => fieldCantidad.Text;
        set => fieldCantidad.Text = value;
    }

    public Color ColorFondoTupla {
        get => layoutVista.BackColor;
        set => layoutVista.BackColor = value;
    }

    public event EventHandler? PrecioCompraventaModificado;
    public event EventHandler? TuplaSeleccionada;
    public event EventHandler? EditarDatosTupla;
    public event EventHandler? EliminarDatosTupla;
    

    public void Inicializar() {
        // Eventos
        fieldNombreProducto.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldPrecioCompraventa.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldPrecioCompraventa.LostFocus += delegate { FormatearMontoModificado(); };
        fieldPrecioCompraventa.KeyDown += delegate(object? sender, KeyEventArgs args) {
            if (args.KeyCode != Keys.Enter)
                return;

            FormatearMontoModificado();

            args.SuppressKeyPress = true;
        };
        fieldCantidad.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };

        btnEliminar.Click += delegate(object? sender, EventArgs e) {
            EliminarDatosTupla?.Invoke(new[] { IdProducto, NombreProducto, PrecioCompraventaFinal, Cantidad }, e);
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
        if (!decimal.TryParse(PrecioCompraventaFinal, NumberStyles.Any, CultureInfo.InvariantCulture, out var monto))
            return;

        PrecioCompraventaFinal = monto.ToString("N2", CultureInfo.InvariantCulture);
        PrecioCompraventaModificado?.Invoke(this,
            EventArgs.Empty); // Dispara el evento para notificar que se ha modificado el monto
    }
}