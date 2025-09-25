using System.Text.Json;

namespace aDVanceERP.Core.Eventos;

public static class AgregadorEventos {
    private static readonly Dictionary<string, List<Action<string>>> _suscriptores =
        new Dictionary<string, List<Action<string>>>();

    // Suscribe un manejador a un tipo de evento
    public static void Subscribir(string eventType, Action<string> handler) {
        if (!_suscriptores.ContainsKey(eventType)) {
            _suscriptores[eventType] = new List<Action<string>>();
        }
        _suscriptores[eventType].Add(handler);
    }

    // Desuscribe un manejador
    public static void Desuscribir(string eventType, Action<string> handler) {
        if (_suscriptores.ContainsKey(eventType)) {
            _suscriptores[eventType].Remove(handler);
        }
    }

    // Publica un evento con un payload (datos en JSON)
    public static void Publicar(string eventType, string jsonPayload) {
        if (_suscriptores.ContainsKey(eventType)) {
            foreach (var handler in _suscriptores[eventType]) {
                try {
                    handler(jsonPayload);
                }
                catch (Exception ex) {
                    Console.WriteLine($"Error manejando el evento {eventType}: {ex.Message}");
                }
            }
        }
    }

    // Helper para serializar el payload a JSON
    public static string SerializarPayload(object data) {
        return JsonSerializer.Serialize(data);
    }

    // Helper para deserializar el JSON a un tipo específico
    public static T DeserializarPayload<T>(string json) {
        return JsonSerializer.Deserialize<T>(json)!;
    }
}