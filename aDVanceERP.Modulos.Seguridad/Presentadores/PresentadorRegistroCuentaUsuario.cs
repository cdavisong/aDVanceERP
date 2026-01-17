using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Helpers.Comun;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Presentadores;

public class PresentadorRegistroCuentaUsuario : PresentadorVistaRegistro<IVistaRegistroCuentaUsuario, Core.Modelos.Modulos.Seguridad.CuentaUsuario, RepoCuentaUsuario, FiltroBusquedaCuentaUsuario> {
    public PresentadorRegistroCuentaUsuario(IVistaRegistroCuentaUsuario vista) : base(vista) {
        AgregadorEventos.Suscribir("MostrarVistaRegistroCuentaUsuario", OnMostrarVistaRegistroCuentaUsuario);
        AgregadorEventos.Suscribir("MostrarVistaEdicionCuentaUsuario", OnMostrarVistaEdicionCuentaUsuario);
    }

    private void OnMostrarVistaRegistroCuentaUsuario(string obj) {
        Vista.ModoEdicion = false;

        // Carga inicial de datos
        Vista.CargarRolesUsuarios(RepoRolUsuario.Instancia.ObtenerTodos().Select(ru => ru.entidadBase.Nombre).ToArray());

        Vista.Restaurar();
        Vista.Mostrar();
    }

    private void OnMostrarVistaEdicionCuentaUsuario(string obj) {
        Vista.ModoEdicion = true;

        if (string.IsNullOrEmpty(obj))
            return;

        var cuentaUsuario = AgregadorEventos.DeserializarPayload<CuentaUsuario>(obj);

        if (cuentaUsuario == null)
            return;

        // Carga inicial de datos
        Vista.CargarRolesUsuarios([.. RepoRolUsuario.Instancia.ObtenerTodos().Select(ru => ru.entidadBase.Nombre)]);

        Vista.Restaurar();

        PopularVistaDesdeEntidad(cuentaUsuario);

        Vista.Mostrar();
    }

    public override void PopularVistaDesdeEntidad(CuentaUsuario entidad) {
        base.PopularVistaDesdeEntidad(entidad);

        Vista.NombreUsuario = entidad.Nombre;        
        Vista.NombreRolUsuario = entidad.NombreRolUsuario ?? string.Empty;
    }

    protected override CuentaUsuario? ObtenerEntidadDesdeVista() {
        var passwordSeguro = SecureStringHelper.HashPassword(Vista.Password);
        var rolUsuario = RepoRolUsuario.Instancia.Buscar(FiltroBusquedaRolUsuario.Nombre, Vista.NombreRolUsuario).resultadosBusqueda.FirstOrDefault().entidadBase;

        return new CuentaUsuario(
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