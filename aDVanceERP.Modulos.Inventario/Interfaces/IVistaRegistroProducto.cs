using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces;

public interface IVistaRegistroProducto : IVistaRegistro {
    Image? Imagen { get; set; }
    CategoriaProducto Categoria { get; set; }
    string Nombre { get; set; }
    string? Codigo { get; set; }
    string NombreProveedor { get; set; }
    string Descripcion { get; set; }
    string NombreUnidadMedida { get; set; }
    string NombreClasificacionProducto { get; set; }
    bool EsVendible { get; set; }
    decimal CostoUnitario { get; set; }
    decimal CostoAdquisicionUnitario { get; }
    decimal CostoProduccionUnitario { get; }
    decimal ImpuestoVentaPorcentaje { get; set; }
    decimal MargenGananciaDeseado { get; set; }
    decimal PrecioVentaBase { get; set; }
    string NombreAlmacen { get; set; }
    decimal CantidadInicial { get; set; }
    decimal CantidadMinima { get; set; }
    bool HabilitarNotificacionesStockBajo { get; set; }

    void CargarNombresProveedores(string[] nombresProvedores);
    void CargarUnidadesMedida(UnidadMedida[] unidadesMedida);
    void CargarClasificaciones(ClasificacionProducto[] nombresClasificaciones);
    void CargarNombresAlmacenes(string[] nombresAlmacenes);
}