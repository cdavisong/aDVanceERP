// ============================================================
//  aDVanceSTOCK.Mobile — ConfiguracionService
//  Archivo: Servicios/ConfiguracionService.cs
// ============================================================

using System.Text.Json;
using aDVanceSTOCK.Mobile.Modelos;

namespace aDVanceSTOCK.Mobile.Servicios {

    public static class ConfiguracionService {

        public static ConfiguracionApp Cargar() {
            try {
                var json = File.ReadAllText(RutasApp.RutaConfiguracion);
                return JsonSerializer.Deserialize(json, JsonContexto.Default.ConfiguracionApp)
                       ?? new ConfiguracionApp();
            } catch {
                return new ConfiguracionApp();
            }
        }

        public static void Guardar(ConfiguracionApp config) {
            var json = JsonSerializer.Serialize(config, JsonContexto.Default.ConfiguracionApp);
            File.WriteAllText(RutasApp.RutaConfiguracion, json);
        }
    }
}
