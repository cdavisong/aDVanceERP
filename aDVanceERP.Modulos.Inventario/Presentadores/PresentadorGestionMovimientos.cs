using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Vistas;

using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorGestionMovimientos : PresentadorVistaGestion<PresentadorTuplaMovimiento, IVistaGestionMovimientos, IVistaTuplaMovimiento, Movimiento, RepoMovimiento, FiltroBusquedaMovimiento> {
        public PresentadorGestionMovimientos(IVistaGestionMovimientos vista) : base(vista) { 
            RegistrarEntidad += OnRegistrarMovimiento;
            EditarEntidad += OnEditarMovimiento;

            AgregadorEventos.Suscribir("MostrarVistaGestionMovimientos", OnMostrarVistaGestionMovimientos);
        }

        private void OnRegistrarMovimiento(object? sender, EventArgs e) {
            if (RepoProducto.Instancia.Cantidad() == 0) {
                CentroNotificaciones.Mostrar("No es posible registrar un nuevo movimiento porque no hay productos registrados en el sistema. Por favor, registre al menos un producto antes de continuar.", TipoNotificacion.Advertencia);
                return;
            }

            AgregadorEventos.Publicar("MostrarVistaRegistroMovimiento", string.Empty);
        }

        private void OnEditarMovimiento(object? sender, Movimiento e) {
            AgregadorEventos.Publicar("MostrarVistaEdicionMovimiento", AgregadorEventos.SerializarPayload(e));
        }

        private void OnMostrarVistaGestionMovimientos(string obj) {
            Vista.CargarFiltrosBusqueda(UtilesBusquedaMovimiento.FiltroBusquedaMovimiento);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        protected override PresentadorTuplaMovimiento ObtenerValoresTupla(Movimiento entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaMovimiento(new VistaTuplaMovimiento(), entidad);

            presentadorTupla.Vista.Id = entidad.Id.ToString();
            presentadorTupla.Vista.NombreProducto = entidad.NombreProducto;
            presentadorTupla.Vista.NombreAlmacenOrigen = entidad.NombreAlmacenOrigen;
            presentadorTupla.Vista.ActualizarIconoStock(entidad.EfectoMovimiento);
            presentadorTupla.Vista.NombreAlmacenDestino = entidad.NombreAlmacenDestino;
            presentadorTupla.Vista.SaldoInicial = entidad.SaldoInicial.ToString("N2", CultureInfo.InvariantCulture);
            presentadorTupla.Vista.CantidadMovida = entidad.CantidadMovida.ToString("N2", CultureInfo.InvariantCulture);
            presentadorTupla.Vista.SaldoFinal = entidad.SaldoFinal.ToString("N2", CultureInfo.InvariantCulture);
            presentadorTupla.Vista.TipoMovimiento = entidad.NombreTipoMovimiento;
            presentadorTupla.Vista.Fecha = entidad.FechaCreacion.ToString("yyyy-MM-dd");

            return presentadorTupla;
        }
    }
}