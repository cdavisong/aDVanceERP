using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.Drawing.Imaging;

namespace aDVanceERP.Modulos.Contactos.MVP.Modelos {
    public class Empresa : IEntidadBaseDatos {
        private Image? _logotipo;

        public Empresa() {
        }

        public Empresa(long id, Image? logotipo, string nombre, long idContacto) {
            Id = id;
            Logotipo = logotipo;
            Nombre = nombre;
            IdContacto = idContacto;
        }

        public long Id { get; set; }
        public Image? Logotipo {
            get => _logotipo; 
            set => _logotipo = value;
        }
        public string? Nombre { get; set; }
        public long IdContacto { get; set; }

        internal byte[] ObtenerDatosDbLogotipo() {
            if (_logotipo == null)
                return Array.Empty<byte>();

            try {
                using (MemoryStream ms = new MemoryStream()) {
                    // Guardar en formato PNG que preserva calidad
                    _logotipo.Save(ms, ImageFormat.Png);

                    // Verificar integridad de los datos
                    byte[] bytes = ms.ToArray();
                    if (bytes.Length < 16) return Array.Empty<byte>();

                    // Validar firma PNG
                    if (bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47)
                        return bytes;

                    return Array.Empty<byte>();
                }
            } catch {
                return Array.Empty<byte>();
            }
        }

        internal void EstablecerLogotipoDesdeBytes(byte[] imagenBytes) {
            if (imagenBytes == null || imagenBytes.Length < 16) {
                _logotipo = null;
                return;
            }

            try {
                // Verificar manualmente los primeros bytes
                bool esPng = imagenBytes[0] == 0x89 && imagenBytes[1] == 0x50 && imagenBytes[2] == 0x4E && imagenBytes[3] == 0x47;
                bool esJpeg = imagenBytes[0] == 0xFF && imagenBytes[1] == 0xD8;

                if (!esPng && !esJpeg) {
                    _logotipo = null;
                    return;
                }

                using (var ms = new MemoryStream(imagenBytes)) {
                    // Forzar la recreación del stream para evitar problemas
                    ms.Position = 0;

                    _logotipo = Image.FromStream(ms);
                }
            } catch {
                _logotipo = null;
            }
        }
    }

    public enum FiltroBusquedaEmpresa {
        Todos,
        Id,
        Nombre
    }

    public static class UtilesBusquedaEmpresa {
            public static object[] FiltroBusquedaEmpresa = {
            "Todas las empresas",
            "Identificador de BD",
            "Nombre de la empresa"
        };
    }
}
