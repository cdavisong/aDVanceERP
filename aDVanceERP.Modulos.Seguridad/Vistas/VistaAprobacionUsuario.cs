using aDVanceERP.Core.Eventos;
using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Vistas;

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

    public string NombreUsuario {
        get => fieldTitulo.Text.Replace("¡Bienvenido ", "").Replace("!", "");
        set => fieldTitulo.Text = $"¡Bienvenido {value}!";
    }

    public void Inicializar() {
        // Eventos            
        btnCambiarUsuario.Click += delegate (object? sender, EventArgs args) {
            AgregadorEventos.Publicar("MostrarVistaAutenticacionUsuario", string.Empty);
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