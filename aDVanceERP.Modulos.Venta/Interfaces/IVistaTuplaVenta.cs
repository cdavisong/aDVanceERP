using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    internal interface IVistaTuplaVenta : IVistaTupla {
        long Id { get; set; }
        string NumeroFacturaVenta { get; set; }
        DateTime FechaVenta { get; set; }
        string NombreCliente { get; set; }
        string? MetodoPagoPrincipal { get; set; }
        decimal TotalBruto { get; set; }
        decimal DescuentoTotal { get; set; }
        decimal ImpuestoTotal { get; set; }
        decimal ImporteTotal { get; set; }
        EstadoVentaEnum EstadoVenta { get; set; }
        bool Activo { get; set; }

        event EventHandler<(long, FormatoDocumento)>? ExportarFacturaVenta;
        event EventHandler<long>? AnularVenta;
    }
}