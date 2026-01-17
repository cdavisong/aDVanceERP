using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Interfaces;

public interface IVistaRegistroMensajero : IVistaRegistro {
    string NombreMensajero { get; set; }
    string TelefonoMovil { get; set; }
}