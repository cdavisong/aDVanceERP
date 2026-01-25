using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Desktop.Properties;
using Guna.UI2.WinForms;

namespace aDVanceERP.Desktop.Vistas;

public partial class VistaPrincipal : Form, IVistaPrincipal {
    public VistaPrincipal() {
        InitializeComponent();

        NombreVista = nameof(VistaPrincipal);
        Titulo = Resources.TituloAplicacion.Replace("[version]", $"{Program.Version}-beta");
        BarraTitulo = new RepoVistaBase(barraTitulo);
        PanelCentral = new RepoVistaBase(panelCentral);
        BarraEstado = new RepoVistaBase(barraEstado);

        Inicializar();
    }

    public string Titulo {
        get => fieldTitulo.Text;
        private set => fieldTitulo.Text = value;
    }

    #region Barra de título

    public RepoVistaBase BarraTitulo { get; private set; }
    public FlowLayoutPanel BotonesTitulo => layoutBotones;

    #endregion

    public RepoVistaBase PanelCentral { get; private set; }
    public RepoVistaBase BarraEstado { get; private set; }

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

    public event EventHandler? VerMenuUsuario;
    public event EventHandler? VerMensajes;
    public event EventHandler? VerNotificaciones;

    public void Inicializar() {
        FormClosing += delegate {
            Cerrar();
        };

        // Eventos        
        btnMinimizar.Click += (sender, e) => Ocultar();
        btnCerrar.Click += (sender, e) => Close();
    }

    public void ModificarVisibilidadBotonesBarraTitulo(bool visible) {
        BotonesTitulo.Controls.OfType<Guna2Button>().ToList().ForEach(b => b.Visible = visible);
    }

    public void Mostrar() {
        BringToFront();
        Show();

        WindowState = FormWindowState.Maximized;
    }

    public void Ocultar() {
        WindowState = FormWindowState.Minimized;
    }

    public void Restaurar() {
        //...
    }

    public void Cerrar() {
        BarraTitulo?.CerrarTodos();
        PanelCentral?.CerrarTodos();
        BarraEstado?.CerrarTodos();
    }
}