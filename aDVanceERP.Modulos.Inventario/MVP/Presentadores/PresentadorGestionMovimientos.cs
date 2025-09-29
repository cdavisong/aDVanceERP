using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;

using aDVanceERP.Modulos.Inventario.MVP.Vistas.Movimiento;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Movimiento.Plantillas;

using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.MVP.Presentadores;

public class PresentadorGestionMovimientos : PresentadorVistaGestion<PresentadorTuplaMovimiento, IVistaGestionMovimientos
    , IVistaTuplaMovimiento, Movimiento, RepoMovimiento, FiltroBusquedaMovimiento> {
    public PresentadorGestionMovimientos(IVistaGestionMovimientos vista) : base(vista) { }

    protected override PresentadorTuplaMovimiento ObtenerValoresTupla(Movimiento entidad) {
        var presentadorTupla = new PresentadorTuplaMovimiento(new VistaTuplaMovimiento(), entidad);

        presentadorTupla.Vista.Id = entidad.Id.ToString();
        presentadorTupla.Vista.NombreProducto = entidad.NombreProducto;
        presentadorTupla.Vista.NombreAlmacenOrigen = entidad.NombreAlmacenOrigen;
        presentadorTupla.Vista.ActualizarIconoStock(entidad.EfectoMovimiento);
        presentadorTupla.Vista.NombreAlmacenDestino = entidad.NombreAlmacenDestino;
        presentadorTupla.Vista.SaldoInicial = entidad.SaldoInicial.ToString("N2", CultureInfo.InvariantCulture);
        presentadorTupla.Vista.CantidadMovida = entidad.CantidadMovida.ToString("N2", CultureInfo.InvariantCulture);
        presentadorTupla.Vista.SaldoFinal = entidad.SaldoFinal.ToString("N2", CultureInfo.InvariantCulture);
        presentadorTupla.Vista.TipoMovimiento = entidad.NombreTipoMovimiento;
        presentadorTupla.Vista.Fecha = entidad.FechaCreacion.ToString("yyyy-MM-dd");

        return presentadorTupla;
    }
}