using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

using aDVanceERP.Modulos.Venta.Vistas;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    public class PresentadorGestionVentas : PresentadorVistaGestion<PresentadorTuplaVenta, IVistaGestionVentas, IVistaTuplaVenta, Core.Modelos.Modulos.Venta.Venta, RepoVenta, FiltroBusquedaVenta> {
        public PresentadorGestionVentas(IVistaGestionVentas vista) : base(vista) {
            RegistrarEntidad += OnRegistrarVenta;
            EditarEntidad += OnEditarVenta;

            AgregadorEventos.Suscribir("MostrarVistaGestionVentas", OnMostrarVistaGestionVentas);
            AgregadorEventos.Suscribir("HabilitarDeshabilitarVenta", OnHabilitarDeshabilitarVenta);
        }

        private void OnRegistrarVenta(object? sender, EventArgs e) {
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
            presentadorTupla.Vista.FechaVenta = entidad.FechaVenta;
            presentadorTupla.Vista.NombreCliente = persona.NombreCompleto ?? "Anónimo";
            presentadorTupla.Vista.MetodoPagoPrincipal = entidad.MetodoPagoPrincipal;
            presentadorTupla.Vista.TotalBruto = entidad.TotalBruto;
            presentadorTupla.Vista.DescuentoTotal = entidad.DescuentoTotal;
            presentadorTupla.Vista.ImpuestoTotal = entidad.ImpuestoTotal;
            presentadorTupla.Vista.ImporteTotal = entidad.ImporteTotal;            
            presentadorTupla.Vista.Activo = entidad.Activo;
            presentadorTupla.Vista.EstadoVenta = entidad.EstadoVenta;

            return presentadorTupla;
        }
    }
}