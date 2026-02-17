using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    internal class PresentadorRegistroEnvio : PresentadorVistaRegistro<IVistaRegistroEnvio, SeguimientoEntrega, RepoSeguimientoEntrega, FiltroBusquedaSeguimientoEntrega> {
        public PresentadorRegistroEnvio(IVistaRegistroEnvio vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroEnvio", OnMostrarVistaRegistroEnvio);
            AgregadorEventos.Suscribir("MostrarVistaEdicionEnvio", OnMostrarVistaEdicionEnvio);
        }

        private void OnMostrarVistaRegistroEnvio(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();
            Vista.Mostrar();
        }

        private void OnMostrarVistaEdicionEnvio(string obj) {
            Vista.ModoEdicion = true;
            Vista.Restaurar();

            if (string.IsNullOrEmpty(obj))
                return;

            var envio = AgregadorEventos.DeserializarPayload<SeguimientoEntrega>(obj);

            if (envio == null)
                return;

            PopularVistaDesdeEntidad(envio);

            Vista.Mostrar();
        }

        protected override SeguimientoEntrega? ObtenerEntidadDesdeVista() {
            throw new NotImplementedException();
        }
    }
}
