using aDVanceERP.Modulos.Finanzas.MVP.Modelos;
using aDVanceERP.Modulos.Finanzas.MVP.Presentadores;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.CuentaBancaria;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
    private PresentadorGestionCuentasBancarias? _gestionCuentasBancarias;

    private async void InicializarVistaGestionCuentasBancarias() {
        _gestionCuentasBancarias = new PresentadorGestionCuentasBancarias(new VistaGestionCuentasBancarias());
        _gestionCuentasBancarias.MostrarQrTupla += MostrarVistaQR;
        _gestionCuentasBancarias.EditarEntidad += MostrarVistaEdicionCuentaBancaria;
        _gestionCuentasBancarias.Vista.RegistrarEntidad += MostrarVistaRegistroCuentaBancaria;

        Vista.PanelCentral.Registrar(_gestionCuentasBancarias.Vista);
    }

    private void MostrarVistaGestionCuentasBancarias(object? sender, EventArgs e) {
        if (_gestionCuentasBancarias?.Vista == null)
            return;

        _gestionCuentasBancarias.Vista.CargarFiltrosBusqueda(UtilesBusquedaCuentaBancaria.FiltroBusquedaCuentaBancaria);
        _gestionCuentasBancarias.Vista.Restaurar();
        _gestionCuentasBancarias.Vista.Mostrar();

        _gestionCuentasBancarias.ActualizarResultadosBusqueda();
    }
}