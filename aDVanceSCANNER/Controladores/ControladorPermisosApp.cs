using Android;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;

namespace aDVanceSCANNER.Controladores;

public class ControladorPermisosApp {
    private const int REQUEST_CAMERA = 1;
    private const int REQUEST_INTERNET = 2;
    private readonly Activity _activity;

    public ControladorPermisosApp(Activity activity) {
        _activity = activity;
    }

    public void ComprobarPermisos() {
        if (ContextCompat.CheckSelfPermission(_activity, Manifest.Permission.Camera) != Permission.Granted) {
            ActivityCompat.RequestPermissions(_activity,
                new[] { Manifest.Permission.Camera },
                REQUEST_CAMERA);
        }

        if (ContextCompat.CheckSelfPermission(_activity, Manifest.Permission.Internet) != Permission.Granted) {
            ActivityCompat.RequestPermissions(_activity,
                new[] { Manifest.Permission.Internet },
                REQUEST_INTERNET);
        }
    }

    public void ControlResultadoEncuestaPermisos(int requestCode, string[] permissions, Permission[] grantResults) {
        if (requestCode != REQUEST_CAMERA) return;

        if (grantResults.Length > 0 && grantResults[0] == Permission.Granted) {
            Toast.MakeText(_activity, "Permiso de cámara concedido", ToastLength.Short)?.Show();
        } else {
            Toast.MakeText(_activity, "Se necesita permiso de cámara para escanear", ToastLength.Long)?.Show();
        }

        Toast.MakeText(_activity, "Permiso INTERNET concedido: " +
            (ContextCompat.CheckSelfPermission(_activity, Manifest.Permission.Internet) == Permission.Granted),
            ToastLength.Long)?.Show();
    }
}