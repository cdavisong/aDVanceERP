using aDVanceERP.Core.Controladores;
using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Compra;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Movil.Interfaces;

using System.Text;

namespace aDVanceERP.Modulos.Movil.Presentadores {

    internal class PresentadorGestionAdvanceStock : PresentadorVistaBase<IVistaGestionAdvanceStock> {

        private readonly ControladorArchivosAndroidStock _controladorStock;
        private readonly ExportadorCatalogosStock        _exportador;
        private readonly ImportadorSesionesStock         _importador;

        public PresentadorGestionAdvanceStock(IVistaGestionAdvanceStock vista) : base(vista) {
            _controladorStock = new ControladorArchivosAndroidStock(Application.StartupPath);
            _exportador       = new ExportadorCatalogosStock(CarpetaExportacion);
            _importador       = new ImportadorSesionesStock();

            Vista.VerificarConexion   += OnVerificarConexion;
            Vista.EnviarCatalogos     += OnEnviarCatalogos;
            Vista.EliminarCatalogos   += OnEliminarCatalogos;
            Vista.ImportarSesiones    += OnImportarSesiones;

            AgregadorEventos.Suscribir("MostrarVistaGestionStock", OnMostrarVistaGestionStock);
        }

        private string CarpetaExportacion => Path.Combine(Application.StartupPath, "exports", "stock");
        private string CarpetaImportacion => Path.Combine(Application.StartupPath, "imports", "stock");

        // ══════════════════════════════════════════════════════
        //  CICLO DE VIDA
        // ══════════════════════════════════════════════════════

        private void OnMostrarVistaGestionStock(string obj) {
            Vista.Restaurar();
            ActualizarEstadoConexion();
            Vista.Mostrar();
        }

        public override void Dispose() {
            Vista.VerificarConexion  -= OnVerificarConexion;
            Vista.EnviarCatalogos    -= OnEnviarCatalogos;
            Vista.EliminarCatalogos  -= OnEliminarCatalogos;
            Vista.ImportarSesiones   -= OnImportarSesiones;

            AgregadorEventos.Desuscribir("MostrarVistaGestionStock", OnMostrarVistaGestionStock);

            Vista.Cerrar();
        }

        // ══════════════════════════════════════════════════════
        //  HANDLERS DE VISTA
        // ══════════════════════════════════════════════════════

        private void OnVerificarConexion(object? sender, EventArgs e)
            => ActualizarEstadoConexion(mostrarAdvertencia: true);

        private void OnEnviarCatalogos(object? sender, EventArgs e) {
            if (!_controladorStock.CheckDeviceConnection(mostrarAdvertencia: true)) return;

            GenerarCatalogosStock();

            _controladorStock.FlujoPreparacion(
                _exportador.RutaProductos,
                _exportador.RutaProveedores,
                _exportador.RutaUnidades,
                _exportador.RutaClasificaciones,
                _exportador.RutaAlmacenes);

            ActualizarEstadoConexion();
        }

        private void OnEliminarCatalogos(object? sender, EventArgs e) {
            var respuesta = CentroNotificaciones.MostrarMensaje(
                "¿Está seguro de que desea eliminar todos los catálogos del dispositivo? " +
                "aDVance.STOCK no podrá registrar inventario hasta que se envíen nuevos catálogos.",
                TipoMensaje.Advertencia,
                BotonesMensaje.SiNo);

            if (respuesta != DialogResult.Yes) return;

            _controladorStock.LimpiarCatalogos();
            ActualizarEstadoConexion();
        }

        private void OnImportarSesiones(object? sender, EventArgs e) {
            if (!_controladorStock.CheckDeviceConnection(mostrarAdvertencia: true)) return;

            string archivoIndividual = sender as string ?? string.Empty;

            ResultadoPullStock pullResultado;

            if (!string.IsNullOrEmpty(archivoIndividual) && archivoIndividual.StartsWith("stock_")) {
                string destino = Path.Combine(CarpetaImportacion, archivoIndividual);
                pullResultado = _controladorStock.PullSesionIndividual(archivoIndividual, destino);
            } else {
                pullResultado = _controladorStock.FlujoImportacion(
                    CarpetaImportacion, limpiarDispositivoTrasImportar: true);
            }

            if (!pullResultado.Exitoso || pullResultado.JsonDescargados.Count == 0) return;

            var resultado = _importador.Procesar(
                pullResultado.JsonDescargados,
                CarpetaImportacion);

            MostrarResumenImportacion(resultado);
            ActualizarEstadoConexion();
        }

        // ══════════════════════════════════════════════════════
        //  GENERACIÓN DE CATÁLOGOS
        // ══════════════════════════════════════════════════════

        private void GenerarCatalogosStock() {
            var productos       = RepoProducto.Instancia.ObtenerTodos().Select(r => r.entidadBase);
            var proveedores     = RepoProveedor.Instancia.ObtenerTodos().Select(r => r.entidadBase);
            var unidades        = RepoUnidadMedida.Instancia.ObtenerTodos().Select(r => r.entidadBase);
            var clasificaciones = RepoClasificacionProducto.Instancia.ObtenerTodos().Select(r => r.entidadBase);
            var almacenes       = RepoAlmacen.Instancia.ObtenerTodos().Select(r => r.entidadBase);

            _exportador.ExportarTodo(productos, proveedores, unidades, clasificaciones, almacenes);
        }

        // ══════════════════════════════════════════════════════
        //  ESTADO DE CONEXIÓN
        // ══════════════════════════════════════════════════════

        private void ActualizarEstadoConexion(bool mostrarAdvertencia = false) {
            bool conectado    = _controladorStock.CheckDeviceConnection(mostrarAdvertencia);
            bool appInstalada = conectado && _controladorStock.CheckAppInstalada();

            Vista.DispositivoConectado        = conectado;
            Vista.AppInstalada                = appInstalada;
            Vista.MostrarBotonEnviarCatalogos  = appInstalada;
            Vista.MostrarBotonImportarSesiones = appInstalada;

            if (conectado && appInstalada) {
                var (existen, fecha) = _controladorStock.ObtenerInfoCatalogos();
                Vista.CatalogosExistenEnDispositivo = existen;
                Vista.FechaActualizacionCatalogos   = fecha;
                Vista.MostrarBotonEliminarCatalogos = existen;

                var sesiones = _controladorStock.ListarArchivosStock();
                Vista.ArchivosDisponiblesDispositivo = sesiones.Count;
                Vista.ActualizarArchivosSesion(sesiones);
            } else {
                Vista.CatalogosExistenEnDispositivo = false;
                Vista.FechaActualizacionCatalogos   = null;
                Vista.MostrarBotonEliminarCatalogos = false;
                Vista.ActualizarArchivosSesion(
                    new List<(string fileName, DateTime fechaHora, double tamanoKb)>());
            }
        }

        // ══════════════════════════════════════════════════════
        //  HELPERS
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Muestra el resumen de la importación de sesiones al usuario.
        /// Se separa del importador para mantener las notificaciones en la capa de UI.
        /// </summary>
        private void MostrarResumenImportacion(ImportadorSesionesStock.ResultadoImportacion r) {
            var sb = new StringBuilder();
            sb.AppendLine($"Productos actualizados: {r.ProductosActualizados}");

            if (r.ProductosNuevos > 0)
                sb.AppendLine($"Productos nuevos registrados: {r.ProductosNuevos}");

            if (r.Errores.Count > 0) {
                sb.AppendLine();
                sb.AppendLine("Errores (primeros 5):");
                foreach (var err in r.Errores.Take(5))
                    sb.AppendLine($"• {err}");
            }

            CentroNotificaciones.MostrarNotificacion(
                sb.ToString().TrimEnd(),
                r.Errores.Count == 0
                    ? TipoNotificacionEnum.Ok
                    : TipoNotificacionEnum.Advertencia);
        }
    }
}
