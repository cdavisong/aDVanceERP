
namespace aDVanceERP.Core.Mensajes.MVP.Modelos {
    public class MensajeTelegram {
        public string? TelefonoMovilDestinatario {  get; set; }
        public string? IdChat { get; set; }
        public required string Texto { get; set; }
        public DateTime TiempoEnvio { get; set; }
        public bool FueEnviado { get; set; }
        public int IdMensaje { get; set; }
    }
}
