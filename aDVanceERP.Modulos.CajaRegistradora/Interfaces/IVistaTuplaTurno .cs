using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.CajaRegistradora.Interfaces {
    internal interface IVistaTuplaTurno : IVistaTupla {
        long Id { get; set; }
        string Codigo { get; set; }
        string NombreAlmacen { get; set; }
        string NombreUsuarioApertura { get; set; }
        DateTime FechaApertura { get; set; }
        DateTime? FechaCierre { get; set; }
        decimal MontoApertura { get; set; }
        decimal? MontoEfectivoCalculado { get; set; }
        decimal? MontoEfectivoDeclarado { get; set; }
        decimal? DiferenciaEfectivo { get; set; }
        decimal? MontoTransferenciasCalculado { get; set; }
        decimal? MontoTransferenciasDeclarado { get; set; }
        decimal? DiferenciaTransferencias { get; set; }
        EstadoCajaTurnoEnum Estado { get; set; }

        /// <summary>Solicita abrir la vista de detalle/historial del turno.</summary>
        event EventHandler<long>? VerDetalleTurno;

        /// <summary>Solicita anular el turno (solo si no tiene movimientos).</summary>
        event EventHandler<long>? AnularTurno;
    }
}
