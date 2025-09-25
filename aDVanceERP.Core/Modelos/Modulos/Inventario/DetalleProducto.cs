using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario;

public class DetalleProducto : IEntidadBaseDatos {
    public DetalleProducto() {
        IdUnidadMedida = 0;
        Descripcion = "No hay descripción disponible";
    }

    public DetalleProducto(long id, long idUnidadMedida, string descripcion) {
        Id = id;
        IdUnidadMedida = idUnidadMedida;
        Descripcion = descripcion ?? "No hay descripción disponible";
    }

    public long Id { get; set; }
    public long IdUnidadMedida { get; set; }
    public string? Descripcion { get; set; }
}

public enum FiltroBusquedaDetalleProducto {
    Todos,
    Id,
    UnidadMedida,
    Descripcion
}

public static class UtilesBusquedaDetallesProducto {
    public static object[] FiltroBusquedaDetallesProducto = {
        "Todos los detalles de productos",
        "Identificador de BD",
        "Unidad de medida del producto",
        "Descripción del producto"
    };
}
