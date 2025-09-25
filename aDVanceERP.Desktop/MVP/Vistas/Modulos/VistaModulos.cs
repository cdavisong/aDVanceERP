using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Core.Utiles;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.CompraVenta;
using aDVanceERP.Modulos.Contactos;
using aDVanceERP.Modulos.Finanzas;
using aDVanceERP.Modulos.Inventario;
using aDVanceERP.Modulos.Taller;

namespace aDVanceERP.Desktop.MVP.Vistas.Modulos;

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

    public int AlturaContenedorVistas {
        get => panelCentral.Height;
    }

    public int TuplasMaximasContenedor {
        get => AlturaContenedorVistas / VariablesGlobales.AlturaTuplaPredeterminada;
    }



    //public bool BtnModuloAdministracionVisible {
    //    get => btnModuloAdministracion.Visible;
    //    set => btnModuloAdministracion.Visible = value;
    //}

    public FlowLayoutPanel PanelMenuLateral => layoutModulos;

    public RepoVistaBase PanelCentral { get; private set; }

    public string MensajePortada {
        get => fieldTextoBienvenida.Text;
        set {
            fieldTextoBienvenida.Text = value;
            fieldTextoBienvenida.Visible = true;
        }
    }

    public event EventHandler? MostrarVistaInicio;
    public event EventHandler? MostrarVistaEstadisticas;
    public event EventHandler? MostrarMenuContactos;
    public event EventHandler? MostrarMenuFinanzas;
    public event EventHandler? MostrarMenuInventario;
    public event EventHandler? MostrarMenuTaller;
    public event EventHandler? MostrarMenuVentas;
    public event EventHandler? MostrarMenuSeguridad;
    public event EventHandler? CambioModulo;
    

    public void Inicializar() {
        btnInicio.PerformClick();
        btnInicio.Click += delegate {
            PanelCentral.OcultarTodos();
        };

        // Eventos
        btnInicio.Click += delegate (object? sender, EventArgs e) { PresionarBotonModulo(1, e); };
        btnEstadisticas.Click += delegate (object? sender, EventArgs e) { PresionarBotonModulo(2, e); };
        btnModuloContactos.Click += delegate (object? sender, EventArgs e) { PresionarBotonModulo(3, e); };
        btnModuloFinanzas.Click += delegate (object? sender, EventArgs e) { PresionarBotonModulo(4, e); };
        btnModuloInventario.Click += delegate (object? sender, EventArgs e) { PresionarBotonModulo(5, e); };
        btnModuloTaller.Click += delegate (object? sender, EventArgs e) { PresionarBotonModulo(8, e); };
        btnModuloVentas.Click += delegate (object? sender, EventArgs e) { PresionarBotonModulo(6, e); };
        btnModuloSeguridad.Click += delegate (object? sender, EventArgs e) { PresionarBotonModulo(7, e); };
        CambioModulo += delegate { Restaurar(); };
    }

    public void PresionarBotonModulo(object? sender, EventArgs e) {
        var indiceValido = int.TryParse(sender?.ToString() ?? string.Empty, out var indice);

        if (!indiceValido)
            return;

        CambioModulo?.Invoke(sender, e);

        switch (indice) {
            case 1:
                MostrarVistaInicio?.Invoke(btnInicio, e);
                if (!btnInicio.Checked)
                    btnInicio.Checked = true;
                break;
            case 2:
                MostrarVistaEstadisticas?.Invoke(btnEstadisticas, e);
                if (!btnEstadisticas.Checked)
                    btnEstadisticas.Checked = true;
                break;
            case 3:
                MostrarMenuContactos?.Invoke(btnModuloContactos, e);
                if (!btnModuloContactos.Checked)
                    btnModuloContactos.Checked = true;
                break;
            case 4:
                MostrarMenuFinanzas?.Invoke(btnModuloFinanzas, e);
                if (!btnModuloFinanzas.Checked)
                    btnModuloFinanzas.Checked = true;
                break;
            case 5:
                MostrarMenuInventario?.Invoke(btnModuloInventario, e);
                if (!btnModuloInventario.Checked)
                    btnModuloInventario.Checked = true;
                break;
            case 6:
                MostrarMenuVentas?.Invoke(btnModuloVentas, e);
                if (!btnModuloVentas.Checked)
                    btnModuloVentas.Checked = true;
                break;
            case 7:
                MostrarMenuSeguridad?.Invoke(btnModuloSeguridad, e);
                if (!btnModuloSeguridad.Checked)
                    btnModuloSeguridad.Checked = true;
                break;
            case 8:
                MostrarMenuTaller?.Invoke(btnModuloTaller, e);
                if (!btnModuloTaller.Checked)
                    btnModuloTaller.Checked = true;
                break;
        }
    }

    public void Mostrar() {
        btnEstadisticas.Visible = false;//UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false;
        btnModuloContactos.Visible = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false) ||
                                     (UtilesCuentaUsuario.PermisosUsuario?.ContienePermisoParcial(ModuloContactos.Nombre) ?? false);
        btnModuloFinanzas.Visible = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false) ||
                                    (UtilesCuentaUsuario.PermisosUsuario?.ContienePermisoParcial(ModuloFinanzas.Nombre) ?? false);
        btnModuloInventario.Visible = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false) ||
                                      (UtilesCuentaUsuario.PermisosUsuario?.ContienePermisoParcial(ModuloInventario.Nombre) ?? false);
        btnModuloTaller.Visible = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false) ||
                                     (UtilesCuentaUsuario.PermisosUsuario?.ContienePermisoParcial(ModuloTaller.Nombre) ?? false);
        btnModuloVentas.Visible = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false) ||
                                  (UtilesCuentaUsuario.PermisosUsuario?.ContienePermisoParcial(ModuloCompraventa.Nombre) ?? false);
        btnModuloSeguridad.Visible = UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false;

        BringToFront();
        Show();
    }

    public void Restaurar() {
        PanelCentral?.OcultarTodos();
    }

    public void Ocultar() {
        btnInicio.Checked = true;

        Hide();
    }

    public void Cerrar() {
        PanelCentral?.CerrarTodos();
    }
}