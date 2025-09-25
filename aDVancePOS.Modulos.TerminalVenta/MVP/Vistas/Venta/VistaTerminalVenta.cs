using System.Globalization;

using aDVanceERP.Core.Mensajes.MVP.Modelos;
using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.MVP.Modelos.Repositorios;
using aDVanceERP.Core.MVP.Modelos.Repositorios.Plantillas;
using aDVanceERP.Core.Utiles;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.DetalleCompraventaProducto.Plantillas;

using aDVancePOS.Modulos.TerminalVenta.MVP.Vistas.Venta.Plantillas;

namespace aDVancePOS.Modulos.TerminalVenta.MVP.Vistas.Venta {
    public partial class VistaTerminalVenta : Form, IVistaTerminalVenta, IVistaGestionDetallesCompraventaProductos {
        private bool _pagoEfectuado;
        private bool _mensajeriaConfigurada;
        private string? _tipoEntrega;
        private long _idTipoEntrega = 0;
        private float _cantidad = 0;

        public VistaTerminalVenta() {
            InitializeComponent();
            Inicializar();
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

        public DateTime Fecha {
            get => DateTime.Now;
            set { }
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

        public List<string[]>? Productos { get; private set; }

        public float Cantidad {
            get => _cantidad;
            set {
                _cantidad = value;

                btnCantidadProducto.Text = $@"Cantidad ({_cantidad.ToString("0.00", CultureInfo.InvariantCulture)})";
            }
        }

        public decimal Subtotal {
            get => decimal.TryParse(fieldSubtotal.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var total)
               ? total
               : 0;
            set => fieldSubtotal.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public decimal Descuento {
            get => decimal.TryParse(fieldDescuento.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var total)
               ? total
               : 0;
            set => fieldSubtotal.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public decimal Total {
            get => decimal.TryParse(fieldTotal.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var total)
                ? total
                : 0;
            set => fieldTotal.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public long IdTipoEntrega {
            get => _idTipoEntrega;
            set {
                _idTipoEntrega = value;

                var idTipoEntregaPresencial = UtilesEntrega.ObtenerIdTipoEntrega("Presencial").Result;

                if (ModoEdicionDatos && value.Equals(idTipoEntregaPresencial))
                    btnAsignarMensajeria.Enabled = false;
            }
        }

        public string? Direccion { get; set; } = string.Empty;

        public bool PagoEfectuado {
            get => _pagoEfectuado;
            set {
                _pagoEfectuado = value;
            }
        }

        public bool MensajeriaConfigurada {
            get => _mensajeriaConfigurada;
            set {
                _mensajeriaConfigurada = value;
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

        public int AlturaContenedorVistas {
            get => contenedorVistas.Height;
        }

        public int TuplasMaximasContenedor {
            get => AlturaContenedorVistas / VariablesGlobales.AlturaTuplaPredeterminada;
        }

        public IRepositorioVista? Vistas { get; private set; }

        public bool ModoEdicionDatos { get; set; }

        public event EventHandler? AlturaContenedorTuplasModificada;
        public event EventHandler? RegistrarDatos;
        public event EventHandler? EditarDatos;
        public event EventHandler? EliminarDatos;
        public event EventHandler? ProductoAgregado;
        public event EventHandler? ProductoEliminado;
        public event EventHandler? ModificarCantidadProductos;
        public event EventHandler? EfectuarPago;
        public event EventHandler? AsignarMensajeria;
        public event EventHandler? Salir;

        public void Inicializar() {
            Productos = new List<string[]>();
            Vistas = new RepositorioVistaBase(contenedorVistas);

            // Eventos
            fieldNombreAlmacen.SelectedIndexChanged += async delegate {
                var idAlmacen = UtilesAlmacen.ObtenerIdAlmacen(NombreAlmacen).Result;

                CargarNombresProductos(await UtilesProducto.ObtenerNombresProductos(idAlmacen));

                fieldNombreProducto.Focus();
            };
            fieldNombreProducto.KeyDown += delegate (object? sender, KeyEventArgs args) {
                switch (args.KeyCode) {
                    case Keys.Enter:
                        AdicionarProducto();

                        args.SuppressKeyPress = true;
                        break;
                    case Keys.F3:
                        ModificarCantidadProductos?.Invoke(sender, args);

                        args.SuppressKeyPress = true;
                        break;
                }

                fieldNombreProducto.Focus();
            };
            btnAdicionarProducto.Click += delegate {
                AdicionarProducto();
            };
            ProductoEliminado += delegate {
                ActualizarTuplasProductos();
                ActualizarSubtotal();

                fieldNombreProducto.Focus();
            };
            btnEliminarProductos.Click += delegate (object? sender, EventArgs args) {
                EliminarProductosVenta(sender, args);

                fieldNombreProducto.Focus();
            };
            btnCantidadProducto.Click += delegate (object? sender, EventArgs args) {
                ModificarCantidadProductos?.Invoke(sender, args);

                fieldNombreProducto.Focus();
            };
            btnGestionarPago.Click += delegate (object? sender, EventArgs args) {
                EfectuarPago?.Invoke(sender, args);
                RegistrarDatos?.Invoke(sender, args);

                EliminarProductosVenta(sender, args);

                fieldNombreProducto.Focus();
            };
            btnAsignarMensajeria.Click += delegate (object? sender, EventArgs args) {
                AsignarMensajeria?.Invoke(sender, args);
                RegistrarDatos?.Invoke(sender, args);

                EliminarProductosVenta(sender, args);

                fieldNombreProducto.Focus();
            };
            contenedorVistas.Resize += delegate {
                AlturaContenedorTuplasModificada?.Invoke(this, EventArgs.Empty);
            };

            // Enlace de scanner
            UtilesServidorScanner.Servidor.DatosRecibidos += ProcesarDatosScanner;
        }

        private void EliminarProductosVenta(object? sender, EventArgs args) {
            Productos?.Clear();
            ProductoEliminado?.Invoke(sender, args);
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

        private void ProcesarDatosScanner(string codigo) {
            var nombreProducto = UtilesProducto.ObtenerNombreProducto(codigo.Replace("\0", "")).Result;

            if (string.IsNullOrEmpty(nombreProducto))
                return;

            Invoke((MethodInvoker) delegate {
                NombreProducto = nombreProducto;

                AdicionarProducto();
            });
        }

        public async void AdicionarProducto(string nombreAlmacen = "", string nombreProducto = "", string cantidad = "") {
            var adNombreAlmacen = string.IsNullOrEmpty(nombreAlmacen) ? NombreAlmacen : nombreAlmacen;
            var idAlmacen = await UtilesAlmacen.ObtenerIdAlmacen(adNombreAlmacen);
            var adNombreProducto = string.IsNullOrEmpty(nombreProducto) ? NombreProducto : nombreProducto;

            if (adNombreProducto != null) {
                var idProducto = await UtilesProducto.ObtenerIdProducto(adNombreProducto);
                var adCantidad = string.IsNullOrEmpty(cantidad) ? Cantidad.ToString("0.00", CultureInfo.InvariantCulture) : cantidad;
                var stockProducto = await UtilesProducto.ObtenerStockProducto(adNombreProducto, adNombreAlmacen);

                // Verificar ID y stock del producto
                if (idProducto == 0 || stockProducto == 0) {
                    CentroNotificaciones.Mostrar($"El producto {adNombreProducto} no existe o no tiene stock disponible en el almacén {adNombreAlmacen}. Rectifique los datos.", TipoNotificacion.Advertencia);

                    NombreProducto = string.Empty;

                    fieldNombreProducto.Focus();

                    return;
                }

                // Verificar que la cantidad no exceda el stock del producto
                if (Productos != null) {
                    var stockComprometido = Productos
                        .Where(a => a[0].Equals(idProducto.ToString()) && a[5].Equals(idAlmacen.ToString()))
                        .Sum(a => decimal.Parse(a[4], NumberStyles.Any, CultureInfo.InvariantCulture));
                    if (decimal.Parse(adCantidad, NumberStyles.Any, CultureInfo.InvariantCulture) + stockComprometido > stockProducto) {
                        btnCantidadProducto.ForeColor = Color.Firebrick;
                        btnCantidadProducto.Font = new Font(btnCantidadProducto.Font, FontStyle.Bold);
                        btnCantidadProducto.Margin = new Padding(3);

                        CentroNotificaciones.Mostrar($"La cantidad del producto {adNombreProducto} excede el stock disponible ({stockProducto}). Rectifique los datos o aumente la cantidad disponible en almacén.", TipoNotificacion.Advertencia);
                        return;
                    }

                    btnCantidadProducto.ForeColor = Color.Black;
                    btnCantidadProducto.Font = new Font(btnCantidadProducto.Font, FontStyle.Regular);
                    btnCantidadProducto.Margin = new Padding(3);
                }

                var precioCompraVigenteProducto = await UtilesProducto.ObtenerPrecioCompra(idProducto);
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
            Cantidad = 1;

            ActualizarTuplasProductos();
            ActualizarSubtotal();

            fieldNombreProducto.Focus();
        }

        private void ActualizarTuplasProductos() {
            foreach (var tupla in contenedorVistas.Controls)
                if (tupla is IVistaTuplaDetalleCompraventaProducto vistaTupla)
                    vistaTupla.Cerrar();
            contenedorVistas.Controls.Clear();

            // Restablecer útima coordenada Y de la tupla
            VariablesGlobales.CoordenadaYUltimaTupla = 0;

            for (var i = 0; i < Productos?.Count; i++) {
                var producto = Productos[i];
                var tuplaDetallesVentaProducto = new VistaTuplaVentaProducto();
                var precioVentaFinal = decimal.TryParse(
                    producto[3], NumberStyles.Any, CultureInfo.InvariantCulture,
                    out var pvf) ? pvf : 0;
                var cantidad = decimal.TryParse(
                    producto[4],
                    NumberStyles.Any, CultureInfo.InvariantCulture,
                    out var c) ? c : 0;

                tuplaDetallesVentaProducto.IdProducto = producto[0];
                tuplaDetallesVentaProducto.NombreProducto = producto[1];
                tuplaDetallesVentaProducto.PrecioVentaFinal = precioVentaFinal.ToString("N", CultureInfo.InvariantCulture);
                tuplaDetallesVentaProducto.Cantidad = cantidad.ToString("N", CultureInfo.InvariantCulture);
                tuplaDetallesVentaProducto.Subtotal = (precioVentaFinal * cantidad).ToString("N", CultureInfo.InvariantCulture);
                tuplaDetallesVentaProducto.PrecioVentaModificado += delegate (object? sender, EventArgs args) {
                    if (sender is not IVistaTuplaVentaProducto vista)
                        return;

                    var indiceProducto = Productos.FindIndex(a => a[0].Equals(vista.IdProducto));

                    if (indiceProducto == -1)
                        return;

                    Productos[indiceProducto][3] = vista.PrecioVentaFinal; // Actualizar precio de venta del producto

                    ActualizarSubtotal();
                };
                tuplaDetallesVentaProducto.EliminarDatosTupla += delegate (object? sender, EventArgs args) {
                    producto = sender as string[];

                    Productos.RemoveAt(Productos.FindIndex(p => p[0].Equals(producto?[0])));
                    ProductoEliminado?.Invoke(producto, args);
                };

                // Registro y muestra
                Vistas?.Registrar(
                    $"vistaTupla{tuplaDetallesVentaProducto.GetType().Name}{i}",
                    tuplaDetallesVentaProducto,
                    new Point(0, VariablesGlobales.CoordenadaYUltimaTupla),
                    new Size(contenedorVistas.Width - 20, VariablesGlobales.AlturaTuplaPredeterminada), "N");
                tuplaDetallesVentaProducto.Mostrar();

                // Incremento de la útima coordenada Y de la tupla
                VariablesGlobales.CoordenadaYUltimaTupla += VariablesGlobales.AlturaTuplaPredeterminada;
            }
        }

        private void ActualizarSubtotal() {
            Subtotal = 0;

            if (Productos != null)
                foreach (var producto in Productos) {
                    var cantidad = decimal.TryParse(producto[4], NumberStyles.Any, CultureInfo.InvariantCulture, 
                        out var cantProductos) 
                        ? cantProductos : 
                        0m;

                    Subtotal += decimal.TryParse(producto[3], NumberStyles.Any, CultureInfo.InvariantCulture,
                        out var precioVentaTotal)
                        ? precioVentaTotal * cantidad
                        : 0m;
                }

            btnGestionarPago.Enabled = Subtotal > 0;
            KeyPago.Visible = Subtotal > 0;
            btnAsignarMensajeria.Enabled = Subtotal > 0;

            ActualizarTotal();
        }

        private void ActualizarTotal() {
            Total = Subtotal - Descuento;
        }

        public void Mostrar() {
            Habilitada = true;
            BringToFront();
            Show();

            fieldNombreProducto.Focus();
        }

        public void Restaurar() {
            Habilitada = true;
            Fecha = DateTime.Now;
            NombreAlmacen = string.Empty;
            fieldNombreAlmacen.SelectedIndex = 0;
            NombreProducto = string.Empty;
            btnGestionarPago.Enabled = false;
            KeyPago.Visible = false;
            btnAsignarMensajeria.Enabled = false;
            Cantidad = 1;
            Subtotal = 0;
            Descuento = 0;
            Total = 0;
        }

        public void Ocultar() {
            Habilitada = false;
            Hide();
        }

        public void Cerrar() {
            UtilesServidorScanner.Servidor.DatosRecibidos -= ProcesarDatosScanner; // Mover a FormCLosing
        }
    }
}
