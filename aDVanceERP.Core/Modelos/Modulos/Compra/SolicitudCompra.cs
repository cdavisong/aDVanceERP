using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Compra {
    public sealed class SolicitudCompra : IEntidadBaseDatos {
        public SolicitudCompra() {
            Codigo = "N/A";
            Observaciones = "N/A";
            Estado = "Borrador";
            Activo = true;
            FechaSolicitud = DateTime.UtcNow;
        }

        public SolicitudCompra(
            long id,
            string codigo,
            long idSolicitante,
            DateTime fechaSolicitud,
            DateTime? fechaRequerida,
            string observaciones,
            string estado,
            bool activo) {
            Id = id;
            Codigo = codigo;
            IdSolicitante = idSolicitante;
            FechaSolicitud = fechaSolicitud;
            FechaRequerida = fechaRequerida;
            Observaciones = observaciones;
            Estado = estado;
            Activo = activo;
        }

        public long Id { get; set; }
        public string Codigo { get; set; }
        public long IdSolicitante { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaRequerida { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; } // Borrador, Pendiente_Aprobacion, Aprobada, Rechazada, Convertida, Cancelada
        public bool Activo { get; set; }

        // Propiedades de navegación (no mapeadas directamente a BD)
        public List<DetalleSolicitudCompra> Detalles { get; set; } = new();
    }

    public enum EstadoSolicitudCompra {
        Borrador,
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