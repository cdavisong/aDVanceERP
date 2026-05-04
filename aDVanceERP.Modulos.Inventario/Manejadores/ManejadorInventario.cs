using aDVanceERP.Core.Eventos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;

namespace aDVanceERP.Modulos.Inventario.Manejadores {
    internal class ManejadorInventario {
        private readonly RepoInventario _repoInventario = null!;

        internal ManejadorInventario() {
            _repoInventario = new RepoInventario();
        }

        internal void Manejar(EventoInventarioModificado e) {
            if (e.Producto != null) {
                if (e.AlmacenOrigen != null || e.AlmacenDestino != null) {
                    if (e.Cantidad > 0) {
                        _repoInventario.ModificarInventario(
                            e.Producto,
                            e.AlmacenOrigen,
                            e.AlmacenDestino,
                            e.Cantidad
                        );
                    }
                }
            }
        }
    }
}
