using aDVanceERP.Core.Mensajes.MVP.Modelos;
using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Vistas.CuentaUsuario;
using aDVanceERP.Modulos.Seguridad.Vistas.CuentaUsuario.Plantillas;
using aDVanceERP.Core.Eventos;

namespace aDVanceERP.Modulos.Seguridad.Presentadores.CuentaUsuario;

public class PresentadorGestionCuentasUsuarios : PresentadorVistaGestion<PresentadorTuplaCuentaUsuario,
    IVistaGestionCuentasUsuarios, IVistaTuplaCuentaUsuario, Core.Modelos.Modulos.Seguridad.CuentaUsuario, RepoCuentaUsuario,
    FiltroBusquedaCuentaUsuario> {
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
        var rolUsuario = RepoRolUsuario.Instancia.ObtenerPorId(entidad.IdRolUsuario);

        presentadorTupla.Vista.Id = entidad.Id.ToString();
        presentadorTupla.Vista.NombreUsuario = entidad.Nombre;
        presentadorTupla.Vista.NombreRolUsuario = rolUsuario?.Nombre ?? "No asignado";
        presentadorTupla.Vista.EstadoCuentaUsuario = entidad.Aprobado ? "Activa" : "Esperando aprobación";
        presentadorTupla.EntidadSeleccionada += OnCambioSeleccionObjeto;
        presentadorTupla.EntidadDeseleccionada += OnCambioSeleccionObjeto;

        return presentadorTupla;
    }

    private void OnAprobarSolicitudCuentaUsuario(object? sender, EventArgs e) {
        var usuariosRol0 = 0;

        foreach (var tupla in _tuplasEntidades)
            if (tupla.EstadoSeleccion) {
                if (tupla.Entidad.IdRolUsuario != 0) {
                    tupla.Entidad.Aprobado = true;

                    // Editar la cuenta de usuario
                    Repositorio.Editar(tupla.Entidad);
                } else {
                    usuariosRol0++;
                }

                break;
            }

        if (usuariosRol0 > 0) {
            CentroNotificaciones.Mostrar(
                $"{(usuariosRol0 <= 1 ? "El usuario" : "Existen usuarios")} seleccionado{(usuariosRol0 == 1 ? "" : "s")} {(usuariosRol0 == 1 ? "no tiene" : "sin")} un rolUsuario asignado, por lo que no se puede aprobar la solicitud de cuenta. Por favor, edite el usuario para asignarle un rol.",
                TipoNotificacion.Advertencia);
            return;
        }

        Vista.HabilitarBtnAprobacionSolicitudCuenta = false;

        ActualizarResultadosBusqueda();
    }

    private void OnCambioSeleccionObjeto(object? sender, Core.Modelos.Modulos.Seguridad.CuentaUsuario e) {
        if (_tuplasEntidades.Any(t => t.EstadoSeleccion)) {
            foreach (var tupla in _tuplasEntidades)
                if (tupla.EstadoSeleccion) {
                    if (!tupla.Entidad.Aprobado) {
                        Vista.HabilitarBtnAprobacionSolicitudCuenta = true;
                    } else {
                        Vista.HabilitarBtnAprobacionSolicitudCuenta = false;
                        return;
                    }
                }
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