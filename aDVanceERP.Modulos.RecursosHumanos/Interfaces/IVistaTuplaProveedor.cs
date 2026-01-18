using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Interfaces;

public interface IVistaTuplaProveedor : IVistaTupla {
    string Id { get; set; }
    string RazonSocial { get; set; }
    string NumeroIdentificacionTributaria { get; set; }
    string Telefonos { get; set; }
    string Direccion { get; set; }
    string NombreRepresentante { get; set; }
}