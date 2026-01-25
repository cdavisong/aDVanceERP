using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    public interface IVistaTuplaMensajero : IVistaTupla {
        long Id { get; set; }
        string CodigoMensajero { get; set; }
        string NombreCompleto { get; set; }    
        string Telefonos { get; set; }
        public string MatriculaVehiculo { get; set; }
        public bool Activo { get; set; }
    }
}