using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Documentos;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    internal class PresentadorTuplaVenta : PresentadorVistaTupla<IVistaTuplaVenta, Core.Modelos.Modulos.Venta.Venta> {
        private DocFacturaVenta _docFacturaVenta = null!;

        public PresentadorTuplaVenta(IVistaTuplaVenta vista, Core.Modelos.Modulos.Venta.Venta entidad) : base(vista, entidad) {
            vista.EditarDatosTupla += MostrarVistaEdicionVenta;
            vista.ExportarFacturaVenta += OnExportarFacturaVenta;
            vista.AnularVenta += OnAnularVenta;
        }

        private void MostrarVistaEdicionVenta(object? sender, EventArgs e) {
            var entidad = RepoVenta.Instancia.ObtenerPorId(Entidad.Id);

            if (entidad == null) 
                return;

            AgregadorEventos.Publicar("MostrarVistaEdicionVenta", AgregadorEventos.SerializarPayload(entidad));
        }

        private void OnExportarFacturaVenta(object? sender, (long Id, FormatoDocumento Formato) e) {
            _docFacturaVenta = new DocFacturaVenta(e.Id);
            _docFacturaVenta.GenerarDocumentoConParametros(e.Formato);
        }

        private void OnAnularVenta(object? sender, long e) {
            var venta = RepoVenta.Instancia.ObtenerPorId(e)!;
            var detallesVenta = RepoDetalleVentaProducto.Instancia
                .Buscar(FiltroBusquedaDetalleVenta.PorVenta, venta.Id.ToString())
                .resultadosBusqueda
                .Select(r => r.entidadBase)
                .ToList();

            AgregadorEventos.Publicar(
                "VentaAnulada",
                AgregadorEventos.SerializarPayload(new object[] { venta, detallesVenta })
            );

            RepoDetalleVentaProducto.Instancia.EliminarDetallesPorVenta(venta.Id);
            RepoVenta.Instancia.CambiarEstadoVenta(e, EstadoVentaEnum.Anulada);
        }

        public override void Dispose() {
            Vista.EditarDatosTupla -= MostrarVistaEdicionVenta;
            Vista.ExportarFacturaVenta -= OnExportarFacturaVenta;
            Vista.AnularVenta -= OnAnularVenta;

            base.Dispose();
        }
    }
}