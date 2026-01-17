using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos {
    public class RepoEmpleado : RepoEntidadBaseDatos<Empleado, FiltroBusquedaEmpleado> {
        public RepoEmpleado() : base("adv__empleado", "id_empleado") { }

        protected override string GenerarComandoAdicionar(Empleado entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                INSERT INTO adv__empleado (
                    id_persona,
                    codigo_empleado,
                    fecha_contratacion,
                    fecha_nacimiento,
                    cargo,
                    departamento,
                    salario,
                    activo
                ) VALUES (
                    @id_persona,
                    @codigo_empleado,
                    @fecha_contratacion,
                    @fecha_nacimiento,
                    @cargo,
                    @departamento,
                    @salario,
                    @activo
                )
                """;

            parametros = new Dictionary<string, object> {
                { "@id_persona", entidad.IdPersona },
                { "@codigo_empleado", entidad.CodigoEmpleado },
                { "@fecha_contratacion", entidad.FechaContratacion.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@fecha_nacimiento", entidad.FechaNacimiento.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@cargo", entidad.Cargo },
                { "@departamento", entidad.Departamento },
                { "@salario", entidad.Salario.ToString("N2", CultureInfo.InvariantCulture) },
                { "@activo", entidad.Activo ? 1 : 0 }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(Empleado entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                UPDATE adv__empleado 
                SET 
                    id_persona = @id_persona,
                    codigo_empleado = @codigo_empleado,
                    fecha_contratacion = @fecha_contratacion,
                    fecha_nacimiento = @fecha_nacimiento,
                    cargo = @cargo,
                    departamento = @departamento,
                    salario = @salario,
                    activo = @activo
                WHERE id_empleado = @id_empleado;
                """;

            parametros = new Dictionary<string, object> {
                { "@id_persona", entidad.IdPersona },
                { "@codigo_empleado", entidad.CodigoEmpleado },
                { "@fecha_contratacion", entidad.FechaContratacion.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@fecha_nacimiento", entidad.FechaNacimiento.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@cargo", entidad.Cargo },
                { "@departamento", entidad.Departamento },
                { "@salario", entidad.Salario.ToString("N2", CultureInfo.InvariantCulture) },
                { "@activo", entidad.Activo ? 1 : 0 },
                { "@id_empleado", entidad.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var consulta = $"""
                DELETE FROM adv__empleado
                WHERE id_empleado = @id_empleado;
                """;

            parametros = new Dictionary<string, object> {
                { "@id_empleado", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaEmpleado filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consultaComun = $"""
                SELECT * FROM adv__empleado e
                INNER JOIN adv__persona p ON e.id_persona = p.id_persona
                """;
            var consulta = filtroBusqueda switch {
                FiltroBusquedaEmpleado.Id => $"""
                        {consultaComun}
                        WHERE id_empleado = @id_empleado;
                        """,
                FiltroBusquedaEmpleado.IdPersona => $"""
                        {consultaComun}
                        WHERE e.id_persona = @id_persona;
                        """,
                FiltroBusquedaEmpleado.CodigoEmpleado => $"""
                        {consultaComun}
                        WHERE codigo_empleado = @codigo_empleado;
                        """,
                FiltroBusquedaEmpleado.Cargo => $"""
                        {consultaComun}
                        WHERE cargo = @cargo;
                        """,
                FiltroBusquedaEmpleado.Departamento => $"""
                        {consultaComun}
                        WHERE departamento = @departamento;
                        """,
                _ => $"""
                        {consultaComun};
                        """
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaEmpleado.Id => new Dictionary<string, object> {
                        { "@id_empleado", Convert.ToInt64(criterio) }
                    },
                FiltroBusquedaEmpleado.IdPersona => new Dictionary<string, object> {
                        { "@id_persona", Convert.ToInt64(criterio) }
                    },
                FiltroBusquedaEmpleado.CodigoEmpleado => new Dictionary<string, object> {
                        { "@codigo_empleado", criterio }
                    },
                FiltroBusquedaEmpleado.Cargo => new Dictionary<string, object> {
                        { "@cargo", criterio }
                    },
                FiltroBusquedaEmpleado.Departamento => new Dictionary<string, object> {
                        { "@departamento", criterio }
                    },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (Empleado, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var persona = new Persona(
                id: Convert.ToInt64(lector["id_persona"]),
                nombreCompleto: Convert.ToString(lector["nombre_completo"]) ?? "N/A",
                tipoDocumento: Enum.TryParse<TipoDocumento>(Convert.ToString(lector["tipo_documento"]) ?? "NI", out var tipoDocumento) ? tipoDocumento : TipoDocumento.NI,
                numeroDocumento: Convert.ToString(lector["numero_documento"]) ?? "N/A",
                direccionPrincipal: lector["direccion_principal"] != DBNull.Value ? Convert.ToString(lector["direccion_principal"]) : null,
                fechaRegistro: Convert.ToDateTime(lector["fecha_registro"]),
                activo: Convert.ToBoolean(lector["activo"])
            );

            return (new Empleado(
                id: Convert.ToInt64(lector["id_empleado"]),
                idPersona: Convert.ToInt64(lector["id_persona"]),
                codigoEmpleado: Convert.ToString(lector["codigo_empleado"]) ?? "N/A",
                fechaContratacion: Convert.ToDateTime(lector["fecha_contratacion"]),
                fechaNacimiento: Convert.ToDateTime(lector["fecha_nacimiento"]),
                cargo: Convert.ToString(lector["cargo"] ?? "N/A"),
                departamento: Convert.ToString(lector["departamento"] ?? "N/A"),
                salario: Convert.ToDecimal(lector["salario"], CultureInfo.InvariantCulture),
                activo: Convert.ToBoolean(lector["activo"])
            ), new List<IEntidadBaseDatos>() {
                persona
            });
        }

        #region STATIC

        public static RepoEmpleado Instancia { get; } = new RepoEmpleado();

        #endregion
    }
}
