using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Seguridad {
    public class RepoPermisoRol : RepoEntidadBaseDatos<PermisoRol, FiltroBusquedaPermisoRol> {
        public RepoPermisoRol() : base("adv__permiso_rol", "id_permiso_rol") { }

        protected override string GenerarComandoAdicionar(PermisoRol objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                INSERT INTO adv__permiso_rol (
                    id_rol,
                    modulo,
                    puede_ver,
                    puede_crear,
                    puede_editar,
                    puede_eliminar
                ) VALUES (
                    @id_rol,
                    @modulo,
                    @puede_ver,
                    @puede_crear,
                    @puede_editar,
                    @puede_eliminar
                );
                """;

            parametros = new Dictionary<string, object> {
                { "@id_rol", objeto.IdRol },
                { "@modulo", objeto.Modulo },
                { "@puede_ver", Convert.ToInt32(objeto.PuedeVer) },
                { "@puede_crear", Convert.ToInt32(objeto.PuedeCrear) },
                { "@puede_editar", Convert.ToInt32(objeto.PuedeEditar) },
                { "@puede_eliminar", Convert.ToInt32(objeto.PuedeEliminar) }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(PermisoRol objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                UPDATE adv__permiso_rol 
                SET 
                    puede_ver = @puede_ver,
                    puede_crear = @puede_crear,
                    puede_editar = @puede_editar,
                    puede_eliminar = @puede_eliminar
                WHERE id_permiso_rol = @id;
                """;

            parametros = new Dictionary<string, object> {
                { "@puede_ver", Convert.ToInt32(objeto.PuedeVer) },
                { "@puede_crear", Convert.ToInt32(objeto.PuedeCrear) },
                { "@puede_editar", Convert.ToInt32(objeto.PuedeEditar) },
                { "@puede_eliminar", Convert.ToInt32(objeto.PuedeEliminar) },
                { "@id", objeto.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var consulta = $"""
                DELETE FROM adv__permiso_rol 
                WHERE id_permiso_rol = @id;
                """;

            parametros = new Dictionary<string, object> {
                { "@id", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaPermisoRol filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consultaComun = $"""
                SELECT pr.*
                FROM adv__permiso_rol pr
                """;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaPermisoRol.IdRol => $"""
                    {consultaComun} 
                    WHERE pr.id_rol = @id_rol
                    """,
                FiltroBusquedaPermisoRol.Modulo => $"""
                    {consultaComun} 
                    WHERE LOWER(pr.modulo) LIKE LOWER(@modulo)
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaPermisoRol.IdRol => new Dictionary<string, object> {
                    { "@id_rol", Convert.ToInt64(criterio) }
                },
                FiltroBusquedaPermisoRol.Modulo => new Dictionary<string, object> {
                    { "@modulo", $"%{criterio}%" }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (PermisoRol, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lectorDatos) {
            return (new PermisoRol() {
                Id = Convert.ToInt64(lectorDatos["id_permiso_rol"]),
                IdRol = Convert.ToInt64(lectorDatos["id_rol"]),
                Modulo = Convert.ToString(lectorDatos["modulo"]) ?? string.Empty,
                PuedeVer = Convert.ToBoolean(lectorDatos["puede_ver"]),
                PuedeCrear = Convert.ToBoolean(lectorDatos["puede_crear"]),
                PuedeEditar = Convert.ToBoolean(lectorDatos["puede_editar"]),
                PuedeEliminar = Convert.ToBoolean(lectorDatos["puede_eliminar"])
            }, new List<IEntidadBaseDatos>());
        }

        #region SINGLETON

        public static RepoPermisoRol Instancia { get; } = new RepoPermisoRol();

        #endregion

        #region UTILES

        public List<PermisoRol> ObtenerPermisosPorRol(long idRol) {
            var consulta = $"""
                SELECT pr.*
                FROM adv__permiso_rol pr
                WHERE pr.id_rol = @id_rol
                ORDER BY pr.modulo;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_rol", idRol }
            };

            var resultados = ContextoBaseDatos.EjecutarConsulta(consulta, parametros, MapearEntidad);
            return resultados.Select(r => r.Item1).ToList();
        }

        public PermisoRol? ObtenerPermisoRolModulo(long idRol, string modulo) {
            var consulta = $"""
                SELECT pr.*
                FROM adv__permiso_rol pr
                WHERE pr.id_rol = @id_rol 
                AND LOWER(pr.modulo) = LOWER(@modulo)
                LIMIT 1;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_rol", idRol },
                { "@modulo", modulo }
            };

            var resultados = ContextoBaseDatos.EjecutarConsulta(consulta, parametros, MapearEntidad);
            return resultados.Select(r => r.Item1).FirstOrDefault();
        }

        public void GuardarPermisosRol(long idRol, List<PermisoRol> permisos) {
            // Primero eliminar permisos existentes
            EliminarPermisosPorRol(idRol);

            // Luego insertar los nuevos
            foreach (var permiso in permisos) {
                permiso.IdRol = idRol;
                Adicionar(permiso);
            }
        }

        public void EliminarPermisosPorRol(long idRol) {
            var consulta = $"""
                DELETE FROM adv__permiso_rol 
                WHERE id_rol = @id_rol;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_rol", idRol }
            };

            ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);
        }

        #endregion
    }
}