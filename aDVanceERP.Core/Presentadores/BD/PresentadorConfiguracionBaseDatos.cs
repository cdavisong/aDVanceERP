using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.BD;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Core.Vistas.BD;

namespace aDVanceERP.Core.Presentadores.BD
{
    public class PresentadorConfiguracionBaseDatos : PresentadorVistaBase<VistaConfiguracionBaseDatos> {
        private readonly RepoConfiguracionBaseDatos _repositorio;

        public PresentadorConfiguracionBaseDatos(VistaConfiguracionBaseDatos vista, RepoConfiguracionBaseDatos repositorio) : base(vista) {
            _repositorio = repositorio;

            Vista.AlmacenarConfiguracion += OnAlmacenarConfiguracion;
        }

        public RepoConfiguracionBaseDatos Repositorio => _repositorio;

        public event EventHandler? ConfiguracionCargada;

        public void CargarConfiguracion() {
            var config = Repositorio.ObtenerPorId(0);

            if (config != null) {
                Vista.NombreDireccionServidor = config.Servidor;
                Vista.NombreBaseDatos = config.BaseDatos;
                Vista.NombreUsuario = config.Usuario;
                Vista.Password = config.Password;
                Vista.RecordarConfiguracion = config.RecordarConfiguracion;

                // Actualizar el contexto global
                try {
                    ContextoBaseDatos.ActualizarConfiguracion(config);
                } catch (InvalidOperationException) {
                    return;
                }

                // Disparar el evento de configuración cargada
                ConfiguracionCargada?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnAlmacenarConfiguracion(object? sender, ConfiguracionBaseDatos e) {
            if (e == null) {
                throw new ArgumentNullException(nameof(e), "La configuración del servidor MySQL no puede ser nula.");
            }
            try {
                // Actualizar el contexto global
                try {
                    ContextoBaseDatos.ActualizarConfiguracion(e);
                } catch (InvalidOperationException) {
                    return;
                }

                // Guardar la configuración utilizando el repositorio
                Repositorio.Salvar(string.Empty, e);

                // Disparar el evento de configuración cargada
                ConfiguracionCargada?.Invoke(this, EventArgs.Empty);
            } catch (Exception ex) {
                // Manejo de excepciones, por ejemplo, registrar el error o mostrar un mensaje al usuario
                throw new InvalidOperationException("Error al guardar la configuración del servidor MySQL.", ex);
            }
        }

        public override void Dispose() {
            Vista.AlmacenarConfiguracion -= OnAlmacenarConfiguracion;
        }
    }
}