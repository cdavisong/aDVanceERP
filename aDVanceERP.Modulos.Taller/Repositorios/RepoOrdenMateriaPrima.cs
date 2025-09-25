using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Modulos.Taller.Modelos;

using MySql.Data.MySqlClient;
using System.Globalization;

namespace aDVanceERP.Modulos.Taller.Repositorios
{
    public class RepoOrdenMateriaPrima : RepoEntidadBaseDatos<OrdenMateriaPrima, FiltroBusquedaOrdenMateriaPrima> {
        public RepoOrdenMateriaPrima() : base("adv__orden_material", "id_orden_material") { }

        protected override string GenerarComandoAdicionar(OrdenMateriaPrima objeto) {
            return $"""
                INSERT INTO adv__orden_material (
                    id_orden_produccion,
                    id_almacen,
                    id_producto,
                    cantidad,
                    costo_unitario,
                    total,
                    fecha_registro
                )
                VALUES (
                    {objeto.IdOrdenProduccion},
                    {objeto.IdAlmacen},
                    {objeto.IdProducto},
                    {objeto.Cantidad.ToString(CultureInfo.InvariantCulture)},
                    {objeto.CostoUnitario.ToString(CultureInfo.InvariantCulture)},
                    {objeto.Total.ToString(CultureInfo.InvariantCulture)},
                    '{objeto.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}'
                );
                """;
        }

        protected override string GenerarComandoEditar(OrdenMateriaPrima objeto) {
            return $"""
                UPDATE adv__orden_material
                SET
                    id_orden_produccion = {objeto.IdOrdenProduccion},
                    id_almacen = {objeto.IdAlmacen},
                    id_producto = {objeto.IdProducto},
                    cantidad = {objeto.Cantidad.ToString(CultureInfo.InvariantCulture)},
                    costo_unitario = {objeto.CostoUnitario.ToString(CultureInfo.InvariantCulture)},
                    total = {objeto.Total.ToString(CultureInfo.InvariantCulture)},
                    fecha_registro = '{objeto.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}'
                WHERE id_orden_material = {objeto.Id};
                """;
        }

        protected override string GenerarComandoEliminar(long id) {
            return $"""
                DELETE FROM adv__orden_material 
                WHERE id_orden_material = {id};
                """;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaOrdenMateriaPrima criterio, string dato) {
            var datoSplit = dato.Split(';');

            return criterio switch {
                FiltroBusquedaOrdenMateriaPrima.Todos =>
                    "SELECT * FROM adv__orden_material;",
                FiltroBusquedaOrdenMateriaPrima.Id =>
                    $"SELECT * FROM adv__orden_material WHERE id_orden_material = {dato};",
                FiltroBusquedaOrdenMateriaPrima.OrdenProduccion =>
                    $"SELECT * FROM adv__orden_material WHERE id_orden_produccion = {dato};",
                FiltroBusquedaOrdenMateriaPrima.Producto =>
                    datoSplit.Length > 1
                        ? $"SELECT * FROM adv__orden_material WHERE id_orden_produccion = {datoSplit[0]} AND id_producto = {datoSplit[1]}"
                        : $"SELECT * FROM adv__orden_material WHERE id_producto = {dato};",
                FiltroBusquedaOrdenMateriaPrima.FechaRegistro =>
                    $"SELECT * FROM adv__orden_material WHERE DATE(fecha_registro) = '{dato}';",
                _ => throw new ArgumentOutOfRangeException(nameof(criterio), criterio, null)
            };
        }

        protected override OrdenMateriaPrima MapearEntidad(MySqlDataReader lectorDatos) {
            return new OrdenMateriaPrima {
                Id = lectorDatos.GetInt64("id_orden_material"),
                IdOrdenProduccion = lectorDatos.GetInt64("id_orden_produccion"),
                IdAlmacen = lectorDatos.GetInt64("id_almacen"),
                IdProducto = lectorDatos.GetInt64("id_producto"),
                Cantidad = lectorDatos.GetDecimal("cantidad"),
                CostoUnitario = lectorDatos.GetDecimal("costo_unitario"),
                Total = lectorDatos.GetDecimal("total"),
                FechaRegistro = lectorDatos.GetDateTime("fecha_registro")
            };
        }
    }
}