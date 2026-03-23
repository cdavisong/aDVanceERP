using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Repositorios.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Presentadores.Comun {
    public abstract class PresentadorVistaRegistro<Vr, En, Re, Fb> : PresentadorVistaBase<Vr>, IPresentadorVistaRegistro<Vr, Re, En, Fb>
        where Vr : class, IVistaRegistro
        where Re : class, IRepoEntidadBaseDatos<En, Fb>, new()
        where En : class, IEntidadBaseDatos, new()
        where Fb : Enum {
        private bool _disposed; // Para evitar llamadas redundantes a Dispose

        protected En? _entidad = null!;
        protected Re _repositorio = new();

        protected PresentadorVistaRegistro(Vr vista) : base(vista) {
            Vista.RegistrarEntidad += OnRegistrarEntidad;
            Vista.EditarEntidad += OnEditarEntidad;
        }

        public En? Entidad => _entidad;

        public Re Repositorio => _repositorio;

        public event EventHandler? EntidadRegistradaActualizada;
        public event EventHandler? Salir;

        public virtual void PopularVistaDesdeEntidad(En entidad) {
            _entidad = entidad;
        }

        protected abstract En? ObtenerEntidadDesdeVista();

        protected virtual bool EntidadCorrecta() {
            return true;
        }

        protected virtual void RegistroEdicionAuxiliar(Re repositorio, long id) { }

        protected virtual void OnRegistrarEntidad(object? sender, EventArgs e) {
            RegistrarEditarEntidad(sender, e);
        }

        protected virtual void OnEditarEntidad(object? sender, EventArgs e) {
            RegistrarEditarEntidad(sender, e);
        }

        private void RegistrarEditarEntidad(object? sender, EventArgs e) {
            if (!EntidadCorrecta())
                return;

            try {
                var entidad = ObtenerEntidadDesdeVista();

                if (entidad != null) {
                    if (Vista.ModoEdicion) {
                        if (_entidad != null) {
                            entidad.Id = _entidad.Id;

                            Repositorio.Editar(entidad);
                        }
                    } else
                        entidad.Id = Repositorio.Adicionar(entidad);

                    // Actualizar la entidad global
                    _entidad = entidad;
                }

                RegistroEdicionAuxiliar(_repositorio, entidad?.Id ?? 0);

                EntidadRegistradaActualizada?.Invoke(sender, e);
                Salir?.Invoke(sender, e);

                Vista.ModoEdicion = false;
                Vista.Ocultar();

                // Limpiar entidad después de la edición o registro 
                _entidad = null;
            } catch (ArgumentException ex) {
                CentroNotificaciones.MostrarNotificacion(ex.Message, TipoNotificacionEnum.Advertencia);
            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion($"Error al registrar o editar la entidad: {ex.Message}", TipoNotificacionEnum.Error);
            }
        }

        private void OnSalir(object? sender, EventArgs e) {
            Salir?.Invoke(sender, e);

            Vista.ModoEdicion = false;
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
}