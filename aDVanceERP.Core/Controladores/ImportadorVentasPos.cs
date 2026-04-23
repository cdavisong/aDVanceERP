using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.Modulos.Caja;
using aDVanceERP.Core.Repositorios.Modulos.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Venta;

using System.Text.Json;

namespace aDVanceERP.Core.Controladores {

    /// <summary>
    /// Procesa los archivos JSON de ventas descargados desde aDVance POS Mobile
    /// y los persiste en la base de datos del ERP.
    ///
    /// Responsabilidades:
    ///   - Deserializar los JSON de ventas
    ///   - Validar integridad (almacén, productos, pagos, duplicados)
    ///   - Persistir Venta, DetalleVenta, Movimiento, Inventario, Pago y,
    ///     si procede, CajaMovimiento
    ///   - Publicar el evento "VentasImportadas" al terminar
    ///
    /// El presenter solo construye el importador, llama a Procesar()
    /// y muestra el resumen devuelto al usuario.
    /// </summary>
    public class ImportadorVentasPos {
        /// <summary>Resumen de la operación de importación.</summary>
        public record ResultadoImportacion(
            int VentasImportadas,
            int VentasDuplicadasOmitidas,
            List<string> Errores);

        /// <summary>
        /// Procesa una colección de archivos JSON descargados del dispositivo.
        /// </summary>
        /// <param name="rutasArchivos">Rutas locales de los archivos ventas_*.json.</param>
        /// <param name="registrarEnCaja">
        ///   Si true, crea un CajaMovimiento por cada pago importado.
        ///   El presenter pasa este valor consultando si MOD_CAJA está cargado
        ///   y si hay un turno abierto en el almacén correspondiente.
        /// </param>
        /// <param name="eliminarTrasImportar">
        ///   Si true, elimina el archivo local después de procesarlo.
        /// </param>
        public ResultadoImportacion Procesar(
            IEnumerable<string> rutasArchivos,
            bool registrarEnCaja = false,
            bool eliminarTrasImportar = false) {

            int ventasImportadas = 0;
            int ventasSaltadasDuplicadas = 0;
            var errores = new List<string>();

            var repoVenta = RepoVenta.Instancia;
            var repoAlmacen = RepoAlmacen.Instancia;
            var repoProducto = RepoProducto.Instancia;
            var repoPresentacion = RepoPresentacionProducto.Instancia;
            var repoDetalleVenta = RepoDetalleVentaProducto.Instancia;
            var repoMovimiento = RepoMovimiento.Instancia;
            var repoInventario = RepoInventario.Instancia;
            var repoPago = RepoPago.Instancia;
            var repoDetallePagoTransf = RepoDetallePagoTransferencia.Instancia;
            var repoCajaTurno = RepoCajaTurno.Instancia;
            var repoMovimientoCaja = RepoCajaMovimiento.Instancia;

            // Resolver el IdTipoMovimiento "Venta" una sola vez fuera del loop
            var idTipoMovimientoVenta = RepoTipoMovimiento.Instancia
                .Buscar(FiltroBusquedaTipoMovimiento.Nombre, "Venta")
                .resultadosBusqueda.FirstOrDefault().entidadBase?.Id ?? 0;

            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            foreach (var archivo in rutasArchivos) {
                try {
                    if (!File.Exists(archivo)) continue;

                    var root = JsonSerializer.Deserialize<VentasExportacionJson>(File.ReadAllText(archivo), opciones);

                    if (root?.Ventas == null || root.Ventas.Count == 0)
                        continue;

                    foreach (var ventaExp in root.Ventas) {
                        try {
                            var almacen = repoAlmacen.ObtenerPorId(ventaExp.IdAlmacen);

                            if (almacen == null || !almacen.Estado) {
                                errores.Add($"Venta {ventaExp.NumeroTicket}: " +
                                            $"Almacén {ventaExp.IdAlmacen} no existe o está inactivo.");
                                continue;
                            }

                            bool productosValidos = true;

                            foreach (var det in ventaExp.Detalles ?? new List<DetalleExportacion>()) {
                                var prod = repoProducto.ObtenerPorId(det.IdProducto);

                                if (prod == null || !prod.Activo || !prod.EsVendible) {
                                    errores.Add($"Venta {ventaExp.NumeroTicket}: " +
                                                $"Producto {det.IdProducto} no existe o no es vendible.");
                                    productosValidos = false;
                                    break;
                                }
                            }
                            if (!productosValidos) continue;

                            if (ventaExp.Pagos?.Count > 0) {
                                var totalPagado = ventaExp.Pagos.Sum(p => p.MontoPagado);

                                if (Math.Abs(totalPagado - ventaExp.ImporteTotal) > 0.01m) {
                                    errores.Add($"Venta {ventaExp.NumeroTicket}: " +
                                                $"Pagos ({totalPagado:C}) no cuadran con total " +
                                                $"({ventaExp.ImporteTotal:C}).");
                                    continue;
                                }
                            }

                            var existentes = repoVenta
                                .Buscar(FiltroBusquedaVenta.NumeroFactura, ventaExp.NumeroTicket)
                                .resultadosBusqueda;

                            if (existentes?.Count > 0) {
                                ventasSaltadasDuplicadas++;
                                continue;
                            }

                            var ventaBD = new Venta {
                                Id = 0,
                                IdPedido = 0,
                                IdCliente = ventaExp.IdCliente,
                                IdEmpleadoVendedor = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                                IdAlmacen = ventaExp.IdAlmacen,
                                NumeroFacturaTicket = ventaExp.NumeroTicket,
                                FechaVenta = ventaExp.FechaVenta,
                                TotalBruto = ventaExp.TotalBruto,
                                DescuentoTotal = ventaExp.DescuentoTotal,
                                ImpuestoTotal = ventaExp.ImpuestoTotal,
                                ImporteTotal = ventaExp.ImporteTotal,
                                CanalPagoPrincipal = ventaExp.Pagos?
                                    .FirstOrDefault()?
                                    .MetodoPago ?? string.Empty,
                                EstadoVenta = Enum.TryParse<EstadoVentaEnum>(ventaExp.EstadoVenta, out var ev)
                                    ? ev
                                    : EstadoVentaEnum.Completada,
                                ObservacionesVenta = ventaExp.Observaciones ?? string.Empty,
                                Activo = true
                            };

                            var idVenta = repoVenta.Adicionar(ventaBD);
                            var turno = registrarEnCaja
                                          ? repoCajaTurno.ObtenerTurnoAbierto(ventaBD.IdAlmacen)
                                          : null;

                            if (ventaExp.Detalles != null) {
                                foreach (var det in ventaExp.Detalles) {
                                    try {
                                        var producto = repoProducto.ObtenerPorId(det.IdProducto);
                                        decimal costo = ObtenerCostoProducto(producto);

                                        repoDetalleVenta.Adicionar(new DetalleVentaProducto {
                                            Id = 0,
                                            IdVenta = idVenta,
                                            IdProducto = det.IdProducto,
                                            IdPresentacion = det.IdPresentacion,
                                            Cantidad = det.Cantidad,
                                            PrecioCompraVigente = costo,
                                            PrecioVentaUnitario = det.PrecioVentaUnitario,
                                            DescuentoItem = det.DescuentoItem,
                                            Subtotal = det.Subtotal
                                        });

                                        var inventarioProducto = repoInventario
                                            .Buscar(FiltroBusquedaInventario.IdProducto, det.IdProducto.ToString())
                                            .resultadosBusqueda
                                            .FirstOrDefault(r => r.entidadBase.IdAlmacen == ventaBD.IdAlmacen)
                                            .entidadBase;
                                        var presentacion = det.IdPresentacion != 0
                                            ? repoPresentacion.ObtenerPorId(det.IdPresentacion)
                                            : null;

                                        repoMovimiento.Adicionar(new Movimiento {
                                            Id = 0,
                                            IdProducto = det.IdProducto,
                                            CostoUnitario = costo,
                                            IdAlmacenOrigen = ventaBD.IdAlmacen,
                                            IdAlmacenDestino = 0,
                                            Estado = EstadoMovimientoEnum.Completado,
                                            FechaCreacion = DateTime.Now,
                                            FechaTermino = ventaBD.EstadoVenta == EstadoVentaEnum.Completada
                                                ? DateTime.Now
                                                : DateTime.MinValue,
                                            SaldoInicial = inventarioProducto?.Cantidad ?? 0,
                                            CantidadMovida = presentacion != null
                                                ? presentacion.Cantidad
                                                : det.Cantidad,
                                            SaldoFinal = (inventarioProducto?.Cantidad ?? 0) - (presentacion != null
                                                ? presentacion.Cantidad
                                                : det.Cantidad),
                                            IdTipoMovimiento = idTipoMovimientoVenta,
                                            IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                                            Notas = "Venta importada desde aDVance.POS."
                                        });

                                        repoInventario.ModificarInventario(
                                            producto,
                                            almacen,
                                            null,
                                            presentacion != null
                                                ? presentacion.Cantidad
                                                : det.Cantidad);

                                    } catch (Exception dex) {
                                        errores.Add($"'{Path.GetFileName(archivo)}' " +
                                                    $"— producto {det.IdProducto}: {dex.Message}");
                                    }
                                }
                            }

                            if (ventaExp.Pagos != null) {
                                foreach (var pagoExp in ventaExp.Pagos) {
                                    try {
                                        bool confirmado = pagoExp.EstadoPago?.Equals(
                                            "Confirmado", StringComparison.OrdinalIgnoreCase) == true;

                                        var pagoBD = new Pago {
                                            Id = 0,
                                            IdVenta = idVenta,
                                            MetodoPago = Enum.TryParse<CanalPagoEnum>(pagoExp.MetodoPago, out var mp)
                                                ? mp
                                                : CanalPagoEnum.Efectivo,
                                            MontoPagado = pagoExp.MontoPagado,
                                            FechaPago = pagoExp.FechaPagoCliente,
                                            FechaConfirmacionPago = confirmado
                                                ? DateTime.Now
                                                : DateTime.MinValue,
                                            EstadoPago = confirmado
                                                ? EstadoPagoEnum.Confirmado
                                                : EstadoPagoEnum.Pendiente
                                        };

                                        // Validar número de transacción duplicado
                                        if (pagoExp.DetalleTransferencia != null &&
                                            !string.IsNullOrEmpty(
                                                pagoExp.DetalleTransferencia.NumeroTransaccion) &&
                                            repoDetallePagoTransf.ExisteNumeroTransaccion(
                                                pagoExp.DetalleTransferencia.NumeroTransaccion)) {
                                            errores.Add($"'{Path.GetFileName(archivo)}' " +
                                                        $"— pago {ventaExp.NumeroTicket}: " +
                                                        $"Número de transacción " +
                                                        $"'{pagoExp.DetalleTransferencia.NumeroTransaccion}' " +
                                                        $"duplicado.");
                                            continue;
                                        }

                                        long idPago = repoPago.Adicionar(pagoBD);

                                        if (pagoExp.DetalleTransferencia != null) {
                                            repoDetallePagoTransf.Adicionar(new DetallePagoTransferencia {
                                                Id = 0,
                                                IdPago = idPago,
                                                NumeroTelefonoConfirmacion = pagoExp.DetalleTransferencia.NumeroConfirmacion,
                                                NumeroTransaccion = pagoExp.DetalleTransferencia.NumeroTransaccion,
                                                MontoTransferencia = pagoExp.MontoPagado
                                            });
                                        }

                                        // Registrar en caja si el módulo está activo y hay turno
                                        if (registrarEnCaja && turno != null) {
                                            repoMovimientoCaja.Adicionar(new CajaMovimiento {
                                                Id = 0,
                                                IdTurno = turno.Id,
                                                Tipo = TipoMovimientoCajaEnum.Venta,
                                                CanalPago = (CanalPagoCajaEnum) ((int) pagoBD.MetodoPago),
                                                IdVenta = ventaBD.Id,
                                                Monto = pagoBD.MontoPagado,
                                                Descripcion = $"Pago de factura {ventaBD.NumeroFacturaTicket}",
                                                IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                                                FechaMovimiento = pagoBD.FechaConfirmacionPago ?? DateTime.Now
                                            });
                                        }

                                    } catch (Exception pex) {
                                        errores.Add($"'{Path.GetFileName(archivo)}' " +
                                                    $"— pago {ventaExp.NumeroTicket}: {pex.Message}");
                                    }
                                }
                            }

                            try {
                                if (repoVenta.VentaEstaPagadaCompletamente(idVenta))
                                    repoVenta.CambiarEstadoVenta(idVenta, EstadoVentaEnum.Completada);

                                repoVenta.ActualizarMetodoPagoPrincipal(idVenta);
                            } catch { /* no crítico */ }

                            ventasImportadas++;

                        } catch (Exception innerEx) {
                            errores.Add($"'{Path.GetFileName(archivo)}' " +
                                        $"— venta {ventaExp?.NumeroTicket ?? "<sin ticket>"}: " +
                                        $"{innerEx.Message}");
                        }
                    }

                } catch (Exception exFile) {
                    errores.Add($"Error procesando '{Path.GetFileName(archivo)}': {exFile.Message}");
                } finally {
                    if (eliminarTrasImportar) {
                        try { File.Delete(archivo); } catch { }
                    }
                }
            }

            if (ventasImportadas > 0)
                AgregadorEventos.Publicar("VentasImportadas", string.Empty);

            return new ResultadoImportacion(
                ventasImportadas,
                ventasSaltadasDuplicadas,
                errores);
        }

        private static decimal ObtenerCostoProducto(Producto? producto) {
            if (producto == null) return 0m;
            return producto.Categoria == CategoriaProductoEnum.ProductoTerminado
                ? producto.CostoProduccionUnitario
                : producto.CostoAdquisicionUnitario;
        }
    }
}
