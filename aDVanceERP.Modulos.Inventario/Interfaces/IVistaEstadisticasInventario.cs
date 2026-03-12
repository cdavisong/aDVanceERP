using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    internal interface IVistaEstadisticasInventario : IVistaBase {
        int TotalProductos { get; set; }
        int ProductosBajoStockMinimo { get; set; }
        int ProductosSinStock { get; set; }
        decimal ValorTotalInventario { get; set; }
        int TotalAlmacenes { get; set; }
        int MovimientosHoy { get; set; }

        event EventHandler? ActualizarTodo;
        event EventHandler<PaintEventArgs>? ActualizarEvolucionMovimientos;
        event EventHandler<PaintEventArgs>? ActualizarValorAlmacen;

        void CargarTopProductosValor(List<ProductoTopInventario> topProductosValor);
        void RenderizarGraficosMovimientosAlmacenes();
    }
}
