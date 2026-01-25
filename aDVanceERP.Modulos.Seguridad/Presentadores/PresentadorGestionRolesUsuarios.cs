using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Interfaces;
using aDVanceERP.Modulos.Seguridad.Vistas;

namespace aDVanceERP.Modulos.Seguridad.Presentadores {
    public class PresentadorGestionRolesUsuarios : PresentadorVistaGestion<PresentadorTuplaRolUsuario, IVistaGestionRolesUsuarios, IVistaTuplaRolUsuario, RolUsuario, RepoRolUsuario, FiltroBusquedaRolUsuario> {
        public PresentadorGestionRolesUsuarios(IVistaGestionRolesUsuarios vista) : base(vista) {
            RegistrarEntidad += OnRegistrarRolUsuario;
            EditarEntidad += OnEditarRolUsuario;

            AgregadorEventos.Suscribir("MostrarVistaGestionRolesUsuarios", OnMostrarVistaGestionRolesUsuarios);
        }

        private void OnRegistrarRolUsuario(object? sender, EventArgs e) {
            AgregadorEventos.Publicar("MostrarVistaRegistroRolUsuario", string.Empty);
        }

        private void OnEditarRolUsuario(object? sender, RolUsuario e) {
            AgregadorEventos.Publicar("MostrarVistaEdicionRolUsuario", AgregadorEventos.SerializarPayload(e));
        }

        private void OnMostrarVistaGestionRolesUsuarios(string obj) {
            Vista.CargarFiltrosBusqueda(UtilesBusquedaRolUsuario.FiltroBusquedaRolUsuario);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        protected override PresentadorTuplaRolUsuario ObtenerValoresTupla(RolUsuario entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaRolUsuario(new VistaTuplaRolUsuario(), entidad);
        
            presentadorTupla.Vista.Id = entidad.Id.ToString();
            presentadorTupla.Vista.NombreRolUsuario = entidad.Nombre;
            presentadorTupla.Vista.CantidadPermisos = entidad.Nombre?.Equals("Administrador") ?? false
                ? "TODOS"
                : entidad.CantidadPermisos.ToString();
            presentadorTupla.Vista.CantidadUsuarios = entidad.CantidadUsuariosAsignados.ToString();

            return presentadorTupla;
        }
    }
}