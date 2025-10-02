using aDVanceERP.Core.Modelos.Modulos.Finanzas;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Finanzas;
using aDVanceERP.Modulos.Seguridad.Utiles;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja.Plantillas;

namespace aDVanceERP.Modulos.Finanzas.MVP.Presentadores {
    public class PresentadorRegistroAperturaCaja : PresentadorVistaRegistro<IVistaRegistroAperturaCaja, Caja, RepoCaja, FiltroBusquedaCaja> {
        public PresentadorRegistroAperturaCaja(IVistaRegistroAperturaCaja vista) 
            : base(vista) { }

        public override void PopularVistaDesdeEntidad(Caja objeto) {
            Vista.ModoEdicion = true;
            Vista.Fecha = objeto.FechaApertura;
            Vista.SaldoInicial = objeto.SaldoInicial;

            _entidad = objeto;
        }

        protected override Caja ObtenerEntidadDesdeVista() {
            return new Caja(
                Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
                Vista.Fecha,
                Vista.SaldoInicial,
                Vista.SaldoInicial,
                DateTime.MinValue,
                UtilesCuentaUsuario.UsuarioAutenticado?.Id ?? 0
            ) {
                Estado = EstadoCaja.Abierta
            };
        }
    }
}
