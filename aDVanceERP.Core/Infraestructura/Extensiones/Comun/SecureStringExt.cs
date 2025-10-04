using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

namespace aDVanceERP.Core.Infraestructura.Extensiones.Comun;

public static class SecureStringExt {
    public static bool VerificarPassword(this SecureString passwordSeguro, string hashAlmacenado, string saltAlmacenado) {
        var unmanagedString = nint.Zero;

        try {
            unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(passwordSeguro);
            var password = Marshal.PtrToStringUni(unmanagedString);

            return VerificarPassword(password, hashAlmacenado, saltAlmacenado);
        } finally {
            if (unmanagedString != nint.Zero)
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
        }
    }

    private static bool VerificarPassword(string? password, string hashAlmacenado, string saltAlmacenado) {
        var saltBytes = Convert.FromBase64String(saltAlmacenado);
        var hash = string.Empty;

        using (var rfc2898 = new Rfc2898DeriveBytes(password, saltBytes, 10000)) {
            var hashBytes = rfc2898.GetBytes(32);

            hash = Convert.ToBase64String(hashBytes);
        }

        return hashAlmacenado.Equals(hash);
    }
}