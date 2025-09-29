using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Contactos;

public class TelefonoContacto : IEntidadBaseDatos {
    public TelefonoContacto() {
        Prefijo = "+53";
        Numero = "00000000";
    }

    public TelefonoContacto(long id, string prefijo, string numero, CategoriaTelefonoContacto categoria, long idContacto) {
        Id = id;
        Prefijo = prefijo;
        Numero = numero;
        Categoria = categoria;
        IdContacto = idContacto;
    }

    public long Id { get; set; }
    public string Prefijo { get; }
    public string Numero { get; set; }
    public CategoriaTelefonoContacto Categoria { get; set; }
    public long IdContacto { get; }
}

public enum FiltroBusquedaTelefonoContacto {
    Todos,
    Id,
    Numero,
    IdContacto
}