using aDVanceERP.Core.Eventos;
using GMap.NET.MapProviders;
using GMap.NET;
using System.Net;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Presentadores.Comun {
    public abstract class PresentadorVistaGestionUbicaciones : PresentadorVistaBase<IVistaGestionUbicaciones> {
        public PresentadorVistaGestionUbicaciones(IVistaGestionUbicaciones vista, string nombreEvento) : base(vista) {
            vista.RegistrarEntidad += OnRegistrarUbicacion;
            vista.EditarEntidad += OnEditarUbicacion;

            InicializarComponenteMapa();

            AgregadorEventos.Suscribir(nombreEvento, OnMostrarVistaGestionUbicaciones);
        }

        public abstract void OnRegistrarUbicacion(object? sender, EventArgs e);

        public abstract void OnEditarUbicacion(object? sender, EventArgs e);

        private void InicializarComponenteMapa() {
            // Utilizar el proxy de sistema
            WebRequest.DefaultWebProxy = WebRequest.GetSystemWebProxy();
            WebRequest.DefaultWebProxy.Credentials = CredentialCache.DefaultCredentials;

            // Inicializar las propiedades del mapa
            GMaps.Instance.Mode = AccessMode.ServerAndCache;

            if (!Directory.Exists(@".\mapcache"))
                Directory.CreateDirectory(@".\mapcache");

            Vista.Mapa.MapProvider = GMapProviders.OpenStreetMap;
            Vista.Mapa.CacheLocation = @".\mapcache";
            Vista.Mapa.Position = new PointLatLng(23.13538, -82.35899); // Km 0 de la Carretera Central (Habana, Cuba)
            Vista.Mapa.MinZoom = 1;
            Vista.Mapa.MaxZoom = 20;
            Vista.Mapa.Zoom = 14;

            // Eventos
            Vista.Mapa.MouseMove += MostrarCoordenadasCursor;
        }

        private void MostrarCoordenadasCursor(object? sender, MouseEventArgs e) {
            var latLong = Vista.Mapa.FromLocalToLatLng(e.X, e.Y);
            var ubicacion = new CoordenadasGeograficas(latLong.Lat, latLong.Lng);

            Vista.Ubicacion = ubicacion;
        }

        public abstract void OnMostrarVistaGestionUbicaciones(string obj);

        public override void Dispose() {
            Vista.Mapa.Dispose();
        }
    }
}