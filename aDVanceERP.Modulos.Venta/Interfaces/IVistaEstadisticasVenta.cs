using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    internal interface IVistaEstadisticasVenta : IVistaBase {
        int VentasHoy { get; set; }
        decimal IngresosHoy { get; set; }
        decimal IngresosMes { get; set; }
        decimal IngresosMesAnterior { get; set; }
        int VentasPendientes { get; set; }
        int PagosPendientesConfirmacion { get; set; }
        int PedidosActivos { get; set; }

        event EventHandler? ActualizarTodo;
        event EventHandler<PaintEventArgs>? ActualizarIngresosDiarios;
        event EventHandler<PaintEventArgs>? ActualizarMetodosPago;

        void CargarTopProductos(List<ProductoTopVenta> topProductos);
        void ActualizarPorcentajeVentasHoyVsAyer(decimal actual, decimal anterior);
        void ActualizarPorcentajeIngresosVsMesAnterior(decimal actual, decimal anterior);
        void RenderizarGraficosGastosDistribuciones();
    }
}
