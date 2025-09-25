using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Mensajeria.Plantillas;

public interface IVistaRegistroMensajeria : IVistaRegistro {
    long IdVenta { get; set; }
    string? RazonSocialCliente { get; set; }
    string? TelefonosCliente { get; }
    string? NombreMensajero { get; set; }
    string? TipoEntrega { get; set; }
    string DescripcionTipoEntrega { get; set; }
    string? Direccion { get; set; }
    string? Observaciones { get; set; }
    string ResumenEntrega { get; set; }

    event EventHandler? AsignarNuevoMensajero;
    event EventHandler? AsignarNuevoCliente;

    void CargarNombresMensajeros(object[] nombresMensajeros);
    void CargarTiposEntrega();
    void CargarRazonesSocialesClientes(string[] razonesSocialesClientes);
    void PopularProductosVenta(List<string[]>? datosProductos);
}