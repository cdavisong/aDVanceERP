using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Seguridad.MVP.Modelos;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Seguridad.MVP.Vistas.CuentaUsuario.Plantillas;

public interface IVistaGestionCuentasUsuarios : IVistaContenedor, IGestorEntidades,
    IBuscadorEntidades<FiltroBusquedaCuentaUsuario>, INavegadorTuplasEntidades {
    bool HabilitarBtnAprobacionSolicitudCuenta { get; set; }

    event EventHandler? AprobarSolicitudCuenta;
}