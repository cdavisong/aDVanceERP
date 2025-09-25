using aDVanceERP.Core.Documentos.Interfaces;
using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Almacen.Plantillas;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Almacen;

public partial class VistaTuplaAlmacen : Form, IVistaTuplaAlmacen {
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

    public string Id {
        get => fieldId.Text;
        set => fieldId.Text = value;
    }

    public string NombreAlmacen {
        get => fieldNombre.Text;
        set => fieldNombre.Text = value;
    }

    public string Direccion {
        get => fieldDireccion.Text;
        set {
            fieldDireccion.Text = value;
            fieldDireccion.Margin = new Padding(1, value?.Length > 43 ? 10 : 1, 1, 1);
        }
    }

    public string Descripcion {
        get => fieldDescripcion.Text;
        set {
            fieldDescripcion.Text = value;
            fieldDescripcion.Margin = new Padding(1, value?.Length > 43 ? 10 : 1, 1, 1);
        }
    }

    public bool MostrarBotonExportarProductos {
        get => btnExportarProductos.Visible;
        set => btnExportarProductos.Visible = value;
    }

    public Color ColorFondoTupla {
        get => layoutVista.BackColor;
        set => layoutVista.BackColor = value;
    }

    public event EventHandler? TuplaSeleccionada;
    public event EventHandler<(int, FormatoDocumento)>? ExportarDocumentoInventario;
    public event EventHandler? DescargarProductos;
    public event EventHandler? EditarDatosTupla;
    public event EventHandler? EliminarDatosTupla;


    public void Inicializar() {
        // Eventos
        fieldId.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldNombre.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldDireccion.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldDescripcion.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };

        btnExportarDocumentoInventario.Click += delegate {
            btnExportarDocumentoInventario.ContextMenuStrip?.Show(btnExportarDocumentoInventario, new Point(0, 40));
        };
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
        btnEditar.Enabled = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                            || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                "MOD_INVENTARIO_ALMACENES_EDITAR")
                            || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                "MOD_INVENTARIO_ALMACENES_TODOS")
                            || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
        btnEliminar.Enabled = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                              || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                  "MOD_INVENTARIO_ALMACENES_ELIMINAR")
                              || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                  "MOD_INVENTARIO_ALMACENES_TODOS")
                              || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
    }
}