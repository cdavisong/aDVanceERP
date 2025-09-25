using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Compra.Plantillas;

public interface IVistaTuplaCompra : IVistaTupla {
    string Id { get; set; }
    string Fecha { get; set; }
    string NombreAlmacen { get; set; }
    string NombreProveedor { get; set; }
    string CantidadProductos { get; set; }
    string MontoTotal { get; set; }
}