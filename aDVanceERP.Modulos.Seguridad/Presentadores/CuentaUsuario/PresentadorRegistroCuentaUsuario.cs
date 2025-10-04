using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Helpers;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Vistas.CuentaUsuario.Plantillas;

namespace aDVanceERP.Modulos.Seguridad.Presentadores.CuentaUsuario;

public class PresentadorRegistroCuentaUsuario : PresentadorVistaRegistro<IVistaRegistroCuentaUsuario, Core.Modelos.Modulos.Seguridad.CuentaUsuario,
    RepoCuentaUsuario, FiltroBusquedaCuentaUsuario> {
    public PresentadorRegistroCuentaUsuario(IVistaRegistroCuentaUsuario vista) : base(vista) {
        AgregadorEventos.Suscribir("MostrarVistaRegistroCuentaUsuario", OnMostrarVistaRegistroCuentaUsuario);
        AgregadorEventos.Suscribir("MostrarVistaEdicionCuentaUsuario", OnMostrarVistaEdicionCuentaUsuario);
    }

    private void OnMostrarVistaRegistroCuentaUsuario(string obj) {
        Vista.Restaurar();
        Vista.Mostrar();
    }

    private void OnMostrarVistaEdicionCuentaUsuario(string obj) {
        if (string.IsNullOrEmpty(obj))
            return;

        var cuentaUsuario = AgregadorEventos.DeserializarPayload<Core.Modelos.Modulos.Seguridad.CuentaUsuario>(obj);

        if (cuentaUsuario == null)
            return;

        Vista.Restaurar();

        PopularVistaDesdeEntidad(cuentaUsuario);

        Vista.Mostrar();
    }

    public override void PopularVistaDesdeEntidad(Core.Modelos.Modulos.Seguridad.CuentaUsuario entidad) {
        var rolUsuario = RepoRolUsuario.Instancia.ObtenerPorId(entidad.IdRolUsuario);

        Vista.NombreUsuario = entidad.Nombre;
        Vista.NombreRolUsuario = rolUsuario?.Nombre ?? "No asignado";
        Vista.ModoEdicion = true;

        _entidad = entidad;
    }

    protected override Core.Modelos.Modulos.Seguridad.CuentaUsuario? ObtenerEntidadDesdeVista() {
        var passwordSeguro = SecureStringHelper.HashPassword(Vista.Password);
        var rolUsuario = RepoRolUsuario.Instancia.Buscar(FiltroBusquedaRolUsuario.Nombre, Vista.NombreRolUsuario).entidades.FirstOrDefault();

        return new Core.Modelos.Modulos.Seguridad.CuentaUsuario(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.NombreUsuario,
            passwordSeguro.hash,
            passwordSeguro.salt,
            rolUsuario?.Id ?? 0
        ) {
            Aprobado = Entidad?.Aprobado ?? false
        };
    }
}