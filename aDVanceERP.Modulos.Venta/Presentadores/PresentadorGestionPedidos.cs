using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

using DVanceERP.Modulos.Venta.Vistas;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    public class PresentadorGestionPedidos : PresentadorVistaGestion<PresentadorTuplaPedido, IVistaGestionPedidos, IVistaTuplaPedido, Pedido, RepoPedido, FiltroBusquedaPedido> {
        public PresentadorGestionPedidos(IVistaGestionPedidos vista) : base(vista) {
            RegistrarEntidad += OnRegistrarPedido;
            EditarEntidad += OnEditarPedido;

            AgregadorEventos.Suscribir("MostrarVistaGestionPedidos", OnMostrarVistaGestionPedidos);
            AgregadorEventos.Suscribir("ActivarDesactivarPedido", OnActivarDesactivarPedido);
        }

        private void OnRegistrarPedido(object? sender, EventArgs e) {
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

        private void OnActivarDesactivarPedido(string obj) {
            var idPedidoSeleccionado = _tuplasEntidades.FirstOrDefault(t => t.EstadoSeleccion)?.Vista.Id ?? 0;

            if (idPedidoSeleccionado != 0) {
                var estado = RepoPedido.Instancia.HabilitarDeshabilitarPedido(idPedidoSeleccionado);

                ActualizarResultadosBusqueda();

                CentroNotificaciones.MostrarNotificacion($"La venta ha sido {(estado ? "habilitada" : "deshabilitada")} satisfactoriamente.", TipoNotificacion.Info);
            }
        }

        protected override PresentadorTuplaPedido ObtenerValoresTupla(Pedido entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaPedido(new VistaTuplaPedido(), entidad);
            
            presentadorTupla.Vista.Id = entidad.Id;
            
            presentadorTupla.Vista.Activo = entidad.Activo;

            return presentadorTupla;
        }
    }
}