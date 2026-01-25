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
            Vista.CargarNombresProductos(RepoProducto.Instancia.ObtenerTodos().Select(p => p.entidadBase.Nombre).ToArray());
            Vista.CargarNombresAlmacenes(RepoAlmacen.Instancia.ObtenerTodos().Select(a => a.entidadBase.Nombre).ToArray());

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
                            CentroNotificaciones.Mostrar("Debe especificar un almacén de destino para la operación de carga solicitada", TipoNotificacion.Advertencia);
                            return false;
                        }
                        break;
                    case EfectoMovimiento.Descarga:
                        if (string.IsNullOrEmpty(Vista.NombreAlmacenOrigen) || Vista.NombreAlmacenOrigen.Equals("Ninguno")) {
                            CentroNotificaciones.Mostrar("Debe especificar un almacén de origen para la operación de descarga solicitada", TipoNotificacion.Advertencia);
                            return false;
                        }
                        break;
                    case EfectoMovimiento.Transferencia:
                        if (string.IsNullOrEmpty(Vista.NombreAlmacenOrigen) || string.IsNullOrEmpty(Vista.NombreAlmacenDestino) || Vista.NombreAlmacenOrigen.Equals("Ninguno") || Vista.NombreAlmacenDestino.Equals("Ninguno")) {
                            CentroNotificaciones.Mostrar("Debe especificar un almacén de origen y un destino para la operación de transferencia solicitada", TipoNotificacion.Advertencia);
                            return false;
                        }
                        if (transferenciaAlmacenesIguales) {
                            CentroNotificaciones.Mostrar("Error al especificar el origen o destino del producto, ambos almacenes deben tener distinta nomenclatura. Verifique los datos.", TipoNotificacion.Error);
                            return false;
                        }
                        break;
                    default:
                        CentroNotificaciones.Mostrar("Efecto de movimiento desconocido", TipoNotificacion.Error);
                        return false;
                }
            }

            var cantidadOk = Vista.CantidadMovida > 0;

            if (tipoMovimiento?.Efecto == EfectoMovimiento.Descarga || tipoMovimiento?.Efecto == EfectoMovimiento.Transferencia) {
                if (!string.IsNullOrEmpty(Vista.NombreAlmacenOrigen)) {
                    var producto = RepoProducto.Instancia.Buscar(FiltroBusquedaProducto.Nombre, Vista.NombreProducto).resultadosBusqueda.FirstOrDefault().entidadBase;
                    var almacenOrigen = RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, Vista.NombreAlmacenOrigen).resultadosBusqueda.FirstOrDefault().entidadBase;
                    var inventarioProducto = RepoInventario.Instancia.Buscar(FiltroBusquedaInventario.IdProducto, producto?.Id.ToString() ?? "0").resultadosBusqueda.FirstOrDefault(i => i.entidadBase.IdAlmacen.Equals(almacenOrigen?.Id ?? 0m)).entidadBase;
                    var cantidadInicialOrigen = inventarioProducto?.Cantidad;

                    if (cantidadInicialOrigen - Vista.CantidadMovida < 0) {
                        CentroNotificaciones.Mostrar($"No se puede mover una cantidad de productos hacia el destino menor que la cantidad orígen ({cantidadInicialOrigen} unidades) en el almacén {Vista.NombreAlmacenOrigen}", TipoNotificacion.Advertencia);
                        return false;
                    }
                }
            }

            if (!nombreProductoOk)
                CentroNotificaciones.Mostrar("El campo de nombre para el producto es obligatorio para el producto, por favor, corrija los datos entrados", TipoNotificacion.Advertencia);
            if (!tipoMovimientoOk)
                CentroNotificaciones.Mostrar("Debe especificar un tipo de movimiento válido para el movimiento de productos, por favor, corrija los datos entrados", TipoNotificacion.Advertencia);
            if (!noCompraventaOk)
                CentroNotificaciones.Mostrar("Las operaciones de compraventa no están permitidas directamente desde la sección de movimientos de inventario. Para registrar compras o ventas diríjase al módulo correspondiente", TipoNotificacion.Advertencia);
            if (!cantidadOk)
                CentroNotificaciones.Mostrar("La cantidad de productos a mover en una operación de carga, descarga o transferencia debe ser mayor que 0, corrija los datos entrados", TipoNotificacion.Advertencia);


            return nombreProductoOk && tipoMovimientoOk && noCompraventaOk && cantidadOk;
        }

        protected override void RegistroEdicionAuxiliar(RepoMovimiento repoMovimiento, long id) {
            if (Entidad != null)
                RepoInventario.Instancia.ModificarInventario(
                    Vista.NombreProducto,
                    Vista.NombreAlmacenOrigen,
                    Vista.NombreAlmacenDestino,
                    Vista.CantidadMovida
                );
        }

        protected override Movimiento? ObtenerEntidadDesdeVista() {
            var producto = RepoProducto.Instancia.Buscar(FiltroBusquedaProducto.Nombre, Vista.NombreProducto).resultadosBusqueda.FirstOrDefault(p => p.entidadBase.Nombre.Equals(Vista.NombreProducto)).entidadBase;
            var almacenOrigen = RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, Vista.NombreAlmacenOrigen).resultadosBusqueda.FirstOrDefault(a => a.entidadBase.Nombre.Equals(Vista.NombreAlmacenOrigen)).entidadBase;
            var almacenDestino = RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, Vista.NombreAlmacenDestino).resultadosBusqueda.FirstOrDefault(a => a.entidadBase.Nombre.Equals(Vista.NombreAlmacenDestino)).entidadBase;
            var inventario = RepoInventario.Instancia.Buscar(FiltroBusquedaInventario.IdProducto, producto?.Id.ToString()).resultadosBusqueda.FirstOrDefault(i => i.entidadBase.IdAlmacen.Equals(almacenOrigen?.Id) || i.entidadBase.IdAlmacen.Equals(almacenDestino?.Id)).entidadBase;
            var costoUnitario = producto.Categoria == CategoriaProducto.ProductoTerminado ? producto.CostoProduccionUnitario : producto.CostoAdquisicionUnitario;
            var tipoMovimiento = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Nombre, Vista.NombreTipoMovimiento).resultadosBusqueda.FirstOrDefault(tm => tm.entidadBase.Nombre.Equals(Vista.NombreTipoMovimiento)).entidadBase;
            var saldoFinal = (inventario?.Cantidad ?? 0) + (Vista.CantidadMovida * (tipoMovimiento?.Efecto == EfectoMovimiento.Carga ? 1 : -1));

            return new Movimiento() { 
                Id = Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
                IdProducto = producto?.Id ?? throw new ArgumentNullException("El producto especificado no es válido"),
                CostoUnitario = costoUnitario,
                IdAlmacenOrigen = almacenOrigen?.Id ?? 0,
                IdAlmacenDestino = almacenDestino?.Id ?? 0,
                Estado = EstadoMovimiento.Pendiente,
                FechaCreacion = Vista.Fecha,
                SaldoInicial = inventario?.Cantidad ?? 0,
                FechaTermino = DateTime.MinValue,
                CantidadMovida = Vista.CantidadMovida,
                SaldoFinal = saldoFinal,
                IdTipoMovimiento = tipoMovimiento?.Id ?? throw new ArgumentNullException("El tipo de movimiento especificado no es válido"),
                IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                Notas = Vista.Notas ?? string.Empty
            };
        }
    }
}