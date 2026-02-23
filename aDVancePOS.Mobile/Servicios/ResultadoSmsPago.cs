namespace aDVancePOS.Mobile.Servicios {
    public class ResultadoSmsPago {
        /// <summary>Número de transacción. Ej: AY6005VFEW999</summary>
        public string NumeroTransaccion { get; init; } = string.Empty;

        /// <summary>Monto transferido en CUP.</summary>
        public decimal Monto { get; init; }

        /// <summary>Teléfono del remitente (solo en el formato con titular).</summary>
        public string? TelefonoRemitente { get; init; }
    }
}
