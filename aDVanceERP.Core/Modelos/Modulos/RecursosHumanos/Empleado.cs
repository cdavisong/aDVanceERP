using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.RecursosHumanos {
    public sealed class Empleado : IEntidadBaseDatos {
        public Empleado() {
            Id = 0;
            IdPersona = 0;
            CodigoEmpleado = "N/A";
            FechaContratacion = DateTime.MinValue;
            FechaNacimiento = DateTime.MinValue;
            Cargo = "N/A";
            Departamento = "N/A";
            Salario = 0;
            Activo = false;
        }

        public Empleado(long id, long idPersona, string codigoEmpleado, DateTime fechaContratacion, DateTime fechaNacimiento, string cargo, string departamento, decimal salario, bool activo) {
            Id = id;
            IdPersona = idPersona;
            CodigoEmpleado = codigoEmpleado;
            FechaContratacion = fechaContratacion;
            FechaNacimiento = fechaNacimiento;
            Cargo = cargo;
            Departamento = departamento;
            Salario = salario;
            Activo = activo;
        }

        public long Id { get; set; }
        public long IdPersona { get; set; }
        public string CodigoEmpleado { get; set; }
        public DateTime FechaContratacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Cargo { get; set; }
        public string Departamento { get; set; }
        public decimal Salario { get; set; }
        public bool Activo { get; set; }
    }

    public enum FiltroBusquedaEmpleado {
        Todos,
        Id,
        IdPersona,
        CodigoEmpleado,
        Cargo,
        Departamento
    }

    public static class UtilesBusquedaEmpleado {
        public static object[] FiltroBusquedaEmpleado = {
            "Todos los empleados",
            "Identificador de BD",
            "Identificador de la persona",
            "Código del empleado",
            "Cargo del empleado",
            "Departamento del empleado"
        };
    }
}
