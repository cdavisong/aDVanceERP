using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Core.Utiles;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Movimiento.Plantillas;
using aDVanceERP.Modulos.Inventario.Properties;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Movimiento; 

public partial class VistaTuplaMovimiento : Form, IVistaTuplaMovimiento {
    public VistaTuplaMovimiento() {
        InitializeComponent();

        NombreVista = nameof(VistaTuplaMovimiento);

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

    public string NombreProducto {
        get => fieldNombreProducto.Text;
        set => fieldNombreProducto.Text = value;
    }

    public string NombreAlmacenOrigen {
        get => fieldNombreAlmacenOrigen.Text;
        set {
            fieldNombreAlmacenOrigen.Text = string.IsNullOrEmpty(value) ? "Ninguno" : value;
            fieldNombreAlmacenOrigen.Margin = new Padding(1, value?.Length > 16 ? 10 : 1, 1, 1);
        }
    }

    public string NombreAlmacenDestino {
        get => fieldNombreAlmacenDestino.Text;
        set {
            fieldNombreAlmacenDestino.Text = string.IsNullOrEmpty(value) ? "Ninguno" : value;
            fieldNombreAlmacenDestino.Margin = new Padding(1, value?.Length > 16 ? 10 : 1, 1, 1);
        }
    }

    public string SaldoInicial {
        get => fieldSaldoInicial.Text;
        set => fieldSaldoInicial.Text = value;
    }

    public string CantidadMovida {
        get => fieldCantidadMovida.Text;
        set => fieldCantidadMovida.Text = value;
    }

    public string SaldoFinal {
        get => fieldSaldoFinal.Text;
        set { 
            fieldSaldoFinal.Text = value;

            //ColorFondoTupla = ObtenerColorTupla();
        }
    }

    public string TipoMovimiento {
        get => fieldTipoMovimiento.Text;
        set {
            fieldTipoMovimiento.Text = string.IsNullOrEmpty(value) ? "ERROR" : value;
            fieldTipoMovimiento.Margin = new Padding(1, value?.Length > 23 ? 10 : 1, 1, 1);
        }
    }

    public string Fecha {
        get => fieldFecha.Text;
        set => fieldFecha.Text = value;
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
        fieldNombreProducto.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldNombreAlmacenOrigen.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldNombreAlmacenDestino.Click += delegate(object? sender, EventArgs e) {
            TuplaSeleccionada?.Invoke(this, e);
        };
        fieldCantidadMovida.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldTipoMovimiento.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldFecha.Click += delegate(object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };

        btnEditar.Click += delegate(object? sender, EventArgs e) { EditarDatosTupla?.Invoke(this, e); };
        btnEliminar.Click += delegate(object? sender, EventArgs e) { EliminarDatosTupla?.Invoke(this, e); };
    }

    public void ActualizarIconoStock(EfectoMovimiento efecto) {
        fieldIcono.BackgroundImage = efecto switch {
            EfectoMovimiento.Carga => Resources.load_cargo_20px,
            EfectoMovimiento.Descarga => Resources.unload_cargo_20px,
            EfectoMovimiento.Transferencia => Resources.transfer_20px,
            _ => fieldIcono.BackgroundImage
        };
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
        if (UtilesCuentaUsuario.UsuarioAutenticado == null || UtilesCuentaUsuario.PermisosUsuario == null) {
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            return;
        }

        btnEditar.Enabled = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                            || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                "MOD_INVENTARIO_MOVIMIENTOS_EDITAR")
                            || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                "MOD_INVENTARIO_MOVIMIENTOS_TODOS")
                            || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
        btnEliminar.Enabled = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                              || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                  "MOD_INVENTARIO_MOVIMIENTOS_ELIMINAR")
                              || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                  "MOD_INVENTARIO_MOVIMIENTOS_TODOS")
                              || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
    }

    private Color ObtenerColorTupla() {
        var producto = RepoProducto.Instancia.Buscar(FiltroBusquedaProducto.Nombre, NombreProducto).resultados.FirstOrDefault(p => p.Nombre.Equals(NombreProducto));
        var inventarioProducto = producto != null ? RepoInventario.Instancia.Buscar(FiltroBusquedaInventario.IdProducto, producto.Id.ToString()).resultados : null;
        var saldoRealProducto = inventarioProducto != null && inventarioProducto.Any() ? inventarioProducto.Sum(i => i.Cantidad) : 0.0m;
        var saldoFinalDecimal = decimal.TryParse(SaldoFinal, out var saldoFinal) ? saldoFinal : 0.0m;

        if (saldoRealProducto.CompareTo(saldoFinalDecimal) != 0)
            return VariablesGlobales.ColorErrorTupla;

        return BackColor;
    }
}