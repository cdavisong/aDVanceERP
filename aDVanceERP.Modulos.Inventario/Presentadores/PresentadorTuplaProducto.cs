using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorTuplaProducto : PresentadorVistaTupla<IVistaTuplaProducto, Producto> {
        public PresentadorTuplaProducto(IVistaTuplaProducto vista, Producto objeto) : base(vista, objeto) {
            vista.EditarDatosTupla += MostrarVistaEdicionProducto;
            vista.GestionarPresentaciones += MostrarVistaGestionPresentacionProducto;
            vista.MovimientoCarga += MostrarVistaRegistroMovimientoCarga;
            vista.MovimientoDescarga += MostrarVistaRegistroMovimientoDescarga;
        }

        private void MostrarVistaEdicionProducto(object? sender, EventArgs e) {
            var almacen = Vista.Almacen;
            var producto = RepoProducto.Instancia.ObtenerPorId(Vista.Id);
            var inventarioProducto = RepoInventario
                .Instancia
                .Buscar(FiltroBusquedaInventario.IdProducto, producto?.Id.ToString() ?? "0")
                .resultadosBusqueda
                .Select(r => r.entidadBase);

            AgregadorEventos.Publicar(new EventoMostrarVistaEdicionProducto() {
                Almacen = almacen!,
                Producto = producto!,
                Inventario = inventarioProducto
            });
        }

        private void MostrarVistaGestionPresentacionProducto(object? sender, EventArgs e) {
            var almacen = Vista.Almacen;
            var producto = RepoProducto.Instancia.ObtenerPorId(Vista.Id);

            AgregadorEventos.Publicar(new EventoMostrarVistaGestionPresentacionProducto() {
                Almacen = almacen!,
                Producto = producto!,
            });
        }

        private void MostrarVistaRegistroMovimientoCarga(object? sender, EventArgs e) {
            var almacenDestino = Vista.Almacen;
            var producto = RepoProducto.Instancia.ObtenerPorId(Vista.Id);

            AgregadorEventos.Publicar(new EventoMostrarVistaRegistroMovimiento() {
                EfectoMovimiento = EfectoMovimientoEnum.Carga,
                AlmacenDestino = almacenDestino!,
                Producto = producto!
            });
        }

        private void MostrarVistaRegistroMovimientoDescarga(object? sender, EventArgs e) {
            var almacenOrigen = Vista.Almacen;
            var producto = RepoProducto.Instancia.ObtenerPorId(Vista.Id);

            AgregadorEventos.Publicar(new EventoMostrarVistaRegistroMovimiento() {
                EfectoMovimiento = EfectoMovimientoEnum.Descarga,
                AlmacenOrigen = almacenOrigen!,
                Producto = producto!
            });
        }

        public override void Dispose() {
            Vista.EditarDatosTupla -= MostrarVistaEdicionProducto;
            Vista.GestionarPresentaciones -= MostrarVistaGestionPresentacionProducto;
            Vista.MovimientoCarga -= MostrarVistaRegistroMovimientoCarga;
            Vista.MovimientoDescarga -= MostrarVistaRegistroMovimientoDescarga;

            base.Dispose();
        }
    }
}