using Android.Content;
using Android.Views;

namespace aDVanceSCANNER.Controladores; 

public class ControladorHistorial {
    private readonly List<string> _historialCodigos = new List<string>();
    private readonly LinearLayout? _layoutHistorial;
    private readonly Context _context;

    public ControladorHistorial(Context context, LinearLayout? layoutHistorial) {
        _context = context;
        _layoutHistorial = layoutHistorial;
    }

    public void AdicionarResultado(string code, string format) {
        _historialCodigos.Insert(0, $"{DateTime.Now:HH:mm:ss} - {format}: {code}");

        if (_historialCodigos.Count > 50) {
            _historialCodigos.RemoveAt(_historialCodigos.Count - 1);
        }

        ActualizarInterfaz();
    }

    private void ActualizarInterfaz() {
        _layoutHistorial.RemoveAllViews();

        foreach (var textView in _historialCodigos.Select(item => new TextView(_context) {
                     Text = item,
                     TextSize = 12,
                     LayoutParameters = new LinearLayout.LayoutParams(
                         ViewGroup.LayoutParams.MatchParent,
                         ViewGroup.LayoutParams.WrapContent)
                 })) {
            ((LinearLayout.LayoutParams) textView.LayoutParameters!).BottomMargin = 8;

            _layoutHistorial.AddView(textView);
        }
    }
}