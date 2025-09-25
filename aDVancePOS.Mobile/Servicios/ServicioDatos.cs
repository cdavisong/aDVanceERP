using aDVancePOS.Mobile.Modelos;

using Android.OS;

using Newtonsoft.Json;

using System.Xml;

namespace aDVancePOS.Mobile.Servicios {
    public class ServicioDatos {
        private List<Producto> _productos;
        private List<Venta> _ventas;
        
        public ServicioDatos() {
            if (!Directory.Exists(ObtenerRutaArchivosInterna()))
                Directory.CreateDirectory(ObtenerRutaArchivosInterna());

            DirectorioDescargas = ObtenerRutaArchivosInterna() ?? "";
            ProductosPath = Path.Combine(DirectorioDescargas, "productos.json");
            VentasPath = Path.Combine(DirectorioDescargas, "ventas.json");

            if (!File.Exists(ProductosPath)) {
                File.WriteAllText(ProductosPath, "[]");
            }

            if (!File.Exists(VentasPath)) {
                File.WriteAllText(VentasPath, "[]");
            }

            CargarProductos();
            CargarVentas();
        }

        public string DirectorioDescargas { get; }

        public string ProductosPath { get; }

        public string VentasPath { get; }

        public static string? ObtenerRutaArchivosInterna() {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Q) {
                // Directorio de medios externos específico de la aplicación
                return Application.Context.GetExternalMediaDirs().FirstOrDefault()?.AbsolutePath;
            } else {
                // Directorio público de medios (por ejemplo, Descargas)
                return Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryDownloads)?.AbsolutePath;
            }
        }

        private void CargarProductos() {
            if (File.Exists(ProductosPath)) {
                var json = File.ReadAllText(ProductosPath);
                _productos = JsonConvert.DeserializeObject<List<Producto>>(json);
            } else {
                _productos = new List<Producto>();
            }
        }

        private void CargarVentas() {
            if (File.Exists(VentasPath)) {
                var json = File.ReadAllText(VentasPath);
                _ventas = JsonConvert.DeserializeObject<List<Venta>>(json);
            } else {
                _ventas = new List<Venta>();
            }
        }

        public List<Producto> BuscarProductos(string termino) {
            return _productos.Where(p =>
                p.nombre.Contains(termino, StringComparison.OrdinalIgnoreCase) ||
                p.codigo.Contains(termino, StringComparison.OrdinalIgnoreCase) &&
                p.cantidad > 0).ToList();
        }

        public Producto ObtenerProductoPorId(int id) {
            return _productos.FirstOrDefault(p => p.id_producto == id);
        }

        public void RegistrarVenta(Venta venta) {
            // Actualizar stock
            foreach (var item in venta.Productos) {
                var producto = _productos.FirstOrDefault(p => p.id_producto == item.Producto.id_producto);
                if (producto != null) {
                    producto.cantidad -= item.Cantidad;
                    if (producto.cantidad < 0) producto.cantidad = 0; // Prevenir stock negativo
                }
            }

            // Guardar venta
            venta.IdVenta = _ventas.Count + 1;
            venta.Fecha = DateTime.Now;
            _ventas.Add(venta);

            // Guardar cambios
            GuardarDatos();
        }

        public decimal ObtenerTotalVentasAcumuladas() {
            return _ventas.Sum(v => v.Total);
        }

        public (decimal Efectivo, decimal Transferencia, decimal Total) ObtenerEstadisticasVentas() {
            decimal efectivo = _ventas.Where(v => v.MetodoPago == "Efectivo").Sum(v => v.Total);
            decimal transferencia = _ventas.Where(v => v.MetodoPago == "Transferencia").Sum(v => v.Total);
            decimal total = efectivo + transferencia;

            return (efectivo, transferencia, total);
        }

        public void LimpiarVentas() {
            _ventas.Clear();
            GuardarDatos(); // Para persistir los cambios
        }

        private void GuardarDatos() {
            var productosJson = JsonConvert.SerializeObject(_productos, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(ProductosPath, productosJson);

            var ventasJson = JsonConvert.SerializeObject(_ventas, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(VentasPath, ventasJson);
        }

        public List<Producto> ValidarEstructuraProductos(string json) {
            try {
                var productos = JsonConvert.DeserializeObject<List<Producto>>(json);
                var productosValidados = new List<Producto>();

                if (productos == null || productos.Count == 0)
                    return productosValidados;

                foreach (var producto in productos) {
                    if (
                        producto.id_producto > 0 &&
                        !string.IsNullOrEmpty(producto.nombre) &&
                        producto.precio_venta_base > 0 &&
                        producto.cantidad > 0) {
                        productosValidados.Add(producto);
                    }
                }

                return productosValidados;
            } catch {
                return new List<Producto>();
            }
        }
    }
}