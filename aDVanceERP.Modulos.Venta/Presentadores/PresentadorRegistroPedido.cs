using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    internal class PresentadorRegistroPedido : PresentadorVistaRegistro<IVistaRegistroPedido, Pedido, RepoPedido, FiltroBusquedaPedido> {
        public PresentadorRegistroPedido(IVistaRegistroPedido vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroPedido", OnMostrarVistaRegistroPedido);
            AgregadorEventos.Suscribir("MostrarVistaEdicionPedido", OnMostrarVistaEdicionPedido);
        }

        private void OnMostrarVistaRegistroPedido(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();

            // Carga inicial de datos
            Vista.CargarNombresClientes([.. RepoCliente.Instancia.ObtenerNombres()]);
            Vista.CargarNombresProductos([.. RepoProducto.Instancia.ObtenerTodos().Select(p => p.entidadBase.Nombre)]);

            Vista.Mostrar();
        }

        private void OnMostrarVistaEdicionPedido(string obj) {
            Vista.ModoEdicion = true;
            Vista.Restaurar();

            if (string.IsNullOrEmpty(obj))
                return;

            var venta = AgregadorEventos.DeserializarPayload<Pedido>(obj);

            if (venta == null)
                return;

            // Carga inicial de datos
            Vista.CargarNombresClientes([.. RepoCliente.Instancia.ObtenerNombres()]);
            Vista.CargarNombresProductos([.. RepoProducto.Instancia.ObtenerTodos().Select(p => p.entidadBase.Nombre)]);

            PopularVistaDesdeEntidad(venta);

            Vista.Mostrar();
        }

        protected override Pedido? ObtenerEntidadDesdeVista() {
            var persona = RepoPersona.Instancia.Buscar(Core.Modelos.Modulos.Maestros.FiltroBusquedaPersona.NombreCompleto, Vista.NombreCliente).resultadosBusqueda.FirstOrDefault().entidadBase;
            var cliente = RepoCliente.Instancia.Buscar(FiltroBusquedaCliente.IdPersona, persona?.Id.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;
            
            return new Pedido() {
                Id = 0,
                Codigo = Vista.Codigo,
                IdCliente = cliente?.Id ?? 0,
                IdEmpleadoVendedor = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0, // TODO: Integrar con empleados autenticados
                FechaPedido = DateTime.Today,
                FechaEntregaSolicitada = Vista.FechaEntregaSolicitada,
                DireccionEntrega = Vista.DireccionEntrega,
                TotalPedido = Vista.ImporteEstimado,
                EstadoPedido = EstadoPedidoEnum.Pendiente,
                ObservacionesPedido = Vista.ObservacionesPedido,
                Activo = true
            };
        }

        protected override void RegistroEdicionAuxiliar(RepoPedido repositorio, long id) {
            var repoProducto = RepoProducto.Instancia;
            var repoDetallePedido = RepoDetallePedidoProducto.Instancia;
            
            foreach (var productoCarrito in Vista.Carrito) {
                // Detalles del pedido
                var producto = repoProducto.ObtenerPorId(productoCarrito.Key);
                var subtotal = productoCarrito.Value.CostoGeneral * productoCarrito.Value.Cantidad;
                var detallePedido = new DetallePedidoProducto() {
                    Id = 0,
                    IdPedido = id,
                    IdProducto = producto?.Id ?? throw new ArgumentException("Ha ocurrido un error al tratar de registrar los detalles de la venta, uno de los productos del carrito no se encuentra registrado en la base de datos.", nameof(Vista.Carrito)),
                    CantidadSolicitada = productoCarrito.Value.Cantidad,
                    PrecioVentaReferencia = producto.PrecioVentaBase,
                    Subtotal = subtotal
                };
                                
                repoDetallePedido.Adicionar(detallePedido);
            }
        }
    }
}
