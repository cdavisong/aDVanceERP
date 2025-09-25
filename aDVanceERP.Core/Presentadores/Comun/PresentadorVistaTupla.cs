using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Utiles;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Presentadores.Comun;

public abstract class PresentadorVistaTupla<Vt, En> : PresentadorVistaBase<Vt>, IPresentadorVistaTupla<Vt, En>
    where Vt : class, IVistaTupla
    where En : class, IEntidadBaseDatos, new() {
    private bool _disposed; // Para evitar llamadas redundantes a Dispose

    private En _entidad;

    protected PresentadorVistaTupla(Vt vista, En entidad) : base(vista) {
        _entidad = entidad ?? throw new ArgumentNullException(nameof(entidad));

        // Suscribir a eventos de la vista
        Vista.TuplaSeleccionada += OnTuplaSeleccionada;
        Vista.EditarDatosTupla += OnEditarDatosTupla;
        Vista.EliminarDatosTupla += OnEliminarDatosTupla;
    }

    public En Entidad => _entidad;

    public bool EstadoSeleccion {
        get => Vista.ColorFondoTupla.Equals(VariablesGlobales.ColorResaltadoTupla);
        set {
            if (value) {
                Vista.ColorFondoTupla = VariablesGlobales.ColorResaltadoTupla;
                EntidadSeleccionada?.Invoke(Vista, EventArgs.Empty);
            }
            else {
                Vista.Restaurar();
                EntidadDeseleccionada?.Invoke(Vista, EventArgs.Empty);
            }
        }
    }

    public event EventHandler? EntidadSeleccionada;
    public event EventHandler? EntidadDeseleccionada;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarEntidad;

    private void OnTuplaSeleccionada(object? sender, EventArgs e) {
        EstadoSeleccion = !EstadoSeleccion;
    }

    private void OnEditarDatosTupla(object? sender, EventArgs e) {
        EditarEntidad?.Invoke(Entidad, e);
    }

    private void OnEliminarDatosTupla(object? sender, EventArgs e) {
        EliminarEntidad?.Invoke(Entidad, e);
    }

    protected virtual void Dispose(bool disposing) {
        if (_disposed)
            return;

        if (disposing) {
            // Liberar recursos administrados
        }

        Vista.TuplaSeleccionada -= OnTuplaSeleccionada;
        Vista.EditarDatosTupla -= OnEditarDatosTupla;
        Vista.EliminarDatosTupla -= OnEliminarDatosTupla;

        _disposed = true;
    }

    public override void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}