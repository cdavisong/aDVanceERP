using System.Globalization;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Venta;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Venta.Plantillas;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Presentadores;

public class PresentadorGestionVentas : PresentadorVistaGestion<PresentadorTuplaVenta, IVistaGestionVentas,
    IVistaTuplaVenta, Venta, RepoVenta, FiltroBusquedaVenta> {
    public PresentadorGestionVentas(IVistaGestionVentas vista) : base(vista) {
        vista.ConfirmarEntrega += OnConfirmarEntregaAriculos;
        vista.EditarEntidad += delegate {
            Vista.HabilitarBtnConfirmarEntrega = false;
            Vista.HabilitarBtnConfirmarPagos = false;
        };
    }

    protected override PresentadorTuplaVenta ObtenerValoresTupla(Venta objeto) {
        var presentadorTupla = new PresentadorTuplaVenta(new VistaTuplaVenta(), objeto);
        var nombreCliente = UtilesCliente.ObtenerRazonSocialCliente(objeto.IdCliente) ?? string.Empty;

        presentadorTupla.Vista.Id = objeto.Id.ToString();
        presentadorTupla.Vista.Fecha = objeto.Fecha.ToString("yyyy-MM-dd");
        presentadorTupla.Vista.NombreAlmacen = UtilesAlmacen.ObtenerNombreAlmacen(objeto.IdAlmacen) ?? string.Empty;
        presentadorTupla.Vista.NombreCliente = string.IsNullOrEmpty(nombreCliente) ? "Anónimo" : nombreCliente;
        presentadorTupla.Vista.CantidadProductos = UtilesVenta.ObtenerCantidadProductosVenta(objeto.Id).ToString("N2", CultureInfo.InvariantCulture);
        presentadorTupla.Vista.MontoTotal = objeto.Total.ToString("N2", CultureInfo.InvariantCulture);
        presentadorTupla.Vista.EstadoEntrega = objeto.EstadoEntrega;

        var pagosVenta = UtilesVenta.ObtenerPagosPorVenta(objeto.Id);

        presentadorTupla.Vista.EstadoPago =
            pagosVenta.Count == 0 || pagosVenta.Any(p => !p.Split('|')[5].Equals("Confirmado"))
                ? "Pendiente"
                : "Confirmado";
        presentadorTupla.EntidadSeleccionada += CambiarVisibilidadBtnConfirmarEntrega;
        presentadorTupla.EntidadSeleccionada += CambiarVisibilidadBtnConfirmarPagos;
        presentadorTupla.EntidadDeseleccionada += CambiarVisibilidadBtnConfirmarEntrega;
        presentadorTupla.EntidadDeseleccionada += CambiarVisibilidadBtnConfirmarPagos;

        return presentadorTupla;
    }

    public override void ActualizarResultadosBusqueda() {
        // Cambiar la visibilidad de los botones de confirmación
        Vista.HabilitarBtnConfirmarEntrega = false;
        Vista.HabilitarBtnConfirmarPagos = false;

        // Actualizar el valor bruto de las ventas al refrescar la lista de objetos.
        Vista.ActualizarValorBrutoVentas();

        base.ActualizarResultadosBusqueda();
    }

    private void OnConfirmarEntregaAriculos(object? sender, EventArgs e) {
        foreach (var tupla in _tuplasEntidades)
            if (tupla.EstadoSeleccion) {
                tupla.Entidad.EstadoEntrega = "Completada";

                // Editar la venta del producto
                Repositorio.Editar(tupla.Entidad);

                // Actualizar el seguimiento de entrega
                using (var datosSeguimiento = new RepoSeguimientoEntrega()) {
                    var objetoSeguimiento = datosSeguimiento
                        .Buscar(FiltroBusquedaSeguimientoEntrega.IdVenta, tupla.Vista.Id).resultados.FirstOrDefault();

                    if (objetoSeguimiento != null) {
                        objetoSeguimiento.FechaEntrega = DateTime.Now;

                        datosSeguimiento.Editar(objetoSeguimiento);
                    }
                }

                break;
            }

        ActualizarResultadosBusqueda();
    }

    private void CambiarVisibilidadBtnConfirmarEntrega(object? sender, EventArgs e) {
        if (_tuplasEntidades.Any(t => t.EstadoSeleccion)) {
            foreach (var tupla in _tuplasEntidades)
                if (tupla.EstadoSeleccion) {
                    if (!tupla.Entidad.EstadoEntrega.Equals("Completada")) {
                        Vista.HabilitarBtnConfirmarEntrega = true;
                    }
                    else {
                        Vista.HabilitarBtnConfirmarEntrega = false;
                        return;
                    }
                }
        }
        else {
            Vista.HabilitarBtnConfirmarEntrega = false;
        }
    }

    private void CambiarVisibilidadBtnConfirmarPagos(object? sender, EventArgs e) {
        if (_tuplasEntidades.Any(t => t.EstadoSeleccion)) {
            foreach (var tupla in _tuplasEntidades)
                if (tupla.EstadoSeleccion) {
                    if (!tupla.Vista.EstadoPago.Equals("Confirmado")) {
                        Vista.HabilitarBtnConfirmarPagos = true;
                    }
                    else {
                        Vista.HabilitarBtnConfirmarPagos = false;
                        return;
                    }
                }
        }
        else {
            Vista.HabilitarBtnConfirmarPagos = false;
        }
    }
}