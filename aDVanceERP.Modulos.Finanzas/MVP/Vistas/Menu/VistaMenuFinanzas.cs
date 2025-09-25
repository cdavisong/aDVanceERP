using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.Menu.Plantillas;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.Menu;

public partial class VistaMenuFinanzas : Form, IVistaMenuFinanzas {
    public VistaMenuFinanzas() {
        InitializeComponent();

        NombreVista = nameof(VistaMenuFinanzas);

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

    public event EventHandler? VerCuentas;
    public event EventHandler? VerCajas;
    public event EventHandler? CambioMenu;
    

    public void Inicializar() {
        // Eventos
        btnCuentasBancarias.Click += delegate (object? sender, EventArgs e) { PresionarBotonSeleccion(1, e); };
        btnCajas.Click += delegate (object? sender, EventArgs e) { PresionarBotonSeleccion(2, e); };
    }

    public void MostrarCaracteristicaInicial() {
        if (btnCuentasBancarias.Visible)
            btnCuentasBancarias.PerformClick();
        else if (btnCajas.Visible)
            btnCajas.PerformClick();
    }

    public void PresionarBotonSeleccion(object? sender, EventArgs e) {
        var indiceValido = int.TryParse(sender?.ToString() ?? string.Empty, out var indice);

        if (!indiceValido)
            return;

        CambioMenu?.Invoke(sender, e);

        switch (indice) {
            case 1:
                VerCuentas?.Invoke(btnCuentasBancarias, e);
                if (!btnCuentasBancarias.Checked)
                    btnCuentasBancarias.Checked = true;
                break;
            case 2:
                VerCajas?.Invoke(btnCajas, e);
                if (!btnCajas.Checked)
                    btnCajas.Checked = true;
                break;
        }
    }

    public void Mostrar() {
        VerificarPermisos();
        BringToFront();
        Show();
    }

    public void Restaurar() {
        btnCuentasBancarias.Checked = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }

    private void VerificarPermisos() {
        btnCuentasBancarias.Visible = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                                      || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoParcial("MOD_FINANZAS_CUENTAS_BANCARIAS")
                                      || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_FINANZAS_TODOS");
        btnCajas.Visible = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                                || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoParcial("MOD_FINANZAS_CAJA")
                                || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_FINANZAS_TODOS");
    }
}