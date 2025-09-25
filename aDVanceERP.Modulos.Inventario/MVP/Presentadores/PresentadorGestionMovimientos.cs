using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Movimiento;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Movimiento.Plantillas;
using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.MVP.Presentadores;

public class PresentadorGestionMovimientos : PresentadorVistaGestion<PresentadorTuplaMovimiento, IVistaGestionMovimientos
    , IVistaTuplaMovimiento, Movimiento, RepoMovimiento, FiltroBusquedaMovimiento> {
    public PresentadorGestionMovimientos(IVistaGestionMovimientos vista) : base(vista) { }

    protected override PresentadorTuplaMovimiento ObtenerValoresTupla(Movimiento entidad) {
        var presentadorTupla = new PresentadorTuplaMovimiento(new VistaTuplaMovimiento(), entidad);
        var tipoMovimiento = RepoTipoMovimiento.Instancia.ObtenerPorId(entidad.IdTipoMovimiento);

        presentadorTupla.Vista.Id = entidad.Id.ToString();
        presentadorTupla.Vista.NombreProducto = UtilesProducto.ObtenerNombreProducto(entidad.IdProducto).Result ?? string.Empty;
        presentadorTupla.Vista.NombreAlmacenOrigen = UtilesAlmacen.ObtenerNombreAlmacen(entidad.IdAlmacenOrigen) ?? string.Empty;
        presentadorTupla.Vista.ActualizarIconoStock(tipoMovimiento?.Efecto ?? EfectoMovimiento.Ninguno);
        presentadorTupla.Vista.NombreAlmacenDestino = UtilesAlmacen.ObtenerNombreAlmacen(entidad.IdAlmacenDestino) ?? string.Empty;
        presentadorTupla.Vista.SaldoInicial = entidad.SaldoInicial.ToString("N2", CultureInfo.InvariantCulture);
        presentadorTupla.Vista.CantidadMovida = entidad.CantidadMovida.ToString("N2", CultureInfo.InvariantCulture);
        presentadorTupla.Vista.SaldoFinal = entidad.SaldoFinal.ToString("N2", CultureInfo.InvariantCulture);
        presentadorTupla.Vista.TipoMovimiento = tipoMovimiento?.Nombre ?? string.Empty;
        presentadorTupla.Vista.Fecha = entidad.FechaCreacion.ToString("yyyy-MM-dd");

        return presentadorTupla;
    }
}