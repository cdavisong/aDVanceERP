// =============================================================================
// Models.cs
// =============================================================================
namespace ConsoleApp2;

public class FilaProducto {
    public string Category { get; set; } = "";
    public string ShortName { get; set; } = "";
    public string LongName { get; set; } = "";
    public decimal Cost { get; set; }
    public decimal Price { get; set; }
    public int Taxable { get; set; }
    public decimal Qty { get; set; }
}

public class ResultadoMigracion {
    public int ClasificacionesInsertadas { get; set; }
    public int AlmacenCreado { get; set; }
    public int ProductosInsertados { get; set; }
    public int ProductosOmitidos { get; set; }
    public int InventariosInsertados { get; set; }
    public int MovimientosInsertados { get; set; }
    public List<string> Advertencias { get; set; } = [];
}