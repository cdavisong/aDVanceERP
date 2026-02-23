using Android.Content;
using Android.Telephony;

using System;
using System.Collections.Generic;
using System.Text;

namespace aDVancePOS.Mobile.Servicios {
    /// <summary>
    /// Escucha SMS entrantes en tiempo real mientras el diálogo
    /// de transferencia está abierto.
    ///
    /// Ciclo de vida:
    ///   - Se instancia en MostrarDialogoTransferencia()
    ///   - RegisterReceiver() al abrir el diálogo
    ///   - UnregisterReceiver() al confirmar, cancelar o presionar atrás
    /// </summary>
    public class SmsTransferenciaBroadcastReceiver : BroadcastReceiver {

        private const string RemitentePagoMovil = "PAGOxMOVIL";

        /// <summary>
        /// Callback invocado en el hilo UI cuando llega un SMS
        /// de PAGOxMOVIL que pudo parsearse correctamente.
        /// </summary>
        public Action<ResultadoSmsPago>? OnPagoDetectado { get; set; }

        public override void OnReceive(Context? context, Intent? intent) {
            if (intent?.Action != Android.Provider.Telephony.Sms.Intents.SmsReceivedAction)
                return;

            var pdus = ObtenerPdus(intent); // Devuelve Java.Lang.Object[]
            if (pdus == null) return;

            var format = intent.GetStringExtra("format");

            foreach (var pdu in pdus) {
                if (pdu == null) continue;

                // Cast seguro: Java.Lang.Object -> byte[]
                var pduBytes = (byte[]) (object) pdu;
                var sms = SmsMessage.CreateFromPdu(pduBytes, format);

                if (sms == null) continue;

                var remitente = sms.OriginatingAddress ?? "";
                if (!remitente.Contains(RemitentePagoMovil,
                        StringComparison.OrdinalIgnoreCase)) continue;

                var resultado = PagoMovilParser.Parsear(sms.MessageBody ?? "");
                if (resultado != null)
                    OnPagoDetectado?.Invoke(resultado);
            }
        }

        private static Java.Lang.Object[]? ObtenerPdus(Intent intent) {
            try {
                var rawPdus = intent.GetSerializableExtra("pdus");

                // Caso normal: llega como Array (Object[] en Java)
                if (rawPdus is Array array) {
                    var result = new Java.Lang.Object[array.Length];
                    for (int i = 0; i < array.Length; i++)
                        result[i] = (Java.Lang.Object) array.GetValue(i)!;
                    return result;
                }

                // Fallback: llegó como objeto único
                if (rawPdus is Java.Lang.Object single)
                    return new Java.Lang.Object[] { single };

                return null;
            } catch {
                return null;
            }
        }
    }
}
