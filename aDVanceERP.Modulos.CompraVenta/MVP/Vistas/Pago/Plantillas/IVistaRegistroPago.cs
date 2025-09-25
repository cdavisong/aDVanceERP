using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Pago.Plantillas;

public interface IVistaRegistroPago : IVistaRegistro {
    long IdVenta { get; set; }
    string MetodoPago { get; set; }
    decimal Monto { get; set; }
    List<string[]>? Pagos { get; }
    decimal Total { get; set; }
    decimal Suma { get; set; }
    decimal Pendiente { get; set; }
    decimal Devolucion { get; set; }

    event EventHandler? EfectuarTransferencia;
    event EventHandler? PagoAgregado;
    event EventHandler? PagoEliminado;

    void CargarMetodosPago();
}