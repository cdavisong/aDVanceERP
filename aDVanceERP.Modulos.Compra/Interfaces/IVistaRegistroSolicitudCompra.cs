using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Compra.Interfaces {
    internal interface IVistaRegistroSolicitudCompra : IVistaRegistro {
        string Codigo { get; }
        string NombreSolicitante { get; set; }
        DateTime FechaRequerida { get; set; }
        string Observaciones { get; set; }

        void CargarNombresTrabajadores(string[] nombresTrabajadores);
        void CargarNombresProductos(string[] nombresProductos);
    }
}
