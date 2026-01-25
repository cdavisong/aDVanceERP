using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    public interface IVistaRegistroVenta : IVistaRegistro {
        DateTime Fecha {  get; set; }
        string? RazonSocialCliente { get; set; }
        string? NombreAlmacen { get; set; }
        string? NombreProducto { get; set; }
        List<string[]>? Productos { get; }
        decimal Cantidad { get; set; }
        decimal Total { get; set; }
        long IdTipoEntrega { get; set; }
        string? Direccion { get; set; }
        bool PagoEfectuado { get; set; }
        bool MensajeriaConfigurada { get; set; }
        string? TipoEntrega { get; set; }
        string? EstadoEntrega { get; set; }

        event EventHandler? AlturaContenedorTuplasModificada;
        event EventHandler? ProductoAgregado;
        event EventHandler? ProductoEliminado;
        event EventHandler? EfectuarPago;
        event EventHandler? AsignarMensajeria;

        void CargarNombresAlmacenes(object[] nombresAlmacenes);
        void CargarNombresProductos(string[] nombresProductos);
    }
}