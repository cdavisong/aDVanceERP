using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Interfaces;

public interface IVistaRegistroMensajero : IVistaRegistro {
    string NombreCompleto { get; set; }
    public string CodigoMensajero { get; set; }
    TipoDocumento TipoDocumento { get; set; }
    string NumeroDocumento { get; set; }
    public string MatriculaVehiculo { get; set; }
    DateTime FechaRegistro { get; set; }
    TelefonoContacto Telefono { get; set; }

    void CargarNombresPersonas(string[] nombresPersonas);
}