using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public class Movimiento : IEntidadBaseDatos {
        public Movimiento() {
            NombreProducto = string.Empty;
            NombreAlmacenOrigen = string.Empty;
            NombreAlmacenDestino = string.Empty;
            NombreTipoMovimiento = string.Empty;
        }

        public Movimiento(long id, long idProducto, decimal costoUnitario, long idAlmacenOrigen, long idAlmacenDestino, DateTime fechaCreacion, EstadoMovimiento estado, DateTime fecha, decimal saldoInicial, decimal cantidadMovida, decimal saldoFinal, long idTipoMovimiento, long idCuentaUsuario, string notas) {
            Id = id;
            IdProducto = idProducto;
            CostoUnitario = costoUnitario;
            IdAlmacenOrigen = idAlmacenOrigen;
            IdAlmacenDestino = idAlmacenDestino;
            FechaCreacion = fechaCreacion;
            Estado = estado;
            FechaTermino = fecha;
            SaldoInicial = saldoInicial;
            CantidadMovida = cantidadMovida;
            SaldoFinal = saldoFinal;
            IdTipoMovimiento = idTipoMovimiento;
            IdCuentaUsuario = idCuentaUsuario;
            Notas = notas;
            CostoTotal = costoUnitario * cantidadMovida;

            NombreProducto = string.Empty;
            NombreAlmacenOrigen = string.Empty;
            NombreAlmacenDestino = string.Empty;
            NombreTipoMovimiento = string.Empty;
        }

        public long Id { get; set; }
        public long IdProducto { get; set; }
        public decimal CostoUnitario { get; set; } // Costo unitario del producto, para valorización de inventario	
        public decimal CostoTotal { get; private set; }
        public long IdAlmacenOrigen { get; set; }
        public long IdAlmacenDestino { get; set; }
        public DateTime FechaCreacion { get; set; }
        public EstadoMovimiento Estado { get; set; }
        public DateTime FechaTermino { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal CantidadMovida { get; set; }
        public decimal SaldoFinal { get; set; }
        public long IdTipoMovimiento { get; set; }
        public long IdCuentaUsuario { get; set; }
        public string? Notas { get; set; }

        #region Datos auxiliares de tupla

        public string NombreProducto { get; set; }
        public string NombreAlmacenOrigen { get; set; }
        public string NombreAlmacenDestino { get; set; }
        public string NombreTipoMovimiento { get; set; }
        public EfectoMovimientoEnum EfectoMovimiento { get; set; }

        #endregion
    }

    public enum FiltroBusquedaMovimiento {
        Todos,
        Id,
        IdProducto,
        AlmacenOrigen,
        AlmacenDestino,
        Tipo
    }

    public static class UtilesBusquedaMovimiento {
        public static object[] FiltroBusquedaMovimiento = {
            "Todos",
            "ID",
            "ID del producto",
            "Almacén origen",
            "Almacén destino",
            "Tipo"
        };
    }
}