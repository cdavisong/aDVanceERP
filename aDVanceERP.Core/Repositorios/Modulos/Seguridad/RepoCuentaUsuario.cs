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
                nombre, 
                password_hash, 
                password_salt,
                administrador, 
                aprobado
            ) VALUES (
                @nombre, 
                @password_hash, 
                @password_salt, 
                @administrador, 
                @aprobado
            );
            """;

            parametros = new Dictionary<string, object> {
                { "@nombre", objeto.Nombre },
                { "@password_hash", objeto.PasswordHash },
                { "@password_salt", objeto.PasswordSalt },
                { "@administrador", Convert.ToInt32(objeto.Administrador) },
                { "@aprobado", Convert.ToInt32(objeto.Aprobado) }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(CuentaUsuario objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                UPDATE adv__cuenta_usuario 
                SET 
                    nombre = @nombre,
                    aprobado = @aprobado 
                WHERE id_cuenta_usuario = @id;
                """;

            parametros = new Dictionary<string, object> {
                { "@nombre", objeto.Nombre },
                { "@aprobado", Convert.ToInt32(objeto.Aprobado) },
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
            return (new CuentaUsuario(
                id: Convert.ToInt64(lectorDatos["id_cuenta_usuario"]),
                nombre: Convert.ToString(lectorDatos["nombre"]),
                passwordHash: Convert.ToString(lectorDatos["password_hash"]),
                passwordSalt: Convert.ToString(lectorDatos["password_salt"])) {
                Administrador = Convert.ToBoolean(lectorDatos["administrador"]),
                Aprobado = Convert.ToBoolean(lectorDatos["aprobado"])
            }, new List<IEntidadBaseDatos>());
        }

        #region STATIC

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

        #endregion
    }
}