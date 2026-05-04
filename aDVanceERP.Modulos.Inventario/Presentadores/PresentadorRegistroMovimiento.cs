using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Inventario;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorRegistroMovimiento : PresentadorVistaRegistro<IVistaRegistroMovimiento, Movimiento, RepoMovimiento, FiltroBusquedaMovimiento> {
        public PresentadorRegistroMovimiento(IVistaRegistroMovimiento vista) : base(vista) {
            vista.RegistrarProducto += OnMostrarVistaRegistroProducto;

            AgregadorEventos.Suscribir<EventoMostrarVistaRegistroMovimiento>(OnMostrarVistaRegistroMovimiento);
            AgregadorEventos.Suscribir<EventoMostrarVistaEdicionMovimiento>(OnMostrarVistaEdicionMovimiento);
            AgregadorEventos.Suscribir<EventoProductoRegistrado>(OnNuevoProductoRegistrado);
        }

        private void OnMostrarVistaRegistroMovimiento(EventoMostrarVistaRegistroMovimiento e) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();
            
            CargarDatosComunes(e.EfectoMovimiento);

            Vista.Producto = e.Producto;
            Vista.AlmacenOrigen = e.AlmacenOrigen;
            Vista.AlmacenDestino = e.AlmacenDestino;
            Vista.Mostrar();
        }

        private void OnMostrarVistaEdicionMovimiento(EventoMostrarVistaEdicionMovimiento e) {
            Vista.ModoEdicion = true;
            Vista.Restaurar();

            CargarDatosComunes();
            PopularVistaDesdeEntidad(e.Movimiento);

            Vista.Mostrar();
        }

        private void CargarDatosComunes(EfectoMovimientoEnum efectoMovimiento = EfectoMovimientoEnum.Ninguno) {
            Vista.CargarProductos([.. RepoProducto.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
            Vista.CargarAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
            Vista.CargarTiposMovimientos(efectoMovimiento == EfectoMovimientoEnum.Ninguno 
                ? [.. RepoTipoMovimiento.Instancia
                    .ObtenerTodos()
                    .Select(r => r.entidadBase)]
                : [.. RepoTipoMovimiento.Instancia
                    .ObtenerTodos()
                    .Select(r => r.entidadBase)
                    .Where(tm => tm.Efecto == efectoMovimiento)]);
        }

        private void OnMostrarVistaRegistroProducto(object? sender, EventArgs e) {
            AgregadorEventos.Publicar(new EventoMostrarVistaRegistroProducto());
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

            // Para Carga el saldo de referencia es el almacén destino
            // Para Descarga/Transferencia el saldo de referencia es el almacén origen
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
            if (!Vista.ModoEdicion) {
                AgregadorEventos.Publicar(new EventoMovimientoRegistrado() {
                    Movimiento = Entidad!
                });

                AgregadorEventos.Publicar(new EventoInventarioModificado() {
                    Producto = Vista.Producto!,
                    AlmacenOrigen = Vista.AlmacenOrigen!,
                    AlmacenDestino = Vista.AlmacenDestino!,
                    Cantidad = Vista.CantidadMovida
                });
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

        private void OnNuevoProductoRegistrado(EventoProductoRegistrado e) { 
            CargarDatosComunes();

            Vista.Producto = e.Producto;
        }

        public override void Dispose() {
            Vista.RegistrarProducto -= OnMostrarVistaRegistroProducto;

            AgregadorEventos.Desuscribir<EventoMostrarVistaRegistroMovimiento>(OnMostrarVistaRegistroMovimiento);
            AgregadorEventos.Desuscribir<EventoMostrarVistaEdicionMovimiento>(OnMostrarVistaEdicionMovimiento);
            AgregadorEventos.Desuscribir<EventoProductoRegistrado>(OnNuevoProductoRegistrado);

            base.Dispose();
        }
    }
}