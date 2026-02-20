using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    internal class PresentadorRegistroEnvio : PresentadorVistaRegistro<IVistaRegistroEnvio, SeguimientoEntrega, RepoSeguimientoEntrega, FiltroBusquedaSeguimientoEntrega> {
        public PresentadorRegistroEnvio(IVistaRegistroEnvio vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroEnvio", OnMostrarVistaRegistroEnvio);
            AgregadorEventos.Suscribir("MostrarVistaEdicionEnvio", OnMostrarVistaEdicionEnvio);
        }

        private void OnMostrarVistaRegistroEnvio(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();

            // Carga inicial de datos
            var tiposEnvio = new List<string>();

            foreach (TipoEnvioEnum metodo in Enum.GetValues(typeof(TipoEnvioEnum)))
                tiposEnvio.Add(metodo.ObtenerDisplayName());

            Vista.CargarFacturasVentasPendientes([.. RepoVenta.Instancia.ObtenerVentasPendientesDePago().Select(v => v.NumeroFacturaTicket)]);
            Vista.CargarTiposEnvio([.. tiposEnvio]);
            Vista.CargarNombresClientes([.. RepoCliente.Instancia.ObtenerNombres()]);
            Vista.CargarNombresMensajeros([.. RepoMensajero.Instancia.ObtenerNombres()]);

            Vista.Mostrar();
        }

        private void OnMostrarVistaEdicionEnvio(string obj) {
            Vista.ModoEdicion = true;
            Vista.Restaurar();

            if (string.IsNullOrEmpty(obj))
                return;

            var envio = AgregadorEventos.DeserializarPayload<SeguimientoEntrega>(obj);

            if (envio == null)
                return;

            // Carga inicial de datos
            var tiposEnvio = new List<string>();

            foreach (TipoEnvioEnum metodo in Enum.GetValues(typeof(TipoEnvioEnum)))
                tiposEnvio.Add(metodo.ObtenerDisplayName());

            Vista.CargarFacturasVentasPendientes([.. RepoVenta.Instancia.ObtenerVentasPendientesDePago().Select(v => v.NumeroFacturaTicket)]);
            Vista.CargarTiposEnvio([.. tiposEnvio]);
            Vista.CargarNombresClientes([.. RepoCliente.Instancia.ObtenerNombres()]);
            Vista.CargarNombresMensajeros([.. RepoMensajero.Instancia.ObtenerNombres()]);

            PopularVistaDesdeEntidad(envio);

            Vista.Mostrar();
        }

        protected override SeguimientoEntrega? ObtenerEntidadDesdeVista() {
            var venta = RepoVenta.Instancia.Buscar(FiltroBusquedaVenta.NumeroFactura, Vista.NumeroFacturaVenta).resultadosBusqueda.FirstOrDefault().entidadBase;

            return new SeguimientoEntrega() {
                Id = 0,
                IdVenta = venta.Id,
                IdCliente = Vista.Cliente.cliente?.Id ?? 0,
                IdMensajero = Vista.Mensajero.mensajero?.Id ?? 0,
                TipoEnvio = Vista.TipoEnvio,
                FechaAsignacion = Vista.FechaAsignacion,
                FechaEntregaRealizada = DateTime.MinValue,
                FechaPagoNegocio = DateTime.MinValue,
                EstadoEntrega = Vista.TipoEnvio switch {
                    TipoEnvioEnum.RetiroEnLocal => EstadoEntregaEnum.EnEspera,
                    TipoEnvioEnum.MensajeriaConFondo => Vista.Mensajero.mensajero != null ? EstadoEntregaEnum.Asignado : EstadoEntregaEnum.PendienteAsignacion,
                    TipoEnvioEnum.MensajeriaSinFondo => Vista.Mensajero.mensajero != null ? EstadoEntregaEnum.Asignado : EstadoEntregaEnum.PendienteAsignacion,
                    _ => throw new NotImplementedException(),
                },
                MontoCobradoAlCliente = Vista.MontoCobradoAlCliente,
                ObservacionesEntrega = Vista.ObservacionesEntrega
            };
        }

        protected override void RegistroEdicionAuxiliar(RepoSeguimientoEntrega repositorio, long id) {
            var repoPersona = RepoPersona.Instancia;
            var repoTelefonoContacto = RepoTelefonoContacto.Instancia;
            var repoVenta = RepoVenta.Instancia;
            var repoPago = RepoPago.Instancia;

            // Persona-Cliente:
            var personaCliente = Vista.Cliente.persona;

            if (personaCliente != null) {
                if (personaCliente.Id == 0)
                    personaCliente.Id = repoPersona.Adicionar(Vista.Cliente.persona);
                else repoPersona.Editar(personaCliente);
            }

            // Cliente:
            var cliente = Vista.Cliente.cliente;

            if (cliente != null) {
                cliente.IdPersona = personaCliente!.Id;

                if (cliente.Id == 0)
                    cliente.Id = Vista.Cliente.cliente.Id = RepoCliente.Instancia.Adicionar(cliente);
                else RepoCliente.Instancia.Editar(cliente);
            }

            // Telefono de contacto del cliente:
            var telefonoCliente = Vista.Cliente.telefono;

            if (telefonoCliente != null) {
                telefonoCliente.IdPersona = personaCliente!.Id;

                if (telefonoCliente.Id == 0)
                    repoTelefonoContacto.Adicionar(telefonoCliente);
                else repoTelefonoContacto.Editar(telefonoCliente);
            }

            // Actualizar el id de cliente en la venta asociada
            var venta = repoVenta.Buscar(FiltroBusquedaVenta.NumeroFactura, Vista.NumeroFacturaVenta).resultadosBusqueda.FirstOrDefault().entidadBase;

            venta.IdCliente = cliente!.Id;
            repoVenta.Editar(venta);

            // Persona-Mensajero:
            var personaMensajero = Vista.Mensajero.persona;

            if (personaMensajero != null) {
                if (personaMensajero.Id == 0)
                    personaMensajero.Id = repoPersona.Adicionar(personaMensajero);
                else repoPersona.Editar(personaMensajero);
            }

            // Mensajero:
            var mensajero = Vista.Mensajero.mensajero;

            if (mensajero != null) {
                mensajero.IdPersona = personaMensajero!.Id;

                if (mensajero.Id == 0)
                    mensajero.Id = RepoMensajero.Instancia.Adicionar(mensajero);
                else RepoMensajero.Instancia.Editar(mensajero);
            }

            // Telefono de contacto del mensajero:
            var telefonoMensajero = Vista.Mensajero.telefono;

            if (telefonoMensajero != null) {
                telefonoMensajero.IdPersona = personaMensajero!.Id;

                if (telefonoMensajero.Id == 0)
                    telefonoMensajero.Id = repoTelefonoContacto.Adicionar(telefonoMensajero);
                else repoTelefonoContacto.Editar(telefonoMensajero);
            }

            // Actualizar el id de cliente y mensajero en el seguimiento de entrega
            var envio = repositorio.ObtenerPorId(id);

            envio!.IdCliente = cliente!.Id;
            envio!.IdMensajero = mensajero!.Id;
            repositorio.Editar(envio);

            // Si el tipo de envío es mensajería con fondo, verificar si existe un pago pendiente de la venta y completarlo, de no
            // existir un pago pendiente, registrar un nuevo pago.
            if (Vista.TipoEnvio != TipoEnvioEnum.RetiroEnLocal) {
                var pagosVenta = repoPago.Buscar(FiltroBusquedaPago.IdVenta, venta.Id.ToString()).resultadosBusqueda.Select(r => r.entidadBase).ToList();

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
                            pago.FechaConfirmacionPago = envio.TipoEnvio == TipoEnvioEnum.MensajeriaSinFondo ? DateTime.MinValue : DateTime.Today;
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

                    // Actualizar datos de la venta con respecto a los pagos
                    venta.MetodoPagoPrincipal = repoVenta.DeterminarMetodoPagoPrincipal(venta.Id)?.ObtenerDisplayName();
                    venta.EstadoVenta = EstadoVentaEnum.Completada;

                    repoVenta.Editar(venta);
                }
            }
        }
    }
}
