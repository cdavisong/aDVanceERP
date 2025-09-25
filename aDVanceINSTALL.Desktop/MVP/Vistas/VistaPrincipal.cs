
using System.Diagnostics;

namespace aDVanceINSTALL.Desktop.MVP.Vistas {
    public partial class VistaPrincipal : Form {
        private List<Form> _paginas = new List<Form>();
        private int _paginaActual = 0;
        
        private VistaBienvenida _bienvenida;
        private VistaDirectorioInstalacion _directorioInstalacion;
        private VistaProgresoInstalacion _progresoInstalacion;
        private VistaInstalacionCompletada _instalacionCompletada;

        public VistaPrincipal() {
            InitializeComponent();

            // Inicializar vistas
            _bienvenida = new VistaBienvenida() { Name = "vistaBienvenida" };
            _directorioInstalacion = new VistaDirectorioInstalacion() { Name = "vistaDirectorioInstalacion" };
            _progresoInstalacion = new VistaProgresoInstalacion() { Name = "vistaProgresoInstalacion" };
            _instalacionCompletada = new VistaInstalacionCompletada() { Name = "vistaInstalacionCompletada" };

            // Adicionar páginas
            _paginas.Add(_bienvenida);
            _paginas.Add(_directorioInstalacion);
            _paginas.Add(_progresoInstalacion);
            _paginas.Add(_instalacionCompletada);

            // Adicionar vistas al contenedor
            AdicionarVistaContenedor(_bienvenida);
            AdicionarVistaContenedor(_directorioInstalacion);
            AdicionarVistaContenedor(_progresoInstalacion);
            AdicionarVistaContenedor(_instalacionCompletada);

            // Mostrar la primera vista
            _paginas.ElementAt(_paginaActual).Show();

            // Eventos
            _directorioInstalacion.VisibleChanged += delegate {
                if (!_directorioInstalacion.Visible)
                    _progresoInstalacion.DirectorioInstalacion = _directorioInstalacion.DirectorioInstalacion;
            };
            _progresoInstalacion.VisibleChanged += delegate {
                if (!_progresoInstalacion.Visible) {
                    btnSalir.Enabled = true;

                    _instalacionCompletada.Show();
                }
            };
            btnSiguiente.Click += delegate {
                _paginas.ElementAt(_paginaActual).Hide();
                _paginaActual++;
                _paginas.ElementAt(_paginaActual).Show();

                if (_paginaActual == 2) {
                    btnSiguiente.Hide();
                    btnSalir.Enabled = false;

                    _progresoInstalacion.InstalarAplicacion();
                }
            };
            btnSalir.Click += delegate {
                if (_instalacionCompletada.EjecutarAplicacionAlSalir)
                    EjecutarAplicacion();

                Dispose(); 
            };
        }

        private void AdicionarVistaContenedor(Form vista) {
            vista.Dock = DockStyle.Fill;
            vista.TopLevel = false;            

            contenedorVistas.Controls.Add(vista);

            vista.SendToBack();
        }

        private void EjecutarAplicacion() {
            try {
                var appProcess = new Process {
                    StartInfo = new ProcessStartInfo {
                        FileName = $"{_directorioInstalacion.DirectorioInstalacion}aDVanceERP.Desktop.exe",
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WorkingDirectory = _directorioInstalacion.DirectorioInstalacion
                    }
                };
                                
                appProcess.Start();
            } catch (Exception ex) {
                MessageBox.Show("Se produjo un error al intentar ejecutar la aplicación : " + ex.Message, "Error al ejecutar la aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
