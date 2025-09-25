using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Utiles;

using aDVanceERP.Modulos.Finanzas.MVP.Modelos;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja.Plantillas;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja
{
    public partial class VistaGestionCajas : Form, IVistaGestionCajas {
        private int _paginaActual = 1;
        private int _paginasTotales = 1;

        public VistaGestionCajas() {
            InitializeComponent();

            NombreVista = nameof(VistaGestionCajas);

            Inicializar();
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

        public FiltroBusquedaCaja FiltroBusqueda {
            get => fieldFiltroBusqueda.SelectedIndex >= 0 ? (FiltroBusquedaCaja) fieldFiltroBusqueda.SelectedIndex : default;
            set => fieldFiltroBusqueda.SelectedIndex = (int) value;
        }

        public string CriterioBusqueda {
            get => fieldDatoBusqueda.Text;
            set => fieldDatoBusqueda.Text = value;
        }

        public int AlturaContenedorVistas {
            get => contenedorVistas.Height;
        }

        public int TuplasMaximasContenedor {
            get => AlturaContenedorVistas / VariablesGlobales.AlturaTuplaPredeterminada;
        }

        public int PaginaActual {
            get => _paginaActual;
            set {
                _paginaActual = value;
                fieldPaginaActual.Text = $"Página {value}";
            }
        }

        public int PaginasTotales {
            get => _paginasTotales;
            set {
                _paginasTotales = value;
                fieldPaginasTotales.Text = $"de {value}";
                HabilitarBotonesPaginacion();
            }
        }

        public bool HabilitarBtnRegistroMovimientoCaja {
            get => btnRegistrarMovimientoCaja.Visible;
            set => btnRegistrarMovimientoCaja.Visible = value;
        }

        public bool HabilitarBtnCierreCaja {
            get => btnCierreCaja.Visible;
            set => btnCierreCaja.Visible = value;
        }

        public RepoVistaBase? PanelCentral { get; private set; }

        public event EventHandler? AlturaContenedorTuplasModificada;
        public event EventHandler? MostrarPrimeraPagina;
        public event EventHandler? MostrarPaginaAnterior;
        public event EventHandler? MostrarPaginaSiguiente;
        public event EventHandler? MostrarUltimaPagina;
        public event EventHandler? SincronizarDatos;
        
        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;
        public event EventHandler<(FiltroBusquedaCaja, string?)>? BuscarEntidades;
        public event EventHandler? RegistrarMovimientoCajaSeleccionada;
        public event EventHandler? CerrarCajaSeleccionada;

        public void Inicializar() {
            // Variables locales
            PanelCentral = new RepoVistaBase(contenedorVistas);

            // Eventos
            fieldFiltroBusqueda.SelectedIndexChanged += delegate {
                if (FiltroBusqueda == FiltroBusquedaCaja.FechaApertura || FiltroBusqueda == FiltroBusquedaCaja.FechaCierre) {
                    fieldDatoBusquedaFecha.Value = DateTime.Now;
                    fieldDatoBusquedaFecha.Focus();
                }
                else {
                    fieldDatoBusqueda.Text = string.Empty;
                    fieldDatoBusqueda.Focus();
                }

                fieldDatoBusqueda.Visible = FiltroBusqueda != FiltroBusquedaCaja.FechaApertura &&
                                            FiltroBusqueda != FiltroBusquedaCaja.FechaCierre &&
                                            fieldFiltroBusqueda.SelectedIndex != 0;
                fieldDatoBusquedaFecha.Visible = (FiltroBusqueda == FiltroBusquedaCaja.FechaApertura ||
                                                 FiltroBusqueda == FiltroBusquedaCaja.FechaCierre) &&
                                                 fieldFiltroBusqueda.SelectedIndex != 0;

                if (FiltroBusqueda != FiltroBusquedaCaja.FechaApertura &&
                    FiltroBusqueda != FiltroBusquedaCaja.FechaCierre)
                    BuscarEntidades?.Invoke(this, (FiltroBusqueda, string.Empty));

                // Ir a la primera página al cambiar el criterio de búsqueda
                PaginaActual = 1;
                HabilitarBotonesPaginacion();
            };
            fieldDatoBusqueda.KeyDown += delegate(object? sender, KeyEventArgs args) {
                if (args.KeyCode != Keys.Enter)
                    return;

                if (!string.IsNullOrEmpty(CriterioBusqueda))
                    BuscarEntidades?.Invoke(this, (FiltroBusqueda, CriterioBusqueda));
                else SincronizarDatos?.Invoke(sender, args);

                args.SuppressKeyPress = true;
            };
            fieldDatoBusquedaFecha.ValueChanged += delegate (object? sender, EventArgs e) {
                BuscarEntidades?.Invoke(this, (FiltroBusqueda, fieldDatoBusquedaFecha.Value.ToString("yyyy-MM-dd")));
            };
            btnRegistrar.Click += delegate (object? sender, EventArgs e) {
                RegistrarEntidad?.Invoke(sender, e);
            };
            btnRegistrarMovimientoCaja.Click += delegate (object? sender, EventArgs e) {
                RegistrarMovimientoCajaSeleccionada?.Invoke(sender, e);
            };
            btnCierreCaja.Click += delegate (object? sender, EventArgs e) {
                CerrarCajaSeleccionada?.Invoke(sender, e);
            };
            btnPrimeraPagina.Click += delegate (object? sender, EventArgs e) {
                PaginaActual = 1;
                MostrarPrimeraPagina?.Invoke(sender, e);
                SincronizarDatos?.Invoke(sender, e);
                HabilitarBotonesPaginacion();
            };
            btnPaginaAnterior.Click += delegate (object? sender, EventArgs e) {
                PaginaActual--;
                MostrarPaginaAnterior?.Invoke(sender, e);
                SincronizarDatos?.Invoke(sender, e);
                HabilitarBotonesPaginacion();
            };
            btnPaginaSiguiente.Click += delegate (object? sender, EventArgs e) {
                PaginaActual++;
                MostrarPaginaSiguiente?.Invoke(sender, e);
                SincronizarDatos?.Invoke(sender, e);
                HabilitarBotonesPaginacion();
            };
            btnUltimaPagina.Click += delegate (object? sender, EventArgs e) {
                PaginaActual = PaginasTotales;
                MostrarUltimaPagina?.Invoke(sender, e);
                SincronizarDatos?.Invoke(sender, e);
                HabilitarBotonesPaginacion();
            };
            btnSincronizarDatos.Click += delegate (object? sender, EventArgs e) {
                SincronizarDatos?.Invoke(sender, e);
            };
            contenedorVistas.Resize += delegate {
                AlturaContenedorTuplasModificada?.Invoke(this, EventArgs.Empty);
            };
        }

        public void CargarFiltrosBusqueda(object[] criteriosBusqueda) {
            fieldFiltroBusqueda.Items.Clear();
            fieldFiltroBusqueda.Items.AddRange(criteriosBusqueda);
            
            fieldFiltroBusqueda.SelectedIndex = 0;
        }

        public void Mostrar() {
            Habilitada = true;
            BringToFront();
            Show();
        }

        public void Restaurar() {
            Habilitada = true;
            PaginaActual = 1;
            PaginasTotales = 1;
            HabilitarBtnRegistroMovimientoCaja = false;
            HabilitarBtnCierreCaja = false;

            fieldFiltroBusqueda.SelectedIndex = 0;
        }

        public void Ocultar() {
            Habilitada = false;
            Hide();
        }

        public void Cerrar() {
            // ...
        }

        private void HabilitarBotonesPaginacion() {
            btnPrimeraPagina.Enabled = PaginaActual > 1;
            btnPaginaAnterior.Enabled = PaginaActual > 1;
            btnUltimaPagina.Enabled = PaginaActual < PaginasTotales;
            btnPaginaSiguiente.Enabled = PaginaActual < PaginasTotales;
        }
    }
}
