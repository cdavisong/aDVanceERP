using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Taller;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Taller {
    public class RepoOrdenGastoIndirecto : RepoEntidadBaseDatos<OrdenGastoIndirecto, FiltroBusquedaOrdenGastoIndirecto> {
        public RepoOrdenGastoIndirecto() : base("adv__orden_gasto_indirecto", "id_orden_gasto_indirecto") { }

        protected override string GenerarComandoAdicionar(OrdenGastoIndirecto objeto) {
            return $"""
                INSERT INTO adv__orden_gasto_indirecto (
                    id_orden_produccion,
                    concepto,
                    cantidad,
                    monto,
                    total,
                    fecha_registro
                )
                VALUES (
                    {objeto.IdOrdenProduccion},
                    '{objeto.Concepto}',
                    {objeto.Cantidad.ToString(CultureInfo.InvariantCulture)},
                    {objeto.Monto.ToString(CultureInfo.InvariantCulture)},
                    {objeto.Total.ToString(CultureInfo.InvariantCulture)},
                    '{objeto.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}'
                );
                """;
        }

        protected override string GenerarComandoEditar(OrdenGastoIndirecto objeto) {
            return $"""
                UPDATE adv__orden_gasto_indirecto
                SET
                    id_orden_produccion = {objeto.IdOrdenProduccion},
                    concepto = '{objeto.Concepto}',
                    cantidad = {objeto.Cantidad.ToString(CultureInfo.InvariantCulture)},
                    monto = {objeto.Monto.ToString(CultureInfo.InvariantCulture)},
                    total = {objeto.Total.ToString(CultureInfo.InvariantCulture)},
                    fecha_registro = '{objeto.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}'
                WHERE id_orden_gasto_indirecto = {objeto.Id};
                """;
        }

        protected override string GenerarComandoEliminar(long id) {
            return $"""
                -- Eliminar gastos dinamicos asociados si existen
                DELETE FROM adv__orden_gasto_dinamico
                WHERE id_orden_gasto_indirecto = {id};

                DELETE FROM adv__orden_gasto_indirecto 
                WHERE id_orden_gasto_indirecto = {id};
                """;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaOrdenGastoIndirecto filtroBusqueda, string criterio) {
            string? comando;
            string[] datoSplit = criterio.Split(';');

            switch(filtroBusqueda) {
                case FiltroBusquedaOrdenGastoIndirecto.Id:
                    comando = $"""
                        SELECT * 
                        FROM adv__orden_gasto_indirecto 
                        WHERE id_orden_gasto_indirecto = {criterio};
                        """;
                    break;
                case FiltroBusquedaOrdenGastoIndirecto.OrdenProduccion:
                    comando = $"""
                        SELECT * 
                        FROM adv__orden_gasto_indirecto 
                        WHERE id_orden_produccion = {criterio};
                        """;
                    break;
                case FiltroBusquedaOrdenGastoIndirecto.Concepto:
                    if (datoSplit.Length > 1) {
                        comando = $"""
                            SELECT * 
                            FROM adv__orden_gasto_indirecto 
                            WHERE id_orden_produccion = {datoSplit[0]} 
                              AND concepto = '{datoSplit[1]}';
                            """;
                    } else {
                        comando = $"""
                            SELECT * 
                            FROM adv__orden_gasto_indirecto 
                            WHERE concepto LIKE '%{criterio}%';
                            """;
                    }
                    break;
                case FiltroBusquedaOrdenGastoIndirecto.FechaRegistro:
                    comando = $"""
                        SELECT * 
                        FROM adv__orden_gasto_indirecto 
                        WHERE DATE(fecha_registro) = '{criterio}';
                        """;
                    break;
                default:
                    comando = """
                        SELECT * 
                        FROM adv__orden_gasto_indirecto;
                        """;
                    break;
            }

            return comando;
        }

        protected override OrdenGastoIndirecto MapearEntidad(MySqlDataReader lectorDatos) {
            return new OrdenGastoIndirecto(
                id: Convert.ToInt64(lectorDatos["id_orden_gasto_indirecto"]),
                idOrdenProduccion: Convert.ToInt64(lectorDatos["id_orden_produccion"]),
                concepto: Convert.ToString(lectorDatos["concepto"]) ?? string.Empty,
                cantidad: Convert.ToDecimal(lectorDatos["cantidad"], CultureInfo.InvariantCulture),
                monto: Convert.ToDecimal(lectorDatos["monto"], CultureInfo.InvariantCulture),
                costoTotal: Convert.ToDecimal(lectorDatos["total"], CultureInfo.InvariantCulture)) {
                FechaRegistro = Convert.ToDateTime(lectorDatos["fecha_registro"])
            };
        }

        #region UTILES

        #endregion

        public bool EsDinamico(OrdenGastoIndirecto gasto, out string formula) {
            var consulta = $"""
                SELECT COUNT(*) FROM adv__orden_gasto_indirecto gi
                JOIN adv__gasto_dinamico gd ON gi.id_orden_gasto_indirecto = gd.id_orden_gasto_indirecto
                WHERE gi.id_orden_gasto_indirecto = {gasto.Id};
                """;
            var cantidad = ContextoBaseDatos.EjecutarConsultaEscalar<int>(consulta);

            if (cantidad > 0) {
                var consultaFormula = $"""
                    SELECT gd.formula FROM adv__orden_gasto_indirecto gi
                    JOIN adv__gasto_dinamico gd ON gi.id_orden_gasto_indirecto = gd.id_orden_gasto_indirecto
                    WHERE gi.id_orden_gasto_indirecto = {gasto.Id};
                    """;
                formula = ContextoBaseDatos.EjecutarConsultaEscalar<string>(consultaFormula);
                return true;
            } else {
                formula = string.Empty;
                return false;
            }
        }

        #region STATIC

        public static RepoOrdenGastoIndirecto Instancia { get; } = new RepoOrdenGastoIndirecto();

        #endregion
    }
}