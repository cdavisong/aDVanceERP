using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Maestros
{
    public sealed class PersonaRol : IEntidadBaseDatos
    {
        public PersonaRol()
        {
            FechaAsignacion = DateTime.UtcNow;
            Activo = true;
        }

        public PersonaRol(long id, long idPersona, long idRolContacto, DateTime fechaAsignacion, bool activo)
        {
            Id = id;
            IdPersona = idPersona;
            IdRolContacto = idRolContacto;
            FechaAsignacion = fechaAsignacion;
            Activo = activo;
        }

        public long Id { get; set; }
        public long IdPersona { get; set; }
        public long IdRolContacto { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public bool Activo { get; set; }
    }

    public enum FiltroBusquedaPersonaRol
    {
        Todos,
        Id,
        IdPersona,
        IdRolContacto
    }

    public static class UtilesBusquedaPersonaRol
    {
        public static object[] FiltroBusquedaPersonaRol = {
            "Todos los roles asignados a personas",
            "Identificador de BD",
            "Identificador de la persona",
            "Identificador del rol de contacto"
        };
    }
}
