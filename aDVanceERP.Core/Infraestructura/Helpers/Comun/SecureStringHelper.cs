using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

namespace aDVanceERP.Core.Infraestructura.Helpers.Comun;

public static class SecureStringHelper {
    public static (string hash, string salt) HashPassword(SecureString passwordSeguro) {
        var unmanagedString = nint.Zero;

        try {
            unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(passwordSeguro);
            var password = Marshal.PtrToStringUni(unmanagedString);

            return HashPassword(password);
        } finally {
            if (unmanagedString != nint.Zero)
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
        }
    }

    // ...

    private static (string hash, string salt) HashPassword(string? password) {
        var saltBytes = new byte[16];

        RandomNumberGenerator.Fill(saltBytes);

        var salt = Convert.ToBase64String(saltBytes);
        var hash = string.Empty;

        // Use SHA256 as the hash algorithm for PBKDF2
        using (var rfc2898 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256)) {
            var hashBytes = rfc2898.GetBytes(32);

            hash = Convert.ToBase64String(hashBytes);
        }

        return (hash, salt);
    }
}
