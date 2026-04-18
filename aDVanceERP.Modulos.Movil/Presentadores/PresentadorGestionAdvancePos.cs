using aDVanceERP.Core.Controladores;
using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Extension.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Caja;
using aDVanceERP.Core.Repositorios.Modulos.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Movil.Interfaces;

using System.Text.Json;

namespace aDVanceERP.Modulos.Movil.Presentadores {
    internal class PresentadorGestionAdvancePos : PresentadorVistaBase<IVistaGestionAdvancePos> {
        private readonly ControladorArchivosAndroidPos _controladorPos = null!;
        private readonly ExportadorCatalogosPos _exportador = null!;

        public PresentadorGestionAdvancePos(IVistaGestionAdvancePos vista) : base(vista) {
            _controladorPos = new ControladorArchivosAndroidPos(Application.StartupPath);
            _exportador = new ExportadorCatalogosPos(CarpetaExportacion);

            vista.VerificarConexion += OnVerificarConexion;
            vista.EnviarCatalogo += OnEnviarCatalogo;
            vista.EliminarCatalogo += OnEliminarCatalogo;
            vista.ImportarVentas += OnImportarVentas;
            vista.ImportarTodasLasVentas += OnImportarTodasLasVentas;

            AgregadorEventos.Suscribir("MostrarVistaGestionPos", OnMostrarVistaGestionPos);
        }

        private string CarpetaExportacion => Path.Combine(Application.StartupPath, "exports", "pos");

        private string CarpetaImportacion => Path.Combine(Application.StartupPath, "imports", "pos");

        private void OnMostrarVistaGestionPos(string obj) {
            CargarDatosComunes();

            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarEstadoConexion();
        }

        private void CargarDatosComunes() {
            Vista.CargarAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
        }

        private void OnVerificarConexion(object? sender, EventArgs e) {
            ActualizarEstadoConexion(mostrarAdvertencia: true);
        }

        private void OnEnviarCatalogo(object? sender, EventArgs e) {
            if (!_controladorPos.CheckDeviceConnection(mostrarAdvertencia: true))
                return;

            // Generar el catálogo desde los repositorios
            GenerarCatalogoPos(Vista.Almacen?.Id ?? 0);

            // Enviarlo al dispositivo
            bool ok = _controladorPos.PushCatalogo(Path.Combine(CarpetaExportacion, "catalogo.json"));

            if (ok)
                ActualizarEstadoConexion(); // refresca el botón de eliminar
        }

        private void OnEliminarCatalogo(object? sender, EventArgs e) {
            var resultado = CentroNotificaciones.MostrarMensaje(
                "¿Está seguro de que desea eliminar el catálogo del dispositivo? " +
                "aDVance.POS no podrá registrar nuevas ventas hasta que se envíe un nuevo catálogo.",
                TipoMensaje.Advertencia,
                BotonesMensaje.SiNo);

            if (resultado != DialogResult.Yes)
                return;

            _controladorPos.EliminarCatalogo();

            ActualizarEstadoConexion();
        }

        private void OnImportarVentas(object? sender, EventArgs e) {
            if (!_controladorPos.CheckDeviceConnection(mostrarAdvertencia: true))
                return;

            string carpetaDestino = Path.Combine(Application.StartupPath, "imports", "pos");
            string rutaArchivoIndividual = sender as string ?? string.Empty;
            string rutaArchivoHoy = rutaArchivoIndividual.StartsWith("ventas_") 
                ? Path.Combine(carpetaDestino, rutaArchivoIndividual)
                : Path.Combine(carpetaDestino, $"ventas_{DateTime.Today:yyyyMMdd}.json");

            bool ok = _controladorPos.PullVentas(rutaArchivoHoy, DateTime.Today);

            if (!ok)
                return;

            ProcesarArchivosVentas([rutaArchivoHoy], eliminarTrasImportar: true);
        }

        private void OnImportarTodasLasVentas(object? sender, EventArgs e) {
            if (!_controladorPos.CheckDeviceConnection(mostrarAdvertencia: true))
                return;

            string carpetaDestino = Path.Combine(Application.StartupPath, "imports", "pos");
            var archivos = _controladorPos.FlujoFinDia(carpetaDestino, eliminarDelDispositivo: true);

            if (archivos.Count == 0)
                return;

            ProcesarArchivosVentas(archivos, eliminarTrasImportar: false); // ya eliminados por FlujoFinDia
        }

        private void GenerarCatalogoPos(long idAlmacen) {
            _exportador.ExportarCatalogoCompleto(idAlmacen);
        }

        private void ProcesarArchivosVentas(List<string> archivos, bool eliminarTrasImportar) {
            int ventasImportadas = 0;
            int ventasSaltadasDuplicadas = 0;
            var errores = new List<string>();

            var repoVenta = RepoVenta.Instancia;
            var repoAlmacen = RepoAlmacen.Instancia;
            var repoProducto = RepoProducto.Instancia;
            var repoDetalleVenta = RepoDetalleVentaProducto.Instancia;
            var repoMovimiento = RepoMovimiento.Instancia;
            var repoInventario = RepoInventario.Instancia;
            var repoPago = RepoPago.Instancia;
            var repoDetallePagoTransferencia = RepoDetallePagoTransferencia.Instancia;
            var repoCaja = RepoCajaTurno.Instancia;
            var repoMovimientoCaja = RepoCajaMovimiento.Instancia;
            
            // Resolver el IdTipoMovimiento "Venta" una sola vez fuera del loop
            var idTipoMovimientoVenta = RepoTipoMovimiento.Instancia
                .Buscar(FiltroBusquedaTipoMovimiento.Nombre, "Venta")
                .resultadosBusqueda.FirstOrDefault().entidadBase?.Id ?? 0;

            foreach (var archivo in archivos) {
                try {
                    if (!File.Exists(archivo)) 
                        continue;

                    var contenido = File.ReadAllText(archivo);
                    var root = JsonSerializer.Deserialize<VentasExportacionJson>(contenido);

                    if (root?.Ventas == null || root.Ventas.Count == 0)
                        continue;

                    foreach (var ventaExp in root.Ventas) {
                        try {
                            // Almacén existe y está activo
                            var almacen = repoAlmacen.ObtenerPorId(ventaExp.IdAlmacen);
                            if (almacen == null || !almacen.Estado) {
                                errores.Add($"Venta {ventaExp.NumeroTicket}: Almacén {ventaExp.IdAlmacen} no existe o está inactivo. Omitiendo venta.");
                                continue;
                            }

                            // Productos existen y son vendibles
                            bool productosValidos = true;

                            foreach (var det in ventaExp.Detalles ?? new List<DetalleExportacion>()) {
                                var producto = repoProducto.ObtenerPorId(det.IdProducto);
                                if (producto == null || !producto.Activo || !producto.EsVendible) {
                                    errores.Add($"Venta {ventaExp.NumeroTicket}: Producto {det.IdProducto} no existe o no es vendible. Omitiendo venta.");
                                    productosValidos = false;
                                    break;
                                }
                            }
                            if (!productosValidos) continue;

                            // Pagos cuadran con total (tolerancia 0.01 por redondeo)
                            if (ventaExp.Pagos != null && ventaExp.Pagos.Count > 0) {
                                var totalPagado = ventaExp.Pagos.Sum(p => p.MontoPagado);
                                if (Math.Abs(totalPagado - ventaExp.ImporteTotal) > 0.01m) {
                                    errores.Add($"Venta {ventaExp.NumeroTicket}: Pagos ({totalPagado:C}) no cuadran con total ({ventaExp.ImporteTotal:C}). Omitiendo venta.");
                                    continue;
                                }
                            }

                            // Número de ticket no duplicado
                            var existentes = repoVenta
                                .Buscar(FiltroBusquedaVenta.NumeroFactura, ventaExp.NumeroTicket)
                                .resultadosBusqueda;

                            if (existentes != null && existentes.Count > 0) {
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
                                MetodoPagoPrincipal = ventaExp.Pagos?.FirstOrDefault()?.MetodoPago ?? string.Empty,
                                EstadoVenta = Enum.TryParse<EstadoVentaEnum>(ventaExp.EstadoVenta, out var ev)
                                                        ? ev
                                                        : EstadoVentaEnum.Completada,
                                ObservacionesVenta = ventaExp.Observaciones ?? string.Empty,
                                Activo = true
                            };

                            var idVenta = repoVenta.Adicionar(ventaBD);
                            var turno = repoCaja.ObtenerTurnoAbierto(ventaBD.IdAlmacen);

                            // ── Detalles + movimientos de inventario ──
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
                                            .FirstOrDefault(p => p.entidadBase.IdAlmacen == ventaBD.IdAlmacen)
                                            .entidadBase;

                                        repoMovimiento.Adicionar(new Movimiento {
                                            Id = 0,
                                            IdProducto = det.IdProducto,
                                            CostoUnitario = costo,
                                            IdAlmacenOrigen = ventaBD.IdAlmacen,
                                            IdAlmacenDestino = 0,
                                            Estado = EstadoMovimientoEnum.Completado,
                                            FechaCreacion = DateTime.Now,
                                            SaldoInicial = inventarioProducto?.Cantidad ?? 0,
                                            FechaTermino = ventaBD.EstadoVenta == EstadoVentaEnum.Completada
                                                                    ? DateTime.Now
                                                                    : DateTime.MinValue,
                                            CantidadMovida = det.Cantidad,
                                            SaldoFinal = (inventarioProducto?.Cantidad ?? 0) - det.Cantidad,
                                            IdTipoMovimiento = idTipoMovimientoVenta,
                                            IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                                            Notas = "Venta importada desde aDVance.POS."
                                        });

                                        repoInventario.ModificarInventario(
                                            producto,
                                            almacen,
                                            null,
                                            det.Cantidad);

                                    } catch (Exception dex) {
                                        errores.Add($"'{Path.GetFileName(archivo)}' — producto {det.IdProducto}: {dex.Message}");
                                    }
                                }
                            }

                            // ── Pagos ──
                            if (ventaExp.Pagos != null) {
                                foreach (var pagoExp in ventaExp.Pagos) {
                                    try {
                                        bool confirmado = pagoExp.EstadoPago?.Equals(
                                            "Confirmado", StringComparison.OrdinalIgnoreCase) == true;

                                        var pagoBD = new Pago {
                                            Id = 0,
                                            IdVenta = idVenta,
                                            MetodoPago = Enum.TryParse<MetodoPagoEnum>(pagoExp.MetodoPago, out var mp)
                                                                        ? mp
                                                                        : MetodoPagoEnum.Efectivo,
                                            MontoPagado = pagoExp.MontoPagado,
                                            FechaPago = pagoExp.FechaPagoCliente,
                                            FechaConfirmacionPago = confirmado ? DateTime.Now : DateTime.MinValue,
                                            EstadoPago = confirmado ? EstadoPagoEnum.Confirmado : EstadoPagoEnum.Pendiente
                                        };

                                        // Validar número de transacción duplicado para transferencias
                                        if (pagoExp.DetalleTransferencia != null && 
                                            !string.IsNullOrEmpty(pagoExp.DetalleTransferencia.NumeroTransaccion) &&
                                            repoDetallePagoTransferencia.ExisteNumeroTransaccion(pagoExp.DetalleTransferencia.NumeroTransaccion)) {
                                            errores.Add($"'{Path.GetFileName(archivo)}' — pago {ventaExp.NumeroTicket}: Número de transacción '{pagoExp.DetalleTransferencia.NumeroTransaccion}' duplicado. Omitiendo este pago.");
                                            continue;
                                        }

                                        long idPago = repoPago.Adicionar(pagoBD);

                                        if (pagoExp.DetalleTransferencia != null) {
                                            repoDetallePagoTransferencia.Adicionar(new DetallePagoTransferencia {
                                                Id = 0,
                                                IdPago = idPago,
                                                NumeroTelefonoConfirmacion = pagoExp.DetalleTransferencia.NumeroConfirmacion,
                                                NumeroTransaccion = pagoExp.DetalleTransferencia.NumeroTransaccion,
                                                MontoTransferencia = pagoExp.MontoPagado
                                            });
                                        }

                                        // Registrar pago de venta en caja automáticamente
                                        if (ContextoModulos.NombresModulosCargados.Exists(nm => nm.Equals("MOD_CAJA"))
                                            && turno != null) {
                                            var movimiento = new CajaMovimiento() {
                                                Id = 0,
                                                IdTurno = turno.Id,
                                                Tipo = TipoMovimientoCajaEnum.Venta,
                                                CanalPago = (CanalPagoCajaEnum) ((int) pagoBD.MetodoPago),
                                                IdVenta = ventaBD.Id,
                                                Monto = pagoBD.MontoPagado,
                                                Descripcion = $"Pago de factura {ventaBD.NumeroFacturaTicket}",
                                                IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                                                FechaMovimiento = pagoBD.FechaConfirmacionPago ?? DateTime.Now,
                                            };

                                            repoMovimientoCaja.Adicionar(movimiento);
                                        }
                                    } catch (Exception pex) {
                                        errores.Add($"'{Path.GetFileName(archivo)}' — pago {ventaExp.NumeroTicket}: {pex.Message}");
                                    }
                                }
                            }

                            // ── Cerrar venta si está totalmente pagada ──
                            try {
                                if (repoVenta.VentaEstaPagadaCompletamente(idVenta))
                                    repoVenta.CambiarEstadoVenta(idVenta, EstadoVentaEnum.Completada);

                                repoVenta.ActualizarMetodoPagoPrincipal(idVenta);
                            } catch { /* no crítico */ }

                            ventasImportadas++;

                        } catch (Exception innerEx) {
                            errores.Add($"'{Path.GetFileName(archivo)}' — venta {ventaExp?.NumeroTicket ?? "<sin ticket>"}: {innerEx.Message}");
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

            // ── Notificación resumen ──
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"Ventas importadas: {ventasImportadas}");
            sb.AppendLine($"Ventas duplicadas omitidas: {ventasSaltadasDuplicadas}");
            if (errores.Count > 0) {
                sb.AppendLine();
                sb.AppendLine("Errores (primeros 5):");
                foreach (var err in errores.Take(5))
                    sb.AppendLine($"• {err}");
            }

            CentroNotificaciones.MostrarNotificacion(
                sb.ToString().TrimEnd(),
                errores.Count == 0 ? TipoNotificacionEnum.Ok : TipoNotificacionEnum.Advertencia);

            // Notificar a MOD_VENTA para que refresque su lista si está abierta
            if (ventasImportadas > 0)
                AgregadorEventos.Publicar("VentasImportadas", string.Empty);
        }

        private void ActualizarEstadoConexion(bool mostrarAdvertencia = false) {
            bool conectado = _controladorPos.CheckDeviceConnection(mostrarAdvertencia);
            bool appInstalada = conectado && _controladorPos.CheckAppInstalada();

            Vista.DispositivoConectado = conectado;
            Vista.AppInstalada = appInstalada;

            Vista.MostrarBotonEnviarCatalogo = appInstalada;
            Vista.MostrarBotonEliminarCatalogo = appInstalada && _controladorPos.ExisteCatalogo();
            Vista.MostrarBotonImportarVentas = appInstalada;

            if (conectado && appInstalada) {
                // ── Estado del catálogo ──────────────────────────────────────
                var (existe, fechaCatalogo) = _controladorPos.ObtenerInfoCatalogo();
                Vista.CatalogoExisteEnDispositivo = existe;
                Vista.FechaActualizacionCatalogo = fechaCatalogo;

                // ── Archivos de ventas ───────────────────────────────────────
                var archivosVenta = _controladorPos.ListarArchivosVentas();
                Vista.ArchivosDisponiblesDispositivo = archivosVenta.Count;
                Vista.ActualizarArchivosVenta(archivosVenta);
            } else {
                Vista.CatalogoExisteEnDispositivo = false;
                Vista.FechaActualizacionCatalogo = null;
                Vista.ActualizarArchivosVenta(new List<(string fileName, DateTime fecha, double tamanoKb)>());
            }
        }

        private static decimal ObtenerCostoProducto(Producto? producto) {
            if (producto == null) return 0m;
            return producto.Categoria == CategoriaProductoEnum.ProductoTerminado
                ? producto.CostoProduccionUnitario
                : producto.CostoAdquisicionUnitario;
        }

        public override void Dispose() {
            Vista.VerificarConexion -= OnVerificarConexion;
            Vista.EnviarCatalogo -= OnEnviarCatalogo;
            Vista.EliminarCatalogo -= OnEliminarCatalogo;
            Vista.ImportarVentas -= OnImportarVentas;
            Vista.ImportarTodasLasVentas -= OnImportarTodasLasVentas;

            AgregadorEventos.Desuscribir("MostrarVistaGestionPos", OnMostrarVistaGestionPos);

            Vista.Cerrar();
        }
    }
}
