using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Core.Eventos;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorRegistroMovimiento : PresentadorVistaRegistro<IVistaRegistroMovimiento, Core.Modelos.Modulos.Inventario.Movimiento, RepoMovimiento, FiltroBusquedaMovimiento> {
        public PresentadorRegistroMovimiento(IVistaRegistroMovimiento vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroMovimiento", OnMostrarVistaRegistroMovimiento);
            AgregadorEventos.Suscribir("MostrarVistaEdicionMovimiento", OnMostrarVistaEdicionMovimiento);
        }

        private void OnMostrarVistaRegistroMovimiento(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();

            // Carga inicial de datos
            Vista.CargarNombresProductos([.. RepoProducto.Instancia.ObtenerTodos().Select(p => p.entidadBase.Nombre)]);
            Vista.CargarNombresAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(a => a.entidadBase.Nombre)]);

            // Carga de datos extra
            if (!string.IsNullOrEmpty(obj)) {
                var datosExtra = AgregadorEventos.DeserializarPayload<object[]>(obj);
                var producto = AgregadorEventos.DeserializarPayload<Producto>(datosExtra[0].ToString());
                var signoMovimiento = datosExtra[1].ToString();

                if (producto != null) {
                    Vista.NombreProducto = producto.Nombre;
                    Vista.ActualizarUnidadMedidaProductoSeleccionado(producto);
                }

                if (signoMovimiento == "+" || signoMovimiento == "-") {
                    var tiposMovimiento = RepoTipoMovimiento.Instancia.ObtenerTodos().Where(tm => tm.entidadBase.Efecto == (signoMovimiento == "+" ? EfectoMovimiento.Carga : EfectoMovimiento.Descarga)).Select(tm => tm.entidadBase).ToArray();

                    Vista.CargarTiposMovimientos(tiposMovimiento);

                    if (tiposMovimiento.Length == 1)
                        Vista.NombreTipoMovimiento = tiposMovimiento[0].Nombre;
                }
            } else
                Vista.CargarTiposMovimientos([.. RepoTipoMovimiento.Instancia.ObtenerTodos().Select(tm => tm.entidadBase)]);

            Vista.Mostrar();
        }

        private void OnMostrarVistaEdicionMovimiento(string obj) {
            Vista.ModoEdicion = true;
            Vista.Restaurar();

            if (string.IsNullOrEmpty(obj))
                return;

            var movimiento = AgregadorEventos.DeserializarPayload<Movimiento>(obj.ToString());

            if (movimiento == null)
                return;

            // Carga inicial de datos
            Vista.CargarNombresProductos([.. RepoProducto.Instancia.ObtenerTodos().Select(p => p.entidadBase.Nombre)]);
            Vista.CargarNombresAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(a => a.entidadBase.Nombre)]);
            Vista.CargarTiposMovimientos([.. RepoTipoMovimiento.Instancia.ObtenerTodos().Select(tm => tm.entidadBase)]);

            PopularVistaDesdeEntidad(movimiento);

            Vista.Mostrar();
        }

        public override void PopularVistaDesdeEntidad(Movimiento entidad) {
            base.PopularVistaDesdeEntidad(entidad);

            // Variables auxiliares
            var tipoMovimiento = RepoTipoMovimiento.Instancia.ObtenerPorId(entidad.IdTipoMovimiento);

            Vista.NombreProducto = RepoProducto.Instancia.ObtenerPorId(entidad.IdProducto)?.Nombre ?? string.Empty;
            Vista.Notas = entidad.Notas ?? string.Empty;
            Vista.NombreAlmacenOrigen = RepoAlmacen.Instancia.ObtenerPorId(entidad.IdAlmacenOrigen)?.Nombre ?? string.Empty;
            Vista.NombreAlmacenDestino = RepoAlmacen.Instancia.ObtenerPorId(entidad.IdAlmacenDestino)?.Nombre ?? string.Empty;
            Vista.Fecha = entidad.FechaCreacion;
            Vista.CantidadMovida = entidad.CantidadMovida;
            Vista.NombreTipoMovimiento = tipoMovimiento?.Nombre ?? string.Empty;
        }

        protected override Movimiento? ObtenerEntidadDesdeVista() {
            var producto = RepoProducto.Instancia
                .Buscar(FiltroBusquedaProducto.Nombre, Vista.NombreProducto)
                .resultadosBusqueda
                .FirstOrDefault(p => p.entidadBase.Nombre == Vista.NombreProducto)
                .entidadBase;
            var almacenOrigen = RepoAlmacen.Instancia
                .Buscar(FiltroBusquedaAlmacen.Nombre, Vista.NombreAlmacenOrigen)
                .resultadosBusqueda
                .FirstOrDefault(a => a.entidadBase.Nombre == Vista.NombreAlmacenOrigen)
                .entidadBase;
            var almacenDestino = RepoAlmacen.Instancia
                .Buscar(FiltroBusquedaAlmacen.Nombre, Vista.NombreAlmacenDestino)
                .resultadosBusqueda
                .FirstOrDefault(a => a.entidadBase.Nombre == Vista.NombreAlmacenDestino)
                .entidadBase;
            var tipoMovimiento = RepoTipoMovimiento.Instancia
                .Buscar(FiltroBusquedaTipoMovimiento.Nombre, Vista.NombreTipoMovimiento)
                .resultadosBusqueda
                .FirstOrDefault(tm => tm.entidadBase.Nombre == Vista.NombreTipoMovimiento)
                .entidadBase;
            var costoUnitario = producto.Categoria == CategoriaProducto.ProductoTerminado
                                    ? producto.CostoProduccionUnitario
                                    : producto.CostoAdquisicionUnitario;

            // ✅ Para Carga → el saldo de referencia es el almacén destino
            // ✅ Para Descarga/Transferencia → el saldo de referencia es el almacén origen
            var idAlmacenReferencia = tipoMovimiento?.Efecto == EfectoMovimiento.Carga
                ? almacenDestino?.Id ?? 0
                : almacenOrigen?.Id ?? 0;

            var inventario = RepoInventario.Instancia
                .Buscar(FiltroBusquedaInventario.IdProducto, producto?.Id.ToString())
                .resultadosBusqueda
                .FirstOrDefault(i => i.entidadBase.IdAlmacen == idAlmacenReferencia)
                .entidadBase;

            var saldoInicial = inventario?.Cantidad ?? 0m;
            var signo = tipoMovimiento?.Efecto == EfectoMovimiento.Carga ? 1m : -1m;
            var saldoFinal = saldoInicial + (Vista.CantidadMovida * signo);

            return new Movimiento() {
                Id = Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
                IdProducto = producto?.Id ?? throw new ArgumentNullException(nameof(producto)),
                CostoUnitario = costoUnitario,
                IdAlmacenOrigen = almacenOrigen?.Id ?? 0,
                IdAlmacenDestino = almacenDestino?.Id ?? 0,
                Estado = EstadoMovimiento.Completado,
                FechaCreacion = Vista.Fecha,
                SaldoInicial = saldoInicial,
                FechaTermino = DateTime.MinValue,
                CantidadMovida = Vista.CantidadMovida,
                SaldoFinal = saldoFinal,
                IdTipoMovimiento = tipoMovimiento?.Id ?? throw new ArgumentNullException(nameof(tipoMovimiento)),
                IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                Notas = Vista.Notas ?? string.Empty
            };
        }

        protected override void RegistroEdicionAuxiliar(RepoMovimiento repoMovimiento, long id) {
            // ⚠️ La edición de movimientos no recalcula inventario para evitar
            // doble ajuste. Solo se actualizan las notas y metadatos del registro.
            // Si se necesita corrección de stock, usar un Ajuste de Inventario manual.
            if (Entidad != null && !Vista.ModoEdicion) {
                RepoInventario.Instancia.ModificarInventario(
                    Vista.NombreProducto,
                    Vista.NombreAlmacenOrigen,
                    Vista.NombreAlmacenDestino,
                    Vista.CantidadMovida
                );
            }
        }

        protected override bool EntidadCorrecta() {
            var nombreProductoOk = !string.IsNullOrEmpty(Vista.NombreProducto);
            var tipoMovimientoOk = !string.IsNullOrEmpty(Vista.NombreTipoMovimiento);
            var noCompraventaOk = !(Vista.NombreTipoMovimiento.Equals("Compra") || Vista.NombreTipoMovimiento.Equals("Venta"));
            var tipoMovimiento = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Nombre, Vista.NombreTipoMovimiento).resultadosBusqueda.FirstOrDefault(tm => tm.entidadBase.Nombre.Equals(Vista.NombreTipoMovimiento)).entidadBase;
            var transferenciaAlmacenesIguales = Vista.NombreAlmacenOrigen?.Equals(Vista?.NombreAlmacenDestino) ?? false;

            if (tipoMovimiento != null) {
                switch (tipoMovimiento.Efecto) {
                    case EfectoMovimiento.Carga:
                        if (string.IsNullOrEmpty(Vista.NombreAlmacenDestino) || Vista.NombreAlmacenDestino.Equals("Ninguno")) {
                            CentroNotificaciones.MostrarNotificacion("Debe especificar un almacén de destino para la operación de carga solicitada", TipoNotificacionEnum.Advertencia);
                            return false;
                        }
                        break;
                    case EfectoMovimiento.Descarga:
                        if (string.IsNullOrEmpty(Vista.NombreAlmacenOrigen) || Vista.NombreAlmacenOrigen.Equals("Ninguno")) {
                            CentroNotificaciones.MostrarNotificacion("Debe especificar un almacén de origen para la operación de descarga solicitada", TipoNotificacionEnum.Advertencia);
                            return false;
                        }
                        break;
                    case EfectoMovimiento.Transferencia:
                        if (string.IsNullOrEmpty(Vista.NombreAlmacenOrigen) || string.IsNullOrEmpty(Vista.NombreAlmacenDestino) || Vista.NombreAlmacenOrigen.Equals("Ninguno") || Vista.NombreAlmacenDestino.Equals("Ninguno")) {
                            CentroNotificaciones.MostrarNotificacion("Debe especificar un almacén de origen y un destino para la operación de transferencia solicitada", TipoNotificacionEnum.Advertencia);
                            return false;
                        }
                        if (transferenciaAlmacenesIguales) {
                            CentroNotificaciones.MostrarNotificacion("Error al especificar el origen o destino del producto, ambos almacenes deben tener distinta nomenclatura. Verifique los datos.", TipoNotificacionEnum.Error);
                            return false;
                        }
                        break;
                    default:
                        CentroNotificaciones.MostrarNotificacion("Efecto de movimiento desconocido", TipoNotificacionEnum.Error);
                        return false;
                }
            }

            var cantidadOk = Vista.CantidadMovida > 0;

            if (tipoMovimiento?.Efecto == EfectoMovimiento.Descarga || tipoMovimiento?.Efecto == EfectoMovimiento.Transferencia) {
                if (!string.IsNullOrEmpty(Vista.NombreAlmacenOrigen)) {
                    var producto = RepoProducto.Instancia
                        .Buscar(FiltroBusquedaProducto.Nombre, Vista.NombreProducto)
                        .resultadosBusqueda
                        .FirstOrDefault()
                        .entidadBase;
                    var almacenOrigen = RepoAlmacen.Instancia
                        .Buscar(FiltroBusquedaAlmacen.Nombre, Vista.NombreAlmacenOrigen)
                        .resultadosBusqueda
                        .FirstOrDefault()
                        .entidadBase;
                    var inventarioProducto = RepoInventario.Instancia
                        .Buscar(FiltroBusquedaInventario.IdProducto, producto?.Id.ToString() ?? "0")
                        .resultadosBusqueda
                        .FirstOrDefault(i => i.entidadBase.IdAlmacen == (almacenOrigen?.Id ?? 0))
                        .entidadBase;
                    var cantidadInicialOrigen = inventarioProducto?.Cantidad ?? 0m;

                    if (cantidadInicialOrigen - Vista.CantidadMovida < 0) {
                        CentroNotificaciones.MostrarNotificacion(
                            $"Stock insuficiente: el almacén '{Vista.NombreAlmacenOrigen}' solo tiene " +
                            $"{cantidadInicialOrigen:N2} unidades disponibles.",
                            TipoNotificacionEnum.Advertencia);
                        return false;
                    }
                }
            }

            if (!nombreProductoOk)
                CentroNotificaciones.MostrarNotificacion("El campo de nombre para el producto es obligatorio para el producto, por favor, corrija los datos entrados", TipoNotificacionEnum.Advertencia);
            if (!tipoMovimientoOk)
                CentroNotificaciones.MostrarNotificacion("Debe especificar un tipo de movimiento válido para el movimiento de productos, por favor, corrija los datos entrados", TipoNotificacionEnum.Advertencia);
            if (!noCompraventaOk)
                CentroNotificaciones.MostrarNotificacion("Las operaciones de compraventa no están permitidas directamente desde la sección de movimientos de inventario. Para registrar compras o ventas diríjase al módulo correspondiente", TipoNotificacionEnum.Advertencia);
            if (!cantidadOk)
                CentroNotificaciones.MostrarNotificacion("La cantidad de productos a mover en una operación de carga, descarga o transferencia debe ser mayor que 0, corrija los datos entrados", TipoNotificacionEnum.Advertencia);


            return nombreProductoOk && tipoMovimientoOk && noCompraventaOk && cantidadOk;
        }
    }
}