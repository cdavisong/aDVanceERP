using aDVancePOS.Mobile.Modelos;

using System.Text.Json;

namespace aDVancePOS.Mobile.Servicios {
    public class VentaService {
        private readonly ConfiguracionApp _config;
        private int _contadorTicketHoy = 0;

        private VentasExportacionJson _ventasHoy = new();

        // Ventas archivadas en espera de confirmación de transferencia
        // Se guardan en el mismo JSON bajo una lista separada
        private readonly List<VentaExportacion> _ventasEnEspera = new();

        public VentaService(ConfiguracionApp config) {
            _config = config;
        }

        public async Task CargarVentasDelDiaAsync() {
            try {
                if (!File.Exists(RutasApp.RutaVentasHoy)) {
                    _ventasHoy = NuevoArchivoVentas();
                    return;
                }
                var json = await File.ReadAllTextAsync(RutasApp.RutaVentasHoy);
                _ventasHoy = JsonSerializer.Deserialize(json, JsonContexto.Default.VentasExportacionJson)
                             ?? NuevoArchivoVentas();
                _contadorTicketHoy = _ventasHoy.Ventas.Count;

                // Restaurar ventas en espera que quedaron del mismo día
                _ventasEnEspera.Clear();
                _ventasEnEspera.AddRange(
                    _ventasHoy.VentasEnEspera ?? new());
            } catch {
                _ventasHoy = NuevoArchivoVentas();
            }
        }

        // ── Registrar venta completada ─────────────────────────

        /// <summary>
        /// Registra una venta con los pagos ya construidos externamente.
        /// CobroActivity arma los pagos con su lógica multimoneda y los pasa aquí.
        /// </summary>
        public async Task<VentaExportacion> RegistrarVentaAsync(
            CarritoService carrito,
            List<PagoExportacion> pagos,
            string? observaciones = null) {

            var venta = ConstruirVenta(carrito, "Completada", observaciones);
            venta.Pagos.AddRange(pagos);
            await GuardarVentaAsync(venta);
            return venta;
        }

        // ── Ventas en espera ───────────────────────────────────

        /// <summary>
        /// Archiva la venta como "EnEspera" — los productos ya salen del stock
        /// pero la venta no se exporta al ERP hasta que se complete.
        /// </summary>
        public async Task<VentaExportacion> ArchivarEnEsperaAsync(
            CarritoService carrito,
            List<PagoExportacion> pagosParcialesHastaAhora,
            string? observaciones = null) {

            var venta = ConstruirVenta(carrito, "EnEspera", observaciones);
            venta.Pagos.AddRange(pagosParcialesHastaAhora);

            _ventasEnEspera.Add(venta);
            _ventasHoy.VentasEnEspera = _ventasEnEspera.ToList();
            await PersistirJsonAsync();
            return venta;
        }

        /// <summary>
        /// Completa una venta que estaba en espera agregando los pagos finales.
        /// La mueve de la lista de espera a la lista de ventas del día.
        /// </summary>
        public async Task<VentaExportacion> CompletarVentaEnEsperaAsync(
            VentaExportacion ventaEnEspera,
            List<PagoExportacion> pagosNuevos) {

            ventaEnEspera.EstadoVenta = "Completada";
            ventaEnEspera.Pagos.AddRange(pagosNuevos);

            _ventasEnEspera.Remove(ventaEnEspera);
            _ventasHoy.VentasEnEspera = _ventasEnEspera.ToList();

            await GuardarVentaAsync(ventaEnEspera);
            return ventaEnEspera;
        }

        /// <summary>Descarta una venta en espera y devuelve el stock al catálogo.</summary>
        public async Task CancelarVentaEnEsperaAsync(
            VentaExportacion venta,
            CatalogoService catalogoService) {

            // Devolver stock: buscar cada producto y sumar
            foreach (var det in venta.Detalles) {
                var prod = catalogoService.BuscarPorId((int)det.IdProducto);
                if (prod != null)
                    prod.StockEnSesion += det.Cantidad;
            }

            _ventasEnEspera.Remove(venta);
            _ventasHoy.VentasEnEspera = _ventasEnEspera.ToList();
            await PersistirJsonAsync();
        }

        // ── Consultas ──────────────────────────────────────────

        public int TotalVentasHoy      => _ventasHoy.Ventas.Count;
        public int TotalEnEspera       => _ventasEnEspera.Count;
        public decimal TotalRecaudadoHoy => _ventasHoy.Meta.TotalRecaudado;

        public List<VentaExportacion> ObtenerVentasDia() =>
            _ventasHoy.Ventas.AsEnumerable().Reverse().ToList();

        public List<VentaExportacion> ObtenerVentasEnEspera() =>
            _ventasEnEspera.AsEnumerable().Reverse().ToList();

        public (decimal Efectivo, decimal Transferencia) ObtenerResumenPorMetodo() {
            decimal ef = 0m, tr = 0m;
            foreach (var venta in _ventasHoy.Ventas)
                foreach (var pago in venta.Pagos) {
                    if (pago.MetodoPago == "Efectivo")         ef += pago.MontoPagado;
                    else if (pago.MetodoPago == "TransferenciaBancaria") tr += pago.MontoPagado;
                }
            return (ef, tr);
        }

        // ── Privados ───────────────────────────────────────────

        private VentaExportacion ConstruirVenta(
            CarritoService carrito,
            string estado,
            string? observaciones) {

            _contadorTicketHoy++;
            var venta = new VentaExportacion {
                IdLocal       = Guid.NewGuid().ToString(),
                IdCliente     = _config.IdClienteAnonimo,
                IdAlmacen     = _config.IdAlmacen,
                NumeroTicket  = $"{_config.PrefijoTicket}{DateTime.Now:yyyyMMdd}{_contadorTicketHoy:D4}",
                FechaVenta    = DateTime.UtcNow,
                TotalBruto    = carrito.TotalBruto,
                DescuentoTotal = 0,
                ImpuestoTotal  = carrito.TotalImpuesto,
                ImporteTotal   = carrito.ImporteTotal,
                EstadoVenta    = estado,
                Observaciones  = observaciones
            };

            foreach (var item in carrito.Items)
                venta.Detalles.Add(new DetalleExportacion {
                    IdProducto          = item.Producto.Id,
                    IdPresentacion      = item.IdPresentacion,
                    Cantidad            = item.Cantidad,
                    PrecioVentaUnitario = item.PrecioUnitario,
                    DescuentoItem       = 0,
                    Subtotal            = item.Subtotal
                });

            return venta;
        }

        private async Task GuardarVentaAsync(VentaExportacion venta) {
            _ventasHoy.Ventas.Add(venta);
            _ventasHoy.Meta.TotalVentas    = _ventasHoy.Ventas.Count;
            _ventasHoy.Meta.TotalRecaudado += venta.ImporteTotal;
            _ventasHoy.Meta.ExportadoEn    = DateTime.UtcNow;
            _ventasHoy.Meta.IdAlmacen      = _config.IdAlmacen;
            await PersistirJsonAsync();
        }

        private async Task PersistirJsonAsync() {
            var json = JsonSerializer.Serialize(
                _ventasHoy, JsonContexto.Default.VentasExportacionJson);
            await File.WriteAllTextAsync(RutasApp.RutaVentasHoy, json);
        }

        private VentasExportacionJson NuevoArchivoVentas() => new() {
            Meta = new ExportacionMeta {
                IdAlmacen   = _config.IdAlmacen,
                ExportadoEn = DateTime.UtcNow
            }
        };
    }
}
