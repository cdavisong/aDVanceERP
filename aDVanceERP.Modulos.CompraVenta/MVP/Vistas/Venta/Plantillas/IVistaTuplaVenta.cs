using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Venta.Plantillas;

public interface IVistaTuplaVenta : IVistaTupla {
    string Id { get; set; }
    string Fecha { get; set; }
    string NombreAlmacen { get; set; }
    string NombreCliente { get; set; }
    string CantidadProductos { get; set; }
    string MontoTotal { get; set; }
    string? EstadoEntrega { get; set; }
    string EstadoPago { get; set; }

    event EventHandler? DescargarFactura;
}