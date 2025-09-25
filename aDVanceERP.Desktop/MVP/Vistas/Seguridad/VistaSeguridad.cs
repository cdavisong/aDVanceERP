using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Core.Utiles;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Desktop.MVP.Vistas.Seguridad;

public partial class VistaSeguridad : Form, IVistaSeguridad {
    public VistaSeguridad() {
        InitializeComponent();

        PanelCentral = new RepoVistaBase(contenedorVistas);
        NombreVista = nameof(VistaSeguridad);

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

    public int AlturaContenedorVistas {
        get => contenedorVistas.Height;
    }

    public int TuplasMaximasContenedor {
        get => AlturaContenedorVistas / VariablesGlobales.AlturaTuplaPredeterminada;
    }

    public RepoVistaBase PanelCentral { get; private set; }

    public void Inicializar() {
                
    }

    public void Mostrar() {
        BringToFront();
        Show();
    }

    public void Restaurar() {
        PanelCentral?.Restaurar("vistaAutenticacionUsuario");

        // Restablecer el usuario autenticado
        UtilesCuentaUsuario.UsuarioAutenticado = null;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        PanelCentral?.CerrarTodos();
    }
}