using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Modulos.Contactos.MVP.Modelos;

public class Proveedor : IEntidadBaseDatos {
    public Proveedor() { }

    public Proveedor(long idProveedor, string razonSocial, string numeroIdentificacionTributaria,
        long idContacto) {
        Id = idProveedor;
        RazonSocial = razonSocial;
        NumeroIdentificacionTributaria = numeroIdentificacionTributaria;
        IdContacto = idContacto;
    }

    public long Id { get; set; }
    public string? RazonSocial { get; }
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