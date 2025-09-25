using aDVanceERP.Core.MVP.Vistas.Plantillas;

namespace aDVancePOS.Modulos.TerminalVenta.MVP.Vistas.Venta.Plantillas;

public interface IVistaTerminalVenta : IVistaRegistro {
    string? RazonSocialCliente { get; set; }
    string? NombreAlmacen { get; set; }
    string? NombreProducto { get; set; }
    List<string[]>? Productos { get; }
    decimal Cantidad { get; set; }
    decimal Subtotal { get; set; }
    decimal Descuento { get; set; }
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
    event EventHandler? ModificarCantidadProductos;
    event EventHandler? EfectuarPago;
    event EventHandler? AsignarMensajeria;

    void CargarNombresAlmacenes(object[] nombresAlmacenes);
    void CargarNombresProductos(string[] nombresProductos);
}