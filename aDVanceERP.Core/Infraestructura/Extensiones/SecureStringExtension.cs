using aDVanceERP.Core.Infraestructura.Helpers;

using System.Runtime.InteropServices;
using System.Security;

namespace aDVanceERP.Core.Infraestructura.Extensiones {
    public static class SecureStringExtension {
        public static (string hash, string salt) HashPassword(this SecureString passwordSeguro) {
            var unmanagedString = nint.Zero;

            try {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(passwordSeguro);
                var password = Marshal.PtrToStringUni(unmanagedString);

                return SecureStringHelper.HashPassword(password);
            } finally {
                if (unmanagedString != nint.Zero)
                    Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static bool VerificarPassword(this SecureString passwordSeguro, string hashAlmacenado, string saltAlmacenado) {
            var unmanagedString = nint.Zero;

            try {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(passwordSeguro);
                var password = Marshal.PtrToStringUni(unmanagedString);

                return SecureStringHelper.VerificarPassword(password, hashAlmacenado, saltAlmacenado);
            } finally {
                if (unmanagedString != nint.Zero)
                    Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}