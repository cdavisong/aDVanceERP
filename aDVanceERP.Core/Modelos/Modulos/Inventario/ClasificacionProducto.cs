using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario;

public class ClasificacionProducto : IEntidadBaseDatos {
    public ClasificacionProducto() {
        Nombre = string.Empty;
        Descripcion = string.Empty;
    }

    public ClasificacionProducto(long id, string nombre, string descripcion) {
        Id = id;
        Nombre = nombre;
        Descripcion = descripcion;
    }

    public long Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
}

public enum FiltroBusquedaClasificacionProducto {
    Todos,
    Id,
    Nombre
}

public static class UtilesBusquedaClasificacionProducto {
    public static object[] FiltroBusquedaTiposProducto = {
        "Todas las clasificaciones de producto",
        "Identificador de BD",
        "Nombre de la clasificación de producto"
    };
}