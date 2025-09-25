using System.Globalization;

using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Pago.Plantillas;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Presentadores;

public class
    PresentadorRegistroPago : PresentadorVistaRegistro<IVistaRegistroPago, Pago, RepoPago, FiltroBusquedaPago> {
    public PresentadorRegistroPago(IVistaRegistroPago vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(Pago objeto) {
        Vista.ModoEdicion = true;
        Vista.IdVenta = objeto.IdVenta;
        Vista.Total = objeto.Monto;

        var pagos = UtilesVenta.ObtenerPagosPorVenta(objeto.IdVenta);

        foreach (var pago in pagos) {
            var pagoSplit = pago.Split('|');
            ((IVistaGestionPagos)Vista).AdicionarPago(long.Parse(pagoSplit[0]),
                long.Parse(pagoSplit[1]),
                pagoSplit[2],
                decimal.TryParse(pagoSplit[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var monto) ? monto : 0.00m);
        }

        _entidad = objeto;
    }

    protected override bool EntidadCorrecta() {
        var indice = 1;

        foreach (var pago in Vista.Pagos) {
            var montoOk = (decimal.TryParse(pago[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var monto) ? monto : 0.00m) > 0;

            if (!montoOk) {
                CentroNotificaciones.Mostrar($"El pago registrado en el índice {indice} tiene un monto menor o igual a cero, corrija los datos para registrar los pagos de forma correcta", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
                return false;
            }

            indice++;
        };

        return true;
    }

    protected override void OnRegistrarEntidad(object? sender, EventArgs e) {
        RegistrarEditarRepoPagos(sender, e);
    }

    protected override void OnEditarEntidad(object? sender, EventArgs e) {
        RegistrarEditarRepoPagos(sender, e);
    }

    private void RegistrarEditarRepoPagos(object? sender, EventArgs e) {
        var objetosVista = ObtenerObjetosDesdeVista();
        
        if (!EntidadCorrecta())
            return;
        
        foreach (var objeto in objetosVista) {
            if (Vista.ModoEdicion && objeto.Id != 0)
                Repositorio.Editar(Entidad);
            else if (objeto.Id != 0)
                Repositorio.Editar(Entidad);
            else
                objeto.Id = Repositorio.Adicionar(objeto);            
        };

        InvokeDatosRegistradosActualizados(objetosVista, e);
        Dispose();
        Vista.Cerrar();
    }

    private List<Pago> ObtenerObjetosDesdeVista() {
        var pagos = new List<Pago>();

        foreach (var pago in Vista.Pagos) {
            var objetoPago = new Pago(long.TryParse(pago[0], out var idPago) ? idPago : 0,
                Vista.IdVenta,
                pago[2],
                decimal.TryParse(pago[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var monto)
                    ? monto
                    : 0.00m) {
                FechaConfirmacion = DateTime.Now,
                Estado = "Confirmado"
            };

            pagos.Add(objetoPago);
        };

        return pagos;
    }

    protected override Pago? ObtenerEntidadDesdeVista() {
        throw new NotImplementedException();
    }    
}