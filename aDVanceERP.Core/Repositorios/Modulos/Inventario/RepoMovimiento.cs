using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario;

public class RepoMovimiento : RepoEntidadBaseDatos<Movimiento, FiltroBusquedaMovimiento> {
    public RepoMovimiento() : base("adv__movimiento", "id_movimiento") { }

    protected override string GenerarComandoAdicionar(Movimiento objeto) {
        return $"""
                INSERT INTO adv__movimiento (
                    id_producto,
                    costo_unitario,
                    id_almacen_origen,
                    id_almacen_destino,
                    fecha_creacion,
                    estado,
                    fecha,
                    saldo_inicial,
                    cantidad_movida,
                    saldo_final,
                    id_tipo_movimiento,
                    id_cuenta_usuario
                )
                VALUES (
                    {objeto.IdProducto},
                    {objeto.CostoUnitario.ToString(CultureInfo.InvariantCulture)},
                    {objeto.IdAlmacenOrigen},
                    {objeto.IdAlmacenDestino},
                    '{objeto.FechaCreacion:yyyy-MM-dd HH:mm:ss}',
                    '{objeto.Estado}',
                    '{DateTime.MinValue:yyyy-MM-dd HH:mm:ss}',
                    {objeto.SaldoInicial.ToString(CultureInfo.InvariantCulture)},
                    {objeto.CantidadMovida.ToString(CultureInfo.InvariantCulture)},
                    {objeto.SaldoFinal.ToString(CultureInfo.InvariantCulture)},
                    {objeto.IdTipoMovimiento},
                    {objeto.IdCuentaUsuario}
                );
                """;
    }

    protected override string GenerarComandoEditar(Movimiento objeto) {
        return $"""
                UPDATE adv__movimiento
                SET
                    id_producto = {objeto.IdProducto},
                    costo_unitario = {objeto.CostoUnitario.ToString(CultureInfo.InvariantCulture)},
                    id_almacen_origen = {objeto.IdAlmacenOrigen},
                    id_almacen_destino = {objeto.IdAlmacenDestino},
                    fecha_creacion = '{objeto.FechaCreacion:yyyy-MM-dd HH:mm:ss}',
                    estado = '{objeto.Estado}',
                    fecha = '{objeto.Fecha:yyyy-MM-dd HH:mm:ss}',
                    saldo_inicial = {objeto.SaldoInicial.ToString(CultureInfo.InvariantCulture)},
                    cantidad_movida = {objeto.CantidadMovida.ToString(CultureInfo.InvariantCulture)},
                    saldo_final = {objeto.SaldoFinal.ToString(CultureInfo.InvariantCulture)},
                    id_tipo_movimiento = {objeto.IdTipoMovimiento},
                    id_cuenta_usuario = {objeto.IdCuentaUsuario}
                WHERE id_movimiento = {objeto.Id};
                """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__movimiento 
            WHERE id_movimiento = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaMovimiento criterio, string dato) {
        string? comando;

        switch (criterio) {
            case FiltroBusquedaMovimiento.Id:
                comando = $"""
                    SELECT * 
                    FROM adv__movimiento
                    WHERE id_movimiento = {dato};
                    """;
                break;
            case FiltroBusquedaMovimiento.Producto:
                comando = $"""
                    SELECT m.* 
                    FROM adv__movimiento m 
                    JOIN adv__producto p ON m.id_producto = p.id_producto 
                    WHERE LOWER(p.nombre) LIKE LOWER('%{dato}%');
                    """;
                break;
            case FiltroBusquedaMovimiento.AlmacenOrigen:
                comando = $"""
                    SELECT * 
                    FROM adv__movimiento m 
                    JOIN adv__almacen a ON m.id_almacen_origen = a.id_almacen 
                    WHERE LOWER(a.nombre) LIKE LOWER('%{dato}%');
                    """;
                break;
            case FiltroBusquedaMovimiento.AlmacenDestino:
                comando = $"""
                    SELECT * 
                    FROM adv__movimiento m 
                    JOIN adv__almacen a ON m.id_almacen_destino = a.id_almacen 
                    WHERE LOWER(a.nombre) LIKE LOWER('%{dato}%');
                    """;
                break;
            case FiltroBusquedaMovimiento.Fecha:
                comando = $"""
                    SELECT * 
                    FROM adv__movimiento
                    WHERE DATE(fecha) = '{dato}';
                    """;
                break;
            case FiltroBusquedaMovimiento.TipoMovimiento:
                comando =
                    $"""
                    SELECT *
                    FROM adv__movimiento m
                    JOIN adv__tipo_movimiento tm ON m.id_tipo_movimiento = tm.id_tipo_movimiento
                    WHERE LOWER(tm.nombre) LIKE LOWER('%{dato}%');
                    """;
                break;
            default:
                comando = """
                    SELECT * 
                    FROM adv__movimiento;
                    """;
                break;
        }

        return comando;
    }

    protected override Movimiento MapearEntidad(MySqlDataReader lectorDatos) {
        return new Movimiento(
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_movimiento")),
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_producto")),
            lectorDatos.GetDecimal(lectorDatos.GetOrdinal("costo_unitario")),
            lectorDatos.GetDecimal(lectorDatos.GetOrdinal("costo_total")),
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_almacen_origen")),
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_almacen_destino")),
            lectorDatos.GetDateTime(lectorDatos.GetOrdinal("fecha_creacion")),
            (EstadoMovimiento)Enum.Parse(typeof(EstadoMovimiento), lectorDatos.GetValue(lectorDatos.GetOrdinal("estado")).ToString()),
            lectorDatos.GetDateTime(lectorDatos.GetOrdinal("fecha")),
            lectorDatos.GetDecimal(lectorDatos.GetOrdinal("saldo_inicial")),
            lectorDatos.GetDecimal(lectorDatos.GetOrdinal("cantidad_movida")),
            lectorDatos.GetDecimal(lectorDatos.GetOrdinal("saldo_final")),
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_tipo_movimiento")),
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_cuenta_usuario"))
        );
    }

    #region STATIC

    public static RepoMovimiento Instancia = new RepoMovimiento();

    #endregion
}