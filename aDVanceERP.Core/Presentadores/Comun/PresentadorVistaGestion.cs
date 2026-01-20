using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Repositorios.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Presentadores.Comun;

public abstract class PresentadorVistaGestion<Pt, Vg, Vt, En, Re, Fb> : PresentadorVistaBase<Vg>, IPresentadorVistaGestion<Vg, Re, En, Fb>
    where Pt : IPresentadorVistaTupla<Vt, En>
    where Vg : class, IVistaContenedor, IGestorEntidades, IBuscadorEntidades<Fb>, INavegadorTuplasEntidades
    where Vt : class, IVistaTupla
    where Re : class, IRepoEntidadBaseDatos<En, Fb>, new()
    where En : class, IEntidadBaseDatos, new()
    where Fb : Enum {
    private bool _disposed; // Para evitar llamadas redundantes a Dispose

    protected readonly VistaCargaDatos _cargaDatos;
    protected readonly List<Pt> _tuplasEntidades;
    private Re _repositorio;

    protected PresentadorVistaGestion(Vg vista) : base(vista) {
        _cargaDatos = new VistaCargaDatos();
        _tuplasEntidades = new List<Pt>();
        _repositorio = new();

        if (Vista is Form form)
            form.VisibleChanged += OnMostrarOcultarVista;

        Vista.Habilitada = false;
        Vista.BuscarEntidades += OnBuscarEntidad;
        Vista.RegistrarEntidad += OnRegistrarEntidad;
        Vista.AlturaContenedorTuplasModificada += OnAlturaContenedorTuplasModificada;
        Vista.SincronizarDatos += OnSincronizarDatos;

        AgregadorEventos.Suscribir("ResultadosBusquedaActualizados", OnResultadosBusquedaActualizados);
    }

    public Re Repositorio => _repositorio;
    public Fb FiltroBusqueda { get; protected set; } = default!;
    public string[] CriteriosBusqueda { get; protected set; } = Array.Empty<string>();
    public IEnumerable<Pt> TuplasSeleccionadas => _tuplasEntidades.Where(t => t.EstadoSeleccion);

    public event EventHandler? RegistrarEntidad;
    public event EventHandler<En>? EditarEntidad;

    public void Buscar(Fb filtroBusqueda, params string[] criteriosBusqueda) {
        FiltroBusqueda = filtroBusqueda;
        CriteriosBusqueda = criteriosBusqueda;

        ActualizarResultadosBusqueda();

        Vista.PaginaActual = 1;
    }

    public virtual void ActualizarResultadosBusqueda() {
        if (!Vista.Habilitada)
            return;

        try {
            if (Vista.TuplasMaximasContenedor == 0) return;

            Vista.PanelCentral.CerrarTodos();

            // Desuscribir eventos del presentador de tuplas
            foreach (var presentadorTupla in _tuplasEntidades) {
                presentadorTupla.EntidadSeleccionada -= OnEntidadSeleccionada;
                presentadorTupla.EditarEntidad -= OnEditarEntidad;
                presentadorTupla.EliminarEntidad -= OnEliminarEntidad;
                presentadorTupla.Dispose();
            }

            _tuplasEntidades.Clear();

            ContextoAplicacion.CoordenadaYUltimaTupla = 0;

            var incremento = (Vista.PaginaActual - 1) * Vista.TuplasMaximasContenedor;

            Repositorio.Limite = Vista.TuplasMaximasContenedor;
            Repositorio.Desplazamiento = incremento;

            var datos = Repositorio.Buscar(FiltroBusqueda, CriteriosBusqueda);
            var resultados = datos.resultadosBusqueda.ToList();
            var calculoPaginas = datos.cantidad / Vista.TuplasMaximasContenedor;
            var entero = datos.cantidad % Vista.TuplasMaximasContenedor == 0;

            Vista.PaginasTotales = calculoPaginas < 1 ? 1 : entero ? calculoPaginas : calculoPaginas + 1;

            _cargaDatos.Mostrar();

            for (var i = 0; i < resultados.Count && i < Vista.TuplasMaximasContenedor; i++) {
                var resultado = resultados[i];

                (Vista as Control)?.Invoke(() => {
                    AdicionarTuplaEntidad(resultado.entidadBase, resultado.entidadesExtra);
                });

                Application.DoEvents();
            }

            AgregadorEventos.Publicar("ResultadosBusquedaActualizados", AgregadorEventos.SerializarPayload(resultados));
        } catch (Exception ex) {
            CentroNotificaciones.Mostrar($"Error al refrescar la lista de objetos: {ex.Message}", TipoNotificacion.Error);
        }
    }

    protected virtual void AdicionarTuplaEntidad(En entidad, List<IEntidadBaseDatos> entidadesExtra) {
        (Vista as Control)?.Invoke(() => {
            var presentadorTupla = ObtenerValoresTupla(entidad, entidadesExtra);

            if (presentadorTupla == null) return;

            presentadorTupla.EntidadSeleccionada += OnEntidadSeleccionada;
            presentadorTupla.EditarEntidad += OnEditarEntidad;
            presentadorTupla.EliminarEntidad += OnEliminarEntidad;

            _tuplasEntidades.Add(presentadorTupla);

            Vista.PanelCentral.Registrar(
                presentadorTupla.Vista,
                new Point(0, ContextoAplicacion.CoordenadaYUltimaTupla),
                new Size(0, ContextoAplicacion.AlturaTuplaPredeterminada),
                TipoRedimensionadoVista.Horizontal);

            presentadorTupla.Vista.Mostrar();
        });

        ContextoAplicacion.CoordenadaYUltimaTupla += ContextoAplicacion.AlturaTuplaPredeterminada;
    }

    private void DeseleccionarTuplas(IVistaTupla vista) {
        _tuplasEntidades.ForEach(tupla => {
            if (!tupla.Vista.Equals(vista)) {
                tupla.EstadoSeleccion = false;
                tupla.Vista.EstadoSeleccion = false;
            }
        });
    }

    protected abstract Pt ObtenerValoresTupla(En entidad, List<IEntidadBaseDatos> entidadesExtra);

    protected virtual void OnEntidadSeleccionada(object? sender, En entidad) {
        DeseleccionarTuplas(sender as IVistaTupla);
    }

    protected virtual void OnEditarEntidad(object? sender, En entidad) {
        EditarEntidad?.Invoke(sender, entidad);
    }

    protected virtual void OnEliminarEntidad(object? sender, En entidad) {
        if (entidad != null)
            try {
                Repositorio.Eliminar(entidad.Id);
                Vista.PaginaActual = 1;

                ActualizarResultadosBusqueda();
            } catch (Exception ex) {
                throw new Exception($"Error al eliminar el objeto: {ex.Message}");
            }
    }

    private void OnBuscarEntidad(object? sender, (Fb filtro, string[] criterios) e) {
        Buscar(e.filtro, e.criterios);
    }

    private void OnRegistrarEntidad(object? sender, EventArgs e) {
        RegistrarEntidad?.Invoke(sender, e);
    }

    private void OnAlturaContenedorTuplasModificada(object? sender, EventArgs e) {
        if (Vista is Form vistaForm)
            if (!vistaForm.Visible)
                return;

        ActualizarResultadosBusqueda();
    }

    private void OnSincronizarDatos(object? sender, EventArgs e) {
        ActualizarResultadosBusqueda();
    }

    private void OnResultadosBusquedaActualizados(string obj) {
        _cargaDatos.Ocultar();
    }

    private void OnMostrarOcultarVista(object? sender, EventArgs e) {
        var vista = Vista as Control;

        if (vista == null)
            return;

        Vista.Habilitada = vista.Visible;
    }

    protected virtual void Dispose(bool disposing) {
        if (!_disposed) {
            if (disposing) {
                if (Vista is Form form)
                    form.VisibleChanged -= OnMostrarOcultarVista;

                Vista.BuscarEntidades -= OnBuscarEntidad;
                Vista.AlturaContenedorTuplasModificada -= OnAlturaContenedorTuplasModificada;
                Vista.SincronizarDatos -= OnSincronizarDatos;
            }

            _disposed = true;
        }
    }

    public override void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}