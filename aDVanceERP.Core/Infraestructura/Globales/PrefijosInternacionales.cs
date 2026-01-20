using aDVanceERP.Core.Properties;

using Svg;

using System;

namespace aDVanceERP.Core.Infraestructura.Globales {
    public static class PrefijosInternacionales {
        private static List<(string pais, string prefijo, string codigoIso)> _ppc = new List<(string pais, string prefijo, string codigoIso)>();

        static PrefijosInternacionales() {
            var lineas = Resources.Prefijos.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);

            foreach (var linea in lineas) {
                var partes = linea.Split('|');
                
                if (partes.Length < 3) // Asegurarse de que tiene los 3 campos
                    continue;

                var pais = partes[0].Trim();
                var prefijo = partes[1].TrimStart('0').Insert(0, "+");
                var codigoIso = partes[2].Trim().ToLower(); // Convertir a minúsculas para consistencia

                _ppc.Add((pais, prefijo, codigoIso));
            }
        }

        public static List<(string pais, string prefijo, string codigoIso)> ObtenerTodos() {
            return [.. _ppc];
        }

        public static string[] ObtenerPaises() {
            return [.. _ppc.Select(p => p.pais)];
        }

        public static string ObtenerPais(string prefijo) {
            var entry = _ppc.FirstOrDefault(p => p.prefijo.Equals(prefijo, StringComparison.OrdinalIgnoreCase));

            return entry.pais ?? throw new ArgumentException("Ha ocurrido un error, el prefijo especificado no es correcto", nameof(prefijo));
        }

        public static string ObtenerPrefijo(string pais) {
            var entry = _ppc.FirstOrDefault(p => p.pais.Equals(pais, StringComparison.OrdinalIgnoreCase));
            
            return entry.prefijo ?? throw new ArgumentException("Ha ocurrido un error, el país especificado no es correcto", nameof(pais));
        }

        public static string ObtenerCodigoIso(string pais) {
            var entry = _ppc.FirstOrDefault(p => p.pais.Equals(pais, StringComparison.OrdinalIgnoreCase));
            
            return entry.codigoIso ?? throw new ArgumentException("Ha ocurrido un error, el país especificado no es correcto", nameof(pais));
        }

        public static Image ObtenerFlag(string pais) {
            var entry = _ppc.FirstOrDefault(p => p.pais.Equals(pais, StringComparison.OrdinalIgnoreCase));
            var codigoIso = entry.codigoIso ?? throw new ArgumentException("Ha ocurrido un error, el país especificado no es correcto", nameof(pais));
            var svgDoc = SvgDocument.Open($@".\res\flags\{codigoIso}.svg");
            var flag = svgDoc.Draw();

            return flag;
        }
    }
}
