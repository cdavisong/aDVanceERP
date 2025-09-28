using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Compraventa;

public class EstadoEntrega : IEntidadBaseDatos {
    public EstadoEntrega() {
        Nombre = "NULL";
        Descripcion = "No disponible";
    }

    public EstadoEntrega(long id, string nombre, string descripcion, int orden) {
        Id = id;
        Nombre = nombre;
        Descripcion = descripcion;
        Orden = orden;
    }

    public long Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int Orden { get; set; }
}

public enum FiltroBusquedaEstadoEntrega {
    Todos,
    Id,
    Nombre
}

public static class UtilidadesEstadoEntrega {
    public static object[] CriterioEstadoEntrega = {
        "Todos los estados",
        "Identificador de BD",
        "Nombre del estado"
    };
}