using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Menu.Plantillas;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Menu; 

public partial class VistaMenuCompraventas : Form, IVistaMenuVentas {
    public VistaMenuCompraventas() {
        InitializeComponent();

        NombreVista = nameof(VistaMenuCompraventas);

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

    public event EventHandler? VerCompras;
    public event EventHandler? VerVentas;
    public event EventHandler? CambioMenu;
    


    public void Inicializar() {
        // Eventos        
        btnVenta.Click += delegate(object? sender, EventArgs e) { PresionarBotonSeleccion(1, e); };
        btnCompra.Click += delegate (object? sender, EventArgs e) { PresionarBotonSeleccion(2, e); };
    }

    public void SeleccionarVistaInicial() {
        if (btnVenta.Visible)
            btnVenta.PerformClick();
        else if (btnCompra.Visible)
            btnCompra.PerformClick();
    }

    public void PresionarBotonSeleccion(object? sender, EventArgs e) {
        var indiceValido = int.TryParse(sender?.ToString() ?? string.Empty, out var indice);

        if (!indiceValido)
            return;

        CambioMenu?.Invoke(sender, e);

        switch (indice) {            
            case 1:
                VerVentas?.Invoke(btnVenta, e);
                if (!btnVenta.Checked)
                    btnVenta.Checked = true;
                break;
            case 2:
                VerCompras?.Invoke(btnCompra, e);
                if (!btnCompra.Checked)
                    btnCompra.Checked = true;
                break;
        }
    }

    public void Mostrar() {
        VerificarPermisos();
        BringToFront();
        Show();
    }

    public void Restaurar() {
        btnVenta.Checked = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }

    private void VerificarPermisos() {
        btnCompra.Visible = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoParcial("MOD_COMPRAVENTA_COMPRA")
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_COMPRA");
        btnVenta.Visible = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                           || ContextoSeguridad.PermisosUsuario.ContienePermisoParcial("MOD_COMPRAVENTA_VENTA")
                           || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_TODOS");
    }
}