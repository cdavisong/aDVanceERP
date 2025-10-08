using aDVanceERP.Core.Modelos.Modulos.Finanzas;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Finanzas;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja.Plantillas;

using System.Globalization;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;

namespace aDVanceERP.Modulos.Finanzas.MVP.Presentadores {
    public class PresentadorGestionCajas : PresentadorVistaGestion<PresentadorTuplaCaja, IVistaGestionCajas, IVistaTuplaCaja, Caja, RepoCaja, FiltroBusquedaCaja> {
        public PresentadorGestionCajas(IVistaGestionCajas vista) 
            : base(vista) {
            vista.CerrarCajaSeleccionada += CerrarCajaSeleccionada;
            vista.EditarEntidad += delegate {
                Vista.HabilitarBtnRegistroMovimientoCaja = false;
                Vista.HabilitarBtnCierreCaja = false;
            };
        }        

        protected override PresentadorTuplaCaja ObtenerValoresTupla(Caja objeto) {
            var presentadorTupla = new PresentadorTuplaCaja(new VistaTuplaCaja(), objeto);
            var cuentaUsuario = RepoCuentaUsuario.Instancia.ObtenerPorId(objeto.IdCuentaUsuario);

            presentadorTupla.Vista.Id = objeto.Id.ToString();
            presentadorTupla.Vista.FechaApertura = objeto.FechaApertura.ToString("yyyy-MM-dd HH:mm");
            presentadorTupla.Vista.SaldoInicial = objeto.SaldoInicial.ToString("N2", CultureInfo.InvariantCulture);
            presentadorTupla.Vista.CantidadMovimientos = UtilesCaja.ObtenerCantidadMovimientos(objeto.Id).ToString();
            presentadorTupla.Vista.SaldoActual = objeto.SaldoActual.ToString("N2", CultureInfo.InvariantCulture);
            presentadorTupla.Vista.FechaCierre = objeto.FechaCierre != DateTime.MinValue ? objeto.FechaCierre.ToString("yyyy-MM-dd HH:mm") : "-";
            presentadorTupla.Vista.Estado = (int) objeto.Estado;
            presentadorTupla.Vista.NombreUsuario = cuentaUsuario?.Nombre ?? string.Empty;
            presentadorTupla.EntidadSeleccionada += CambiarVisibilidadBotones;
            presentadorTupla.EntidadDeseleccionada += CambiarVisibilidadBotones;
            
            return presentadorTupla;
        }

        public override void ActualizarResultadosBusqueda() {
            // Cambiar la visibilidad de los botones 
            Vista.HabilitarBtnRegistroMovimientoCaja = false;
            Vista.HabilitarBtnCierreCaja = false;            

            base.ActualizarResultadosBusqueda();
        }

        private void CerrarCajaSeleccionada(object? sender, EventArgs e) {
            foreach (var tupla in _tuplasEntidades)
                if (tupla.EstadoSeleccion) {
                    tupla.Entidad.FechaCierre = DateTime.Now;
                    tupla.Entidad.Estado = EstadoCaja.Cerrada;

                    // Editar la venta del producto
                    Repositorio.Editar(tupla.Entidad);

                    break;
                }

            ActualizarResultadosBusqueda();
        }

        private void CambiarVisibilidadBotones(object? sender, Caja e) {
            // 1. Filtrar primero las tuplas seleccionadas para evitar procesamiento innecesario
            var tuplaSeleccionada = _tuplasEntidades.Where(t => t.EstadoSeleccion).FirstOrDefault();

            // 2. Actualizar la visibilidad de botones            
            Vista.HabilitarBtnCierreCaja = tuplaSeleccionada != null && tuplaSeleccionada.Entidad.Estado == EstadoCaja.Abierta;
            Vista.HabilitarBtnRegistroMovimientoCaja = tuplaSeleccionada != null && tuplaSeleccionada.Entidad.Estado == EstadoCaja.Abierta;
        }
    }
}
