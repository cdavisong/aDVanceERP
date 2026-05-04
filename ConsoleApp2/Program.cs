// =============================================================================
// Program.cs — Punto de entrada del Migrador aDVance ERP
//
// Uso:
//   MigradorAdvance.exe <ruta_archivo>
//   MigradorAdvance.exe               (pide la ruta interactivamente)
//
// El programa solicita host, base de datos, usuario y contrasena por consola.
// =============================================================================
using ConsoleApp2;

using MySql.Data.MySqlClient;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Consola.Banner();

// ── 1. Detectar archivo ───────────────────────────────────────────────────────
string rutaArchivo;

if (args.Length > 0) {
    rutaArchivo = args[0];
} else {
    // Intentar detectar automaticamente CSV/XLSX en el directorio del exe
    var dir = AppContext.BaseDirectory;
    var archivos = Directory.GetFiles(dir, "*.csv")
                            .Concat(Directory.GetFiles(dir, "*.xlsx"))
                            .ToArray();

    if (archivos.Length == 1) {
        rutaArchivo = archivos[0];
        Consola.Info($"Archivo detectado automaticamente: {Path.GetFileName(rutaArchivo)}");
    } else if (archivos.Length > 1) {
        Consola.Warn("Se encontraron varios archivos en la carpeta. Escribe la ruta exacta:");
        foreach (var a in archivos)
            Console.WriteLine($"  {Path.GetFileName(a)}");
        rutaArchivo = Consola.Pedir("Ruta del archivo");
    } else {
        Consola.Warn("No se encontro ningun CSV ni XLSX en la carpeta del ejecutable.");
        rutaArchivo = Consola.Pedir("Ruta del archivo (CSV o XLSX)");
    }
}

if (!File.Exists(rutaArchivo)) {
    Consola.Error($"Archivo no encontrado: {rutaArchivo}");
    Consola.Salir(1);
    return;
}

// ── 2. Leer archivo ───────────────────────────────────────────────────────────
Console.WriteLine();
Consola.Info($"Leyendo: {Path.GetFileName(rutaArchivo)}");

List<FilaProducto> filas;
try {
    filas = LectorArchivo.Leer(rutaArchivo);
} catch (Exception ex) {
    Consola.Error($"Error al leer el archivo:\n  {ex.Message}");
    Consola.Salir(1);
    return;
}

if (filas.Count == 0) {
    Consola.Error("El archivo no contiene filas de datos.");
    Consola.Salir(1);
    return;
}

int conStock = filas.Count(f => f.Qty > 0);
int sinStock = filas.Count(f => f.Qty <= 0);
int numCats = filas.Select(f => f.Category.Trim())
                       .Where(c => c.Length > 0)
                       .Distinct(StringComparer.OrdinalIgnoreCase)
                       .Count();

Console.WriteLine();
Console.WriteLine($"  Productos encontrados : {filas.Count}");
Console.WriteLine($"  Con stock (Qty > 0)   : {conStock}");
Console.WriteLine($"  Sin stock (Qty = 0)   : {sinStock}");
Console.WriteLine($"  Categorias detectadas : {numCats}");
Console.WriteLine($"  Almacen destino       : P. VENTA  (Primario)");
Console.WriteLine();

// ── 3. Datos de conexion ──────────────────────────────────────────────────────
Consola.Seccion("Conexion a MySQL / MariaDB");

string host = Consola.Pedir("Host", "127.0.0.1");
string port = Consola.Pedir("Puerto", "3306");
string db = Consola.Pedir("Base de datos", "advanceerp");
string user = Consola.Pedir("Usuario", "root");
string pass = Consola.PedirPassword("Contrasena");

string connStr =
    $"Server={host};Port={port};Database={db};Uid={user};Pwd={pass};" +
    "CharSet=utf8mb4;SslMode=None;AllowZeroDateTime=True;Convert Zero Datetime=True;";

// ── 4. Verificar conexion ─────────────────────────────────────────────────────
Console.WriteLine();
Consola.Info("Verificando conexion...");
try {
    using var testConn = new MySqlConnection(connStr);
    testConn.Open();
    Consola.Ok("Conexion exitosa.");
} catch (Exception ex) {
    Consola.Error($"No se pudo conectar:\n  {ex.Message}");
    Consola.Salir(1);
    return;
}

// ── 5. Confirmacion ───────────────────────────────────────────────────────────
Console.WriteLine();
Console.Write("  Presiona S + ENTER para iniciar la migracion, o cualquier otra tecla para cancelar: ");
string? confirm = Console.ReadLine();
if (!string.Equals(confirm?.Trim(), "S", StringComparison.OrdinalIgnoreCase)) {
    Console.WriteLine();
    Consola.Warn("Operacion cancelada.");
    Consola.Salir(0);
    return;
}

// ── 6. Ejecutar migracion ─────────────────────────────────────────────────────
Console.WriteLine();
Consola.Seccion("Ejecutando migracion");

ResultadoMigracion resultado;
try {
    var migrador = new Migrador(connStr);
    resultado = migrador.Ejecutar(filas);
} catch (Exception ex) {
    Consola.Error($"La migracion fallo y se hizo ROLLBACK:\n  {ex.Message}");
    if (ex.InnerException != null)
        Console.WriteLine($"  Detalle: {ex.InnerException.Message}");
    Consola.Salir(1);
    return;
}

// ── 7. Resumen ────────────────────────────────────────────────────────────────
Console.WriteLine();
Consola.Seccion("Resumen de la migracion");
Console.WriteLine($"  Clasificaciones insertadas : {resultado.ClasificacionesInsertadas}");
Console.WriteLine($"  Almacen creado             : {(resultado.AlmacenCreado == 1 ? "Si (P. VENTA)" : "Ya existia")}");
Console.WriteLine($"  Productos insertados       : {resultado.ProductosInsertados}");
Console.WriteLine($"  Productos omitidos (dupl.) : {resultado.ProductosOmitidos}");
Console.WriteLine($"  Registros de inventario    : {resultado.InventariosInsertados}");
Console.WriteLine($"  Movimientos Carga Inicial  : {resultado.MovimientosInsertados}");

if (resultado.Advertencias.Count > 0) {
    Console.WriteLine();
    Consola.Warn($"Advertencias ({resultado.Advertencias.Count}):");
    foreach (var adv in resultado.Advertencias)
        Console.WriteLine($"  ! {adv}");
}

Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("  MIGRACION COMPLETADA EXITOSAMENTE");
Console.ResetColor();

Consola.Salir(0);

// =============================================================================
// Consola — helpers de presentacion
// =============================================================================
static class Consola {
    public static void Banner() {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine();
        Console.WriteLine("  ============================================================");
        Console.WriteLine("   aDVance ERP - Migrador de datos  |  DaviSOFT");
        Console.WriteLine("  ============================================================");
        Console.ResetColor();
        Console.WriteLine();
    }

    public static void Seccion(string titulo) {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"  -- {titulo}");
        Console.ResetColor();
    }

    public static void Info(string msg) {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("  [INFO] ");
        Console.ResetColor();
        Console.WriteLine(msg);
    }

    public static void Ok(string msg) {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("  [ OK ] ");
        Console.ResetColor();
        Console.WriteLine(msg);
    }

    public static void Warn(string msg) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("  [WARN] ");
        Console.ResetColor();
        Console.WriteLine(msg);
    }

    public static void Error(string msg) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(" [ERROR] ");
        Console.ResetColor();
        Console.WriteLine(msg);
    }

    public static string Pedir(string campo, string @default = "") {
        string display = @default.Length > 0 ? $" [{@default}]" : "";
        Console.Write($"  {campo}{display}: ");
        string? input = Console.ReadLine()?.Trim();
        return string.IsNullOrEmpty(input) ? @default : input;
    }

    public static string PedirPassword(string campo) {
        Console.Write($"  {campo}: ");
        var sb = new System.Text.StringBuilder();
        while (true) {
            var key = Console.ReadKey(intercept: true);
            if (key.Key == ConsoleKey.Enter) break;
            if (key.Key == ConsoleKey.Backspace && sb.Length > 0) { sb.Remove(sb.Length - 1, 1); Console.Write("\b \b"); } else if (key.Key != ConsoleKey.Backspace) { sb.Append(key.KeyChar); Console.Write('*'); }
        }
        Console.WriteLine();
        return sb.ToString();
    }

    public static void Salir(int code) {
        Console.WriteLine();
        Console.Write("  Presiona ENTER para cerrar...");
        Console.ReadLine();
        Environment.Exit(code);
    }
}