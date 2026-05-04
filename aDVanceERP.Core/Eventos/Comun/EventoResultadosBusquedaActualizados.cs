using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Eventos.Comun {
    public class EventoResultadosBusquedaActualizados<En>
         where En : class, IEntidadBaseDatos, new() {
        public int Cantidad { get; internal set; } = 0;
        public List<En> Resultados { get; internal set; } = new List<En>();
    }
}
