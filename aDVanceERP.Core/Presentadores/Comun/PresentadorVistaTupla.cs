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
                EntidadSeleccionada?.Invoke(Vista, Entidad);
            }
            else {
                Vista.Restaurar();
                EntidadDeseleccionada?.Invoke(Vista, Entidad);
            }
        }
    }

    public event EventHandler<En>? EntidadSeleccionada;
    public event EventHandler<En>? EntidadDeseleccionada;
    public event EventHandler<En>? EditarEntidad;
    public event EventHandler<En>? EliminarEntidad;

    private void OnTuplaSeleccionada(object? sender, EventArgs e) {
        EstadoSeleccion = !EstadoSeleccion;
    }

    private void OnEditarDatosTupla(object? sender, EventArgs e) {
        EditarEntidad?.Invoke(sender, Entidad);
    }

    private void OnEliminarDatosTupla(object? sender, EventArgs e) {
        EliminarEntidad?.Invoke(sender, Entidad);
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