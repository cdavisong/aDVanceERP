using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Vistas;

public partial class VistaRegistroRolUsuario : Form, IVistaRegistroRolUsuario {
    private bool _modoEdicion;

    public VistaRegistroRolUsuario() {
        InitializeComponent();

        NombreVista = nameof(VistaRegistroRolUsuario);
        PanelCentral = new RepoVistaBase(panelPermisos);

        Inicializar();
    }

    public string NombreVista {
        get => Name;
        private set => Name = value;
    }

    public RepoVistaBase PanelCentral { get; private set; }

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

    public bool ModoEdicion {
        get => _modoEdicion;
        set {
            fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
            btnRegistrar.Text = value ? "Actualizar rol" : "Registrar rol";
            _modoEdicion = value;
        }
    }
    public int TuplasMaximasContenedor {
        get => panelPermisos.Height / ContextoAplicacion.AlturaTuplaPredeterminada;
    }

    public string NombreRolUsuario {
        get => fieldNombreRolUsuario.Text;
        set => fieldNombreRolUsuario.Text = value;
    }

    public string[] NombresModulos {
        get => fieldNombreModulo.Items.Cast<string>().ToArray();
        set {
            fieldNombreModulo.Items.Clear();
            fieldNombreModulo.Items.AddRange(value); 
        }
    }

    public string[] NombresPermisos {
        get => fieldNombrePermiso.Items.Cast<string>().ToArray();
        set {
            fieldNombrePermiso.Items.Clear();
            fieldNombrePermiso.Items.AddRange(value);
        }
    }
    public List<Core.Modelos.Modulos.Seguridad.Permiso> Permisos { get; } = new();

    public event EventHandler? RegistrarEntidad;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarEntidad;
    public event EventHandler<string> CambioModulo;
    public event EventHandler<string>? RegistrarPermiso;

    public void Inicializar() {
        // Eventos
        fieldNombreModulo.SelectedIndexChanged += delegate {
            CambioModulo?.Invoke(this, fieldNombreModulo.Text);
        };
        btnAdicionarPermiso.Click += delegate {
            RegistrarPermiso?.Invoke(this, fieldNombrePermiso.Text);
        };
        btnRegistrar.Click += delegate (object? sender, EventArgs args) {
            if (ModoEdicion)
                EditarEntidad?.Invoke(sender, args);
            else
                RegistrarEntidad?.Invoke(sender, args);
        };
        btnSalir.Click += delegate (object? sender, EventArgs args) { Ocultar(); };
    }

    public void Mostrar() {
        BringToFront();
        Show();
    }

    public void Restaurar() {
        NombreRolUsuario = string.Empty;

        if (fieldNombreModulo.Items.Count > 0)
            fieldNombreModulo.SelectedIndex = 0;
        if (fieldNombrePermiso.Items.Count > 0)
            fieldNombrePermiso.SelectedIndex = 0;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }
}