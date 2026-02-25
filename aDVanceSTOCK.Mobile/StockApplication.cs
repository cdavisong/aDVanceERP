// ============================================================
//  aDVanceSTOCK.Mobile — StockApplication
//  Archivo: StockApplication.cs
//
//  Singleton de servicios. Accesible desde cualquier Activity:
//    var app = (StockApplication)Application!;
//    var sesion = app.SesionService;
// ============================================================

using aDVanceSTOCK.Mobile.Servicios;

using Android.App;
using Android.Runtime;

namespace aDVanceSTOCK.Mobile {

    [Application]
    public class StockApplication : Application {

        public CatalogoService    CatalogoService    { get; private set; } = null!;
        public SesionService      SesionService      { get; private set; } = null!;
        public ExportacionService ExportacionService { get; private set; } = null!;
        public ConfiguracionApp   Config             { get; private set; } = null!;

        public StockApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer) { }

        public override void OnCreate() {
            base.OnCreate();
            InicializarServicios();
        }

        private void InicializarServicios() {
            Config             = ConfiguracionService.Cargar();
            CatalogoService    = new CatalogoService();
            SesionService      = new SesionService();
            ExportacionService = new ExportacionService();
        }

        /// <summary>
        /// Llamar desde ConfiguracionActivity después de guardar el almacén.
        /// </summary>
        public void RefrescarConfig(ConfiguracionApp nuevaConfig) {
            Config = nuevaConfig;
        }
    }
}
