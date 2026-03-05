using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Compra {
    public sealed class Compra : IEntidadBaseDatos {
        public Compra() {
            Codigo = "N/A";
            CondicionesPago = "N/A";
            Observaciones = "N/A";
            Activo = true;
            FechaOrden = DateTime.UtcNow;
            Subtotal = 0;
            ImpuestoTotal = 0;
            TotalCompra = 0;
        }

        public Compra(
            long id,
            string codigo,
            long idProveedor,
            long? idSolicitudCompra,
            long? idEmpleadoComprador,
            long idAlmacenDestino,
            long? idTipoCompra,
            DateTime fechaOrden,
            DateTime? fechaEntregaEsperada,
            string condicionesPago,
            decimal subtotal,
            decimal impuestoTotal,
            decimal totalCompra,
            EstadoCompraEnum estadoCompra,
            DateTime? fechaAprobacion,
            long? aprobadoPor,
            string observaciones,
            bool activo) {
            Id = id;
            Codigo = codigo;
            IdProveedor = idProveedor;
            IdSolicitudCompra = idSolicitudCompra;
            IdEmpleadoComprador = idEmpleadoComprador;
            IdAlmacenDestino = idAlmacenDestino;
            IdTipoCompra = idTipoCompra;
            FechaOrden = fechaOrden;
            FechaEntregaEsperada = fechaEntregaEsperada;
            CondicionesPago = condicionesPago;
            Subtotal = subtotal;
            ImpuestoTotal = impuestoTotal;
            TotalCompra = totalCompra;
            EstadoCompra = estadoCompra;
            FechaAprobacion = fechaAprobacion;
            AprobadoPor = aprobadoPor;
            Observaciones = observaciones;
            Activo = activo;
        }

        public long Id { get; set; }
        public string Codigo { get; set; }
        public long IdProveedor { get; set; }
        public long? IdSolicitudCompra { get; set; }
        public long? IdEmpleadoComprador { get; set; }
        public long IdAlmacenDestino { get; set; }
        public long? IdTipoCompra { get; set; }
        public DateTime FechaOrden { get; set; }
        public DateTime? FechaEntregaEsperada { get; set; }
        public string CondicionesPago { get; set; }
        public decimal Subtotal { get; set; }
        public decimal ImpuestoTotal { get; set; }
        public decimal TotalCompra { get; set; }
        public EstadoCompraEnum EstadoCompra { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public long? AprobadoPor { get; set; }
        public string Observaciones { get; set; }
        public bool Activo { get; set; }

        // Propiedades de navegación
        public List<DetalleCompraProducto> Detalles { get; set; } = new();
        public List<RecepcionCompra> Recepciones { get; set; } = new();
    }

    public enum EstadoCompraEnum {
        Borrador,
        [Display(Name = "Pendiente de aprobación")]
        Pendiente_Aprobacion,
        Aprobada,
        Enviada,
        [Display(Name = "Recibida parcialmente")]
        Recibida_Parcial,
        [Display(Name = "Recibida completamente")]
        Recibida_Completa,
        Cancelada,
        Facturada
    }

    public enum FiltroBusquedaCompra {
        Todos,
        Id,
        Codigo,
        IdProveedor,
        IdSolicitudCompra,
        Estado,
        FechaOrden,
        PendientesAprobacion,
        PendientesRecepcion,
        PendientesPago
    }

    public static class UtilesBusquedaCompra {
        public static object[] Filtros = {
            "Todas las compras",
            "Identificador de BD",
            "Código de compra",
            "ID del proveedor",
            "ID de solicitud",
            "Estado",
            "Fecha de orden",
            "Pendientes de aprobación",
            "Pendientes de recepción",
            "Pendientes de pago"
        };
    }
}