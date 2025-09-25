using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Modulos.Taller.Modelos;

using MySql.Data.MySqlClient;
using System.Globalization;

namespace aDVanceERP.Modulos.Taller.Repositorios
{
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

        protected override string GenerarComandoObtener(FiltroBusquedaOrdenGastoIndirecto criterio, string dato) {
            var datoSplit = dato.Split(';');

            return criterio switch {
                FiltroBusquedaOrdenGastoIndirecto.Todos =>
                    "SELECT * FROM adv__orden_gasto_indirecto;",
                FiltroBusquedaOrdenGastoIndirecto.Id =>
                    $"SELECT * FROM adv__orden_gasto_indirecto WHERE id_orden_gasto_indirecto = {dato};",
                FiltroBusquedaOrdenGastoIndirecto.OrdenProduccion =>
                    $"SELECT * FROM adv__orden_gasto_indirecto WHERE id_orden_produccion = {dato};",
                FiltroBusquedaOrdenGastoIndirecto.Concepto =>
                    datoSplit.Length > 1
                        ? $"SELECT * FROM adv__orden_gasto_indirecto WHERE id_orden_produccion = {datoSplit[0]} AND concepto = '{datoSplit[1]}'"
                        : $"SELECT * FROM adv__orden_gasto_indirecto WHERE concepto LIKE '%{dato}%';",
                FiltroBusquedaOrdenGastoIndirecto.FechaRegistro =>
                    $"SELECT * FROM adv__orden_gasto_indirecto WHERE DATE(fecha_registro) = '{dato}';",
                _ => throw new ArgumentOutOfRangeException(nameof(criterio), criterio, null)
            };
        }

        protected override OrdenGastoIndirecto MapearEntidad(MySqlDataReader lectorDatos) {
            return new OrdenGastoIndirecto {
                Id = lectorDatos.GetInt64("id_orden_gasto_indirecto"),
                IdOrdenProduccion = lectorDatos.GetInt64("id_orden_produccion"),
                Concepto = lectorDatos.GetString("concepto"),
                Cantidad = lectorDatos.GetDecimal("cantidad"),
                Monto = lectorDatos.GetDecimal("monto"),
                Total = lectorDatos.GetDecimal("total"),
                FechaRegistro = lectorDatos.GetDateTime("fecha_registro")
            };
        }

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
    }
}