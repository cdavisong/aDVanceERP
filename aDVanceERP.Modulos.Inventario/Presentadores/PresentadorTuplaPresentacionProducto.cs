using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    internal class PresentadorTuplaPresentacionProducto : PresentadorVistaTupla<IVistaTuplaVentaPresentacion, PresentacionProducto> {
        public PresentadorTuplaPresentacionProducto(IVistaTuplaVentaPresentacion vista, PresentacionProducto entidad) : base(vista, entidad) {
            vista.EditarDatosTupla += OnEditarPresentacionProducto;
        }

        private void OnEditarPresentacionProducto(object? sender, EventArgs e) {
            var presentacionProducto = RepoPresentacionProducto.Instancia.ObtenerPorId(Vista.Id);

            AgregadorEventos.Publicar(new EventoMostrarVistaEdicionPresentacionProducto() {
                PresentacionProducto = presentacionProducto!
            });
        }

        public override void Dispose() {
            Vista.EditarDatosTupla -= OnEditarPresentacionProducto;

            base.Dispose();
        }
    }
}
