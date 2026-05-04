using aDVanceERP.Core.Infraestructura.Globales;

using System.Collections.Concurrent;

namespace aDVanceERP.Core.Eventos.Comun {
    public static class AgregadorEventos {
        private static readonly ConcurrentDictionary<Type, List<Delegate>> _subscriptoresTipados = new();

        public static void Suscribir<TEvent>(Action<TEvent> handler) where TEvent : class {
            var list = _subscriptoresTipados.GetOrAdd(typeof(TEvent), _ => new List<Delegate>());
            lock (list) { list.Add(handler); }
        }

        public static void Desuscribir<TEvent>(Action<TEvent> handler) where TEvent : class {
            if (_subscriptoresTipados.TryGetValue(typeof(TEvent), out var list)) {
                lock (list) { list.Remove(handler); }
            }
        }

        public static void Publicar<TEvent>(TEvent @event) where TEvent : class {
            if (_subscriptoresTipados.TryGetValue(typeof(TEvent), out var list)) {
                List<Delegate> snapshot;
                lock (list) { snapshot = list.ToList(); }
                foreach (var d in snapshot) {
                    try {
                        ((Action<TEvent>) d)(@event);
                    } catch (Exception ex) {
                        CentroNotificaciones.MostrarNotificacion($"Error manejando el evento {@event?.GetType().Name}: {ex.Message}", Modelos.Comun.TipoNotificacionEnum.Error);
                    }
                }
            }
        }
    }
}
