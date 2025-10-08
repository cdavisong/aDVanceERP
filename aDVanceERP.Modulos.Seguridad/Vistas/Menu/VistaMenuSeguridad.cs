using aDVanceERP.Core.Eventos;
using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Vistas.Menu;

public partial class VistaMenuSeguridad : Form, IVistaMenuSeguridad {
    public VistaMenuSeguridad() {
        InitializeComponent();

        NombreVista = nameof(VistaMenuSeguridad);

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

    public void Inicializar() {
        // Eventos
        btnUsuarios.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionCuentasUsuarios", string.Empty); };
        btnRolesUsuarios.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionRolesUsuarios", string.Empty); };
    }

    public void SeleccionarVistaInicial() {
        if (btnUsuarios.Visible)
            btnUsuarios.PerformClick();
        else if (btnRolesUsuarios.Visible)
            btnRolesUsuarios.PerformClick();
    }

    public void Mostrar() {
        BringToFront();
        Show();
    }

    public void Restaurar() {
        btnUsuarios.Checked = false;
        btnRolesUsuarios.Checked = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }
}