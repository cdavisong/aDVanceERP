// ============================================================
//  aDVancePOS.Mobile — DialogHelper
//  Archivo: DialogHelper.cs
//
//  Sistema centralizado de diálogos custom.
//  Todos los diálogos:
//  - Son centrados en pantalla
//  - Oscurecen el fondo (dim 55%)
//  - Tienen fondo blanco redondeado (14dp)
//  - Ancho fijo (~88% del ancho de pantalla)
//  - Crecen solo verticalmente según el contenido
//
//  Métodos disponibles:
//  - MostrarInfo       : mensaje + un botón OK
//  - MostrarConfirmar  : título + mensaje + confirmar / cancelar
//  - MostrarOpciones   : título + mensaje + N botones verticales
//  - MostrarLista      : título + lista de opciones scrolleable
// ============================================================

using aDVancePOS.Mobile.Vistas;

using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;

namespace aDVancePOS.Mobile {

    public static class DialogHelper {

        // ── Info / Error — mensaje simple con un botón ────────

        public static void MostrarInfo(
            Activity ctx,
            string mensaje,
            string titulo = "Información",
            string textoBoton = "Aceptar") {

            var dialog = CrearDialogo(ctx);
            var root = ConstruirContenedor(ctx);

            if (!string.IsNullOrEmpty(titulo))
                root.AddView(CrearTitulo(ctx, titulo));

            root.AddView(CrearMensaje(ctx, mensaje));
            root.AddView(CrearDivisorBotones(ctx));

            var fila = CrearFilaBotones(ctx);
            fila.AddView(CrearBotonPrimario(ctx, textoBoton, () => dialog.Dismiss()));
            root.AddView(fila);

            MostrarDialogo(ctx, dialog, root);
        }

        // ── Confirmar — título + mensaje + 2 botones ──────────

        public static void MostrarConfirmar(
            Activity ctx,
            string titulo,
            string mensaje,
            string textoConfirmar,
            Action onConfirmar,
            string textoCancelar = "Cancelar",
            bool destructivo = false) {

            var dialog = CrearDialogo(ctx);
            var root = ConstruirContenedor(ctx);

            root.AddView(CrearTitulo(ctx, titulo));
            root.AddView(CrearMensaje(ctx, mensaje));
            root.AddView(CrearDivisorBotones(ctx));

            var fila = CrearFilaBotones(ctx);
            fila.AddView(CrearBotonSecundario(ctx, textoCancelar, () => dialog.Dismiss()));
            fila.AddView(CrearSeparadorBotones(ctx));
            fila.AddView(destructivo
                ? CrearBotonDestructivo(ctx, textoConfirmar, () => { dialog.Dismiss(); onConfirmar(); })
                : CrearBotonPrimario(ctx, textoConfirmar, () => { dialog.Dismiss(); onConfirmar(); }));
            root.AddView(fila);

            MostrarDialogo(ctx, dialog, root);
        }

        // ── Opciones — título + mensaje + N botones verticales ─

        public static void MostrarOpciones(
            Activity ctx,
            string titulo,
            string mensaje,
            params (string Texto, bool EsPrimario, Action Accion)[] opciones) {

            var dialog = CrearDialogo(ctx);
            var root = ConstruirContenedor(ctx);

            root.AddView(CrearTitulo(ctx, titulo));
            if (!string.IsNullOrEmpty(mensaje))
                root.AddView(CrearMensaje(ctx, mensaje));
            root.AddView(CrearDivisorBotones(ctx));

            // Botones apilados verticalmente
            var contenedor = new LinearLayout(ctx) {
                Orientation = Orientation.Vertical
            };
            contenedor.SetPadding(Dp(ctx, 20), Dp(ctx, 12), Dp(ctx, 20), Dp(ctx, 4));

            foreach (var (texto, esPrimario, accion) in opciones) {
                var btn = CrearBotonVertical(ctx, texto, esPrimario);
                var accionCapture = accion;
                btn.Click += (s, e) => { dialog.Dismiss(); accionCapture(); };
                contenedor.AddView(btn);

                // Margen entre botones
                var separador = new View(ctx);
                separador.LayoutParameters = new LinearLayout.LayoutParams(
                    LinearLayout.LayoutParams.MatchParent, Dp(ctx, 6));
                contenedor.AddView(separador);
            }

            root.AddView(contenedor);
            MostrarDialogo(ctx, dialog, root);
        }

        // ── Lista — título + lista scrolleable de opciones ────

        public static void MostrarLista(
            Activity ctx,
            string titulo,
            string[] items,
            Action<int> onElegir,
            string textoCancelar = "Cancelar") {

            var dialog = CrearDialogo(ctx);
            var root = ConstruirContenedor(ctx);

            root.AddView(CrearTitulo(ctx, titulo));
            root.AddView(CrearDivisorBotones(ctx));

            // Lista de opciones
            var scrollView = new MaxHeightScrollView(ctx, Dp(ctx, 280));
            scrollView.LayoutParameters = new LinearLayout.LayoutParams(
                 ViewGroup.LayoutParams.MatchParent,
                 ViewGroup.LayoutParams.WrapContent);

            // Nota: MaxHeight en ScrollView no está disponible directamente.
            // Usamos un LinearLayout con límite manual.
            var listaContenedor = new LinearLayout(ctx) {
                Orientation = Orientation.Vertical
            };

            for (int i = 0; i < items.Length; i++) {
                int idx = i;
                var fila = new TextView(ctx) {
                    Text = items[i],
                    TextSize = 15f
                };
                fila.SetTextColor(Color.ParseColor("#222222"));
                fila.SetPadding(Dp(ctx, 24), Dp(ctx, 14), Dp(ctx, 24), Dp(ctx, 14));
                fila.SetBackgroundResource(Android.Resource.Drawable.ListSelectorBackground);
                fila.Clickable = true;
                fila.Click += (s, e) => { dialog.Dismiss(); onElegir(idx); };
                listaContenedor.AddView(fila);

                if (i < items.Length - 1) {
                    var div = new View(ctx);
                    div.LayoutParameters = new LinearLayout.LayoutParams(
                        LinearLayout.LayoutParams.MatchParent, 1);
                    div.SetBackgroundColor(Color.ParseColor("#F0F0F0"));
                    listaContenedor.AddView(div);
                }
            }

            scrollView.AddView(listaContenedor);
            root.AddView(scrollView);
            root.AddView(CrearDivisorBotones(ctx));

            var fila2 = CrearFilaBotones(ctx);
            fila2.AddView(CrearBotonSecundario(ctx, textoCancelar, () => dialog.Dismiss()));
            root.AddView(fila2);

            MostrarDialogo(ctx, dialog, root);
        }

        // ── Construcción del diálogo ──────────────────────────

        private static Dialog CrearDialogo(Activity ctx) {
            var dialog = new Dialog(ctx);
            dialog.RequestWindowFeature((int) WindowFeatures.NoTitle);
            return dialog;
        }

        private static void MostrarDialogo(Activity ctx, Dialog dialog, LinearLayout root) {
            dialog.SetContentView(root);

            var window = dialog.Window!;
            window.SetBackgroundDrawableResource(Resource.Drawable.dialog_background);

            // Dim del fondo
            var lp = window.Attributes!;
            lp.DimAmount = 0.55f;
            window.Attributes = lp;
            window.AddFlags(Android.Views.WindowManagerFlags.DimBehind);

            // Ancho: 88% de pantalla, altura: wrap
            int screenWidth = ctx.Resources!.DisplayMetrics!.WidthPixels;
            window.SetLayout(
                (int) (screenWidth * 0.88f),
                Android.Views.ViewGroup.LayoutParams.WrapContent);

            dialog.Show();
        }

        // ── Componentes visuales ──────────────────────────────

        private static LinearLayout ConstruirContenedor(Activity ctx) {
            var root = new LinearLayout(ctx) {
                Orientation = Orientation.Vertical
            };
            root.SetBackgroundResource(Resource.Drawable.dialog_background);
            return root;
        }

        private static TextView CrearTitulo(Activity ctx, string texto) {
            var tv = new TextView(ctx) {
                Text = texto,
                TextSize = 16f
            };
            tv.SetTextColor(Color.ParseColor("#1A1A1A"));
            tv.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
            tv.SetPadding(Dp(ctx, 22), Dp(ctx, 20), Dp(ctx, 22), Dp(ctx, 8));
            return tv;
        }

        private static TextView CrearMensaje(Activity ctx, string texto) {
            var tv = new TextView(ctx) {
                Text = texto,
                TextSize = 14f
            };
            tv.SetTextColor(Color.ParseColor("#555555"));
            tv.SetLineSpacing(2f, 1.3f);
            tv.SetPadding(Dp(ctx, 22), Dp(ctx, 4), Dp(ctx, 22), Dp(ctx, 16));
            return tv;
        }

        private static View CrearDivisorBotones(Activity ctx) {
            var v = new View(ctx);
            v.LayoutParameters = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MatchParent, 1);
            v.SetBackgroundColor(Color.ParseColor("#EEEEEE"));
            return v;
        }

        private static LinearLayout CrearFilaBotones(Activity ctx) {
            var fila = new LinearLayout(ctx) {
                Orientation = Orientation.Horizontal
            };
            fila.SetPadding(Dp(ctx, 16), Dp(ctx, 12), Dp(ctx, 16), Dp(ctx, 14));
            fila.SetGravity(Android.Views.GravityFlags.End);
            return fila;
        }

        private static View CrearSeparadorBotones(Activity ctx) {
            var v = new View(ctx);
            v.LayoutParameters = new LinearLayout.LayoutParams(Dp(ctx, 8), 1);
            return v;
        }

        private static Button CrearBotonPrimario(
            Activity ctx, string texto, Action onClick) {
            var btn = new Button(ctx) {
                Text = texto,
                TextSize = 13.5f
            };
            btn.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
            btn.SetTextColor(Color.White);
            btn.SetBackgroundResource(Resource.Drawable.dialog_btn_primary);
            btn.SetPadding(Dp(ctx, 20), Dp(ctx, 8), Dp(ctx, 20), Dp(ctx, 8));
            btn.LayoutParameters = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.WrapContent,
                Dp(ctx, 40));
            btn.Click += (s, e) => onClick();
            return btn;
        }

        private static Button CrearBotonSecundario(
            Activity ctx, string texto, Action onClick) {
            var btn = new Button(ctx) {
                Text = texto,
                TextSize = 13.5f
            };
            btn.SetTextColor(Color.ParseColor("#555555"));
            btn.SetBackgroundResource(Resource.Drawable.dialog_btn_outline);
            btn.SetPadding(Dp(ctx, 20), Dp(ctx, 8), Dp(ctx, 20), Dp(ctx, 8));
            btn.LayoutParameters = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.WrapContent,
                Dp(ctx, 40));
            btn.Click += (s, e) => onClick();
            return btn;
        }

        private static Button CrearBotonDestructivo(
            Activity ctx, string texto, Action onClick) {
            var btn = new Button(ctx) {
                Text = texto,
                TextSize = 13.5f
            };
            btn.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
            btn.SetTextColor(Color.ParseColor("#B22222"));
            btn.SetBackgroundResource(Resource.Drawable.dialog_btn_outline);
            btn.SetPadding(Dp(ctx, 20), Dp(ctx, 8), Dp(ctx, 20), Dp(ctx, 8));
            btn.LayoutParameters = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.WrapContent,
                Dp(ctx, 40));
            btn.Click += (s, e) => onClick();
            return btn;
        }

        private static Button CrearBotonVertical(
            Activity ctx, string texto, bool primario) {
            var btn = new Button(ctx) {
                Text = texto,
                TextSize = 14f
            };
            btn.SetTypeface(null, primario
                ? Android.Graphics.TypefaceStyle.Bold
                : Android.Graphics.TypefaceStyle.Normal);
            btn.SetTextColor(primario ? Color.White : Color.ParseColor("#333333"));
            btn.SetBackgroundResource(primario
                ? Resource.Drawable.dialog_btn_primary
                : Resource.Drawable.dialog_btn_outline);
            btn.LayoutParameters = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MatchParent,
                Dp(ctx, 46));
            return btn;
        }

        private static int Dp(Activity ctx, int dp) =>
            (int) (dp * ctx.Resources!.DisplayMetrics!.Density);
    }
}
