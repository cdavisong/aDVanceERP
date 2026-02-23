using System.Text.Json.Serialization;

using aDVancePOS.Mobile.Modelos;
using aDVancePOS.Mobile.Servicios; // ConfiguracionApp está en Servicios

namespace aDVancePOS.Mobile.Modelos {

    [JsonSerializable(typeof(CatalogoJson))]
    [JsonSerializable(typeof(CatalogoMeta))]
    [JsonSerializable(typeof(List<ProductoCatalogo>))]
    [JsonSerializable(typeof(ProductoCatalogo))]
    [JsonSerializable(typeof(VentasExportacionJson))]
    [JsonSerializable(typeof(ExportacionMeta))]
    [JsonSerializable(typeof(List<VentaExportacion>))]
    [JsonSerializable(typeof(VentaExportacion))]
    [JsonSerializable(typeof(List<DetalleExportacion>))]
    [JsonSerializable(typeof(DetalleExportacion))]
    [JsonSerializable(typeof(List<PagoExportacion>))]
    [JsonSerializable(typeof(PagoExportacion))]
    [JsonSerializable(typeof(DetalleTransferenciaExportacion))]
    [JsonSerializable(typeof(ConfiguracionApp))] // namespace Servicios — incluido arriba
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.Unspecified)]
    internal partial class JsonContexto : JsonSerializerContext { }
}
