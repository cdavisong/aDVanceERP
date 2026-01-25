using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Ventas;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;


namespace aDVanceERP.Core.Repositorios.Modulos.Ventas {
    public class RepoMensajero : RepoEntidadBaseDatos<Mensajero, FiltroBusquedaMensajero> {
        public RepoMensajero() : base("adv__mensajero", "id_mensajero") {
        }

        protected override string GenerarComandoAdicionar(Mensajero entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
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
                );
                """;

            parametros = new Dictionary<string, object> {
                { "@id_persona", entidad.IdPersona },
                { "@codigo_mensajero", entidad.CodigoMensajero },
                { "@matricula_vehiculo", entidad.MatriculaVehiculo },
                { "@activo", entidad.Activo }
            };

            return comando;
        }

        protected override string GenerarComandoEditar(Mensajero entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
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

            return comando;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var comando = $"""
                DELETE FROM adv__mensajero 
                WHERE id_mensajero = @id_mensajero
                """;

            parametros = new Dictionary<string, object> {
                { "@id_mensajero", id }
            };

            return comando;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaMensajero filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;

            var consultaComun = $"""
                SELECT m.*, p.nombre_completo, p.numero_documento
                FROM adv__mensajero m
                LEFT JOIN adv__persona p ON m.id_persona = p.id_persona
                WHERE m.activo = 1
                """;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaMensajero.Id => $"""
                    {consultaComun}
                    AND m.id_mensajero = @id_mensajero
                    """,
                FiltroBusquedaMensajero.IdPersona => $"""
                    {consultaComun}
                    AND m.id_persona = @id_persona
                    """,
                FiltroBusquedaMensajero.CodigoMensajero => $"""
                    {consultaComun}
                    AND m.codigo_mensajero = @codigo_mensajero
                    """,
                FiltroBusquedaMensajero.MatriculaVehiculo => $"""
                    {consultaComun}
                    AND m.matricula_vehiculo = @matricula_vehiculo
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
            var mensajero = new Mensajero {
                Id = Convert.ToInt64(lector["id_mensajero"]),
                IdPersona = Convert.ToInt64(lector["id_persona"]),
                CodigoMensajero = Convert.ToString(lector["codigo_mensajero"]) ?? "N/A",
                MatriculaVehiculo = lector["matricula_vehiculo"] != DBNull.Value ? Convert.ToString(lector["matricula_vehiculo"]) : null,
                Activo = Convert.ToBoolean(lector["activo"])
            };

            var entidadesExtra = new List<IEntidadBaseDatos>();

            if (lector.VisibleFieldCount > 5) {
                entidadesExtra.Add(new Persona {
                    NombreCompleto = Convert.ToString(lector["nombre_completo"]) ?? "N/A",
                    NumeroDocumento = Convert.ToString(lector["numero_documento"]) ?? "N/A"
                });
            }

            return (mensajero, entidadesExtra);
        }

        #region STATIC

        public static RepoMensajero Instancia { get; } = new RepoMensajero();

        #endregion

        #region UTILES

        public bool HabilitarDeshabilitarMensajero(long id) {
            var consulta = $"""
                UPDATE adv__mensajero
                SET activo = NOT activo
                WHERE id_mensajero = @IdMensajero;
                """;
            var parametros = new Dictionary<string, object> {
                { "@IdMensajero", id }
            };

            ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);

            consulta = $"""
                SELECT activo
                FROM adv__mensajero
                WHERE id_mensajero = @IdMensajero;
                """;

            var resultado = ContextoBaseDatos.EjecutarConsultaEscalar<bool>(consulta, parametros);
            return resultado;
        }

        public List<Mensajero> ObtenerMensajerosActivos() {
            var consulta = $"""
                SELECT m.*, p.nombre_completo
                FROM adv__mensajero m
                LEFT JOIN adv__persona p ON m.id_persona = p.id_persona
                WHERE m.activo = 1
                ORDER BY p.nombre_completo;
                """;

            var parametros = new Dictionary<string, object>();

            var mensajeros = new List<Mensajero>();
            var resultados = ContextoBaseDatos.EjecutarConsulta(
                consulta,
                parametros,
                (reader) => {
                    var (mensajero, entidadesExtra) = MapearEntidad(reader);
                    return (mensajero, entidadesExtra);
                }
            );

            foreach (var (mensajero, _) in resultados) {
                mensajeros.Add(mensajero);
            }

            return mensajeros;
        }

        public int ContarEntregasPorMensajero(long idMensajero, DateTime? fechaInicio = null, DateTime? fechaFin = null) {
            var consulta = $"""
                SELECT COUNT(*) as total_entregas
                FROM adv__seguimiento_entrega
                WHERE id_mensajero = @id_mensajero
                AND estado_entrega = 'Entregado'
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_mensajero", idMensajero }
            };

            if (fechaInicio.HasValue) {
                consulta += " AND fecha_entrega_realizada >= @fecha_inicio";
                parametros.Add("@fecha_inicio", fechaInicio.Value.ToString("yyyy-MM-dd 00:00:00"));
            }

            if (fechaFin.HasValue) {
                consulta += " AND fecha_entrega_realizada <= @fecha_fin";
                parametros.Add("@fecha_fin", fechaFin.Value.ToString("yyyy-MM-dd 23:59:59"));
            }

            var resultado = ContextoBaseDatos.EjecutarConsultaEscalar<int>(consulta, parametros);
            return resultado;
        }

        public bool AsignarMatriculaVehiculo(long idMensajero, string matricula) {
            var consulta = $"""
                UPDATE adv__mensajero
                SET matricula_vehiculo = @matricula_vehiculo
                WHERE id_mensajero = @id_mensajero;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_mensajero", idMensajero },
                { "@matricula_vehiculo", matricula }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        #endregion
    }
}