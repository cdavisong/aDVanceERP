using aDVanceERP.Core.Modelos.Modulos.Finanzas;
using HtmlAgilityPack;
using System.Globalization;

namespace aDVanceERP.Core.Utiles
{
    public static class UtilesCambioMoneda {
        private static readonly ScraperDivisas _scraper = new ScraperDivisas();

        public static async Task<TasaCambio?> ObtenerTasaPorDivisa(string nombreDivisa) {
            var tasas = await _scraper.ObtenerTasasCambioAsync();

            return tasas.FirstOrDefault(t =>
                t.NombreDivisa?.Contains(nombreDivisa, StringComparison.OrdinalIgnoreCase) ?? false);
        }
    }

    public class ScraperDivisas {
        private HttpClient? _clienteHttp;
        private const string UrlBase = "https://eltoque.com/precio-del-mlc-en-cuba-hoy";

        public async Task<List<TasaCambio>> ObtenerTasasCambioAsync() {
            var tasas = new List<TasaCambio>();

            try {
                // Verificar conectividad antes de hacer la petición
                if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                    throw new InvalidOperationException("No hay conexión a internet disponible.");

                // Configurar timeout para evitar esperas prolongadas
                _clienteHttp = new HttpClient();
                _clienteHttp.Timeout = TimeSpan.FromSeconds(30);

                var html = await _clienteHttp.GetStringAsync(UrlBase).ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(html)) {
                    throw new Exception("La respuesta del servidor está vacía.");
                }

                var documento = new HtmlAgilityPack.HtmlDocument();
                documento.LoadHtml(html);

                // Seleccionar la tabla de tasas
                var tabla = documento.DocumentNode.SelectSingleNode("//table[contains(@class, 'gmdijD')]");

                if (tabla == null) {
                    throw new Exception("No se encontró la tabla de tasas en el HTML.");
                }

                var filas = tabla.SelectNodes(".//tr[position()>1]"); // Saltar encabezado

                if (filas == null || !filas.Any()) {
                    throw new Exception("No se encontraron filas con datos en la tabla.");
                }

                foreach (var fila in filas) {
                    var tasa = ParsearFilaTasa(fila);
                    if (tasa != null) {
                        tasas.Add(tasa);
                    }
                }

                if (!tasas.Any()) {
                    throw new Exception("No se pudieron parsear tasas válidas.");
                }

                _clienteHttp.Dispose();
            }
            catch (HttpRequestException httpEx) {
                // Errores específicos de HTTP
                string mensajeError = httpEx.StatusCode switch {
                    System.Net.HttpStatusCode.NotFound => "El recurso solicitado no fue encontrado (404).",
                    System.Net.HttpStatusCode.Unauthorized => "No autorizado para acceder al recurso (401).",
                    System.Net.HttpStatusCode.Forbidden => "Acceso prohibido al recurso (403).",
                    System.Net.HttpStatusCode.InternalServerError => "Error interno del servidor (500).",
                    _ => $"Error en la solicitud HTTP: {httpEx.Message}"
                };

                Console.WriteLine($"Error HTTP: {mensajeError}");
                throw; // Relanzar para manejo superior si es necesario
            }
            catch (TaskCanceledException) {
                Console.WriteLine("La solicitud fue cancelada por timeout.");
                throw;
            }
            catch (InvalidOperationException invOpEx) {
                Console.WriteLine($"Error de operación: {invOpEx.Message}");
                throw;
            }
            catch (Exception ex) {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                throw;
            }

            return tasas;
        }

        private TasaCambio? ParsearFilaTasa(HtmlNode fila) {
            try {
                var celdas = fila.SelectNodes(".//td");

                if (celdas == null || celdas.Count < 3)
                    return null;

                var nombreDivisa = celdas[0].InnerText.Trim();
                var textoPrecio = celdas[2].SelectSingleNode(".//span[contains(@class, 'price-text')]")?.InnerText;
                var nodoCambio = celdas[2].SelectSingleNode(".//div[contains(@class, 'change-icon-value')]");

                // Parsear valores
                decimal valor = ParsearDecimal(textoPrecio?.Replace("CUP", "").Trim());
                var direccion = DireccionCambio.Neutral;
                decimal montoCambio = 0;

                if (nodoCambio != null) {
                    var textoCambio = nodoCambio.SelectSingleNode(".//span[contains(@class, 'dif-number')]")?.InnerText;
                    montoCambio = ParsearDecimal(textoCambio?.Replace("+", "").Replace("-", "").Trim());

                    if (nodoCambio.Attributes["class"]?.Value.Contains("change-plus") == true) {
                        direccion = DireccionCambio.Aumento;
                    }
                    else if (nodoCambio.Attributes["class"]?.Value.Contains("change-minus") == true) {
                        direccion = DireccionCambio.Disminucion;
                    }
                }

                return new TasaCambio {
                    NombreDivisa = nombreDivisa,
                    Valor = valor,
                    Direccion = direccion,
                    MontoCambio = montoCambio,
                    UltimaActualizacion = DateTime.Now
                };
            }
            catch {
                return null;
            }
        }

        private decimal ParsearDecimal(string valor) {
            if (decimal.TryParse(valor, NumberFormatInfo.InvariantInfo, out decimal resultado))
                return resultado;

            return 0;
        }
    }
}
