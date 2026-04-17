using System.Text.Json.Serialization;

using aDVancePOS.Mobile.Modelos;
using aDVancePOS.Mobile.Servicios;

namespace aDVancePOS.Mobile.Modelos {

    [JsonSerializable(typeof(CatalogoJson))]
    [JsonSerializable(typeof(CatalogoMeta))]
    [JsonSerializable(typeof(List<ProductoCatalogo>))]
    [JsonSerializable(typeof(ProductoCatalogo))]
    [JsonSerializable(typeof(List<PresentacionVenta>))]
    [JsonSerializable(typeof(PresentacionVenta))]
    [JsonSerializable(typeof(List<MonedaCatalogo>))]
    [JsonSerializable(typeof(MonedaCatalogo))]
    [JsonSerializable(typeof(VentasExportacionJson))]
    [JsonSerializable(typeof(ExportacionMeta))]
    [JsonSerializable(typeof(List<VentaExportacion>))]
    [JsonSerializable(typeof(VentaExportacion))]
    [JsonSerializable(typeof(List<DetalleExportacion>))]
    [JsonSerializable(typeof(DetalleExportacion))]
    [JsonSerializable(typeof(List<PagoExportacion>))]
    [JsonSerializable(typeof(PagoExportacion))]
    [JsonSerializable(typeof(List<PagoDetalleMoneda>))]
    [JsonSerializable(typeof(PagoDetalleMoneda))]
    [JsonSerializable(typeof(ConfiguracionApp))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.Unspecified)]
    internal partial class JsonContexto : JsonSerializerContext { }
}
