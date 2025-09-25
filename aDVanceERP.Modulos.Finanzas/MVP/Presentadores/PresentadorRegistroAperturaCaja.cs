using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja.Plantillas;

namespace aDVanceERP.Modulos.Finanzas.MVP.Presentadores
{
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
