using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Venta.Vistas;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    internal interface IVistaRegistroPedido : IVistaRegistro {
        DateTime FechaEntregaSolicitada {  get; set; }
        string Codigo { get; }
        string NombreCliente { get; set; }
        string DireccionEntrega { get; set; }
        string ObservacionesPedido { get; set; }
        Dictionary<long, VistaTuplaCarrito> Carrito { get; }
        decimal SubTotal { get; }
        decimal ImporteEstimado { get; }

        void CargarNombresClientes(string[] nombresClientes);
        void CargarNombresProductos(string[] nombresProductos);
    }
}