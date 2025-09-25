using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Compra.Plantillas;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.DetalleCompraventaProducto.Plantillas;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Presentadores;

public class
    PresentadorRegistroCompra : PresentadorVistaRegistro<IVistaRegistroCompra, Compra, RepoCompra,
        FiltroBusquedaCompra> {
    public PresentadorRegistroCompra(IVistaRegistroCompra vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(Compra objeto) {
        Vista.ModoEdicion = true;
        Vista.RazonSocialProveedor = UtilesProveedor.ObtenerRazonSocialProveedor(objeto.IdProveedor) ?? string.Empty;
        Vista.NombreAlmacen = UtilesAlmacen.ObtenerNombreAlmacen(objeto.IdAlmacen) ?? string.Empty;        

        var productosCompra = UtilesCompra.ObtenerProductosPorCompra(objeto.Id);

        foreach (var productoSplit in productosCompra.Select(producto => producto.Split('|')))
            ((IVistaGestionDetallesCompraventaProductos)Vista).AdicionarProducto(Vista.NombreAlmacen, productoSplit[0],
                productoSplit[1]);

        _entidad = objeto;
    }

    protected override Compra? ObtenerEntidadDesdeVista() {
        return new Compra(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            DateTime.Now,
            UtilesAlmacen.ObtenerIdAlmacen(Vista.NombreAlmacen).Result,
            UtilesProveedor.ObtenerIdProveedor(Vista.RazonSocialProveedor).Result,
            Vista.Total
        );
    }
}