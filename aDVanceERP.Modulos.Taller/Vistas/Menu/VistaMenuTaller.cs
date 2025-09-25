using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Modulos.Taller.Interfaces;

namespace aDVanceERP.Modulos.Taller.Vistas.Menu;

public partial class VistaMenuTaller : Form, IVistaMenuTaller {
    public VistaMenuTaller() {
        InitializeComponent();

        NombreVista = nameof(VistaMenuTaller);

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

    public event EventHandler? VerOrdenesProduccion;
    public event EventHandler? CambioMenu;
    

    public void Inicializar() {
        // Eventos
        btnOrdenesProduccion.Click += delegate (object? sender, EventArgs e) { PresionarBotonSeleccion(1, e); };
    }

    public void MostrarCaracteristicaInicial() {
        if (btnOrdenesProduccion.Visible)
            btnOrdenesProduccion.PerformClick();
    }

    public void PresionarBotonSeleccion(object? sender, EventArgs e) {
        var indiceValido = int.TryParse(sender?.ToString() ?? string.Empty, out var indice);

        if (!indiceValido)
            return;

        CambioMenu?.Invoke(sender, e);

        switch (indice) {
            case 1:
                VerOrdenesProduccion?.Invoke(btnOrdenesProduccion, e);
                if (!btnOrdenesProduccion.Checked)
                    btnOrdenesProduccion.Checked = true;
                break;
        }
    }

    public void Mostrar() {
        VerificarPermisos();
        BringToFront();
        Show();
    }

    public void Restaurar() {
        btnOrdenesProduccion.Checked = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }

    private void VerificarPermisos() {
        btnOrdenesProduccion.Visible = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                               || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoParcial("MOD_TALLER_ORDENES_PRODUCCION")
                               || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_TALLER_ORDENES_PRODUCCION_TODOS");
    }
}