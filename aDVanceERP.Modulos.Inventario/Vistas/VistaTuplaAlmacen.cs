using aDVanceERP.Core.Documentos.Interfaces;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Vistas;

public partial class VistaTuplaAlmacen : Form, IVistaTuplaAlmacen {
    private CoordenadasGeograficas _coordenadasGeograficas = null!;

    public VistaTuplaAlmacen() {
        InitializeComponent();
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

    public string NombreAlmacen {
        get => fieldNombre.Text;
        set => fieldNombre.Text = value;
    }

    public string Tipo { 
        get => fieldTipo.Text;
        set => fieldTipo.Text = value;
    }

    public CoordenadasGeograficas CoordenadasGeograficas { 
        get => _coordenadasGeograficas;
        set {
            _coordenadasGeograficas = value;

            var coordenadasInvalidas = _coordenadasGeograficas is null || (_coordenadasGeograficas.Latitud == 0 && _coordenadasGeograficas.Longitud == 0);

            fieldCoordenadasGeograficas.BackgroundImage = coordenadasInvalidas
                ? Properties.Resources.markerF_off_20px
                : Properties.Resources.locationG_20px;
            fieldCoordenadasGeograficas.Cursor = coordenadasInvalidas
                ? Cursors.Default
                : Cursors.Hand;
        }
    }

    public string Direccion {
        get => fieldDireccion.Text;
        set {
            fieldDireccion.Text = value;
            fieldDireccion.Margin = fieldDireccion.AjusteAutomaticoMargenTexto();
        }
    }

    public string Descripcion {
        get => fieldDescripcion.Text;
        set {
            fieldDescripcion.Text = value;
            fieldDescripcion.Margin = fieldDireccion.AjusteAutomaticoMargenTexto();
        }
    }

    public bool Estado { 
        get => fieldEstado.Text.Equals("Activo");
        set {
            fieldEstado.Text = value ? "Activo" : "Inactivo";
            fieldEstado.ForeColor = value ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
        }
    }

    public bool MostrarBotonExportarProductos {
        get => btnExportarProductos.Visible;
        set => btnExportarProductos.Visible = value;
    }

    public event EventHandler<(int, FormatoDocumento)>? ExportarDocumentoInventario;
    public event EventHandler? DescargarProductos;
    public event EventHandler? EditarDatosTupla;
    public event EventHandler? EliminarDatosTupla;

    public void Inicializar() {
        // Eventos
        btnExportarDocumentoInventario.Click += delegate { btnExportarDocumentoInventario.ContextMenuStrip?.Show(btnExportarDocumentoInventario, new Point(0, 40)); };
        btnExportarPdf.Click += delegate { ExportarDocumentoInventario?.Invoke(this, (int.Parse(Id), FormatoDocumento.PDF)); };
        btnExportarXlsx.Click += delegate { ExportarDocumentoInventario?.Invoke(this, (int.Parse(Id), FormatoDocumento.Excel)); };
        btnExportarProductos.Click += delegate (object? sender, EventArgs e) { DescargarProductos?.Invoke(Id, e); };
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
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                "MOD_INVENTARIO_ALMACENES_EDITAR")
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                "MOD_INVENTARIO_ALMACENES_TODOS")
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
        btnEliminar.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                  "MOD_INVENTARIO_ALMACENES_ELIMINAR")
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                  "MOD_INVENTARIO_ALMACENES_TODOS")
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
    }
}