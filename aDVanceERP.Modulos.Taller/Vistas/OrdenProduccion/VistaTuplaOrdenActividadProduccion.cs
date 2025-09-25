using aDVanceERP.Modulos.Taller.Interfaces;
using System.Globalization;

namespace aDVanceERP.Modulos.Taller.Vistas.OrdenProduccion;

public partial class VistaTuplaOrdenActividadProduccion : Form, IVistaTuplaOrdenActividadProduccion {
    private bool _habilitada = true;

    public VistaTuplaOrdenActividadProduccion() {
        InitializeComponent();

        NombreVista = nameof(VistaTuplaOrdenActividadProduccion);

        Inicializar();
    }

    public string NombreVista {
        get => $"{Name}{Indice}";
        private set => Name = value;
    }

    public bool Habilitada {
        get => _habilitada;
        set {
            fieldCosto.ReadOnly = !value;
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

    public string NombreActividadProduccion {
        get => fieldNombreActividad.Text;
        set {
            fieldNombreActividad.Text = value;
            fieldNombreActividad.Margin = new Padding(1, value?.Length > 20 ? 10 : 1, 1, 1);
        }
    }

    public string CostoActividad {
        get => fieldCosto.Text;
        set => fieldCosto.Text = value;
    }

    public string Cantidad {
        get => fieldCantidad.Text;
        set => fieldCantidad.Text = value;
    }

    public Color ColorFondoTupla {
        get => layoutVista.BackColor;
        set => layoutVista.BackColor = value;
    }

    public event EventHandler? CostoActividadModificado;
    public event EventHandler? TuplaSeleccionada;
    public event EventHandler? EditarDatosTupla;
    public event EventHandler? EliminarDatosTupla;
    

    public void Inicializar() {
        // Eventos
        fieldNombreActividad.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldCosto.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldCosto.LostFocus += delegate { FormatearMontoModificado(); };
        fieldCosto.KeyDown += delegate(object? sender, KeyEventArgs args) {
            if (args.KeyCode != Keys.Enter)
                return;

            FormatearMontoModificado();

            args.SuppressKeyPress = true;
        };
        fieldCantidad.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };

        btnEliminar.Click += delegate(object? sender, EventArgs e) {
            EliminarDatosTupla?.Invoke(new[] { NombreActividadProduccion, Cantidad, CostoActividad }, e);
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
        if (!decimal.TryParse(CostoActividad, NumberStyles.Any, CultureInfo.InvariantCulture, out var monto))
            return;

        CostoActividad = monto.ToString("N2", CultureInfo.InvariantCulture);
        CostoActividadModificado?.Invoke(new[] { NombreActividadProduccion, Cantidad, CostoActividad },
            EventArgs.Empty); // Dispara el evento para notificar que se ha modificado el monto
    }
}