using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorTuplaProducto : PresentadorVistaTupla<IVistaTuplaProducto, Producto> {
        public PresentadorTuplaProducto(IVistaTuplaProducto vista, Producto objeto) : base(vista, objeto) {
            vista.GestionarPresentaciones += MostrarVistaGestionPrecioPresentacion;
            vista.MovimientoPositivoStock += MostrarVistaRegistroMovimientoPos;
            vista.MovimientoNegativoStock += MostrarVistaRegistroMovimientoNeg;
        }

        private void MostrarVistaGestionPrecioPresentacion(object? sender, EventArgs e) {
            AgregadorEventos.Publicar("MostrarVistaGestionPrecioPresentacion", AgregadorEventos.SerializarPayload(Entidad));
        }

        private void MostrarVistaRegistroMovimientoPos(object? sender, EventArgs e) {
            var nombreAlmacen = sender as string;
            var objetoPos = new object[] { Entidad, "+" };

            AgregadorEventos.Publicar("MostrarVistaRegistroMovimiento", AgregadorEventos.SerializarPayload(objetoPos));
        }

        private void MostrarVistaRegistroMovimientoNeg(object? sender, EventArgs e) {
            var nombreAlmacen = sender as string;
            var objetoNeg = new object[] { Entidad, "-" };

            AgregadorEventos.Publicar("MostrarVistaRegistroMovimiento", AgregadorEventos.SerializarPayload(objetoNeg));
        }

        public override void Dispose() {
            Vista.GestionarPresentaciones -= MostrarVistaGestionPrecioPresentacion;
            Vista.MovimientoPositivoStock -= MostrarVistaRegistroMovimientoPos;
            Vista.MovimientoNegativoStock -= MostrarVistaRegistroMovimientoNeg;

            base.Dispose();
        }
    }
}