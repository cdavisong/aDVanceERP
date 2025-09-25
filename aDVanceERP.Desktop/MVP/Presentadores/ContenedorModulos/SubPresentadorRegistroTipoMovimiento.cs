using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Desktop.Utiles;
using aDVanceERP.Modulos.Inventario.MVP.Presentadores;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.TipoMovimiento;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
    private PresentadorRegistroTipoMovimiento? _registroTipoMovimiento;

    private Task InicializarVistaRegistroTipoMovimiento() {
        var tiposMovimiento = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Todos, string.Empty).resultados;

        _registroTipoMovimiento = new PresentadorRegistroTipoMovimiento(new VistaRegistroTipoMovimiento());
        _registroTipoMovimiento.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
        _registroTipoMovimiento.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
        _registroTipoMovimiento.EntidadRegistradaActualizada += delegate {
            _registroMovimiento?.Vista.CargarTiposMovimientos(tiposMovimiento.Select(tm => tm.Nombre).ToArray());
        };

        return Task.CompletedTask;
    }

    private async void MostrarVistaRegistroTipoMovimiento(object? sender, EventArgs e) {
        await InicializarVistaRegistroTipoMovimiento();

        _registroTipoMovimiento?.Vista.Mostrar();
        _registroTipoMovimiento?.Dispose();
    }

    private void EliminarTipoMovimiento(object? sender, EventArgs e) {
        var repoTipoMovimiento = RepoTipoMovimiento.Instancia;
        var tiposMovimiento = repoTipoMovimiento.Buscar(FiltroBusquedaTipoMovimiento.Todos, string.Empty).resultados;

        if (sender is string nombreTipoMovimiento) {
            var tipoMovimiento = tiposMovimiento.FirstOrDefault(tm => tm.Nombre.Equals(nombreTipoMovimiento));

            if (tipoMovimiento == null)
                return;

            repoTipoMovimiento.Eliminar(tipoMovimiento.Id);
        }

        _registroMovimiento?.Vista.CargarTiposMovimientos(
             Signo.Equals("+")
                ? tiposMovimiento.Where(tm => tm.Efecto == EfectoMovimiento.Carga).Select(tm => tm.Nombre).ToArray()
                : Signo.Equals("-")
                    ? tiposMovimiento.Where(tm => tm.Efecto == EfectoMovimiento.Descarga).Select(tm => tm.Nombre).ToArray()
                    : tiposMovimiento.Select(tm => tm.Nombre).ToArray()
        );
    }
}