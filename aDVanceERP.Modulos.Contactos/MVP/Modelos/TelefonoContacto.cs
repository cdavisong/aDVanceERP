using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Modulos.Contactos.MVP.Modelos; 

public enum CategoriaTelefonoContacto {
    Otro,
    Fijo,
    Movil
}

public class TelefonoContacto : IEntidadBaseDatos {
    public TelefonoContacto() { }

    public TelefonoContacto(long idTelefonoContacto, string prefijo, string numero, CategoriaTelefonoContacto categoria,
        long idContacto) {
        Id = idTelefonoContacto;
        Prefijo = prefijo;
        Numero = numero;
        Categoria = categoria;
        IdContacto = idContacto;
    }

    public string? Prefijo { get; }
    public string? Numero { get; set; }
    public CategoriaTelefonoContacto Categoria { get; set; }
    public long IdContacto { get; }

    public long Id { get; set; }
}

public enum FiltroBusquedaTelefonoContacto {
    Todos,
    Id,
    Numero,
    IdContacto
}