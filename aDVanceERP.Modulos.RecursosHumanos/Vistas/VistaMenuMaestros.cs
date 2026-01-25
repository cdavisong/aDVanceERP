using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Vistas;

public partial class VistaMenuMaestros : Form, IVistaMenuMaestros {
    public VistaMenuMaestros() {
        InitializeComponent();

        NombreVista = $"{nameof(VistaMenuMaestros)}RecursosHumanos";

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
        btnPersonas.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionPersonas", string.Empty); };
        btnAtras.Click += delegate { AgregadorEventos.Publicar("MostrarVistaMenuRecursosHumanos", string.Empty); };
    }

    public void SeleccionarVistaInicial() {
        if (btnEmpleados.Visible)
            btnEmpleados.PerformClick();
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
        btnPersonas.Visible = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoParcial("MOD_RRHH_CONTACTOS")
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_TODOS");
    }
}