using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Interfaces
{
    public interface IVistaRegistroEmpleado : IVistaRegistro {
        string NombreCompleto { get; set; }
        TipoDocumento TipoDocumento { get; set; }
        string NumeroDocumento { get; set; }
        string? DireccionPrincipal { get; set; }
        string CodigoEmpleado { get; set; }
        DateTime FechaContratacion { get; set; }
        DateTime FechaNacimiento { get; set; }
        string Cargo { get; set; }
        string Departamento { get; set; }
        decimal Salario { get; set; }
        List<TelefonoContacto> Telefonos { get; }
        List<CorreoContacto> DireccionesCorreo { get; }

        void AgregarTelefono(long id, string categoria, string prefijo, string numero, long idPersona = 0);
        void AgregarDireccionCorreo(long id, string categoria, string direccionCorreo, long idPersona = 0);
        void CargarNombresPersonas(string[] nombresPersonas);
    }
}
