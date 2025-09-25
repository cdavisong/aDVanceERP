using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Modulos.Contactos.MVP.Modelos; 

public class Mensajero : IEntidadBaseDatos {
    public Mensajero() { }

    public Mensajero(long id, string nombre, bool activo, long idContacto) {
        Id = id;
        Nombre = nombre;
        Activo = activo;
        IdContacto = idContacto;
    }

    public long Id { get; set; }
    public string Nombre { get; set; }
    public bool Activo { get; set; }
    public long IdContacto { get; set; }    
}

public enum FiltroBusquedaMensajero {
    Todos,
    Id,
    Nombre
}

public static class UtilesBusquedaMensajero {
    public static object[] FiltroBusquedaMensajero = {
        "Todos los mensajeros",
        "Identificador de BD",
        "Nombre del mensajero"
    };
}