using aDVanceERP.Core.Seguridad.MVP.Vistas.Permiso.Plantillas;

namespace aDVanceERP.Core.Seguridad.MVP.Vistas.Permiso; 

public partial class VistaTuplaPermiso : Form, IVistaTuplaPermiso {
    private string _idPermiso = string.Empty;

    public VistaTuplaPermiso() {
        InitializeComponent();

        NombreVista = nameof(VistaTuplaPermiso);

        Inicializar();
    }

    public string NombreVista {
        get => $"{Name}{Id}";
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

    public string? Id {
        get => _idPermiso;
        set => _idPermiso = value ?? string.Empty;
    }

    public string? NombrePermiso {
        get => fieldNombrePermiso.Text;
        set => fieldNombrePermiso.Text = value;
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
        fieldNombrePermiso.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };

        btnEliminar.Click += delegate(object? sender, EventArgs e) {
            EliminarDatosTupla?.Invoke(new[] { Id ?? string.Empty, NombrePermiso ?? string.Empty }, e);
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