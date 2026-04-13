using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
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

        void SalvarImagenEnDirectorioLocal();
        void CargarProveedores(Proveedor[] proveedores);
        void CargarUnidadesMedida(UnidadMedida[] unidadesMedida);
        void CargarClasificaciones(ClasificacionProducto[] clasificaciones);
        void CargarAlmacenes(Almacen[] almacenes);
    }
}