using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Compra.Plantillas;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Compra; 

public partial class VistaTuplaCompra : Form, IVistaTuplaCompra {
    public VistaTuplaCompra() {
        InitializeComponent();

        NombreVista = nameof(VistaTuplaCompra);

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

    public string Fecha {
        get => fieldFecha.Text;
        set => fieldFecha.Text = value;
    }

    public string NombreAlmacen {
        get => fieldNombreAlmacen.Text;
        set {
            fieldNombreAlmacen.Text = string.IsNullOrEmpty(value) ? "Ninguno" : value;
            fieldNombreAlmacen.Margin = new Padding(1, value?.Length > 16 ? 10 : 1, 1, 1);
        }
    }

    public string NombreProveedor {
        get => fieldNombreProveedor.Text;
        set => fieldNombreProveedor.Text = value;
    }

    public string CantidadProductos {
        get => fieldCantidadProductos.Text;
        set => fieldCantidadProductos.Text = value;
    }

    public string MontoTotal {
        get => fieldMontoTotal.Text;
        set => fieldMontoTotal.Text = value;
    }

    public Color ColorFondoTupla {
        get => layoutVista.BackColor;
        set => layoutVista.BackColor = value;
    }

    public event EventHandler? TuplaSeleccionada;
    public event EventHandler? EditarDatosTupla;
    public event EventHandler? EliminarDatosTupla;
    

    public void Inicializar() {
        // Eventos
        fieldId.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldFecha.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldNombreAlmacen.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldNombreProveedor.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldCantidadProductos.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldMontoTotal.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };

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
        btnEditar.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                "MOD_COMPRAVENTA_COMPRA_EDITAR")
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_COMPRA_TODOS")
                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_TODOS");
        btnEliminar.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                  "MOD_COMPRAVENTA_COMPRA_ELIMINAR")
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                  "MOD_COMPRAVENTA_COMPRA_TODOS")
                              || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_TODOS");
    }
}