using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Empresa.Interfaces {
    public interface IVistaTuplaEmpresa : IVistaTupla {
        long Id { get; set; }
        string Nombre { get; set; } // Nombre comercial
        string RazonSocial { get; set; } // Razón social legal
        string? Rif { get; set; } // RIF / NIT
        string? Direccion { get; set; }
        string? Telefono { get; set; }
        string? Email { get; set; }
        DateTime FechaRegistro { get; set; }
    }
}