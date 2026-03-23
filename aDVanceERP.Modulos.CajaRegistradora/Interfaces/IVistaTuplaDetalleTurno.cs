using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.CajaRegistradora.Interfaces {
    internal interface IVistaTuplaDetalleTurno : IVistaTupla {
        long Id { get; set; }
        string FechaMovimiento { get; set; }
        TipoMovimientoCajaEnum Tipo { get; set; }
        CanalPagoCajaEnum CanalPago { get; set; }
        string? DescripcionFactura { get; set; }
        string Operador { get; set; }
        decimal Monto { get; set; }
    }
}
