using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Vistas.CuentaUsuario;
using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Presentadores.CuentaUsuario;

public class PresentadorGestionCuentasUsuarios : PresentadorVistaGestion<PresentadorTuplaCuentaUsuario, IVistaGestionCuentasUsuarios, IVistaTuplaCuentaUsuario, Core.Modelos.Modulos.Seguridad.CuentaUsuario, RepoCuentaUsuario, FiltroBusquedaCuentaUsuario> {
    public PresentadorGestionCuentasUsuarios(IVistaGestionCuentasUsuarios vista) : base(vista) {
        vista.AprobarSolicitudCuenta += OnAprobarSolicitudCuentaUsuario;

        RegistrarEntidad += OnRegistrarCuentaUsuario;
        EditarEntidad += OnEditarCuentaUsuario;

        AgregadorEventos.Suscribir("MostrarVistaGestionCuentasUsuarios", OnMostrarVistaGestionCuentasUsuarios);
    }

    private void OnRegistrarCuentaUsuario(object? sender, EventArgs e) {
        AgregadorEventos.Publicar("MostrarVistaRegistroCuentaUsuario", string.Empty);
        Vista.HabilitarBtnAprobacionSolicitudCuenta = false;
    }

    private void OnEditarCuentaUsuario(object? sender, Core.Modelos.Modulos.Seguridad.CuentaUsuario e) {
        AgregadorEventos.Publicar("MostrarVistaEdicionCuentaUsuario", AgregadorEventos.SerializarPayload(e));
        Vista.HabilitarBtnAprobacionSolicitudCuenta = false;
    }

    private void OnMostrarVistaGestionCuentasUsuarios(string obj) {
        Vista.CargarFiltrosBusqueda(UtilesBusquedaCuentaUsuario.FiltroBusquedaCuentaUsuario);
        Vista.Restaurar();
        Vista.Mostrar();

        ActualizarResultadosBusqueda();
    }

    protected override PresentadorTuplaCuentaUsuario ObtenerValoresTupla(Core.Modelos.Modulos.Seguridad.CuentaUsuario entidad) {
        var presentadorTupla = new PresentadorTuplaCuentaUsuario(new VistaTuplaCuentaUsuario(), entidad);

        presentadorTupla.Vista.Id = entidad.Id.ToString();
        presentadorTupla.Vista.NombreUsuario = entidad.Nombre;
        presentadorTupla.Vista.NombreRolUsuario = entidad.NombreRolUsuario ?? "No asignado";
        presentadorTupla.Vista.EstadoCuentaUsuario = entidad.Aprobado ? "Activa" : "Esperando aprobación";
        presentadorTupla.EntidadSeleccionada += OnCambioSeleccionEntidad;
        presentadorTupla.EntidadDeseleccionada += OnCambioSeleccionEntidad;

        return presentadorTupla;
    }

    private void OnAprobarSolicitudCuentaUsuario(object? sender, EventArgs e) {
        var tuplaSeleccionada = _tuplasEntidades.FirstOrDefault(t => t.EstadoSeleccion);

        if (tuplaSeleccionada == null) {
            CentroNotificaciones.Mostrar("No se ha seleccionado ninguna cuenta de usuario para aprobar.", TipoNotificacion.Advertencia);
            return;
        }

        if (tuplaSeleccionada.Entidad.IdRolUsuario == 0) {
            CentroNotificaciones.Mostrar("El usuario seleccionado no tiene un rolUsuario asignado, por lo que no se puede aprobar la solicitud de cuenta. Por favor, edite el usuario para asignarle un rol.", TipoNotificacion.Advertencia);
            return;
        }

        tuplaSeleccionada.Entidad.Aprobado = true;
        Repositorio.Editar(tuplaSeleccionada.Entidad);
        Vista.HabilitarBtnAprobacionSolicitudCuenta = false;

        ActualizarResultadosBusqueda();

        CentroNotificaciones.Mostrar("La cuenta de usuario ha sido aprobada correctamente.", TipoNotificacion.Info);
    }

    private void OnCambioSeleccionEntidad(object? sender, Core.Modelos.Modulos.Seguridad.CuentaUsuario e) {
        var tuplaSeleccionada = _tuplasEntidades.FirstOrDefault(t => t.EstadoSeleccion);

        if (tuplaSeleccionada != null && !tuplaSeleccionada.Entidad.Aprobado) {
            Vista.HabilitarBtnAprobacionSolicitudCuenta = true;
        } else {
            Vista.HabilitarBtnAprobacionSolicitudCuenta = false;
        }
    }

    protected override void Dispose(bool disposing) {
        Vista.AprobarSolicitudCuenta -= OnAprobarSolicitudCuentaUsuario;

        RegistrarEntidad -= OnRegistrarCuentaUsuario;
        EditarEntidad -= OnEditarCuentaUsuario;

        AgregadorEventos.Desuscribir("MostrarVistaGestionCuentasUsuarios", OnMostrarVistaGestionCuentasUsuarios);

        base.Dispose(disposing);
    }
}