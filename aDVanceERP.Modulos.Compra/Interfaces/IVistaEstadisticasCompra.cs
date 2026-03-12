using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Compra.Interfaces {
    internal interface IVistaEstadisticasCompra : IVistaBase {
        int OrdenesPendientesAprobacion { get; set; }
        int OrdenesAprobadas { get; set; }
        int OrdenesRecibidasParcial { get; set; }
        int SolicitudesPendientes { get; set; }
        decimal GastoMesActual { get; set; }
        decimal GastoMesAnterior { get; set; }

        event EventHandler? ActualizarTodo;
        event EventHandler<PaintEventArgs>? ActualizarEvolucionGasto6Meses;
        event EventHandler<PaintEventArgs>? ActualizarDistribucionPorEstado;

        void CargarTopProveedores(List<ProveedorTopCompras> topProveedores);
        void ActualizarPorcentajeVsMesAnterior(decimal actual, decimal anterior);
        void RenderizarGraficosGastosDistribuciones();
    }
}
