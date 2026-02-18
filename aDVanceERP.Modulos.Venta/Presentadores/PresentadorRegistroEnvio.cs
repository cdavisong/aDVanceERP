using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
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
            // Persona-Cliente:
            if (Vista.Cliente.persona != null) {
                if (Vista.Cliente.persona.Id == 0)
                    Vista.Cliente.persona.Id = RepoPersona.Instancia.Adicionar(Vista.Cliente.persona);
                else RepoPersona.Instancia.Editar(Vista.Cliente.persona);
            }

            // Cliente:
            if (Vista.Cliente.cliente != null) {
                Vista.Cliente.cliente.IdPersona = Vista.Cliente.persona?.Id ?? 0;

                if (Vista.Cliente.cliente.Id == 0)
                    Vista.Cliente.cliente.Id = Vista.Cliente.cliente.Id = RepoCliente.Instancia.Adicionar(Vista.Cliente.cliente);
                else RepoCliente.Instancia.Editar(Vista.Cliente.cliente);
            }

            // Telefono de contacto del cliente:
            if (Vista.Cliente.telefono != null) {
                Vista.Cliente.telefono.IdPersona = Vista.Cliente.persona?.Id ?? 0;

                if (Vista.Cliente.telefono.Id == 0)
                    Vista.Cliente.telefono.Id = Vista.Cliente.telefono.Id = RepoTelefonoContacto.Instancia.Adicionar(Vista.Cliente.telefono);
                else RepoTelefonoContacto.Instancia.Editar(Vista.Cliente.telefono);
            }

            // Persona-Mensajero:
            if (Vista.Mensajero.persona != null) {
                if (Vista.Mensajero.persona.Id == 0)
                    Vista.Mensajero.persona.Id = RepoPersona.Instancia.Adicionar(Vista.Mensajero.persona);
                else RepoPersona.Instancia.Editar(Vista.Mensajero.persona);
            }

            // Mensajero:
            if (Vista.Mensajero.mensajero != null) {
                Vista.Mensajero.mensajero.IdPersona = Vista.Mensajero.persona?.Id ?? 0;

                if (Vista.Mensajero.mensajero.Id == 0)
                    Vista.Mensajero.mensajero.Id = Vista.Mensajero.mensajero.Id = RepoMensajero.Instancia.Adicionar(Vista.Mensajero.mensajero);
                else RepoMensajero.Instancia.Editar(Vista.Mensajero.mensajero);
            }

            // Telefono de contacto del mensajero:
            if (Vista.Mensajero.telefono != null) {
                Vista.Mensajero.telefono.IdPersona = Vista.Mensajero.persona?.Id ?? 0;

                if (Vista.Mensajero.telefono.Id == 0)
                    Vista.Mensajero.telefono.Id = Vista.Mensajero.telefono.Id = RepoTelefonoContacto.Instancia.Adicionar(Vista.Mensajero.telefono);
                else RepoTelefonoContacto.Instancia.Editar(Vista.Mensajero.telefono);
            }

            // Si el tipo de envío es mensajería con fondo, verificar si existe un pago pendiente de la venta y completarlo, de no
            // existir un pago pendiente, registrar un nuevo pago.
            if (Vista.TipoEnvio != TipoEnvioEnum.RetiroEnLocal) {
                var venta = RepoVenta.Instancia.Buscar(FiltroBusquedaVenta.NumeroFactura, Vista.NumeroFacturaVenta).resultadosBusqueda.FirstOrDefault().entidadBase;
                var pagosVenta = RepoPago.Instancia.Buscar(FiltroBusquedaPago.IdVenta, venta.Id.ToString()).resultadosBusqueda.Select(r => r.entidadBase).ToList();

                if (pagosVenta.Count == 0) {
                    // Registrar nuevo pago
                    var pago = new Pago() {
                        Id = 0,
                        IdVenta = venta.Id,
                        MetodoPago = MetodoPagoEnum.Efectivo,
                        MontoPagado = venta.ImporteTotal,
                        FechaPagoCliente = DateTime.Now,
                        FechaConfirmacionPago = DateTime.Now,
                        EstadoPago = Vista.TipoEnvio == TipoEnvioEnum.MensajeriaConFondo 
                            ? EstadoPagoEnum.Confirmado
                            : EstadoPagoEnum.Pendiente
                    };

                    RepoPago.Instancia.Adicionar(pago);
                } else {
                    foreach (var pago in pagosVenta) {
                        if (pago.EstadoPago == EstadoPagoEnum.Pendiente)
                            RepoPago.Instancia.CambiarEstadoPago(pago.Id, EstadoPagoEnum.Confirmado);
                    }
                }
            }
        }
    }
}
