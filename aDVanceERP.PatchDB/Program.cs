using aDVanceERP.Core.Infraestructura.Extensiones.BD;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.PatchDB {
    internal class Program {
        private static string workingDirectory = "C:\\advanceerp\\programa\\";

        private static void Main(string[] args) {
            Console.CursorVisible = false;
            Console.Title = "aDVance ERP º Sistema de Parches";
            var version = "desconocida";

            if (File.Exists(@".\app.ver"))
                using (var fs = new FileStream(@".\app.ver", FileMode.Open)) {
                    using (var sr = new StreamReader(fs)) {
                        version = sr.ReadToEnd().Trim();
                    }
                }

            // Logo en ASCII con colores básicos
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(@"
                █████╗ ██████╗ ██╗   ██╗ █████╗ ███╗   ██╗ ██████╗███████╗
               ██╔══██╗██╔══██╗██║   ██║██╔══██╗████╗  ██║██╔════╝██╔════╝
               ███████║██║  ██║██║   ██║███████║██╔██╗ ██║██║     █████╗  
               ██╔══██║██║  ██║╚██╗ ██╔╝██╔══██║██║╚██╗██║██║     ██╔══╝  
               ██║  ██║██████╔╝ ╚████╔╝ ██║  ██║██║ ╚████║╚██████╗███████╗
               ╚═╝  ╚═╝╚═════╝   ╚═══╝  ╚═╝  ╚═╝╚═╝  ╚═══╝ ╚═════╝╚══════╝");
            Console.ResetColor();
            Console.WriteLine("               E R P  -  S I S T E M A  D E  P A R C H E S");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"               versión {version}\n");

            // Cargar la configuración de la base de datos
            RepoConfiguracionBaseDatos repoConfig = new(workingDirectory);
            ContextoBaseDatos.ActualizarConfiguracion(repoConfig.ObtenerPorId(0));

            try {
                ExecuteStep(CrearBaseDatosTemporal, "Creando base de datos temporal");
                ExecuteStep(() => EjecutarScriptEstructuraEmpty(ContextoBaseDatos.Configuracion.ToStringConexion().Replace("Database = advanceerp", "Database = advanceerp_temp")),
                    "Ejecutando estructura EMPTY en temporal");
                ExecuteStep(MigrarDatosMaestros, "Migrando datos maestros");
                ExecuteStep(EliminarBaseDatosOriginal, "Eliminando base original");
                ExecuteStep(() => LimpiarTablasTransaccionales(ContextoBaseDatos.Configuracion.ToStringConexion().Replace("Database = advanceerp", "Database = advanceerp_temp")), 
                    "Limpiando tablas transaccionales");

                RenderStatus("Parche aDVance ERP aplicado correctamente", ConsoleColor.Green);
            } catch (Exception ex) {
                RenderStatus($"Error crítico: {ex.Message}", ConsoleColor.Red);
            }

            Console.CursorVisible = true;
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Presione cualquier tecla para salir...");
            Console.ReadKey();
        }

        private static void CrearBaseDatosTemporal() {
            using (var conexion = ContextoBaseDatos.ObtenerConexionOptimizada()) {
                conexion.Open();
                using (var cmd = new MySqlCommand("CREATE DATABASE IF NOT EXISTS advanceerp_temp", conexion))
                    cmd.ExecuteNonQuery();
            }

            Thread.Sleep(5000);
        }

        private static void EjecutarScriptEstructuraEmpty(string stringConexion) {
            // Este método debería ejecutar el contenido completo de bd_EMPTY.sql
            // en la base de datos advanceerp_temp
            // Por simplicidad, asumimos que el script está embebido o en un archivo
            string scriptEmpty = File.ReadAllText("bd_EMPTY.sql");

            using (var conexion = new MySqlConnection(stringConexion)) {
                conexion.Open();

                var scriptParts = scriptEmpty.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in scriptParts) {
                    if (!string.IsNullOrWhiteSpace(part)) {
                        using (var cmd = new MySqlCommand(part, conexion))
                            cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private static void MigrarDatosMaestros() {
            string migracionScript = @"
                USE advanceerp_temp;

                -- adv__almacen
                INSERT INTO adv__almacen (id_almacen, nombre, direccion, autorizo_venta, notas)
                SELECT id_almacen, nombre, direccion, autorizo_venta, notas FROM advanceerp.adv__almacen;

                -- adv__contacto
                INSERT INTO adv__contacto (id_contacto, nombre, direccion_correo_electronico, direccion, notas)
                SELECT id_contacto, nombre, direccion_correo_electronico, direccion, notas FROM advanceerp.adv__contacto;

                -- adv__cuenta_bancaria
                INSERT INTO adv__cuenta_bancaria (id_cuenta_bancaria, alias, numero_tarjeta, moneda, id_contacto)
                SELECT id_cuenta_bancaria, alias, numero_tarjeta, moneda, id_contacto FROM advanceerp.adv__cuenta_bancaria;

                -- adv__cuenta_usuario
                INSERT INTO adv__cuenta_usuario (id_cuenta_usuario, nombre, password_hash, password_salt, id_rol_usuario, administrador, aprobado)
                SELECT id_cuenta_usuario, nombre, password_hash, password_salt, id_rol_usuario, administrador, aprobado FROM advanceerp.adv__cuenta_usuario;

                -- adv__db_version
                INSERT INTO adv__db_version (version, applied_date, patch_name)
                SELECT version, applied_date, patch_name FROM advanceerp.adv__db_version;

                -- adv__detalle_producto
                INSERT INTO adv__detalle_producto (id_detalle_producto, id_unidad_medida, descripcion, activo)
                SELECT id_detalle_producto, id_unidad_medida, descripcion, activo FROM advanceerp.adv__detalle_producto;

                -- adv__empresa
                INSERT INTO adv__empresa (id_empresa, nombre, logotipo, id_contacto)
                SELECT id_empresa, nombre, logotipo, id_contacto FROM advanceerp.adv__empresa;

                -- adv__modulo
                INSERT INTO adv__modulo (id_modulo, nombre)
                SELECT id_modulo, nombre FROM advanceerp.adv__modulo;

                -- adv__permiso
                INSERT INTO adv__permiso (id_permiso, id_modulo, nombre)
                SELECT id_permiso, id_modulo, nombre FROM advanceerp.adv__permiso;

                -- adv__producto
                INSERT INTO adv__producto (id_producto, categoria, codigo, nombre, id_detalle_producto, id_proveedor, id_tipo_materia_prima, es_vendible, precio_compra, costo_produccion_unitario, precio_venta_base)
                SELECT id_producto, categoria, codigo, nombre, id_detalle_producto, id_proveedor, id_tipo_materia_prima, es_vendible, precio_compra, costo_produccion_unitario, precio_venta_base FROM advanceerp.adv__producto;

                -- adv__inventario (desde adv__producto_almacen)
                INSERT INTO adv__inventario (id_inventario, id_producto, id_almacen, cantidad, costo_promedio, valor_total, ultima_actualizacion)
                SELECT 
                    pa.id_producto_almacen, 
                    pa.id_producto, 
                    pa.id_almacen, 
                    pa.stock,
                    CASE 
                        WHEN p.categoria = 'MateriaPrima' THEN COALESCE(p.precio_compra, 0.00)
                        WHEN p.categoria = 'Mercancia' THEN COALESCE(p.precio_compra, 0.00)
                        WHEN p.categoria = 'ProductoTerminado' THEN COALESCE(p.costo_produccion_unitario, 0.00)
                        ELSE 0.00
                    END AS costo_promedio,
                    CASE 
                        WHEN p.categoria = 'MateriaPrima' THEN COALESCE(p.precio_compra, 0.00) * pa.stock
                        WHEN p.categoria = 'Mercancia' THEN COALESCE(p.precio_compra, 0.00) * pa.stock
                        WHEN p.categoria = 'ProductoTerminado' THEN COALESCE(p.costo_produccion_unitario, 0.00) * pa.stock
                        ELSE 0.00
                    END AS valor_total,
                    NOW()
                FROM advanceerp.adv__producto_almacen pa
                INNER JOIN advanceerp.adv__producto p ON pa.id_producto = p.id_producto;

                -- adv__proveedor
                INSERT INTO adv__proveedor (id_proveedor, razon_social, nit, id_contacto)
                SELECT id_proveedor, razon_social, nit, id_contacto FROM advanceerp.adv__proveedor;

                -- adv__rol_permiso
                INSERT INTO adv__rol_permiso (id_rol_permiso, id_rol_usuario, id_permiso)
                SELECT id_rol_permiso, id_rol_usuario, id_permiso FROM advanceerp.adv__rol_permiso;

                -- adv__rol_usuario
                INSERT INTO adv__rol_usuario (id_rol_usuario, nombre)
                SELECT id_rol_usuario, nombre FROM advanceerp.adv__rol_usuario;

                -- adv__telefono_contacto
                INSERT INTO adv__telefono_contacto (id_telefono_contacto, prefijo, numero, categoria, id_contacto)
                SELECT id_telefono_contacto, prefijo, numero, categoria, id_contacto FROM advanceerp.adv__telefono_contacto;

                -- adv__tipo_entrega
                INSERT INTO adv__tipo_entrega (id_tipo_entrega, nombre, descripcion, requiere_pago_previo)
                SELECT id_tipo_entrega, nombre, descripcion, requiere_pago_previo FROM advanceerp.adv__tipo_entrega;

                -- adv__tipo_materia_prima
                INSERT INTO adv__tipo_materia_prima (id_tipo_materia_prima, nombre, descripcion)
                SELECT id_tipo_materia_prima, nombre, descripcion FROM advanceerp.adv__tipo_materia_prima;

                -- adv__tipo_movimiento
                INSERT INTO adv__tipo_movimiento (id_tipo_movimiento, nombre, efecto)
                SELECT id_tipo_movimiento, nombre, efecto FROM advanceerp.adv__tipo_movimiento;

                -- adv__unidad_medida
                INSERT INTO adv__unidad_medida (id_unidad_medida, nombre, abreviatura, descripcion)
                SELECT id_unidad_medida, nombre, abreviatura, descripcion FROM advanceerp.adv__unidad_medida;
            ";

            using (var conexion = ContextoBaseDatos.ObtenerConexionOptimizada()) {
                conexion.Open();
                var scriptParts = migracionScript.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in scriptParts) {
                    if (!string.IsNullOrWhiteSpace(part)) {
                        using (var cmd = new MySqlCommand(part, conexion))
                            cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private static void EliminarBaseDatosOriginal() {
            using (var conexion = ContextoBaseDatos.ObtenerConexionOptimizada()) {
                conexion.Open();
                using (var cmd = new MySqlCommand("DROP DATABASE IF EXISTS advanceerp", conexion))
                    cmd.ExecuteNonQuery();
            }

            Thread.Sleep(2000);
        }

        private static void LimpiarTablasTransaccionales(string stringConexion) {
            string limpiezaScript = @"
                USE advanceerp_temp;

                TRUNCATE TABLE adv__caja;
                TRUNCATE TABLE adv__compra;
                TRUNCATE TABLE adv__detalle_compra_producto;
                TRUNCATE TABLE adv__venta;
                TRUNCATE TABLE adv__detalle_venta_producto;
                TRUNCATE TABLE adv__pago;
                TRUNCATE TABLE adv__detalle_pago_transferencia;
                TRUNCATE TABLE adv__seguimiento_entrega;
                TRUNCATE TABLE adv__movimiento;
                TRUNCATE TABLE adv__movimiento_caja;
                TRUNCATE TABLE adv__orden_actividad;
                TRUNCATE TABLE adv__orden_gasto_dinamico;
                TRUNCATE TABLE adv__orden_gasto_indirecto;
                TRUNCATE TABLE adv__orden_material;
                TRUNCATE TABLE adv__orden_produccion;
                TRUNCATE TABLE adv__producto_mano_obra;
                TRUNCATE TABLE adv__producto_materia_prima;
            ";

            using (var conexion = new MySqlConnection(stringConexion)) {
                conexion.Open();
                var scriptParts = limpiezaScript.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in scriptParts) {
                    if (!string.IsNullOrWhiteSpace(part)) {
                        using (var cmd = new MySqlCommand(part, conexion))
                            cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        #region Funciones de Interfaz

        private static void ExecuteStep(Action<string> step, string title, string stringConexion) {
            RenderProgressBar(title, ConsoleColor.White);
            try {
                step.Invoke(stringConexion);
                Console.SetCursorPosition(60, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("COMPLETADO");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("]");
            } catch (Exception ex) {
                Console.SetCursorPosition(60, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("FALLIDO");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("]");
                throw;
            }
            Console.ResetColor();
        }

        private static void ExecuteStep(Action step, string title) {
            RenderProgressBar(title, ConsoleColor.White);
            try {
                step.Invoke();
                Console.SetCursorPosition(60, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("COMPLETADO");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("]");
            } catch (Exception ex) {
                Console.SetCursorPosition(60, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("FALLIDO");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("]");
                throw;
            }

            Console.ResetColor();
        }

        private static void RenderStatus(string message, ConsoleColor color) {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"\n[{DateTime.Now:HH:mm:ss}]");
            Console.ForegroundColor = color;
            Console.Write(" » ");
            Console.ForegroundColor = color;
            Console.Write(message, color);
            Console.WriteLine("\n");
            Console.ResetColor();
        }

        private static void RenderProgressBar(string text, ConsoleColor color) {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($" [{DateTime.Now:HH:mm:ss}]");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" -");
            Console.ForegroundColor = color;
            Console.Write($" {text,-40}");
            Console.ResetColor();
        }

        #endregion
    }
}