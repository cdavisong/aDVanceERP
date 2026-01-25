using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Vistas;

public partial class VistaMenuVenta : Form, IVistaMenuVenta {
    public VistaMenuVenta() {
        InitializeComponent();

        NombreVista = nameof(VistaMenuVenta);

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
        btnVentas.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionVentas", string.Empty); };
        btnEnvios.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionEnvios", string.Empty); };
        btnMaestros.Click += delegate { AgregadorEventos.Publicar("MostrarVistaMenuMaestrosVenta", string.Empty); };
    }

    public void SeleccionarVistaInicial() {
        if (btnVentas.Visible)
            btnVentas.PerformClick();
        else if (btnEnvios.Visible)
            btnEnvios.PerformClick();
    }

    public void Mostrar() {
        VerificarPermisos();
        BringToFront();
        Show();
    }

    public void Restaurar() {
        btnVentas.Checked = false;
        btnEnvios.Checked = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    } 

    private void VerificarPermisos() {
        btnVentas.Visible = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoParcial("MOD_VENTA_VENTAS")
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_VENTA_TODOS");
        btnEnvios.Visible = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                                 || ContextoSeguridad.PermisosUsuario.ContienePermisoParcial("MOD_VENTA_ENVIOS")
                                 || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_VENTA_TODOS");
    }
}