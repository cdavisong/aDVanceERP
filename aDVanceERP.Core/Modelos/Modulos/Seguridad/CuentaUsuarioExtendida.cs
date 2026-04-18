namespace aDVanceERP.Core.Modelos.Modulos.Seguridad {
    /// <summary>
    /// Extensión de CuentaUsuario con información de rol incluida
    /// </summary>
    public class CuentaUsuarioExtendida : CuentaUsuario {
        public string NombreRol { get; set; }
        public string? DescripcionRol { get; set; }

        public CuentaUsuarioExtendida() : base() {
            NombreRol = string.Empty;
            DescripcionRol = null;
        }

        public static CuentaUsuarioExtendida DesdeCuentaUsuario(CuentaUsuario usuario, Rol? rol) {
            return new CuentaUsuarioExtendida {
                Id = usuario.Id,
                IdPersona = usuario.IdPersona,
                Nombre = usuario.Nombre,
                PasswordHash = usuario.PasswordHash,
                PasswordSalt = usuario.PasswordSalt,
                Email = usuario.Email,
                IdRol = usuario.IdRol,
                Administrador = usuario.Administrador,
                Aprobado = usuario.Aprobado,
                Estado = usuario.Estado,
                UltimoAcceso = usuario.UltimoAcceso,
                NombreRol = rol?.Nombre ?? "Sin rol",
                DescripcionRol = rol?.Descripcion
            };
        }
    }
}