using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario;

public class Inventario : IEntidadBaseDatos {
    public Inventario() { }

    public Inventario(long idInventario, long idProducto, long idAlmacen, decimal cantidad, decimal costoPromedio, decimal valorTotal, DateTime ultimaActualizacion) {
        Id = idInventario;
        IdProducto = idProducto;
        IdAlmacen = idAlmacen;
        Cantidad = cantidad;
        CostoPromedio = costoPromedio;
        ValorTotal = valorTotal;
        UltimaActualizacion = ultimaActualizacion;
    }

    public long Id { get; set; }
    public long IdProducto { get; set; }
    public long IdAlmacen { get; set; }
    public decimal Cantidad { get; set; }
    public decimal CostoPromedio { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime UltimaActualizacion { get; set; }
}

public enum FiltroBusquedaInventario {
    Todos,
    Id,
    IdProducto,
    IdAlmacen
}