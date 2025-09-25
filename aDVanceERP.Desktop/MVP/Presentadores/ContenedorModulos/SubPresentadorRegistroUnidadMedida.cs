using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Desktop.Utiles;
using aDVanceERP.Modulos.Inventario.MVP.Presentadores;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.UnidadMedida;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
    private PresentadorRegistroUnidadMedida? _registroUnidadMedida;

    private Task InicializarVistaRegistroUnidadMedida() {
        _registroUnidadMedida = new PresentadorRegistroUnidadMedida(new VistaRegistroUnidadMedida());
        _registroUnidadMedida.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
        _registroUnidadMedida.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
        _registroUnidadMedida.EntidadRegistradaActualizada += delegate {
            _registroProducto?.Vista.CargarUnidadesMedida(UtilesUnidadMedida.ObtenerUnidadesMedida());
        };

        return Task.CompletedTask;
    }

    private async void MostrarVistaRegistroUnidadMedida(object? sender, EventArgs e) {
        await InicializarVistaRegistroUnidadMedida();

        _registroUnidadMedida?.Vista.Mostrar();
        _registroUnidadMedida?.Dispose();
    }

    private void EliminarUnidadMedida(object? sender, EventArgs e) {
        using (var unidadMedida = new RepoUnidadMedida()) {
            if (sender is string nombreUnidadMedida) {
                var idUnidadMedida = UtilesUnidadMedida.ObtenerIdUnidadMedida(nombreUnidadMedida).Result;

                if (idUnidadMedida == 0)
                    return;

                unidadMedida.Eliminar(idUnidadMedida);
            }

            _registroProducto?.Vista.CargarUnidadesMedida(UtilesUnidadMedida.ObtenerUnidadesMedida());
        }
    }
}