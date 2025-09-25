using Android.Content;
using ZXing.Mobile;
using ZXing;

namespace aDVanceSCANNER.Controladores; 

public sealed class ControladorScanner : IDisposable {
    private readonly MobileBarcodeScanner _barcodeScanner;
    private readonly Context _context;
    private bool _disposed;

    public ControladorScanner(Context context) {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        MobileBarcodeScanner.Initialize((Application) context.ApplicationContext!);
        _barcodeScanner = new MobileBarcodeScanner();
    }

    public async Task<ScanResult> EscanearAsync() {
        try {
            var options = new MobileBarcodeScanningOptions {
                PossibleFormats = new List<BarcodeFormat>
                {
                        BarcodeFormat.EAN_13,
                        BarcodeFormat.EAN_8,
                        BarcodeFormat.UPC_A,
                        BarcodeFormat.CODE_128,
                        BarcodeFormat.QR_CODE
                    },
                TryHarder = true,
                UseFrontCameraIfAvailable = false
            };

            // Configurar UI del escáner
            _barcodeScanner.UseCustomOverlay = false;
            _barcodeScanner.TopText = "Escaneo de código QR/Barra";
            _barcodeScanner.FlashButtonText = "Flash";
            _barcodeScanner.CancelButtonText = "Cancelar";

            var result = await _barcodeScanner.Scan(options);

            return result != null
                ? new ScanResult(result.Text, result.BarcodeFormat.ToString())
                : ScanResult.EscaneoCancelado;
        } catch (Exception ex) {
            return new ScanResult(ex);
        }
    }

    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing) {
        if (_disposed) return;

        if (disposing) {
            // Limpiar recursos si es necesario
        }

        _disposed = true;
    }
}

public class ScanResult {
    public bool Exitoso { get; }
    public bool Cancelado { get; private init; }
    public string? Contenido { get; set; }
    public string? Formato { get; }
    public string? Error { get; }

    public static ScanResult EscaneoCancelado { get; } = new ScanResult {
        Cancelado = true
    };

    public ScanResult(string? contenido, string? formato) {
        Exitoso = true;
        Contenido = contenido;
        Formato = formato;
    }

    public ScanResult(Exception ex) {
        Error = ex.Message;
    }

    private ScanResult() { } // Para el resultado Cancelado
}