using System.Text.Json;

namespace aDVancePOS.Mobile.Servicios {
    public static class ConfiguracionService {
        private static readonly JsonSerializerOptions _opts =
            new() { WriteIndented = true };

        public static ConfiguracionApp Cargar() {
            try {
                if (!File.Exists(RutasApp.RutaConfiguracion))
                    return new ConfiguracionApp();

                var json = File.ReadAllText(RutasApp.RutaConfiguracion);
                return JsonSerializer.Deserialize<ConfiguracionApp>(json)
                       ?? new ConfiguracionApp();
            } catch {
                return new ConfiguracionApp();
            }
        }

        public static void Guardar(ConfiguracionApp config) {
            var json = JsonSerializer.Serialize(config, _opts);
            File.WriteAllText(RutasApp.RutaConfiguracion, json);
        }
    }
}
