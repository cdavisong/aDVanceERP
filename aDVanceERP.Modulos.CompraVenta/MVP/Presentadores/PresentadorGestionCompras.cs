using System.Globalization;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Compra;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Compra.Plantillas;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Presentadores;

public class PresentadorGestionCompras : PresentadorVistaGestion<PresentadorTuplaCompra, IVistaGestionCompras, IVistaTuplaCompra, Compra, RepoCompra, FiltroBusquedaCompra> {
    public PresentadorGestionCompras(IVistaGestionCompras vista) : base(vista) { }

    protected override PresentadorTuplaCompra ObtenerValoresTupla(Compra objeto) {
        var presentadorTupla = new PresentadorTuplaCompra(new VistaTuplaCompra(), objeto);
        var nombreProveedor = UtilesProveedor.ObtenerRazonSocialProveedor(objeto.IdProveedor) ?? string.Empty;

        presentadorTupla.Vista.Id = objeto.Id.ToString();
        presentadorTupla.Vista.Fecha = objeto.Fecha.ToString("yyyy-MM-dd");
        presentadorTupla.Vista.NombreAlmacen = UtilesAlmacen.ObtenerNombreAlmacen(objeto.IdAlmacen) ?? string.Empty;
        presentadorTupla.Vista.NombreProveedor = string.IsNullOrEmpty(nombreProveedor) ? "Anónimo" : nombreProveedor;
        presentadorTupla.Vista.CantidadProductos = UtilesCompra.ObtenerCantidadProductosCompra(objeto.Id).ToString("N2", CultureInfo.InvariantCulture);
        presentadorTupla.Vista.MontoTotal = objeto.Total.ToString("N2", CultureInfo.InvariantCulture);

        return presentadorTupla;
    }

    public override void ActualizarResultadosBusqueda() {
        // Actualizar el valor bruto de las compras al refrescar la lista de objetos.
        Vista.ActualizarValorBrutoCompras();

        base.ActualizarResultadosBusqueda();
    }
}