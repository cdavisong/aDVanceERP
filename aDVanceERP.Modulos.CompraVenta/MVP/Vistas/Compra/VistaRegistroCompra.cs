using System.Globalization;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Utiles;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Compra.Plantillas;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.DetalleCompraventaProducto;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.DetalleCompraventaProducto.Plantillas;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Compra;

public partial class VistaRegistroCompra : Form, IVistaRegistroCompra, IVistaGestionDetallesCompraventaProductos {
    private bool _modoEdicion;

    public VistaRegistroCompra() {
        InitializeComponent();

        NombreVista = nameof(VistaRegistroCompra);
        Productos = new List<string[]>();
        PanelCentral = new RepoVistaBase(contenedorVistas);

        Inicializar();
    }

    public string NombreVista {
        get => Name;
        private set => Name = value;
    }

    public DateTime Fecha {
        get => DateTime.Now;
        set { }
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
            btnRegistrar.Text = value ? "Actualizar venta" : "Registrar venta";
            _modoEdicion = value;
        }
    }

    public string? RazonSocialProveedor {
        get => fieldNombreProveedor.Text;
        set => fieldNombreProveedor.Text = value;
    }

    public string? NombreAlmacen {
        get => fieldNombreAlmacen.Text;
        set => fieldNombreAlmacen.Text = value;
    }

    public string? NombreProducto {
        get => fieldNombreProducto.Text;
        set => fieldNombreProducto.Text = value;
    }

    public List<string[]> Productos { get; private set; }

    public decimal Cantidad {
        get => decimal.TryParse(fieldCantidad.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var cantidad)
            ? cantidad
            : 0;
        set => fieldCantidad.Text = value > 0 ? value.ToString("N2", CultureInfo.InvariantCulture) : "N2";

    }

    public decimal Total {
        get => decimal.TryParse(fieldTotalCompra.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var total)
            ? total
            : 0;
        set => fieldTotalCompra.Text = value.ToString("N2", CultureInfo.InvariantCulture);
    }

    public event EventHandler? AlturaContenedorTuplasModificada;
    public event EventHandler? ProductoAgregado;
    public event EventHandler? ProductoEliminado;
    public event EventHandler? RegistrarEntidad;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarEntidad;


    public void Inicializar() {
        // Eventos
        btnCerrar.Click += delegate (object? sender, EventArgs args) {
            Close();
        };
        fieldNombreAlmacen.SelectedIndexChanged += async delegate {
            var idAlmacen = UtilesAlmacen.ObtenerIdAlmacen(NombreAlmacen).Result;

            CargarNombresProductos(await UtilesProducto.ObtenerNombresProductos(idAlmacen));

            fieldNombreProducto.Focus();
        };
        fieldCantidad.TextChanged += delegate {
            btnAdicionarProducto.Enabled = Cantidad > 0;
        };
        fieldNombreProducto.KeyDown += delegate (object? sender, KeyEventArgs args) {
            if (args.KeyCode != Keys.Enter)
                return;

            fieldCantidad.Focus();

            args.SuppressKeyPress = true;
        };
        fieldCantidad.KeyDown += delegate (object? sender, KeyEventArgs args) {
            if (args.KeyCode != Keys.Enter)
                return;

            AdicionarProducto();

            args.SuppressKeyPress = true;
        };
        btnAdicionarProducto.Click += delegate {
            AdicionarProducto();
        };
        ProductoEliminado += delegate {
            ActualizarTuplasProductos();
            ActualizarTotal();
        };
        btnRegistrar.Click += delegate (object? sender, EventArgs args) {
            if (ModoEdicion)
                EditarEntidad?.Invoke(sender, args);
            else
                RegistrarEntidad?.Invoke(sender, args);
        };
        btnSalir.Click += delegate (object? sender, EventArgs args) {
            Close();
        };
        contenedorVistas.Resize += delegate {
            AlturaContenedorTuplasModificada?.Invoke(this, EventArgs.Empty);
        };
    }

    public void CargarRazonesSocialesProveedores(object[] nombresProveedores) {
        fieldNombreProveedor.Items.Clear();
        fieldNombreProveedor.Items.Add("Anónimo");
        fieldNombreProveedor.Items.AddRange(nombresProveedores);
        fieldNombreProveedor.SelectedIndex = nombresProveedores.Length > 0 ? 0 : -1;
    }

    public void CargarNombresAlmacenes(object[] nombresAlmacenes) {
        fieldNombreAlmacen.Items.Clear();
        fieldNombreAlmacen.Items.AddRange(nombresAlmacenes);
        fieldNombreAlmacen.SelectedIndex = nombresAlmacenes.Length > 0 ? 0 : -1;
    }

    public void CargarNombresProductos(string[] nombresProductos) {
        fieldNombreProducto.AutoCompleteCustomSource.Clear();
        fieldNombreProducto.AutoCompleteCustomSource.AddRange(nombresProductos);
        fieldNombreProducto.AutoCompleteMode = AutoCompleteMode.Suggest;
        fieldNombreProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;
    }

    public async void AdicionarProducto(string nombreAlmacen = "", string nombreProducto = "", string cantidad = "") {
        var adNombreAlmacen = string.IsNullOrEmpty(nombreAlmacen) ? NombreAlmacen : nombreAlmacen;
        var idAlmacen = await UtilesAlmacen.ObtenerIdAlmacen(adNombreAlmacen);
        var adNombreProducto = string.IsNullOrEmpty(nombreProducto) ? NombreProducto : nombreProducto;
        var idProducto = await UtilesProducto.ObtenerIdProducto(adNombreProducto);
        var adCantidad = string.IsNullOrEmpty(cantidad) ? Cantidad.ToString("N2", CultureInfo.InvariantCulture) : cantidad;

        if (!ModoEdicion) {
            // Verificar ID del producto
            if (idProducto == 0) {
                NombreProducto = string.Empty;

                fieldCantidad.Text = string.Empty;
                fieldNombreProducto.Focus();

                return;
            }
        } else {
            fieldNombreProducto.ReadOnly = true;
            fieldCantidad.ReadOnly = true;
        }

        var precioCompraBaseProducto = await UtilesProducto.ObtenerPrecioCompra(idProducto);
        var tuplaProducto = new[] {
            idProducto.ToString(),
            adNombreProducto,
            precioCompraBaseProducto.ToString("N2", CultureInfo.InvariantCulture),
            adCantidad,
            idAlmacen.ToString()
        };

        // Verificar que el producto ya se encuentre registrado
        if (Productos != null) {
            var indiceProducto =
                Productos.FindIndex(a => a[0].Equals(idProducto.ToString()) && a[4].Equals(idAlmacen.ToString()));
            if (indiceProducto != -1) {
                Productos[indiceProducto][3] =
                    (decimal.Parse(Productos[indiceProducto][3], NumberStyles.Any, CultureInfo.InvariantCulture) +
                     decimal.Parse(adCantidad, NumberStyles.Any, CultureInfo.InvariantCulture))
                     .ToString("N2", CultureInfo.InvariantCulture);
            } else {
                Productos.Add(tuplaProducto);
                ProductoAgregado?.Invoke(tuplaProducto, EventArgs.Empty);
            }
        }

        NombreProducto = string.Empty;
        Cantidad = 0;

        ActualizarTuplasProductos();
        ActualizarTotal();

        fieldNombreProducto.Focus();
    }

    private void ActualizarTuplasProductos() {
        PanelCentral.CerrarTodos();

        // Restablecer útima coordenada Y de la tupla
        VariablesGlobales.CoordenadaYUltimaTupla = 0;

        for (var i = 0; i < Productos?.Count; i++) {
            var producto = Productos[i];
            var tuplaDetallesVentaProducto = new VistaTuplaDetalleCompraventaProducto();

            tuplaDetallesVentaProducto.IdProducto = producto[0];
            tuplaDetallesVentaProducto.NombreProducto = producto[1];
            tuplaDetallesVentaProducto.PrecioCompraventaFinal = producto[2];
            tuplaDetallesVentaProducto.Cantidad = producto[3];
            tuplaDetallesVentaProducto.Habilitada = !ModoEdicion;
            tuplaDetallesVentaProducto.PrecioCompraventaModificado += delegate (object? sender, EventArgs args) {
                if (sender is not IVistaTuplaDetalleCompraventaProducto vista)
                    return;

                var indiceProducto = Productos.FindIndex(a => a[0].Equals(vista.IdProducto));

                if (indiceProducto == -1)
                    return;

                Productos[indiceProducto][2] = vista.PrecioCompraventaFinal; // Actualizar precio de compra

                ActualizarTotal();
            };
            tuplaDetallesVentaProducto.EliminarDatosTupla += delegate (object? sender, EventArgs args) {
                producto = sender as string[];

                Productos.RemoveAt(Productos.FindIndex(p => p[0].Equals(producto?[0])));
                ProductoEliminado?.Invoke(producto, args);
            };

            // Registro y muestra
            PanelCentral?.Registrar(
                tuplaDetallesVentaProducto,
                new Point(0, VariablesGlobales.CoordenadaYUltimaTupla),
                new Size(contenedorVistas.Width - 20, VariablesGlobales.AlturaTuplaPredeterminada), 
                TipoRedimensionadoVista.Ninguno);
            tuplaDetallesVentaProducto.Mostrar();

            // Incremento de la útima coordenada Y de la tupla
            VariablesGlobales.CoordenadaYUltimaTupla += VariablesGlobales.AlturaTuplaPredeterminada;
        }
    }

    private void ActualizarTotal() {
        Total = 0;

        if (Productos == null)
            return;

        foreach (var producto in Productos) {
            var cantidad = decimal.TryParse(producto[3], NumberStyles.Any, CultureInfo.InvariantCulture,
                out var cantProductos)
                ? cantProductos
                : 0;

            Total += decimal.TryParse(producto[2], NumberStyles.Any, CultureInfo.InvariantCulture,
                out var precioCompraTotal)
                ? precioCompraTotal * cantidad
                : 0;
        }
    }

    public void Mostrar() {
        BringToFront();
        ShowDialog();
    }

    public void Restaurar() {
        Fecha = DateTime.Now;
        RazonSocialProveedor = string.Empty;
        fieldNombreProveedor.SelectedIndex = 0;
        NombreAlmacen = string.Empty;
        fieldNombreAlmacen.SelectedIndex = 0;
        fieldNombreProducto.AutoCompleteCustomSource.Clear();
        Total = 0;
        ModoEdicion = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }
}