using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Compra.Vistas;

namespace aDVanceERP.Modulos.Compra.Interfaces {
    internal interface IVistaRegistroSolicitudCompra : IVistaRegistro {
        string Codigo { get; }
        DateTime FechaRequerida { get; set; }
        string Observaciones { get; set; }
        Dictionary<long, VistaTuplaCarrito> Carrito { get; }

        event EventHandler? RegistrarNuevoProducto;

        void CargarNombresProductos(string[] nombresProductos);
    }
}
