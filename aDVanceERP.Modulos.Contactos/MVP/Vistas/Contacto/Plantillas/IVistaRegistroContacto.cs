using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Contacto.Plantillas;

public interface IVistaRegistroContacto : IVistaRegistro {
    string NombreContacto { get; set; }
    string TelefonoMovil { get; set; }
    string TelefonoFijo { get; set; }
    string CorreoElectronico { get; set; }
    string Direccion { get; set; }
    string Notas { get; set; }
}