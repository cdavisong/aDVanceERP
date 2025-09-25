using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto.Plantillas;

public interface IVistaRegistroProducto : IVistaRegistro {
    // P1 : Datos generales
    CategoriaProducto CategoriaProducto { get; set; }
    string NombreProducto { get; set; }
    string Codigo { get; set; }
    string Descripcion { get; set; }

    // P1_1 : Datos del roveedor y disponibilidad de venta directa de materias primas
    string RazonSocialProveedor { get; set; }
    bool EsVendible { get; set; }

    // P2 : Unidad de medida, precios de compraventa y cantidad inicial
    string UnidadMedida { get; set; }
    string TipoMateriaPrima { get; set; }
    decimal PrecioCompra { get; set; }
    decimal CostoProduccionUnitario { get; set; }
    decimal PrecioVentaBase { get; set; }
    string? NombreAlmacen { get; set; }
    decimal CantidadInicial { get; set; }

    event EventHandler? RegistrarUnidadMedida;
    event EventHandler? RegistrarTipoMateriaPrima;
    event EventHandler? EliminarUnidadMedida;
    event EventHandler? EliminarTipoMateriaPrima;

    void CargarNombresProductos(string[] nombresProductos);
    void CargarRazonesSocialesProveedores(object[] nombresProveedores);
    void CargarUnidadesMedida((string[] nombres, string[] abreviaturas, string[] descripciones) unidadesMedida);
    void CargarTiposMateriaPrima(object[] nombresTiposMateriaPrima);
    void CargarDescripcionesTiposMateriaPrima(string[] descripcionesTiposMateriaPrima);
    void CargarNombresAlmacenes(object[] nombresAlmacenes);
}