using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Desktop.Utiles;
using aDVanceERP.Modulos.Inventario.MVP.Presentadores;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.TipoMateriaPrima;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
    private PresentadorRegistroTipoMateriaPrima? _registroTipoMateriaPrima;

    private Task InicializarVistaRegistroTipoMateriaPrima() {
        _registroTipoMateriaPrima = new PresentadorRegistroTipoMateriaPrima(new VistaRegistroTipoMateriaPrima());
        _registroTipoMateriaPrima.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
        _registroTipoMateriaPrima.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
        _registroTipoMateriaPrima.EntidadRegistradaActualizada += delegate {
            _registroProducto?.Vista.CargarTiposMateriaPrima(UtilesTipoMateriaPrima.ObtenerNombresTiposMateriasPrimas());
        };

        return Task.CompletedTask;
    }

    private async void MostrarVistaRegistroTipoMateriaPrima(object? sender, EventArgs e) {
        await InicializarVistaRegistroTipoMateriaPrima();

        _registroTipoMateriaPrima?.Vista.Mostrar();
        _registroTipoMateriaPrima?.Dispose();
    }

    private void EliminarTipoMateriaPrima(object? sender, EventArgs e) {
        using (var tipoProducto = new RepoTipoMateriaPrima()) {
            if (sender is string nombreTipoMateriaPrima) {
                var idTipoMateriaPrima = UtilesTipoMateriaPrima.ObtenerIdTipoMateriaPrima(nombreTipoMateriaPrima).Result;

                if (idTipoMateriaPrima == 0)
                    return;

                tipoProducto.Eliminar(idTipoMateriaPrima);
            }

            _registroProducto?.Vista.CargarTiposMateriaPrima(UtilesTipoMateriaPrima.ObtenerNombresTiposMateriasPrimas());
        }
    }
}
