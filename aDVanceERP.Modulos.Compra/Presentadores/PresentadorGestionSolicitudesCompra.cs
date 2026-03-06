using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Compra;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.Compra.Interfaces;
using aDVanceERP.Modulos.Compra.Vistas;

namespace aDVanceERP.Modulos.Compra.Presentadores {
    internal class PresentadorGestionSolicitudesCompra : PresentadorVistaGestion<PresentadorTuplaSolicitudCompra, IVistaGestionSolicitudesCompra, IVistaTuplaSolicitudCompra, SolicitudCompra, RepoSolicitudCompra, FiltroBusquedaSolicitudCompra> {
        public PresentadorGestionSolicitudesCompra(IVistaGestionSolicitudesCompra vista) : base(vista) {
            RegistrarEntidad += OnRegistrarSolicitudCompra;
            EditarEntidad += OnEditarSolicitudCompra;

            AgregadorEventos.Suscribir("MostrarVistaGestionSolicitudesCompra", OnMostrarVistaGestionSolicitudesCompra);
        }

        private void OnRegistrarSolicitudCompra(object? sender, EventArgs e) {
            AgregadorEventos.Publicar("MostrarVistaRegistroSolicitudCompra", string.Empty);
        }

        private void OnEditarSolicitudCompra(object? sender, SolicitudCompra e) {
            AgregadorEventos.Publicar("MostrarVistaEdicionSolicitudCompra", AgregadorEventos.SerializarPayload(e));
        }

        private void OnMostrarVistaGestionSolicitudesCompra(string obj) {
            Vista.CargarFiltrosBusqueda(UtilesBusquedaSolicitudCompra.Filtros);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        public override void ActualizarResultadosBusqueda() {
            if (FiltroBusqueda == FiltroBusquedaSolicitudCompra.Todos && (CriteriosBusqueda == null || CriteriosBusqueda.Length == 0))
                CriteriosBusqueda = [string.Empty];

            base.ActualizarResultadosBusqueda();
        }

        protected override PresentadorTuplaSolicitudCompra ObtenerValoresTupla(SolicitudCompra entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaSolicitudCompra(new VistaTuplaSolicitudCompra(), entidad);
            var cuentaUsuario = RepoCuentaUsuario.Instancia.ObtenerPorId(entidad.IdSolicitante);
            var persona = RepoPersona.Instancia.ObtenerPorId(cuentaUsuario?.IdPersona ?? 0);
            var detallesSolicitud = RepoDetalleSolicitudCompra.Instancia.Buscar(FiltroBusquedaDetalleSolicitudCompra.IdSolicitudCompra, entidad.Id.ToString()).resultadosBusqueda.Select(r => r.entidadBase).ToList();

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.Codigo = entidad.Codigo;
            presentadorTupla.Vista.NombreSolicitante = persona != null ? persona.NombreCompleto : cuentaUsuario != null ? $"Cuenta de usuario: {cuentaUsuario.Nombre}" : "Desconocido";
            presentadorTupla.Vista.FechaSolicitud = entidad.FechaSolicitud;
            presentadorTupla.Vista.FechaRequerida = entidad.FechaRequerida ?? DateTime.MinValue;
            presentadorTupla.Vista.Observaciones = entidad.Observaciones;
            presentadorTupla.Vista.ImporteTotal = detallesSolicitud.Sum(d => d.Subtotal);
            presentadorTupla.Vista.Estado = entidad.Estado;
            presentadorTupla.Vista.Activo = entidad.Activo;
            presentadorTupla.Vista.CambioEstadoSolicitudCompra += OnCambioEstadoSolicitudCompra;

            return presentadorTupla;
        }

        private void OnCambioEstadoSolicitudCompra(object? sender, (long idSolicitudCompra, EstadoSolicitudCompraEnum estado) e) {
            var entidad = RepoSolicitudCompra.Instancia.ObtenerPorId(e.idSolicitudCompra);
            var cuentaUsuario = RepoCuentaUsuario.Instancia.ObtenerPorId(entidad?.IdSolicitante);

            if (e.estado == EstadoSolicitudCompraEnum.Aprobada)
                RepoSolicitudCompra.Instancia.AprobarSolicitud(e.idSolicitudCompra, cuentaUsuario?.Id ?? 0);
            else if (e.estado == EstadoSolicitudCompraEnum.Rechazada)
                RepoSolicitudCompra.Instancia.RechazarSolicitud(e.idSolicitudCompra, "Solicitud de compra rechazada");
        }
    }
}