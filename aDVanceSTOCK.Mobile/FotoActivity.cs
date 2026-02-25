// ============================================================
//  aDVanceSTOCK.Mobile — FotoActivity
//  Archivo: FotoActivity.cs
//
//  Captura de foto con Camera2 nativa (sin AndroidX, sin FileProvider).
//  Al confirmar la captura, guarda el JPEG en la ruta indicada
//  por ExtraRutaDestino y devuelve Result.Ok.
//
//  Flujo:
//    1. Muestra preview en SurfaceView
//    2. Botón capturar → toma foto JPEG
//    3. Botón confirmar → guarda en ruta destino y cierra
//    4. Botón reintentar → vuelve al preview
// ============================================================

using aDVanceSTOCK.Mobile.Servicios;

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

namespace aDVanceSTOCK.Mobile {

    [Activity(
        Label = "Tomar foto",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class FotoActivity : Activity, ISurfaceHolderCallback {

        public const int    RequestCode      = 3001;
        public const string ExtraRutaDestino = "ruta_destino";

        private const int AnchoCap = 1280;
        private const int AltoCap  = 960;

        // ── Destino ───────────────────────────────────────────────────
        private string _rutaDestino = "";

        // ── Vistas ───────────────────────────────────────────────────
        private SurfaceView  _surfaceView    = null!;
        private ImageView    _imgPreview     = null!;
        private LinearLayout _panelPreview   = null!;
        private LinearLayout _panelRevision  = null!;
        private Button       _btnCapturar    = null!;
        private Button       _btnConfirmar   = null!;
        private Button       _btnReintentar  = null!;
        private ImageButton  _btnCancelar    = null!;

        // ── Camera2 ───────────────────────────────────────────────────
        private CameraManager          _cameraManager  = null!;
        private CameraDevice?          _camaraDevice;
        private CameraCaptureSession?  _captureSession;
        private ImageReader?           _imageReader;
        private HandlerThread?         _hiloCamera;
        private Handler?               _handlerCamera;

        // ── Estado ────────────────────────────────────────────────────
        private byte[]? _bytesCapturados;

        // ── Ciclo de vida ─────────────────────────────────────────────

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            SetContentView(Resource.Layout.activity_foto);

            _rutaDestino = Intent?.GetStringExtra(ExtraRutaDestino) ?? RutasApp.RutaFotoTemporal;

            _surfaceView   = FindViewById<SurfaceView>(Resource.Id.surfaceFoto)!;
            _imgPreview    = FindViewById<ImageView>(Resource.Id.imgRevision)!;
            _panelPreview  = FindViewById<LinearLayout>(Resource.Id.panelPreview)!;
            _panelRevision = FindViewById<LinearLayout>(Resource.Id.panelRevision)!;
            _btnCapturar   = FindViewById<Button>(Resource.Id.btnCapturar)!;
            _btnConfirmar  = FindViewById<Button>(Resource.Id.btnConfirmar)!;
            _btnReintentar = FindViewById<Button>(Resource.Id.btnReintentar)!;
            _btnCancelar   = FindViewById<ImageButton>(Resource.Id.btnCancelarFoto)!;

            _btnCancelar.Click   += (s, e) => { SetResult(Result.Canceled); Finish(); };
            _btnCapturar.Click   += (s, e) => CapturarFoto();
            _btnConfirmar.Click  += (s, e) => ConfirmarFoto();
            _btnReintentar.Click += (s, e) => Reintentar();

            _cameraManager = (CameraManager)GetSystemService(CameraService)!;
            _surfaceView.Holder!.AddCallback(this);

            MostrarPanelPreview();
        }

        // ── ISurfaceHolderCallback ────────────────────────────────────

        public void SurfaceCreated(ISurfaceHolder holder) {
            IniciarHiloCamera();
            AbrirCamara();
        }

        public void SurfaceChanged(ISurfaceHolder holder, Android.Graphics.Format format,
            int width, int height) { }

        public void SurfaceDestroyed(ISurfaceHolder holder) => LiberarCamera();

        // ── Camera2 ───────────────────────────────────────────────────

        private void IniciarHiloCamera() {
            _hiloCamera = new HandlerThread("CameraFotoThread");
            _hiloCamera.Start();
            _handlerCamera = new Handler(_hiloCamera.Looper!);
        }

        private void DetenerHiloCamera() {
            _hiloCamera?.QuitSafely();
            _hiloCamera?.Join();
            _hiloCamera = null;
            _handlerCamera = null;
        }

        private void AbrirCamara() {
            string? idCamara = null;
            foreach (var id in _cameraManager.GetCameraIdList()!) {
                var caract   = _cameraManager.GetCameraCharacteristics(id);
                var facing   = caract.Get(CameraCharacteristics.LensFacing);
                if (facing != null &&
                    ((Java.Lang.Integer)facing).IntValue() == (int)LensFacing.Back) {
                    idCamara = id;
                    break;
                }
            }
            if (idCamara == null) {
                Toast.MakeText(this, "No se encontró cámara trasera.", ToastLength.Long)?.Show();
                SetResult(Result.Canceled);
                Finish();
                return;
            }

            _imageReader = ImageReader.NewInstance(AnchoCap, AltoCap, ImageFormatType.Jpeg, maxImages: 1);
            _imageReader.SetOnImageAvailableListener(
                new FotoDisponibleListener(OnFotoDisponible), _handlerCamera);

            _cameraManager.OpenCamera(idCamara,
                new FotoCameraStateCallback(this), _handlerCamera);
        }

        internal void OnCameraAbierta(CameraDevice camara) {
            _camaraDevice = camara;
            IniciarSesionPreview();
        }

        private void IniciarSesionPreview() {
            if (_camaraDevice == null || _imageReader == null) return;

            var superficiePreview = _surfaceView.Holder!.Surface!;
            var superficieCaptura = _imageReader.Surface!;

            var outputs = new List<OutputConfiguration> {
                new OutputConfiguration(superficiePreview),
                new OutputConfiguration(superficieCaptura)
            };

            var config = new SessionConfiguration(
                (int)SessionType.Regular, outputs,
                Executors.NewSingleThreadExecutor()!,
                new FotoCaptureSessionCallback(this));

            _camaraDevice.CreateCaptureSession(config);
        }

        internal void OnSesionConfigurada(CameraCaptureSession sesion) {
            _captureSession = sesion;
            IniciarPreviewContinuo();
        }

        private void IniciarPreviewContinuo() {
            if (_captureSession == null || _camaraDevice == null) return;
            try {
                var req = _camaraDevice.CreateCaptureRequest(CameraTemplate.Preview)!;
                req.AddTarget(_surfaceView.Holder!.Surface!);
                req.Set(CaptureRequest.ControlAfMode, (int)ControlAFMode.ContinuousPicture);
                req.Set(CaptureRequest.ControlAeMode, (int)ControlAEMode.On);
                _captureSession.SetRepeatingRequest(req.Build()!, null, _handlerCamera);
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine($"Error preview: {ex.Message}");
            }
        }

        // ── Captura ───────────────────────────────────────────────────

        private void CapturarFoto() {
            if (_captureSession == null || _camaraDevice == null || _imageReader == null) return;
            try {
                _captureSession.StopRepeating();
                var req = _camaraDevice.CreateCaptureRequest(CameraTemplate.StillCapture)!;
                req.AddTarget(_imageReader.Surface!);
                req.Set(CaptureRequest.ControlAfMode, (int)ControlAFMode.ContinuousPicture);
                req.Set(CaptureRequest.JpegQuality, (Java.Lang.Byte)(sbyte)85);
                _captureSession.Capture(req.Build()!, null, _handlerCamera);
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine($"Error captura: {ex.Message}");
            }
        }

        private void OnFotoDisponible(ImageReader reader) {
            Android.Media.Image? img = null;
            try {
                img = reader.AcquireNextImage();
                if (img == null) return;

                var buffer = img.GetPlanes()![0].Buffer!;
                _bytesCapturados = new byte[buffer.Remaining()];
                buffer.Get(_bytesCapturados);

                RunOnUiThread(MostrarPanelRevision);
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine($"Error leyendo foto: {ex.Message}");
            } finally {
                img?.Close();
            }
        }

        // ── Revisión / confirmación ───────────────────────────────────

        private void MostrarPanelRevision() {
            _panelPreview.Visibility  = ViewStates.Gone;
            _panelRevision.Visibility = ViewStates.Visible;

            if (_bytesCapturados != null) {
                var bmp = BitmapFactory.DecodeByteArray(_bytesCapturados, 0, _bytesCapturados.Length);
                _imgPreview.SetImageBitmap(bmp);
            }
        }

        private void MostrarPanelPreview() {
            _panelPreview.Visibility  = ViewStates.Visible;
            _panelRevision.Visibility = ViewStates.Gone;
        }

        private void Reintentar() {
            _bytesCapturados = null;
            _imgPreview.SetImageBitmap(null);
            MostrarPanelPreview();
            IniciarPreviewContinuo();
        }

        private void ConfirmarFoto() {
            if (_bytesCapturados == null) return;
            try {
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(_rutaDestino)!);
                File.WriteAllBytes(_rutaDestino, _bytesCapturados);

                var result = new Intent();
                result.PutExtra(ExtraRutaDestino, _rutaDestino);
                SetResult(Result.Ok, result);
                Finish();
            } catch (Exception ex) {
                Toast.MakeText(this, "Error al guardar foto: " + ex.Message, ToastLength.Long)?.Show();
            }
        }

        // ── Limpieza ──────────────────────────────────────────────────

        private void LiberarCamera() {
            try {
                _captureSession?.Close(); _captureSession = null;
                _camaraDevice?.Close();   _camaraDevice   = null;
                _imageReader?.Close();    _imageReader    = null;
            } catch { }
            finally { DetenerHiloCamera(); }
        }

        protected override void OnDestroy() {
            LiberarCamera();
            base.OnDestroy();
        }
    }

    // ── Callbacks Camera2 ─────────────────────────────────────────────

    internal sealed class FotoCameraStateCallback : CameraDevice.StateCallback {
        private readonly FotoActivity _a;
        public FotoCameraStateCallback(FotoActivity a) => _a = a;
        public override void OnOpened(CameraDevice cam) => _a.OnCameraAbierta(cam);
        public override void OnDisconnected(CameraDevice cam) => cam.Close();
        public override void OnError(CameraDevice cam, CameraError err) {
            cam.Close();
            _a.RunOnUiThread(() => {
                Toast.MakeText(_a, $"Error de cámara: {err}", ToastLength.Long)?.Show();
                _a.SetResult(Result.Canceled);
                _a.Finish();
            });
        }
    }

    internal sealed class FotoCaptureSessionCallback : CameraCaptureSession.StateCallback {
        private readonly FotoActivity _a;
        public FotoCaptureSessionCallback(FotoActivity a) => _a = a;
        public override void OnConfigured(CameraCaptureSession s) => _a.OnSesionConfigurada(s);
        public override void OnConfigureFailed(CameraCaptureSession s) {
            _a.RunOnUiThread(() => {
                Toast.MakeText(_a, "Error configurando sesión de cámara.", ToastLength.Long)?.Show();
                _a.Finish();
            });
        }
    }

    internal sealed class FotoDisponibleListener : Java.Lang.Object, ImageReader.IOnImageAvailableListener {
        private readonly Action<ImageReader> _cb;
        public FotoDisponibleListener(Action<ImageReader> cb) => _cb = cb;
        public void OnImageAvailable(ImageReader? r) { if (r != null) _cb(r); }
    }
}
