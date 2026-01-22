using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Repositorios.BD;

using DocumentFormat.OpenXml.Office2010.Excel;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos {
    public class RepoPersona : RepoEntidadBaseDatos<Persona, FiltroBusquedaPersona> {
        public RepoPersona() : base("adv__persona", "id_persona") { }

        protected override string GenerarComandoAdicionar(Persona entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                INSERT INTO adv__persona (
                    nombre_completo,
                    tipo_documento,
                    numero_documento,
                    direccion_principal,
                    fecha_registro,
                    activo
                ) VALUES (
                    @nombre_completo,
                    @tipo_documento,
                    @numero_documento,
                    @direccion_principal,
                    @fecha_registro,
                    @activo
                );
                """;

            parametros = new Dictionary<string, object> {
                { "@nombre_completo", entidad.NombreCompleto },
                { "@tipo_documento", entidad.TipoDocumento.ToString() },
                { "@numero_documento", entidad.NumeroDocumento },
                { "@direccion_principal", entidad.DireccionPrincipal ?? (object)DBNull.Value },
                { "@fecha_registro", entidad.FechaRegistro },
                { "@activo", entidad.Activo ? 1 : 0 }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(Persona entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                UPDATE adv__persona 
                SET 
                    nombre_completo = @nombre_completo,
                    tipo_documento = @tipo_documento,
                    numero_documento = @numero_documento,
                    direccion_principal = @direccion_principal,
                    fecha_registro = @fecha_registro,
                    activo = @activo
                WHERE id_persona = @id_persona;
                """;

            parametros = new Dictionary<string, object> {
                { "@nombre_completo", entidad.NombreCompleto },
                { "@tipo_documento", entidad.TipoDocumento.ToString() },
                { "@numero_documento", entidad.NumeroDocumento },
                { "@direccion_principal", entidad.DireccionPrincipal ?? (object)DBNull.Value },
                { "@fecha_registro", entidad.FechaRegistro },
                { "@activo", entidad.Activo ? 1 : 0 },
                { "@id_persona", entidad.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var consulta = $"""
                DELETE FROM adv__persona 
                WHERE id_persona = @id_persona;
                """;

            parametros = new Dictionary<string, object> {
                { "@id_persona", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaPersona filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consulta = filtroBusqueda switch {
                FiltroBusquedaPersona.Id => $"""
                    SELECT * FROM adv__persona 
                    WHERE id_persona = @id_persona;
                    """,
                FiltroBusquedaPersona.NombreCompleto => $"""
                    SELECT * FROM adv__persona 
                    WHERE LOWER(nombre_completo) LIKE LOWER(@nombre_completo);
                    """,
                FiltroBusquedaPersona.NumeroDocumento => $"""
                    SELECT * FROM adv__persona 
                    WHERE numero_documento = @numero_documento;
                    """,
                _ => "SELECT * FROM adv__persona;"
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaPersona.Id => new Dictionary<string, object> {
                    { "@id_persona", Convert.ToInt64(criterio) }
                },
                FiltroBusquedaPersona.NombreCompleto => new Dictionary<string, object> {
                    { "@nombre_completo", $"%{criterio}%" }
                },
                FiltroBusquedaPersona.NumeroDocumento => new Dictionary<string, object> {
                    { "@numero_documento", criterio }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (Persona, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            return (new Persona(
                id: Convert.ToInt64(lector["id_persona"]),
                nombreCompleto: Convert.ToString(lector["nombre_completo"]) ?? "N/A",
                tipoDocumento: Enum.TryParse<TipoDocumento>(Convert.ToString(lector["tipo_documento"]) ?? "NI", out var tipoDocumento) ? tipoDocumento : TipoDocumento.CI,
                numeroDocumento: Convert.ToString(lector["numero_documento"]) ?? "N/A",
                direccionPrincipal: lector["direccion_principal"] != DBNull.Value ? Convert.ToString(lector["direccion_principal"]) : null,
                fechaRegistro: Convert.ToDateTime(lector["fecha_registro"]),
                activo: Convert.ToBoolean(lector["activo"])
            ), new List<IEntidadBaseDatos>());
        }

        #region STATIC

        public static RepoPersona Instancia { get; } = new RepoPersona();

        #endregion

        #region UTILES

        public string[] NombresPersonasNoMensajeros() {
            var consulta = $"""
                SELECT p.nombre_completo
                FROM adv__persona p
                LEFT JOIN adv__mensajero m ON p.id_persona = m.id_persona
                WHERE m.id_persona IS NULL -- Excluye las personas que tienen un registro en adv__mensajero
                  AND p.activo = 1; -- Opcional: solo personas activas
                """;
            var parametros = new Dictionary<string, object>();

            return ContextoBaseDatos.EjecutarConsulta(consulta, parametros, MapearNombreCompleto).Select(result => result.entidadBase).ToArray() ?? [];
        }

        public string[] NombresPersonasNoClientes() {
            var consulta = $"""
                SELECT p.nombre_completo
                FROM adv__persona p
                LEFT JOIN adv__cliente c ON p.id_persona = c.id_persona
                WHERE c.id_persona IS NULL -- Excluye las personas que tienen un registro en adv__cliente
                  AND p.activo = 1; -- Opcional: solo personas activas
                """;
            var parametros = new Dictionary<string, object>();

            return ContextoBaseDatos.EjecutarConsulta(consulta, parametros, MapearNombreCompleto).Select(result => result.entidadBase).ToArray() ?? [];
        }

        public string[] NombresPersonasNoProveedores() {
            var consulta = $"""
                SELECT p.nombre_completo
                FROM adv__persona p
                LEFT JOIN adv__proveedor pr ON p.id_persona = pr.id_persona
                WHERE pr.id_persona IS NULL -- Excluye las personas que tienen un registro en adv__proveedor
                  AND p.activo = 1; -- Opcional: solo personas activas
                """;
            var parametros = new Dictionary<string, object>();

            return ContextoBaseDatos.EjecutarConsulta(consulta, parametros, MapearNombreCompleto).Select(result => result.entidadBase).ToArray() ?? [];
        }

        public string[] NombresPersonasNoEmpleados() {
            var consulta = $"""
                SELECT p.nombre_completo
                FROM adv__persona p
                LEFT JOIN adv__empleado e ON p.id_persona = e.id_persona
                WHERE e.id_persona IS NULL -- Excluye las personas que tienen un registro en adv__empleado
                  AND p.activo = 1; -- Opcional: solo personas activas
                """;
            var parametros = new Dictionary<string, object>();

            return ContextoBaseDatos.EjecutarConsulta(consulta, parametros, MapearNombreCompleto).Select(result => result.entidadBase).ToArray() ?? [];
        }

        private (string, List<IEntidadBaseDatos>) MapearNombreCompleto(MySqlDataReader lector) {
            return (Convert.ToString(lector["nombre_completo"]) ?? string.Empty, []);
        }

        #endregion
    }
}
