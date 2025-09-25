using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Mensajero.Plantillas;

public interface IVistaRegistroMensajero : IVistaRegistro {
    string NombreMensajero { get; set; }
    string TelefonoMovil { get; set; }
}