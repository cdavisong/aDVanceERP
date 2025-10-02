using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.MVP.Presentadores;
using aDVanceERP.Modulos.Seguridad.MVP.Vistas.CuentaUsuario;
using aDVanceERP.Modulos.Seguridad.Utiles;
using aDVanceERP.Desktop.Utiles;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
    private PresentadorRegistroCuentaUsuario? _registroCuentaUsuario;

    private Task InicializarVistaRegistroCuentaUsuario() {
        _registroCuentaUsuario = new PresentadorRegistroCuentaUsuario(new VistaRegistroCuentaUsuario());
        _registroCuentaUsuario.Vista.CargarRolesUsuarios(UtilesRolUsuario.ObtenerNombresRolesUsuarios());
        _registroCuentaUsuario.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
        _registroCuentaUsuario.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
        _registroCuentaUsuario.EntidadRegistradaActualizada += delegate {
            if (_gestionCuentasUsuarios == null)
                return;

            _gestionCuentasUsuarios.Vista.HabilitarBtnAprobacionSolicitudCuenta = false;
            
            _gestionCuentasUsuarios.ActualizarResultadosBusqueda();
        };

        return Task.CompletedTask;
    }

    private async void MostrarVistaRegistroCuentaUsuario(object? sender, EventArgs e) {
        await InicializarVistaRegistroCuentaUsuario();

        if (_registroCuentaUsuario == null)
            return;

        _registroCuentaUsuario.Vista.Mostrar();
        _registroCuentaUsuario.Dispose();
    }

    private async void MostrarVistaEdicionCuentaUsuario(object? sender, EventArgs e) {
        await InicializarVistaRegistroCuentaUsuario();

        if (sender is CuentaUsuario cuentaUsuario) {
            if (_registroCuentaUsuario != null) {
                _registroCuentaUsuario.PopularVistaDesdeEntidad(cuentaUsuario);
                _registroCuentaUsuario.Vista.Mostrar();
            }
        }

        _registroCuentaUsuario?.Dispose();
    }
}