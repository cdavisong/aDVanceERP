using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Modulos.Taller.Modelos;

public class OrdenGastoDinamico : IEntidadBaseDatos {
    public OrdenGastoDinamico() {
        IdOrdenGastoIndirecto = 0;
        Formula = string.Empty;
    }

    public OrdenGastoDinamico(long id, long idOrdenGastoIndirecto, string formula) {
        Id = id;
        IdOrdenGastoIndirecto = idOrdenGastoIndirecto;
        Formula = formula;
    }

    public long Id { get; set; }
    public long IdOrdenGastoIndirecto { get; set; }
    public string Formula { get; set; }
}

public enum FiltroBusquedaOrdenGastoDinamico {
    Todos,
    Id,
    IdOrdenGastoIndirecto
}

public static class UtilesBusquedaOrdenGastoDinamico {
    public static object[] FiltroBusquedaOrdenGastoDinamico =
    {
        "Todos los gastos dinámicos",
        "Identificador de BD",
        "Identificador del gasto indirecto asociado"
    };
}
