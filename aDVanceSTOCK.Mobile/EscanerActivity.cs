// ============================================================
//  aDVancePOS.Mobile — EscanerActivity (CON ROTACIÓN INTELIGENTE)
//  Archivo: EscanerActivity.cs
// ============================================================

using Android.Content;
using Android.Graphics;
using Android.Hardware.Camera2;
using Android.Hardware.Camera2.Params;
using Android.Media;
using Android.OS;
using Android.Views;

using Java.Util.Concurrent;

using ZXing;
using ZXing.Common;

namespace aDVanceSTOCK.Mobile {

    [Activity(
        Label = "Escanear código",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class EscanerActivity : Activity, ISurfaceHolderCallback {

        public const int RequestCode = 2001;
        public const string ExtraCodigoBarras = "codigo_barras";

        private const int AnchoCapturaAnalisis = 640;
        private const int AltoCapturaAnalisis = 480;

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

        // ── Modos de escaneo ──────────────────────────────────
        private bool _modoBarras = false;
        private int _framesSinExito = 0;

        // ── Rotación ─────────────────────────────────────────
        private int _rotacionSensor = 0; // Rotación física del sensor
        private bool _necesitaRotacion = false;

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
                    BarcodeFormat.EAN_13, BarcodeFormat.EAN_8,
                    BarcodeFormat.CODE_128, BarcodeFormat.CODE_39,
                    BarcodeFormat.UPC_A, BarcodeFormat.UPC_E,
                    BarcodeFormat.QR_CODE
                },
                [DecodeHintType.TRY_HARDER] = true,
                [DecodeHintType.PURE_BARCODE] = false
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
                var facingObj = caract.Get(CameraCharacteristics.LensFacing);

                if (facingObj != null &&
                    ((Java.Lang.Integer) facingObj).IntValue() == (int) LensFacing.Back) {
                    idCamara = id;

                    // Obtener orientación del sensor
                    var sensorOrientation = caract.Get(CameraCharacteristics.SensorOrientation);
                    if (sensorOrientation != null) {
                        _rotacionSensor = (int) sensorOrientation;
                    }
                    break;
                }
            }

            if (idCamara == null) {
                MostrarErrorYSalir("No se encontró cámara trasera.");
                return;
            }

            _imageReader = ImageReader.NewInstance(
                AnchoCapturaAnalisis, AltoCapturaAnalisis,
                ImageFormatType.Yuv420888, maxImages: 1);

            _imageReader.SetOnImageAvailableListener(
                new ImageDisponibleListener(ProcesarFrameYuv), _handlerCamera);

            _cameraManager.OpenCamera(idCamara, new CameraStateCallback(this), _handlerCamera);
        }

        internal void OnCameraAbierta(CameraDevice camara) {
            _camaraDevice = camara;
            IniciarSesionPreview();
        }

        // ── Camera2: sesión de preview ────────────────────────
        private void IniciarSesionPreview() {
            if (_camaraDevice == null || _imageReader == null) return;

            var superficiePreview = _surfaceView.Holder!.Surface!;
            var superficieAnalisis = _imageReader.Surface!;

            var outputs = new List<OutputConfiguration> {
                new OutputConfiguration(superficiePreview),
                new OutputConfiguration(superficieAnalisis)
            };

            var config = new SessionConfiguration(
                (int) SessionType.Regular, outputs,
                Executors.NewSingleThreadExecutor()!,
                new CaptureSessionCallback(this));

            _camaraDevice.CreateCaptureSession(config);
        }

        internal void OnSesionConfigurada(CameraCaptureSession sesion) {
            _captureSession = sesion;
            IniciarCapturaContinua();
        }

        private void IniciarCapturaContinua() {
            if (_captureSession == null || _camaraDevice == null) return;

            try {
                var requestBuilder = _camaraDevice.CreateCaptureRequest(CameraTemplate.Preview)!;
                requestBuilder.AddTarget(_surfaceView.Holder!.Surface!);
                requestBuilder.AddTarget(_imageReader!.Surface!);

                // Configuración de enfoque
                requestBuilder.Set(CaptureRequest.ControlAfMode, (int) ControlAFMode.ContinuousPicture);
                requestBuilder.Set(CaptureRequest.ControlAeMode, (int) ControlAEMode.On);
                requestBuilder.Set(CaptureRequest.ControlAwbMode, (int) ControlAwbMode.Auto);
                requestBuilder.Set(CaptureRequest.EdgeMode, (int) EdgeMode.Fast);
                requestBuilder.Set(CaptureRequest.NoiseReductionMode, (int) NoiseReductionMode.Fast);

                _captureSession.SetRepeatingRequest(requestBuilder.Build()!, null, _handlerCamera);

            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine($"Error configurando captura: {ex.Message}");
            }
        }

        // ── Procesamiento de frames con rotación inteligente ──
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

                ZXing.Result? resultado = null;

                // ===== ROTACIÓN INTELIGENTE =====
                // Si estamos en modo barras, rotamos la imagen 90 grados
                // porque los códigos de barras necesitan orientación horizontal
                if (_modoBarras) {
                    // Rotar la imagen 90 grados para orientación horizontal
                    byte[] bytesRotados = RotarImagen90(bytesY, ancho, alto);

                    var fuenteRotada = new PlanarYUVLuminanceSource(
                        bytesRotados, alto, ancho, 0, 0, alto, ancho, false);

                    var bitmap = new BinaryBitmap(new GlobalHistogramBinarizer(fuenteRotada));

                    try {
                        resultado = _lector.decode(bitmap);
                    } catch (ReaderException) { }

                } else {
                    // Modo QR - usar imagen original
                    var fuente = new PlanarYUVLuminanceSource(
                        bytesY, ancho, alto, 0, 0, ancho, alto, false);

                    var bitmap = new BinaryBitmap(new HybridBinarizer(fuente));

                    try {
                        resultado = _lector.decode(bitmap);
                    } catch (ReaderException) { }
                }

                // ===== DETECCIÓN DE MODO =====
                if (resultado != null) {
                    _framesSinExito = 0;
                    _modoBarras = (resultado.BarcodeFormat != BarcodeFormat.QR_CODE);
                } else {
                    _framesSinExito++;

                    // Cambiar de modo cada 30 frames sin éxito
                    if (_framesSinExito > 30) {
                        _modoBarras = !_modoBarras;
                        _framesSinExito = 0;
                        System.Diagnostics.Debug.WriteLine($"Cambiando a modo: {(_modoBarras ? "BARRAS" : "QR")}");
                    }
                }

                // ===== RESULTADO =====
                if (resultado?.Text != null) {
                    _escaneado = true;
                    var codigo = resultado.Text;
                    RunOnUiThread(() => {
                        var intent = new Intent();
                        intent.PutExtra(ExtraCodigoBarras, codigo);
                        SetResult(Android.App.Result.Ok, intent);
                        Finish();
                    });
                }

            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine($"Error procesando frame: {ex.Message}");
            } finally {
                imagen?.Close();
            }
        }

        /// <summary>
        /// Rota una imagen YUV 90 grados (optimizada para rendimiento)
        /// </summary>
        private byte[] RotarImagen90(byte[] yuvData, int width, int height) {
            byte[] rotated = new byte[yuvData.Length];
            int index = 0;

            // Rotar 90 grados en sentido horario
            for (int x = 0; x < width; x++) {
                for (int y = height - 1; y >= 0; y--) {
                    rotated[index++] = yuvData[y * width + x];
                }
            }

            return rotated;
        }

        /// <summary>
        /// Versión alternativa: rota 270 grados si es necesario
        /// </summary>
        private byte[] RotarImagen270(byte[] yuvData, int width, int height) {
            byte[] rotated = new byte[yuvData.Length];
            int index = 0;

            // Rotar 270 grados (o 90 anti-horario)
            for (int x = width - 1; x >= 0; x--) {
                for (int y = 0; y < height; y++) {
                    rotated[index++] = yuvData[y * width + x];
                }
            }

            return rotated;
        }

        // ── Limpieza ──────────────────────────────────────────
        private void LiberarCamera() {
            try {
                _captureSession?.Close();
                _captureSession = null;
                _camaraDevice?.Close();
                _camaraDevice = null;
                _imageReader?.Close();
                _imageReader = null;
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine($"Error liberando cámara: {ex.Message}");
            } finally {
                DetenerHiloCamera();
            }
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

    // Callbacks sin cambios...
    internal sealed class CameraStateCallback : CameraDevice.StateCallback {
        private readonly EscanerActivity _activity;
        public CameraStateCallback(EscanerActivity activity) => _activity = activity;
        public override void OnOpened(CameraDevice camera) => _activity.OnCameraAbierta(camera);
        public override void OnDisconnected(CameraDevice camera) => camera.Close();
        public override void OnError(CameraDevice camera, CameraError error) {
            camera.Close();
            _activity.RunOnUiThread(() => {
                Toast.MakeText(_activity, $"Error de cámara: {error}", ToastLength.Long)?.Show();
                _activity.Finish();
            });
        }
    }

    internal sealed class CaptureSessionCallback : CameraCaptureSession.StateCallback {
        private readonly EscanerActivity _activity;
        public CaptureSessionCallback(EscanerActivity activity) => _activity = activity;
        public override void OnConfigured(CameraCaptureSession session) => _activity.OnSesionConfigurada(session);
        public override void OnConfigureFailed(CameraCaptureSession session) {
            _activity.RunOnUiThread(() => {
                Toast.MakeText(_activity, "No se pudo configurar la sesión de cámara.", ToastLength.Long)?.Show();
                _activity.Finish();
            });
        }
    }

    internal sealed class ImageDisponibleListener : Java.Lang.Object, ImageReader.IOnImageAvailableListener {
        private readonly Action<ImageReader> _onDisponible;
        public ImageDisponibleListener(Action<ImageReader> onDisponible) => _onDisponible = onDisponible;
        public void OnImageAvailable(ImageReader? reader) {
            if (reader != null) _onDisponible(reader);
        }
    }
}