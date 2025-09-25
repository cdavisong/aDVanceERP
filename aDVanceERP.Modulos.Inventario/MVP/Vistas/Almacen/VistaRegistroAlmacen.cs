using aDVanceERP.Modulos.Inventario.MVP.Vistas.Almacen.Plantillas;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Almacen; 

public partial class VistaRegistroAlmacen : Form, IVistaRegistroAlmacen {
    private bool _modoEdicion;

    public VistaRegistroAlmacen() {
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

    public string NombreAlmacen {
        get => fieldNombre.Text;
        set => fieldNombre.Text = value;
    }

    public string Direccion {
        get => fieldDireccion.Text;
        set => fieldDireccion.Text = value;
    }

    public bool AutorizoVenta {
        get => fieldAutorizoVentaProductos.Checked;
        set => fieldAutorizoVentaProductos.Checked = value;
    }

    public string Descripcion {
        get => fieldNotas.Text;
        set => fieldNotas.Text = value;
    }

    public bool ModoEdicion {
        get => _modoEdicion;
        set {
            fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
            btnRegistrar.Text = value ? "Actualizar almacén" : "Registrar almacén";
            _modoEdicion = value;
        }
    }

    public event EventHandler? RegistrarEntidad;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarEntidad;
    

    public void Inicializar() {
        // Eventos
        btnRegistrar.Click += delegate(object? sender, EventArgs args) {
            if (ModoEdicion)
                EditarEntidad?.Invoke(sender, args);
            else
                RegistrarEntidad?.Invoke(sender, args);
        };
        btnSalir.Click += delegate(object? sender, EventArgs args) {
            Close();
        };
    }

    public void Mostrar() {
        BringToFront();
        Show();
    }

    public void Restaurar() {
        NombreAlmacen = string.Empty;
        Direccion = string.Empty;
        AutorizoVenta = false;
        Descripcion = string.Empty;
        ModoEdicion = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }
}