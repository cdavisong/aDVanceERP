using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Contactos;

public class Cliente : IEntidadBaseDatos {
    public Cliente() { }

    public Cliente(long id, string? numero, string? razonSocial, long idContacto) {
        Id = id;
        Numero = numero;
        RazonSocial = razonSocial;
        IdContacto = idContacto;
    }

    public long Id { get; set; }
    public string? Numero { get; }
    public string? RazonSocial { get; }
    public long IdContacto { get; set; }
}

public enum FiltroBusquedaCliente {
    Todos,
    Id,
    Numero,
    RazonSocial
}

public static class UtilesBusquedaCliente {
    public static readonly object[] FiltroBusquedaCliente = {
        "Todos los clientes",
        "Identificador de BD",
        "Número del cliente",
        "Razón Social"
    };
}