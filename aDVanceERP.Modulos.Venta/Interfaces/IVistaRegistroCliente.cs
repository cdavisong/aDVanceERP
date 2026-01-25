using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    public interface IVistaRegistroCliente : IVistaRegistro {
        string NombreCompleto { get; set; }
        TipoDocumento TipoDocumento { get; set; }
        string NumeroDocumento { get; set; }
        string? DireccionPrincipal { get; set; }
        DateTime FechaRegistro { get; set; }
        string CodigoCliente { get; set; }
        decimal LimiteCredito { get; set; }
        List<TelefonoContacto> Telefonos { get; }

        void AgregarTelefono(long id, string categoria, string prefijo, string numero, long idPersona = 0);
        void CargarNombresPersonas(string[] nombresPersonas);
    }
}