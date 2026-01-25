using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Modulos.Venta.Interfaces;
namespace aDVanceERP.Modulos.Venta.Vistas;

public partial class VistaTuplaMensajero : Form, IVistaTuplaMensajero {
    public VistaTuplaMensajero() {
        InitializeComponent();

        NombreVista = nameof(VistaTuplaMensajero);

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

    public Color ColorFondoTupla {
        get => layoutVista.BackColor;
        set => layoutVista.BackColor = value;
    }

    public bool EstadoSeleccion { get; set; }

    public long Id {
        get => Convert.ToInt64(fieldId.Text);
        set => fieldId.Text = value.ToString();
    }

    public string CodigoMensajero {
        get => fieldCodigo.Text;
        set => fieldCodigo.Text = value;
    }

    public string NombreCompleto {
        get => fieldNombreCompleto.Text;
        set {
            fieldNombreCompleto.Text = value;
            fieldNombreCompleto.Margin = fieldNombreCompleto.AjusteAutomaticoMargenTexto();
        }    
    }

    public string Telefonos {
        get => fieldTelefonos.Text;
        set {
            fieldTelefonos.Text = value;
            fieldTelefonos.Margin = fieldTelefonos.AjusteAutomaticoMargenTexto();
        }
    }

    public string MatriculaVehiculo {
        get => fieldMatriculaVehiculo.Text;
        set => fieldMatriculaVehiculo.Text = value;
    }

    public bool Activo {
        get => fieldEstado.Text.Equals("Activo");
        set {
            fieldEstado.Text = value ? "Activo" : "Inactivo";
            fieldEstado.ForeColor = value ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
        }
    }

    public event EventHandler? EditarDatosTupla;
    public event EventHandler? EliminarDatosTupla;

    public void Inicializar() {
        // Eventos
        btnEditar.Click += delegate (object? sender, EventArgs e) { EditarDatosTupla?.Invoke(this, e); };
        btnEliminar.Click += delegate (object? sender, EventArgs e) { EliminarDatosTupla?.Invoke(this, e); };
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
        btnEditar.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_MENSAJEROS_EDITAR")
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_MENSAJEROS_TODOS")
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_TODOS");
        btnEliminar.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_MENSAJEROS_ELIMINAR")
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_MENSAJEROS_TODOS")
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_TODOS");
    }
}