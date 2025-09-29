using System.Globalization;

using aDVanceERP.Core.Modelos.Modulos.Compraventa;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Core.Repositorios.Modulos.Compraventa;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Venta;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Venta.Plantillas;
using aDVanceERP.Core.Seguridad.Utiles;

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
        var seguimientoEntrega = RepoSeguimientoEntrega.Instancia.Buscar(FiltroBusquedaSeguimientoEntrega.IdVenta, objeto.Id.ToString()).entidades.FirstOrDefault();
        var historialEntrega = RepoHistorialEntrega.Instancia.Buscar(FiltroBusquedaHistorialEntrega.IdSeguimientoEntrega, seguimientoEntrega?.Id.ToString() ?? "0").entidades;
        var repoEstadoEntrega = RepoEstadoEntrega.Instancia;

        presentadorTupla.Vista.Id = objeto.Id.ToString();
        presentadorTupla.Vista.Fecha = objeto.Fecha.ToString("yyyy-MM-dd");
        presentadorTupla.Vista.NombreAlmacen = UtilesAlmacen.ObtenerNombreAlmacen(objeto.IdAlmacen) ?? string.Empty;
        presentadorTupla.Vista.NombreCliente = string.IsNullOrEmpty(nombreCliente) ? "Anónimo" : nombreCliente;
        presentadorTupla.Vista.CantidadProductos = UtilesVenta.ObtenerCantidadProductosVenta(objeto.Id).ToString("N2", CultureInfo.InvariantCulture);
        presentadorTupla.Vista.MontoTotal = objeto.Total.ToString("N2", CultureInfo.InvariantCulture);
        presentadorTupla.Vista.EstadoEntrega = historialEntrega.Count > 0 ?
            repoEstadoEntrega.Buscar(FiltroBusquedaEstadoEntrega.Id, historialEntrega.OrderByDescending(h => h.FechaRegistro).First().IdEstadoEntrega.ToString()).entidades.FirstOrDefault()?.Nombre ?? "Desconocido" :
            "Pendiente";

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
                var seguimientoEntrega = RepoSeguimientoEntrega.Instancia.Buscar(FiltroBusquedaSeguimientoEntrega.IdVenta, tupla.Entidad.Id.ToString()).entidades.FirstOrDefault();

                if (seguimientoEntrega != null) {
                    var historialEntrega = RepoHistorialEntrega.Instancia.Buscar(FiltroBusquedaHistorialEntrega.IdSeguimientoEntrega, seguimientoEntrega.Id.ToString()).entidades;
                    var ultimoEstadoEntrega = historialEntrega.Count > 0
                        ? historialEntrega.OrderByDescending(h => h.FechaRegistro).FirstOrDefault()
                        : null;

                    if (ultimoEstadoEntrega != null) {
                        var estadosEntrega = RepoEstadoEntrega.Instancia.Buscar(FiltroBusquedaEstadoEntrega.Todos, string.Empty).entidades;
                        var estadoCompletado = estadosEntrega.FirstOrDefault(e => e.Nombre.Equals("Entregado"));

                        if (estadoCompletado != null && !ultimoEstadoEntrega.IdEstadoEntrega.Equals(estadoCompletado.Id)) {
                            var nuevoHistorial = new HistorialEntrega(0,
                                seguimientoEntrega.Id,
                                estadoCompletado.Id,
                                DateTime.Now,
                                UtilesCuentaUsuario.UsuarioAutenticado?.Id ?? 0,
                                "Entrega confirmada desde la gestión de ventas."
                                );

                            RepoHistorialEntrega.Instancia.Adicionar(nuevoHistorial);
                        }
                    } else {
                        var estadosEntrega = RepoEstadoEntrega.Instancia.Buscar(FiltroBusquedaEstadoEntrega.Todos, string.Empty).entidades;
                        var estadoCompletado = estadosEntrega.FirstOrDefault(e => e.Nombre.Equals("Entregado"));

                        if (estadoCompletado != null) {
                            var nuevoHistorial = new HistorialEntrega(0,
                                seguimientoEntrega.Id,
                                estadoCompletado.Id,
                                DateTime.Now,
                                UtilesCuentaUsuario.UsuarioAutenticado?.Id ?? 0,
                                "Entrega confirmada desde la gestión de ventas."
                                );

                            RepoHistorialEntrega.Instancia.Adicionar(nuevoHistorial);
                        }
                    }
                } else {
                    var tuplaSeleccionada = _tuplasEntidades.FirstOrDefault(t => t.EstadoSeleccion);

                    if (tuplaSeleccionada == null)
                        continue;

                    // Si no existe un seguimiento de entrega, crear uno nuevo
                    var nuevoSeguimiento = new SeguimientoEntrega(0,
                        tuplaSeleccionada.Entidad.Id,
                        0,
                        DateTime.Now,
                        DateTime.Now,
                        DateTime.Now,
                        "Seguimiento de entrega creado desde la gestión de ventas."
                        );

                    var idNuevoSeguimiento = RepoSeguimientoEntrega.Instancia.Adicionar(nuevoSeguimiento);
                    var estadosEntrega = RepoEstadoEntrega.Instancia.Buscar(FiltroBusquedaEstadoEntrega.Todos, string.Empty).entidades;
                    var estadoCompletado = estadosEntrega.FirstOrDefault(e => e.Nombre.Equals("Entregado"));

                    if (estadoCompletado != null) {
                        var nuevoHistorial = new HistorialEntrega(0,
                            idNuevoSeguimiento,
                            estadoCompletado.Id,
                            DateTime.Now,
                            UtilesCuentaUsuario.UsuarioAutenticado?.Id ?? 0,
                            "Entrega confirmada desde la gestión de ventas."
                            );

                        RepoHistorialEntrega.Instancia.Adicionar(nuevoHistorial);
                    }
                }
            }

        ActualizarResultadosBusqueda();
    }

    private void CambiarVisibilidadBtnConfirmarEntrega(object? sender, EventArgs e) {
        if (_tuplasEntidades.Any(t => t.EstadoSeleccion)) {
            foreach (var tupla in _tuplasEntidades)
                if (tupla.EstadoSeleccion) {
                    var seguimientoEntrega = RepoSeguimientoEntrega.Instancia.Buscar(FiltroBusquedaSeguimientoEntrega.IdVenta, tupla.Entidad.Id.ToString()).entidades.FirstOrDefault();

                    if (seguimientoEntrega != null) {
                        var historialEntrega = RepoHistorialEntrega.Instancia.Buscar(FiltroBusquedaHistorialEntrega.IdSeguimientoEntrega, seguimientoEntrega.Id.ToString()).entidades;
                        var ultimoEstadoEntrega = historialEntrega.OrderByDescending(h => h.FechaRegistro).FirstOrDefault();
                        var estadosEntrega = RepoEstadoEntrega.Instancia.Buscar(FiltroBusquedaEstadoEntrega.Todos, string.Empty).entidades;
                        var estadoCompletado = estadosEntrega.FirstOrDefault(e => e.Nombre.Equals("Entregado"));

                        if (historialEntrega.Count == 0 || ultimoEstadoEntrega == null || estadoCompletado != null && !ultimoEstadoEntrega.IdEstadoEntrega.Equals(estadoCompletado.Id)) {
                            Vista.HabilitarBtnConfirmarEntrega = true;
                        } else {
                            Vista.HabilitarBtnConfirmarEntrega = false;
                            return;
                        }
                    } else {
                        Vista.HabilitarBtnConfirmarEntrega = true;
                        return;
                    }
                }
        } else {
            Vista.HabilitarBtnConfirmarEntrega = false;
        }
    }

    private void CambiarVisibilidadBtnConfirmarPagos(object? sender, EventArgs e) {
        if (_tuplasEntidades.Any(t => t.EstadoSeleccion)) {
            foreach (var tupla in _tuplasEntidades)
                if (tupla.EstadoSeleccion) {
                    if (!tupla.Vista.EstadoPago.Equals("Confirmado")) {
                        Vista.HabilitarBtnConfirmarPagos = true;
                    } else {
                        Vista.HabilitarBtnConfirmarPagos = false;
                        return;
                    }
                }
        } else {
            Vista.HabilitarBtnConfirmarPagos = false;
        }
    }
}