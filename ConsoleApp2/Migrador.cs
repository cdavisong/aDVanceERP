// =============================================================================
// Migrador.cs — Lógica principal de migración contra advanceerp
//
// Orden de operaciones (dentro de una transacción):
//   1. Insertar clasificaciones de producto (una por Category del CSV)
//   2. Insertar almacen "P. VENTA" de tipo Primario (si no existe)
//   3. Insertar productos (INSERT IGNORE en nombre, que tiene UNIQUE KEY)
//   4. Para cada producto con Qty > 0:
//        a. INSERT adv__movimiento  (Carga Inicial, tipo 11)
//        b. INSERT adv__inventario  (ON DUPLICATE KEY UPDATE)
//
// Notas de esquema:
//   - adv__movimiento.costo_total es GENERATED, no se inserta.
//   - adv__almacen no tiene capacidad ni coordenadas.
//   - Codigo de producto = "MIGR-" + ShortName sanitizado (max 90 chars).
//   - id_cuenta_usuario = 1 (admin) en movimientos.
// =============================================================================
using MySql.Data.MySqlClient;

namespace ConsoleApp2;

public class Migrador {
    private readonly string _connStr;
    private const string NombreAlmacen = "P. VENTA";

    // Mapeo de categorias del CSV a nombres legibles en la BD
    private static readonly Dictionary<string, string> CatMap =
        new(StringComparer.OrdinalIgnoreCase) {
            ["BEBIDAS"] = "Bebidas",
            ["MERCADO"] = "Mercado",
            ["CONFITURAS"] = "Confituras",
            ["ASEO"] = "Aseo",
            ["CARNICOS"] = "Carnicos",
            ["CIGARROS"] = "Cigarros",
            ["PAN"] = "Pan",
        };

    public Migrador(string connStr) => _connStr = connStr;

    public ResultadoMigracion Ejecutar(List<FilaProducto> filas) {
        var resultado = new ResultadoMigracion();

        using var conn = new MySqlConnection(_connStr);
        conn.Open();

        using var tx = conn.BeginTransaction();
        try {
            // ── PASO 1: Clasificaciones ───────────────────────────────────────
            var categoriasUnicas = filas
                .Select(f => f.Category.Trim())
                .Where(c => c.Length > 0)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            foreach (var cat in categoriasUnicas) {
                string nombre = CatMap.TryGetValue(cat, out var mapped) ? mapped : cat;
                using var cmd = conn.CreateCommand();
                cmd.Transaction = tx;
                cmd.CommandText = @"
                    INSERT IGNORE INTO adv__clasificacion_producto (nombre, descripcion)
                    VALUES (@nombre, @desc)";
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@desc",
                    $"Migrada desde CSV ({cat})");
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0) resultado.ClasificacionesInsertadas++;
            }

            // ── PASO 2: Almacen ───────────────────────────────────────────────
            int idAlmacen;
            {
                using var cmdChk = conn.CreateCommand();
                cmdChk.Transaction = tx;
                cmdChk.CommandText =
                    "SELECT id_almacen FROM adv__almacen WHERE nombre = @nombre LIMIT 1";
                cmdChk.Parameters.AddWithValue("@nombre", NombreAlmacen);
                var existing = cmdChk.ExecuteScalar();

                if (existing != null && existing != DBNull.Value) {
                    idAlmacen = Convert.ToInt32(existing);
                } else {
                    using var cmdIns = conn.CreateCommand();
                    cmdIns.Transaction = tx;
                    cmdIns.CommandText = @"
                        INSERT INTO adv__almacen (nombre, descripcion, direccion, tipo, estado)
                        VALUES (@nombre, @desc, @dir, 'Primario', 1)";
                    cmdIns.Parameters.AddWithValue("@nombre", NombreAlmacen);
                    cmdIns.Parameters.AddWithValue("@desc",
                        "Almacen principal creado en migracion inicial");
                    cmdIns.Parameters.AddWithValue("@dir", "Sin direccion registrada");
                    cmdIns.ExecuteNonQuery();
                    idAlmacen = (int) cmdIns.LastInsertedId;
                    resultado.AlmacenCreado = 1;
                }
            }

            // ── Precargar ids de clasificacion (evita N subqueries) ───────────
            var idClasif = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            {
                using var cmdCl = conn.CreateCommand();
                cmdCl.Transaction = tx;
                cmdCl.CommandText =
                    "SELECT id_clasificacion_producto, nombre FROM adv__clasificacion_producto";
                using var rd = cmdCl.ExecuteReader();
                while (rd.Read())
                    idClasif[rd.GetString(1)] = rd.GetInt32(0);
            }

            // ── PASO 3: Productos ─────────────────────────────────────────────
            foreach (var fila in filas) {
                string nombreProd = fila.LongName.Trim();
                if (string.IsNullOrEmpty(nombreProd)) continue;

                string catNombre = CatMap.TryGetValue(fila.Category.Trim(), out var cm)
                    ? cm : fila.Category.Trim();
                int idClasifProd = idClasif.TryGetValue(catNombre, out var cid) ? cid : 1;
                decimal impuesto = fila.Taxable == 1 ? 10.00m : 0.00m;
                string codigo = CodigoHelper.GenerarEan13($"Mercancia{nombreProd}");

                using var cmd = conn.CreateCommand();
                cmd.Transaction = tx;
                cmd.CommandText = @"
                    INSERT IGNORE INTO adv__producto
                        (categoria, nombre, codigo, id_proveedor, descripcion,
                         id_unidad_medida, id_clasificacion_producto, es_vendible,
                         costo_adquisicion_unitario, costo_produccion_unitario,
                         impuesto_venta_porcentaje, margen_ganancia_deseado,
                         precio_venta_base, activo)
                    VALUES
                        ('Mercancia', @nombre, @codigo, 0, @desc,
                         1, @idClasif, 1,
                         @costo, 0.00,
                         @impuesto, 0.00,
                         @precio, 1)";
                cmd.Parameters.AddWithValue("@nombre", nombreProd);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@desc", nombreProd);
                cmd.Parameters.AddWithValue("@idClasif", idClasifProd);
                cmd.Parameters.AddWithValue("@costo", fila.Cost);
                cmd.Parameters.AddWithValue("@impuesto", impuesto);
                cmd.Parameters.AddWithValue("@precio", fila.Price);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0) resultado.ProductosInsertados++;
                else resultado.ProductosOmitidos++;
            }

            // ── Precargar ids de producto recien insertados ────────────────────
            var idProducto = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            {
                using var cmdP = conn.CreateCommand();
                cmdP.Transaction = tx;
                cmdP.CommandText =
                    "SELECT id_producto, nombre FROM adv__producto WHERE LENGTH(codigo) = 13 AND codigo REGEXP '^[0-9]+$'";
                using var rd = cmdP.ExecuteReader();
                while (rd.Read())
                    idProducto[rd.GetString(1)] = rd.GetInt32(0);
            }

            // ── PASO 4: Inventario + Movimientos (solo Qty > 0) ───────────────
            foreach (var fila in filas.Where(f => f.Qty > 0)) {
                string nombreProd = fila.LongName.Trim();
                if (!idProducto.TryGetValue(nombreProd, out int idProd)) {
                    resultado.Advertencias.Add(
                        $"Producto '{nombreProd}' no encontrado en BD tras insercion, omitido en inventario.");
                    continue;
                }

                decimal valorTotal = fila.Qty * fila.Cost;

                // adv__movimiento
                using var cmdMov = conn.CreateCommand();
                cmdMov.Transaction = tx;
                cmdMov.CommandText = @"
                    INSERT INTO adv__movimiento
                        (id_producto, costo_unitario,
                         id_almacen_origen, id_almacen_destino,
                         estado, saldo_inicial, fecha_termino,
                         cantidad_movida, saldo_final,
                         id_tipo_movimiento, id_cuenta_usuario, notas)
                    VALUES
                        (@idProd, @costo,
                         @idAlm, @idAlm,
                         'Completado', 0.00, NOW(),
                         @qty, @qty,
                         11, 1,
                         'Carga inicial - migracion desde sistema anterior')";
                cmdMov.Parameters.AddWithValue("@idProd", idProd);
                cmdMov.Parameters.AddWithValue("@costo", fila.Cost);
                cmdMov.Parameters.AddWithValue("@idAlm", idAlmacen);
                cmdMov.Parameters.AddWithValue("@qty", fila.Qty);
                cmdMov.ExecuteNonQuery();
                resultado.MovimientosInsertados++;

                // adv__inventario
                using var cmdInv = conn.CreateCommand();
                cmdInv.Transaction = tx;
                cmdInv.CommandText = @"
                    INSERT INTO adv__inventario
                        (id_producto, id_almacen, cantidad, cantidad_minima,
                         costo_promedio, valor_total)
                    VALUES
                        (@idProd, @idAlm, @qty, 0.00, @costo, @valor)
                    ON DUPLICATE KEY UPDATE
                        cantidad       = cantidad + @qty,
                        costo_promedio = @costo,
                        valor_total    = (cantidad + @qty) * @costo";
                cmdInv.Parameters.AddWithValue("@idProd", idProd);
                cmdInv.Parameters.AddWithValue("@idAlm", idAlmacen);
                cmdInv.Parameters.AddWithValue("@qty", fila.Qty);
                cmdInv.Parameters.AddWithValue("@costo", fila.Cost);
                cmdInv.Parameters.AddWithValue("@valor", valorTotal);
                cmdInv.ExecuteNonQuery();
                resultado.InventariosInsertados++;
            }

            tx.Commit();
        } catch {
            tx.Rollback();
            throw;
        }

        return resultado;
    }

}