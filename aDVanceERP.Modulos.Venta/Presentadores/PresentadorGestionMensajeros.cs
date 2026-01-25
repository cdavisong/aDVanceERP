using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;
using aDVanceERP.Modulos.Venta.Vistas;

namespace aDVanceERP.Modulos.Venta.Presentadores;

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

    protected override PresentadorTuplaMensajero ObtenerValoresTupla(Mensajero entidad, List<IEntidadBaseDatos> entidadesExtra) {
        var presentadorTupla = new PresentadorTuplaMensajero(new VistaTuplaMensajero(), entidad);
        var persona = entidadesExtra.Count > 0 ? entidadesExtra.FirstOrDefault() as Persona : null!;
        var telefonos = RepoTelefonoContacto.Instancia.Buscar(FiltroBusquedaTelefonoContacto.IdPersona, persona?.Id.ToString() ?? "0").resultadosBusqueda.Select(t => t.entidadBase);

        presentadorTupla.Vista.Id = entidad.Id;
        presentadorTupla.Vista.CodigoMensajero = entidad.CodigoMensajero;
        presentadorTupla.Vista.NombreCompleto = persona?.NombreCompleto ?? "N/A";
        presentadorTupla.Vista.Telefonos = string.Concat(telefonos.Select(t => $"{t.PrefijoPais} {t.NumeroTelefono}, ")).TrimEnd(',', ' ');
        presentadorTupla.Vista.MatriculaVehiculo = entidad.MatriculaVehiculo;
        presentadorTupla.Vista.Activo = entidad.Activo;

        return presentadorTupla;
    }
}