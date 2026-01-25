using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Vistas;

public partial class VistaMenuMaestros : Form, IVistaMenuMaestros {
    public VistaMenuMaestros() {
        InitializeComponent();

        NombreVista = $"{nameof(VistaMenuMaestros)}Venta";

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
        btnClientes.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionClientes", string.Empty); };
        btnMensajeros.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionMensajeros", string.Empty); };
        btnAtras.Click += delegate { AgregadorEventos.Publicar("MostrarVistaMenuVenta", string.Empty); };
    }

    public void SeleccionarVistaInicial() {
        if (btnClientes.Visible)
            btnClientes.PerformClick();
        else if (btnMensajeros.Visible)
            btnMensajeros.PerformClick();
    }

    public void Mostrar() {
        VerificarPermisos();
        BringToFront();
        Show();
    }

    public void Restaurar() {
        btnClientes.Checked = false;
        btnMensajeros.Checked = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    } 

    private void VerificarPermisos() {
        btnClientes.Visible = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoParcial("MOD_RRHH_CLIENTES")
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_TODOS");
        btnMensajeros.Visible = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                                 || ContextoSeguridad.PermisosUsuario.ContienePermisoParcial("MOD_RRHH_MENSAJEROS")
                                 || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_TODOS");
    }
}