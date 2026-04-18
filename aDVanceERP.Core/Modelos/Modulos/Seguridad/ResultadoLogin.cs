namespace aDVanceERP.Core.Modelos.Modulos.Seguridad {
    /// <summary>
    /// Resultado del proceso de login
    /// </summary>
    public class ResultadoLogin {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public CuentaUsuario Usuario { get; set; }
        public Rol Rol { get; set; }

        public ResultadoLogin() {
            Exitoso = false;
            Mensaje = string.Empty;
        }

        public static ResultadoLogin Exito(CuentaUsuario usuario, Rol rol) {
            return new ResultadoLogin {
                Exitoso = true,
                Mensaje = "Inicio de sesión exitoso",
                Usuario = usuario,
                Rol = rol
            };
        }

        public static ResultadoLogin Fallo(string mensaje) {
            return new ResultadoLogin {
                Exitoso = false,
                Mensaje = mensaje
            };
        }
    }
}