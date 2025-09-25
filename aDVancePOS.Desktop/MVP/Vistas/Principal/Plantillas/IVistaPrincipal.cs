using aDVanceERP.Core.MVP.Vistas.Plantillas;

namespace aDVancePOS.Desktop.MVP.Vistas.Principal.Plantillas;

public interface IVistaPrincipal : IVistaContenedorMenu {
    bool BtnSubmenuUsuarioDisponible { get; set; }

    event EventHandler? VerNotificaciones;
    event EventHandler? VerMensajes;
    event EventHandler? SubMenuUsuario;
}