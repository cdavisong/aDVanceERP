using aDVanceERP.Core.Documentos.Interfaces;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    public interface IVistaTuplaAlmacen : IVistaTupla {
        string Id { get; set; }
        string NombreAlmacen { get; set; }
        string Tipo { get; set; }
        string Direccion { get; set; }
        CoordenadasGeograficas CoordenadasGeograficas { get; set; }
        string Descripcion { get; set; }
        bool Estado { get; set; }
        bool MostrarBotonExportarProductos { get; set; }   

        event EventHandler<(int, FormatoDocumento)>? ExportarDocumentoInventario;
        event EventHandler? DescargarProductos;
    }
}