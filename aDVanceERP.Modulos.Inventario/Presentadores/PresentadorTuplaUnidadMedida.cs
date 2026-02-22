using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    internal class PresentadorTuplaUnidadMedida : PresentadorVistaTupla<IVistaTuplaUnidadMedida, UnidadMedida> {
        public PresentadorTuplaUnidadMedida(IVistaTuplaUnidadMedida vista, UnidadMedida entidad) : base(vista, entidad) {
        }
    }
}