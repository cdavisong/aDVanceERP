using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Monedas;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    internal interface IVistaGestionVentaPresentacion : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaPrecioPresentacion>, INavegadorTuplasEntidades {
        long IdProducto { get; set; }
        UnidadMedida? UnidadMedida { get; set; }
        decimal Cantidad { get; set; }
        decimal PrecioVenta { get; set; }

        // ── SOPORTE MULTIMONEDA ────────────────────────────────────────────────

        /// <summary>
        /// Moneda elegida por el usuario para el precio de venta de la presentación.
        /// El presentador convierte a moneda base antes de persistir.
        /// </summary>
        Moneda? MonedaPrecioVenta { get; set; }

        // ── MÉTODOS ───────────────────────────────────────────────────────────

        void CargarUnidadesMedida(UnidadMedida[] unidadesMedida);
        void CargarDatosProducto(Producto producto);

        /// <summary>
        /// Carga el combo de monedas para la presentación.
        /// La moneda base debe quedar preseleccionada por defecto.
        /// </summary>
        void CargarMonedas(Moneda[] monedas);

        /// <summary>
        /// Actualiza el símbolo visible junto al campo de precio de venta
        /// cuando el usuario cambia la moneda seleccionada.
        /// </summary>
        void ActualizarSimboloMoneda(string simbolo);
    }
}
