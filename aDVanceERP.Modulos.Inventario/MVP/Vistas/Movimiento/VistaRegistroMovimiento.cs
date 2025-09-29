using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Movimiento.Plantillas;

using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Movimiento;

public partial class VistaRegistroMovimiento : Form, IVistaRegistroMovimiento {
    private bool _modoEdicion;

    public VistaRegistroMovimiento() {
        InitializeComponent();

        NombreVista = nameof(VistaRegistroMovimiento);

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

    public string NombreProducto {
        get => fieldNombreProducto.Text;
        set => fieldNombreProducto.Text = value;
    }

    public string? NombreAlmacenOrigen {
        get => fieldNombreAlmacenOrigen.Text;
        set => fieldNombreAlmacenOrigen.Text = value;
    }

    public string? NombreAlmacenDestino {
        get => fieldNombreAlmacenDestino.Text;
        set => fieldNombreAlmacenDestino.Text = value;
    }

    public DateTime Fecha {
        get => fieldFecha.Value;
        set => fieldFecha.Value = value;
    }

    public decimal CantidadMovida {
        get => decimal.TryParse(fieldCantidadMovida.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var value) ? value : 0m;
        set => fieldCantidadMovida.Text = value.ToString("N2", CultureInfo.InvariantCulture);
    }

    public string TipoMovimiento {
        get => fieldTipoMovimiento.Text;
        set => fieldTipoMovimiento.Text = value;
    }

    public bool ModoEdicion {
        get => _modoEdicion;
        set {
            fieldSubtitulo.Text = value 
                ? "Detalles y actualización" 
                : "Registro";
            btnRegistrar.Text = value 
                ? "Actualizar movimiento" 
                : "Registrar movimiento";

            fieldNombreProducto.ReadOnly = value;
            fieldTipoMovimiento.Enabled = !value;
            btnAdicionarTipoMovimiento.Enabled = !value;
            btnEliminarTipoMovimiento.Enabled = !value;
            fieldNombreAlmacenOrigen.Enabled = !value;
            fieldNombreAlmacenDestino.Enabled = !value;
            fieldCantidadMovida.ReadOnly = value;

            _modoEdicion = value;
        }
    }

    public event EventHandler? RegistrarTipoMovimiento;
    public event EventHandler? EliminarTipoMovimiento;
    public event EventHandler? RegistrarEntidad;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarEntidad;
    

    public void Inicializar() {
        // Propiedades
        ModoEdicion = false;

        // Eventos
        fieldTipoMovimiento.SelectedIndexChanged += delegate { 
            ActualizarCamposAlmacenes(); 
        };
        btnCerrar.Click += delegate(object? sender, EventArgs args) {
            Close();
        };
        btnAdicionarTipoMovimiento.Click += delegate(object? sender, EventArgs args) {
            RegistrarTipoMovimiento?.Invoke(sender, args);
        };
        btnEliminarTipoMovimiento.Click += delegate(object? sender, EventArgs args) {
            EliminarTipoMovimiento?.Invoke(TipoMovimiento, args);
        };
        btnRegistrar.Click += delegate(object? sender, EventArgs args) {
            if (ModoEdicion)
                EditarEntidad?.Invoke(sender, args);
            else
                RegistrarEntidad?.Invoke(sender, args);
        };
        btnSalir.Click += delegate(object? sender, EventArgs args) { Close(); };
    }

    public void CargarNombresProductos(string[] nombresProductos) {
        fieldNombreProducto.AutoCompleteCustomSource.Clear();
        fieldNombreProducto.AutoCompleteCustomSource.AddRange(nombresProductos);
        fieldNombreProducto.AutoCompleteMode = AutoCompleteMode.Suggest;
        fieldNombreProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;
    }

    public void CargarNombresAlmacenes(object[] nombresAlmacenes) {
        fieldNombreAlmacenOrigen.Items.Clear();
        fieldNombreAlmacenOrigen.Items.Add("Ninguno");
        fieldNombreAlmacenOrigen.Items.AddRange(nombresAlmacenes);
        fieldNombreAlmacenOrigen.SelectedIndex = nombresAlmacenes.Length > 0 ? 0 : -1;

        fieldNombreAlmacenDestino.Items.Clear();
        fieldNombreAlmacenDestino.Items.Add("Ninguno");
        fieldNombreAlmacenDestino.Items.AddRange(nombresAlmacenes);
        fieldNombreAlmacenDestino.SelectedIndex = nombresAlmacenes.Length > 0 ? 0 : -1;
    }

    public void CargarTiposMovimientos(object[] tiposMovimientos) {
        fieldTipoMovimiento.Items.Clear();

        foreach (var tipoMovimiento in tiposMovimientos) {
            var tipoMovimientoString = tipoMovimiento.ToString();

            if (string.IsNullOrEmpty(tipoMovimientoString) || 
                tipoMovimientoString.Equals("Compra") || 
                tipoMovimientoString.Equals("Venta") ||
                tipoMovimientoString.Equals("Gasto material") ||
                tipoMovimientoString.Equals("Producción"))
                continue;

            fieldTipoMovimiento.Items.Add(tipoMovimiento);
        }

        if (fieldTipoMovimiento.Items.Count > 0)
            fieldTipoMovimiento.SelectedIndex = tiposMovimientos.Length > 0 ? 0 : -1;
    }

    public void ActualizarCamposAlmacenes() {
        var tipoMovimiento = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Nombre, TipoMovimiento).entidades.FirstOrDefault(tm => tm.Nombre.Equals(TipoMovimiento));

        if (tipoMovimiento?.Efecto == EfectoMovimiento.Carga) {
            fieldNombreAlmacenOrigen.SelectedIndex = 0;
            fieldNombreAlmacenOrigen.Enabled = false;
            fieldNombreAlmacenDestino.Enabled = !ModoEdicion;
        }
        else if (tipoMovimiento?.Efecto == EfectoMovimiento.Descarga) {
            fieldNombreAlmacenDestino.SelectedIndex = 0;
            fieldNombreAlmacenDestino.Enabled = false;
            fieldNombreAlmacenOrigen.Enabled = !ModoEdicion;
        }
        else {
            fieldNombreAlmacenOrigen.Enabled = !ModoEdicion;
            fieldNombreAlmacenDestino.Enabled = !ModoEdicion;
        }
    }

    public void Mostrar() {
        Fecha = DateTime.Now;

        BringToFront();
        ShowDialog();
    }

    public void Restaurar() {
        NombreProducto = string.Empty;
        NombreAlmacenOrigen = string.Empty;
        fieldNombreAlmacenOrigen.SelectedIndex = 0;
        NombreAlmacenDestino = string.Empty;
        fieldNombreAlmacenDestino.SelectedIndex = 0;
        CantidadMovida = 0;
        TipoMovimiento = string.Empty;
        fieldTipoMovimiento.SelectedIndex = 0;
        Fecha = DateTime.Now;
        ModoEdicion = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }
}