using System.Security.Cryptography;
using System.Text;

namespace aDVanceERP.Core.Utiles.Datos;

public static class UtilesCodigoBarras {
    public static string GenerarEan13(string nombreProducto) {
        if (string.IsNullOrWhiteSpace(nombreProducto)) {
            throw new ArgumentException("El nombre del producto no puede estar vacío");
        }

        // Paso 1: Obtener un hash del nombre del producto
        byte[] hashBytes;
        using (var sha256 = SHA256.Create())
            hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(nombreProducto));

        // Paso 2: Convertir el hash a un número largo
        long numericHash = BitConverter.ToInt64(hashBytes, 0);
        numericHash = Math.Abs(numericHash); // Asegurar que sea positivo

        // Paso 3: Tomar los primeros 12 dígitos (ajustando si es necesario)
        string baseNumber = numericHash.ToString().PadRight(12, '0').Substring(0, 12);

        // Paso 4: Calcular el dígito de control
        int checksum = CalcularSumaChequeoEan13(baseNumber);
        string ean13 = baseNumber + checksum;

        return ean13;
    }

    private static int CalcularSumaChequeoEan13(string primeros12Digitos) {
        if (primeros12Digitos.Length != 12 || !primeros12Digitos.All(char.IsDigit)) {
            throw new ArgumentException("Se requieren exactamente 12 dígitos");
        }

        int sum = 0;
        for (int i = 0; i < 12; i++) {
            int digit = int.Parse(primeros12Digitos[i].ToString());
            // Los dígitos en posiciones impares (basado en 1) se multiplican por 3
            sum += (i % 2 == 0 ? 1 : 3) * digit;
        }

        int checksum = (10 - (sum % 10)) % 10;
        return checksum;
    }
}