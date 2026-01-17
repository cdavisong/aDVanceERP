using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Interfaces;

public interface IVistaRegistroProveedor : IVistaRegistro {
    string RazonSocial { get; set; }
    string NumeroIdentificacionTributaria { get; set; }
    string TelefonoMovil { get; set; }
    string TelefonoFijo { get; set; }
    string CorreoElectronico { get; set; }
    string Direccion { get; set; }
}