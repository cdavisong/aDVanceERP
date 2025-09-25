using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Seguridad.MVP.Modelos;
using aDVanceERP.Core.Seguridad.MVP.Modelos.Repositorios;
using aDVanceERP.Core.Seguridad.MVP.Vistas.CuentaUsuario.Plantillas;
using aDVanceERP.Core.Seguridad.Utiles;

namespace aDVanceERP.Core.Seguridad.MVP.Presentadores;

public class PresentadorRegistroCuentaUsuario : PresentadorVistaRegistro<IVistaRegistroCuentaUsuario, CuentaUsuario,
    RepoCuentaUsuario, FiltroBusquedaCuentaUsuario> {
    public PresentadorRegistroCuentaUsuario(IVistaRegistroCuentaUsuario vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(CuentaUsuario objeto) {
        Vista.NombreUsuario = objeto.Nombre;
        Vista.NombreRolUsuario = UtilesRolUsuario.ObtenerNombreRolUsuario(objeto.IdRolUsuario);
        Vista.ModoEdicion = true;

        _entidad = objeto;
    }

    protected override CuentaUsuario? ObtenerEntidadDesdeVista() {
        var passwordSeguro = UtilesPassword.HashPassword(Vista.Password);

        return new CuentaUsuario(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.NombreUsuario,
            passwordSeguro.hash,
            passwordSeguro.salt,
            UtilesRolUsuario.ObtenerIdRolUsuario(Vista.NombreRolUsuario).Result
        ) {
            Aprobado = Entidad?.Aprobado ?? false
        };
    }
}