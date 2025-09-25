using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja.Plantillas;

namespace aDVanceERP.Modulos.Finanzas.MVP.Presentadores;

public class PresentadorRegistroMovimientoCaja : PresentadorVistaRegistro<IVIstaRegistroMovimientoCaja, MovimientoCaja, RepoMovimientoCaja, FiltroBusquedaMovimientoCaja> {
    public PresentadorRegistroMovimientoCaja(IVIstaRegistroMovimientoCaja vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(MovimientoCaja objeto) {
        Vista.ModoEdicion = true;
        Vista.Fecha = objeto.Fecha;
        Vista.Monto = objeto.Monto;
        Vista.TipoMovimiento = objeto.Tipo.ToString();
        Vista.Concepto = objeto.Concepto ?? string.Empty;
        Vista.Observaciones = objeto.Observaciones;

        _entidad = objeto;
    }

    protected override MovimientoCaja? ObtenerEntidadDesdeVista() {
        return new MovimientoCaja(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            UtilesCaja.ObtenerIdCajaActiva(),
            Vista.Fecha,
            Vista.Monto,
            Enum.Parse<TipoMovimientoCaja>(Vista.TipoMovimiento),
            Vista.Concepto,
            0,
            UtilesCuentaUsuario.UsuarioAutenticado?.Id ?? 0,
            Vista.Observaciones
        );
    }
}