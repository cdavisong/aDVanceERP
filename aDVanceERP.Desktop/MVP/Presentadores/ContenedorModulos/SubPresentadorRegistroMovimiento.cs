using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Desktop.Utiles;
using aDVanceERP.Modulos.Inventario.MVP.Presentadores;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Movimiento;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
    private PresentadorRegistroMovimiento? _registroMovimiento;

    private string? Signo { get; set; }

    private async Task InicializarVistaRegistroMovimiento() {
        _registroMovimiento = new PresentadorRegistroMovimiento(new VistaRegistroMovimiento());
        _registroMovimiento.Vista.CargarNombresProductos(await UtilesProducto.ObtenerNombresProductos());
        _registroMovimiento.Vista.CargarNombresAlmacenes(UtilesAlmacen.ObtenerNombresAlmacenes());
        _registroMovimiento.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
        _registroMovimiento.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
        _registroMovimiento.Vista.RegistrarTipoMovimiento += MostrarVistaRegistroTipoMovimiento;
        _registroMovimiento.Vista.EliminarTipoMovimiento += EliminarTipoMovimiento;
        _registroMovimiento.EntidadRegistradaActualizada += delegate {
            if (_gestionMovimientos == null)
                return;

            _gestionMovimientos.ActualizarResultadosBusqueda(); 

            if (_gestionProductos == null) 
                return;

            _gestionProductos.ActualizarResultadosBusqueda();
        };
    }

    private async Task InicializarVistaRegistroMovimiento(string? signo, string? nombreAlmacen) {
        await InicializarVistaRegistroMovimiento();

        var tiposMovimiento = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Todos, string.Empty).resultados;

        Signo = signo ?? string.Empty;

        if (_registroMovimiento != null) {
            _registroMovimiento.Vista.CargarTiposMovimientos(
                    Signo.Equals("+")
                        ? tiposMovimiento.Where(tm => tm.Efecto == EfectoMovimiento.Carga).Select(tm => tm.Nombre).ToArray()
                        : Signo.Equals("-")
                            ? tiposMovimiento.Where(tm => tm.Efecto == EfectoMovimiento.Descarga).Select(tm => tm.Nombre).ToArray()
                            : tiposMovimiento.Select(tm => tm.Nombre).ToArray()
                );

            if (nombreAlmacen != null && !nombreAlmacen.Equals("-")) {
                _registroMovimiento.Vista.NombreAlmacenOrigen = Signo.Equals("-") ? nombreAlmacen : "Ninguno";
                _registroMovimiento.Vista.NombreAlmacenDestino = Signo.Equals("+") ? nombreAlmacen : "Ninguno";
            }
        }
    }

    private async Task InicializarVistaRegistroMovimiento(string? signo, string? nombreAlmacen, Producto? objeto) {
        await InicializarVistaRegistroMovimiento(signo, nombreAlmacen);

        Signo = signo ?? string.Empty;

        if (_registroMovimiento != null) _registroMovimiento.Vista.NombreProducto = objeto?.Nombre ?? string.Empty;
    }

    private async void MostrarVistaRegistroMovimiento(object? sender, EventArgs e) {
        if (sender is object[] datosAlmacenProducto)
            await InicializarVistaRegistroMovimiento(datosAlmacenProducto[0].ToString(),
                datosAlmacenProducto[1].ToString(), datosAlmacenProducto[2] as Producto);
        else
            await InicializarVistaRegistroMovimiento(string.Empty, string.Empty);

        if (_registroMovimiento == null) 
            return;

        _registroMovimiento.Vista.Mostrar();
        _registroMovimiento.Dispose();
    }

    private async void MostrarVistaEdicionMovimiento(object? sender, EventArgs e) {
        await InicializarVistaRegistroMovimiento(string.Empty, string.Empty);

        if (sender is Movimiento movimiento) {
            if (_registroMovimiento != null) {
                _registroMovimiento.PopularVistaDesdeEntidad(movimiento);
                _registroMovimiento.Vista.Mostrar();
            }
        }

        _registroMovimiento?.Dispose();
    }
}