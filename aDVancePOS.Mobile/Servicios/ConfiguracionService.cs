using System.Text.Json;

using aDVancePOS.Mobile.Modelos; // JsonContexto

namespace aDVancePOS.Mobile.Servicios {
    public static class ConfiguracionService {

        public static ConfiguracionApp Cargar() {
            try {
                if (!File.Exists(RutasApp.RutaConfiguracion))
                    return new ConfiguracionApp();

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
