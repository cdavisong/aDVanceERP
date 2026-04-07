using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Vistas;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    internal class PresentadorGestionVentaPresentacion : PresentadorVistaGestion<PresentadorTuplaVentaPresentacion, IVistaGestionVentaPresentacion, IVistaTuplaVentaPresentacion, PrecioPresentacion, RepoPrecioPresentacion, FiltroBusquedaPrecioPresentacion> {
        public PresentadorGestionVentaPresentacion(IVistaGestionVentaPresentacion vista) : base(vista) {
            RegistrarEntidad += OnRegistrarVentaPresentacion;
            EditarEntidad += OnEditarVentaPresentacion;

            AgregadorEventos.Suscribir("MostrarVistaGestionPrecioPresentacion", OnMostrarVistaGestionVentaPresentacion);
        }

        private void OnRegistrarVentaPresentacion(object? sender, EventArgs e) {
            if (sender is not PrecioPresentacion precioPresentacion)
                return;

            RepoPrecioPresentacion.Instancia.Adicionar(precioPresentacion);

            ActualizarResultadosBusqueda();
        }

        private void OnEditarVentaPresentacion(object? sender, PrecioPresentacion e) {
            if (sender is not PrecioPresentacion precioPresentacion)
                return;

            ActualizarResultadosBusqueda();
        }

        private void OnMostrarVistaGestionVentaPresentacion(string obj) {
            Vista.Restaurar();

            // Carga inicial de datos
            Vista.CargarDatosProducto(AgregadorEventos.DeserializarPayload<Producto>(obj));
            Vista.CargarUnidadesMedida([.. RepoUnidadMedida.Instancia.ObtenerTodos().Select(um => um.entidadBase)]);
            
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        protected override PresentadorTuplaVentaPresentacion ObtenerValoresTupla(PrecioPresentacion entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaVentaPresentacion(new VistaTuplaVentaPresentacion(), entidad);
            var producto = RepoProducto.Instancia.ObtenerPorId(entidad.IdProducto)!;
            var unidadMedida = RepoUnidadMedida.Instancia.ObtenerPorId(entidad.IdUnidadMedida)!;
                        
            presentadorTupla.Vista.NombreUM = unidadMedida.Nombre;
            presentadorTupla.Vista.AbreviaturaUM = unidadMedida.Abreviatura;
            presentadorTupla.Vista.Cantidad = entidad.Cantidad;
            presentadorTupla.Vista.PrecioVenta = entidad.PrecioVenta;
            presentadorTupla.Vista.PrecioPorUnidad = entidad.PrecioPorUnidad;
            presentadorTupla.Vista.Descuento = ((producto.PrecioVentaBase - entidad.PrecioPorUnidad) / producto.PrecioVentaBase) * 100;
            presentadorTupla.Vista.Estado = entidad.Activo;

            return presentadorTupla;
        }

        public override void ActualizarResultadosBusqueda() {
            FiltroBusqueda = Vista.FiltroBusqueda;
            CriteriosBusqueda = Vista.CriteriosBusqueda;

            base.ActualizarResultadosBusqueda();
        }
    }
}
