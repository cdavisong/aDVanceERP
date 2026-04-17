using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    internal class PresentadorTuplaPresentacionProducto : PresentadorVistaTupla<IVistaTuplaVentaPresentacion, PresentacionProducto> {
        public PresentadorTuplaPresentacionProducto(IVistaTuplaVentaPresentacion vista, PresentacionProducto entidad) : base(vista, entidad) {
            vista.EditarDatosTupla += OnEditarPresentacionProducto;
        }

        private void OnEditarPresentacionProducto(object? sender, EventArgs e) {
            AgregadorEventos.Publicar("EditarPresentacionProducto", AgregadorEventos.SerializarPayload(sender));
        }

        public override void Dispose() {
            Vista.EditarDatosTupla -= OnEditarPresentacionProducto;

            base.Dispose();
        }
    }
}
