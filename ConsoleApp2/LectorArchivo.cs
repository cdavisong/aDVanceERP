// =============================================================================
// LectorArchivo.cs — Lee CSV o Excel y devuelve List<FilaProducto>
// Columnas requeridas: Category, ShortName, LongName, Cost, Price, Taxable, Qty
// SolNo es opcional; si no existe el codigo se genera desde ShortName.
// =============================================================================

using ClosedXML.Excel;

namespace ConsoleApp2;

public static class LectorArchivo {
    private static readonly string[] ColsRequeridas =
        ["category", "shortname", "longname", "cost", "price", "taxable", "qty"];

    public static List<FilaProducto> Leer(string ruta) {
        string ext = Path.GetExtension(ruta).ToLowerInvariant();
        return ext switch {
            ".csv" => LeerCsv(ruta),
            ".xlsx" => LeerExcel(ruta),
            ".xls" => throw new NotSupportedException(
                "Formato .xls no soportado. Guarda el archivo como .xlsx o .csv."),
            _ => throw new NotSupportedException(
                $"Extension '{ext}' no reconocida. Usa .csv o .xlsx.")
        };
    }

    // ── CSV ───────────────────────────────────────────────────────────────────
    private static List<FilaProducto> LeerCsv(string ruta) {
        var lineas = File.ReadAllLines(ruta)
                         .Where(l => !string.IsNullOrWhiteSpace(l))
                         .ToArray();

        if (lineas.Length < 2)
            throw new InvalidDataException("El CSV esta vacio o solo tiene encabezado.");

        var encabezado = SplitCsv(lineas[0]);
        var idx = MapearColumnas(encabezado, ruta);
        var filas = new List<FilaProducto>();

        for (int i = 1; i < lineas.Length; i++) {
            var campos = SplitCsv(lineas[i]);
            if (campos.Length < 3) continue;
            filas.Add(CamposAFila(campos, idx));
        }
        return filas;
    }

    // ── Excel ─────────────────────────────────────────────────────────────────
    private static List<FilaProducto> LeerExcel(string ruta) {
        using var wb = new XLWorkbook(ruta);
        var ws = wb.Worksheets.First();
        var filaEnc = ws.RowsUsed().First();
        var encabezado = filaEnc.CellsUsed()
                                .Select(c => c.GetString())
                                .ToArray();
        var idx = MapearColumnas(encabezado, ruta);
        var filas = new List<FilaProducto>();

        foreach (var fila in ws.RowsUsed().Skip(1)) {
            int lastCol = fila.LastCellUsed()?.Address.ColumnNumber ?? 0;
            if (lastCol == 0) continue;
            var campos = fila.Cells(1, lastCol)
                             .Select(c => c.GetString())
                             .ToArray();
            filas.Add(CamposAFila(campos, idx));
        }
        return filas;
    }

    // ── Helpers ───────────────────────────────────────────────────────────────
    private static Dictionary<string, int> MapearColumnas(string[] encabezado, string ruta) {
        var map = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i < encabezado.Length; i++)
            map[encabezado[i].Trim()] = i;

        var faltantes = ColsRequeridas.Where(c => !map.ContainsKey(c)).ToList();
        if (faltantes.Count > 0)
            throw new InvalidDataException(
                $"Columnas requeridas no encontradas en '{Path.GetFileName(ruta)}': " +
                string.Join(", ", faltantes) + "\n" +
                $"Columnas encontradas: {string.Join(", ", encabezado)}");

        return map;
    }

    private static FilaProducto CamposAFila(string[] campos, Dictionary<string, int> idx) {
        string Cel(string col) =>
            idx.TryGetValue(col, out int i) && i < campos.Length
                ? campos[i].Trim() : "";

        decimal ParseDec(string col) {
            var s = Cel(col).Replace(",", ".");
            return decimal.TryParse(s,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out var v) ? v : 0m;
        }

        return new FilaProducto {
            Category = Cel("category"),
            ShortName = Cel("shortname"),
            LongName = Cel("longname"),
            Cost = ParseDec("cost"),
            Price = ParseDec("price"),
            Taxable = (int) Math.Round(ParseDec("taxable")),
            Qty = ParseDec("qty"),
        };
    }

    // RFC-4180: maneja comillas y comas internas
    public static string[] SplitCsv(string linea) {
        var campos = new List<string>();
        var campo = new System.Text.StringBuilder();
        bool enComilla = false;

        for (int i = 0; i < linea.Length; i++) {
            char c = linea[i];
            if (enComilla) {
                if (c == '"' && i + 1 < linea.Length && linea[i + 1] == '"') { campo.Append('"'); i++; } else if (c == '"') enComilla = false;
                else campo.Append(c);
            } else {
                if (c == '"') enComilla = true;
                else if (c == ',') { campos.Add(campo.ToString().Trim()); campo.Clear(); } else campo.Append(c);
            }
        }
        campos.Add(campo.ToString().Trim());
        return [.. campos];
    }
}