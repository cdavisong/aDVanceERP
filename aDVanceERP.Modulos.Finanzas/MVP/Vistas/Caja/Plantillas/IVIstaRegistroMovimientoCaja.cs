using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja.Plantillas;

public interface IVIstaRegistroMovimientoCaja : IVistaRegistro {
    DateTime Fecha { get; set; }
    decimal Monto { get; set; }
    string TipoMovimiento { get; set; }
    string Concepto { get; set; }
    string? Observaciones { get; set; }
}