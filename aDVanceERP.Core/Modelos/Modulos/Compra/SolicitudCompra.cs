using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Compra {
    public sealed class SolicitudCompra : IEntidadBaseDatos {
        public SolicitudCompra() {
            Codigo = "N/A";
            Observaciones = "N/A";
            Activo = true;
            FechaSolicitud = DateTime.Now;
        }

        public long Id { get; set; }
        public string Codigo { get; set; }
        public long IdSolicitante { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaRequerida { get; set; }
        public string Observaciones { get; set; }
        public EstadoSolicitudCompraEnum Estado { get; set; }
        public bool Activo { get; set; }

        // Propiedades de navegación (no mapeadas directamente a BD)
        public List<DetalleSolicitudCompra> Detalles { get; set; } = new();
    }

    public enum EstadoSolicitudCompraEnum {
        Borrador,
        [Display(Name = "Pendiente de aprobación")]
        Pendiente_Aprobacion,
        Aprobada,
        Rechazada,
        Convertida,
        Cancelada
    }

    public enum FiltroBusquedaSolicitudCompra {
        Todos,
        Id,
        Codigo,
        IdSolicitante,
        Estado,
        FechaSolicitud,
        PendientesAprobacion
    }

    public static class UtilesBusquedaSolicitudCompra {
        public static object[] Filtros = {
            "Todas las solicitudes",
            "Identificador de BD",
            "Código de solicitud",
            "ID del solicitante",
            "Estado",
            "Fecha de solicitud",
            "Pendientes de aprobación"
        };
    }
}