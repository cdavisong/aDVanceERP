using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    public interface IVistaTuplaEnvio : IVistaTupla {
        long Id { get; set; }
        long IdVenta { get; set; }
        string NumeroFacturaVenta { get; set; }
        long? IdMensajero { get; set; }
        string? NombreMensajero { get; set; }
        TipoEnvioEnum TipoEnvio { get; set; }
        DateTime FechaAsignacion { get; set; }
        DateTime FechaEntregaRealizada { get; set; }
        string? ObservacionesEntrega { get; set; }
        decimal MontoCobradoAlCliente { get; set; }
        EstadoEntregaEnum EstadoEntrega { get; set; }
    }
}