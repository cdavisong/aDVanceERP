using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using ClosedXML.Excel;

using MySql.Data.MySqlClient;

namespace MigracionExcelERP {
    class Program {
        // ==================================================================
        // CONFIGURACIÓN - CAMBIAR SEGÚN TU ENTORNO
        // ==================================================================
        private const string ConnectionString = "Server=localhost;Database=advanceerp;Uid=root;Pwd=;";
        private const string ExcelFilePath = @".\ipv pnto 3lm.xlsx";

        // Almacén por defecto
        private const string AlmacenNombre = "P. VENTA";
        private const string AlmacenDireccion = "Dirección por defecto";
        private const string AlmacenTipo = "Primario";

        // Valores fijos
        private const int IdClasificacionDefecto = 1;
        private const int IdUnidadFija = 1;  // Siempre "Unidad"
        private const int IdUsuarioMigracion = 1;  // ID del usuario que ejecuta la migración (ajústalo)
        private const int IdTipoMovimientoCargaInicial = 11;  // Según tu trigger

        // ==================================================================
        // ESTRUCTURA DE DATOS
        // ==================================================================
        class ProductoExcel {
            public string Nombre { get; set; }
            public string Unidad { get; set; }  // Solo referencia, no se usa
            public decimal CantidadInicial { get; set; }
            public decimal CostoAdquisicion { get; set; }
            public decimal PrecioVenta { get; set; }
        }

        // ==================================================================
        // HELPER PARA GENERAR CÓDIGO EAN13
        // ==================================================================
        public static class CodigoHelper {
            public static string GenerarEan13(string texto) {
                if (string.IsNullOrWhiteSpace(texto))
                    throw new ArgumentException("El texto no puede estar vacío");

                // Paso 1: Obtener un hash del texto
                byte[] hashBytes;
                using (var sha256 = SHA256.Create())
                    hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));

                // Paso 2: Convertir el hash a un número largo
                long numericHash = BitConverter.ToInt64(hashBytes, 0);
                numericHash = Math.Abs(numericHash);

                // Paso 3: Tomar los primeros 12 dígitos
                string baseNumber = numericHash.ToString().PadRight(12, '0').Substring(0, 12);

                // Paso 4: Calcular el dígito de control
                int checksum = CalcularSumaChequeoEan13(baseNumber);
                string ean13 = baseNumber + checksum;

                return ean13;
            }

            private static int CalcularSumaChequeoEan13(string primeros12Digitos) {
                if (primeros12Digitos.Length != 12 || !primeros12Digitos.All(char.IsDigit))
                    throw new ArgumentException("Se requieren exactamente 12 dígitos");

                int sum = 0;
                for (int i = 0; i < 12; i++) {
                    int digit = int.Parse(primeros12Digitos[i].ToString());
                    sum += (i % 2 == 0 ? 1 : 3) * digit;
                }

                return (10 - (sum % 10)) % 10;
            }
        }

        // ==================================================================
        // MAIN
        // ==================================================================
        static void Main(string[] args) {
            Console.WriteLine("=== Migración desde Excel a aDVance ERP ===");
            Console.WriteLine("=================== 3LM ===================\n");

            if (!File.Exists(ExcelFilePath)) {
                Console.WriteLine($"ERROR: No se encuentra el archivo {ExcelFilePath}");
                Console.WriteLine("Presione cualquier tecla para salir...");
                Console.ReadKey();
                return;
            }

            try {
                // 1. Leer productos del Excel
                Console.WriteLine("Leyendo archivo Excel...");
                var productos = LeerProductosDesdeExcel(ExcelFilePath);
                Console.WriteLine($"Se leyeron {productos.Count} productos del Excel.\n");

                // 2. Conectar a la base de datos
                using (var conn = new MySqlConnection(ConnectionString)) {
                    conn.Open();
                    Console.WriteLine("Conexión a la base de datos establecida.\n");

                    // 3. Asegurar almacén por defecto
                    int idAlmacen = ObtenerOCrearAlmacen(conn);
                    Console.WriteLine($"Almacén asegurado: '{AlmacenNombre}' (ID = {idAlmacen})\n");

                    // 4. Procesar cada producto
                    int totalProductos = productos.Count;
                    int productosNuevos = 0;
                    int productosExistentes = 0;
                    int movimientosRegistrados = 0;

                    for (int i = 0; i < productos.Count; i++) {
                        var prod = productos[i];
                        Console.WriteLine($"[{i + 1}/{totalProductos}] Procesando: {prod.Nombre}");

                        // Obtener o crear producto
                        var resultado = ObtenerOCrearProducto(conn, prod);
                        int idProducto = resultado.idProducto;
                        bool esNuevo = resultado.esNuevo;

                        if (esNuevo)
                            productosNuevos++;
                        else
                            productosExistentes++;

                        // Actualizar inventario y registrar movimiento si es necesario
                        if (prod.CantidadInicial != 0) {
                            bool movimientoRegistrado = ActualizarInventarioConMovimiento(
                                conn, idProducto, idAlmacen,
                                prod.CantidadInicial, prod.CostoAdquisicion,
                                esNuevo);

                            if (movimientoRegistrado)
                                movimientosRegistrados++;

                            Console.WriteLine($"  ✓ Stock actualizado: {prod.CantidadInicial} unidades (Costo: {prod.CostoAdquisicion:C2})");
                        } else {
                            Console.WriteLine($"  ✓ Producto registrado sin stock inicial");
                        }

                        Console.WriteLine();
                    }

                    // 5. Resumen final
                    Console.WriteLine("===========================================");
                    Console.WriteLine("MIGRACIÓN COMPLETADA");
                    Console.WriteLine("===========================================");
                    Console.WriteLine($"Total productos leídos:     {totalProductos}");
                    Console.WriteLine($"Productos nuevos:            {productosNuevos}");
                    Console.WriteLine($"Productos existentes:        {productosExistentes}");
                    Console.WriteLine($"Movimientos carga inicial:   {movimientosRegistrados}");
                    Console.WriteLine("===========================================");
                }
            } catch (Exception ex) {
                Console.WriteLine($"\nERROR GENERAL: {ex.Message}");
                Console.WriteLine($"Detalle: {ex.StackTrace}");
            } finally {
                Console.WriteLine("\nPresione cualquier tecla para salir...");
                Console.ReadKey();
            }
        }

        // ==================================================================
        // LECTURA DEL EXCEL (ClosedXML)
        // ==================================================================
        static List<ProductoExcel> LeerProductosDesdeExcel(string ruta) {
            var lista = new List<ProductoExcel>();
            using (var workbook = new XLWorkbook(ruta)) {
                var hoja = workbook.Worksheet(1);
                bool modoLectura = false;
                int filaNumero = 0;

                foreach (var row in hoja.RowsUsed()) {
                    filaNumero = row.RowNumber();
                    var celdaA = row.Cell(1).GetString().Trim();

                    // Detectar cabecera "Producto"
                    if (!modoLectura && celdaA.Equals("Producto", StringComparison.OrdinalIgnoreCase)) {
                        modoLectura = true;
                        Console.WriteLine($"  Cabecera encontrada en fila {filaNumero}, iniciando lectura...");
                        continue;
                    }

                    if (modoLectura && !string.IsNullOrEmpty(celdaA)) {
                        string lower = celdaA.ToLower();
                        if (lower == "ok" || lower == "ganancia" || lower == "total" || lower == "sum") {
                            modoLectura = false;
                            Console.WriteLine($"  Fin de bloque de datos en fila {filaNumero}");
                            continue;
                        }

                        if (celdaA.StartsWith("=") || celdaA.StartsWith("'"))
                            continue;

                        string nombre = celdaA;
                        string unidad = row.Cell(2).GetString();
                        decimal cantidad = 0;
                        decimal costo = 0;
                        decimal precio = 0;

                        // Columna C - Cantidad
                        var cellC = row.Cell(3);
                        if (cellC.DataType == XLDataType.Number)
                            cantidad = (decimal) cellC.GetDouble();
                        else if (cellC.DataType == XLDataType.Text && decimal.TryParse(cellC.GetString(), out decimal tmpC))
                            cantidad = tmpC;

                        // Columna D - Costo (pc)
                        var cellD = row.Cell(4);
                        if (cellD.DataType == XLDataType.Number)
                            costo = (decimal) cellD.GetDouble();
                        else if (cellD.DataType == XLDataType.Text && decimal.TryParse(cellD.GetString(), out decimal tmpD))
                            costo = tmpD;

                        // Columna E - Precio Venta (pv)
                        var cellE = row.Cell(5);
                        if (cellE.DataType == XLDataType.Number)
                            precio = (decimal) cellE.GetDouble();
                        else if (cellE.DataType == XLDataType.Text && decimal.TryParse(cellE.GetString(), out decimal tmpE))
                            precio = tmpE;

                        if (!string.IsNullOrEmpty(nombre) && (cantidad != 0 || costo != 0)) {
                            lista.Add(new ProductoExcel {
                                Nombre = nombre,
                                Unidad = unidad,
                                CantidadInicial = cantidad,
                                CostoAdquisicion = costo,
                                PrecioVenta = precio
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // ==================================================================
        // ALMACÉN
        // ==================================================================
        static int ObtenerOCrearAlmacen(MySqlConnection conn) {
            string sql = "SELECT id_almacen FROM adv__almacen WHERE nombre = @nombre";
            using (var cmd = new MySqlCommand(sql, conn)) {
                cmd.Parameters.AddWithValue("@nombre", AlmacenNombre);
                var result = cmd.ExecuteScalar();
                if (result != null)
                    return Convert.ToInt32(result);
            }

            string insert = @"INSERT INTO adv__almacen (nombre, direccion, tipo, estado) 
                              VALUES (@nombre, @direccion, @tipo, 1)";
            using (var cmd = new MySqlCommand(insert, conn)) {
                cmd.Parameters.AddWithValue("@nombre", AlmacenNombre);
                cmd.Parameters.AddWithValue("@direccion", AlmacenDireccion);
                cmd.Parameters.AddWithValue("@tipo", AlmacenTipo);
                cmd.ExecuteNonQuery();
                return (int) cmd.LastInsertedId;
            }
        }

        // ==================================================================
        // PRODUCTO
        // ==================================================================
        static (int idProducto, bool esNuevo) ObtenerOCrearProducto(MySqlConnection conn, ProductoExcel prod) {
            // Verificar si ya existe por nombre exacto
            string sqlSelect = "SELECT id_producto FROM adv__producto WHERE nombre = @nombre";
            using (var cmd = new MySqlCommand(sqlSelect, conn)) {
                cmd.Parameters.AddWithValue("@nombre", prod.Nombre);
                var result = cmd.ExecuteScalar();
                if (result != null)
                    return (Convert.ToInt32(result), false);
            }

            // Generar código EAN13 a partir del nombre
            string codigoEan13 = CodigoHelper.GenerarEan13(prod.Nombre);

            // Descripción vacía
            string descripcion = string.Empty;

            // Insertar nuevo producto
            string insert = @"INSERT INTO adv__producto
                (nombre, codigo, descripcion, id_unidad_medida, id_clasificacion_producto,
                 costo_adquisicion_unitario, costo_produccion_unitario, precio_venta_base,
                 impuesto_venta_porcentaje, margen_ganancia_deseado, es_vendible, activo,
                 ruta_imagen, id_proveedor)
                VALUES (@nombre, @codigo, @descripcion, @idUnidad, @idClasif,
                        @costoAdquisicion, 0, @precioVenta,
                        10.0, 0.0, 1, 1,
                        NULL, 0)";

            using (var cmd = new MySqlCommand(insert, conn)) {
                cmd.Parameters.AddWithValue("@nombre", prod.Nombre);
                cmd.Parameters.AddWithValue("@codigo", codigoEan13);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@idUnidad", IdUnidadFija);
                cmd.Parameters.AddWithValue("@idClasif", IdClasificacionDefecto);
                cmd.Parameters.AddWithValue("@costoAdquisicion", prod.CostoAdquisicion);
                cmd.Parameters.AddWithValue("@precioVenta", prod.PrecioVenta);
                cmd.ExecuteNonQuery();
                return ((int) cmd.LastInsertedId, true);
            }
        }

        // ==================================================================
        // INVENTARIO CON MOVIMIENTO DE CARGA INICIAL
        // ==================================================================
        static bool ActualizarInventarioConMovimiento(MySqlConnection conn, int idProducto, int idAlmacen,
                                                       decimal cantidad, decimal costo, bool esProductoNuevo) {
            if (cantidad == 0) return false;

            // Verificar si ya existe registro de inventario
            string sqlSelect = "SELECT id_inventario, cantidad, costo_promedio FROM adv__inventario " +
                               "WHERE id_producto = @idProd AND id_almacen = @idAlm";
            using (var cmd = new MySqlCommand(sqlSelect, conn)) {
                cmd.Parameters.AddWithValue("@idProd", idProducto);
                cmd.Parameters.AddWithValue("@idAlm", idAlmacen);
                using (var reader = cmd.ExecuteReader()) {
                    if (reader.Read()) {
                        int idInv = reader.GetInt32(0);
                        decimal cantActual = reader.GetDecimal(1);
                        decimal costActual = reader.GetDecimal(2);
                        reader.Close();

                        decimal nuevaCantidad = cantActual + cantidad;
                        decimal nuevoCosto = (cantActual * costActual + cantidad * costo) / nuevaCantidad;
                        decimal nuevoValor = nuevaCantidad * nuevoCosto;

                        string update = @"UPDATE adv__inventario 
                                          SET cantidad = @cant, costo_promedio = @costo, 
                                              valor_total = @valor, ultima_actualizacion = NOW()
                                          WHERE id_inventario = @idInv";
                        using (var updCmd = new MySqlCommand(update, conn)) {
                            updCmd.Parameters.AddWithValue("@cant", nuevaCantidad);
                            updCmd.Parameters.AddWithValue("@costo", nuevoCosto);
                            updCmd.Parameters.AddWithValue("@valor", nuevoValor);
                            updCmd.Parameters.AddWithValue("@idInv", idInv);
                            updCmd.ExecuteNonQuery();
                        }

                        // Solo registrar movimiento si es producto nuevo
                        if (esProductoNuevo) {
                            RegistrarMovimientoCargaInicial(conn, idProducto, idAlmacen, cantidad, costo);
                            return true;
                        }
                        return false;
                    }
                }
            }

            // No existía el inventario - insertar nuevo
            decimal valorTotal = cantidad * costo;
            string insertInventario = @"INSERT INTO adv__inventario 
                                        (id_producto, id_almacen, cantidad, costo_promedio, valor_total)
                                        VALUES (@idProd, @idAlm, @cant, @costo, @valor)";

            using (var cmd = new MySqlCommand(insertInventario, conn)) {
                cmd.Parameters.AddWithValue("@idProd", idProducto);
                cmd.Parameters.AddWithValue("@idAlm", idAlmacen);
                cmd.Parameters.AddWithValue("@cant", cantidad);
                cmd.Parameters.AddWithValue("@costo", costo);
                cmd.Parameters.AddWithValue("@valor", valorTotal);
                cmd.ExecuteNonQuery();
            }

            // Registrar movimiento de carga inicial
            RegistrarMovimientoCargaInicial(conn, idProducto, idAlmacen, cantidad, costo);
            return true;
        }

        // ==================================================================
        // REGISTRAR MOVIMIENTO DE CARGA INICIAL
        // ==================================================================
        static void RegistrarMovimientoCargaInicial(MySqlConnection conn, int idProducto, int idAlmacen,
                                                    decimal cantidad, decimal costo) {
            string insertMovimiento = @"INSERT INTO adv__movimiento 
                (id_producto, costo_unitario, id_almacen_origen, id_almacen_destino, 
                 fecha_creacion, estado, saldo_inicial, fecha_termino, 
                 cantidad_movida, saldo_final, id_tipo_movimiento, id_cuenta_usuario, notas)
                VALUES (@idProducto, @costoUnitario, @idAlmacenOrigen, @idAlmacenDestino,
                        NOW(), 'Completado', 0, NOW(),
                        @cantidadMovida, @saldoFinal, @idTipoMovimiento, @idUsuario, @notas)";

            using (var cmd = new MySqlCommand(insertMovimiento, conn)) {
                cmd.Parameters.AddWithValue("@idProducto", idProducto);
                cmd.Parameters.AddWithValue("@costoUnitario", costo);
                cmd.Parameters.AddWithValue("@idAlmacenOrigen", idAlmacen);
                cmd.Parameters.AddWithValue("@idAlmacenDestino", idAlmacen);
                cmd.Parameters.AddWithValue("@cantidadMovida", cantidad);
                cmd.Parameters.AddWithValue("@saldoFinal", cantidad);
                cmd.Parameters.AddWithValue("@idTipoMovimiento", IdTipoMovimientoCargaInicial);
                cmd.Parameters.AddWithValue("@idUsuario", IdUsuarioMigracion);
                cmd.Parameters.AddWithValue("@notas", "Carga inicial de inventario desde migración Excel");
                cmd.ExecuteNonQuery();
            }
        }
    }
}