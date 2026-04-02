using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Documentos;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Vistas;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorGestionAlmacenes : PresentadorVistaGestion<PresentadorTuplaAlmacen, IVistaGestionAlmacenes, IVistaTuplaAlmacen, Almacen, RepoAlmacen, FiltroBusquedaAlmacen> {
        private DocInventarioAlmacen _docInventarioAlmacen = new DocInventarioAlmacen();

        public PresentadorGestionAlmacenes(IVistaGestionAlmacenes vista) : base(vista) {
            vista.ExportarDocumentoInventario += OnExportarDocumentoInventarioAlmacenes;

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
            Vista.CargarFiltrosBusqueda(UtilesBusquedaAlmacen.FiltroBusquedaAlmacen);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        protected override PresentadorTuplaAlmacen ObtenerValoresTupla(Almacen entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaAlmacen(new VistaTuplaAlmacen(), entidad);

            presentadorTupla.Vista.Id = entidad.Id.ToString();
            presentadorTupla.Vista.NombreAlmacen = entidad.Nombre;
            presentadorTupla.Vista.Tipo = entidad.Tipo.ToString();
            presentadorTupla.Vista.Direccion = entidad.Direccion ?? "No hay dirección disponible";
            presentadorTupla.Vista.CoordenadasGeograficas = entidad.Coordenadas ?? new CoordenadasGeograficas { Latitud = 0, Longitud = 0 };
            presentadorTupla.Vista.Estado = entidad.Estado;
            presentadorTupla.Vista.Descripcion = entidad.Descripcion ?? "No hay descripción disponible";
            presentadorTupla.Vista.ExportarDocumentoInventario += OnExportarDocumentoInventarioAlmacen;

            return presentadorTupla;
        }

        private void OnExportarDocumentoInventarioAlmacen(object? sender, (int id, FormatoDocumento formato) e) {
            _docInventarioAlmacen.GenerarDocumentoConParametros(e.formato, e.id);
        }

        private void OnExportarDocumentoInventarioAlmacenes(object? sender, FormatoDocumento e) {
            _docInventarioAlmacen.GenerarDocumento(true, e);
        }
    }
}