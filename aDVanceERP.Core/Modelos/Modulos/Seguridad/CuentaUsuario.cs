using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Seguridad {
    public class CuentaUsuario : IEntidadBaseDatos {
        public long Id { get; set; }
        public long IdPersona { get; set; }
        public string Nombre { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string? Email { get; set; }
        public long IdRol { get; set; }
        public bool Administrador { get; set; }
        public bool Aprobado { get; set; }
        public bool Estado { get; set; }
        public DateTime? UltimoAcceso { get; set; }
    }

    public enum FiltroBusquedaCuentaUsuario {
        Todos,
        Nombre,
        Email
    }
}