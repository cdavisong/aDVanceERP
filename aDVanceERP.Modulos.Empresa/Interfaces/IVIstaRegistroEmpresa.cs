using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Empresa.Interfaces {
    public interface IVIstaRegistroEmpresa : IVistaRegistro {
        Image? Imagen { get; set; }
        long IdPersona { get; set; }
        string Nombre { get; set; } // Nombre comercial
        string RazonSocial { get; set; } // Razón social legal
        string? Rif { get; set; } // RIF / NIT
        string? Direccion { get; set; }
        TelefonoContacto? Telefono { get; set; }
        CorreoContacto? Email { get; set; }
        string? Web { get; set; }
        string? RutaLogo { get; } // Ruta al archivo del logo
        DateTime FechaRegistro { get; set; }

        void SalvarImagenEnDirectorioLocal();
    }
}
