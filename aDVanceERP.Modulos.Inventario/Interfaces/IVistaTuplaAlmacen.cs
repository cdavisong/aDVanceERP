using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    public interface IVistaTuplaAlmacen : IVistaTupla {
        long Id { get; set; }
        string NombreAlmacen { get; set; }
        string Tipo { get; set; }
        string Direccion { get; set; }
        string Descripcion { get; set; }
        bool Estado { get; set; } 

        event EventHandler<(long Id, FormatoDocumento Formato)>? ExportarDocumentoInventario;
    }
}