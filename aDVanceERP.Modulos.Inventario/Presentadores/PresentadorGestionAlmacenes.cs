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
            RegistrarEntidad += OnRegistrarAlmacen;
            EditarEntidad += OnEditarAlmacen;

            AgregadorEventos.Suscribir("MostrarVistaGestionAlmacenes", OnMostrarVistaGestionAlmacenes);
        }

        private void OnRegistrarAlmacen(object? sender, EventArgs e) {
            AgregadorEventos.Publicar("MostrarVistaRegistroAlmacen", string.Empty);
        }

        private void OnEditarAlmacen(object? sender, Almacen e) {
            AgregadorEventos.Publicar("MostrarVistaEdicionAlmacen", AgregadorEventos.SerializarPayload(e));
        }

        private void OnMostrarVistaGestionAlmacenes(string obj) {
            Vista.CargarFiltrosBusqueda([.. EnumExt.ObtenerNombresDescripciones<FiltroBusquedaAlmacen>()]);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        protected override PresentadorTuplaAlmacen ObtenerValoresTupla(Almacen entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaAlmacen(new VistaTuplaAlmacen(), entidad);

            presentadorTupla.Vista.Id = entidad.Id.ToString();
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
    }
}