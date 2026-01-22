using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos {
    public class RepoCliente : RepoEntidadBaseDatos<Cliente, FiltroBusquedaCliente> {
        public RepoCliente() : base("adv__cliente", "id_cliente") {
        }

        protected override string GenerarComandoAdicionar(Cliente entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
                INSERT INTO adv__cliente (
                    id_persona,
                    codigo_cliente,
                    limite_credito,
                    fecha_registro,
                    activo
                ) VALUES (
                    @id_persona,
                    @codigo_cliente,
                    @limite_credito,
                    @fecha_registro,
                    @activo
                );
                """;

            parametros = new Dictionary<string, object> {
                { "@id_persona", entidad.IdPersona },
                { "@codigo_cliente", entidad.CodigoCliente },
                { "@limite_credito", entidad.LimiteCredito.ToString(CultureInfo.InvariantCulture) },
                { "@fecha_registro", entidad.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@activo", entidad.Activo }
            };

            return comando;
        }

        protected override string GenerarComandoEditar(Cliente entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
                UPDATE adv__cliente 
                SET 
                    id_persona = @id_persona,
                    codigo_cliente = @codigo_cliente,
                    limite_credito = @limite_credito,
                    fecha_registro = @fecha_registro,
                    activo = @activo
                WHERE id_cliente = @id_cliente
                """;

            parametros = new Dictionary<string, object> {
                { "@id_cliente", entidad.Id },
                { "@id_persona", entidad.IdPersona },
                { "@codigo_cliente", entidad.CodigoCliente },
                { "@limite_credito", entidad.LimiteCredito.ToString(CultureInfo.InvariantCulture) },
                { "@fecha_registro", entidad.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@activo", entidad.Activo }
            };

            return comando;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var comando = $"""
                DELETE FROM adv__cliente 
                WHERE id_cliente = @id_cliente
                """;

            parametros = new Dictionary<string, object> {
                { "@id_cliente", id }
            };

            return comando;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaCliente filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consultaComun = $"""
                SELECT *
                FROM adv__cliente c
                JOIN adv__persona p ON c.id_persona = p.id_persona
                """;
            var consulta = filtroBusqueda switch {
                FiltroBusquedaCliente.Id => $"""
                    {consultaComun}
                    WHERE c.id_cliente = @id_cliente
                    """,
                FiltroBusquedaCliente.IdPersona => $"""
                    {consultaComun}
                    WHERE c.id_persona = @id_persona
                    """,
                FiltroBusquedaCliente.CodigoCliente => $"""
                    {consultaComun}
                    WHERE c.codigo_cliente = @codigo_cliente
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaCliente.Id => new Dictionary<string, object> {
                    { "@id_cliente", long.Parse(criterio) }
                },
                FiltroBusquedaCliente.IdPersona => new Dictionary<string, object> {
                    { "@id_persona", long.Parse(criterio) }
                },
                FiltroBusquedaCliente.CodigoCliente => new Dictionary<string, object> {
                    { "@codigo_cliente", criterio }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (Cliente, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var persona = lector.VisibleFieldCount > 6
                ? new Persona(
                    id: Convert.ToInt64(lector["id_persona"]),
                    nombreCompleto: Convert.ToString(lector["nombre_completo"]) ?? "N/A",
                    tipoDocumento: Enum.TryParse<TipoDocumento>(Convert.ToString(lector["tipo_documento"]) ?? "NI", out var tipoDocumento) ? tipoDocumento : TipoDocumento.CI,
                    numeroDocumento: Convert.ToString(lector["numero_documento"]) ?? "N/A",
                    direccionPrincipal: lector["direccion_principal"] != DBNull.Value ? Convert.ToString(lector["direccion_principal"]) : null,
                    fechaRegistro: Convert.ToDateTime(lector["fecha_registro"]),
                    activo: Convert.ToBoolean(lector["activo"])
                ) : null!;

            return (new Cliente {
                Id = Convert.ToInt64(lector["id_cliente"]),
                IdPersona = Convert.ToInt64(lector["id_persona"]),
                CodigoCliente = Convert.ToString(lector["codigo_cliente"]) ?? "N/A",
                LimiteCredito = Convert.ToDecimal(lector["limite_credito"], CultureInfo.InvariantCulture),
                FechaRegistro = Convert.ToDateTime(lector["fecha_registro"]),
                Activo = Convert.ToBoolean(lector["activo"])
            }, persona != null ? [persona] : []);
        }

        #region STATIC

        public static RepoCliente Instancia { get; } = new RepoCliente();

        #endregion
    }
}
