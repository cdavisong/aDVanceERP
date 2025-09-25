using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Menu.Plantillas;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Menu;

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

    public event EventHandler? VerProductos;
    public event EventHandler? VerMovimientos;
    public event EventHandler? VerAlmacenes;
    public event EventHandler? CambioMenu;
    

    public void Inicializar() {
        // Eventos
        btnProductos.Click += delegate (object? sender, EventArgs e) { PresionarBotonSeleccion(1, e); };
        btnMovimientos.Click += delegate (object? sender, EventArgs e) { PresionarBotonSeleccion(2, e); };
        btnAlmacenes.Click += delegate (object? sender, EventArgs e) { PresionarBotonSeleccion(3, e); };
    }

    public void MostrarCaracteristicaInicial() {
        if (btnProductos.Visible)
            btnProductos.PerformClick();
        else if (btnMovimientos.Visible)
            btnMovimientos.PerformClick();
        else if (btnAlmacenes.Visible)
            btnAlmacenes.PerformClick();
    }

    public void PresionarBotonSeleccion(object? sender, EventArgs e) {
        var indiceValido = int.TryParse(sender?.ToString() ?? string.Empty, out var indice);

        if (!indiceValido)
            return;

        CambioMenu?.Invoke(sender, e);

        switch (indice) {
            case 1:
                VerProductos?.Invoke(btnProductos, e);
                if (!btnProductos.Checked)
                    btnProductos.Checked = true;
                break;
            case 2:
                VerMovimientos?.Invoke(btnMovimientos, e);
                if (!btnMovimientos.Checked)
                    btnMovimientos.Checked = true;
                break;
            case 3:
                VerAlmacenes?.Invoke(btnAlmacenes, e);
                if (!btnAlmacenes.Checked)
                    btnAlmacenes.Checked = true;
                break;
        }
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
        btnProductos.Visible = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                               || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoParcial("MOD_INVENTARIO_PRODUCTOS")
                               || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
        btnMovimientos.Visible = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                                 || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoParcial("MOD_INVENTARIO_MOVIMIENTOS")
                                 || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
        btnAlmacenes.Visible = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                               || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoParcial("MOD_INVENTARIO_ALMACENES")
                               || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
    }
}