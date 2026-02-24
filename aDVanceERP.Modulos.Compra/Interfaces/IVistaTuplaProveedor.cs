using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Compra.Interfaces {
    public interface IVistaTuplaProveedor : IVistaTupla {
        long Id { get; set; }
        string CodigoProveedor { get; set; }
        string RazonSocial { get; set; }    
        string Telefonos { get; set; }
        string Direccion { get; set; }
        string NombreRepresentante { get; set; }
        public bool Activo { get; set; }
    }
}