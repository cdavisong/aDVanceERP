using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Vistas;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorGestionAlmacenes : PresentadorVistaGestion<PresentadorTuplaAlmacen, IVistaGestionAlmacenes, IVistaTuplaAlmacen, Almacen, RepoAlmacen, FiltroBusquedaAlmacen> {
        public PresentadorGestionAlmacenes(IVistaGestionAlmacenes vista) : base(vista) {
            vista.RegistrarEntidad += OnRegistrarAlmacen;

            AgregadorEventos.Suscribir("MostrarVistaGestionAlmacenes", OnMostrarVistaGestionAlmacenes);
        }

        private void OnMostrarVistaGestionAlmacenes(string obj) {
            CargarDatosComunes();

            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        private void OnRegistrarAlmacen(object? sender, EventArgs e) {
            AgregadorEventos.Publicar("MostrarVistaRegistroAlmacen", sender?.ToString());
        }

        private void CargarDatosComunes() {
            Vista.CargarFiltrosBusqueda([.. EnumExt.ObtenerNombresDescripciones<FiltroBusquedaAlmacen>()]);
        }

        protected override PresentadorTuplaAlmacen ObtenerValoresTupla(Almacen entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaAlmacen(new VistaTuplaAlmacen(), entidad);

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.NombreAlmacen = entidad.Nombre;
            presentadorTupla.Vista.Tipo = entidad.Tipo.ToString();
            presentadorTupla.Vista.Direccion = string.IsNullOrEmpty(entidad.Direccion) 
                ? "No especificada"
                : entidad.Direccion;
            presentadorTupla.Vista.Estado = entidad.Estado;
            presentadorTupla.Vista.Descripcion = string.IsNullOrEmpty(entidad.Descripcion) 
                ? "No disponible"
                : entidad.Descripcion;

            return presentadorTupla;
        }

        public override void Dispose() {
            Vista.RegistrarEntidad -= OnRegistrarAlmacen;

            AgregadorEventos.Desuscribir("MostrarVistaGestionAlmacenes", OnMostrarVistaGestionAlmacenes);

            base.Dispose();
        }
    }
}