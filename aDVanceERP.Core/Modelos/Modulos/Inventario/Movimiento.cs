using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public class Movimiento : IEntidadBaseDatos {
        public Movimiento() {
            NombreProducto = string.Empty;
            NombreAlmacenOrigen = string.Empty;
            NombreAlmacenDestino = string.Empty;
            NombreTipoMovimiento = string.Empty;
        }

        public Movimiento(long id, long idProducto, decimal costoUnitario, long idAlmacenOrigen, long idAlmacenDestino, DateTime fechaCreacion, EstadoMovimientoEnum estado, DateTime fecha, decimal saldoInicial, decimal cantidadMovida, decimal saldoFinal, long idTipoMovimiento, long idCuentaUsuario, string notas) {
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
        public EstadoMovimientoEnum Estado { get; set; }
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

        public override string ToString() {
            return $"ID: {Id:000}, PROD: {NombreProducto}, CANT: {CantidadMovida.ToString("N1", CultureInfo.InvariantCulture)}, ORIG: {NombreAlmacenOrigen}, DEST: {NombreAlmacenDestino}, TIPO: {NombreTipoMovimiento}, FECHA: {FechaCreacion:yyyy-MM-dd}";
        }
    }

    public enum FiltroBusquedaMovimiento {
        Todos,
        [Display(Name = "ID")]
        Id,
        [Display(Name = "ID del Producto")]
        IdProducto,
        [Display(Name = "Almacén Origen")]
        AlmacenOrigen,
        [Display(Name = "Almacén Destino")]
        AlmacenDestino,
        Tipo
    }
}