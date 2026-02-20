using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Documentos;
using aDVanceERP.Modulos.Venta.Interfaces;
using aDVanceERP.Modulos.Venta.Vistas;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    internal class PresentadorGestionVentas : PresentadorVistaGestion<PresentadorTuplaVenta, IVistaGestionVentas, IVistaTuplaVenta, Core.Modelos.Modulos.Venta.Venta, RepoVenta, FiltroBusquedaVenta> {
        private DocFacturaVenta _docFacturaVenta = null!;

        public PresentadorGestionVentas(IVistaGestionVentas vista) : base(vista) {
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

        private void OnMostrarVistaGestionVentas(string obj) {
            Vista.CargarFiltrosBusqueda(UtilesBusquedaVenta.FiltroBusquedaVenta);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
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
                var pagosVenta = repoPago.Buscar(FiltroBusquedaPago.IdVenta, venta.Id.ToString()).resultadosBusqueda.Select(p => p.entidadBase).ToList();

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