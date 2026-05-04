using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Documentos;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorTuplaAlmacen : PresentadorVistaTupla<IVistaTuplaAlmacen, Almacen> {
        private DocInventarioAlmacen _docInventarioAlmacen = new DocInventarioAlmacen();

        public PresentadorTuplaAlmacen(IVistaTuplaAlmacen vista, Almacen objeto) : base(vista, objeto) {
            vista.EditarDatosTupla += MostrarVistaEdicionAlmacen;
            vista.ExportarDocumentoInventario += OnExportarDocumentoInventarioAlmacen;
        }

        private void MostrarVistaEdicionAlmacen(object? sender, EventArgs e) {
            var almacen = RepoAlmacen.Instancia.ObtenerPorId(Vista.Id);

            AgregadorEventos.Publicar(new EventoMostrarVistaEdicionAlmacen() {
                Almacen = almacen!
            });
        }

        private void OnExportarDocumentoInventarioAlmacen(object? sender, (long Id, FormatoDocumento Formato) e) {
            _docInventarioAlmacen.GenerarDocumentoConParametros(e.Formato, e.Id);
        }

        public override void Dispose() {
            Vista.EditarDatosTupla -= MostrarVistaEdicionAlmacen;
            Vista.ExportarDocumentoInventario -= OnExportarDocumentoInventarioAlmacen;

            base.Dispose();
        }
    }
}