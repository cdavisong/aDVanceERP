using System;
namespace aDVanceERP.Core.Modelos.Modulos.Venta {
    public class MetricasVentas {
        /// <summary>
        /// Ventas completadas hoy.
        /// </summary>
        public int VentasHoy { get; set; }

        /// <summary>
        /// Ingresos totales del día (importe_total, estado Completada/Entregada).
        /// </summary>
        public decimal IngresosHoy { get; set; }

        /// <summary>
        /// Ingresos del mes en curso.
        /// </summary>
        public decimal IngresosMes { get; set; }

        /// <summary>
        /// Ingresos del mes anterior.
        /// </summary>
        public decimal IngresosMesAnterior { get; set; }

        /// <summary>
        /// Ventas en estado Pendiente.
        /// </summary>
        public int VentasPendientes { get; set; }

        /// <summary>
        /// Pagos en estado Pendiente (por confirmar).
        /// </summary>
        public int PagosPendientesConfirmacion { get; set; }

        /// <summary>
        /// Pedidos activos (estado != Cancelado, Retirado).
        /// </summary>
        public int PedidosActivos { get; set; }

        /// <summary>
        /// Top 5 productos más vendidos por cantidad en el mes.
        /// </summary>
        public List<ProductoTopVenta> TopProductos { get; set; } = [];

        /// <summary>
        /// Ingresos diarios de los últimos 30 días.
        /// </summary>
        public List<IngresoDiario> EvolucionIngresos { get; set; } = [];

        /// <summary>
        /// Distribución de ventas por método de pago.
        /// </summary>
        public List<VentasPorMetodoPago> DistribucionMetodosPago { get; set; } = [];
    }
}
