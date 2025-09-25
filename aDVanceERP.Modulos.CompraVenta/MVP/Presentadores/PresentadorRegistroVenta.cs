using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.DetalleCompraventaProducto.Plantillas;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Venta.Plantillas;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Presentadores;

public class
    PresentadorRegistroVenta : PresentadorVistaRegistro<IVistaRegistroVenta, Venta, RepoVenta, FiltroBusquedaVenta> {
    public PresentadorRegistroVenta(IVistaRegistroVenta vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(Venta entidad) {
        Vista.ModoEdicion = true;
        Vista.Fecha = entidad.Fecha;
        Vista.RazonSocialCliente = UtilesCliente.ObtenerRazonSocialCliente(entidad.IdCliente) ?? string.Empty;
        Vista.NombreAlmacen = UtilesAlmacen.ObtenerNombreAlmacen(entidad.IdAlmacen) ?? string.Empty;
        Vista.Direccion = entidad.DireccionEntrega;
        Vista.EstadoEntrega = entidad.EstadoEntrega;

        var productosVenta = UtilesVenta.ObtenerProductosPorVenta(entidad.Id);

        foreach (var productoSplit in productosVenta.Select(producto => producto.Split('|')))
            ((IVistaGestionDetallesCompraventaProductos)Vista).AdicionarProducto(Vista.NombreAlmacen, productoSplit[0],
                productoSplit[1]);

        Vista.IdTipoEntrega = entidad.IdTipoEntrega;

        _entidad = entidad;
    }

    protected override Venta? ObtenerEntidadDesdeVista() {
        return new Venta(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.Fecha,
            UtilesAlmacen.ObtenerIdAlmacen(Vista.NombreAlmacen).Result,
            UtilesCliente.ObtenerIdCliente(Vista.RazonSocialCliente),
            Vista.IdTipoEntrega,
            Vista.Direccion,
            Vista.EstadoEntrega,
            Vista.Total
        );
    }
}