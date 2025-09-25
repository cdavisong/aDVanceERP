using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario;

public class UnidadMedida : IEntidadBaseDatos {
    public UnidadMedida() {
        Nombre = string.Empty;
        Abreviatura = string.Empty;
        Descripcion = string.Empty;
    }

    public UnidadMedida(long id, string nombre, string abreviatura, string descripcion) {
        Id = id;
        Nombre = nombre;
        Abreviatura = abreviatura;
        Descripcion = descripcion;
    }

    public long Id { get; set; }
    public string Nombre { get; set; }
    public string Abreviatura { get; set; }
    public string Descripcion { get; set; }
}

public enum FiltroBusquedaUnidadMedida {
    Todos,
    Id,
    Nombre,
    Abreviatura
}

public static class UtilesBusquedaUnidadesMedida {
    public static object[] FiltroBusquedaUnidadesMedida = {
        "Todas las unidades de medida",
        "Identificador de BD",
        "Nombre de la unidad de medida",
        "Abreviatura de la unidad de medida"
    };
}

