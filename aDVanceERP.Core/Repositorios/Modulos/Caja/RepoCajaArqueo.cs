using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Caja {

    public class RepoCajaArqueo : RepoEntidadBaseDatos<CajaArqueo, FiltroBusquedaCajaTurno> {

        public RepoCajaArqueo() : base("adv__caja_arqueo", "id_arqueo") { }

        // ── CRUD base ─────────────────────────────────────────────

        protected override string GenerarComandoAdicionar(
                CajaArqueo entidad,
                out Dictionary<string, object> parametros,
                params IEntidadBaseDatos[] entidadesExtra) {

            // Usa INSERT … ON DUPLICATE KEY UPDATE para respetar
            // el UNIQUE KEY (id_turno, denominacion) sin lanzar error
            // si el cajero corrige una denominación ya ingresada.
            var comando = """
                INSERT INTO adv__caja_arqueo (id_turno, denominacion, cantidad)
                VALUES (@id_turno, @denominacion, @cantidad)
                ON DUPLICATE KEY UPDATE cantidad = VALUES(cantidad);
                """;

            parametros = new Dictionary<string, object> {
                { "@id_turno",     entidad.IdTurno },
                { "@denominacion", entidad.Denominacion },
                { "@cantidad",     entidad.Cantidad }
            };

            return comando;
        }

        protected override string GenerarComandoEditar(
                CajaArqueo entidad,
                out Dictionary<string, object> parametros,
                params IEntidadBaseDatos[] entidadesExtra) {

            var comando = """
                UPDATE adv__caja_arqueo
                SET cantidad = @cantidad
                WHERE id_arqueo = @id_arqueo;
                """;

            parametros = new Dictionary<string, object> {
                { "@id_arqueo", entidad.Id },
                { "@cantidad",  entidad.Cantidad }
            };

            return comando;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var comando = """
                DELETE FROM adv__caja_arqueo
                WHERE id_arqueo = @id_arqueo;
                """;

            parametros = new Dictionary<string, object> {
                { "@id_arqueo", id }
            };

            return comando;
        }

        protected override string GenerarComandoObtener(
                FiltroBusquedaCajaTurno filtroBusqueda,
                out Dictionary<string, object> parametros,
                params string[] criteriosBusqueda) {

            var criterio = criteriosBusqueda.Length >= 1 ? criteriosBusqueda[^1] : string.Empty;

            // RepoCajaArqueo solo necesita consultar por turno
            var consulta = """
                SELECT id_arqueo, id_turno, denominacion, cantidad, subtotal
                FROM adv__caja_arqueo
                WHERE id_turno = @id_turno
                ORDER BY denominacion DESC;
                """;

            parametros = new Dictionary<string, object> {
                { "@id_turno", Convert.ToInt64(string.IsNullOrEmpty(criterio) ? "0" : criterio) }
            };

            return consulta;
        }

        protected override (CajaArqueo, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var arqueo = new CajaArqueo {
                Id = Convert.ToInt64(lector["id_arqueo"]),
                IdTurno = Convert.ToInt64(lector["id_turno"]),
                Denominacion = Convert.ToDecimal(lector["denominacion"], CultureInfo.InvariantCulture),
                Cantidad = Convert.ToInt32(lector["cantidad"]),
                Subtotal = Convert.ToDecimal(lector["subtotal"], CultureInfo.InvariantCulture)
            };

            return (arqueo, new List<IEntidadBaseDatos>());
        }

        // ── Singleton ─────────────────────────────────────────────

        public static RepoCajaArqueo Instancia { get; } = new RepoCajaArqueo();

        // ── Operaciones específicas ───────────────────────────────

        /// <summary>
        /// Reemplaza todas las filas de arqueo de un turno en una sola transacción.
        /// El presentador llama a esto cuando el cajero confirma el conteo completo.
        /// </summary>
        public bool GuardarArqueoCompleto(long idTurno, IEnumerable<CajaArqueo> denominaciones) {
            var consultaEliminar = """
                DELETE FROM adv__caja_arqueo
                WHERE id_turno = @id_turno;
                """;

            var consultaInsertar = """
                INSERT INTO adv__caja_arqueo (id_turno, denominacion, cantidad)
                VALUES (@id_turno, @denominacion, @cantidad);
                """;

            using var conexion = ContextoBaseDatos.ObtenerConexionOptimizada();
            conexion.Open();

            using var transaccion = conexion.BeginTransaction();

            try {
                // 1. Limpiar arqueo anterior del turno
                ContextoBaseDatos.EjecutarComandoNoQuery(
                    consultaEliminar,
                    new Dictionary<string, object> { { "@id_turno", idTurno } },
                    conexion);

                // 2. Insertar las denominaciones nuevas (solo las que tienen cantidad > 0)
                foreach (var den in denominaciones.Where(d => d.Cantidad > 0)) {
                    ContextoBaseDatos.EjecutarComandoNoQuery(
                        consultaInsertar,
                        new Dictionary<string, object> {
                            { "@id_turno",     idTurno },
                            { "@denominacion", den.Denominacion },
                            { "@cantidad",     den.Cantidad }
                        },
                        conexion);
                }

                transaccion.Commit();
                return true;
            } catch {
                transaccion.Rollback();
                throw;
            } finally {
                conexion.Close();
            }
        }

        /// <summary>
        /// Devuelve todas las filas de arqueo de un turno ordenadas de mayor a menor denominación.
        /// </summary>
        public List<CajaArqueo> ObtenerPorTurno(long idTurno) {
            var consulta = """
                SELECT id_arqueo, id_turno, denominacion, cantidad, subtotal
                FROM adv__caja_arqueo
                WHERE id_turno = @id_turno
                ORDER BY denominacion DESC;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_turno", idTurno }
            };

            return ContextoBaseDatos.EjecutarConsulta(consulta, parametros, MapearEntidad)
                .Select(r => r.entidadBase)
                .ToList();
        }
    }
}   