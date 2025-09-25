using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.CuentaBancaria.Plantillas;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.CuentaBancaria; 

public partial class VistaTuplaCuentaBancaria : Form, IVistaTuplaCuentaBancaria {
    public VistaTuplaCuentaBancaria() {
        InitializeComponent();

        NombreVista = nameof(VistaTuplaCuentaBancaria);

        Inicializar();
    }

    public string NombreVista {
        get => $"{Name}{Id}";
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

    public string Id {
        get => fieldId.Text;
        set => fieldId.Text = value;
    }

    public string Alias {
        get => fieldAlias.Text;
        set => fieldAlias.Text = value;
    }

    public string NumeroTarjeta {
        get => fieldNumeroTarjeta.Text;
        set => fieldNumeroTarjeta.Text = value;
    }

    public string Moneda {
        get => fieldTipoMoneda.Text;
        set => fieldTipoMoneda.Text = value;
    }

    public string NombrePropietario {
        get => fieldNombrePropietario.Text;
        set => fieldNombrePropietario.Text = value;
    }

    public Color ColorFondoTupla {
        get => layoutVista.BackColor;
        set => layoutVista.BackColor = value;
    }

    public event EventHandler? TuplaSeleccionada;
    public event EventHandler? MostrarQR;
    public event EventHandler? EditarDatosTupla;
    public event EventHandler? EliminarDatosTupla;
    

    public void Inicializar() {
        // Eventos
        fieldId.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldAlias.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldNumeroTarjeta.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldTipoMoneda.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldNombrePropietario.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        btnQR.Click += delegate(object? sender, EventArgs e) {
            var numeroTarjeta = NumeroTarjeta.Replace(" ", string.Empty);
            var idContacto = UtilesContacto.ObtenerIdContacto(NombrePropietario).Result;
            var telefonoMovil = UtilesTelefonoContacto.ObtenerTelefonoContacto(idContacto, true);

            MostrarQR?.Invoke($"{Alias},{numeroTarjeta},{telefonoMovil}", e);
        };
        btnEditar.Click += delegate(object? sender, EventArgs e) { EditarDatosTupla?.Invoke(this, e); };
        btnEliminar.Click += delegate(object? sender, EventArgs e) { EliminarDatosTupla?.Invoke(this, e); };
    }

    public void Mostrar() {
        VerificarPermisos();
        BringToFront();
        Show();
    }

    public void Restaurar() {
        ColorFondoTupla = BackColor;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }

    private void VerificarPermisos() {
        btnEditar.Enabled = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                            || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                "MOD_FINANZAS_CUENTAS_BANCARIAS_EDITAR")
                            || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                "MOD_FINANZAS_CUENTAS_BANCARIAS_TODOS")
                            || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_FINANZAS_TODOS");
        btnEliminar.Enabled = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                              || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                  "MOD_FINANZAS_CUENTAS_BANCARIAS_ELIMINAR")
                              || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                  "MOD_FINANZAS_CUENTAS_BANCARIAS_TODOS")
                              || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_FINANZAS_TODOS");
    }
}