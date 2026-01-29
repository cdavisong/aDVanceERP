using aDVanceERP.Core.Modelos.Comun.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Venta {
    public sealed class SeguimientoEntrega : IEntidadBaseDatos {
        public SeguimientoEntrega() {
            TipoEnvio = TipoEnvioEnum.RetiroEnLocal;
            EstadoEntrega = EstadoEntregaEnum.PendienteAsignacion;
            MontoCobradoAlCliente = 0.0m;
        }

        public SeguimientoEntrega(long id, long idVenta, long? idMensajero,
                                 TipoEnvioEnum tipoEnvio, DateTime? fechaAsignacion,
                                 DateTime? fechaEntregaRealizada, DateTime? fechaPagoNegocio,
                                 EstadoEntregaEnum estadoEntrega, decimal montoCobradoAlCliente,
                                 string observacionesEntrega) {
            Id = id;
            IdVenta = idVenta;
            IdMensajero = idMensajero;
            TipoEnvio = tipoEnvio;
            FechaAsignacion = fechaAsignacion;
            FechaEntregaRealizada = fechaEntregaRealizada;
            FechaPagoNegocio = fechaPagoNegocio;
            EstadoEntrega = estadoEntrega;
            MontoCobradoAlCliente = montoCobradoAlCliente;
            ObservacionesEntrega = observacionesEntrega;
        }

        public long Id { get; set; }
        public long IdVenta { get; set; }
        public long? IdMensajero { get; set; }
        public TipoEnvioEnum TipoEnvio { get; set; }
        public DateTime? FechaAsignacion { get; set; }
        public DateTime? FechaEntregaRealizada { get; set; }
        public DateTime? FechaPagoNegocio { get; set; }
        public EstadoEntregaEnum EstadoEntrega { get; set; }
        public decimal MontoCobradoAlCliente { get; set; }
        public string? ObservacionesEntrega { get; set; }
    }

    public enum TipoEnvioEnum {
        [Display(Name = "Retiro en Local")]
        RetiroEnLocal,
        [Display(Name = "Mensajería con Fondo")]
        MensajeriaConFondo,
        [Display(Name = "Mensajería sin Fondo")]
        MensajeriaSinFondo
    }

    public enum EstadoEntregaEnum {
        [Display(Name = "Pendiente Asignación")]
        PendienteAsignacion,
        Asignado,
        [Display(Name = "En Ruta")]
        EnRuta,
        Entregado,
        [Display(Name = "Pago Recibido")]
        PagoRecibido,
        Cancelado,
        Fallido
    }

    public enum FiltroBusquedaSeguimientoEntrega {
        Todos,
        Id,
        IdVenta,
        IdMensajero,
        Estado
    }

    public static class UtilesBusquedaSeguimientoEntrega {
        public static object[] FiltroBusquedaSeguimientoEntrega = {
            "Todos los seguimientos",
            "Identificador de BD",
            "Identificador de la venta",
            "Identificador del mensajero",
            "Estado de entrega"
        };
    }
}