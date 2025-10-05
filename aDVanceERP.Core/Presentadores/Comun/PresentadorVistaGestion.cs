using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Repositorios.Comun.Interfaces;
using aDVanceERP.Core.Utiles;
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

        CargaDatosCompletada += delegate { _cargaDatos.Ocultar(); };
    }

    public Re Repositorio => _repositorio;
    public Fb FiltroBusqueda { get; protected set; }
    public string? CriterioBusqueda { get; protected set; }
    public IEnumerable<Pt> TuplasSeleccionadas => _tuplasEntidades.Where(t => t.EstadoSeleccion);

    public event EventHandler? RegistrarEntidad;
    public event EventHandler<En>? EditarEntidad;
    public event EventHandler<bool>? CargaDatosCompletada;

    public void Buscar(Fb filtroBusqueda, string? criterioBusqueda) {
        FiltroBusqueda = filtroBusqueda;
        CriterioBusqueda = criterioBusqueda;

        ActualizarResultadosBusqueda();

        Vista.PaginaActual = 1;
    }

    public async virtual void ActualizarResultadosBusqueda() {
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

            VariablesGlobales.CoordenadaYUltimaTupla = 0;

            var incremento = (Vista.PaginaActual - 1) * Vista.TuplasMaximasContenedor;

            // Ejecutar la búsqueda en un hilo separado
            var datos = await Task.Run(() =>
                Repositorio.Buscar(FiltroBusqueda, CriterioBusqueda, Vista.TuplasMaximasContenedor, incremento));

            var entidades = datos.entidades.ToList();
            var calculoPaginas = datos.cantidad / Vista.TuplasMaximasContenedor;
            var entero = datos.cantidad % Vista.TuplasMaximasContenedor == 0;

            Vista.PaginasTotales = calculoPaginas < 1 ? 1 : entero ? calculoPaginas : calculoPaginas + 1;

            _cargaDatos.Mostrar();

            // Procesar las tuplas de forma asíncrona
            await Task.Run(() => {
                for (var i = 0; i < entidades.Count && i < Vista.TuplasMaximasContenedor; i++) {
                    var entidad = entidades[i];
                    
                    (Vista as Control)?.Invoke(() => {
                        AdicionarTuplaEntidad(entidad);
                    });

                    // Pequeña pausa para permitir que la UI se actualice
                    Thread.Sleep(10);
                }

                CargaDatosCompletada?.Invoke(this, true);
            });            
        }
        catch (Exception ex) {
            CentroNotificaciones.Mostrar($"Error al refrescar la lista de objetos: {ex.Message}", TipoNotificacion.Error);
        }
    }

    protected virtual void AdicionarTuplaEntidad(En entidad) {
        (Vista as Control)?.Invoke(() => {
            var presentadorTupla = ObtenerValoresTupla(entidad);

            if (presentadorTupla == null) return;

            presentadorTupla.EntidadSeleccionada += OnEntidadSeleccionada;
            presentadorTupla.EditarEntidad += OnEditarEntidad;
            presentadorTupla.EliminarEntidad += OnEliminarEntidad;

            _tuplasEntidades.Add(presentadorTupla);

            Vista.PanelCentral.Registrar(
                presentadorTupla.Vista,
                new Point(0, VariablesGlobales.CoordenadaYUltimaTupla),
                new Size(0, VariablesGlobales.AlturaTuplaPredeterminada),
                TipoRedimensionadoVista.Horizontal);

            presentadorTupla.Vista.Mostrar();
        });

        VariablesGlobales.CoordenadaYUltimaTupla += VariablesGlobales.AlturaTuplaPredeterminada;
    }

    private void DeseleccionarTuplas(IVistaTupla vista) {
        _tuplasEntidades.ForEach(tupla => {
            if (!tupla.Vista.Equals(vista))
                tupla.EstadoSeleccion = false;
        });
    }

    protected abstract Pt ObtenerValoresTupla(En entidad);

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
            }
            catch (Exception ex) {
                throw new Exception($"Error al eliminar el objeto: {ex.Message}");
            }
    }

    private void OnBuscarEntidad(object? sender, (Fb filtro, string? criterio) e) {
        Buscar(e.filtro, e.criterio);
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