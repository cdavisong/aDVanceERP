using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Caja {

    public class RepoCajaMovimiento : RepoEntidadBaseDatos<CajaMovimiento, FiltroBusquedaCajaMovimiento> {

        public RepoCajaMovimiento() : base("adv__caja_movimiento", "id_movimiento_caja") { }

        // ── CRUD base ─────────────────────────────────────────────

        protected override string GenerarComandoAdicionar(
                CajaMovimiento entidad,
                out Dictionary<string, object> parametros,
                params IEntidadBaseDatos[] entidadesExtra) {

            var comando = """
                INSERT INTO adv__caja_movimiento (
                    id_turno,
                    tipo,
                    canal_pago,
                    id_venta,
                    monto,
                    descripcion,
                    id_cuenta_usuario,
                    fecha_movimiento
                ) VALUES (
                    @id_turno,
                    @tipo,
                    @canal_pago,
                    @id_venta,
                    @monto,
                    @descripcion,
                    @id_cuenta_usuario,
                    @fecha_movimiento
                );
                """;

            parametros = new Dictionary<string, object> {
                { "@id_turno",          entidad.IdTurno },
                { "@tipo",              entidad.Tipo.ToString() },
                { "@canal_pago",        entidad.CanalPago.ToString() },
                { "@id_venta",          entidad.IdVenta.HasValue ? entidad.IdVenta.Value : (object)DBNull.Value },
                { "@monto",             entidad.Monto },
                { "@descripcion",       entidad.Descripcion ?? (object)DBNull.Value },
                { "@id_cuenta_usuario", entidad.IdCuentaUsuario },
                { "@fecha_movimiento",  entidad.FechaMovimiento.ToString("yyyy-MM-dd HH:mm:ss") }
            };

            return comando;
        }

        protected override string GenerarComandoEditar(
                CajaMovimiento entidad,
                out Dictionary<string, object> parametros,
                params IEntidadBaseDatos[] entidadesExtra) {

            // Los movimientos de caja son inmutables por diseño.
            // Solo se permite corregir la descripción en casos excepcionales.
            var comando = """
                UPDATE adv__caja_movimiento
                SET descripcion = @descripcion
                WHERE id_movimiento_caja = @id_movimiento_caja;
                """;

            parametros = new Dictionary<string, object> {
                { "@id_movimiento_caja", entidad.Id },
                { "@descripcion",        entidad.Descripcion ?? (object)DBNull.Value }
            };

            return comando;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            // Los movimientos no se eliminan físicamente.
            // Este método existe por contrato de la clase base.
            throw new InvalidOperationException(
                "Los movimientos de caja son inmutables. " +
                "Para revertir un movimiento incorrecto, registre uno de tipo AjusteArqueo.");
        }

        protected override string GenerarComandoObtener(FiltroBusquedaCajaMovimiento filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var fechaDesde = criteriosBusqueda.Length == 3
                ? criteriosBusqueda[0]
                : DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var fechaHasta = criteriosBusqueda.Length == 3
                ? criteriosBusqueda[1]
                : DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var criterio = criteriosBusqueda.Length >= 1 ? criteriosBusqueda[^1] : string.Empty;

            var consultaBase = $"""
                SELECT
                    m.*,
                    v.numero_factura_ticket AS numero_factura,
                    cu.nombre               AS nombre_usuario
                FROM adv__caja_movimiento m
                LEFT JOIN adv__venta         v  ON v.id_venta          = m.id_venta
                LEFT JOIN adv__cuenta_usuario cu ON cu.id_cuenta_usuario = m.id_cuenta_usuario
                {(criteriosBusqueda.Length == 3
                    ? "WHERE m.fecha_movimiento >= @fecha_desde AND m.fecha_movimiento <= @fecha_hasta"
                    : "WHERE 1=1")}
                """;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaCajaMovimiento.IdTurno => $"""
                    {consultaBase}
                    AND m.id_turno = @id_turno
                    ORDER BY m.fecha_movimiento ASC
                    """,
                FiltroBusquedaCajaMovimiento.Tipo => $"""
                    {consultaBase}
                    AND m.tipo = @tipo
                    ORDER BY m.fecha_movimiento DESC
                    """,
                FiltroBusquedaCajaMovimiento.Canal => $"""
                    {consultaBase}
                    AND m.canal_pago = @canal_pago
                    ORDER BY m.fecha_movimiento DESC
                    """,
                _ => $"{consultaBase} ORDER BY m.fecha_movimiento DESC"
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaCajaMovimiento.IdTurno => new Dictionary<string, object> {
                    { "@id_turno",    Convert.ToInt64(string.IsNullOrEmpty(criterio) ? "0" : criterio) },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 23:59:59") }
                },
                FiltroBusquedaCajaMovimiento.Tipo => new Dictionary<string, object> {
                    { "@tipo",        criterio },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 23:59:59") }
                },
                FiltroBusquedaCajaMovimiento.Canal => new Dictionary<string, object> {
                    { "@canal_pago",  criterio },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 23:59:59") }
                },
                _ => new Dictionary<string, object> {
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 23:59:59") }
                }
            };

            return consulta;
        }

        protected override (CajaMovimiento, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var movimiento = new CajaMovimiento {
                Id = Convert.ToInt64(lector["id_movimiento_caja"]),
                IdTurno = Convert.ToInt64(lector["id_turno"]),
                Tipo = Enum.Parse<TipoMovimientoCajaEnum>(Convert.ToString(lector["tipo"]) ?? "EntradaManual"),
                CanalPago = Enum.Parse<CanalPagoCajaEnum>(Convert.ToString(lector["canal_pago"]) ?? "NA"),
                IdVenta = lector["id_venta"] != DBNull.Value ? Convert.ToInt64(lector["id_venta"]) : null,
                Monto = Convert.ToDecimal(lector["monto"], CultureInfo.InvariantCulture),
                Descripcion = lector["descripcion"] != DBNull.Value ? Convert.ToString(lector["descripcion"]) : null,
                IdCuentaUsuario = Convert.ToInt64(lector["id_cuenta_usuario"]),
                FechaMovimiento = Convert.ToDateTime(lector["fecha_movimiento"])
            };

            // Datos auxiliares de JOIN (presentes en consultas de historial)
            if (lector.VisibleFieldCount > 8) {
                movimiento.NumeroFactura = lector["numero_factura"] != DBNull.Value ? Convert.ToString(lector["numero_factura"]) : null;
                movimiento.NombreUsuario = lector["nombre_usuario"] != DBNull.Value ? Convert.ToString(lector["nombre_usuario"]) : null;
            }

            return (movimiento, new List<IEntidadBaseDatos>());
        }

        // ── Singleton ─────────────────────────────────────────────

        public static RepoCajaMovimiento Instancia { get; } = new RepoCajaMovimiento();

        // ── Operaciones específicas del dominio de caja ───────────

        /// <summary>
        /// Calcula los totales de entradas netas por canal para el cierre del turno.
        /// Incluye Venta, DevolucionVenta, EntradaManual y SalidaManual.
        /// Excluye AjusteArqueo (se registra después del cálculo).
        /// Esta es la FUENTE DE VERDAD: el presentador escribe estos valores en adv__caja_turno.
        /// </summary>
        public TotalesCierreCaja ObtenerTotalesPorCanal(long idTurno) {
            var consulta = """
                SELECT
                    canal_pago,
                    SUM(monto) AS total
                FROM adv__caja_movimiento
                WHERE id_turno = @id_turno
                  AND tipo IN ('Venta','DevolucionVenta','EntradaManual','SalidaManual')
                GROUP BY canal_pago;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_turno", idTurno }
            };

            var totales = new TotalesCierreCaja { IdTurno = idTurno };

            var resultados = ContextoBaseDatos.EjecutarConsulta(consulta, parametros, (lector) => {
                var canal = Enum.Parse<CanalPagoCajaEnum>(Convert.ToString(lector["canal_pago"]) ?? "NA");
                var total = Convert.ToDecimal(lector["total"], CultureInfo.InvariantCulture);
                return (new { canal, total }, new List<IEntidadBaseDatos>());
            });

            foreach (var (fila, _) in resultados) {
                switch (fila.canal) {
                    case CanalPagoCajaEnum.Efectivo:
                        totales.TotalEfectivo = fila.total;
                        break;
                    case CanalPagoCajaEnum.Transferencia:
                        totales.TotalTransferencias = fila.total;
                        break;
                    case CanalPagoCajaEnum.Mixto:
                        // Un pago Mixto se contabiliza en ambos canales en el momento
                        // del registro (el presentador lo desglosa en 2 movimientos).
                        // Si llegó aquí como Mixto es un caso no desglosado → se suma a Efectivo
                        // por criterio conservador.
                        totales.TotalEfectivo += fila.total;
                        break;
                }
            }

            // Sumar el monto de apertura al total de efectivo esperado
            var consultaApertura = """
                SELECT monto_apertura
                FROM adv__caja_turno
                WHERE id_turno = @id_turno
                LIMIT 1;
                """;

            totales.TotalEfectivo += ContextoBaseDatos.EjecutarConsultaEscalar<decimal>(
                consultaApertura, parametros);

            return totales;
        }

        /// <summary>
        /// Devuelve todos los movimientos de un turno ordenados cronológicamente.
        /// Usado para renderizar el historial en la vista de detalle del turno.
        /// </summary>
        public List<CajaMovimiento> ObtenerMovimientosTurno(long idTurno) {
            var consulta = """
                SELECT
                    m.*,
                    v.numero_factura_ticket AS numero_factura,
                    cu.nombre               AS nombre_usuario
                FROM adv__caja_movimiento m
                LEFT JOIN adv__venta          v  ON v.id_venta           = m.id_venta
                LEFT JOIN adv__cuenta_usuario cu ON cu.id_cuenta_usuario  = m.id_cuenta_usuario
                WHERE m.id_turno = @id_turno
                ORDER BY m.fecha_movimiento ASC;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_turno", idTurno }
            };

            return ContextoBaseDatos.EjecutarConsulta(consulta, parametros, MapearEntidad)
                .Select(r => r.entidadBase)
                .ToList();
        }

        /// <summary>
        /// Verifica si un turno tiene al menos un movimiento registrado.
        /// Usado por el presentador para decidir si permite anular el turno.
        /// </summary>
        public bool TurnoTieneMovimientos(long idTurno) {
            var consulta = """
                SELECT COUNT(*)
                FROM adv__caja_movimiento
                WHERE id_turno = @id_turno
                LIMIT 1;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_turno", idTurno }
            };

            return ContextoBaseDatos.EjecutarConsultaEscalar<int>(consulta, parametros) > 0;
        }
    }
}