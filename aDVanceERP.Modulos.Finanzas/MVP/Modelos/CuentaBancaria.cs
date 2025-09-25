using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Modulos.Finanzas.MVP.Modelos; 

public enum TipoMoneda {
    CUP,
    MLC,
    USD
}

public class CuentaBancaria : IEntidadBaseDatos {
    public CuentaBancaria() { }

    public CuentaBancaria(long id, string alias, string numeroTarjeta, TipoMoneda moneda, long idContacto) {
        Id = id;
        Alias = alias;
        NumeroTarjeta = numeroTarjeta;
        Moneda = moneda;
        IdContacto = idContacto;
    }

    public long Id { get; set; }
    public string? Alias { get; }
    public string? NumeroTarjeta { get; }
    public TipoMoneda Moneda { get; }
    public long IdContacto { get; set; }    
}

public enum FiltroBusquedaCuentaBancaria {
    Todos,
    Id,
    Alias
}

public static class UtilesBusquedaCuentaBancaria {
    public static string[] FiltroBusquedaCuentaBancaria = {
        "Todas las cuentas",
        "Identificador de BD",
        "Alias de la cuenta"
    };
}