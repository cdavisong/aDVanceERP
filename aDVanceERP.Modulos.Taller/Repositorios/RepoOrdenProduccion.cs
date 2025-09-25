using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Modulos.Taller.Modelos;

using MySql.Data.MySqlClient;
using System.Globalization;

namespace aDVanceERP.Modulos.Taller.Repositorios
{
    public class RepoOrdenProduccion : RepoEntidadBaseDatos<OrdenProduccion, FiltroBusquedaOrdenProduccion> {
        public RepoOrdenProduccion() : base("adv__orden_produccion", "id_orden_produccion") { }

        protected override string GenerarComandoAdicionar(OrdenProduccion objeto) {
            return $"""
                INSERT INTO adv__orden_produccion (
                    numero_orden,
                    fecha_apertura,
                    id_almacen,
                    nombre_producto,
                    cantidad,
                    estado,
                    observaciones,
                    costo_total,
                    precio_unitario,
                    margen_ganancia
                )
                VALUES (
                    '{objeto.NumeroOrden}',
                    '{objeto.FechaApertura.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}',
                    {objeto.IdAlmacen},
                    '{objeto.NombreProducto}',
                    {objeto.Cantidad.ToString(CultureInfo.InvariantCulture)},
                    '{objeto.Estado}',
                    '{objeto.Observaciones?.Replace("'", "''") ?? string.Empty}',
                    {objeto.CostoTotal.ToString(CultureInfo.InvariantCulture)},
                    {objeto.PrecioUnitario.ToString(CultureInfo.InvariantCulture)},
                    {objeto.MargenGanancia.ToString(CultureInfo.InvariantCulture)}
                );
                """;
        }

        protected override string GenerarComandoEditar(OrdenProduccion objeto) {
            return $"""
                UPDATE adv__orden_produccion
                SET
                    numero_orden = '{objeto.NumeroOrden}',
                    fecha_apertura = '{objeto.FechaApertura.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}',
                    id_almacen = {objeto.IdAlmacen},
                    nombre_producto = '{objeto.NombreProducto}',
                    cantidad = {objeto.Cantidad.ToString(CultureInfo.InvariantCulture)},
                    estado = '{objeto.Estado}',
                    observaciones = '{objeto.Observaciones?.Replace("'", "''") ?? string.Empty}',
                    fecha_cierre = {(objeto.FechaCierre.HasValue ? $"'{objeto.FechaCierre.Value:yyyy-MM-dd HH:mm:ss}'" : "NULL")},
                    costo_total = {objeto.CostoTotal.ToString(CultureInfo.InvariantCulture)},
                    precio_unitario = {objeto.PrecioUnitario.ToString(CultureInfo.InvariantCulture)},
                    margen_ganancia = {objeto.MargenGanancia.ToString(CultureInfo.InvariantCulture)}
                WHERE id_orden_produccion = {objeto.Id};
                """;
        }

        protected override string GenerarComandoEliminar(long id) {
            return $"""
                -- Primero eliminamos los registros relacionados
                DELETE FROM adv__orden_actividad WHERE id_orden_produccion = {id};
                DELETE FROM adv__orden_material WHERE id_orden_produccion = {id};
                DELETE FROM adv__orden_gasto_indirecto WHERE id_orden_produccion = {id};
                
                -- Finalmente eliminamos la orden
                DELETE FROM adv__orden_produccion WHERE id_orden_produccion = {id};
                """;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaOrdenProduccion criterio, string dato) {
            return criterio switch {
                FiltroBusquedaOrdenProduccion.Todas =>
                    "SELECT * FROM adv__orden_produccion;",
                FiltroBusquedaOrdenProduccion.NumeroOrden =>
                    $"SELECT * FROM adv__orden_produccion WHERE numero_orden LIKE '%{dato}%';",
                FiltroBusquedaOrdenProduccion.Producto =>
                    $"SELECT * FROM adv__orden_produccion WHERE nombre_producto = {dato};",
                FiltroBusquedaOrdenProduccion.Estado =>
                    $"SELECT * FROM adv__orden_produccion WHERE estado = '{dato}';",
                FiltroBusquedaOrdenProduccion.FechaApertura =>
                    $"SELECT * FROM adv__orden_produccion WHERE DATE(fecha_apertura) = '{dato}';",
                FiltroBusquedaOrdenProduccion.FechaCierre =>
                    $"SELECT * FROM adv__orden_produccion WHERE DATE(fecha_cierre) = '{dato}';",
                _ => throw new ArgumentOutOfRangeException(nameof(criterio), criterio, null)
            };
        }

        protected override OrdenProduccion MapearEntidad(MySqlDataReader lectorDatos) {
            return new OrdenProduccion {
                Id = lectorDatos.GetInt64("id_orden_produccion"),
                NumeroOrden = lectorDatos.GetString("numero_orden"),
                FechaApertura = lectorDatos.GetDateTime("fecha_apertura"),
                FechaCierre = lectorDatos.IsDBNull(lectorDatos.GetOrdinal("fecha_cierre")) ?
                    null : lectorDatos.GetDateTime("fecha_cierre"),
                IdAlmacen = lectorDatos.GetInt64("id_almacen"),
                NombreProducto = lectorDatos.GetString("nombre_producto") ?? string.Empty,
                Cantidad = lectorDatos.GetDecimal("cantidad"),
                Estado = (EstadoOrdenProduccion) Enum.Parse(
                    typeof(EstadoOrdenProduccion),
                    lectorDatos.GetString("estado")),
                Observaciones = lectorDatos.IsDBNull(lectorDatos.GetOrdinal("observaciones")) ?
                    string.Empty : lectorDatos.GetString("observaciones"),
                CostoTotal = lectorDatos.GetDecimal("costo_total"),
                PrecioUnitario = lectorDatos.GetDecimal("precio_unitario"),
                MargenGanancia = lectorDatos.GetDecimal("margen_ganancia")
            };
        }
    }
}