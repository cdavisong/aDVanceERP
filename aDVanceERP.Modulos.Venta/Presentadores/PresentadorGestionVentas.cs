using aDVanceERP.Core.Controladores;
using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Documentos;
using aDVanceERP.Modulos.Venta.Interfaces;
using aDVanceERP.Modulos.Venta.Vistas;

using System.Text.Json;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    internal class PresentadorGestionVentas : PresentadorVistaGestion<PresentadorTuplaVenta, IVistaGestionVentas, IVistaTuplaVenta, Core.Modelos.Modulos.Venta.Venta, RepoVenta, FiltroBusquedaVenta> {
        private ControladorArchivosAndroidPos _androidPos = new ControladorArchivosAndroidPos(Application.StartupPath);
        private DocFacturaVenta _docFacturaVenta = null!;

        public PresentadorGestionVentas(IVistaGestionVentas vista) : base(vista) {
            vista.ImportarVentasDesdeDispositivo += OnImportarVentasDesdeDispositivo;

            RegistrarEntidad += OnRegistrarVenta;
            EditarEntidad += OnEditarVenta;

            AgregadorEventos.Suscribir("MostrarVistaGestionVentas", OnMostrarVistaGestionVentas);
            AgregadorEventos.Suscribir("HabilitarDeshabilitarVenta", OnHabilitarDeshabilitarVenta);
        }

        private void OnRegistrarVenta(object? sender, EventArgs e) {
            if (RepoProducto.Instancia.Cantidad() == 0) {
                CentroNotificaciones.MostrarNotificacion("No es posible registrar una venta manual porque no hay productos registrados en el sistema. Por favor, registre al menos un producto antes de continuar.", TipoNotificacion.Advertencia);
                return;
            }

            AgregadorEventos.Publicar("MostrarVistaRegistroVenta", string.Empty);
        }

        private void OnEditarVenta(object? sender, Core.Modelos.Modulos.Venta.Venta e) {
            AgregadorEventos.Publicar("MostrarVistaEdicionVenta", AgregadorEventos.SerializarPayload(e));
        }

        private void OnImportarVentasDesdeDispositivo(object? sender, EventArgs e) {
            var rutaArchivoVentas = Path.Combine(Application.StartupPath, "ventas.json");
            var archivosDescargados = _androidPos.FlujoFinDia(rutaArchivoVentas, true);

            if (archivosDescargados.Count == 0) {
                CentroNotificaciones.MostrarNotificacion("No se encontraron ventas para importar desde el dispositivo. Por favor, asegúrese de que existan archivos de ventas en el dispositivo y que contengan datos válidos.", TipoNotificacion.Advertencia);
                return;
            }

            int ventasImportadas = 0;
            int ventasSaltadasDuplicadas = 0;
            var errores = new List<string>();
            var repoVenta = RepoVenta.Instancia;
            var repoProducto = RepoProducto.Instancia;
            var repoDetalleVenta = RepoDetalleVentaProducto.Instancia;
            var repoinventario = RepoInventario.Instancia;

            foreach (var archivo in archivosDescargados) {
                try {
                    if (!File.Exists(archivo)) continue;

                    var contenido = File.ReadAllText(archivo);
                    var root = JsonSerializer.Deserialize<VentasExportacionJson>(contenido);

                    if (root?.Ventas == null || root.Ventas.Count == 0) {
                        // Archivo válido pero sin ventas
                        continue;
                    }

                    foreach (var ventaExp in root.Ventas) {
                        try {
                            // Evitar duplicados: buscar por número de ticket existente
                            var existentes = repoVenta.Buscar(FiltroBusquedaVenta.NumeroFactura, ventaExp.NumeroTicket).resultadosBusqueda;
                            if (existentes != null && existentes.Count > 0) {
                                ventasSaltadasDuplicadas++;
                                continue;
                            }

                            // Construir entidad Venta para la BD
                            var ventaBD = new Core.Modelos.Modulos.Venta.Venta {
                                Id = 0,
                                IdPedido = 0,
                                IdCliente = ventaExp.IdCliente,
                                IdEmpleadoVendedor = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0, // TODO: Integrar con empleados autenticados,
                                IdAlmacen = ventaExp.IdAlmacen,
                                NumeroFacturaTicket = ventaExp.NumeroTicket,
                                FechaVenta = ventaExp.FechaVenta,
                                TotalBruto = ventaExp.TotalBruto,
                                DescuentoTotal = ventaExp.DescuentoTotal,
                                ImpuestoTotal = ventaExp.ImpuestoTotal,
                                ImporteTotal = ventaExp.ImporteTotal,
                                MetodoPagoPrincipal = ventaExp.Pagos?.FirstOrDefault()?.MetodoPago ?? string.Empty,
                                EstadoVenta = Enum.TryParse<EstadoVentaEnum>(ventaExp.EstadoVenta, out var ev) ? ev : EstadoVentaEnum.Completada,
                                ObservacionesVenta = ventaExp.Observaciones ?? string.Empty,
                                Activo = true
                            };

                            // Adicionar Venta
                            long idVenta = repoVenta.Adicionar(ventaBD);

                            // Detalles: insertar y ajustar inventario
                            if (ventaExp.Detalles != null && ventaExp.Detalles.Count > 0) {
                                foreach (var det in ventaExp.Detalles) {
                                    try {
                                        var producto = repoProducto.ObtenerPorId(det.IdProducto);
                                        decimal precioCompra = 0m;
                                        if (producto != null)
                                            precioCompra = producto.Categoria == CategoriaProducto.ProductoTerminado
                                                ? producto.CostoProduccionUnitario
                                                : producto.CostoAdquisicionUnitario;

                                        var detalleBD = new DetalleVentaProducto {
                                            Id = 0,
                                            IdVenta = idVenta,
                                            IdProducto = det.IdProducto,
                                            Cantidad = det.Cantidad,
                                            PrecioCompraVigente = precioCompra,
                                            PrecioVentaUnitario = det.PrecioVentaUnitario,
                                            DescuentoItem = det.DescuentoItem,
                                            Subtotal = det.Subtotal
                                        };

                                        repoDetalleVenta.Adicionar(detalleBD);

                                        // Crear movimiento de inventario
                                        var inventarioProducto = repoinventario.Buscar(FiltroBusquedaInventario.IdProducto, det.IdProducto.ToString()).resultadosBusqueda.FirstOrDefault(p => p.entidadBase.IdAlmacen.Equals(ventaBD.IdAlmacen)).entidadBase;
                                        var movimiento = new Movimiento() {
                                            Id = 0,
                                            IdProducto = det.IdProducto,
                                            CostoUnitario = precioCompra,
                                            IdAlmacenOrigen = ventaBD.IdAlmacen,
                                            IdAlmacenDestino = 0,
                                            Estado = EstadoMovimiento.Completado,
                                            FechaCreacion = DateTime.Now,
                                            SaldoInicial = inventarioProducto.Cantidad,
                                            FechaTermino = ventaBD.EstadoVenta == EstadoVentaEnum.Completada ? DateTime.Now : DateTime.MinValue,
                                            CantidadMovida = det.Cantidad,
                                            SaldoFinal = inventarioProducto.Cantidad - det.Cantidad,
                                            IdTipoMovimiento = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Nombre, "Venta").resultadosBusqueda.FirstOrDefault().entidadBase?.Id ?? 0,
                                            IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                                            Notas = "Venta de producto importada desde la aplicación.",
                                        };

                                        RepoMovimiento.Instancia.Adicionar(movimiento);

                                        // Modificar inventario
                                        repoinventario.ModificarInventario(
                                            det.IdProducto,
                                            ventaBD.IdAlmacen,
                                            0,
                                            det.Cantidad);
                                    } catch (Exception dex) {
                                        // Registrar error pero continuar con otros detalles
                                        errores.Add($"Archivo '{Path.GetFileName(archivo)}' - detalle producto {det.IdProducto}: {dex.Message}");
                                    }
                                }
                            }

                            // Pagos: insertar y detalle transferencia si aplica
                            var repoPago = RepoPago.Instancia;
                            var repoDetallePagoTransferencia = RepoDetallePagoTransferencia.Instancia;

                            if (ventaExp.Pagos != null && ventaExp.Pagos.Count > 0) {
                                foreach (var pagoExp in ventaExp.Pagos) {
                                    try {
                                        var pagoBD = new Pago {
                                            Id = 0,
                                            IdVenta = idVenta,
                                            MetodoPago = Enum.TryParse<MetodoPagoEnum>(pagoExp.MetodoPago, out var mp) ? mp : MetodoPagoEnum.Efectivo,
                                            MontoPagado = pagoExp.MontoPagado,
                                            FechaPago = pagoExp.FechaPagoCliente,
                                            FechaConfirmacionPago = pagoExp.EstadoPago != null && pagoExp.EstadoPago.Equals("Confirmado", StringComparison.OrdinalIgnoreCase)
                                                ? DateTime.Now
                                                : DateTime.MinValue,
                                            EstadoPago = pagoExp.EstadoPago != null && pagoExp.EstadoPago.Equals("Confirmado", StringComparison.OrdinalIgnoreCase)
                                                ? EstadoPagoEnum.Confirmado
                                                : EstadoPagoEnum.Pendiente
                                        };

                                        long idPago = repoPago.Adicionar(pagoBD);

                                        if (pagoExp.DetalleTransferencia != null) {
                                            var dt = new DetallePagoTransferencia {
                                                Id = 0,
                                                IdPago = idPago,
                                                NumeroTelefonoRemitente = pagoExp.DetalleTransferencia.NumeroConfirmacion,
                                                NumeroTransaccion = pagoExp.DetalleTransferencia.NumeroTransaccion,
                                                MontoTransferencia = pagoExp.MontoPagado
                                            };

                                            repoDetallePagoTransferencia.Adicionar(dt);
                                        }
                                    } catch (Exception pex) {
                                        errores.Add($"Archivo '{Path.GetFileName(archivo)}' - pago venta {ventaExp.NumeroTicket}: {pex.Message}");
                                    }
                                }
                            }

                            try {
                                // Actualizar el estado de la venta a completada si el total pagado cubre el importe total de la venta
                                if (repoVenta.VentaEstaPagadaCompletamente(idVenta))
                                    repoVenta.CambiarEstadoVenta(idVenta, EstadoVentaEnum.Completada);

                                // Actualizar el método de pago principal de la venta
                                repoVenta.ActualizarMetodoPagoPrincipal(idVenta);
                            } catch { /* no crítico */ }

                            ventasImportadas++;
                        } catch (Exception innerEx) {
                            errores.Add($"Archivo '{Path.GetFileName(archivo)}' - venta {ventaExp?.NumeroTicket ?? "<sin ticket>"}: {innerEx.Message}");
                        }
                    }
                } catch (Exception exFile) {
                    errores.Add($"Error procesando archivo '{Path.GetFileName(archivo)}': {exFile.Message}");
                } finally {
                    // Eliminar archivo temporal local
                    try { File.Delete(archivo); } catch { }
                }
            }

            // Notificaciones resumen
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"Ventas importadas: {ventasImportadas}");
            sb.AppendLine($"Ventas duplicadas omitidas: {ventasSaltadasDuplicadas}");
            if (errores.Count > 0) {
                sb.AppendLine();
                sb.AppendLine("Errores (muestras hasta 5):");
                foreach (var err in errores.Take(5))
                    sb.AppendLine($"• {err}");
            }

            CentroNotificaciones.MostrarNotificacion(sb.ToString().TrimEnd(), TipoNotificacion.Info);

            // Refrescar resultados en la vista
            ActualizarResultadosBusqueda();
        }

        private void OnMostrarVistaGestionVentas(string obj) {
            Vista.CargarFiltrosBusqueda(UtilesBusquedaVenta.FiltroBusquedaVenta);
            Vista.Restaurar();

            // Cambiar visibilidad del botón para importar ventas desde dispositivo según conexión con dispositivo Android
            Vista.MostrarBotonImportarVentasDispositivo = _androidPos.CheckDeviceConnection()&& _androidPos.CheckAppInstalada();

            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        public override void ActualizarResultadosBusqueda() {
            if (FiltroBusqueda == FiltroBusquedaVenta.Todas && (CriteriosBusqueda == null || CriteriosBusqueda.Length == 0))
                CriteriosBusqueda = [DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Today.ToString("yyyy-MM-dd"), string.Empty];

            base.ActualizarResultadosBusqueda();
        }

        private void OnHabilitarDeshabilitarVenta(string obj) {
            var idVentaSeleccionado = _tuplasEntidades.FirstOrDefault(t => t.EstadoSeleccion)?.Vista.Id ?? 0;

            if (idVentaSeleccionado != 0) {
                var estado = RepoVenta.Instancia.HabilitarDeshabilitarVenta(idVentaSeleccionado);

                ActualizarResultadosBusqueda();

                CentroNotificaciones.MostrarNotificacion($"La venta ha sido {(estado ? "habilitada" : "deshabilitada")} satisfactoriamente.", TipoNotificacion.Info);
            }
        }

        protected override PresentadorTuplaVenta ObtenerValoresTupla(Core.Modelos.Modulos.Venta.Venta entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaVenta(new VistaTuplaVenta(), entidad);
            var cliente = RepoCliente.Instancia.ObtenerPorId(entidad.IdCliente);
            var persona = RepoPersona.Instancia.Buscar(FiltroBusquedaPersona.Id, cliente?.IdPersona.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.NumeroFacturaVenta = entidad.NumeroFacturaTicket ?? "-";
            presentadorTupla.Vista.FechaVenta = entidad.FechaVenta;
            presentadorTupla.Vista.NombreCliente = persona?.NombreCompleto ?? "Anónimo";
            presentadorTupla.Vista.MetodoPagoPrincipal = string.IsNullOrEmpty(entidad.MetodoPagoPrincipal) ? "No existen pagos registrados" : Enum.Parse<MetodoPagoEnum>(entidad.MetodoPagoPrincipal).ObtenerDisplayName();
            presentadorTupla.Vista.TotalBruto = entidad.TotalBruto;
            presentadorTupla.Vista.DescuentoTotal = entidad.DescuentoTotal;
            presentadorTupla.Vista.ImpuestoTotal = entidad.ImpuestoTotal;
            presentadorTupla.Vista.ImporteTotal = entidad.ImporteTotal;
            presentadorTupla.Vista.Activo = entidad.Activo;
            presentadorTupla.Vista.EstadoVenta = entidad.EstadoVenta;
            presentadorTupla.Vista.ExportarFacturaVenta += OnExportarDocumentoFacturaVenta;
            presentadorTupla.Vista.AnularVenta += OnAnularVenta;

            return presentadorTupla;
        }

        private void OnExportarDocumentoFacturaVenta(object? sender, (long id, FormatoDocumento formato) e) {
            _docFacturaVenta = new DocFacturaVenta(e.id);
            _docFacturaVenta.GenerarDocumentoConParametros(e.formato);
        }

        private void OnAnularVenta(object? sender, long idVenta) {
            var repoVenta = RepoVenta.Instancia;
            var repoDetalleVentaProducto = RepoDetalleVentaProducto.Instancia;
            var venta = repoVenta.ObtenerPorId(idVenta)!;

            // Verificar si la venta está asociada a algún pedido y cancelarlo
            if (venta.IdPedido != 0) {
                var repoPedido = RepoPedido.Instancia;
                var pedido = repoPedido.ObtenerPorId(venta.IdPedido);

                if (pedido != null)
                    repoPedido.CambiarEstadoPedido(pedido.Id, EstadoPedidoEnum.Cancelado);
            }

            // Verificar si la venta tiene un envío asociado y solicitar cancelar los pagos desde la ventana de envíos
            var repoSeguimientoEntrega = RepoSeguimientoEntrega.Instancia;
            var envio = repoSeguimientoEntrega.Buscar(FiltroBusquedaSeguimientoEntrega.IdVenta, venta.Id.ToString()).resultadosBusqueda.Select(rs => rs.entidadBase).FirstOrDefault()!;

            if (envio != null && envio.EstadoEntrega != EstadoEntregaEnum.Completado && envio.EstadoEntrega != EstadoEntregaEnum.Cancelado) {
                CentroNotificaciones.MostrarNotificacion("Esta venta tiene un envío asociado en curso. Por favor, cancele el envío para anular los pagos vinculados a la venta actual.", TipoNotificacion.Advertencia);
                return;
            } else {
                // Verificar si la venta tiene pagos asociados (pendientes o no) y anularlos
                var repoPago = RepoPago.Instancia;
                var pagosVenta = repoPago.Buscar(FiltroBusquedaPago.IdCompraVenta, venta.Id.ToString(), "Venta").resultadosBusqueda.Select(p => p.entidadBase).ToList();

                if (pagosVenta?.Count > 0) {
                    foreach (var pago in pagosVenta)
                        repoPago.CambiarEstadoPago(pago.Id, EstadoPagoEnum.Anulado);
                }
            }

            // Crear movimiento de inventario para cada detalle de venta y revertir el inventario
            var repoMovimiento = RepoMovimiento.Instancia;
            var repoInventario = RepoInventario.Instancia;
            var detallesVenta = repoDetalleVentaProducto.Buscar(FiltroBusquedaDetalleVenta.PorVenta, venta.Id.ToString()).resultadosBusqueda.Select(dv => dv.entidadBase).ToList();

            foreach (var detalleVenta in detallesVenta) {
                var producto = RepoProducto.Instancia.ObtenerPorId(detalleVenta.IdProducto);
                var inventarioProducto = repoInventario.Buscar(FiltroBusquedaInventario.IdProducto, producto!.Id.ToString()).resultadosBusqueda.FirstOrDefault(p => p.entidadBase.IdAlmacen.Equals(venta.IdAlmacen)).entidadBase;
                var movimiento = new Movimiento() {
                    Id = 0,
                    IdProducto = producto!.Id,
                    CostoUnitario = producto.Categoria == CategoriaProducto.ProductoTerminado ? producto.CostoProduccionUnitario : producto.CostoAdquisicionUnitario,
                    IdAlmacenOrigen = 0,
                    IdAlmacenDestino = venta.IdAlmacen,
                    Estado = EstadoMovimiento.Completado,
                    FechaCreacion = DateTime.Now,
                    SaldoInicial = inventarioProducto.Cantidad,
                    FechaTermino = DateTime.Now,
                    CantidadMovida = detalleVenta.Cantidad,
                    SaldoFinal = inventarioProducto.Cantidad + detalleVenta.Cantidad,
                    IdTipoMovimiento = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Nombre, "Devolución de Venta").resultadosBusqueda.FirstOrDefault().entidadBase?.Id ?? 0,
                    IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                    Notas = $"Devolución para la venta del producto: {producto.Nombre}.",
                };

                // Adicionar a la base de datos local
                repoMovimiento.Adicionar(movimiento);

                // Modificar inventario
                repoInventario.ModificarInventario(
                    producto!.Id,
                    0,
                    venta.IdAlmacen,
                    detalleVenta.Cantidad);
            }

            // Finalmente, anular la venta
            repoVenta.CambiarEstadoVenta(venta.Id, EstadoVentaEnum.Anulada);
        }
    }
}