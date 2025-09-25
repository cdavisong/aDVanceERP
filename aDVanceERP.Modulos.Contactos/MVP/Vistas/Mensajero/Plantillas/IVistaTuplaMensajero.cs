using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Mensajero.Plantillas;

public interface IVistaTuplaMensajero : IVistaTupla {
    string Id { get; set; }
    string NombreMensajero { get; set; }
    string Telefonos { get; set; }
    string Direccion { get; set; }
    bool Activo { get; set; }
}