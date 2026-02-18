using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaGestionMovimientos : Form, IVistaGestionMovimientos {
        private int _paginaActual = 1;
        private int _paginasTotales = 1;

        public VistaGestionMovimientos() {
            InitializeComponent();

            NombreVista = nameof(VistaGestionMovimientos);
            PanelCentral = new RepoVistaBase(contenedorVistas);

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

        public FiltroBusquedaMovimiento FiltroBusqueda {
            get => fieldFiltroBusqueda.SelectedIndex >= 0
                ? (FiltroBusquedaMovimiento) fieldFiltroBusqueda.SelectedIndex
                : default;
            set => fieldFiltroBusqueda.SelectedIndex = (int) value;
        }

        public string[] CriteriosBusqueda {
            get => new[] { fieldDatoBusqueda.Text };
            set => fieldDatoBusqueda.Text = value.Length > 0 ? value[0] : string.Empty;
        }

        public int TuplasMaximasContenedor {
            get => contenedorVistas.Height / ContextoAplicacion.AlturaTuplaPredeterminada;
        }

        public int PaginaActual {
            get => _paginaActual;
            set {
                _paginaActual = value;
                fieldPaginaActual.Text = $@"Página {value}";
            }
        }

        public int PaginasTotales {
            get => _paginasTotales;
            set {
                _paginasTotales = value;
                fieldPaginasTotales.Text = $@"de {value}";
                HabilitarBotonesPaginacion();
            }
        }

        public RepoVistaBase? PanelCentral { get; private set; }

        public event EventHandler? AlturaContenedorTuplasModificada;
        public event EventHandler? MostrarPrimeraPagina;
        public event EventHandler? MostrarPaginaAnterior;
        public event EventHandler? MostrarPaginaSiguiente;
        public event EventHandler? MostrarUltimaPagina;
        public event EventHandler? SincronizarDatos;

        public event EventHandler<(DateTime fechaDesde, DateTime fechaHasta, FormatoDocumento formato)>? AuditarInventario;
        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;
        public event EventHandler<(FiltroBusquedaMovimiento, string[])>? BuscarEntidades;

        public void Inicializar() {
            // Eventos
            fieldFiltroBusqueda.SelectedIndexChanged += OnCambioIndiceFiltroBusqueda;
            fieldDatoBusqueda.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode != Keys.Enter)
                    return;

                if (CriteriosBusqueda.Length > 0 && !string.IsNullOrEmpty(CriteriosBusqueda[0]))
                    BuscarEntidades?.Invoke(this, (FiltroBusqueda, CriteriosBusqueda));
                else SincronizarDatos?.Invoke(sender, args);

                args.SuppressKeyPress = true;
            };
            fieldDatoBusquedaFecha.ValueChanged += delegate (object? sender, EventArgs e) {
                BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { fieldDatoBusquedaFecha.Value.ToString("yyyy-MM-dd") }));
            };
            btnRegistrar.Click += delegate (object? sender, EventArgs e) { RegistrarEntidad?.Invoke(sender, e); };
            btnAuditarInventarioPdf.Click += delegate (object? sender, EventArgs e) { AuditarInventario?.Invoke(sender, (fieldFechaDesde.Value, fieldFechaHasta.Value, FormatoDocumento.PDF)); };
            btnAuditarInventarioXls.Click += delegate (object? sender, EventArgs e) { AuditarInventario?.Invoke(sender, (fieldFechaDesde.Value, fieldFechaHasta.Value, FormatoDocumento.Excel)); };
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
            btnSincronizarDatos.Click += delegate (object? sender, EventArgs e) { SincronizarDatos?.Invoke(sender, e); };
            contenedorVistas.Resize += delegate { AlturaContenedorTuplasModificada?.Invoke(this, EventArgs.Empty); };
        }

        private void OnCambioIndiceFiltroBusqueda(object? sender, EventArgs e) {
            if (FiltroBusqueda == FiltroBusquedaMovimiento.Fecha) {
                fieldDatoBusquedaFecha.Value = DateTime.Now;
                fieldDatoBusquedaFecha.Focus();
            } else {
                fieldDatoBusqueda.Text = string.Empty;
                fieldDatoBusqueda.Focus();
            }

            fieldDatoBusqueda.Visible = FiltroBusqueda != FiltroBusquedaMovimiento.Fecha && fieldFiltroBusqueda.SelectedIndex != 0;
            fieldDatoBusquedaFecha.Visible = FiltroBusqueda == FiltroBusquedaMovimiento.Fecha && fieldFiltroBusqueda.SelectedIndex != 0;

            if (FiltroBusqueda != FiltroBusquedaMovimiento.Fecha)
                BuscarEntidades?.Invoke(this, (FiltroBusqueda, Array.Empty<string>()));

            // Ir a la primera página al cambiar el criterio de búsqueda
            PaginaActual = 1;
            HabilitarBotonesPaginacion();
        }

        public void CargarFiltrosBusqueda(object[] criteriosBusqueda) {
            // Evitar que se dispare el evento SelectedIndexChanged al modificar los ítems
            fieldFiltroBusqueda.SelectedIndexChanged -= OnCambioIndiceFiltroBusqueda;

            fieldFiltroBusqueda.Items.Clear();
            fieldFiltroBusqueda.Items.AddRange(criteriosBusqueda);

            if (fieldFiltroBusqueda.Items.Count > 0)
                fieldFiltroBusqueda.SelectedIndex = 0;

            // Reasignar el evento SelectedIndexChanged
            fieldFiltroBusqueda.SelectedIndexChanged += OnCambioIndiceFiltroBusqueda;
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
            PaginaActual = 1;
            PaginasTotales = 1;

            fieldFechaDesde.Value = DateTime.Now;
            fieldFechaHasta.Value = DateTime.Now;
            fieldFiltroBusqueda.SelectedIndex = 0;
        }

        public void Ocultar() {
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