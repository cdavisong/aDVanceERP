using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Seguridad {
    public class RepoSesionUsuario : RepoEntidadBaseDatos<SesionUsuario, FiltroBusquedaSesionUsuario> {
        public RepoSesionUsuario() : base("adv__sesion_usuario", "id_sesion_usuario") { }

        protected override string GenerarComandoAdicionar(SesionUsuario objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                INSERT INTO adv__sesion_usuario (
                id_cuenta_usuario, 
                token, 
                fecha_inicio, 
                fecha_fin
            ) VALUES (
                @idCuentaUsuario, 
                @token, 
                @fechaInicio, 
                @fechaFin
            );
            """;

            parametros = new Dictionary<string, object> {
                { "@idCuentaUsuario", objeto.IdCuentaUsuario },
                { "@token", objeto.Token },
                { "@fechaInicio", objeto.FechaInicio },
                { "@fechaFin", objeto.FechaFin.HasValue ? (object)objeto.FechaFin.Value : DBNull.Value }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(SesionUsuario objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                UPDATE adv__sesion_usuario 
            SET 
                id_cuenta_usuario = @idCuentaUsuario, 
                token = @token, 
                fecha_inicio = @fechaInicio, 
                fecha_fin = @fechaFin
            WHERE id_sesion_usuario = @idSesionUsuario;
            """;

            parametros = new Dictionary<string, object> {
                { "@idSesionUsuario", objeto.Id },
                { "@idCuentaUsuario", objeto.IdCuentaUsuario },
                { "@token", objeto.Token },
                { "@fechaInicio", objeto.FechaInicio },
                { "@fechaFin", objeto.FechaFin.HasValue ? (object)objeto.FechaFin.Value : DBNull.Value }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var consulta = $"""
                DELETE FROM adv__sesion_usuario 
            WHERE id_sesion_usuario = @idSesionUsuario;
            """;

            parametros = new Dictionary<string, object> {
                { "@idSesionUsuario", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaSesionUsuario filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consulta = filtroBusqueda switch {
                FiltroBusquedaSesionUsuario.NombreUsuario => $"""
                    SELECT * 
                FROM adv__sesion_usuario su 
                JOIN adv__cuenta_usuario cu ON su.id_cuenta_usuario = cu.id_cuenta_usuario 
                WHERE LOWER(cu.nombre) LIKE LOWER(@nombre);
                """,
                FiltroBusquedaSesionUsuario.SesionActiva => """
                SELECT * 
                FROM adv__sesion_usuario 
                WHERE fecha_fin IS NULL;
                """,
                _ => """
                SELECT * 
                FROM adv__sesion_usuario;
                """
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaSesionUsuario.NombreUsuario => new Dictionary<string, object> {
                    { "@nombre", $"%{criterio}%" }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (SesionUsuario, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            return (new SesionUsuario(
                id: Convert.ToInt64(lector["id_sesion_usuario"]),
                idCuentaUsuario: Convert.ToInt64(lector["id_cuenta_usuario"]),
                token: Convert.ToString(lector["token"]),
                fechaInicio: Convert.ToDateTime(lector["fecha_inicio"])) {
                FechaFin = lector["fecha_fin"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(lector["fecha_fin"])
            }, new List<IEntidadBaseDatos>());
        }

        #region STATIC

        public static RepoSesionUsuario Instancia { get; } = new RepoSesionUsuario();

        #endregion
    }
}