using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Interfaces;
using aDVanceERP.Modulos.Seguridad.Vistas;

namespace aDVanceERP.Modulos.Seguridad.Presentadores {
    public class PresentadorGestionCuentasUsuarios : PresentadorVistaGestion<PresentadorTuplaCuentaUsuario, IVistaGestionCuentasUsuarios, IVistaTuplaCuentaUsuario, CuentaUsuario, RepoCuentaUsuario, FiltroBusquedaCuentaUsuario> {
        public PresentadorGestionCuentasUsuarios(IVistaGestionCuentasUsuarios vista) : base(vista) {
            vista.RegistrarEntidad += OnRegistrarCuentaUsuario;

            AgregadorEventos.Suscribir<EventoMostrarVistaGestionCuentasUsuarios>(OnMostrarVistaGestionCuentasUsuarios);
        }

        private void OnMostrarVistaGestionCuentasUsuarios(EventoMostrarVistaGestionCuentasUsuarios e) {
            CargarDatosComunes();
            
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        private void OnRegistrarCuentaUsuario(object? sender, EventArgs e) {
            AgregadorEventos.Publicar(new EventoMostrarVistaRegistroCuentaUsuario());
        }

        private void CargarDatosComunes() {
            Vista.CargarFiltrosBusqueda([.. EnumExt.ObtenerNombresDescripciones<FiltroBusquedaCuentaUsuario>()]);
        }

        protected override PresentadorTuplaCuentaUsuario ObtenerValoresTupla(CuentaUsuario entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaCuentaUsuario(new VistaTuplaCuentaUsuario(), entidad);
            var persona = RepoPersona.Instancia.ObtenerPorId(entidad.IdPersona);
            var rol = RepoRol.Instancia.ObtenerPorId(entidad.IdRol);

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.NombrePersona = persona?.NombreCompleto ?? "N/A";
            presentadorTupla.Vista.NombreUsuario = entidad.Nombre;
            presentadorTupla.Vista.NombreRol = rol?.Nombre ?? "N/A";
            presentadorTupla.Vista.Email = string.IsNullOrEmpty(entidad.Email) ? "N/A" : entidad.Email;
            presentadorTupla.Vista.Administrador = entidad.Administrador;
            presentadorTupla.Vista.Aprobado = entidad.Aprobado;
            presentadorTupla.Vista.Estado = entidad.Estado;

            return presentadorTupla;
        }

        protected override void Dispose(bool disposing) {
            Vista.RegistrarEntidad -= OnRegistrarCuentaUsuario;

            AgregadorEventos.Desuscribir<EventoMostrarVistaGestionCuentasUsuarios>(OnMostrarVistaGestionCuentasUsuarios);

            base.Dispose(disposing);
        }
    }
}