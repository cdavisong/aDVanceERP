using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Monedas {
    /// <summary>
    /// Representa una moneda del catálogo <c>adv__moneda</c>.
    /// </summary>
    public class Moneda : IEntidadBaseDatos {
        public Moneda() { }

        public Moneda(long id, string codigo, string nombre, string simbolo,
                      int precisionDecimal, bool esBase, bool activa) {
            Id = id;
            Codigo = codigo;
            Nombre = nombre;
            Simbolo = simbolo;
            PrecisionDecimal = precisionDecimal;
            EsBase = esBase;
            Activa = activa;
        }

        /// <summary>id_moneda</summary>
        public long Id { get; set; }

        /// <summary>Código ISO 4217: CUP, USD, EUR, …</summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre completo: "Peso Cubano", "Dólar Estadounidense", …</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Símbolo de presentación: $, US$, €, …</summary>
        public string Simbolo { get; set; } = string.Empty;

        /// <summary>Decimales estándar de la moneda (normalmente 2).</summary>
        public int PrecisionDecimal { get; set; } = 2;

        /// <summary>
        /// Indica si esta es la moneda base del sistema (solo una puede serlo).
        /// Todos los costos se almacenan en moneda base.
        /// </summary>
        public bool EsBase { get; set; }

        /// <summary>Moneda activa y disponible para selección.</summary>
        public bool Activa { get; set; } = true;

        /// <summary>Representación legible: "CUP – Peso Cubano ($)"</summary>
        public override string ToString() => $"{Codigo}";
    }

    public enum FiltroBusquedaMoneda {
        Todos,
        [Display(Name = "ID")]
        Id,
        [Display(Name = "Código")]
        Codigo,
        [Display(Name = "Moneda activa")]
        SoloActivas,
        [Display(Name = "Moneda base")]
        SoloBase
    }
}
