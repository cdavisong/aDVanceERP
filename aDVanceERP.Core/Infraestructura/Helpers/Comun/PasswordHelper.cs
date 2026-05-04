using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Infraestructura.Helpers.Comun {
    public static class PasswordHelper {
        /// <summary>
        /// Generador de contraseñas seguras
        /// </summary>
        /// <returns></returns>
        public static string GenerarPasswordSeguro() {
            const int length = 16;
            const string lowers = "abcdefghijklmnopqrstuvwxyz";
            const string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digits = "0123456789";
            const string symbols = "!@#$%^&*()-_=+[]{}|;:,.<>?";

            var all = lowers + uppers + digits + symbols;
            var chars = new List<char>(length) {
                // Garantizar al menos un caracter de cada tipo
                allower(lowers),
                allower(uppers),
                allower(digits),
                allower(symbols)
            };

            // Rellenar el resto con caracteres aleatorios de 'all'
            for (int i = chars.Count; i < length; i++) {
                chars.Add(allower(all));
            }

            // Mezclar la lista con un Fisher-Yates usando RNG criptográfico
            for (int i = chars.Count - 1; i > 0; i--) {
                int j = RandomNumberGenerator.GetInt32(i + 1);
                var tmp = chars[i];
                chars[i] = chars[j];
                chars[j] = tmp;
            }

            return new string(chars.ToArray());

            static char allower(string set) {
                return set[RandomNumberGenerator.GetInt32(set.Length)];
            }
        }

        /// <summary>
        /// Niveles de fuerza de contraseña
        /// </summary>
        public enum FuerzaPassword {
            [Display(Name = "Muy débil")]
            MuyDebil = 0,
            [Display(Name = "Débil")]
            Debil = 1,
            Media = 2,
            Fuerte = 3,
            [Display(Name = "Muy fuerte")]
            MuyFuerte = 4
        }

        /// <summary>
        /// Verifica cuán fuerte es la contraseña.
        /// Devuelve un enum con la categoría de fuerza.
        /// </summary>
        public static FuerzaPassword VerificarFuerza(string password) {
            if (string.IsNullOrEmpty(password)) return FuerzaPassword.MuyDebil;

            // Penalizar contraseñas triviales
            if (password.All(c => c == password[0])) return FuerzaPassword.MuyDebil;

            const string symbols = "!@#$%^&*()-_=+[]{}|;:,.<>?";
            int score = 0;

            if (password.Length >= 8) score++;
            if (password.Length >= 12) score++;
            if (password.Length >= 16) score++;

            if (password.Any(char.IsLower)) score++;
            if (password.Any(char.IsUpper)) score++;
            if (password.Any(char.IsDigit)) score++;
            if (password.Any(c => symbols.Contains(c))) score++;

            // score max posible = 7
            if (score <= 1) return FuerzaPassword.MuyDebil;
            if (score <= 3) return FuerzaPassword.Debil;
            if (score == 4) return FuerzaPassword.Media;
            if (score <= 6) return FuerzaPassword.Fuerte;

            return FuerzaPassword.MuyFuerte;
        }
    }
}
