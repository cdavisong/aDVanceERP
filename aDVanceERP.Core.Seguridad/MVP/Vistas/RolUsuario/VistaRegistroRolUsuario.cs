using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Seguridad.MVP.Vistas.Permiso;
using aDVanceERP.Core.Seguridad.MVP.Vistas.Permiso.Plantillas;
using aDVanceERP.Core.Seguridad.MVP.Vistas.RolUsuario.Plantillas;
using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Core.Utiles;
using aDVanceERP.Core.Utiles.Datos;

namespace aDVanceERP.Core.Seguridad.MVP.Vistas.RolUsuario;

public partial class VistaRegistroRolUsuario : Form, IVistaRegistroRolUsuario, IVistaGestionPermisos {
    private bool _modoEdicion;

    public VistaRegistroRolUsuario() {
        InitializeComponent();

        NombreVista = nameof(VistaRegistroRolUsuario);
        Permisos = new List<string[]>();
        PanelCentral = new RepoVistaBase(contenedorVistas);

        Inicializar();
    }

    public string NombreVista {
        get => Name;
        private set => Name = value;
    }

    public int AlturaContenedorVistas {
        get => contenedorVistas.Height;
    }

    public int TuplasMaximasContenedor {
        get => AlturaContenedorVistas / VariablesGlobales.AlturaTuplaPredeterminada;
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

    public string? NombreRolUsuario {
        get => fieldNombreRolUsuario.Text;
        set => fieldNombreRolUsuario.Text = value;
    }

    public string? NombreModulo {
        get => fieldNombreModulo.Text;
        set => fieldNombreModulo.Text = value;
    }

    public string? NombrePermiso {
        get => fieldNombrePermiso.Text;
        set => fieldNombrePermiso.Text = value;
    }

    public List<string[]> Permisos { get; private set; }

    public event EventHandler? AlturaContenedorTuplasModificada;
    public event EventHandler? PermisoAgregado;
    public event EventHandler? PermisoEliminado;
    public event EventHandler? RegistrarEntidad;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarEntidad;


    public void Inicializar() {
        // Eventos
        btnCerrar.Click += delegate (object? sender, EventArgs args) {
            Close();
        };
        fieldNombreModulo.SelectedIndexChanged += delegate {
            var idModulo = UtilesModulo.ObtenerIdModulo(NombreModulo);

            if (idModulo != 0)
                CargarNombresPermisos(UtilesPermiso.ObtenerNombresPermisos(idModulo));
        };
        btnAdicionarPermiso.Click += delegate {
            AdicionarPermisoRol();
        };
        PermisoEliminado += delegate {
            ActualizarTuplasPermisosRoles();
        };
        btnRegistrar.Click += delegate (object? sender, EventArgs args) {
            if (ModoEdicion)
                EditarEntidad?.Invoke(sender, args);
            else
                RegistrarEntidad?.Invoke(sender, args);
        };
        btnSalir.Click += delegate (object? sender, EventArgs args) { Close(); };
    }

    public void CargarNombresModulos(string[] nombresModulos) {
        fieldNombreModulo.Items.AddRange(nombresModulos);
        fieldNombreModulo.SelectedIndex = -1;
    }

    private void CargarNombresPermisos(string[] nombresPermisos) {
        fieldNombrePermiso.Items.Clear();
        fieldNombrePermiso.Items.AddRange(nombresPermisos);
        fieldNombrePermiso.SelectedIndex = nombresPermisos.Length > 0 ? 0 : -1;
    }

    public void AdicionarPermisoRol(string nombrePermiso = "") {
        var adNombrePermiso = string.IsNullOrEmpty(nombrePermiso) ? NombrePermiso : nombrePermiso;
        var idPermiso = UtilesPermiso.ObtenerIdPermiso(adNombrePermiso);

        var tuplaPermiso = new[] {
            idPermiso.ToString(),
            adNombrePermiso
        };

        // Verificar que el permiso ya se encuentre registrado
        if (Permisos != null) {
            var indicePermiso = Permisos?.FindIndex(a => a[0].Equals(idPermiso.ToString()));

            if (indicePermiso != -1)
                return;
            else {
                Permisos?.Add(tuplaPermiso);
                PermisoAgregado?.Invoke(tuplaPermiso, EventArgs.Empty);
            }
        }

        fieldNombreModulo.SelectedIndex = -1;
        fieldNombrePermiso.SelectedIndex = -1;

        ActualizarTuplasPermisosRoles();
    }

    private void ActualizarTuplasPermisosRoles() {
        PanelCentral.CerrarTodos();

        // Restablecer útima coordenada Y de la tupla
        VariablesGlobales.CoordenadaYUltimaTupla = 0;

        for (var i = 0; i < Permisos?.Count; i++) {
            var permiso = Permisos[i];
            var tuplaPermisoRol = new VistaTuplaPermiso();

            tuplaPermisoRol.Id = permiso[0];
            tuplaPermisoRol.NombrePermiso = permiso[1];
            tuplaPermisoRol.EliminarDatosTupla += delegate (object? sender, EventArgs args) {
                permiso = sender as string[];

                Permisos.RemoveAt(Permisos.FindIndex(p => p[0].Equals(permiso?[0])));
                PermisoEliminado?.Invoke(permiso, args);
            };

            // Registro y muestra
            PanelCentral?.Registrar(
                tuplaPermisoRol,
                new Point(0, VariablesGlobales.CoordenadaYUltimaTupla),
                new Size(contenedorVistas.Width - 20, VariablesGlobales.AlturaTuplaPredeterminada), 
                TipoRedimensionadoVista.Ninguno);
            tuplaPermisoRol.Mostrar();

            // Incremento de la útima coordenada Y de la tupla
            VariablesGlobales.CoordenadaYUltimaTupla += VariablesGlobales.AlturaTuplaPredeterminada;
        }
    }

    public void Mostrar() {
        BringToFront();
        ShowDialog();
    }

    public void Restaurar() {
        NombreRolUsuario = string.Empty;
        fieldNombreModulo.SelectedIndex = 0;
        fieldNombrePermiso.SelectedIndex = 0;
        ModoEdicion = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }
}