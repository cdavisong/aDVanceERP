using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Inventario.Documentos;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorTuplaAlmacen : PresentadorVistaTupla<IVistaTuplaAlmacen, Almacen> {
        private DocInventarioAlmacen _docInventarioAlmacen = new DocInventarioAlmacen();

        public PresentadorTuplaAlmacen(IVistaTuplaAlmacen vista, Almacen objeto) : base(vista, objeto) {
            vista.ExportarDocumentoInventario += OnExportarDocumentoInventarioAlmacen;
        }

        private void OnExportarDocumentoInventarioAlmacen(object? sender, (int id, FormatoDocumento formato) e) {
            _docInventarioAlmacen.GenerarDocumentoConParametros(e.formato, e.id);
        }

        public override void Dispose() {
            Vista.ExportarDocumentoInventario -= OnExportarDocumentoInventarioAlmacen;

            base.Dispose();
        }
    }
}