using aDVanceERP.Core.Documentos.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Almacen.Plantillas;

public interface IVistaTuplaAlmacen : IVistaTupla {
    string Id { get; set; }
    string NombreAlmacen { get; set; }
    string Direccion { get; set; }
    string Descripcion { get; set; }
    bool MostrarBotonExportarProductos { get; set; }

    event EventHandler<(int, FormatoDocumento)>? ExportarDocumentoInventario;
    event EventHandler? DescargarProductos;
}