using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Desktop.Utiles;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos;
using aDVanceERP.Modulos.Finanzas.MVP.Presentadores;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.CuentaBancaria;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos; 

public partial class PresentadorModulos {
    private PresentadorRegistroCuentaBancaria? _registroCuentaBancaria;

    private void InicializarVistaRegistroCuentaBancaria() {
        _registroCuentaBancaria = new PresentadorRegistroCuentaBancaria(new VistaRegistroCuentaBancaria());
        _registroCuentaBancaria.Vista.CargarNombresContactos(UtilesContacto.ObtenerNombresContactos());
        _registroCuentaBancaria.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
        _registroCuentaBancaria.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
        _registroCuentaBancaria.EntidadRegistradaActualizada += delegate {
            if (_gestionCuentasBancarias == null)
                return;

            _gestionCuentasBancarias.ActualizarResultadosBusqueda();
        };
    }

    private void MostrarVistaRegistroCuentaBancaria(object? sender, EventArgs e) {
        InicializarVistaRegistroCuentaBancaria();

        if (_registroCuentaBancaria == null) 
            return;

        _registroCuentaBancaria.Vista.Mostrar();
        _registroCuentaBancaria.Dispose();
    }

    private void MostrarVistaEdicionCuentaBancaria(object? sender, EventArgs e) {
        InicializarVistaRegistroCuentaBancaria();

        if (sender is CuentaBancaria cuentaBancaria) {
            if (_registroCuentaBancaria != null) {
                _registroCuentaBancaria.PopularVistaDesdeEntidad(cuentaBancaria);
                _registroCuentaBancaria.Vista.Mostrar();
            }
        }

        _registroCuentaBancaria?.Dispose();
    }
}