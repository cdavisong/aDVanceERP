using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Compra.Interfaces {
    internal interface IVistaTuplaSolicitudCompra : IVistaTupla {
        long Id { get; set; }
        string Codigo { get; set; }
        string NombreSolicitante { get; set; }
        DateTime FechaSolicitud { get; set; }
        DateTime FechaRequerida { get; set; }
        string Observaciones { get; set; }
        EstadoSolicitudCompraEnum Estado { get; set; }
        bool Activo { get; set; }

        event EventHandler<(long idSolicitudCompra, EstadoSolicitudCompraEnum estado)> CambioEstadoSolicitudCompra;
    }
}
