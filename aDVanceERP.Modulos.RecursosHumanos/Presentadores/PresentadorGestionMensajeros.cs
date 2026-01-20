using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
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
        RegistrarEntidad += OnRegistrarMensajero;
        EditarEntidad += OnEditarMensajero;

        AgregadorEventos.Suscribir("MostrarVistaGestionMensajeros", OnMostrarVistaGestionMensajeros);
        AgregadorEventos.Suscribir("ActivarDesactivarMensajero", OnActivarDesactivarMensajero);
    }

    private void OnRegistrarMensajero(object? sender, EventArgs e) {
        AgregadorEventos.Publicar("MostrarVistaRegistroMensajero", string.Empty);
    }

    private void OnEditarMensajero(object? sender, Mensajero e) {
        AgregadorEventos.Publicar("MostrarVistaEdicionMensajero", AgregadorEventos.SerializarPayload(e));
    }

    private void OnMostrarVistaGestionMensajeros(string obj) {
        Vista.CargarFiltrosBusqueda(UtilesBusquedaCliente.FiltroBusquedaCliente);
        Vista.Restaurar();
        Vista.Mostrar();

        ActualizarResultadosBusqueda();
    }

    private void OnActivarDesactivarMensajero(string obj) {
        var idMensajeroSeleccionado = _tuplasEntidades.FirstOrDefault(t => t.EstadoSeleccion)?.Vista.Id ?? 0;

        if (idMensajeroSeleccionado != 0) {
            var estado = RepoMensajero.Instancia.HabilitarDeshabilitarMensajero(idMensajeroSeleccionado);

            ActualizarResultadosBusqueda();

            CentroNotificaciones.Mostrar($"El mensajero ha sido {(estado ? "activado" : "desactivado")} satisfactoriamente.", TipoNotificacion.Info);
        }
    }

    protected override PresentadorTuplaMensajero ObtenerValoresTupla(Mensajero objeto, List<IEntidadBaseDatos> entidadesExtra) {
        var presentadorTupla = new PresentadorTuplaMensajero(new VistaTuplaMensajero(), objeto);

        

        return presentadorTupla;
    }
}