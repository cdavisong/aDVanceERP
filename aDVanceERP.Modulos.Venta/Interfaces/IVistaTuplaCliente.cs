using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Venta.Interfaces;

public interface IVistaTuplaCliente : IVistaTupla {
    long Id { get; set; }
    string CodigoCliente { get; set; }
    string NombreCompleto { get; set; }
    string Telefonos { get; set; }
    string Direccion { get; set; }
    string FechaRegistro { get; set; }
    public bool Activo { get; set; }
}