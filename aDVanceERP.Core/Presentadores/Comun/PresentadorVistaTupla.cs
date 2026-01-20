using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

using Guna.UI2.WinForms;

namespace aDVanceERP.Core.Presentadores.Comun;

public abstract class PresentadorVistaTupla<Vt, En> : PresentadorVistaBase<Vt>, IPresentadorVistaTupla<Vt, En>
    where Vt : class, IVistaTupla
    where En : class, IEntidadBaseDatos, new() {
    private bool _disposed; // Para evitar llamadas redundantes a Dispose

    private En _entidad;

    protected PresentadorVistaTupla(Vt vista, En entidad) : base(vista) {
        _entidad = entidad ?? throw new ArgumentNullException(nameof(entidad));

        // Suscribir a eventos de la vista
        vista.EditarDatosTupla += OnEditarDatosTupla;
        vista.EliminarDatosTupla += OnEliminarDatosTupla;

        // Manejar el evento de selección de la tupla en la vista. Para ello:
        // - Todos los controles registrados en la vista, exceptuando los botones modifican el estado de selección de la tupla.
        var vistaForm = vista as Form;
        var controlesVista = vistaForm?.GetAllControls();

        if (controlesVista != null) {
            foreach (Control control in controlesVista) {
                if (control is not Button && control is not Guna2Button && control is not Guna2CircleButton)
                    control.Click += OnTuplaSeleccionada;
            }
        }
    }

    public En Entidad => _entidad;

    public bool EstadoSeleccion {
        get => Vista.ColorFondoTupla.Equals(ContextoAplicacion.ColorResaltadoTupla);
        set {
            if (value) {
                Vista.ColorFondoTupla = ContextoAplicacion.ColorResaltadoTupla;
                EntidadSeleccionada?.Invoke(Vista, Entidad);
            }
            else {
                Vista.Restaurar();
                EntidadDeseleccionada?.Invoke(Vista, Entidad);
            }

            Vista.EstadoSeleccion = value;

            AgregadorEventos.Publicar("CambioSeleccionTuplaEntidad", AgregadorEventos.SerializarPayload(value));
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

        // Desuscribir de eventos de la vista
        Vista.EditarDatosTupla -= OnEditarDatosTupla;
        Vista.EliminarDatosTupla -= OnEliminarDatosTupla;

        // Desuscribir del evento de selección de la tupla en la vista
        var vistaForm = Vista as Form;
        var controlesVista = vistaForm?.GetAllControls();

        if (controlesVista != null) {
            foreach (Control control in controlesVista) {
                if (control is not Button && control is not Guna2Button && control is not Guna2CircleButton)
                    control.Click -= OnTuplaSeleccionada;
            }
        }

        _disposed = true;
    }

    public override void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}