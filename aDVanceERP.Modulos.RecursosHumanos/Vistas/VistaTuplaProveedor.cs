using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Vistas;

public partial class VistaTuplaProveedor : Form, IVistaTuplaProveedor {
    public VistaTuplaProveedor() {
        InitializeComponent();

        NombreVista = nameof(VistaTuplaProveedor);

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

    public string Id {
        get => fieldId.Text;
        set => fieldId.Text = value;
    }

    public string NumeroIdentificacionTributaria {
        get => fieldNumeroIdentificacionTributaria.Text;
        set => fieldNumeroIdentificacionTributaria.Text = value;
    }

    public string RazonSocial {
        get => fieldRazonSocial.Text;
        set {
            fieldRazonSocial.Text = value;
            fieldRazonSocial.Margin = fieldRazonSocial.AjusteAutomaticoMargenTexto();
        }
    }

    public string Telefonos {
        get => fieldTelefonos.Text;
        set => fieldTelefonos.Text = value;
    }

    public string Direccion {
        get => fieldDireccion.Text;
        set {
            fieldDireccion.Text = value;
            fieldDireccion.Margin = fieldDireccion.AjusteAutomaticoMargenTexto();
        }
    }

    public string NombreRepresentante {
        get => fieldNombreRepresentante.Text;
        set {
            fieldNombreRepresentante.Text = value;
            fieldNombreRepresentante.Margin = fieldNombreRepresentante.AjusteAutomaticoMargenTexto();
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
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_PROVEEDORES_EDITAR")
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_PROVEEDORES_TODOS")
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_TODOS");
        btnEliminar.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_PROVEEDORES_ELIMINAR")
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_PROVEEDORES_TODOS")
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_TODOS");
    }
}