using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Compra;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Compra.Interfaces;

namespace aDVanceERP.Modulos.Compra.Presentadores {
    internal class PresentadorRegistroSolicitudCompra : PresentadorVistaRegistro<IVistaRegistroSolicitudCompra, SolicitudCompra, RepoSolicitudCompra, FiltroBusquedaSolicitudCompra> {
        public PresentadorRegistroSolicitudCompra(IVistaRegistroSolicitudCompra vista) : base(vista) {
            vista.RegistrarNuevoProducto += OnRegistrarNuevoPorducto;

            AgregadorEventos.Suscribir("MostrarVistaRegistroSolicitudCompra", OnMostrarVistaRegistroSolicitudCompra);
            AgregadorEventos.Suscribir("MostrarVistaEdicionSolicitudCompra", OnMostrarVistaEdicionSolicitudCompra);
            AgregadorEventos.Suscribir("NuevoProductoRegistrado", OnProductoRegistrado);
        }

        private void OnRegistrarNuevoPorducto(object? sender, EventArgs e) {
            if (RepoAlmacen.Instancia.Cantidad() == 0) {
                CentroNotificaciones.MostrarNotificacion("No es posible registrar un nuevo producto porque no hay almacenes registrados en el sistema. Por favor, registre al menos un almacén antes de continuar.", TipoNotificacionEnum.Advertencia);
                return;
            }

            AgregadorEventos.Publicar("MostrarVistaRegistroProducto", string.Empty);
        }

        private void OnMostrarVistaRegistroSolicitudCompra(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();

            // Carga inicial de datos
            Vista.CargarNombresProductos([.. RepoProducto.Instancia.ObtenerTodos().Select(p => p.entidadBase.Nombre)]);

            Vista.Mostrar();
        }

        private void OnMostrarVistaEdicionSolicitudCompra(string obj) {
            Vista.ModoEdicion = true;
            Vista.Restaurar();

            if (string.IsNullOrEmpty(obj))
                return;

            var solicitudCompra = AgregadorEventos.DeserializarPayload<SolicitudCompra>(obj);

            if (solicitudCompra == null)
                return;

            // Carga inicial de datos
            Vista.CargarNombresProductos([.. RepoProducto.Instancia.ObtenerTodos().Select(p => p.entidadBase.Nombre)]);

            PopularVistaDesdeEntidad(solicitudCompra);

            Vista.Mostrar();
        }

        private void OnProductoRegistrado(string obj) {
            // Carga de datos al registrar un nuevo producto
            Vista.CargarNombresProductos([.. RepoProducto.Instancia.ObtenerTodos().Select(p => p.entidadBase.Nombre)]);
        }

        protected override SolicitudCompra? ObtenerEntidadDesdeVista() {
            return new SolicitudCompra() {
                Id = 0,
                Codigo = Vista.Codigo,
                IdSolicitante = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                FechaSolicitud = DateTime.Now,
                FechaRequerida = Vista.FechaRequerida,
                Observaciones = Vista.Observaciones,
                Estado = EstadoSolicitudCompraEnum.Pendiente_Aprobacion,
                Activo = true
            };
        }

        protected override async void RegistroEdicionAuxiliar(RepoSolicitudCompra repositorio, long id) {
            var repoProducto = RepoProducto.Instancia;
            var repoDetalleSolicitudCompra = RepoDetalleSolicitudCompra.Instancia;

            foreach (var productoCarrito in Vista.Carrito) {
                // Detalles del pedido
                var producto = repoProducto.ObtenerPorId(productoCarrito.Key);
                var detalleSolicitudCompra = new DetalleSolicitudCompra() {
                    Id = 0,
                    IdSolicitudCompra = id,
                    IdProducto = producto?.Id ?? throw new ArgumentException("Ha ocurrido un error al tratar de registrar los detalles de la venta, uno de los productos del carrito no se encuentra registrado en la base de datos.", nameof(Vista.Carrito)),
                    CantidadSolicitada = productoCarrito.Value.Cantidad,
                    PrecioAdquisicionReferencia = productoCarrito.Value.PrecioUnitario
                };

                repoDetalleSolicitudCompra.Adicionar(detalleSolicitudCompra);
            }
        }
    }
}
