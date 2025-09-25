using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.MVP.Presentadores;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Movimiento;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
    private PresentadorGestionMovimientos? _gestionMovimientos;

    private async void InicializarVistaGestionMovimientos() {
        _gestionMovimientos = new PresentadorGestionMovimientos(new VistaGestionMovimientos());
        _gestionMovimientos.EditarEntidad += MostrarVistaEdicionMovimiento;
        _gestionMovimientos.Vista.RegistrarEntidad += MostrarVistaRegistroMovimiento;

        Vista.PanelCentral.Registrar(_gestionMovimientos.Vista);
    }

    private void MostrarVistaGestionMovimientos(object? sender, EventArgs e) {
        if (_gestionMovimientos?.Vista == null)
            return;

        _gestionMovimientos.Vista.CargarFiltrosBusqueda(UtilesBusquedaMovimiento.FiltroBusquedaMovimiento);
        _gestionMovimientos.Vista.Restaurar();
        _gestionMovimientos.Vista.Mostrar();

        _gestionMovimientos.ActualizarResultadosBusqueda();
    }
}