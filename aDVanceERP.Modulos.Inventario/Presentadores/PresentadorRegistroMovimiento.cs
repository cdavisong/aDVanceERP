using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Core.Eventos;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorRegistroMovimiento : PresentadorVistaRegistro<IVistaRegistroMovimiento, Movimiento, RepoMovimiento, FiltroBusquedaMovimiento> {
        public PresentadorRegistroMovimiento(IVistaRegistroMovimiento vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroMovimiento", OnMostrarVistaRegistroMovimiento);
            AgregadorEventos.Suscribir("MostrarVistaEdicionMovimiento", OnMostrarVistaEdicionMovimiento);
            AgregadorEventos.Suscribir("ProductoRegistrado", OnNuevoProductoRegistrado);
        }

        private void OnMostrarVistaRegistroMovimiento(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();

            // Carga inicial de datos
            Vista.CargarProductos([.. RepoProducto.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
            Vista.CargarAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);

            // Carga de datos extra
            if (!string.IsNullOrEmpty(obj)) {
                var datosExtra = AgregadorEventos.DeserializarPayload<object[]>(obj);
                var producto = AgregadorEventos.DeserializarPayload<Producto>(datosExtra[0].ToString());
                var signoMovimiento = datosExtra[1].ToString();

                if (producto != null)
                    Vista.Producto = producto;
                
                if (signoMovimiento == "+" || signoMovimiento == "-") {
                    var tiposMovimiento = RepoTipoMovimiento.Instancia
                        .ObtenerTodos()
                        .Select(r => r.entidadBase)
                        .Where(tm => tm.Efecto == (signoMovimiento == "+" 
                            ? EfectoMovimientoEnum.Carga 
                            : EfectoMovimientoEnum.Descarga))
                        .ToArray();

                    Vista.CargarTiposMovimientos(tiposMovimiento);

                    if (tiposMovimiento.Length == 1)
                        Vista.TipoMovimiento = tiposMovimiento[0];
                }
            } else
                Vista.CargarTiposMovimientos([.. RepoTipoMovimiento.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);

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
            Vista.CargarProductos([.. RepoProducto.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
            Vista.CargarAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
            Vista.CargarTiposMovimientos([.. RepoTipoMovimiento.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);

            PopularVistaDesdeEntidad(movimiento);

            Vista.Mostrar();
        }

        private void OnNuevoProductoRegistrado(string obj) {
            var datos = AgregadorEventos.DeserializarPayload<object[]>(obj);
            var producto = AgregadorEventos.DeserializarPayload<Producto>(datos[0].ToString());
            var almacen = AgregadorEventos.DeserializarPayload<Almacen>(datos[1].ToString());
            var cantidad = AgregadorEventos.DeserializarPayload<decimal>(datos[2].ToString());

            if (producto == null)
                return;

            var costoUnitario = producto!.Categoria == CategoriaProductoEnum.ProductoTerminado
                                    ? producto.CostoProduccionUnitario
                                    : producto.CostoAdquisicionUnitario;

            // Crear movimiento de inventario inicial
            var movimiento = new Movimiento() {
                Id = 0,
                IdProducto = producto.Id,
                CostoUnitario = costoUnitario,
                IdAlmacenOrigen = 0,
                IdAlmacenDestino = almacen?.Id ?? 0,
                Estado = EstadoMovimientoEnum.Completado,
                FechaCreacion = DateTime.Now,
                SaldoInicial = 0,
                FechaTermino = DateTime.Now,
                CantidadMovida = cantidad,
                SaldoFinal = cantidad,
                IdTipoMovimiento = RepoTipoMovimiento.Instancia
                    .Buscar(FiltroBusquedaTipoMovimiento.Nombre, "Carga Inicial")
                    .resultadosBusqueda
                    .Select(r => r.entidadBase)
                    .FirstOrDefault()?.Id ?? throw new InvalidOperationException("No se encontró el tipo de movimiento 'Carga Inicial'."),
                IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                Notas = "Movimiento inicial generado automáticamente al registrar nuevo producto."
            };

            RepoMovimiento.Instancia.Adicionar(movimiento);
            RepoInventario.Instancia.ModificarInventario(
                producto,
                null,
                almacen,
                cantidad
            );
        }

        public override void PopularVistaDesdeEntidad(Movimiento entidad) {
            base.PopularVistaDesdeEntidad(entidad);

            // Variables auxiliares
            var producto = RepoProducto.Instancia.ObtenerPorId(entidad.IdProducto);
            var almacenOrigen = RepoAlmacen.Instancia.ObtenerPorId(entidad.IdAlmacenOrigen);
            var almacenDestino = RepoAlmacen.Instancia.ObtenerPorId(entidad.IdAlmacenDestino);
            var tipoMovimiento = RepoTipoMovimiento.Instancia.ObtenerPorId(entidad.IdTipoMovimiento);

            Vista.Producto = producto;
            Vista.Notas = entidad.Notas ?? string.Empty;
            Vista.AlmacenOrigen = almacenOrigen;
            Vista.AlmacenDestino = almacenDestino;
            Vista.Fecha = entidad.FechaCreacion;
            Vista.CantidadMovida = entidad.CantidadMovida;
            Vista.TipoMovimiento = tipoMovimiento;
        }

        protected override Movimiento? ObtenerEntidadDesdeVista() {
            var costoUnitario = Vista.Producto?.Categoria == CategoriaProductoEnum.ProductoTerminado
                                    ? Vista.Producto?.CostoProduccionUnitario
                                    : Vista.Producto?.CostoAdquisicionUnitario;

            // ✅ Para Carga → el saldo de referencia es el almacén destino
            // ✅ Para Descarga/Transferencia → el saldo de referencia es el almacén origen
            var almacenReferencia = Vista.TipoMovimiento?.Efecto == EfectoMovimientoEnum.Carga
                ? Vista.AlmacenDestino
                : Vista.AlmacenOrigen;

            var inventario = RepoInventario.Instancia
                .Buscar(FiltroBusquedaInventario.IdProducto, Vista.Producto?.Id.ToString())
                .resultadosBusqueda
                .Select(r => r.entidadBase)
                .FirstOrDefault(i => i.IdAlmacen == almacenReferencia?.Id);

            var saldoInicial = inventario?.Cantidad ?? 0m;
            var signo = Vista.TipoMovimiento?.Efecto == EfectoMovimientoEnum.Carga ? 1m : -1m;
            var saldoFinal = saldoInicial + (Vista.CantidadMovida * signo);

            return new Movimiento() {
                Id = Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
                IdProducto = Vista.Producto?.Id ?? throw new ArgumentNullException(nameof(Vista.Producto)),
                CostoUnitario = costoUnitario ?? throw new ArgumentNullException(nameof(costoUnitario)),
                IdAlmacenOrigen = Vista.AlmacenOrigen?.Id ?? 0,
                IdAlmacenDestino = Vista.AlmacenDestino?.Id ?? 0,
                Estado = EstadoMovimientoEnum.Completado,
                FechaCreacion = Vista.Fecha,
                SaldoInicial = saldoInicial,
                FechaTermino = DateTime.Now,
                CantidadMovida = Vista.CantidadMovida,
                SaldoFinal = saldoFinal,
                IdTipoMovimiento = Vista.TipoMovimiento?.Id ?? throw new ArgumentNullException(nameof(Vista.TipoMovimiento)),
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
                    Vista.Producto,
                    Vista.AlmacenOrigen,
                    Vista.AlmacenDestino,
                    Vista.CantidadMovida
                );
            }
        }

        protected override bool EntidadCorrecta() {
            var productoOk = Vista.Producto != null;
            var tipoMovimientoOk = Vista.TipoMovimiento != null;
            var transferenciaAlmacenesIguales = Vista.AlmacenOrigen?.Equals(Vista?.AlmacenDestino) ?? false;

            if (Vista?.TipoMovimiento != null) {
                switch (Vista.TipoMovimiento.Efecto) {
                    case EfectoMovimientoEnum.Carga:
                        if (Vista.AlmacenDestino == null) {
                            CentroNotificaciones.MostrarNotificacion("Debe especificar un almacén de destino para la operación de carga solicitada", TipoNotificacionEnum.Advertencia);
                            return false;
                        }
                        break;
                    case EfectoMovimientoEnum.Descarga:
                        if (Vista.AlmacenOrigen == null) {
                            CentroNotificaciones.MostrarNotificacion("Debe especificar un almacén de origen para la operación de descarga solicitada", TipoNotificacionEnum.Advertencia);
                            return false;
                        }
                        break;
                    case EfectoMovimientoEnum.Transferencia:
                        if (Vista.AlmacenOrigen == null || Vista.AlmacenDestino == null) {
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

            if (Vista.TipoMovimiento?.Efecto == EfectoMovimientoEnum.Descarga || Vista.TipoMovimiento?.Efecto == EfectoMovimientoEnum.Transferencia) {
                if (Vista.AlmacenOrigen != null) {
                    var inventarioProducto = RepoInventario.Instancia
                        .Buscar(FiltroBusquedaInventario.IdProducto, Vista.Producto?.Id.ToString() ?? "0")
                        .resultadosBusqueda
                        .Select(r => r.entidadBase)
                        .FirstOrDefault(i => i.IdAlmacen == (Vista.AlmacenOrigen?.Id ?? 0));
                    var cantidadInicialOrigen = inventarioProducto?.Cantidad ?? 0m;

                    if (cantidadInicialOrigen - Vista.CantidadMovida < 0) {
                        CentroNotificaciones.MostrarNotificacion(
                            $"Stock insuficiente: el almacén '{Vista.AlmacenOrigen}' solo tiene " +
                            $"{cantidadInicialOrigen:N2} unidades disponibles.",
                            TipoNotificacionEnum.Advertencia);
                        return false;
                    }
                }
            }

            if (!productoOk)
                CentroNotificaciones.MostrarNotificacion("El campo de nombre para el producto es obligatorio para el producto, por favor, corrija los datos entrados", TipoNotificacionEnum.Advertencia);
            if (!tipoMovimientoOk)
                CentroNotificaciones.MostrarNotificacion("Debe especificar un tipo de movimiento válido para el movimiento de productos, por favor, corrija los datos entrados", TipoNotificacionEnum.Advertencia);
            if (!cantidadOk)
                CentroNotificaciones.MostrarNotificacion("La cantidad de productos a mover en una operación de carga, descarga o transferencia debe ser mayor que 0, corrija los datos entrados", TipoNotificacionEnum.Advertencia);


            return productoOk && tipoMovimientoOk && cantidadOk;
        }
    }
}