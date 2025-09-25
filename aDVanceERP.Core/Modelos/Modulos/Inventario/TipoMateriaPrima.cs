using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario;

public class TipoMateriaPrima : IEntidadBaseDatos {
    public TipoMateriaPrima() {
        Nombre = string.Empty;
        Descripcion = string.Empty;
    }

    public TipoMateriaPrima(long id, string nombre, string descripcion) {
        Id = id;
        Nombre = nombre;
        Descripcion = descripcion;
    }

    public long Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
}

public enum FiltroBusquedaTipoMateriaPrima {
    Todos,
    Id,
    Nombre
}

public static class UtilesBusquedaTiposMateriasPrimas {
    public static object[] FiltroBusquedaTiposProducto = {
        "Todos los tipos de materias primas",
        "Identificador de BD",
        "Nombre del tipo de materia prima"
    };
}