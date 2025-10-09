using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;

using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Vistas.Menu;

public partial class VistaMenuInventario : Form, IVistaMenuInventario {
    public VistaMenuInventario() {
        InitializeComponent();

        NombreVista = nameof(VistaMenuInventario);

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
        btnProductos.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionProductos", string.Empty); };
        btnMovimientos.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionMovimientos", string.Empty); };
        btnAlmacenes.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionAlmacenes", string.Empty); };
    }

    public void SeleccionarVistaInicial() {
        if (btnProductos.Visible)
            btnProductos.PerformClick();
        else if (btnMovimientos.Visible)
            btnMovimientos.PerformClick();
        else if (btnAlmacenes.Visible)
            btnAlmacenes.PerformClick();
    }

    public void Mostrar() {
        VerificarPermisos();
        BringToFront();
        Show();
    }

    public void Restaurar() {
        btnProductos.Checked = false;
        btnAlmacenes.Checked = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    } 

    private void VerificarPermisos() {
        btnProductos.Visible = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoParcial("MOD_INVENTARIO_PRODUCTOS")
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
        btnMovimientos.Visible = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                                 || ContextoSeguridad.PermisosUsuario.ContienePermisoParcial("MOD_INVENTARIO_MOVIMIENTOS")
                                 || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
        btnAlmacenes.Visible = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoParcial("MOD_INVENTARIO_ALMACENES")
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
    }
}