using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos {
    public class RepoMensajero : RepoEntidadBaseDatos<Mensajero, FiltroBusquedaMensajero> {
        public RepoMensajero() : base("adv__mensajero", "id_mensajero") { }

        protected override string GenerarComandoAdicionar(Mensajero entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                INSERT INTO adv__mensajero (
                    id_persona,
                    codigo_mensajero,
                    matricula_vehiculo,
                    activo
                ) VALUES (
                    @id_persona,
                    @codigo_mensajero,
                    @matricula_vehiculo,
                    @activo
                )
                """;

            parametros = new Dictionary<string, object> {
                { "@id_persona", entidad.IdPersona },
                { "@codigo_mensajero", entidad.CodigoMensajero },
                { "@matricula_vehiculo", entidad.MatriculaVehiculo },
                { "@activo", entidad.Activo }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(Mensajero entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                UPDATE adv__mensajero 
                SET 
                    id_persona = @id_persona,
                    codigo_mensajero = @codigo_mensajero,
                    matricula_vehiculo = @matricula_vehiculo,
                    activo = @activo
                WHERE id_mensajero = @id_mensajero
                """;

            parametros = new Dictionary<string, object> {
                { "@id_mensajero", entidad.Id },
                { "@id_persona", entidad.IdPersona },
                { "@codigo_mensajero", entidad.CodigoMensajero },
                { "@matricula_vehiculo", entidad.MatriculaVehiculo },
                { "@activo", entidad.Activo }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var consulta = $"""
                DELETE FROM adv__mensajero 
                WHERE id_mensajero = @id_mensajero
                """;

            parametros = new Dictionary<string, object> {
                { "@id_mensajero", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaMensajero filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consultaComun = $"""
                SELECT *
                FROM adv__mensajero m
                INNER JOIN adv__persona p ON m.id_persona = p.id_persona
                """;
            var consulta = filtroBusqueda switch {
                FiltroBusquedaMensajero.Id => $"""
                    {consultaComun}
                    WHERE m.id_mensajero = @id_mensajero
                    """,
                FiltroBusquedaMensajero.IdPersona => $"""
                    {consultaComun}
                    WHERE m.id_persona = @id_persona
                    """,
                FiltroBusquedaMensajero.CodigoMensajero => $"""
                    {consultaComun}
                    WHERE m.codigo_mensajero = @codigo_mensajero
                    """,
                FiltroBusquedaMensajero.MatriculaVehiculo => $"""
                    {consultaComun}
                    WHERE m.matricula_vehiculo = @matricula_vehiculo
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaMensajero.Id => new Dictionary<string, object> {
                    { "@id_mensajero", long.Parse(criterio) }
                },
                FiltroBusquedaMensajero.IdPersona => new Dictionary<string, object> {
                    { "@id_persona", long.Parse(criterio) }
                },
                FiltroBusquedaMensajero.CodigoMensajero => new Dictionary<string, object> {
                    { "@codigo_mensajero", criterio }
                },
                FiltroBusquedaMensajero.MatriculaVehiculo => new Dictionary<string, object> {
                    { "@matricula_vehiculo", criterio }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (Mensajero, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var persona = new Persona(
                id: Convert.ToInt64(lector["id_persona"]),
                nombreCompleto: Convert.ToString(lector["nombre_completo"]) ?? "N/A",
                tipoDocumento: Enum.TryParse<TipoDocumento>(Convert.ToString(lector["tipo_documento"]) ?? "NI", out var tipoDocumento) ? tipoDocumento : TipoDocumento.NI,
                numeroDocumento: Convert.ToString(lector["numero_documento"]) ?? "N/A",
                direccionPrincipal: lector["direccion_principal"] != DBNull.Value ? Convert.ToString(lector["direccion_principal"]) : null,
                fechaRegistro: Convert.ToDateTime(lector["fecha_registro"]),
                activo: Convert.ToBoolean(lector["activo"])
            );

            return (new Mensajero {
                Id = Convert.ToInt64(lector["id_mensajero"]),
                IdPersona = persona.Id,
                CodigoMensajero = Convert.ToString(lector["codigo_mensajero"]) ?? "N/A",
                MatriculaVehiculo = Convert.ToString(lector["matricula_vehiculo"]) ?? "N/A",
                Activo = Convert.ToBoolean(lector["activo"])
            }, new List<IEntidadBaseDatos>() {
                persona
            });
        }

        #region STATIC

        public static RepoMensajero Instancia { get; } = new RepoMensajero();

        #endregion
    }
}
