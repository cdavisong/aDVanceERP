using Android;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;

namespace aDVancePOS.Mobile.Controladores;

public static class ControladorPermisosApp {
    public static bool HasStoragePermissions(Context context) {
        return ContextCompat.CheckSelfPermission(context, Manifest.Permission.ReadExternalStorage) == Permission.Granted &&
               ContextCompat.CheckSelfPermission(context, Manifest.Permission.WriteExternalStorage) == Permission.Granted;
    }

    public static void RequestStoragePermissions(Activity activity, int requestCode) {
        ActivityCompat.RequestPermissions(activity,
            new[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage },
            requestCode);
    }
}