using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Interfaces;

public interface IVistaTuplaCliente : IVistaTupla {
    string Id { get; set; }
    string Numero { get; set; }
    string RazonSocial { get; set; }
    string Telefonos { get; set; }
    string Direccion { get; set; }
}