using aDVanceERP.Modulos.Taller.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Taller.Vistas.OrdenProduccion;

public partial class VistaTuplaOrdenGastoIndirecto : Form, IVistaTuplaOrdenGastoIndirecto {
    private bool _habilitada = true;

    public VistaTuplaOrdenGastoIndirecto(bool gastoDinamico = false) {
        InitializeComponent();

        NombreVista = nameof(VistaTuplaOrdenGastoIndirecto);

        Inicializar();

        // Habilitar segun el tipo de gasto
        fieldMonto.Enabled = !gastoDinamico;
    }

    public string NombreVista {
        get => $"{Name}{Indice}";
        private set => Name = value;
    }

    public bool Habilitada {
        get => _habilitada;
        set {
            fieldMonto.ReadOnly = !value;
            btnEliminar.Enabled = value;

            _habilitada = value;
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

    public string ConceptoGasto {
        get => fieldConceptoGasto.Text;
        set {
            fieldConceptoGasto.Text = value;
            fieldConceptoGasto.Margin = new Padding(1, value?.Length > 20 ? 10 : 1, 1, 1);
        }
    }

    public string Monto {
        get => fieldMonto.Text;
        set => fieldMonto.Text = value;
    }

    public string Cantidad {
        get => fieldCantidad.Text;
        set => fieldCantidad.Text = value;
    }

    public Color ColorFondoTupla {
        get => layoutVista.BackColor;
        set => layoutVista.BackColor = value;
    }

    public event EventHandler? MontoGastoModificado;
    public event EventHandler? TuplaSeleccionada;
    public event EventHandler? EditarDatosTupla;
    public event EventHandler? EliminarDatosTupla;
    

    public void Inicializar() {
        // Eventos
        fieldConceptoGasto.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldMonto.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldMonto.LostFocus += delegate { FormatearMontoModificado(); };
        fieldMonto.KeyDown += delegate (object? sender, KeyEventArgs args) {
            if (args.KeyCode != Keys.Enter)
                return;

            FormatearMontoModificado();

            args.SuppressKeyPress = true;
        };
        fieldCantidad.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };

        btnEliminar.Click += delegate (object? sender, EventArgs e) {
            EliminarDatosTupla?.Invoke(new[] { ConceptoGasto, Cantidad, Monto }, e);
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
        if (!decimal.TryParse(Monto, NumberStyles.Any, CultureInfo.InvariantCulture, out var monto))
            return;

        Monto = monto.ToString("N2", CultureInfo.InvariantCulture);
        MontoGastoModificado?.Invoke(new[] { ConceptoGasto, Cantidad, Monto },
            EventArgs.Empty); // Dispara el evento para notificar que se ha modificado el monto
    }
}