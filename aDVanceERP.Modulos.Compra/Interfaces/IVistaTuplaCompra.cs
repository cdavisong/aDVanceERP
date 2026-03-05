using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Compra.Interfaces {
    internal interface IVistaTuplaCompra : IVistaTupla {
        long Id { get; set; }
        string Codigo { get; set; }
        long IdProveedor { get; set; }
        long? IdSolicitudCompra { get; set; }
        long? IdEmpleadoComprador { get; set; }
        long IdAlmacenDestino { get; set; }
        long? IdTipoCompra { get; set; }
        DateTime FechaOrden { get; set; }
        DateTime FechaEntregaEsperada { get; set; }
        string CondicionesPago { get; set; }
        decimal Subtotal { get; set; }
        decimal ImpuestoTotal { get; set; }
        decimal TotalCompra { get; set; }
        EstadoCompraEnum EstadoCompra { get; set; }
        DateTime FechaAprobacion { get; set; }
        long? AprobadoPor { get; set; }
        string Observaciones { get; set; }
        bool Activo { get; set; }

        event EventHandler<(long, FormatoDocumento)>? ExportarFacturaCompra;
        event EventHandler<long>? AnularCompra;
    }
}