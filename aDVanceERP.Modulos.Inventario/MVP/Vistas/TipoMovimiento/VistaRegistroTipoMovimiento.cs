using aDVanceERP.Modulos.Inventario.MVP.Vistas.TipoMovimiento.Plantillas;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.TipoMovimiento; 

public partial class VistaRegistroTipoMovimiento : Form, IVistaRegistroTipoMovimiento {
    private bool _modoEdicion;

    public VistaRegistroTipoMovimiento() {
        InitializeComponent();



        Inicializar();
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

    public string NombreTipoMovimiento {
        get => fieldNombre.Text;
        set => fieldNombre.Text = value;
    }

    public string Efecto {
        get => fieldEfecto.Text;
        set => fieldEfecto.Text = value;
    }

    public bool ModoEdicion {
        get => _modoEdicion;
        set {
            fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
            btnRegistrar.Text = value ? "Actualizar tipo de movimiento" : "Registrar tipo de movimiento";
            _modoEdicion = value;
        }
    }

    public event EventHandler? RegistrarEntidad;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarEntidad;
    

    public void Inicializar() {
        // Eventos
        btnCerrar.Click += delegate(object? sender, EventArgs args) { Close(); };
        btnRegistrar.Click += delegate(object? sender, EventArgs args) {
            if (ModoEdicion)
                EditarEntidad?.Invoke(sender, args);
            else
                RegistrarEntidad?.Invoke(sender, args);
        };
        btnSalir.Click += delegate(object? sender, EventArgs args) { Close(); };
    }

    public void Mostrar() {
        BringToFront();
        ShowDialog();
    }

    public void Restaurar() {
        NombreTipoMovimiento = string.Empty;
        Efecto = string.Empty;
        fieldEfecto.SelectedIndex = -1;
        ModoEdicion = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }
}