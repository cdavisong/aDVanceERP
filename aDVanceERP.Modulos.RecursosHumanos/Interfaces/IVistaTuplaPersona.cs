using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Interfaces {
    public interface IVistaTuplaPersona : IVistaTupla {
        long Id { get; set; }
        string NumeroIdentidad { get; set; }
        string NombreCompleto { get; set; }
        string Telefonos { get; set; }
        string Direccion { get; set; }
        string FechaRegistro { get; set; }
        public bool Activo { get; set; }
    }
}