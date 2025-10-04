using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos;

public class Modulo : IEntidadBaseDatos {
    public Modulo() {
        Nombre = string.Empty;
    }

    public Modulo(long id, string nombre) {
        Id = id;
        Nombre = nombre;
    }

    public long Id { get; set; }
    public string Nombre { get; set; }
}

public enum FiltroBusquedaModulo {
    Todos,
    Id,
    Nombre
}
