// ============================================================
//  aDVancePOS.Mobile — OnboardingActivity
//  Archivo: OnboardingActivity.cs
//
//  Se muestra UNA sola vez al instalar la app.
//  3 páginas informativas + pantalla de licencia.
//  Al aceptar la licencia, guarda la preferencia y va a MainActivity.
//
//  ÍCONOS ESPERADOS en Resources/drawable/:
//    ic_onboarding_1.png  — pantalla bienvenida
//    ic_onboarding_2.png  — pantalla funciones
//    ic_onboarding_3.png  — pantalla sincronización
// ============================================================

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace aDVancePOS.Mobile {

    [Activity(
        Label = "Bienvenido",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class OnboardingActivity : Activity {

        // Clave en SharedPreferences para recordar que el onboarding fue completado
        public const string PrefOnboardingCompletado = "onboarding_completado";
        public const string NombrePrefs             = "advancepos_prefs";

        private FrameLayout  _frameOnboarding = null!;
        private LinearLayout _layoutIndicadores = null!;
        private Button       _btnSiguiente = null!;
        private Button       _btnOmitir = null!;

        private int _paginaActual = 0;

        // Datos de las 3 páginas: (icono drawable, título, descripción)
        private readonly (string Icono, string Titulo, string Desc)[] _paginas = {
            (
                "ic_onboarding_1",
                "Bienvenido a aDVance POS",
                "El punto de venta móvil de aDVance ERP.\nGestiona tus ventas desde cualquier lugar, sin necesidad de conexión a internet."
            ),
            (
                "ic_onboarding_2",
                "Cobra de cualquier forma",
                "Acepta pagos en efectivo, transferencia bancaria o una combinación de ambos, en múltiples monedas con conversión automática a la moneda base."
            ),
            (
                "ic_onboarding_3",
                "Sincroniza con el ERP",
                "Por la mañana importa el catálogo de productos.\nAl cerrar el día, exporta todas las ventas al sistema central con un solo comando."
            )
        };

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            SetContentView(Resource.Layout.activity_onboarding);

            _frameOnboarding   = FindViewById<FrameLayout>(Resource.Id.frameOnboarding)!;
            _layoutIndicadores = FindViewById<LinearLayout>(Resource.Id.layoutIndicadores)!;
            _btnSiguiente      = FindViewById<Button>(Resource.Id.btnSiguiente)!;
            _btnOmitir         = FindViewById<Button>(Resource.Id.btnOmitir)!;

            CrearIndicadores();
            MostrarPagina(0);

            _btnSiguiente.Click += (s, e) => {
                if (_paginaActual < _paginas.Length - 1) {
                    MostrarPagina(_paginaActual + 1);
                } else {
                    // Última página — ir a licencia
                    IrALicencia();
                }
            };

            _btnOmitir.Click += (s, e) => IrALicencia();
        }

        private void MostrarPagina(int indice) {
            _paginaActual = indice;

            // Inflar la vista de la página
            var vista = LayoutInflater.Inflate(Resource.Layout.fragment_onboarding_page, null)!;

            var imgOnboarding  = vista.FindViewById<ImageView>(Resource.Id.imgOnboarding)!;
            var lblTitulo      = vista.FindViewById<TextView>(Resource.Id.lblTituloOnboarding)!;
            var lblDesc        = vista.FindViewById<TextView>(Resource.Id.lblDescOnboarding)!;

            // Intentar cargar el ícono personalizado; si no existe mostrar placeholder de color
            var (icono, titulo, desc) = _paginas[indice];
            var idRecurso = Resources!.GetIdentifier(icono, "drawable", PackageName);
            if (idRecurso != 0)
                imgOnboarding.SetImageResource(idRecurso);
            else
                imgOnboarding.SetBackgroundColor(Android.Graphics.Color.ParseColor("#F5E6E6"));

            lblTitulo.Text = titulo;
            lblDesc.Text   = desc;

            // Reemplazar contenido del frame
            _frameOnboarding.RemoveAllViews();
            _frameOnboarding.AddView(vista);

            // Actualizar indicadores
            for (int i = 0; i < _layoutIndicadores.ChildCount; i++) {
                var dot = (View)_layoutIndicadores.GetChildAt(i)!;
                dot.Alpha = i == indice ? 1.0f : 0.3f;
            }

            // Actualizar texto del botón
            bool esUltima = indice == _paginas.Length - 1;
            _btnSiguiente.Text = esUltima ? "Ver licencia →" : "Siguiente →";
            _btnOmitir.Visibility = esUltima
                ? ViewStates.Gone
                : ViewStates.Visible;
        }

        private void CrearIndicadores() {
            _layoutIndicadores.RemoveAllViews();
            var densidad = Resources!.DisplayMetrics!.Density;
            int tamano   = (int)(10 * densidad);
            int margen   = (int)(6 * densidad);

            for (int i = 0; i < _paginas.Length; i++) {
                var dot = new View(this);
                var lp  = new LinearLayout.LayoutParams(tamano, tamano);
                lp.SetMargins(margen, 0, margen, 0);
                dot.LayoutParameters = lp;
                dot.SetBackgroundResource(Resource.Drawable.circle_outline);
                dot.Alpha = 0.3f;
                _layoutIndicadores.AddView(dot);
            }
        }

        private void IrALicencia() {
            StartActivity(new Intent(this, typeof(LicenciaActivity)));
        }

        // ── Método estático helper — verificar si ya fue completado ──
        public static bool FueCompletado(Context context) {
            var prefs = context.GetSharedPreferences(NombrePrefs, FileCreationMode.Private);
            return prefs!.GetBoolean(PrefOnboardingCompletado, false);
        }

        public static void MarcarCompletado(Context context) {
            var prefs = context.GetSharedPreferences(NombrePrefs, FileCreationMode.Private);
            var editor = prefs!.Edit()!;
            editor.PutBoolean(PrefOnboardingCompletado, true);
            editor.Apply();
        }
    }
}
