using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Compraventa;

public class HistorialEntrega : IEntidadBaseDatos {
    public HistorialEntrega() { }

    public HistorialEntrega(long id, long idSeguimientoEntrega, long idEstadoEntrega, DateTime fechaRegistro, long idUsuario, string? observaciones) {
        Id = id;
        IdSeguimientoEntrega = idSeguimientoEntrega;
        IdEstadoEntrega = idEstadoEntrega;
        FechaRegistro = fechaRegistro;
        IdUsuario = idUsuario;
        Observaciones = observaciones;
    }

    public long Id { get; set; }
    public long IdSeguimientoEntrega { get; set; }
    public long IdEstadoEntrega { get; set; }
    public DateTime FechaRegistro { get; set; }
    public long IdUsuario { get; set; }
    public string? Observaciones { get; set; }
}

public enum FiltroBusquedaHistorialEntrega {
    Todos,
    Id,
    IdSeguimientoEntrega,
    IdEstadoEntrega,
    IdUsuario
}

public static class UtilidadesHistorialEntrega {
    public static object[] CriterioHistorialEntrega = {
        "Todo el Historial de Entregas",
        "Identificador de BD",
        "Identificador del Seguimiento de Entrega",
        "Estado de Entrega",
        "Usuario que realiza el cambio"
    };
}
