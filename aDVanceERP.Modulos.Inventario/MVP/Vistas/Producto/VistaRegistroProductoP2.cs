using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto;

public partial class VistaRegistroProductoP2 : Form {
    private bool _modoEdicion;

    public VistaRegistroProductoP2() {
        InitializeComponent();
        Inicializar();
    }

    public string UnidadMedida {
        get => fieldUnidadMedida.Text;
        set => fieldUnidadMedida.Text = value;
    }

    public string[] AbreviaturasUnidadesMedida { get; private set; } = Array.Empty<string>();

    public string[] DescripcionesUnidadMedida { get; private set; } = Array.Empty<string>();

    public string TipoMateriaPrima {
        get => fieldTipoMateriaPrima.Text;
        set => fieldTipoMateriaPrima.Text = value;
    }

    public string[] DescripcionesTiposMateriaPrima { get; private set; } = Array.Empty<string>();

    public decimal PrecioCompra {
        get => decimal.TryParse(fieldCostoUnitario.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                 out var value)
                 ? value
                 : 0;
        set => fieldCostoUnitario.Text = value.ToString("N2", CultureInfo.InvariantCulture);
    }

    public decimal CostoProduccionUnitario {
        get => decimal.TryParse(fieldCostoUnitario.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                 out var value)
                 ? value
                 : 0;
        set => fieldCostoUnitario.Text = value.ToString("N2", CultureInfo.InvariantCulture);
    }

    public decimal PrecioVentaBase {
        get => decimal.TryParse(fieldPrecioVentaBase.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                 out var value)
                 ? value
                 : 0m;
        set => fieldPrecioVentaBase.Text = value.ToString("N2", CultureInfo.InvariantCulture);
    }

    public string? NombreAlmacen {
        get => fieldNombreAlmacen.Text;
        set => fieldNombreAlmacen.Text = value;
    }

    public decimal StockInicial {
        get => decimal.TryParse(fieldStockInicial.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var value)
                 ? value
                 : 0m;
        set => fieldStockInicial.Text = value.ToString("N2", CultureInfo.InvariantCulture);
    }

    public bool ModoEdicionDatos {
        get => _modoEdicion;
        set {
            ConfigurarVisibilidadCamposAlmacenStock(!value);

            _modoEdicion = value;
        }
    }

    public event EventHandler? RegistrarUnidadMedida;
    public event EventHandler? RegistrarTipoMateriaPrima;
    public event EventHandler? EliminarUnidadMedida;
    public event EventHandler? EliminarTipoMateriaPrima;

    private void Inicializar() {
        // Configuracion de los campos de tipo de materia prima
        ConfigurarVisibilidadCamposTipoMateriaPrima(false);

        // Eventos
        fieldUnidadMedida.SelectedIndexChanged += delegate (object? sender, EventArgs args) {
            if (AbreviaturasUnidadesMedida.Length == 0 || DescripcionesUnidadMedida.Length == 0)
                return;

            fieldAbreviaturaUnidadMedida.Text = AbreviaturasUnidadesMedida[fieldUnidadMedida.SelectedIndex];
            fieldDescripcionUnidadMedida.Text = DescripcionesUnidadMedida[fieldUnidadMedida.SelectedIndex];
        };
        fieldTipoMateriaPrima.SelectedIndexChanged += delegate (object? sender, EventArgs args) {
            if (DescripcionesTiposMateriaPrima.Length == 0)
                return;

            fieldDescripcionTipoMateriaPrima.Text = DescripcionesTiposMateriaPrima[fieldTipoMateriaPrima.SelectedIndex];
        };
        btnAdicionarUnidadMedida.Click += delegate (object? sender, EventArgs args) {
            RegistrarUnidadMedida?.Invoke(sender, args);
        };
        btnAdicionarTipoMateriaPrima.Click += delegate (object? sender, EventArgs args) {
            RegistrarTipoMateriaPrima?.Invoke(sender, args);
        };
        btnEliminarUnidadMedida.Click += delegate (object? sender, EventArgs args) {
            EliminarUnidadMedida?.Invoke(UnidadMedida, args);
        };
        btnEliminarTipoMateriaPrima.Click += delegate (object? sender, EventArgs args) {
            EliminarTipoMateriaPrima?.Invoke(TipoMateriaPrima, args);
        };
        fieldNombreAlmacen.SelectedIndexChanged += delegate {
            StockInicial = 0;

            fieldStockInicial.Focus();
        };
    }

    public void CargarUnidadesMedida((string[] nombres, string[] abreviaturas, string[] descripciones) unidadesMedida) {
        fieldUnidadMedida.Items.Clear();
        fieldUnidadMedida.Items.AddRange(unidadesMedida.nombres);
        fieldUnidadMedida.SelectedIndex = unidadesMedida.nombres.Length > 0 ? 0 : -1;

        // Limpiar descripciones y abreviaturas
        AbreviaturasUnidadesMedida = unidadesMedida.abreviaturas;
        DescripcionesUnidadMedida = unidadesMedida.descripciones;
    }

    public void CargarTiposMateriaPrima(object[] nombresTiposMateriaPrima) {
        fieldTipoMateriaPrima.Items.Clear();
        fieldTipoMateriaPrima.Items.AddRange(nombresTiposMateriaPrima);
        fieldTipoMateriaPrima.SelectedIndex = nombresTiposMateriaPrima.Length > 0 ? 0 : -1;
    }

    public void CargarDescripcionesTiposMateriaPrima(string[] descripcionesTiposMateriaPrima) {
        Array.Clear(DescripcionesTiposMateriaPrima);
        DescripcionesTiposMateriaPrima = descripcionesTiposMateriaPrima;
    }

    public void CargarNombresAlmacenes(object[] nombresAlmacenes) {
        fieldNombreAlmacen.Items.Clear();
        fieldNombreAlmacen.Items.AddRange(nombresAlmacenes);
        fieldNombreAlmacen.SelectedIndex = nombresAlmacenes.Length > 0 ? 0 : -1;
    }

    public void ConfigurarVisibilidadCamposTipoMateriaPrima(bool mostrarTipoMateriaPrima) {
        // Ajustar visibilidad y altura de filas para tipo de materia prima
        fieldTituloTipoMateriaPrima.Visible = mostrarTipoMateriaPrima;
        layoutBase.RowStyles[4].Height = mostrarTipoMateriaPrima ? 35F : 0F;

        layoutTipoMateriaPrima.Visible = mostrarTipoMateriaPrima;
        layoutBase.RowStyles[5].Height = mostrarTipoMateriaPrima ? 45F : 0F;

        fieldDescripcionTipoMateriaPrima.Visible = mostrarTipoMateriaPrima;
        layoutBase.RowStyles[6].Height = mostrarTipoMateriaPrima ? 25F : 0F;

        // Ajustar el separador según si el producto es materia prima
        separador1.Visible = mostrarTipoMateriaPrima;
        layoutBase.RowStyles[3].Height = mostrarTipoMateriaPrima ? 20F : 0F;

        // Forzar el redibujado del layout
        layoutBase.PerformLayout();

        // Limpiar datos:
        if (fieldTipoMateriaPrima.Items.Count > 0 && !mostrarTipoMateriaPrima)
            fieldTipoMateriaPrima.SelectedIndex = 0;
    }

    public void ConfigurarVisibilidadCamposPrecios(bool costoUnitarioProduccion, bool mostrarVenta) {
        // Actualizar label de título de costo unitario o precio de compra
        fieldTituloCostoUnitario.Text = costoUnitarioProduccion
            ? "  ●   Costo unitario de producción"
            : "  ●   Precio de compra";

        // Ajustar visibilidad y altura de fila para precio de venta
        layoutPrecioVentaBase.Visible = mostrarVenta;
        layoutBase.RowStyles[10].Height = mostrarVenta ? 45F : 0F;

        // Forzar el redibujado del layout
        layoutBase.PerformLayout();
    }

    public void ConfigurarVisibilidadCamposAlmacenStock(bool mostrarAlmacenStock) {
        // Ajustar visibilidad y altura de filas para almacén y cantidad inicial
        layoutTituloAlmacenStock.Visible = mostrarAlmacenStock;
        layoutBase.RowStyles[12].Height = mostrarAlmacenStock ? 35F : 0F;

        layoutAlmacenStock.Visible = mostrarAlmacenStock;
        layoutBase.RowStyles[13].Height = mostrarAlmacenStock ? 45F : 0F;

        // Ajustar el separador según si el campo de almacén y cantidad está visible
        separador1.Visible = mostrarAlmacenStock;
        layoutBase.RowStyles[11].Height = mostrarAlmacenStock ? 20F : 0F;

        // Forzar el redibujado del layout
        layoutBase.PerformLayout();
    }

    public void Restaurar() {
        UnidadMedida = string.Empty;
        fieldUnidadMedida.SelectedIndex = 0;
        fieldDescripcionUnidadMedida.Text = "Seleccione o registre una unidad de medida";
        fieldAbreviaturaUnidadMedida.Text = "u";
        fieldTipoMateriaPrima.SelectedIndex = 0;
        fieldDescripcionTipoMateriaPrima.Text = "Seleccione o registre un tipo de materia prima";
        PrecioCompra = 0;
        PrecioVentaBase = 0;
        NombreAlmacen = string.Empty;
        fieldNombreAlmacen.SelectedIndex = 0;
        StockInicial = 0;
        ModoEdicionDatos = false;
    }
}
