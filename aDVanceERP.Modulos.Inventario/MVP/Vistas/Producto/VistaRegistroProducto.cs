using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto.Plantillas;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto;

public partial class VistaRegistroProducto : Form, IVistaRegistroProducto {
    private bool _modoEdicion;
    private int _paginaActual = 1;

    private VistaRegistroProductoP1 P1DatosGenerales = new VistaRegistroProductoP1();
    private VistaRegistroProductoP2 P2UmPreciosStock = new VistaRegistroProductoP2();

    public VistaRegistroProducto() {
        InitializeComponent();

        NombreVista = nameof(VistaRegistroProducto);

        InicializarVistas();
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

    public CategoriaProducto CategoriaProducto {
        get => P1DatosGenerales.CategoriaProducto;
        set => P1DatosGenerales.CategoriaProducto = value;
    }

    public string NombreProducto {
        get => P1DatosGenerales.Nombre;
        set => P1DatosGenerales.Nombre = value;
    }

    public string Codigo {
        get => P1DatosGenerales.Codigo;
        set => P1DatosGenerales.Codigo = value;
    }

    public string Descripcion {
        get => P1DatosGenerales.Descripcion;
        set => P1DatosGenerales.Descripcion = value;
    }

    public string RazonSocialProveedor {
        get => P1DatosGenerales.RazonSocialProveedor;
        set => P1DatosGenerales.RazonSocialProveedor = value;
    }

    public bool EsVendible {
        get => P1DatosGenerales.EsVendible;
        set => P1DatosGenerales.EsVendible = value;
    }

    public string UnidadMedida {
        get => P2UmPreciosStock.UnidadMedida;
        set => P2UmPreciosStock.UnidadMedida = value;
    }

    public string TipoMateriaPrima {
        get => P2UmPreciosStock.TipoMateriaPrima;
        set => P2UmPreciosStock.TipoMateriaPrima = value;
    }

    public decimal PrecioCompra {
        get => CategoriaProducto == CategoriaProducto.Mercancia || CategoriaProducto == CategoriaProducto.MateriaPrima 
            ? P2UmPreciosStock.PrecioCompra 
            : 0m;
        set {
            if (CategoriaProducto == CategoriaProducto.Mercancia || CategoriaProducto == CategoriaProducto.MateriaPrima)
                P2UmPreciosStock.PrecioCompra = value;
        }
    }

    public decimal CostoProduccionUnitario {
        get => CategoriaProducto == CategoriaProducto.ProductoTerminado
            ? P2UmPreciosStock.CostoProduccionUnitario
            : 0m;
        set {
            if (CategoriaProducto == CategoriaProducto.ProductoTerminado)
                P2UmPreciosStock.CostoProduccionUnitario = value;
        }
    }

    public decimal PrecioVentaBase {
        get => P2UmPreciosStock.PrecioVentaBase;
        set => P2UmPreciosStock.PrecioVentaBase = value;
    }

    public string? NombreAlmacen {
        get => P2UmPreciosStock.NombreAlmacen;
        set => P2UmPreciosStock.NombreAlmacen = value;
    }

    public decimal CantidadInicial {
        get => P2UmPreciosStock.StockInicial;
        set => P2UmPreciosStock.StockInicial = value;
    }

    public bool ModoEdicion {
        get => _modoEdicion;
        set {
            fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
            btnRegistrar.Text = value ? "Actualizar producto" : "Registrar producto";
            _modoEdicion = value;

            // Actualizar modo en páginas
            P2UmPreciosStock.ModoEdicionDatos = value;
        }
    }

    public event EventHandler? RegistrarUnidadMedida;
    public event EventHandler? RegistrarTipoMateriaPrima;
    public event EventHandler? RegistrarEntidad;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarUnidadMedida;
    public event EventHandler? EliminarTipoMateriaPrima;
    public event EventHandler? EliminarEntidad;
    

    private void InicializarVistas() {
        // 1. Datos generales del producto
        P1DatosGenerales.Dock = DockStyle.Fill;
        P1DatosGenerales.TopLevel = false;
        // 3. Unidad de medida, precios de compra y venta, cantidad inicial
        P2UmPreciosStock.Dock = DockStyle.Fill;
        P2UmPreciosStock.TopLevel = false;

        contenedorVistas.Controls.Clear();
        contenedorVistas.Controls.Add(P1DatosGenerales);
        contenedorVistas.Controls.Add(P2UmPreciosStock);

        // Mostrar vista de datos generales
        P1DatosGenerales.Show();
        P1DatosGenerales.BringToFront();
    }

    public void Inicializar() {
        // Eventos
        P1DatosGenerales.CategoriaProductoCambiada += delegate (object? sender, EventArgs args) {
            ActualizarVisibilidadCamposPrecios();

            P2UmPreciosStock.ConfigurarVisibilidadCamposTipoMateriaPrima(CategoriaProducto == CategoriaProducto.MateriaPrima);
        };
        P1DatosGenerales.EsVendibleActualizado += delegate (object? sender, EventArgs args) {
            ActualizarVisibilidadCamposPrecios();
        };
        P2UmPreciosStock.RegistrarUnidadMedida += delegate (object? sender, EventArgs args) {
            RegistrarUnidadMedida?.Invoke(sender, args);
        };
        P2UmPreciosStock.RegistrarTipoMateriaPrima += delegate (object? sender, EventArgs args) {
            RegistrarTipoMateriaPrima?.Invoke(sender, args);
        };
        P2UmPreciosStock.EliminarUnidadMedida += delegate (object? sender, EventArgs args) {
            EliminarUnidadMedida?.Invoke(sender, args);
        };
        P2UmPreciosStock.EliminarTipoMateriaPrima += delegate (object? sender, EventArgs args) {
            EliminarTipoMateriaPrima?.Invoke(sender, args);
        };
        btnAnterior.Click += delegate (object? sender, EventArgs args) {
            if (_paginaActual > 1)
                RetrocederPagina();
        };
        btnRegistrar.Click += delegate (object? sender, EventArgs args) {
            if (ModoEdicion)
                EditarEntidad?.Invoke(sender, args);
            else
                RegistrarEntidad?.Invoke(sender, args);
        };
        btnSiguiente.Click += delegate (object? sender, EventArgs args) {
            if (_paginaActual < 2)
                AvanzarPagina();
        };
        btnSalir.Click += delegate (object? sender, EventArgs args) {
            Close();
        };

        // Navegación
        ConfigurarParametrosBotonesNavegacion(false, true);
    }

    private void ActualizarVisibilidadCamposPrecios() {
        bool mostrarVenta = P1DatosGenerales.CategoriaProducto == CategoriaProducto.Mercancia ||
                            P1DatosGenerales.CategoriaProducto == CategoriaProducto.ProductoTerminado ||
                            P1DatosGenerales.CategoriaProducto == CategoriaProducto.MateriaPrima && P1DatosGenerales.EsVendible;

        // Limpiar el precios de venta segun visibilidad
        if (!mostrarVenta)
            P2UmPreciosStock.PrecioVentaBase = 0;

        P2UmPreciosStock.ConfigurarVisibilidadCamposPrecios(CategoriaProducto == CategoriaProducto.ProductoTerminado, mostrarVenta);
    }

    public void CargarNombresProductos(string[] nombresProductos) {
        P1DatosGenerales.CargarNombresProductos(nombresProductos);
    }

    public void CargarRazonesSocialesProveedores(object[] nombresProveedores) {
        P1DatosGenerales.CargarRazonesSocialesProveedores(nombresProveedores);
    }

    public void CargarUnidadesMedida((string[] nombres, string[] abreviaturas, string[] descripciones) unidadesMedida) {
        P2UmPreciosStock.CargarUnidadesMedida(unidadesMedida);
    }

    public void CargarTiposMateriaPrima(object[] nombresTiposMateriaPrima) {
        P2UmPreciosStock.CargarTiposMateriaPrima(nombresTiposMateriaPrima);
    }

    public void CargarDescripcionesTiposMateriaPrima(string[] descripcionesTiposMateriaPrima) {
        P2UmPreciosStock.CargarDescripcionesTiposMateriaPrima(descripcionesTiposMateriaPrima);
    }

    public void CargarNombresAlmacenes(object[] almacenes) {
        P2UmPreciosStock.CargarNombresAlmacenes(almacenes);
    }

    private void AvanzarPagina() {
        // Mapeo de navegación: página actual -> siguiente página
        var navegacion = new Dictionary<int, Action> {
            [1] = () => MostrarOcultarFormularios(P2UmPreciosStock, [P1DatosGenerales])
        };

        if (navegacion.TryGetValue(_paginaActual, out var action)) {
            action.Invoke();
            _paginaActual++;
        }

        ActualizarBotones();
    }

    private void RetrocederPagina() {
        // Mapeo de navegación: página actual -> página anterior
        var navegacion = new Dictionary<int, Action> {
            [2] = () => MostrarOcultarFormularios(P1DatosGenerales, [P2UmPreciosStock])
        };

        if (navegacion.TryGetValue(_paginaActual, out var action)) {
            action.Invoke();
            _paginaActual--;
        }

        ActualizarBotones();
    }

    private void ActualizarBotones() {
        var mostrarBotonAnterior = _paginaActual > 1;
        var mostrarBotonSiguiente = _paginaActual < 2;

        ConfigurarParametrosBotonesNavegacion(mostrarBotonAnterior, mostrarBotonSiguiente);
    }

    private void MostrarOcultarFormularios(Form formularioMostrar, Form[] formulariosOcultar) {
        foreach (var form in formulariosOcultar)
            form.Hide();

        formularioMostrar.Show();
        formularioMostrar.BringToFront();
    }

    public void ConfigurarParametrosBotonesNavegacion(bool mostrarAnterior, bool mostrarSiguiente) {
        // Ajustar visibilidad y ancho de columna para botón anterior
        btnAnterior.Visible = mostrarAnterior;        
        layoutNavegacion.ColumnStyles[0].Width = mostrarAnterior ? 50F : 0F;

        // Ajustar visibilidad y ancho de columna para botón siguiente
        btnSiguiente.Visible = mostrarSiguiente;
        layoutNavegacion.ColumnStyles[2].Width = mostrarSiguiente ? 50F : 0F;

        // Ajustar bordes del botón registrar
        btnRegistrar.CustomizableEdges = new Guna.UI2.WinForms.Suite.CustomizableEdges(
            !mostrarAnterior, 
            !mostrarSiguiente, 
            !mostrarAnterior, 
            !mostrarSiguiente
        );

        // Forzar el redibujado del layout
        layoutBotones.PerformLayout();
    }

    public void Mostrar() {
        BringToFront();
        ShowDialog();
    }

    public void Restaurar() {
        P1DatosGenerales.Restaurar();
        P2UmPreciosStock.Restaurar();

        ModoEdicion = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }
}