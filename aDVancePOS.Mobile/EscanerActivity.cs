// ============================================================
//  aDVancePOS.Mobile — EscanerActivity
//  Archivo: EscanerActivity.cs
//
//  Escáner de código de barras usando Camera2 API nativa +
//  ZXing.Net (solo el core, sin UI ni dependencias AndroidX).
//
//  PAQUETE NUGET: ZXing.Net 0.16.11
//  ARCHIVOS REQUERIDOS:
//    Resources/layout/activity_escaner.xml
//    Resources/drawable/escaner_marco.xml
// ============================================================

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Hardware.Camera2;
using Android.Hardware.Camera2.Params;
using Android.Media;
using Android.OS;
using Android.Views;
using Android.Widget;

using Java.Util.Concurrent;

using ZXing;
using ZXing.Common;

namespace aDVancePOS.Mobile {

    [Activity(
        Label = "Escanear código",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class EscanerActivity : Activity, ISurfaceHolderCallback {

        public const int RequestCode = 2001;
        public const string ExtraCodigoBarras = "codigo_barras";

        private const int AnchoCapturaAnalisis = 1280;
        private const int AltoCapturaAnalisis = 720;

        // ── Vistas ───────────────────────────────────────────
        private SurfaceView _surfaceView = null!;
        private Button _btnCancelar = null!;

        // ── Camera2 ──────────────────────────────────────────
        private CameraManager _cameraManager = null!;
        private CameraDevice? _camaraDevice;
        private CameraCaptureSession? _captureSession;
        private ImageReader? _imageReader;
        private HandlerThread? _hiloCamera;
        private Handler? _handlerCamera;

        // ── ZXing ────────────────────────────────────────────
        private readonly MultiFormatReader _lector = new();
        private volatile bool _escaneado = false;

        // ─────────────────────────────────────────────────────

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            SetContentView(Resource.Layout.activity_escaner);

            _surfaceView = FindViewById<SurfaceView>(Resource.Id.surfaceView)!;
            _btnCancelar = FindViewById<Button>(Resource.Id.btnCancelarEscaneo)!;

            _btnCancelar.Click += (s, e) => {
                SetResult(Android.App.Result.Canceled);
                Finish();
            };

            _lector.Hints = new Dictionary<DecodeHintType, object> {
                [DecodeHintType.POSSIBLE_FORMATS] = new List<BarcodeFormat> {
                    BarcodeFormat.EAN_13,
                    BarcodeFormat.EAN_8,
                    BarcodeFormat.CODE_128,
                    BarcodeFormat.CODE_39,
                    BarcodeFormat.UPC_A,
                    BarcodeFormat.UPC_E,
                    BarcodeFormat.QR_CODE
                },
                [DecodeHintType.TRY_HARDER] = true
            };

            _cameraManager = (CameraManager) GetSystemService(CameraService)!;
            _surfaceView.Holder!.AddCallback(this);
        }

        // ── ISurfaceHolderCallback ────────────────────────────

        public void SurfaceCreated(ISurfaceHolder holder) {
            IniciarHiloCamera();
            AbrirCamara();
        }

        public void SurfaceChanged(ISurfaceHolder holder, Android.Graphics.Format format,
            int width, int height) { }

        public void SurfaceDestroyed(ISurfaceHolder holder) => LiberarCamera();

        // ── Camera2: hilo dedicado ────────────────────────────

        private void IniciarHiloCamera() {
            _hiloCamera = new HandlerThread("CameraEscanerThread");
            _hiloCamera.Start();
            _handlerCamera = new Handler(_hiloCamera.Looper!);
        }

        private void DetenerHiloCamera() {
            _hiloCamera?.QuitSafely();
            _hiloCamera?.Join();
            _hiloCamera = null;
            _handlerCamera = null;
        }

        // ── Camera2: apertura ─────────────────────────────────

        private void AbrirCamara() {
            string? idCamara = null;

            foreach (var id in _cameraManager.GetCameraIdList()!) {
                var caract = _cameraManager.GetCameraCharacteristics(id);

                // FIX 1: El resultado de Get() es Java.Lang.Object — comparar con int directamente
                var facingObj = caract.Get(CameraCharacteristics.LensFacing);
                if (facingObj != null &&
                    ((Java.Lang.Integer) facingObj).IntValue() == (int) LensFacing.Back) {
                    idCamara = id;
                    break;
                }
            }

            if (idCamara == null) {
                MostrarErrorYSalir("No se encontró cámara trasera.");
                return;
            }

            _imageReader = ImageReader.NewInstance(
                AnchoCapturaAnalisis,
                AltoCapturaAnalisis,
                ImageFormatType.Yuv420888,
                maxImages: 2);

            _imageReader.SetOnImageAvailableListener(
                new ImageDisponibleListener(ProcesarFrameYuv),
                _handlerCamera);

            _cameraManager.OpenCamera(idCamara, new CameraStateCallback(this), _handlerCamera);
        }

        internal void OnCameraAbierta(CameraDevice camara) {
            _camaraDevice = camara;
            IniciarSesionPreview();
        }

        // ── Camera2: sesión de preview + análisis ─────────────

        private void IniciarSesionPreview() {
            if (_camaraDevice == null || _imageReader == null) return;

            var superficiePreview = _surfaceView.Holder!.Surface!;
            var superficieAnalisis = _imageReader.Surface!;

            var outputs = new List<OutputConfiguration> {
                new OutputConfiguration(superficiePreview),
                new OutputConfiguration(superficieAnalisis)
            };

            // FIX 3: Usar Executors.NewSingleThreadExecutor() — SerialExecutor no existe
            //        en los bindings de .NET for Android
            var config = new SessionConfiguration(
                (int) SessionType.Regular,
                outputs,
                Executors.NewSingleThreadExecutor()!,
                new CaptureSessionCallback(this));

            _camaraDevice.CreateCaptureSession(config);
        }

        internal void OnSesionConfigurada(CameraCaptureSession sesion) {
            _captureSession = sesion;

            var requestBuilder = _camaraDevice!.CreateCaptureRequest(CameraTemplate.Preview)!;
            requestBuilder.AddTarget(_surfaceView.Holder!.Surface!);
            requestBuilder.AddTarget(_imageReader!.Surface!);

            requestBuilder.Set(
                CaptureRequest.ControlAfMode,
                (int) ControlAFMode.ContinuousPicture);

            // FIX 4: SetRepeatingRequest no tiene parámetros nombrados —
            //        pasar null y handler posicionalmente
            _captureSession.SetRepeatingRequest(requestBuilder.Build()!, null, _handlerCamera);
        }

        // ── Procesamiento de frames YUV con ZXing ─────────────

        private void ProcesarFrameYuv(ImageReader reader) {
            if (_escaneado) return;

            Android.Media.Image? imagen = null;
            try {
                imagen = reader.AcquireNextImage();
                if (imagen == null) return;

                var planoY = imagen.GetPlanes()![0];
                var bufferY = planoY.Buffer!;
                var bytesY = new byte[bufferY.Remaining()];
                bufferY.Get(bytesY);

                int ancho = imagen.Width;
                int alto = imagen.Height;

                var fuente = new PlanarYUVLuminanceSource(
                    bytesY, ancho, alto,
                    0, 0, ancho, alto,
                    false);

                var bitmap = new BinaryBitmap(new HybridBinarizer(fuente));

                ZXing.Result? resultado = null;
                try {
                    resultado = _lector.decode(bitmap);
                } catch (ReaderException) {
                    // Frame sin código — normal, ignorar
                }

                if (resultado != null && !string.IsNullOrEmpty(resultado.Text)) {
                    _escaneado = true;
                    var codigo = resultado.Text;
                    RunOnUiThread(() => {
                        var intent = new Intent();
                        intent.PutExtra(ExtraCodigoBarras, codigo);
                        SetResult(Android.App.Result.Ok, intent);
                        Finish();
                    });
                }
            } finally {
                imagen?.Close(); // Siempre cerrar — si no el ImageReader se bloquea
            }
        }

        // ── Limpieza ──────────────────────────────────────────

        private void LiberarCamera() {
            _captureSession?.Close();
            _captureSession = null;
            _camaraDevice?.Close();
            _camaraDevice = null;
            _imageReader?.Close();
            _imageReader = null;
            DetenerHiloCamera();
        }

        protected override void OnDestroy() {
            LiberarCamera();
            base.OnDestroy();
        }

        private void MostrarErrorYSalir(string mensaje) {
            RunOnUiThread(() => {
                Toast.MakeText(this, mensaje, ToastLength.Long)?.Show();
                SetResult(Android.App.Result.Canceled);
                Finish();
            });
        }
    }


    // ══════════════════════════════════════════════════════════
    //  CALLBACKS — adaptadores Java callbacks → C#
    // ══════════════════════════════════════════════════════════

    internal sealed class CameraStateCallback : CameraDevice.StateCallback {
        private readonly EscanerActivity _activity;
        public CameraStateCallback(EscanerActivity activity) => _activity = activity;

        public override void OnOpened(CameraDevice camera)
            => _activity.OnCameraAbierta(camera);

        public override void OnDisconnected(CameraDevice camera)
            => camera.Close();

        public override void OnError(CameraDevice camera, CameraError error) {
            camera.Close();
            _activity.RunOnUiThread(() => {
                Toast.MakeText(_activity,
                    $"Error de cámara: {error}", ToastLength.Long)?.Show();
                _activity.Finish();
            });
        }
    }

    internal sealed class CaptureSessionCallback : CameraCaptureSession.StateCallback {
        private readonly EscanerActivity _activity;
        public CaptureSessionCallback(EscanerActivity activity) => _activity = activity;

        public override void OnConfigured(CameraCaptureSession session)
            => _activity.OnSesionConfigurada(session);

        public override void OnConfigureFailed(CameraCaptureSession session) {
            _activity.RunOnUiThread(() => {
                Toast.MakeText(_activity,
                    "No se pudo configurar la sesión de cámara.", ToastLength.Long)?.Show();
                _activity.Finish();
            });
        }
    }

    internal sealed class ImageDisponibleListener : Java.Lang.Object,
        ImageReader.IOnImageAvailableListener {

        private readonly Action<ImageReader> _onDisponible;

        public ImageDisponibleListener(Action<ImageReader> onDisponible)
            => _onDisponible = onDisponible;

        public void OnImageAvailable(ImageReader? reader) {
            if (reader != null) _onDisponible(reader);
        }
    }
}