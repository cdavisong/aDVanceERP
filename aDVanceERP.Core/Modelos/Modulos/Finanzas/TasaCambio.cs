namespace aDVanceERP.Core.Modelos.Modulos.Finanzas {
    public class TasaCambio {
        public string? NombreDivisa { get; set; }
        public decimal Valor { get; set; }
        public DireccionCambio Direccion { get; set; }
        public decimal MontoCambio { get; set; }
        public DateTime UltimaActualizacion { get; set; }

        public override string ToString() {
            return $"{NombreDivisa}: {Valor} {ObtenerSimboloDireccion()} {MontoCambio}";
        }

        private string ObtenerSimboloDireccion() {
            return Direccion switch {
                DireccionCambio.Aumento => "↑",
                DireccionCambio.Disminucion => "↓",
                _ => "→"
            };
        }
    }
}
