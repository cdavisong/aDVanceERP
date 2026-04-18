using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Empresas;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Empresas {
    public class RepoEmpresa : RepoEntidadBaseDatos<Empresa, FiltroBusquedaEmpresa> {
        public RepoEmpresa() : base("adv__empresa", "id_empresa") { }

        protected override string GenerarComandoAdicionar(Empresa entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                INSERT INTO adv__empresa (
                    nombre,
                    razon_social,
                    rif,
                    direccion,
                    telefono,
                    email,
                    web,
                    ruta_logo,
                    fecha_registro
                ) VALUES (
                    @nombre,
                    @razon_social,
                    @rif,
                    @direccion,
                    @telefono,
                    @email,
                    @web,
                    @ruta_logo,
                    @fecha_registro
                );
                """;

            parametros = new Dictionary<string, object> {
                { "@nombre", entidad.Nombre },
                { "@razon_social", entidad.RazonSocial ?? (object)DBNull.Value },
                { "@rif", entidad.Rif ?? (object)DBNull.Value },
                { "@direccion", entidad.Direccion ?? (object)DBNull.Value },
                { "@telefono", entidad.Telefono ?? (object)DBNull.Value },
                { "@email", entidad.Email ?? (object)DBNull.Value },
                { "@web", entidad.Web ?? (object)DBNull.Value },
                { "@ruta_logo", entidad.RutaLogo ?? (object)DBNull.Value },
                { "@fecha_registro", entidad.FechaRegistro }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(Empresa entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                UPDATE adv__empresa 
                SET 
                    nombre = @nombre,
                    razon_social = @razon_social,
                    rif = @rif,
                    direccion = @direccion,
                    telefono = @telefono,
                    email = @email,
                    web = @web,
                    ruta_logo = @ruta_logo
                WHERE id_empresa = @id;
                """;

            parametros = new Dictionary<string, object> {
                { "@nombre", entidad.Nombre },
                { "@razon_social", entidad.RazonSocial ?? (object)DBNull.Value },
                { "@rif", entidad.Rif ?? (object)DBNull.Value },
                { "@direccion", entidad.Direccion ?? (object)DBNull.Value },
                { "@telefono", entidad.Telefono ?? (object)DBNull.Value },
                { "@email", entidad.Email ?? (object)DBNull.Value },
                { "@web", entidad.Web ?? (object)DBNull.Value },
                { "@ruta_logo", entidad.RutaLogo ?? (object)DBNull.Value },
                { "@id", entidad.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var consulta = $"""
                DELETE FROM adv__empresa 
                WHERE id_empresa = @id;
                """;

            parametros = new Dictionary<string, object> {
                { "@id", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaEmpresa filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consulta = filtroBusqueda switch {
                FiltroBusquedaEmpresa.Id => $"""
                    SELECT * 
                    FROM adv__empresa 
                    WHERE id_empresa = @id;
                    """,
                FiltroBusquedaEmpresa.NombreComercial => $"""
                    SELECT * 
                    FROM adv__empresa 
                    WHERE nombre LIKE @nombre;
                    """,
                FiltroBusquedaEmpresa.Rif => $"""
                    SELECT * 
                    FROM adv__empresa 
                    WHERE rif = @rif;
                    """,
                _ => "SELECT * FROM adv__empresa;"
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaEmpresa.Id => new Dictionary<string, object> {
                    { "@id", Convert.ToInt64(string.IsNullOrEmpty(criterio) ? "0" : criterio) },
                },
                FiltroBusquedaEmpresa.NombreComercial => new Dictionary<string, object> {
                    { "@nombre", $"%{criterio}%" }
                },
                FiltroBusquedaEmpresa.Rif => new Dictionary<string, object> {
                    { "@rif", criterio }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (Empresa, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            return (new Empresa(
                id: Convert.ToInt64(lector["id_empresa"]),
                nombre: Convert.ToString(lector["nombre"]) ?? string.Empty,
                razonSocial: lector["razon_social"] != DBNull.Value ? Convert.ToString(lector["razon_social"]) : null,
                rif: lector["rif"] != DBNull.Value ? Convert.ToString(lector["rif"]) : null,
                direccion: lector["direccion"] != DBNull.Value ? Convert.ToString(lector["direccion"]) : null,
                telefono: lector["telefono"] != DBNull.Value ? Convert.ToString(lector["telefono"]) : null,
                email: lector["email"] != DBNull.Value ? Convert.ToString(lector["email"]) : null,
                web: lector["web"] != DBNull.Value ? Convert.ToString(lector["web"]) : null,
                rutaLogo: lector["ruta_logo"] != DBNull.Value ? Convert.ToString(lector["ruta_logo"]) : null,
                fechaRegistro: Convert.ToDateTime(lector["fecha_registro"])
            ), new List<IEntidadBaseDatos>());
        }

        #region SINGLETON

        public static RepoEmpresa Instancia { get; } = new RepoEmpresa();

        #endregion
    }
}
