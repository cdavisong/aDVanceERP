using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Interfaces {
    public interface IVistaRegistroRolUsuario : IVistaRegistro {
        string NombreRolUsuario { get; set; }
        string[] NombresModulos { get; set; }
        string[] NombresPermisos { get; set; }
        List<Core.Modelos.Modulos.Seguridad.Permiso> Permisos { get; }

        public event EventHandler<string>? RegistrarPermiso;
        public event EventHandler<string> CambioModulo;

        RepoVistaBase PanelCentral { get; }
    }
}