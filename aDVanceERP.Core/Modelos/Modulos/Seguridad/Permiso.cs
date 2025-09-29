using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Seguridad;

public class Permiso : IEntidadBaseDatos {
    public Permiso() { }

    public Permiso(long id, long idModuloAplicacion, string? nombre) {
        Id = id;
        IdModuloAplicacion = idModuloAplicacion;
        Nombre = nombre;
    }
    public long Id { get; set; }
    public long IdModuloAplicacion { get; }
    public string? Nombre { get; }

}

public enum FiltroBusquedaPermiso {
    Todos,
    Id,
    Nombre
}

public static class UtilesBusquedaPermiso {
    public static string[] FiltroBusquedaBusquedaPermiso = {
        "Todos los permisos",
        "Identificador de BD",
        "Nombre del permiso"
    };
}