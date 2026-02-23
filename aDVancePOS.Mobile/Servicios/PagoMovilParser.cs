using System.Text.RegularExpressions;

namespace aDVancePOS.Mobile.Servicios {
    public static class PagoMovilParser {

        // Formato 1 — sin titular:
        // "Se ha realizado una transferencia a la cuenta 9227959873988427
        //  de 3000.00 CUP. Nro. Transaccion AY6005VFEW999. Fecha: 21/2/2026."
        private static readonly Regex _regexSinTitular = new(
            @"transferencia a la cuenta \d+" +
            @" de (?<monto>[\d]+(?:[.,]\d+)?) CUP" +
            @".*?Nro\.\s*Transaccion\s+(?<nro>[A-Z0-9]+)",
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        // Formato 2 — con titular:
        // "El titular del telefono 5354599447 le ha realizado una transferencia
        //  a la cuenta 9227959873988427 de 1600.00 CUP. Nro. Transaccion MM603XYRXT987."
        private static readonly Regex _regexConTitular = new(
            @"titular del telefono (?<tel>\d+)" +
            @".*?transferencia a la cuenta \d+" +
            @" de (?<monto>[\d]+(?:[.,]\d+)?) CUP" +
            @".*?Nro\.\s*Transaccion\s+(?<nro>[A-Z0-9]+)",
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// Intenta parsear un mensaje de PAGOxMOVIL.
        /// Devuelve null si el mensaje no coincide con ningún formato conocido.
        /// </summary>
        public static ResultadoSmsPago? Parsear(string mensaje) {
            if (string.IsNullOrWhiteSpace(mensaje)) return null;

            // Intentar formato con titular primero (más específico)
            var matchTitular = _regexConTitular.Match(mensaje);
            if (matchTitular.Success) {
                return new ResultadoSmsPago {
                    NumeroTransaccion = matchTitular.Groups["nro"].Value.Trim(),
                    Monto = ParsearMonto(matchTitular.Groups["monto"].Value),
                    TelefonoRemitente = matchTitular.Groups["tel"].Value
                };
            }

            // Intentar formato sin titular
            var matchSin = _regexSinTitular.Match(mensaje);
            if (matchSin.Success) {
                return new ResultadoSmsPago {
                    NumeroTransaccion = matchSin.Groups["nro"].Value.Trim(),
                    Monto = ParsearMonto(matchSin.Groups["monto"].Value),
                    TelefonoRemitente = null
                };
            }

            return null;
        }

        private static decimal ParsearMonto(string texto) {
            var normalizado = texto.Replace(',', '.');
            return decimal.TryParse(normalizado,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out var resultado) ? resultado : 0m;
        }
    }
}
