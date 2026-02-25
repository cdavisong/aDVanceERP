// ============================================================
//  aDVanceSTOCK.Mobile — JsonContexto
//  Archivo: Modelos/JsonContexto.cs
//
//  Source generator de System.Text.Json.
//  OBLIGATORIO para compatibilidad con TrimMode=full (Release).
//  Registrar aquí TODOS los tipos que se serialicen/deserialicen.
// ============================================================

using System.Text.Json.Serialization;
using aDVanceSTOCK.Mobile.Modelos;
using aDVanceSTOCK.Mobile.Servicios;

namespace aDVanceSTOCK.Mobile.Modelos {

    [JsonSerializable(typeof(List<ProductoExistente>))]
    [JsonSerializable(typeof(ProductoExistente))]

    [JsonSerializable(typeof(List<ProveedorCatalogo>))]
    [JsonSerializable(typeof(ProveedorCatalogo))]

    [JsonSerializable(typeof(List<UnidadMedidaCatalogo>))]
    [JsonSerializable(typeof(UnidadMedidaCatalogo))]

    [JsonSerializable(typeof(List<ClasificacionCatalogo>))]
    [JsonSerializable(typeof(ClasificacionCatalogo))]

    [JsonSerializable(typeof(List<AlmacenCatalogo>))]
    [JsonSerializable(typeof(AlmacenCatalogo))]

    [JsonSerializable(typeof(ExportacionSesion))]
    [JsonSerializable(typeof(List<ProductoExportacion>))]
    [JsonSerializable(typeof(ProductoExportacion))]

    [JsonSerializable(typeof(ConfiguracionApp))]

    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.Unspecified)]
    internal partial class JsonContexto : JsonSerializerContext { }
}
