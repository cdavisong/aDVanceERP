using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Contacto.Plantillas;

public interface IVistaTuplaContacto : IVistaTupla {
    string Id { get; set; }
    string NombreContacto { get; set; }
    string Telefonos { get; set; }
    string CorreoElectronico { get; set; }
    string Direccion { get; set; }
}