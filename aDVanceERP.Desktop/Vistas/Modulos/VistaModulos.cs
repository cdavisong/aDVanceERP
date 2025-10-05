using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Utiles;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Desktop.Vistas.Modulos;

public partial class VistaModulos : Form, IVistaModulos {
    public VistaModulos() {
        InitializeComponent();

        NombreVista = nameof(VistaModulos);
        PanelCentral = new RepoVistaBase(panelCentral);

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

    public FlowLayoutPanel PanelMenuLateral => layoutModulos;

    public RepoVistaBase PanelCentral { get; private set; }

    public string MensajePortada {
        get => fieldTextoBienvenida.Text;
        set {
            fieldTextoBienvenida.Text = value;
            fieldTextoBienvenida.Visible = true;
        }
    }

    public void Inicializar() {
        // Estado inicial
        btnInicio.PerformClick();

        // Eventos
        btnInicio.Click += delegate { PanelCentral.OcultarTodos(); };
        btnInicio.Click += (s, e) => {
            AgregadorEventos.Publicar("EventoCambioMenu", string.Empty);
            AgregadorEventos.Publicar("EventoCambioModulo", string.Empty);
        };

        AgregadorEventos.Suscribir("EventoCambioModulo", OnEventoCambioModulo);
    }

    private void OnEventoCambioModulo(string obj) {
        Restaurar();
    }

    public void Mostrar() {
        BringToFront();
        Show();
    }

    public void Restaurar() {
        PanelCentral.OcultarTodos();
    }

    public void Ocultar() {
        btnInicio.Checked = true;

        Hide();
    }

    public void Cerrar() {
        AgregadorEventos.Desuscribir("EventoCambioModulo", OnEventoCambioModulo);

        PanelCentral?.CerrarTodos();
    }
}