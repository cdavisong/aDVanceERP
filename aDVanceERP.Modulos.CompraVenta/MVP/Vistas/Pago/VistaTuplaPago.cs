using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Pago.Plantillas;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Pago; 

public partial class VistaTuplaPago : Form, IVistaTuplaPago {
    private bool _enabled = true;

    public VistaTuplaPago() {
        InitializeComponent();

        NombreVista = nameof(VistaTuplaPago);

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

    public string MetodoPago {
        get => fieldMetodoPago.Text;
        set => fieldMetodoPago.Text = value;
    }

    public string Monto {
        get => fieldMonto.Text;
        set => fieldMonto.Text = value;
    }

    public Color ColorFondoTupla {
        get => layoutVista.BackColor;
        set => layoutVista.BackColor = value;
    }

    public event EventHandler? TuplaSeleccionada;
    public event EventHandler? EditarDatosTupla;
    public event EventHandler? EliminarDatosTupla;
    

    public void Inicializar() {
        // Eventos
        fieldMetodoPago.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldMonto.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };

        btnEliminar.Click += delegate(object? sender, EventArgs e) {
            EliminarDatosTupla?.Invoke(new[] { MetodoPago, Monto }, e);
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
}