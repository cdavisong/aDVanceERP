using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace aDVanceERP.Core.Infraestructura.Extensiones.Comun {
    public static class EnumExt {
        public static string ObtenerDisplayName(this Enum valor) {
            var campo = valor.GetType().GetField(valor.ToString());
            var atributo = campo?.GetCustomAttribute<DisplayAttribute>();

            return atributo?.Name ?? valor.ToString();
        }

        public static IEnumerable<string> ObtenerDisplayNames<TEnum>() where TEnum : Enum {
            return typeof(TEnum).GetFields()
                .Where(f => f.IsStatic)
                .Select(f => f.GetCustomAttribute<DisplayAttribute>()?.Name ?? f.Name);
        }
    }
}
