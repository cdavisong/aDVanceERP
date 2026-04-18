using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Seguridad {
    public class RepoCuentaUsuario : RepoEntidadBaseDatos<CuentaUsuario, FiltroBusquedaCuentaUsuario> {
        public RepoCuentaUsuario() : base("adv__cuenta_usuario", "id_cuenta_usuario") { }

        protected override string GenerarComandoAdicionar(CuentaUsuario objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                INSERT INTO adv__cuenta_usuario (
                id_persona,
                nombre, 
                password_hash, 
                password_salt,
                email,
                id_rol,
                administrador, 
                aprobado,
                estado,
                ultimo_acceso
            ) VALUES (
                @id_persona,
                @nombre, 
                @password_hash, 
                @password_salt, 
                @email,
                @id_rol,
                @administrador, 
                @aprobado,
                @estado,
                @ultimo_acceso
            );
            """;

            parametros = new Dictionary<string, object> {
                { "@id_persona", objeto.IdPersona },
                { "@nombre", objeto.Nombre },
                { "@password_hash", objeto.PasswordHash },
                { "@password_salt", objeto.PasswordSalt },
                { "@email", objeto.Email },
                { "@id_rol", objeto.IdRol },
                { "@administrador", Convert.ToInt32(objeto.Administrador) },
                { "@aprobado", Convert.ToInt32(objeto.Aprobado) },
                { "@estado",  Convert.ToInt32(objeto.Estado) },
                { "@ultimo_acceso", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(CuentaUsuario objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                UPDATE adv__cuenta_usuario 
                SET 
                    id_persona = @id_persona,
                    nombre = @nombre,
                    email = @email,
                    id_rol = @id_rol,
                    aprobado = @aprobado,
                    estado = @estado,
                    ultimo_acceso = @ultimo_acceso 
                WHERE id_cuenta_usuario = @id;
                """;

            parametros = new Dictionary<string, object> {
                { "@id_persona", objeto.IdPersona  },
                { "@nombre", objeto.Nombre },
                { "@email", objeto.Email },
                { "@id_rol", objeto.IdRol },
                { "@aprobado", Convert.ToInt32(objeto.Aprobado) },
                { "@estado",  Convert.ToInt32(objeto.Estado) },
                { "@ultimo_acceso", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@id", objeto.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var consulta = $"""
                DELETE FROM adv__cuenta_usuario 
                WHERE id_cuenta_usuario = @id;
                """;

            parametros = new Dictionary<string, object> {
                { "@id", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaCuentaUsuario filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var critero = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consultaComun = $"""
                SELECT cu.*
                FROM adv__cuenta_usuario cu
                """;
            var consulta = filtroBusqueda switch {
                FiltroBusquedaCuentaUsuario.Nombre => $"""
                    {consultaComun} 
                    WHERE LOWER(cu.nombre) LIKE LOWER(@nombre);
                """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaCuentaUsuario.Nombre => new Dictionary<string, object> {
                    { "@nombre", $"%{critero}%" }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (CuentaUsuario, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lectorDatos) {
            return (new CuentaUsuario() {
                Id = Convert.ToInt64(lectorDatos["id_cuenta_usuario"]),
                IdPersona = Convert.ToInt64(lectorDatos["id_persona"]),
                Nombre = Convert.ToString(lectorDatos["nombre"]),
                PasswordHash = Convert.ToString(lectorDatos["password_hash"]),
                PasswordSalt = Convert.ToString(lectorDatos["password_salt"]),
                Email = lectorDatos["email"] == DBNull.Value 
                    ? string.Empty 
                    : Convert.ToString(lectorDatos["email"]),
                IdRol = Convert.ToInt64(lectorDatos["id_rol"]),
                Administrador = Convert.ToBoolean(lectorDatos["administrador"]),
                Aprobado = Convert.ToBoolean(lectorDatos["aprobado"]),
                Estado = Convert.ToBoolean(lectorDatos["estado"]),
                UltimoAcceso = lectorDatos["ultimo_acceso"] == DBNull.Value 
                    ? null
                    : Convert.ToDateTime(lectorDatos["ultimo_acceso"])
            }, new List<IEntidadBaseDatos>());
        }

        #region SINGLETON

        public static RepoCuentaUsuario Instancia { get; } = new RepoCuentaUsuario();

        #endregion

        #region UTILES 

        public void CambiarPassword(long idCuentaUsuario, (string hash, string salt) passwordSeguro) {
            var consulta = $"""
                UPDATE adv__cuenta_usuario 
                SET 
                    password_hash = @password_hash,
                    password_salt = @password_salt, 
                WHERE id_cuenta_usuario = @id;
                """;
            var parametros = new Dictionary<string, object> {
                { "@password_hash", passwordSeguro.hash },
                { "@password_salt", passwordSeguro.salt },
                { "@id", idCuentaUsuario }
            };

            ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);
        }

        public void ActualizarUltimoAcceso(long idCuentaUsuario) {
            var consulta = $"""
                UPDATE adv__cuenta_usuario 
                SET ultimo_acceso = @ultimo_acceso
                WHERE id_cuenta_usuario = @id;
                """;

            var parametros = new Dictionary<string, object> {
                { "@ultimo_acceso", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@id", idCuentaUsuario }
            };

            ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);
        }

        #endregion
    }
}