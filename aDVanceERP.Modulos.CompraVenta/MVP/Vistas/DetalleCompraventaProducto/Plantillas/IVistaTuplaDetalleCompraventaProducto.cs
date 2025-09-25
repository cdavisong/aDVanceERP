using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.DetalleCompraventaProducto.Plantillas;

public interface IVistaTuplaDetalleCompraventaProducto : IVistaTupla {
    int Indice { get; set; }
    string IdProducto { get; set; }
    string NombreProducto { get; set; }
    string PrecioCompraventaFinal { get; set; }
    string Cantidad { get; set; }

    event EventHandler? PrecioCompraventaModificado;
}