using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Contactos;

public class Contacto : IEntidadBaseDatos {
    public Contacto() { }

    public Contacto(long id, string nombre, string direccionCorreoElectronico, string direccion, string notas) {
        Id = id;
        Nombre = nombre;
        DireccionCorreoElectronico = direccionCorreoElectronico;
        Direccion = direccion;
        Notas = notas;
    }

    public long Id { get; set; }
    public string? Nombre { get; set; }
    public string? DireccionCorreoElectronico { get; set; }
    public string? Direccion { get; set; }
    public string? Notas { get; set; }
}

public enum FiltroBusquedaContacto {
    Todos,
    Id,
    Nombre
}

public static class UtilesBusquedaContacto {
    public static string[] FiltroBusquedaContacto = {
        "Todos los contactos",
        "Identificador de BD",
        "Nombre del contacto"
    };
}