using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Proveedor.Plantillas;

public interface IVistaTuplaProveedor : IVistaTupla {
    string Id { get; set; }
    string RazonSocial { get; set; }
    string NumeroIdentificacionTributaria { get; set; }
    string Telefonos { get; set; }
    string Direccion { get; set; }
}