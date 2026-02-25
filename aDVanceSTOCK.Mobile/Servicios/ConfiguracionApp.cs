// ============================================================
//  aDVanceSTOCK.Mobile — ConfiguracionApp
//  Archivo: Servicios/ConfiguracionApp.cs
//
//  POCO que se serializa en config.json.
//  El almacén activo se selecciona aquí y se usa como
//  encabezado de todos los JSON de exportación.
// ============================================================

namespace aDVanceSTOCK.Mobile.Servicios {

    public class ConfiguracionApp {
        /// <summary>ID del almacén seleccionado para la sesión.</summary>
        public long   IdAlmacen     { get; set; } = 1;

        /// <summary>Nombre del almacén (desnormalizado para UI y JSON de salida).</summary>
        public string NombreAlmacen { get; set; } = "";
    }
}
