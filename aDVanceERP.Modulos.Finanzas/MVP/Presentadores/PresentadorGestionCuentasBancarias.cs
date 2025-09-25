using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.CuentaBancaria;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.CuentaBancaria.Plantillas;

namespace aDVanceERP.Modulos.Finanzas.MVP.Presentadores;

public class PresentadorGestionCuentasBancarias : PresentadorVistaGestion<PresentadorTuplaCuentaBancaria, IVistaGestionCuentasBancarias, IVistaTuplaCuentaBancaria, CuentaBancaria, RepoCuentaBancaria, FiltroBusquedaCuentaBancaria> {
    public PresentadorGestionCuentasBancarias(IVistaGestionCuentasBancarias vista) : base(vista) { }

    public event EventHandler? MostrarQrTupla;

    protected override PresentadorTuplaCuentaBancaria ObtenerValoresTupla(CuentaBancaria objeto) {
        var presentadorTupla = new PresentadorTuplaCuentaBancaria(new VistaTuplaCuentaBancaria(), objeto);

        presentadorTupla.Vista.Id = objeto.Id.ToString();
        presentadorTupla.Vista.Alias = objeto.Alias ?? string.Empty;
        presentadorTupla.Vista.NumeroTarjeta = objeto.NumeroTarjeta ?? string.Empty;
        presentadorTupla.Vista.Moneda = objeto.Moneda.ToString();
        presentadorTupla.Vista.NombrePropietario = UtilesContacto.ObtenerNombreContacto(objeto.IdContacto) ?? string.Empty;
        presentadorTupla.Vista.MostrarQR += delegate(object? sender, EventArgs args) {
            MostrarQrTupla?.Invoke(sender, args);
        };

        return presentadorTupla;
    }
}