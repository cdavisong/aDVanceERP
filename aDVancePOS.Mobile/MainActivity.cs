using aDVancePOS.Mobile.Modelos;
using aDVancePOS.Mobile.Servicios;
using aDVancePOS.Mobile.Adaptadores;

using Android.Content.PM;
using Android.Views;

using System.Globalization;

namespace aDVancePOS.Mobile {
    [Activity(
        Label = "@string/app_name",
        MainLauncher = true,
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Activity {
        private ServicioDatos _servicioDatos;
        private List<Producto> _productosEncontrados;
        private List<ProductoVendido> _carrito;
        private ProductoAdapter _productosAdapter;
        private ProductoVendidoAdapter _carritoAdapter;

        private EditText _txtBuscar;
        private ListView _lstProductos;
        private ListView _lstCarrito;
        private TextView _lblTotal;
        private Button _btnPagarEfectivo;
        private Button _btnPagarTransferencia;
        private ImageButton _btnImportar;
        private Button _btnCerrarVenta;

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Configuración inicial de la ventana
            RequestWindowFeature(WindowFeatures.NoTitle);
            Window?.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

            SetContentView(Resource.Layout.activity_main);

            //CheckPermissions();            
            InicializarControlesUI();
            InicializarDatos();
            ActualizarEstadisticasVentas();
        }

        private void InicializarControlesUI() {
            _txtBuscar = FindViewById<EditText>(Resource.Id.txtBuscar);
            _lstProductos = FindViewById<ListView>(Resource.Id.lstProductos);
            _lstCarrito = FindViewById<ListView>(Resource.Id.lstCarrito);
            _lblTotal = FindViewById<TextView>(Resource.Id.lblTotal);
            _btnPagarEfectivo = FindViewById<Button>(Resource.Id.btnPagarEfectivo);
            _btnPagarTransferencia = FindViewById<Button>(Resource.Id.btnPagarTransferencia);
            _btnImportar = FindViewById<ImageButton>(Resource.Id.btnImportar);
            _btnCerrarVenta = FindViewById<Button>(Resource.Id.btnCierreVenta);

            // Configurar adaptadores y eventos
            ConfigurarAdaptadores();
            ConfigurarEventos();

            // Acciones UI
            OcultarInterfazSistema();
        }

        private void ConfigurarAdaptadores() {
            _productosEncontrados = new List<Producto>();
            _productosAdapter = new ProductoAdapter(this, _productosEncontrados);
            _lstProductos.Adapter = _productosAdapter;

            _carrito = new List<ProductoVendido>();
            _carritoAdapter = new ProductoVendidoAdapter(this, _carrito);
            _lstCarrito.Adapter = _carritoAdapter;
        }

        private void ConfigurarEventos() {
            var downloadsPath = ServicioDatos.ObtenerRutaArchivosInterna();
            var filePath = Path.Combine(downloadsPath, "productos_almacen.json");

            _txtBuscar.TextChanged += (sender, e) => BuscarProductos();
            _lstProductos.ItemClick += (sender, e) => {
                AgregarAlCarrito(e.Position);
            };
            _lstCarrito.ItemLongClick += (sender, e) => {
                var productoVendido = _carrito[e.Position];

                new AlertDialog.Builder(this)
                    .SetTitle("Eliminar producto")
                    .SetMessage($"¿Desea eliminar {productoVendido.Producto.nombre} del carrito?")
                    .SetPositiveButton("Eliminar", (sender, args) => {
                        _carrito.RemoveAt(e.Position);
                        ActualizarCarrito();
                        Toast.MakeText(this, "Producto eliminado", ToastLength.Short).Show();
                    })
                    .SetNegativeButton("Cancelar", (sender, e) => { })
                    .Show();
            };
            _btnPagarEfectivo.Click += (sender, e) => RealizarPago("Efectivo");
            _btnPagarTransferencia.Click += (sender, e) => RealizarPago("Transferencia");
            _btnImportar.Click += (sender, e) => {
                ImportarProductos(filePath);
                BuscarProductos();
            };
            _btnCerrarVenta.Click += (sender, e) => RealizarCierreVenta();
        }

        private void OcultarInterfazSistema() {
            Window.InsetsController?.Hide(WindowInsets.Type.SystemBars());
            Window.SetDecorFitsSystemWindows(false);
        }
        
        private void InicializarDatos() {
            // Inicializar servicios
            _servicioDatos = new ServicioDatos();

            CargarDatosIniciales();
        }

        private void CargarDatosIniciales() {
            try {
                var downloadsPath = _servicioDatos.DirectorioDescargas;
                var filePath = Path.Combine(downloadsPath, "productos_almacen.json");

                if (File.Exists(filePath))
                    ImportarProductos(filePath);
                BuscarProductos();
            } catch (Exception ex) {
                Toast.MakeText(this, $"Error cargando datos iniciales: {ex.Message}", ToastLength.Short).Show();
            }
        }

        private void ImportarProductos(string filePath) {
            try {
                if (File.Exists(filePath)) {
                    var jsonContent = File.ReadAllText(filePath);

                    // Copiar el archivo a la ubicación interna de la app
                    File.WriteAllText(_servicioDatos.ProductosPath, jsonContent);

                    // Opcional: mover el archivo a la carpeta de importados
                    var importDir = Path.Combine(_servicioDatos.DirectorioDescargas, "imported");
                    Directory.CreateDirectory(importDir);
                    var destPath = Path.Combine(importDir, Path.GetFileName(filePath));
                    File.Move(filePath, destPath, true);

                    _servicioDatos = new ServicioDatos(); // Reiniciar servicio

                    Toast.MakeText(this, "Productos importados correctamente", ToastLength.Long).Show();
                } else {
                    Toast.MakeText(this, "Archivo no encontrado", ToastLength.Long).Show();
                }
            } catch (Exception ex) {
                Toast.MakeText(this, $"Error al importar: {ex.Message}", ToastLength.Long).Show();
            }
        }

        private void ExportarDatos() {
            var downloadsPath = _servicioDatos.DirectorioDescargas;
            Directory.CreateDirectory(downloadsPath);

            var exportTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var productosExportPath = Path.Combine(downloadsPath, $"productos_export_{exportTime}.json");
            var ventasExportPath = Path.Combine(downloadsPath, $"ventas_export_{exportTime}.json");

            File.Copy(_servicioDatos.ProductosPath, productosExportPath, true);
            File.Copy(_servicioDatos.VentasPath, ventasExportPath, true);

            Toast.MakeText(this, $"Datos exportados", ToastLength.Long).Show();
        }

        private void BuscarProductos() {
            var termino = _txtBuscar.Text;
            _productosEncontrados = _servicioDatos.BuscarProductos(termino);
            _productosAdapter.Clear();
            _productosAdapter.AddAll(_productosEncontrados);
            _productosAdapter.NotifyDataSetChanged();
        }

        private void AgregarAlCarrito(int position) {
            var producto = _productosEncontrados[position];

            if (producto.cantidad <= 0) {
                Toast.MakeText(this, "Producto sin stock disponible", ToastLength.Short).Show();
                return;
            }

            // Mostrar diálogo para cantidad
            var input = new EditText(this);
            input.InputType = Android.Text.InputTypes.NumberFlagDecimal;
            input.Text = "1";

            new AlertDialog.Builder(this)
                .SetTitle($"Agregar {producto.nombre}")
                .SetMessage("Ingrese la cantidad:")
                .SetView(input)
                .SetPositiveButton("Agregar", (sender, e) => {
                    if (decimal.TryParse(input.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal cantidad) && cantidad > 0) {
                        // Validar cantidad contra stock
                        if (cantidad > producto.cantidad) {
                            Toast.MakeText(this, $"Cantidad excede stock disponible ({producto.cantidad:N2})", ToastLength.Short).Show();
                            return;
                        }

                        // Verificar si ya está en el carrito
                        var itemExistente = _carrito.FirstOrDefault(p => p.Producto.id_producto == producto.id_producto);
                        if (itemExistente != null) {
                            itemExistente.Cantidad += cantidad;
                        } else {
                            _carrito.Add(new ProductoVendido {
                                Producto = producto,
                                Cantidad = cantidad
                            });
                        }

                        ActualizarCarrito();
                    }
                })
                .SetNegativeButton("Cancelar", (sender, e) => { })
                .Show();
        }

        private void ActualizarCarrito() {
            _carritoAdapter.Clear();
            _carritoAdapter.AddAll(_carrito);
            _carritoAdapter.NotifyDataSetChanged();

            var total = _carrito.Sum(item => item.Producto.precio_venta_base * item.Cantidad);
            _lblTotal.Text = $"Total: ${total:N2}";
        }

        private void RealizarPago(string metodoPago) {
            if (_carrito.Count == 0) {
                Toast.MakeText(this, "El carrito está vacío", ToastLength.Short).Show();
                return;
            }

            var total = _carrito.Sum(item => item.Producto.precio_venta_base * item.Cantidad);

            new AlertDialog.Builder(this)
                .SetTitle("Confirmar pago")
                .SetMessage($"Total: ${total:N2}\nMétodo: {metodoPago}\n¿Confirmar venta?")
                .SetPositiveButton("Confirmar", (sender, e) => {
                    var venta = new Venta {
                        Productos = [.. _carrito],
                        Total = total,
                        MetodoPago = metodoPago
                    };

                    _servicioDatos.RegistrarVenta(venta);
                    _carrito.Clear();
                    ActualizarCarrito();
                    BuscarProductos();
                    ActualizarEstadisticasVentas();
                    Toast.MakeText(this, $"Venta registrada ({metodoPago})", ToastLength.Long).Show();
                })
                .SetNegativeButton("Cancelar", (sender, e) => {
                    _carrito.Clear();
                    ActualizarCarrito();
                    Toast.MakeText(this, "Venta cancelada", ToastLength.Short).Show();
                })
                .Show();
        }

        private void ActualizarEstadisticasVentas() {
            var (efectivo, transferencia, total) = _servicioDatos.ObtenerEstadisticasVentas();

            FindViewById<TextView>(Resource.Id.lblVentasEfectivo).Text = $"Efectivo: ${efectivo:N2}";
            FindViewById<TextView>(Resource.Id.lblVentasTransferencia).Text = $"Transferencias: ${transferencia:N2}";
            FindViewById<TextView>(Resource.Id.lblVentasTotal).Text = $"Total acumulado: ${total:N2}";
        }

        private void RealizarCierreVenta() {
            if (_servicioDatos.ObtenerTotalVentasAcumuladas() <= 0) {
                Toast.MakeText(this, "No hay ventas para cerrar", ToastLength.Short).Show();
                return;
            }

            new AlertDialog.Builder(this)
                .SetTitle("Cierre de venta")
                .SetMessage("¿Desea realizar el cierre de venta?\nSe exportarán los datos y se reiniciarán los acumulados.")
                .SetPositiveButton("Confirmar", (sender, e) => {
                    ExportarDatos();
                    _servicioDatos.LimpiarVentas();
                    ActualizarEstadisticasVentas();
                    Toast.MakeText(this, "Cierre de venta realizado", ToastLength.Long).Show();
                })
                .SetNegativeButton("Cancelar", (sender, e) => { })
                .Show();
        }
    }
}