using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Contactos;

public class Proveedor : IEntidadBaseDatos {
    public Proveedor() { }

    public Proveedor(long id, string razonSocial, string numeroIdentificacionTributaria,
        long idContacto) {
        Id = id;
        RazonSocial = razonSocial;
        NumeroIdentificacionTributaria = numeroIdentificacionTributaria;
        IdContacto = idContacto;
    }

    public long Id { get; set; }
    public string RazonSocial { get; }
    public string? NumeroIdentificacionTributaria { get; }
    public long IdContacto { get; set; }
}

public enum FiltroBusquedaProveedor {
    Todos,
    Id,
    RazonSocial,
    NIT
}

public static class UtilesBusquedaProveedor {
    public static object[] FiltroBusquedaProveedor = {
        "Todos los proveedores",
        "Identificador de BD",
        "Razón Social del proveedor",
        "No. Identificación Tributaria"
    };
}