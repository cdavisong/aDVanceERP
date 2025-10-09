using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Interfaces;

public interface IVistaGestionCuentasUsuarios : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaCuentaUsuario>, INavegadorTuplasEntidades {
    bool MostrarBtnAprobacionSolicitudCuenta { get; set; }

    event EventHandler? AprobarSolicitudCuenta;
}