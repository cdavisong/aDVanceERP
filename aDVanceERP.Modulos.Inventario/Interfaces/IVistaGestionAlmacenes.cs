using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    public interface IVistaGestionAlmacenes : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaAlmacen>, INavegadorTuplasEntidades {
        bool MostrarBtnImportarInventarioVersat { get; set; }

        event EventHandler<string>? ImportarInventarioVersat;
        event EventHandler<FormatoDocumento>? ExportarDocumentoInventario;
    }
}