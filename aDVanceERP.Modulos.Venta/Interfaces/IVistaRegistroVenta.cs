using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

using DVanceERP.Modulos.Venta.Vistas;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    public interface IVistaRegistroVenta : IVistaRegistro {
        DateTime FechaVenta {  get; set; }
        string NumeroPedido { get; set; }
        string NombreCliente { get; set; }
        string NombreAlmacenOrigen { get; set; }
        string ObservacionesPedido { get; set; }
        Dictionary<long, VistaTuplaCarrito> Carrito { get; }

        void CargarNumerosPedidos(object[] numerosPedidos);
        void CargarNombresClientes(string[] nombresClientes);
        void CargarNombresAlmacenes(object[] nombresAlmacenes);
        void CargarNombresProductos(string[] nombresProductos);
    }
}