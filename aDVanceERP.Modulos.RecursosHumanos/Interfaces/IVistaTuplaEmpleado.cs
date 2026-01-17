using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Interfaces {
    public interface IVistaTuplaEmpleado : IVistaTupla {
        public long Id { get; set; }
        public string CodigoEmpleado { get; set; }
        public string NombreCompleto { get; set; }        
        public string Cargo { get; set; }
        public string Departamento { get; set; }
        public string FechaContratacion { get; set; }
        public bool Activo { get; set; }
    }
}
