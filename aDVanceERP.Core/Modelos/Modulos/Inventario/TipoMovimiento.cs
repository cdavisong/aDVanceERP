using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario;

public class TipoMovimiento : IEntidadBaseDatos {
    public TipoMovimiento() {
        Nombre = string.Empty;
        Efecto = EfectoMovimiento.Ninguno;
    }

    public TipoMovimiento(long id, string nombre, EfectoMovimiento efecto) {
        Id = id;
        Nombre = nombre;
        Efecto = efecto;
    }

    public long Id { get; set; }
    public string Nombre { get; set; }
    public EfectoMovimiento Efecto { get; set; }
}

public enum FiltroBusquedaTipoMovimiento {
    Todos,
    Id,
    Nombre
}

public static class UtilesBusquedaTipoMovimiento {
    public static string[] FiltroBusquedaTipoMovimiento = {
        "Todos los tipos de movimiento",
        "Identificador de BD",
        "Nombre del movimiento"
    };
}