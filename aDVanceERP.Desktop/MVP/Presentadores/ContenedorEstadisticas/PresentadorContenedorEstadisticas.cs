using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Desktop.MVP.Vistas.ContenedorEstadisticas.Plantillas;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorEstadisticas;

public class PresentadorContenedorEstadisticas : PresentadorVistaBase<IVistaContenedorEstadisticas> {
    public PresentadorContenedorEstadisticas(IVistaContenedorEstadisticas vista) : base(vista) {
        vista.FechaEstadsticasModificada += delegate(object? sender, EventArgs args) {
            if (sender is DateTime fecha)
                Vista.DatosEstadisticosVentas = UtilesVenta.ObtenerDatosEstadisticosVentas(fecha);
        };
    }

    internal async void RefrescarEstadísticas() {
        Vista.CantidadProductosRegistrados = await UtilesProducto.ObtenerStockTotalProductos();
        //Vista.MontoInversionProductos = await UtilesProducto.ObtenerMontoInvertidoEnProductos();
        Vista.CantidadProductosVendidos = UtilesVenta.ObtenerTotalProductosVendidosHoy();
        Vista.MontoVentaProductosVendidos = UtilesVenta.ObtenerValorBrutoVentaDia(DateTime.Now);
        Vista.MontoGananciaTotalNegocio = UtilesVenta.ObtenerValorGananciaTotalNegocio();
        Vista.MontoGananciaAcumuladaDia = UtilesVenta.ObtenerValorGananciaDia(DateTime.Now);
    }

    public override void Dispose() {
        //...
    }
}