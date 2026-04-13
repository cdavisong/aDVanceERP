using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace aDVanceERP.Core.Infraestructura.Extensiones.Comun {
    public static class EnumExt {
        public static (string Nombre, string Descripcion) ObtenerNombreDescripcion(this Enum valor) {
            var campo = valor.GetType().GetField(valor.ToString());
            var atributo = campo?.GetCustomAttribute<DisplayAttribute>();

            return (atributo != null && !string.IsNullOrEmpty(atributo.Name)) 
                ? (atributo.Name, atributo.Description ?? string.Empty) 
                : (valor.ToString(), string.Empty);
        }

        public static IEnumerable<(string Nombre, string Descripcion)> ObtenerNombresDescripciones<TEnum>() where TEnum : Enum {
            return typeof(TEnum).GetFields()
                .Where(f => f.IsStatic)
                .Select(f => {
                    var atributo = f.GetCustomAttribute<DisplayAttribute>();
                    return (Nombre: atributo?.Name ?? f.Name, Descripcion: atributo?.Description ?? string.Empty);
                });
        }
    }
}
