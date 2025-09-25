using _Microsoft.Android.Resource.Designer;

using aDVanceSCANNER.Controladores;
using aDVanceSCANNER.Modelos;

using Android.Content;
using Android.Views;
using Android.Views.InputMethods;

namespace aDVanceSCANNER
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity {
        private Button? _btnEscanear;
        private TextView? _textoResultadoEscaneo;
        private ControladorUI? _controladorUI;
        private ControladorPermisosApp? _controladorPermisosApp;
        private ControladorScanner? _controladorScanner;
        private ControladorHistorial? _controladorHistorial;
        private ClienteTCP? _clienteTcp;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Configuración inicial de la ventana
            RequestWindowFeature(WindowFeatures.NoTitle);
            Window?.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);            

            SetContentView(ResourceConstant.Layout.activity_main);

            // Inicializar componentes
            _clienteTcp = new ClienteTCP();

            // Inicializar controladores
            _controladorUI = new ControladorUI(this, Window.DecorView.RootView);
            _controladorPermisosApp = new ControladorPermisosApp(this);
            _controladorHistorial = new ControladorHistorial(this, FindViewById<LinearLayout>(ResourceConstant.Id.layoutHistorial));
            _controladorScanner = new ControladorScanner(this);

            // Verificar permisos
            _controladorPermisosApp.ComprobarPermisos();

            // Configurar eventos
            _btnEscanear = FindViewById<Button>(ResourceConstant.Id.scanButton);
            _textoResultadoEscaneo = FindViewById<TextView>(ResourceConstant.Id.resultText);

            if (_controladorUI.BtnConectar != null) _controladorUI.BtnConectar.Click += ConectarCliente;
            if (_btnEscanear != null) _btnEscanear.Click += EscanearCodigo;

            OcultarInterfazSistema();
            CargarOpcionesConexion();
        }

        private async void ConectarCliente(object? sender, EventArgs e) {
            OcultarTeclado();

            if (_clienteTcp != null && _controladorUI?.DireccionIP != null && !_clienteTcp.EstablecerDireccionIp(_controladorUI.DireccionIP)) {
                Toast.MakeText(this, "Ingrese una dirección IP válida", ToastLength.Long)?.Show();
                return;
            }

            if (_clienteTcp != null && !_clienteTcp.EstablecerPuerto(int.TryParse(_controladorUI?.Puerto, out var puerto) ? puerto : 0)) {
                Toast.MakeText(this, "Ingrese un puerto válido", ToastLength.Long)?.Show();
                return;
            }

            _controladorUI?.ActualizarEstadoConexion("Conectando...");

            try {
                if (_clienteTcp is { Conectado: true }) {
                    await _clienteTcp.DesconectarAsync();
                    _controladorUI?.ActualizarEstadoConexion("Desconectado");
                    _controladorUI?.MostrarCamposConexion();
                    Toast.MakeText(this, "Desconectado manualmente", ToastLength.Short)?.Show();
                    return;
                }

                await Task.Run(async () => {
                    var estado = await _clienteTcp?.ConectarAsync()!;
                    RunOnUiThread(() => Toast.MakeText(this, estado, ToastLength.Long)?.Show());

                    _controladorUI?.ActualizarEstadoConexion(estado);
                });                
                
                SalvarOpcionesConexion(_controladorUI?.DireccionIP ?? "192.168.1.", int.Parse(_controladorUI?.Puerto ?? "0"));

                if (_clienteTcp is not { Conectado: true }) 
                    return;

                Toast.MakeText(this, "Conexión exitosa", ToastLength.Short)?.Show();

                _controladorUI?.OcultarCamposConexion();
            } catch (Exception ex) {
                _controladorUI?.ActualizarEstadoConexion("Error de conexión");
                _controladorUI?.MostrarCamposConexion();

                Toast.MakeText(this, $"Error: {ex.Message}", ToastLength.Long)?.Show();
            }
        }

        private void SalvarOpcionesConexion(string ip, int port) {
            var prefs = GetSharedPreferences("ConnectionPrefs", FileCreationMode.Private);
            var editor = prefs?.Edit();

            editor?.PutString("lastIP", ip);
            editor?.PutInt("lastPort", port);
            editor?.Commit();
        }

        private void CargarOpcionesConexion() {
            var prefs = GetSharedPreferences("ConnectionPrefs", FileCreationMode.Private);

            if (_controladorUI == null) 
                return;

            _controladorUI.DireccionIP = prefs?.GetString("lastIP", "192.168.1.");
            _controladorUI.Puerto = prefs?.GetInt("lastPort", 9002).ToString();
        }

        private async void EscanearCodigo(object? sender, EventArgs e) {
            var resultado = await _controladorScanner?.EscanearAsync()!;

            if (resultado.Cancelado)
                return;

            if (!resultado.Exitoso) {
                Toast.MakeText(this, $"Error: {resultado.Error}", ToastLength.Long)?.Show();
                return;
            }

            if (_textoResultadoEscaneo != null) _textoResultadoEscaneo.Text = resultado.Contenido;
            if (resultado is not { Contenido: not null, Formato: not null }) return;
            
            _controladorHistorial?.AdicionarResultado(resultado.Contenido, resultado.Formato);
            
            await _clienteTcp?.EnviarAsync(resultado.Contenido)!;
        }

        private void OcultarInterfazSistema() {
            Window.InsetsController?.Hide(WindowInsets.Type.SystemBars());
            Window.SetDecorFitsSystemWindows(false);
        }

        private void OcultarTeclado() {
            var inputMethodManager = (InputMethodManager) GetSystemService(InputMethodService)!;
            var currentFocus = CurrentFocus;

            if (currentFocus != null) {
                inputMethodManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
            }

            // También limpia el foco del EditText
            _controladorUI.FieldDireccionIp.ClearFocus();
            _controladorUI.FieldPuerto.ClearFocus();
        }

        protected override void OnResume() {
            base.OnResume();
            
            // Restaurar pantalla completa cuando la actividad se reanuda
            OcultarInterfazSistema();
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            _controladorScanner?.Dispose();
            _clienteTcp?.Dispose();
        }
    }
}