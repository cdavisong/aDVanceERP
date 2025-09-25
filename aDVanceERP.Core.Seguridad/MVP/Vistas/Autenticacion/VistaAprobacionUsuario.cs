using aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion.Plantillas;

namespace aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion;

public partial class VistaAprobacionUsuario : Form, IVistaAprobacionUsuario {
    public VistaAprobacionUsuario() {
        InitializeComponent();

        NombreVista = nameof(VistaAprobacionUsuario);

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

    public event EventHandler? CambiarDeUsuario;


    public void Inicializar() {
        // Eventos            
        btnCambiarUsuario.Click += delegate (object? sender, EventArgs args) {
            CambiarDeUsuario?.Invoke("change-user", args);
            Ocultar();
        };
    }

    public void Mostrar() {
        BringToFront();
        Show();
    }

    public void Restaurar() { }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }
}