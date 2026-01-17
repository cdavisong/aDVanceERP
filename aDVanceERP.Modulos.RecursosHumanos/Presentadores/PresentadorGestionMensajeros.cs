using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;
using aDVanceERP.Modulos.RecursosHumanos.Vistas;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores;

public class PresentadorGestionMensajeros : PresentadorVistaGestion<PresentadorTuplaMensajero, IVistaGestionMensajeros,
    IVistaTuplaMensajero, Mensajero, RepoMensajero, FiltroBusquedaMensajero> {
    public PresentadorGestionMensajeros(IVistaGestionMensajeros vista) : base(vista) {
        vista.HabilitarDeshabilitarMensajero += IntercambiarHabilitacionMensajero;
        vista.EditarEntidad += delegate {
            Vista.MostrarBtnHabilitarDeshabilitarMensajero = false;
        };
    }

    protected override PresentadorTuplaMensajero ObtenerValoresTupla(Mensajero objeto, List<IEntidadBaseDatos> entidadesExtra) {
        var presentadorTupla = new PresentadorTuplaMensajero(new VistaTuplaMensajero(), objeto);

        

        return presentadorTupla;
    }

    public override void ActualizarResultadosBusqueda() {
        // Cambiar la visibilidad de los botones
        Vista.MostrarBtnHabilitarDeshabilitarMensajero = false;

        base.ActualizarResultadosBusqueda();
    }

    private void IntercambiarHabilitacionMensajero(object? sender, EventArgs e) {
        // 1. Filtrar primero las tuplas seleccionadas para evitar procesamiento innecesario
        var tuplasSeleccionadas = _tuplasEntidades.Where(t => t.EstadoSeleccion).ToList();

        if (!tuplasSeleccionadas.Any()) {
            Vista.MostrarBtnHabilitarDeshabilitarMensajero = false;
            return;
        }

        
    }

    private void CambiarVisibilidadBtnHabilitacionMensajero(object? sender, Mensajero e) {
        Vista.MostrarBtnHabilitarDeshabilitarMensajero = _tuplasEntidades.Any(t => t.EstadoSeleccion);
    }
}