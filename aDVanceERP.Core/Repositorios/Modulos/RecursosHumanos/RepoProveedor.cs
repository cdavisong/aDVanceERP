using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos {
    public class RepoProveedor : RepoEntidadBaseDatos<Proveedor, FiltroBusquedaProveedor> {
        public RepoProveedor() : base("adv__proveedor", "id_proveedor") {
        }

        protected override string GenerarComandoAdicionar(Proveedor entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                INSERT INTO adv__proveedor (
                    id_persona,
                    codigo_proveedor,
                    razon_social,
                    nit,
                    condiciones_pago,
                    fecha_registro,
                    activo
                ) VALUES (
                    @id_persona,
                    @codigo_proveedor,
                    @razon_social,
                    @nit,
                    @condiciones_pago,
                    @fecha_registro,
                    @activo
                )
                """;

            parametros = new Dictionary<string, object> {
                { "@id_persona", entidad.IdPersona },
                { "@codigo_proveedor", entidad.CodigoProveedor },
                { "@razon_social", entidad.RazonSocial },
                { "@nit", entidad.NIT },
                { "@condiciones_pago", entidad.CondicionesPago },
                { "@fecha_registro", entidad.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@activo", entidad.Activo }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(Proveedor entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                UPDATE adv__proveedor 
                SET 
                    id_persona = @id_persona,
                    codigo_proveedor = @codigo_proveedor,
                    razon_social = @razon_social,
                    nit = @nit,
                    condiciones_pago = @condiciones_pago,
                    fecha_registro = @fecha_registro,
                    activo = @activo
                WHERE id_proveedor = @id_proveedor
                """;

            parametros = new Dictionary<string, object> {
                { "@id_persona", entidad.IdPersona },
                { "@codigo_proveedor", entidad.CodigoProveedor },
                { "@razon_social", entidad.RazonSocial },
                { "@nit", entidad.NIT },
                { "@condiciones_pago", entidad.CondicionesPago },
                { "@fecha_registro", entidad.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@activo", entidad.Activo },
                { "@id_proveedor", entidad.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            const string consulta = """
                DELETE FROM adv__proveedor 
                WHERE id_proveedor = @id_proveedor
                """;

            parametros = new Dictionary<string, object> {
                { "@id_proveedor", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaProveedor filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consultaComun = $"""
                SELECT *
                FROM adv__proveedor p
                INNER JOIN adv__persona per ON p.id_persona = per.id_persona
                """;
            var consulta = filtroBusqueda switch {
                FiltroBusquedaProveedor.Id => $"""
                    {consultaComun}
                    WHERE p.id_proveedor = @id_proveedor
                    """,
                FiltroBusquedaProveedor.IdPersona => $"""
                    {consultaComun}
                    WHERE p.id_persona = @id_persona
                    """,
                FiltroBusquedaProveedor.CodigoProveedor => $"""
                    {consultaComun}
                    WHERE p.codigo_proveedor = @codigo_proveedor
                    """,
                FiltroBusquedaProveedor.RazonSocial => $"""
                    {consultaComun}
                    WHERE p.razon_social LIKE @razon_social
                    """,
                FiltroBusquedaProveedor.NIT => $"""
                    {consultaComun}
                    WHERE p.nit = @nit
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaProveedor.Id => new Dictionary<string, object> {
                    { "@id_proveedor", long.Parse(criterio) }
                },
                FiltroBusquedaProveedor.IdPersona => new Dictionary<string, object> {
                    { "@id_persona", long.Parse(criterio) }
                },
                FiltroBusquedaProveedor.CodigoProveedor => new Dictionary<string, object> {
                    { "@codigo_proveedor", criterio }
                },
                FiltroBusquedaProveedor.RazonSocial => new Dictionary<string, object> {
                    { "@razon_social", $"%{criterio}%" }
                },
                FiltroBusquedaProveedor.NIT => new Dictionary<string, object> {
                    { "@nit", criterio }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (Proveedor, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var persona = new Persona(
                id: Convert.ToInt64(lector["id_persona"]),
                nombreCompleto: Convert.ToString(lector["nombre_completo"]) ?? "N/A",
                tipoDocumento: Enum.TryParse<TipoDocumento>(Convert.ToString(lector["tipo_documento"]) ?? "NI", out var tipoDocumento) ? tipoDocumento : TipoDocumento.CI,
                numeroDocumento: Convert.ToString(lector["numero_documento"]) ?? "N/A",
                direccionPrincipal: lector["direccion_principal"] != DBNull.Value ? Convert.ToString(lector["direccion_principal"]) : null,
                fechaRegistro: Convert.ToDateTime(lector["fecha_registro"]),
                activo: Convert.ToBoolean(lector["activo"])
            );

            return (new Proveedor {
                Id = Convert.ToInt64(lector["id_proveedor"]),
                IdPersona = persona.Id,
                CodigoProveedor = Convert.ToString(lector["codigo_proveedor"]) ?? "N/A",
                RazonSocial = Convert.ToString(lector["razon_social"]) ?? "N/A",
                NIT = Convert.ToString(lector["nit"]) ?? "N/A",
                CondicionesPago = Convert.ToString(lector["condiciones_pago"]) ?? "N/A",
                FechaRegistro = Convert.ToDateTime(lector["fecha_registro"]),
                Activo = Convert.ToBoolean(lector["activo"])
            }, new List<IEntidadBaseDatos>() {
                persona
            });
        }

        #region STATIC

        public static RepoProveedor Instancia { get; } = new RepoProveedor();

        #endregion
    }
}
