using aDVanceERP.Core.MVP.Modelos.Repositorios.Plantillas;
using aDVanceERP.Core.MVP.Modelos.Repositorios;
using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Core.Utiles;
using aDVancePOS.Desktop.MVP.Vistas.ContenedorSeguridad.Plantillas;

namespace aDVancePOS.Desktop.MVP.Vistas.ContenedorSeguridad;

public partial class VistaContenedorSeguridad : Form, IVistaContenedorSeguridad {
    public VistaContenedorSeguridad() {
        InitializeComponent();
        Inicializar();
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

    public IRepositorioVista? Vistas { get; private set; }

    public event EventHandler? Salir;

    public void Inicializar() {
        // Propiedades locales
        Vistas = new RepositorioVistaBase(contenedorVistas);
    }

    public void Mostrar() {
        BringToFront();
        Show();
    }

    public void Restaurar() {
        Vistas?.Restaurar("vistaAutenticacionUsuario");

        // Restablecer el usuario autenticado
        UtilesCuentaUsuario.UsuarioAutenticado = null;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Vistas?.Cerrar();
    }
}