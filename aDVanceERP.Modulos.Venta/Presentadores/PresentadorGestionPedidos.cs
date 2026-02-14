using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

using aDVanceERP.Modulos.Venta.Vistas;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    public class PresentadorGestionPedidos : PresentadorVistaGestion<PresentadorTuplaPedido, IVistaGestionPedidos, IVistaTuplaPedido, Pedido, RepoPedido, FiltroBusquedaPedido> {
        public PresentadorGestionPedidos(IVistaGestionPedidos vista) : base(vista) {
            RegistrarEntidad += OnRegistrarPedido;
            EditarEntidad += OnEditarPedido;

            AgregadorEventos.Suscribir("MostrarVistaGestionPedidos", OnMostrarVistaGestionPedidos);
            AgregadorEventos.Suscribir("HabilitarDeshabilitarPedido", OnHabilitarDeshabilitarPedido);
        }

        private void OnRegistrarPedido(object? sender, EventArgs e) {
            if (RepoProducto.Instancia.Cantidad() == 0) {
                CentroNotificaciones.MostrarNotificacion("No es posible registrar un nuevo pedido porque no hay productos registrados en el sistema. Por favor, registre al menos un producto antes de continuar.", TipoNotificacion.Advertencia);
                return;
            }

            AgregadorEventos.Publicar("MostrarVistaRegistroPedido", string.Empty);
        }

        private void OnEditarPedido(object? sender, Pedido e) {
            AgregadorEventos.Publicar("MostrarVistaEdicionPedido", AgregadorEventos.SerializarPayload(e));
        }

        private void OnMostrarVistaGestionPedidos(string obj) {
            Vista.CargarFiltrosBusqueda(UtilesBusquedaPedido.FiltroBusquedaPedido);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        private void OnHabilitarDeshabilitarPedido(string obj) {
            var idPedidoSeleccionado = _tuplasEntidades.FirstOrDefault(t => t.EstadoSeleccion)?.Vista.Id ?? 0;

            if (idPedidoSeleccionado != 0) {
                var estado = RepoPedido.Instancia.HabilitarDeshabilitarPedido(idPedidoSeleccionado);

                ActualizarResultadosBusqueda();

                CentroNotificaciones.MostrarNotificacion($"El pedido ha sido {(estado ? "habilitado" : "deshabilitado")} satisfactoriamente.", TipoNotificacion.Info);
            }
        }

        protected override PresentadorTuplaPedido ObtenerValoresTupla(Pedido entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaPedido(new VistaTuplaPedido(), entidad);
            var cliente = RepoCliente.Instancia.ObtenerPorId(entidad.IdCliente);
            var persona = RepoPersona.Instancia.Buscar(FiltroBusquedaPersona.Id, cliente?.IdPersona.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.FechaPedido = entidad.FechaPedido;
            presentadorTupla.Vista.NombreCliente = persona?.NombreCompleto ?? "Anónimo";
            presentadorTupla.Vista.FechaEntrega = entidad.FechaEntregaSolicitada ?? DateTime.MinValue;
            presentadorTupla.Vista.DireccionEntrega = entidad.DireccionEntrega!; // TODO: La dirección de entrega es obligatoria para los pedidos
            presentadorTupla.Vista.ImporteTotal = entidad.TotalPedido;
            presentadorTupla.Vista.Activo = entidad.Activo;
            presentadorTupla.Vista.EstadoPedido = entidad.EstadoPedido;

            return presentadorTupla;
        }
    }
}