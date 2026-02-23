using aDVancePOS.Mobile.Modelos;

using System.Text.Json;

namespace aDVancePOS.Mobile.Servicios {
    public class VentaService {
        private readonly ConfiguracionApp _config;
        private int _contadorTicketHoy = 0;

        // Ventas del día en memoria (se persisten al JSON al confirmar)
        private VentasExportacionJson _ventasHoy = new();

        public VentaService(ConfiguracionApp config) {
            _config = config;
        }

        /// <summary>
        /// Carga las ventas del día actual desde el JSON (si ya existía).
        /// Llama esto al iniciar MainActivity para no perder ventas previas.
        /// </summary>
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
            } catch {
                _ventasHoy = NuevoArchivoVentas();
            }
        }

        /// <summary>
        /// Registra una venta en efectivo y la persiste en el JSON del día.
        /// </summary>
        public async Task<VentaExportacion> RegistrarVentaEfectivoAsync(
            CarritoService carrito) {
            var venta = ConstruirVenta(carrito);

            venta.Pagos.Add(new PagoExportacion {
                MetodoPago = "Efectivo",
                MontoPagado = carrito.ImporteTotal,
                FechaPagoCliente = DateTime.UtcNow,
                EstadoPago = "Confirmado",
                DetalleTransferencia = null
            });

            await GuardarVentaAsync(venta);
            return venta;
        }

        /// <summary>
        /// Registra una venta por transferencia y la persiste.
        /// </summary>
        public async Task<VentaExportacion> RegistrarVentaTransferenciaAsync(
            CarritoService carrito,
            string numeroConfirmacion,
            string numeroTransaccion) {
            var venta = ConstruirVenta(carrito);

            venta.Pagos.Add(new PagoExportacion {
                MetodoPago = "TransferenciaBancaria",
                MontoPagado = carrito.ImporteTotal,
                FechaPagoCliente = DateTime.UtcNow,
                EstadoPago = "Pendiente", // Se confirma en el ERP desktop
                DetalleTransferencia = new DetalleTransferenciaExportacion {
                    NumeroConfirmacion = numeroConfirmacion,
                    NumeroTransaccion = numeroTransaccion
                }
            });

            await GuardarVentaAsync(venta);
            return venta;
        }

        public int TotalVentasHoy => _ventasHoy.Ventas.Count;
        public decimal TotalRecaudadoHoy => _ventasHoy.Meta.TotalRecaudado;

        /// <summary>
        /// Devuelve el total recaudado desglosado por método de pago.
        /// Usado por MostrarResumenVentasDia() en MainActivity.
        /// </summary>
        public (decimal Efectivo, decimal Transferencia) ObtenerResumenPorMetodo() {
            decimal efectivo = 0m;
            decimal transferencia = 0m;

            foreach (var venta in _ventasHoy.Ventas) {
                foreach (var pago in venta.Pagos) {
                    if (pago.MetodoPago == "Efectivo")
                        efectivo += pago.MontoPagado;
                    else if (pago.MetodoPago == "TransferenciaBancaria")
                        transferencia += pago.MontoPagado;
                }
            }

            return (efectivo, transferencia);
        }

        // ── Privados ──────────────────────────────────────────

        private VentaExportacion ConstruirVenta(CarritoService carrito) {
            _contadorTicketHoy++;

            var venta = new VentaExportacion {
                IdLocal = Guid.NewGuid().ToString(),
                IdCliente = _config.IdClienteAnonimo,
                IdAlmacen = _config.IdAlmacen,
                NumeroTicket = $"{_config.PrefijoTicket}{DateTime.Now:yyyyMMdd}{_contadorTicketHoy:D4}",
                FechaVenta = DateTime.UtcNow,
                TotalBruto = carrito.TotalBruto,
                DescuentoTotal = 0,
                ImpuestoTotal = carrito.TotalImpuesto,
                ImporteTotal = carrito.ImporteTotal,
                EstadoVenta = "Completada"
            };

            foreach (var item in carrito.Items) {
                venta.Detalles.Add(new DetalleExportacion {
                    IdProducto = item.Producto.Id,
                    Cantidad = item.Cantidad,
                    PrecioVentaUnitario = item.Producto.PrecioConImpuesto,
                    DescuentoItem = 0,
                    Subtotal = item.Subtotal
                });
            }

            return venta;
        }

        private async Task GuardarVentaAsync(VentaExportacion venta) {
            _ventasHoy.Ventas.Add(venta);
            _ventasHoy.Meta.TotalVentas = _ventasHoy.Ventas.Count;
            _ventasHoy.Meta.TotalRecaudado += venta.ImporteTotal;
            _ventasHoy.Meta.ExportadoEn = DateTime.UtcNow;
            _ventasHoy.Meta.IdAlmacen = _config.IdAlmacen;

            var json = JsonSerializer.Serialize(_ventasHoy, JsonContexto.Default.VentasExportacionJson);

            await File.WriteAllTextAsync(RutasApp.RutaVentasHoy, json);
        }

        private VentasExportacionJson NuevoArchivoVentas() => new() {
            Meta = new ExportacionMeta {
                IdAlmacen = _config.IdAlmacen,
                ExportadoEn = DateTime.UtcNow
            }
        };
    }
}
