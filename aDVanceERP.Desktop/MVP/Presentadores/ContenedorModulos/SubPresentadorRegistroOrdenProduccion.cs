using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.Taller.Modelos;
using aDVanceERP.Modulos.Taller.Presentadores.OrdenProduccion;
using aDVanceERP.Modulos.Taller.Vistas.OrdenProduccion;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos {
    public partial class PresentadorModulos {
        private PresentadorRegistroOrdenProduccion? _registroOrdenProduccion;

        private void InicializarVistaRegistroOrdenProduccion() {
            _registroOrdenProduccion = new PresentadorRegistroOrdenProduccion(new VistaRegistroOrdenProduccion());
            _registroOrdenProduccion.EntidadRegistradaActualizada += delegate {
                if (_gestionOrdenesProduccion == null)
                    return;

                _gestionOrdenesProduccion.ActualizarResultadosBusqueda();
            };

            Vista.PanelCentral.Registrar(_registroOrdenProduccion.Vista);
        }

        private void MostrarVistaRegistroOrdenProduccion(object? sender, EventArgs e) {
            if (_registroOrdenProduccion == null)
                return;

            CargarCamposDinamicosVista();

            _registroOrdenProduccion.Vista.Restaurar();            
            _registroOrdenProduccion.Vista.Mostrar();
        }

        

        private void MostrarVistaEdicionOrdenProduccion(object? sender, EventArgs e) {
            if (sender is OrdenProduccion ordenProduccion) {
                if (_registroOrdenProduccion != null) {
                    CargarCamposDinamicosVista();

                    _registroOrdenProduccion.Vista.Restaurar();                    
                    _registroOrdenProduccion.Vista.Mostrar();
                    _registroOrdenProduccion.PopularVistaDesdeEntidad(ordenProduccion);
                }
            }
        }

        private void CargarCamposDinamicosVista() {
            _registroOrdenProduccion.Vista.CargarNombresAlmacenesMateriales(UtilesAlmacen.ObtenerNombresAlmacenes());
            _registroOrdenProduccion.Vista.CargarNombresAlmacenesDestino(UtilesAlmacen.ObtenerNombresAlmacenes());
            _registroOrdenProduccion.Vista.CargarNombresActividadesProduccion([.. UtilesOrdenProduccion.ObtenerNombresActividadesUtilizadas()]);
            _registroOrdenProduccion.Vista.CargarConceptosGastosIndirectos([.. UtilesOrdenProduccion.ObtenerConceptosGastosIndirectosUtilizados()]);
        }
    }
}