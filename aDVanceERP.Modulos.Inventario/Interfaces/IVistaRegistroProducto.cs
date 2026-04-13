using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Monedas;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    public interface IVistaRegistroProducto : IVistaRegistro {
        Image? Imagen { get; set; }
        string RutaImagen { get; }
        CategoriaProductoEnum Categoria { get; set; }
        string NombreProducto { get; set; }
        string? Codigo { get; set; }
        Proveedor? Proveedor { get; set; }
        string Descripcion { get; set; }
        UnidadMedida? UnidadMedida { get; set; }
        ClasificacionProducto? ClasificacionProducto { get; set; }
        bool EsVendible { get; set; }
        decimal CostoUnitario { get; set; }
        decimal CostoAdquisicionUnitario { get; }
        decimal CostoProduccionUnitario { get; }
        decimal ImpuestoVentaPorcentaje { get; set; }
        decimal MargenGananciaDeseado { get; set; }
        decimal PrecioVentaBase { get; set; }
        Almacen? Almacen { get; set; }
        decimal CantidadInicial { get; set; }
        decimal CantidadMinima { get; set; }
        bool HabilitarNotificacionesStockBajo { get; set; }

        // ── SOPORTE MULTIMONEDA ────────────────────────────────────────────────

        /// <summary>
        /// Moneda elegida por el usuario para el costo unitario del producto.
        /// El presentador convierte el valor a moneda base antes de persistir.
        /// </summary>
        Moneda? MonedaCosto { get; set; }

        // ── MÉTODOS ───────────────────────────────────────────────────────────

        void SalvarImagenEnDirectorioLocal();
        void CargarProveedores(Proveedor[] proveedores);
        void CargarUnidadesMedida(UnidadMedida[] unidadesMedida);
        void CargarClasificaciones(ClasificacionProducto[] clasificaciones);
        void CargarAlmacenes(Almacen[] almacenes);

        /// <summary>
        /// Carga el combo de monedas con las monedas activas del catálogo.
        /// La moneda base debe quedar preseleccionada por defecto.
        /// </summary>
        void CargarMonedas(Moneda[] monedas);

        /// <summary>
        /// Actualiza el símbolo visible junto al campo de costo cuando el
        /// usuario cambia la moneda seleccionada.
        /// </summary>
        void ActualizarSimboloMoneda(string simbolo);
    }
}