namespace aDVanceERP.Core.Modelos.Modulos.Compra {
    public class MetricasCompras {
        /// <summary>
        /// Órdenes en estado Pendiente_Aprobacion.
        /// </summary>
        public int OrdenesPendientesAprobacion { get; set; }

        /// <summary>
        /// Órdenes aprobadas aún no recibidas completamente.
        /// </summary>
        public int OrdenesAprobadas { get; set; }

        /// <summary>
        /// Órdenes con recepción parcial.
        /// </summary>
        public int OrdenesRecibidasParcial { get; set; }

        /// <summary>
        /// Suma de total_compra de todas las órdenes activas del mes.
        /// </summary>
        public decimal GastoMesActual { get; set; }

        /// <summary>
        /// Suma del mes anterior para calcular variación.
        /// </summary>
        public decimal GastoMesAnterior { get; set; }

        /// <summary>
        /// Número de solicitudes de compra en estado Pendiente_Aprobacion.
        /// </summary>
        public int SolicitudesPendientes { get; set; }

        /// <summary>
        /// Top 5 proveedores por monto total comprado.
        /// </summary>
        public List<ProveedorTopCompras> TopProveedores { get; set; } = [];

        /// <summary>
        /// Gasto mensual de los últimos 6 meses.
        /// </summary>
        public List<GastoMensual> EvolucionGasto { get; set; } = [];

        /// <summary>
        /// Distribución por estado de las órdenes activas.
        /// </summary>
        public List<ComprasPorEstado> DistribucionEstados { get; set; } = [];
    }
}
