using aDVanceERP.Core.Controladores;
using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Extension.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Monedas;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Monedas;
using aDVanceERP.Modulos.Movil.Interfaces;

using System.Text;

namespace aDVanceERP.Modulos.Movil.Presentadores {

    internal class PresentadorGestionAdvancePos : PresentadorVistaBase<IVistaGestionAdvancePos> {

        private readonly ControladorArchivosAndroidPos _controladorPos;
        private readonly ExportadorCatalogosPos _exportador;
        private readonly ImportadorVentasPos _importador;

        public PresentadorGestionAdvancePos(IVistaGestionAdvancePos vista) : base(vista) {
            _controladorPos = new ControladorArchivosAndroidPos(Application.StartupPath);
            _exportador = new ExportadorCatalogosPos(CarpetaExportacion);
            _importador = new ImportadorVentasPos();

            vista.VerificarConexion += OnVerificarConexion;
            vista.EnviarCatalogo += OnEnviarCatalogo;
            vista.EliminarCatalogo += OnEliminarCatalogo;
            vista.ImportarVentas += OnImportarVentas;
            vista.ImportarTodasLasVentas += OnImportarTodasLasVentas;

            AgregadorEventos.Suscribir("MostrarVistaGestionPos", OnMostrarVistaGestionPos);
        }

        private string CarpetaExportacion => Path.Combine(Application.StartupPath, "exports", "pos");

        private string CarpetaImportacion => Path.Combine(Application.StartupPath, "imports", "pos");

        private void OnMostrarVistaGestionPos(string obj) {
            CargarDatosComunes();

            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarEstadoConexion();
        }

        private void CargarDatosComunes() {
            Vista.CargarAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
        }

        public override void Dispose() {
            Vista.VerificarConexion -= OnVerificarConexion;
            Vista.EnviarCatalogo -= OnEnviarCatalogo;
            Vista.EliminarCatalogo -= OnEliminarCatalogo;
            Vista.ImportarVentas -= OnImportarVentas;
            Vista.ImportarTodasLasVentas -= OnImportarTodasLasVentas;

            AgregadorEventos.Desuscribir("MostrarVistaGestionPos", OnMostrarVistaGestionPos);

            Vista.Cerrar();
        }

        private void OnVerificarConexion(object? sender, EventArgs e)
            => ActualizarEstadoConexion(mostrarAdvertencia: true);

        private void OnEnviarCatalogo(object? sender, EventArgs e) {
            if (!_controladorPos.CheckDeviceConnection(mostrarAdvertencia: true)) return;

            GenerarCatalogoPos(Vista.Almacen?.Id ?? 0);

            bool ok = _controladorPos.PushCatalogo(
                Path.Combine(CarpetaExportacion, "catalogo.json"));

            if (ok) ActualizarEstadoConexion();
        }

        private void OnEliminarCatalogo(object? sender, EventArgs e) {
            var resultado = CentroNotificaciones.MostrarMensaje(
                "¿Está seguro de que desea eliminar el catálogo del dispositivo? " +
                "aDVance.POS no podrá registrar nuevas ventas hasta que se envíe un nuevo catálogo.",
                TipoMensaje.Advertencia,
                BotonesMensaje.SiNo);

            if (resultado != DialogResult.Yes) return;

            _controladorPos.EliminarCatalogo();
            ActualizarEstadoConexion();
        }

        private void OnImportarVentas(object? sender, EventArgs e) {
            if (!_controladorPos.CheckDeviceConnection(mostrarAdvertencia: true)) return;

            string archivoIndividual = sender as string ?? string.Empty;
            string rutaArchivo = archivoIndividual.StartsWith("ventas_")
                ? Path.Combine(CarpetaImportacion, archivoIndividual)
                : Path.Combine(CarpetaImportacion, $"ventas_{DateTime.Today:yyyyMMdd}.json");

            bool ok = _controladorPos.PullVentas(rutaArchivo, DateTime.Today);

            if (!ok)
                return;

            var resultado = _importador.Procesar(
                [rutaArchivo],
                registrarEnCaja: CajaModuloCargado(),
                eliminarTrasImportar: true);

            MostrarResumenImportacion(resultado);

            _controladorPos.EliminarArchivoVentas(rutaArchivo);
        }

        private void OnImportarTodasLasVentas(object? sender, EventArgs e) {
            if (!_controladorPos.CheckDeviceConnection(mostrarAdvertencia: true))
                return;

            var archivos = _controladorPos.FlujoFinDia(CarpetaImportacion, eliminarDelDispositivo: true);

            if (archivos.Count == 0)
                return;

            var resultado = _importador.Procesar(
                archivos,
                registrarEnCaja: CajaModuloCargado(),
                eliminarTrasImportar: true);

            MostrarResumenImportacion(resultado);
        }

        private void GenerarCatalogoPos(long idAlmacen) {
            var almacen = RepoAlmacen.Instancia.ObtenerPorId(idAlmacen) ?? throw new InvalidOperationException($"Almacén {idAlmacen} no encontrado.");

            var inventarioAlmacen = RepoInventario.Instancia
                .Buscar(FiltroBusquedaInventario.IdAlmacen, idAlmacen.ToString())
                .resultadosBusqueda
                .Select(r => r.entidadBase)
                .Where(i => i.Cantidad > 0)
                .ToList();

            var productos = inventarioAlmacen
                .Select(i => RepoProducto.Instancia.ObtenerPorId(i.IdProducto))
                .Where(p => p != null && p.Activo)
                .Cast<Producto>()
                .ToList();

            var unidades = RepoUnidadMedida.Instancia
                .ObtenerTodos().Select(r => r.entidadBase).ToList();

            var presentacionesPorProducto = new Dictionary<long,
                List<PresentacionProducto>>();

            foreach (var p in productos) {
                var prs = RepoPresentacionProducto.Instancia
                    .Buscar(FiltroBusquedaPresentacionProducto.IdProducto, p.Id.ToString())
                    .resultadosBusqueda
                    .Select(r => r.entidadBase)
                    .Where(pr => pr != null && pr.Activo)
                    .Cast<PresentacionProducto>()
                    .ToList();
                presentacionesPorProducto[p.Id] = prs;
            }

            var monedas = ConstruirMonedasDto();

            _exportador.ExportarCatalogo(
                productos,
                inventarioAlmacen,
                almacen,
                unidades,
                monedas,
                presentacionesPorProducto);
        }

        private IEnumerable<MonedaCatalogoDto> ConstruirMonedasDto() {
            var repoMoneda = RepoMoneda.Instancia;
            var repoTasaCambio = RepoTasaCambio.Instancia;
            var monedaBase = repoMoneda.ObtenerMonedaBase();
            var monedasActivas = repoMoneda.ObtenerActivas();

            return monedasActivas.Select(m => {
                var tasa = 1m;
                var aplicaEfectivo = true;

                if (!m.EsBase) {
                    var tasaCambio = repoTasaCambio
                        .Buscar(FiltroBusquedaTasaCambio.VigenteHoy,
                                monedaBase.Id.ToString(), m.Id.ToString())
                        .resultadosBusqueda.FirstOrDefault()
                        .entidadBase as TasaCambio;

                    tasa = tasaCambio?.Tasa ?? 1m;
                    aplicaEfectivo = tasaCambio?.AplicaEfectivo ?? true;
                }

                return new MonedaCatalogoDto(
                    m.Id, m.Codigo, m.Nombre, m.Simbolo,
                    m.EsBase, tasa, aplicaEfectivo, m.PrecisionDecimal);
            });
        }

        private void ActualizarEstadoConexion(bool mostrarAdvertencia = false) {
            bool conectado = _controladorPos.CheckDeviceConnection(mostrarAdvertencia);
            bool appInstalada = conectado && _controladorPos.CheckAppInstalada();

            Vista.DispositivoConectado = conectado;
            Vista.AppInstalada = appInstalada;
            Vista.MostrarBotonEnviarCatalogo = appInstalada;
            Vista.MostrarBotonEliminarCatalogo = appInstalada && _controladorPos.ExisteCatalogo();
            Vista.MostrarBotonImportarVentas = appInstalada;

            if (conectado && appInstalada) {
                var (existe, fechaCatalogo) = _controladorPos.ObtenerInfoCatalogo();
                Vista.CatalogoExisteEnDispositivo = existe;
                Vista.FechaActualizacionCatalogo = fechaCatalogo;

                var archivosVenta = _controladorPos.ListarArchivosVentas();
                Vista.ArchivosDisponiblesDispositivo = archivosVenta.Count;
                Vista.ActualizarArchivosVenta(archivosVenta);
            } else {
                Vista.CatalogoExisteEnDispositivo = false;
                Vista.FechaActualizacionCatalogo = null;
                Vista.ActualizarArchivosVenta(
                    new List<(string fileName, DateTime fecha, double tamanoKb)>());
            }
        }

        /// <summary>
        /// Muestra el resumen de la importación de ventas al usuario.
        /// Se separa del importador para mantener las notificaciones en la capa de UI.
        /// </summary>
        private void MostrarResumenImportacion(ImportadorVentasPos.ResultadoImportacion r) {
            var sb = new StringBuilder();
            sb.AppendLine($"Ventas importadas: {r.VentasImportadas}");

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

        /// <summary>
        /// Comprueba si MOD_CAJA está cargado para decidir si registrar en caja.
        /// La comprobación queda en el presenter porque ContextoModulos es de Core.Extension.
        /// </summary>
        private static bool CajaModuloCargado()
            => ContextoModulos.NombresModulosCargados.Exists(nm => nm.Equals("MOD_CAJA"));
    }
}
