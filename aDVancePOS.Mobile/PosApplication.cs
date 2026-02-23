// ============================================================
//  aDVancePOS.Mobile — PosApplication
//  Archivo: PosApplication.cs
//
//  Subclase de Application que mantiene los servicios como
//  singletons accesibles desde cualquier Activity sin pasar
//  datos pesados por Intent extras.
//
//  Uso desde cualquier Activity:
//    var app = (PosApplication)Application!;
//    var carrito = app.CarritoService;
// ============================================================

using aDVancePOS.Mobile.Servicios;

using Android.App;
using Android.Runtime;

namespace aDVancePOS.Mobile {

    [Application]
    public class PosApplication : Application {

        public CarritoService  CarritoService  { get; private set; } = null!;
        public VentaService    VentaService    { get; private set; } = null!;
        public CatalogoService CatalogoService { get; private set; } = null!;
        public ConfiguracionApp Config         { get; private set; } = null!;

        public PosApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer) { }

        public override void OnCreate() {
            base.OnCreate();
            InicializarServicios();
        }

        private void InicializarServicios() {
            Config         = ConfiguracionService.Cargar();
            CatalogoService = new CatalogoService();
            CarritoService  = new CarritoService();
            VentaService    = new VentaService(Config);
        }

        /// <summary>
        /// Llamar desde ConfiguracionActivity después de guardar.
        /// Actualiza la config y recrea el VentaService con los nuevos valores.
        /// </summary>
        public void RefrescarConfig(ConfiguracionApp nuevaConfig) {
            Config       = nuevaConfig;
            VentaService = new VentaService(nuevaConfig);
        }
    }
}
