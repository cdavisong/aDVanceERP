using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Globalization;

namespace aDVanceERP.Core.Vistas.Comun {
    public partial class VistaGestionUbicaciones : Form, IVistaGestionUbicaciones {
        private CoordenadasGeograficas _ubicacion = null!;

        public VistaGestionUbicaciones() {
            InitializeComponent();

            NombreVista = nameof(VistaGestionUbicaciones);
            PanelCentral = new RepoVistaBase(contenedorVistas);

            Inicializar();
        }

        public Image Icono { 
            get => fieldIcono.BackgroundImage!; 
            set => fieldIcono.BackgroundImage = value; 
        }

        public string NombreVista {
            get => Name;
            private set => Name = value;
        }

        public bool Habilitada {
            get => Enabled;
            set => Enabled = value;
        }

        public Point Coordenadas {
            get => Location;
            set => Location = value;
        }

        public Size Dimensiones {
            get => Size;
            set => Size = value;
        }

        public int TuplasMaximasContenedor {
            get => contenedorVistas.Height / ContextoAplicacion.AlturaTuplaPredeterminada;
        }

        public RepoVistaBase PanelCentral { get; private set; }

        public GMapControl Mapa => fieldMapa;

        public CoordenadasGeograficas Ubicacion {
            get => _ubicacion;
            set { 
                _ubicacion = value;

                fieldLatitudLongitud.Text = $"Latitud: {value.Latitud.ToString("00.00000", CultureInfo.InvariantCulture)} Longitud: {value.Longitud.ToString("00.00000", CultureInfo.InvariantCulture)}"; 
            }
        }

        public event EventHandler? AlturaContenedorTuplasModificada;
        public event EventHandler? SincronizarDatos;

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;
        public event EventHandler<(FiltroBusquedaUbicacion, string[])>? BuscarEntidades;

        public void Inicializar() {
            // Variables locales
            PanelCentral = new RepoVistaBase(contenedorVistas);

            // Eventos            
            btnRegistrar.Click += delegate (object? sender, EventArgs e) { RegistrarEntidad?.Invoke(sender, e); };
            btnSincronizarDatos.Click += delegate (object? sender, EventArgs e) { SincronizarDatos?.Invoke(sender, e); };
            contenedorVistas.Resize += delegate { AlturaContenedorTuplasModificada?.Invoke(this, EventArgs.Empty); };
        }

        public void Mostrar() {
            
            BringToFront();
            Show();
        }

        public void Restaurar() {
            Mapa.Position = new PointLatLng(23.13538, -82.35899); // Km 0 de la Carretera Central (Habana, Cuba)
            Mapa.Zoom = 14;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            // ...
        }

        
    }
}