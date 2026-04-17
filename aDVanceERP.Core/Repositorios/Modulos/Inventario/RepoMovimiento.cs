using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario {
    public class RepoMovimiento : RepoEntidadBaseDatos<Movimiento, FiltroBusquedaMovimiento> {
        public RepoMovimiento() : base("adv__movimiento", "id_movimiento") { }

        protected override string GenerarComandoAdicionar(Movimiento objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                    INSERT INTO adv__movimiento (
                    id_producto,
                    costo_unitario,
                    id_almacen_origen,
                    id_almacen_destino,
                    fecha_creacion,
                    estado,
                    fecha_termino,
                    saldo_inicial,
                    cantidad_movida,
                    saldo_final,
                    id_tipo_movimiento,
                    id_cuenta_usuario,
                    notas
                )
                VALUES (
                    @id_producto,
                    @costo_unitario,
                    @id_almacen_origen,
                    @id_almacen_destino,
                    @fecha_creacion,
                    @estado,
                    @fecha_termino,
                    @saldo_inicial,
                    @cantidad_movida,
                    @saldo_final,
                    @id_tipo_movimiento,
                    @id_cuenta_usuario,
                    @notas
                );
                """;

            parametros = new Dictionary<string, object> {
                { "@id_producto", objeto.IdProducto },
                { "@costo_unitario", objeto.CostoUnitario },
                { "@id_almacen_origen", objeto.IdAlmacenOrigen },
                { "@id_almacen_destino", objeto.IdAlmacenDestino },
                { "@fecha_creacion", objeto.FechaCreacion.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@estado", objeto.Estado.ToString() },
                { "@fecha_termino", objeto.FechaTermino.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@saldo_inicial", objeto.SaldoInicial },
                { "@cantidad_movida", objeto.CantidadMovida },
                { "@saldo_final", objeto.SaldoFinal },
                { "@id_tipo_movimiento", objeto.IdTipoMovimiento },
                { "@id_cuenta_usuario", objeto.IdCuentaUsuario },
                { "@notas", objeto.Notas ?? string.Empty }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(Movimiento objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                    UPDATE adv__movimiento
                SET
                    id_producto = @id_producto,
                    costo_unitario = @costo_unitario,
                    id_almacen_origen = @id_almacen_origen,
                    id_almacen_destino = @id_almacen_destino,
                    fecha_creacion = @fecha_creacion,
                    estado = @estado,
                    fecha_termino = @fecha_termino,
                    saldo_inicial = @saldo_inicial,
                    cantidad_movida = @cantidad_movida,
                    saldo_final = @saldo_final,
                    id_tipo_movimiento = @id_tipo_movimiento,
                    id_cuenta_usuario = @id_cuenta_usuario,
                    notas = @notas
                WHERE id_movimiento = @id_movimiento;
                """;

            parametros = new Dictionary<string, object> {
                { "@id_movimiento", objeto.Id },
                { "@id_producto", objeto.IdProducto },
                { "@costo_unitario", objeto.CostoUnitario },
                { "@id_almacen_origen", objeto.IdAlmacenOrigen },
                { "@id_almacen_destino", objeto.IdAlmacenDestino },
                { "@fecha_creacion", objeto.FechaCreacion.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@estado", objeto.Estado.ToString() },
                { "@fecha_termino", objeto.FechaTermino.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@saldo_inicial", objeto.SaldoInicial },
                { "@cantidad_movida", objeto.CantidadMovida },
                { "@saldo_final", objeto.SaldoFinal },
                { "@id_tipo_movimiento", objeto.IdTipoMovimiento },
                { "@id_cuenta_usuario", objeto.IdCuentaUsuario },
                { "@notas", objeto.Notas ?? string.Empty }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var consulta = $"""
                DELETE FROM adv__movimiento 
                WHERE id_movimiento = @id_movimiento;
                """;

            parametros = new Dictionary<string, object> {
                { "@id_movimiento", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaMovimiento filtroBusqueda, out Dictionary<string, object> parametros, string[] criteriosBusqueda) {
            var fechaDesde = criteriosBusqueda.Length == 3 ? criteriosBusqueda[0] : DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var fechaHasta = criteriosBusqueda.Length == 3 ? criteriosBusqueda[1] : DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var criterio = criteriosBusqueda.Length == 3 ? criteriosBusqueda[2] : criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;

            var consultaComun = $"""
                SELECT m.*, p.nombre AS nombre_producto, ao.nombre AS nombre_almacen_origen, ad.nombre AS nombre_almacen_destino, tm.nombre AS nombre_tipo_movimiento, tm.efecto
                FROM adv__movimiento m
                JOIN adv__producto p ON m.id_producto = p.id_producto
                LEFT JOIN adv__almacen ao ON m.id_almacen_origen = ao.id_almacen
                LEFT JOIN adv__almacen ad ON m.id_almacen_destino = ad.id_almacen
                JOIN adv__tipo_movimiento tm ON m.id_tipo_movimiento = tm.id_tipo_movimiento
                {(criteriosBusqueda.Length == 3 ? "WHERE m.fecha_creacion >= @fecha_desde AND m.fecha_creacion <= @fecha_hasta" : string.Empty)} 
                """;
            var consulta = filtroBusqueda switch {
                FiltroBusquedaMovimiento.Id => $"""
                    {consultaComun}
                    {(criteriosBusqueda.Length == 3 ? "AND" : "WHERE")} id_movimiento = @id_movimiento
                    """,
                FiltroBusquedaMovimiento.IdProducto => $"""
                    {consultaComun}
                    {(criteriosBusqueda.Length == 3 ? "AND" : "WHERE")} m.id_producto = @id_producto
                    """,
                FiltroBusquedaMovimiento.AlmacenOrigen => $"""
                    {consultaComun}
                    {(criteriosBusqueda.Length == 3 ? "AND" : "WHERE")} LOWER(ao.nombre) LIKE LOWER(@nombre_almacen_origen)
                    """,
                FiltroBusquedaMovimiento.AlmacenDestino => $"""
                    {consultaComun}
                    {(criteriosBusqueda.Length == 3 ? "AND" : "WHERE")} LOWER(ad.nombre) LIKE LOWER(@nombre_almacen_destino)
                    """,
                FiltroBusquedaMovimiento.Tipo => $"""
                    {consultaComun}
                    {(criteriosBusqueda.Length == 3 ? "AND" : "WHERE")} LOWER(tm.nombre) LIKE LOWER(@nombre_tipo_movimiento)
                    """,
                _ => $"""
                    {consultaComun}
                    """,
            };

            consulta += "\nORDER BY m.fecha_creacion DESC;";

            parametros = filtroBusqueda switch {
                FiltroBusquedaMovimiento.Id => new Dictionary<string, object> {
                    { "@id_movimiento", Convert.ToInt64(string.IsNullOrEmpty(criterio) ? "0" : criterio) },
                },
                FiltroBusquedaMovimiento.IdProducto => new Dictionary<string, object> {
                    { "@id_producto", criterio }
                },
                FiltroBusquedaMovimiento.AlmacenOrigen => new Dictionary<string, object> {
                    { "@nombre_almacen_origen", $"%{criterio}%" }
                },
                FiltroBusquedaMovimiento.AlmacenDestino => new Dictionary<string, object> {
                    { "@nombre_almacen_destino", $"%{criterio}%" }
                },
                FiltroBusquedaMovimiento.Tipo => new Dictionary<string, object> {
                    { "@nombre_tipo_movimiento", $"%{criterio}%" }
                },
                _ => new Dictionary<string, object>(),
            };

            if (criteriosBusqueda.Length == 3) {
                parametros.Add("@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00"));
                parametros.Add("@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 23:59:59"));
            }

            return consulta;
        }

        protected override (Movimiento, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lectorDatos) {
            var costoUnitario = Convert.ToDecimal(lectorDatos["costo_unitario"], CultureInfo.InvariantCulture);
            var cantidadMovida = Convert.ToDecimal(lectorDatos["cantidad_movida"], CultureInfo.InvariantCulture);
            
            return (new Movimiento(
                id: Convert.ToInt64(lectorDatos["id_movimiento"]),
                idProducto: Convert.ToInt64(lectorDatos["id_producto"]),
                costoUnitario: costoUnitario,
                idAlmacenOrigen: Convert.ToInt64(lectorDatos.IsDBNull(lectorDatos.GetOrdinal("id_almacen_origen")) ? "0" : lectorDatos["id_almacen_origen"]),
                idAlmacenDestino: Convert.ToInt64(lectorDatos.IsDBNull(lectorDatos.GetOrdinal("id_almacen_destino")) ? "0" : lectorDatos["id_almacen_destino"]),
                fechaCreacion: Convert.ToDateTime(lectorDatos["fecha_creacion"]),
                estado: Enum.TryParse<EstadoMovimientoEnum>(lectorDatos["estado"].ToString(), out var estado) ? estado : EstadoMovimientoEnum.Cancelado,
                fecha: lectorDatos.IsDBNull(lectorDatos.GetOrdinal("fecha_termino")) ? DateTime.MinValue : Convert.ToDateTime(lectorDatos["fecha_termino"]),
                saldoInicial: Convert.ToDecimal(lectorDatos["saldo_inicial"], CultureInfo.InvariantCulture),
                cantidadMovida: cantidadMovida,
                saldoFinal: Convert.ToDecimal(lectorDatos["saldo_final"], CultureInfo.InvariantCulture),
                idTipoMovimiento: Convert.ToInt64(lectorDatos["id_tipo_movimiento"]),
                idCuentaUsuario: Convert.ToInt64(lectorDatos["id_cuenta_usuario"]),
                notas: lectorDatos["notas"] != DBNull.Value ? Convert.ToString(lectorDatos["notas"]) : string.Empty) {
                NombreProducto = lectorDatos["nombre_producto"]?.ToString() ?? string.Empty,
                NombreAlmacenOrigen = lectorDatos["nombre_almacen_origen"]?.ToString() ?? string.Empty,
                NombreAlmacenDestino = lectorDatos["nombre_almacen_destino"]?.ToString() ?? string.Empty,
                NombreTipoMovimiento = lectorDatos["nombre_tipo_movimiento"]?.ToString() ?? string.Empty,
                EfectoMovimiento = Enum.TryParse<EfectoMovimientoEnum>(lectorDatos["efecto"].ToString(), out var efecto) ? efecto : EfectoMovimientoEnum.Ninguno
            }, new List<IEntidadBaseDatos>());
        }

        #region STATIC

        public static RepoMovimiento Instancia { get; } = new RepoMovimiento();

        #endregion
    }
}