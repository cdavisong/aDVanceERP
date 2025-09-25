using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario;

public class Movimiento : IEntidadBaseDatos {
    public Movimiento() { }

    public Movimiento(long id, long idProducto, decimal costoUnitario, decimal costoTotal, long idAlmacenOrigen, 
        long idAlmacenDestino, DateTime fechaCreacion, EstadoMovimiento estado, DateTime fecha, decimal saldoInicial, 
        decimal cantidadMovida, decimal saldoFinal, long idTipoMovimiento, long idCuentaUsuario) {
        Id = id;
        IdProducto = idProducto;
        CostoUnitario = costoUnitario;
        CostoTotal = costoTotal;
        IdAlmacenOrigen = idAlmacenOrigen;
        IdAlmacenDestino = idAlmacenDestino;
        FechaCreacion = fechaCreacion;
        Estado = estado;
        Fecha = fecha;
        SaldoInicial = saldoInicial;
        CantidadMovida = cantidadMovida;
        SaldoFinal = saldoFinal;
        IdTipoMovimiento = idTipoMovimiento;
        IdCuentaUsuario = idCuentaUsuario;
    }

    public long Id { get; set; }
    public long IdProducto { get; set; }
    public decimal CostoUnitario { get; set; } // Costo unitario del producto, para valorización de inventario	
    public decimal CostoTotal { get; }
    public long IdAlmacenOrigen { get; set; }
    public long IdAlmacenDestino { get; set; }
    public DateTime FechaCreacion { get; set; }
    public EstadoMovimiento Estado { get; set; }
    public DateTime Fecha { get; set; }
    public decimal SaldoInicial { get; set; }
    public decimal CantidadMovida { get; set; }
    public decimal SaldoFinal { get; set; }
    public long IdTipoMovimiento { get; set; }
    public long IdCuentaUsuario { get; set; }
}

public enum FiltroBusquedaMovimiento {
    Todos,
    Id,
    Producto,
    AlmacenOrigen,
    AlmacenDestino,
    Fecha,
    TipoMovimiento
}

public static class UtilesBusquedaMovimiento {
    public static object[] FiltroBusquedaMovimiento = {
        "Todos los movimientos",
        "Identificador de BD",
        "Nombre del producto",
        "Almacén de orígen",
        "Almacén de destino",
        "Fecha del movimiento",
        "Tipo de movimiento"
    };
}