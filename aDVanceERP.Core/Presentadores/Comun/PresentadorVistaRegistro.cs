using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Repositorios.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Presentadores.Comun;

public abstract class PresentadorVistaRegistro<Vr, En, Re, Fb> : PresentadorVistaBase<Vr>, IPresentadorVistaRegistro<Vr, Re, En, Fb>
    where Vr : class, IVistaRegistro
    where Re : class, IRepoEntidadBaseDatos<En, Fb>, new()
    where En : class, IEntidadBaseDatos, new()
    where Fb : Enum {
    private bool _disposed; // Para evitar llamadas redundantes a Dispose

    protected En? _entidad;
    protected Re _repositorio;

    protected PresentadorVistaRegistro(Vr vista) : base(vista) {
        _entidad = null;
        _repositorio = new();

        Vista.RegistrarEntidad += OnRegistrarEntidad;
        Vista.EditarEntidad += OnEditarEntidad;
    }

    public En? Entidad => _entidad;

    public Re Repositorio => _repositorio;

    public event EventHandler? EntidadRegistradaActualizada;
    public event EventHandler? Salir;

    public abstract void PopularVistaDesdeEntidad(En entidad);

    protected abstract En? ObtenerEntidadDesdeVista();

    protected virtual bool EntidadCorrecta() {
        return true;
    }

    protected virtual void RegistroAuxiliar(Re repositorio, long id) { }

    protected virtual void OnRegistrarEntidad(object? sender, EventArgs e) {
        RegistrarEditarEntidad(sender, e);
    }

    protected virtual void OnEditarEntidad(object? sender, EventArgs e) {
        RegistrarEditarEntidad(sender, e);
    }

    private void RegistrarEditarEntidad(object? sender, EventArgs e) {
        if (!EntidadCorrecta())
            return;

        _entidad = ObtenerEntidadDesdeVista();

        if (_entidad == null)
            return;

        if (Vista.ModoEdicion && _entidad.Id != 0)
            Repositorio.Editar(_entidad);
        else if (_entidad.Id != 0)
            Repositorio.Editar(_entidad);
        else
            _entidad.Id = Repositorio.Adicionar(_entidad);

        RegistroAuxiliar(_repositorio, _entidad.Id);

        EntidadRegistradaActualizada?.Invoke(sender, e);
        Salir?.Invoke(sender, e);

        Vista.Ocultar();
    }

    private void OnSalir(object? sender, EventArgs e) {
        Salir?.Invoke(sender, e);

        Vista.Ocultar();
    }

    protected void InvokeDatosRegistradosActualizados(object? sender, EventArgs e) {
        EntidadRegistradaActualizada?.Invoke(sender, e);
    }

    protected virtual void Dispose(bool disposing) {
        if (_disposed)
            return;

        if (disposing)
            // Liberar recursos administrados
            if (Vista is IDisposable disposableVista)
                disposableVista.Dispose();

        Vista.RegistrarEntidad -= OnRegistrarEntidad;
        Vista.EditarEntidad -= OnEditarEntidad;

        _disposed = true;
    }

    public override void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}