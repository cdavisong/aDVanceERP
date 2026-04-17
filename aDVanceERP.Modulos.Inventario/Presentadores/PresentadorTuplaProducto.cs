using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorTuplaProducto : PresentadorVistaTupla<IVistaTuplaProducto, Producto> {
        public PresentadorTuplaProducto(IVistaTuplaProducto vista, Producto objeto) : base(vista, objeto) {
            vista.EditarDatosTupla += MostrarVistaEdicionProducto;
            vista.GestionarPresentaciones += MostrarVistaGestionPresentacionProducto;
            vista.MovimientoPositivoStock += MostrarVistaRegistroMovimientoPos;
            vista.MovimientoNegativoStock += MostrarVistaRegistroMovimientoNeg;
        }

        private void MostrarVistaEdicionProducto(object? sender, EventArgs e) {
            AgregadorEventos.Publicar("MostrarVistaEdicionProducto", AgregadorEventos.SerializarPayload(sender));
        }

        private void MostrarVistaGestionPresentacionProducto(object? sender, EventArgs e) {
            if (sender is not object[] datos)
                return;

            if (datos[0] is not long id)
                return;

            var entidad = RepoProducto.Instancia.ObtenerPorId(id);

            AgregadorEventos.Publicar("MostrarVistaGestionPresentacionProducto", AgregadorEventos.SerializarPayload(new object[] { entidad!, datos[1] }));
        }

        private void MostrarVistaRegistroMovimientoPos(object? sender, Almacen almacen) {
            var nombreAlmacen = sender as string;
            var objetoPos = new object[] { Entidad, "+" };

            AgregadorEventos.Publicar("MostrarVistaRegistroMovimiento", AgregadorEventos.SerializarPayload(objetoPos));
        }

        private void MostrarVistaRegistroMovimientoNeg(object? sender, Almacen almacen) {
            var nombreAlmacen = sender as string;
            var objetoNeg = new object[] { Entidad, "-" };

            AgregadorEventos.Publicar("MostrarVistaRegistroMovimiento", AgregadorEventos.SerializarPayload(objetoNeg));
        }

        public override void Dispose() {
            Vista.EditarDatosTupla -= MostrarVistaEdicionProducto;
            Vista.GestionarPresentaciones -= MostrarVistaGestionPresentacionProducto;
            Vista.MovimientoPositivoStock -= MostrarVistaRegistroMovimientoPos;
            Vista.MovimientoNegativoStock -= MostrarVistaRegistroMovimientoNeg;

            base.Dispose();
        }
    }
}