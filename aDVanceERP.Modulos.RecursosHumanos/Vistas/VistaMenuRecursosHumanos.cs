using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Vistas;

public partial class VistaMenuRecursosHumanos : Form, IVistaMenuRecursosHumanos {
    public VistaMenuRecursosHumanos() {
        InitializeComponent();

        NombreVista = nameof(VistaMenuRecursosHumanos);

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
        btnEmpleados.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionEmpleados", string.Empty); };
        btnProveedores.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionProveedores", string.Empty); };        
        btnClientes.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionClientes", string.Empty); };
        btnMensajeros.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionMensajeros", string.Empty); };
        btnPersonas.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionPersonas", string.Empty); };
    }

    public void SeleccionarVistaInicial() {
        if (btnEmpleados.Visible)
            btnEmpleados.PerformClick();
        else if (btnProveedores.Visible)
            btnProveedores.PerformClick();
        else if (btnClientes.Visible)
            btnClientes.PerformClick();
        else if (btnMensajeros.Visible)
            btnMensajeros.PerformClick();
        else if (btnPersonas.Visible)
            btnPersonas.PerformClick();
    }
    
    public void Mostrar() {
        VerificarPermisos();
        BringToFront();
        Show();
    }

    public void Restaurar() {
        btnEmpleados.Checked = false;
        btnProveedores.Checked = false;        
        btnClientes.Checked = false;
        btnMensajeros.Checked = false;
        btnPersonas.Checked = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }

    private void VerificarPermisos() {
        btnEmpleados.Visible = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                                 || ContextoSeguridad.PermisosUsuario.ContienePermisoParcial("MOD_RRHH_EMPLEADOS")
                                 || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_TODOS");
        btnProveedores.Visible = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                                 || ContextoSeguridad.PermisosUsuario.ContienePermisoParcial("MOD_RRHH_PROVEEDORES")
                                 || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_TODOS");
        btnClientes.Visible = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoParcial("MOD_RRHH_CLIENTES")
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_TODOS");
        btnMensajeros.Visible = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                                 || ContextoSeguridad.PermisosUsuario.ContienePermisoParcial("MOD_RRHH_MENSAJEROS")
                                 || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_TODOS");
        btnPersonas.Visible = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoParcial("MOD_RRHH_CONTACTOS")
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_TODOS");
    }
}