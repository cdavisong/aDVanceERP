using _Microsoft.Android.Resource.Designer;

using Android.Content;
using Android.Views;

namespace aDVanceSCANNER.Controladores;

public class ControladorUI {
    private readonly Context _context;
    private LinearLayout? _layoutDireccionPuerto;
    private EditText? _fieldDireccionIp;
    private EditText? _fieldPuerto;
    private TextView? _fieldEstadoConexion;
    private Button? _btnConectar;

    public ControladorUI(Context context, View rootView) {
        _context = context;

        InicializarVistas(rootView);
    }

    public string? DireccionIP {
        get => _fieldDireccionIp?.Text?.Trim();
        set {
            if (_fieldDireccionIp != null) _fieldDireccionIp.Text = value;
        }
    }

    public string? Puerto {
        get => _fieldPuerto?.Text?.Trim();
        set {
            if (_fieldPuerto != null) _fieldPuerto.Text = value;
        }
    }

    public EditText? FieldDireccionIp {
        get => _fieldDireccionIp;
    }

    public EditText? FieldPuerto {
        get => _fieldPuerto;
    }

    public Button? BtnConectar {
        get => _btnConectar;
    }

    private void InicializarVistas(View rootView) {
        _layoutDireccionPuerto = rootView.FindViewById<LinearLayout>(ResourceConstant.Id.layoutDireccionPuerto);
        _fieldDireccionIp = rootView.FindViewById<EditText>(ResourceConstant.Id.fieldDireccionIp);
        _fieldPuerto = rootView.FindViewById<EditText>(ResourceConstant.Id.fieldPuerto);
        _fieldEstadoConexion = rootView.FindViewById<TextView>(ResourceConstant.Id.connectionStatus);
        _btnConectar = rootView.FindViewById<Button>(ResourceConstant.Id.connectButton);
    }

    public void MostrarCamposConexion() {
        if (_layoutDireccionPuerto != null) _layoutDireccionPuerto.Visibility = ViewStates.Visible;
        if (_btnConectar != null) _btnConectar.Text = "Conectar";

        _layoutDireccionPuerto?.RequestLayout();
    }

    public void OcultarCamposConexion() {
        if (_layoutDireccionPuerto != null) _layoutDireccionPuerto.Visibility = ViewStates.Gone;
        if (_btnConectar != null) _btnConectar.Text = "Desconectar";

        _layoutDireccionPuerto?.RequestLayout();
    }

    public void ActualizarEstadoConexion(string estado) {
        if (_fieldEstadoConexion != null) _fieldEstadoConexion.Text = estado;
    }
}