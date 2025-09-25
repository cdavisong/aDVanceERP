using aDVanceERP.Modulos.Contactos.MVP.Vistas.Cliente.Plantillas;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Cliente; 

public partial class VistaRegistroCliente : Form, IVistaRegistroCliente {
    private bool _modoEdicion;

    public VistaRegistroCliente() {
        InitializeComponent();

        NombreVista = nameof(VistaRegistroCliente);

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
    
    public string? RazonSocial {
        get => fieldRazonSocial.Text;
        set => fieldRazonSocial.Text = value;
    }

    public string? Numero {
        get => fieldNumero.Text;
        set => fieldNumero.Text = value;
    }

    public string TelefonoMovil {
        get => fieldTelefonoMovil.Text;
        set => fieldTelefonoMovil.Text = value;
    }

    public string Direccion {
        get => fieldDireccion.Text;
        set {
            fieldDireccion.Text = value;
            fieldDireccion.Margin = new Padding(1, value?.Length > 43 ? 10 : 1, 1, 1);
        }
    }

    public bool ModoEdicion {
        get => _modoEdicion;
        set {
            fieldTelefonoMovil.ReadOnly = value;
            fieldDireccion.ReadOnly = value;
            fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
            btnRegistrar.Text = value ? "Actualizar cliente" : "Crear cliente";
            _modoEdicion = value;
        }
    }

    public event EventHandler? RegistrarEntidad;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarEntidad;
    
    public void Inicializar() {
        // Eventos
        btnCerrar.Click += delegate(object? sender, EventArgs args) {
            Close();
        };
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
        ShowDialog();
    }

    public void Restaurar() {
        Numero = string.Empty;
        RazonSocial = string.Empty;
        TelefonoMovil = string.Empty;
        Direccion = string.Empty;
        ModoEdicion = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }
}