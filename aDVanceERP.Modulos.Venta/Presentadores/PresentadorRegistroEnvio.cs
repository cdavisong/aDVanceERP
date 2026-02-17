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

            // Carga inicial de datos
            var tiposEnvio = new List<string>();

            foreach (TipoEnvioEnum metodo in Enum.GetValues(typeof(TipoEnvioEnum)))
                tiposEnvio.Add(metodo.ObtenerDisplayName());

            Vista.CargarFacturasVentasPendientes([.. RepoVenta.Instancia.ObtenerVentasPendientesDePago().Select(v => v.NumeroFacturaTicket)]);
            Vista.CargarTiposEnvio([.. tiposEnvio]);

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

            // Carga inicial de datos
            var tiposEnvio = new List<string>();

            foreach (TipoEnvioEnum metodo in Enum.GetValues(typeof(TipoEnvioEnum)))
                tiposEnvio.Add(metodo.ObtenerDisplayName());

            Vista.CargarFacturasVentasPendientes([.. RepoVenta.Instancia.ObtenerVentasPendientesDePago().Select(v => v.NumeroFacturaTicket)]);
            Vista.CargarTiposEnvio([.. tiposEnvio]);

            PopularVistaDesdeEntidad(envio);

            Vista.Mostrar();
        }

        protected override SeguimientoEntrega? ObtenerEntidadDesdeVista() {
            throw new NotImplementedException();
        }
    }
}
