using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Monedas {
    /// <summary>
    /// Representa una tasa de cambio diaria de <c>adv__tasa_cambio</c>.
    /// La relación es: 1 unidad de <see cref="IdMonedaOrigen"/> = <see cref="Tasa"/> unidades de <see cref="IdMonedaDestino"/>.
    /// </summary>
    public class TasaCambio : IEntidadBaseDatos {
        public TasaCambio() { }

        public TasaCambio(long id, long idMonedaOrigen, long idMonedaDestino,
                          DateOnly fecha, decimal tasa, string? fuente,
                          bool aplicaEfectivo) {
            Id = id;
            IdMonedaOrigen = idMonedaOrigen;
            IdMonedaDestino = idMonedaDestino;
            Fecha = fecha;
            Tasa = tasa;
            Fuente = fuente;
            AplicaEfectivo = aplicaEfectivo;
        }

        public long Id { get; set; }
        public long IdMonedaOrigen { get; set; }
        public long IdMonedaDestino { get; set; }
        public DateOnly Fecha { get; set; }

        /// <summary>
        /// Cuántas unidades de la moneda destino equivalen a 1 unidad de la moneda origen.
        /// Ejemplo: si 1 USD = 230 CUP, entonces Tasa = 230.
        /// </summary>
        public decimal Tasa { get; set; }

        public string? Fuente { get; set; }

        /// <summary>
        /// TRUE si la tasa aplica para transacciones en efectivo.
        /// Las tasas BCC (oficiales) son FALSE.
        /// </summary>
        public bool AplicaEfectivo { get; set; }

        public override string ToString() => $"{Fecha:yyyy-MM-dd}  ×{Tasa:N4}";
    }

    public enum FiltroBusquedaTasaCambio {
        Todos,
        [Display(Name = "ID")]
        Id,
        [Display(Name = "Moneda origen/destino")]
        MonedaOrigenDestino,   // busca por par (idOrigen, idDestino)
        Fecha,
        [Display(Name = "Vigente hoy")]
        VigenteHoy             // tasa más reciente para un par a hoy
    }
}
