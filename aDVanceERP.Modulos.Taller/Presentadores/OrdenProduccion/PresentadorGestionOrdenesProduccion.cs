
using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Taller.Interfaces;
using aDVanceERP.Modulos.Taller.Modelos;
using aDVanceERP.Modulos.Taller.Repositorios;
using aDVanceERP.Modulos.Taller.Vistas.OrdenProduccion;

using System.Globalization;

namespace aDVanceERP.Modulos.Taller.Presentadores.OrdenProduccion {
    public class PresentadorGestionOrdenesProduccion : PresentadorVistaGestion<PresentadortuplaOrdenProduccion, IVistaGestionOrdenesProduccion, IVistaTuplaOrdenProduccion, Modelos.OrdenProduccion, RepoOrdenProduccion, FiltroBusquedaOrdenProduccion> {
        public PresentadorGestionOrdenesProduccion(IVistaGestionOrdenesProduccion vista) : base(vista) {
            vista.CerrarOrdenProduccionSeleccionada += OnCerrarOrdenProduccionSeleccionada;
            vista.EditarEntidad += delegate {
                Vista.HabilitarBtnCierreOrdenProduccion = false;
            };
        }

        public event EventHandler<Modelos.OrdenProduccion> OrdenProduccionCerrada;

        protected override PresentadortuplaOrdenProduccion ObtenerValoresTupla(Modelos.OrdenProduccion entidad) {
            var presentadorTupla = new PresentadortuplaOrdenProduccion(new VistaTuplaOrdenProduccion(), entidad);

            presentadorTupla.Vista.Id = entidad.Id.ToString();
            presentadorTupla.Vista.NumeroOrden = entidad.NumeroOrden;
            presentadorTupla.Vista.FechaApertura = entidad.FechaApertura.ToString("yyyy-MM-dd");
            presentadorTupla.Vista.NombreProducto = entidad.NombreProducto;
            presentadorTupla.Vista.TotalUnidadesProducidas = entidad.Cantidad.ToString(CultureInfo.InvariantCulture);
            presentadorTupla.Vista.CostoTotal = entidad.CostoTotal.ToString("N2", CultureInfo.InvariantCulture);
            presentadorTupla.Vista.PrecioUnitario = entidad.PrecioUnitario.ToString("N2", CultureInfo.InvariantCulture);
            presentadorTupla.Vista.Estado = (int) entidad.Estado;
            presentadorTupla.Vista.FechaCierre = entidad.FechaCierre.HasValue ? !entidad.FechaCierre.Equals(DateTime.MinValue) ? entidad.FechaCierre.Value.ToString("yyyy-MM-dd") : "-" : "-";
            presentadorTupla.EntidadSeleccionada += CambiarVisibilidadBtnCierreOrdenProduccion;
            presentadorTupla.EntidadDeseleccionada += CambiarVisibilidadBtnCierreOrdenProduccion;

            return presentadorTupla;
        }

        private void OnCerrarOrdenProduccionSeleccionada(object? sender, EventArgs e) {
            if (_tuplasEntidades.Any(t => t.EstadoSeleccion)) {
                foreach (var tupla in _tuplasEntidades) {
                    if (tupla.EstadoSeleccion) {
                        tupla.Entidad.Estado = EstadoOrdenProduccion.Cerrada;
                        tupla.Entidad.FechaCierre = DateTime.Now;

                        // Editar la orden de producción
                        Repositorio.Editar(tupla.Entidad);

                        // Invocar evento de cierre para la orden de produccion y registrar los movimientos correspondientes
                        OrdenProduccionCerrada?.Invoke(this, tupla.Entidad);

                        break;
                    }
                }

                ActualizarResultadosBusqueda();
            } else {
                CentroNotificaciones.Mostrar("Debe seleccionar una orden de producción para cerrar.", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
            }
        }

        public override void ActualizarResultadosBusqueda() {
            // Cambiar la visibilidad del botón de cierre de orden de producción al refrescar la lista de objetos.
            Vista.HabilitarBtnCierreOrdenProduccion = false;

            base.ActualizarResultadosBusqueda();
        }

        private void CambiarVisibilidadBtnCierreOrdenProduccion(object? sender, EventArgs e) {
            if (_tuplasEntidades.Any(t => t.EstadoSeleccion)) {
                foreach (var tupla in _tuplasEntidades) {
                    if (tupla.EstadoSeleccion) {
                        var ordenProduccion = tupla.Entidad;

                        if (ordenProduccion != null && ordenProduccion.Estado != EstadoOrdenProduccion.Cerrada) {
                            Vista.HabilitarBtnCierreOrdenProduccion = true;
                            return;
                        }
                    }
                }
            } else {
                Vista.HabilitarBtnCierreOrdenProduccion = false;
            }
        }
    }
}