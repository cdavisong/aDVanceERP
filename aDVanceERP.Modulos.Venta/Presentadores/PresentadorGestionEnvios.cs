using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;
using aDVanceERP.Modulos.Venta.Vistas;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    internal class PresentadorGestionEnvios : PresentadorVistaGestion<PresentadorTuplaEnvio, IVistaGestionEnvios, IVistaTuplaEnvio, SeguimientoEntrega, RepoSeguimientoEntrega, FiltroBusquedaSeguimientoEntrega> {
        private List<VentaPendientePago> _ventasPendientesPago = new List<VentaPendientePago>();

        public PresentadorGestionEnvios(IVistaGestionEnvios vista) : base(vista) {
            RegistrarEntidad += OnRegistrarEnvio;
            EditarEntidad += OnEditarEnvio;

            AgregadorEventos.Suscribir("MostrarVistaGestionEnvios", OnMostrarVistaGestionEnvios);
        }

        private void OnRegistrarEnvio(object? sender, EventArgs e) {
            _ventasPendientesPago = RepoVenta.Instancia.ObtenerVentasPendientesDePago();

            if (_ventasPendientesPago.Count == 0) {
                CentroNotificaciones.MostrarNotificacion("No es posible registrar un nuevo envío puesto que no existen ventas pendientes en el sistema.", TipoNotificacion.Advertencia);
                return;
            }

            AgregadorEventos.Publicar("MostrarVistaRegistroEnvio", string.Empty);
        }

        private void OnEditarEnvio(object? sender, SeguimientoEntrega e) {
            _ventasPendientesPago = RepoVenta.Instancia.ObtenerVentasPendientesDePago();

            AgregadorEventos.Publicar("MostrarVistaEdicionEnvio", AgregadorEventos.SerializarPayload(e));
        }

        private void OnMostrarVistaGestionEnvios(string obj) {
            Vista.CargarFiltrosBusqueda(UtilesBusquedaSeguimientoEntrega.FiltroBusquedaSeguimientoEntrega);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        protected override PresentadorTuplaEnvio ObtenerValoresTupla(SeguimientoEntrega entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaEnvio(new VistaTuplaEnvio(), entidad);
            var venta = RepoVenta.Instancia.ObtenerPorId(entidad.IdVenta);
            var mensajero = RepoMensajero.Instancia.ObtenerPorId(entidad.IdMensajero);
            var persona = RepoPersona.Instancia.ObtenerPorId(mensajero?.IdPersona ?? 0);

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.IdVenta = entidad.IdVenta;
            presentadorTupla.Vista.NumeroFacturaVenta = venta?.NumeroFacturaTicket ?? "-";
            presentadorTupla.Vista.IdMensajero = entidad.IdMensajero;
            presentadorTupla.Vista.NombreMensajero = persona != null ? $"{persona.NombreCompleto}" : "-";
            presentadorTupla.Vista.TipoEnvio = entidad.TipoEnvio;
            presentadorTupla.Vista.FechaAsignacion = entidad.FechaAsignacion ?? DateTime.MinValue;
            presentadorTupla.Vista.FechaEntregaRealizada = entidad.FechaEntregaRealizada ?? DateTime.MinValue;
            presentadorTupla.Vista.ObservacionesEntrega = entidad.ObservacionesEntrega;
            presentadorTupla.Vista.MontoCobradoAlCliente = entidad.MontoCobradoAlCliente;
            presentadorTupla.Vista.EstadoEntrega = entidad.EstadoEntrega;
            presentadorTupla.Vista.CambioEstadoEnvio += OnCambioEstadoEnvio;

            return presentadorTupla;
        }

        private void OnCambioEstadoEnvio(object? sender, (long idEnvio, long idVenta, EstadoEntregaEnum estado) e) {
            var repoSeguimientoEntrega = RepoSeguimientoEntrega.Instancia;
            var repoVenta = RepoVenta.Instancia;
            var repoPago = RepoPago.Instancia;
            var envio = repoSeguimientoEntrega.ObtenerPorId(e.idEnvio)!;
            var venta = repoVenta.ObtenerPorId(e.idVenta)!;
            
            if (venta == null) {
                CentroNotificaciones.MostrarNotificacion("Ha ocurrido un error al obtener la venta correspondiente al envío. Datos corruptos en la base de datos.", TipoNotificacion.Error);
                return;
            }
            
            var pagosVenta = repoPago.Buscar(FiltroBusquedaPago.IdVenta, venta?.Id.ToString() ?? "0").resultadosBusqueda.Select(p => p.entidadBase).ToList();
            
            switch (e.estado) {
                case EstadoEntregaEnum.Asignado:
                case EstadoEntregaEnum.EnRuta:
                case EstadoEntregaEnum.Entregado:
                    // Actualizar datos de la venta con respecto al estado del envío
                    venta.EstadoVenta = EstadoVenta.Entregada;

                    repoVenta.Editar(venta);
                    break;
                case EstadoEntregaEnum.PagoRecibido:
                    if (CentroNotificaciones.MostrarMensaje("Desea adicionar o confirmar los pagos recibidos en la venta correspondiente?", TipoMensaje.Info, BotonesMensaje.SiNo) == DialogResult.Yes) {
                        if (pagosVenta.Count == 0) {
                            var pago = new Pago() {
                                Id = 0,
                                IdVenta = venta.Id,
                                MetodoPago = MetodoPagoEnum.Efectivo,
                                MontoPagado = venta.ImporteTotal,
                                FechaPagoCliente = DateTime.Today,
                                FechaConfirmacionPago = DateTime.Today,
                                EstadoPago = EstadoPagoEnum.Confirmado
                            };

                            repoPago.Adicionar(pago);
                        } else {
                            foreach (var pago in pagosVenta) {
                                pago.FechaPagoCliente = DateTime.Today;
                                pago.FechaConfirmacionPago = envio.TipoEnvio == TipoEnvioEnum.MensajeriaSinFondo ? DateTime.MinValue : DateTime.Today,
                                pago.EstadoPago = EstadoPagoEnum.Confirmado;

                                repoPago.Editar(pago);
                            }

                            // Verificar si los pagos confirmados satisfacen la venta
                            if (!repoVenta.VentaEstaPagadaCompletamente(venta.Id)) {
                                if (CentroNotificaciones.MostrarMensaje("Los pagos confirmados no satisfacen la totalidad del importe de la venta. Desea agregar un pago adicional cubriendo la diferencia?", TipoMensaje.Advertencia, BotonesMensaje.SiNo) == DialogResult.Yes) {
                                    var montoPagado = pagosVenta.Sum(p => p.MontoPagado);
                                    var diferencia = venta.ImporteTotal = montoPagado;
                                    var pago = new Pago() {
                                        Id = 0,
                                        IdVenta = venta.Id,
                                        MetodoPago = MetodoPagoEnum.Efectivo,
                                        MontoPagado = diferencia,
                                        FechaPagoCliente = DateTime.Today,
                                        FechaConfirmacionPago = DateTime.Today,
                                        EstadoPago = EstadoPagoEnum.Confirmado
                                    };

                                    pagosVenta.Add(pago);
                                    repoPago.Adicionar(pago);
                                }
                            } else return;
                        }
                    }
                    break;
                case EstadoEntregaEnum.Completado:
                    // Actualizar datos de la venta con respecto a los pagos
                    venta.MetodoPagoPrincipal = repoVenta.DeterminarMetodoPagoPrincipal(venta.Id)?.ObtenerDisplayName();
                    venta.EstadoVenta = EstadoVenta.Completada;

                    repoVenta.Editar(venta);
                    break;
                default:
                    break;
            }

            repoSeguimientoEntrega.CambiarEstadoEntrega(e.idEnvio, e.estado);
        }
    }
}
