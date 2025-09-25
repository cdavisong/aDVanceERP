using System.Globalization;

using aDVanceERP.Core.Mensajes.MVP.Modelos;
using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Utiles;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.DetalleCompraventaProducto;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.DetalleCompraventaProducto.Plantillas;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Venta.Plantillas;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Venta;

public partial class VistaRegistroVenta : Form, IVistaRegistroVenta, IVistaGestionDetallesCompraventaProductos {
    private bool _modoEdicion;
    private bool _pagoEfectuado;
    private bool _mensajeriaConfigurada;
    private string? _tipoEntrega;
    private long _idTipoEntrega = 0;

    public VistaRegistroVenta() {
        InitializeComponent();

        NombreVista = nameof(VistaRegistroVenta);
        Productos = new List<string[]>();
        PanelCentral = new RepoVistaBase(contenedorVistas);

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

    public int AlturaContenedorVistas {
        get => contenedorVistas.Height;
    }

    public int TuplasMaximasContenedor {
        get => AlturaContenedorVistas / VariablesGlobales.AlturaTuplaPredeterminada;
    }

    public RepoVistaBase PanelCentral { get; private set; }

    public bool ModoEdicion {
        get => _modoEdicion;
        set {
            fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
            btnRegistrar.Text = value ? "Actualizar venta" : "Registrar venta";
            _modoEdicion = value;
        }
    }

    public DateTime Fecha {
        get => fieldFecha.Value;
        set => fieldFecha.Value = value;
    }

    public string? RazonSocialCliente { get; set; } = "Anónimo";

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
        set => fieldCantidad.Text = value.ToString("N2", CultureInfo.InvariantCulture);
    }

    public decimal Total {
        get => decimal.TryParse(fieldTotalVenta.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var total)
            ? total
            : 0;
        set => fieldTotalVenta.Text = value.ToString("N2", CultureInfo.InvariantCulture);
    }

    public long IdTipoEntrega {
        get => _idTipoEntrega;
        set {
            _idTipoEntrega = value;

            var idTipoEntregaPresencial = UtilesEntrega.ObtenerIdTipoEntrega("Presencial").Result;

            if (ModoEdicion && value.Equals(idTipoEntregaPresencial))
                btnAsignarMensajeria.Enabled = false;
        }
    }

    public string? Direccion { get; set; } = string.Empty;

    public bool PagoEfectuado {
        get => _pagoEfectuado;
        set {
            _pagoEfectuado = value;

            if (!ModoEdicion) {
                fieldNombreProducto.ReadOnly = value;
                fieldCantidad.ReadOnly = value;
                btnEfectuarPago.Enabled = !value;
                btnRegistrar.Enabled = value;
                btnAsignarMensajeria.Enabled = !value;
            }
        }
    }

    public bool MensajeriaConfigurada {
        get => _mensajeriaConfigurada;
        set {
            _mensajeriaConfigurada = value;

            if (!ModoEdicion) {
                if (TipoEntrega == "Mensajería (sin fondo)") {
                    fieldNombreProducto.ReadOnly = true;
                    fieldCantidad.ReadOnly = true;
                    btnEfectuarPago.Enabled = false;
                    btnRegistrar.Enabled = true;
                }

                btnAsignarMensajeria.Enabled = !value;
            }
        }
    }

    public string? TipoEntrega {
        get => _tipoEntrega;
        set {
            _tipoEntrega = value;

            IdTipoEntrega = UtilesEntrega.ObtenerIdTipoEntrega(value).Result;
        }
    }

    public string? EstadoEntrega { get; set; } = "Completada";

    public event EventHandler? AlturaContenedorTuplasModificada;
    public event EventHandler? ProductoAgregado;
    public event EventHandler? ProductoEliminado;
    public event EventHandler? EfectuarPago;
    public event EventHandler? AsignarMensajeria;
    public event EventHandler? RegistrarEntidad;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarEntidad;
    

    public void Inicializar() {
        // Eventos            
        btnCerrar.Click += delegate(object? sender, EventArgs args) {
            Close();
        };
        fieldNombreAlmacen.SelectedIndexChanged += async delegate {
            var idAlmacen = UtilesAlmacen.ObtenerIdAlmacen(NombreAlmacen).Result;

            CargarNombresProductos(await UtilesProducto.ObtenerNombresProductos(idAlmacen, "Todas", true));

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
        fieldCantidad.KeyDown += delegate(object? sender, KeyEventArgs args) {
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
        btnEfectuarPago.Click += delegate(object? sender, EventArgs args) {
            EfectuarPago?.Invoke(sender, args);
        };
        btnAsignarMensajeria.Click += delegate(object? sender, EventArgs args) {
            AsignarMensajeria?.Invoke(sender, args);
        };
        btnRegistrar.Click += delegate(object? sender, EventArgs args) {
            if (ModoEdicion)
                EditarEntidad?.Invoke(sender, args);
            else
                RegistrarEntidad?.Invoke(sender, args);
        };
        btnSalir.Click += delegate(object? sender, EventArgs args) {
            Close();
        };
        contenedorVistas.Resize += delegate {
            AlturaContenedorTuplasModificada?.Invoke(this, EventArgs.Empty);
        };
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
        
        if (adNombreProducto != null) {
            var idProducto = await UtilesProducto.ObtenerIdProducto(adNombreProducto);
            var adCantidad = string.IsNullOrEmpty(cantidad) ? Cantidad.ToString("N2", CultureInfo.InvariantCulture) : cantidad;
            var stockProducto = await UtilesProducto.ObtenerStockProducto(adNombreProducto, adNombreAlmacen);

            if (!ModoEdicion) {
                // Verificar ID y cantidad del producto
                if (idProducto == 0 || stockProducto == 0) {
                    CentroNotificaciones.Mostrar($"El producto {adNombreProducto} no existe o no tiene cantidad disponible en el almacén {adNombreAlmacen}. Rectifique los datos.", TipoNotificacion.Advertencia);

                    NombreProducto = string.Empty;

                    fieldCantidad.Text = string.Empty;
                    fieldNombreProducto.Focus();

                    return;
                }

                // Verificar que la cantidad no exceda el cantidad del producto
                if (Productos != null) {
                    var stockComprometido = Productos
                        .Where(a => a[0].Equals(idProducto.ToString()) && a[5].Equals(idAlmacen.ToString()))
                        .Sum(a => decimal.Parse(a[4], NumberStyles.Any, CultureInfo.InvariantCulture));
                    if (decimal.Parse(adCantidad, NumberStyles.Any, CultureInfo.InvariantCulture) + stockComprometido > stockProducto) {
                        fieldCantidad.ForeColor = Color.Firebrick;
                        fieldCantidad.Font = new Font(fieldCantidad.Font, FontStyle.Bold);
                        fieldCantidad.Margin = new Padding(3);

                        CentroNotificaciones.Mostrar($"La cantidad del producto {adNombreProducto} excede el cantidad disponible ({stockProducto}). Rectifique los datos o aumente la cantidad disponible en almacén.", TipoNotificacion.Advertencia);
                        return;
                    }

                    fieldCantidad.ForeColor = Color.Black;
                    fieldCantidad.Font = new Font(fieldCantidad.Font, FontStyle.Regular);
                    fieldCantidad.Margin = new Padding(3);
                }
            } else {
                fieldNombreProducto.ReadOnly = true;
                fieldCantidad.ReadOnly = true;
            }

            var categoriaProducto = await UtilesProducto.ObtenerCategoriaProducto(idProducto);
            var precioCompraVigenteProducto = categoriaProducto.Equals("ProductoTerminado") 
                ? await UtilesProducto.ObtenerCostoProduccionUnitario(idProducto) 
                : await UtilesProducto.ObtenerPrecioCompra(idProducto);
            var precioVentaBaseProducto = await UtilesProducto.ObtenerPrecioVentaBase(idProducto);
            var tuplaProducto = new[] {
                idProducto.ToString(),
                adNombreProducto,
                precioCompraVigenteProducto.ToString("N2", CultureInfo.InvariantCulture),
                precioVentaBaseProducto.ToString("N2", CultureInfo.InvariantCulture),
                adCantidad,
                idAlmacen.ToString()
            };

            // Verificar que el producto ya se encuentre registrado
            if (Productos != null) {
                var indiceProducto =
                    Productos.FindIndex(a => a[0].Equals(idProducto.ToString()) && a[5].Equals(idAlmacen.ToString()));
                if (indiceProducto != -1) {
                    Productos[indiceProducto][4] =
                        (decimal.Parse(Productos[indiceProducto][4], NumberStyles.Any, CultureInfo.InvariantCulture) + 
                         decimal.Parse(adCantidad, NumberStyles.Any, CultureInfo.InvariantCulture))
                         .ToString("N2", CultureInfo.InvariantCulture);
                } else {
                    Productos.Add(tuplaProducto);
                    ProductoAgregado?.Invoke(tuplaProducto, EventArgs.Empty);
                }
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

            tuplaDetallesVentaProducto.Indice = i;
            tuplaDetallesVentaProducto.IdProducto = producto[0];
            tuplaDetallesVentaProducto.NombreProducto = producto[1];
            tuplaDetallesVentaProducto.PrecioCompraventaFinal = producto[3];
            tuplaDetallesVentaProducto.Cantidad = producto[4];
            tuplaDetallesVentaProducto.Habilitada = !ModoEdicion;
            tuplaDetallesVentaProducto.PrecioCompraventaModificado += delegate (object? sender, EventArgs args) {
                if (sender is not IVistaTuplaDetalleCompraventaProducto vista)
                    return;

                var indiceProducto = Productos.FindIndex(a => a[0].Equals(vista.IdProducto));

                if (indiceProducto == -1)
                    return;

                Productos[indiceProducto][3] = vista.PrecioCompraventaFinal; // Actualizar precio de venta del producto

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

        if (Productos != null)
            foreach (var producto in Productos) {
                var cantidad = decimal.TryParse(producto[4], NumberStyles.Any, CultureInfo.InvariantCulture, 
                    out var cantProductos) 
                    ? cantProductos : 
                    0m;

                Total += decimal.TryParse(producto[3], NumberStyles.Any, CultureInfo.InvariantCulture,
                    out var precioVentaTotal)
                    ? precioVentaTotal * cantidad
                    : 0m;
            }

        btnEfectuarPago.Enabled = Total > 0;
        btnAsignarMensajeria.Enabled = Total > 0;
    }

    public void Mostrar() {
        Fecha = DateTime.Now;

        BringToFront();
        ShowDialog();
    }

    public void Restaurar() {
        Fecha = DateTime.Now;
        RazonSocialCliente = string.Empty;
        NombreAlmacen = string.Empty;
        fieldNombreAlmacen.SelectedIndex = 0;
        NombreProducto = string.Empty;
        fieldNombreProducto.AutoCompleteCustomSource.Clear();
        Total = 0;
        ModoEdicion = false;

        fieldNombreProducto.Focus();
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }
}