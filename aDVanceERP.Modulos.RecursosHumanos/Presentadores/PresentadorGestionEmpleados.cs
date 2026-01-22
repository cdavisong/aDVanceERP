using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.RecursosHumanos;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;
using aDVanceERP.Modulos.RecursosHumanos.Vistas;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores {
    public class PresentadorGestionEmpleados : PresentadorVistaGestion<PresentadorTuplaEmpleado, IVistaGestionEmpleados, IVistaTuplaEmpleado, Empleado, RepoEmpleado, FiltroBusquedaEmpleado> {
        public PresentadorGestionEmpleados(IVistaGestionEmpleados vista) : base(vista) {
            RegistrarEntidad += OnRegistrarEmpleado;
            EditarEntidad += OnEditarEmpleado;

            AgregadorEventos.Suscribir("MostrarVistaGestionEmpleados", OnMostrarVistaGestionEmpleados);
            AgregadorEventos.Suscribir("ActivarDesactivarEmpleado", OnActivarDesactivarEmpleado);
        }

        private void OnRegistrarEmpleado(object? sender, EventArgs e) {
            AgregadorEventos.Publicar("MostrarVistaRegistroEmpleado", string.Empty);
        }

        private void OnEditarEmpleado(object? sender, Empleado e) {
            AgregadorEventos.Publicar("MostrarVistaEdicionEmpleado", AgregadorEventos.SerializarPayload(e));
        }

        private void OnMostrarVistaGestionEmpleados(string obj) {
            Vista.CargarFiltrosBusqueda(UtilesBusquedaEmpleado.FiltroBusquedaEmpleado);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        private void OnActivarDesactivarEmpleado(string obj) {
            var idEmpleadoSeleccionado = _tuplasEntidades.FirstOrDefault(t => t.EstadoSeleccion)?.Vista.Id ?? 0;

            if (idEmpleadoSeleccionado != 0) {
                var estado = RepoEmpleado.Instancia.HabilitarDeshabilitarEmpleado(idEmpleadoSeleccionado);

                ActualizarResultadosBusqueda();

                CentroNotificaciones.Mostrar($"El empleado ha sido {(estado ? "activado" : "desactivado")} satisfactoriamente.", TipoNotificacion.Info);
            }
        }

        protected override PresentadorTuplaEmpleado ObtenerValoresTupla(Empleado entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaEmpleado(new VistaTuplaEmpleado(), entidad);
            var persona = entidadesExtra.Count > 0 ? entidadesExtra.FirstOrDefault() as Persona : null!;

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.CodigoEmpleado = entidad.CodigoEmpleado;
            presentadorTupla.Vista.NombreCompleto = persona?.NombreCompleto ?? "N/A";
            presentadorTupla.Vista.Cargo = entidad.Cargo;
            presentadorTupla.Vista.Departamento = entidad.Departamento;
            presentadorTupla.Vista.FechaContratacion = entidad.FechaContratacion.ToString("yyyy-MM-dd");
            presentadorTupla.Vista.Activo = entidad.Activo;

            return presentadorTupla;
        }
    }
}
