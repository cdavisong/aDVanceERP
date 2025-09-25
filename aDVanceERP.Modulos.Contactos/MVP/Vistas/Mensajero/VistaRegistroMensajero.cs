using aDVanceERP.Modulos.Contactos.MVP.Vistas.Mensajero.Plantillas;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Mensajero;

public partial class VistaRegistroMensajero : Form, IVistaRegistroMensajero {
    private bool _modoEdicion;

    public VistaRegistroMensajero() {
        InitializeComponent();

        NombreVista = nameof(VistaRegistroMensajero);

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

    public string NombreMensajero {
        get => fieldNombre.Text;
        set => fieldNombre.Text = value;
    }

    public string TelefonoMovil {
        get => fieldTelefonoMovil.Text;
        set => fieldTelefonoMovil.Text = value;
    }

    public bool ModoEdicion {
        get => _modoEdicion;
        set {
            fieldTelefonoMovil.ReadOnly = value;
            fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
            btnRegistrar.Text = value ? "Actualizar contacto" : "Registrar contacto";
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
        NombreMensajero = string.Empty;
        TelefonoMovil = string.Empty;
        ModoEdicion = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }
}