using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Menu.Plantillas;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Menu;

public partial class VistaMenuContacto : Form, IVistaMenuContacto {
    public VistaMenuContacto() {
        InitializeComponent();

        NombreVista = nameof(VistaMenuContacto);

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

    public event EventHandler? VerProveedores;
    public event EventHandler? VerMensajeros;
    public event EventHandler? VerClientes;
    public event EventHandler? VerContactos;
    public event EventHandler? CambioMenu;
    

    public void Inicializar() {
        // Eventos
        btnProveedores.Click += delegate (object? sender, EventArgs e) { PresionarBotonSeleccion(1, e); };
        btnMensajeros.Click += delegate (object? sender, EventArgs e) { PresionarBotonSeleccion(2, e); };
        btnClientes.Click += delegate (object? sender, EventArgs e) { PresionarBotonSeleccion(3, e); };
        btnContactos.Click += delegate (object? sender, EventArgs e) { PresionarBotonSeleccion(4, e); };
    }

    public void MostrarCaracteristicaInicial() {
        if (btnProveedores.Visible)
            btnProveedores.PerformClick();
        else if (btnMensajeros.Visible)
            btnMensajeros.PerformClick();
        else if (btnClientes.Visible)
            btnClientes.PerformClick();
        else if (btnContactos.Visible)
            btnContactos.PerformClick();
    }

    public void PresionarBotonSeleccion(object? sender, EventArgs e) {
        var indiceValido = int.TryParse(sender?.ToString() ?? string.Empty, out var indice);

        if (!indiceValido)
            return;

        CambioMenu?.Invoke(sender, e);

        switch (indice) {
            case 1:
                VerProveedores?.Invoke(btnProveedores, e);
                if (!btnProveedores.Checked)
                    btnProveedores.Checked = true;
                break;
            case 2:
                VerMensajeros?.Invoke(btnMensajeros, e);
                if (!btnMensajeros.Checked)
                    btnMensajeros.Checked = true;
                break;
            case 3:
                VerClientes?.Invoke(btnClientes, e);
                if (!btnClientes.Checked)
                    btnClientes.Checked = true;
                break;
            case 4:
                VerContactos?.Invoke(btnContactos, e);
                if (!btnContactos.Checked)
                    btnContactos.Checked = true;
                break;
        }
    }

    public void Mostrar() {
        VerificarPermisos();
        BringToFront();
        Show();
    }

    public void Restaurar() {
        btnProveedores.Checked = false;
        btnMensajeros.Checked = false;
        btnClientes.Checked = false;
        btnContactos.Checked = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }

    private void VerificarPermisos() {
        btnProveedores.Visible = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                                 || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoParcial("MOD_CONTACTO_PROVEEDORES")
                                 || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_CONTACTO_TODOS");
        btnMensajeros.Visible = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                                 || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoParcial("MOD_CONTACTO_MENSAJEROS")
                                 || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_CONTACTO_TODOS");
        btnClientes.Visible = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                              || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoParcial("MOD_CONTACTO_CLIENTES")
                              || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_CONTACTO_TODOS");
        btnContactos.Visible = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                               || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoParcial("MOD_CONTACTO_CONTACTOS")
                               || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_CONTACTO_TODOS");
    }
}