using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaGestionMovimientos : Form, IVistaGestionMovimientos {
        private int _paginaActual = 1;
        private int _paginasTotales = 1;
        private decimal _totalEntradas;
        private decimal _totalSalidas;
        private decimal _balance;

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
            get => new[] { fieldCriterioBusqueda.Text };
            set => fieldCriterioBusqueda.Text = value.Length > 0 ? value[0] : string.Empty;
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

        public decimal TotalEntradas {
            get => _totalEntradas;
            set {
                _totalEntradas = value;

                fieldTotalEntradas.Text = $"$ {value.ToString("N2", CultureInfo.InvariantCulture)}";
            }
        }

        public decimal TotalSalidas {
            get => _totalSalidas;
            set {
                _totalSalidas = value;

                fieldTotalSalidas.Text = $"$ {value.ToString("N2", CultureInfo.InvariantCulture)}";
            }
        }

        public decimal Balance {
            get => _balance;
            set {
                _balance = value;

                fieldValorBalance.Text = $"$ {value.ToString("N2", CultureInfo.InvariantCulture)}";
            }
        }

        public event EventHandler? AlturaContenedorTuplasModificada;
        public event EventHandler? MostrarPrimeraPagina;
        public event EventHandler? MostrarPaginaAnterior;
        public event EventHandler? MostrarPaginaSiguiente;
        public event EventHandler? MostrarUltimaPagina;
        public event EventHandler? SincronizarDatos;

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;
        public event EventHandler<(FiltroBusquedaMovimiento, string[])>? BuscarEntidades;

        public void Inicializar() {
            // Eventos
            fieldFiltroBusquedaFechaDesde.Value = DateTime.Today;
            fieldFiltroBusquedaFechaDesde.ValueChanged += OnCambioValorFechaDesde;
            fieldFiltroBusquedaFechaHasta.Value = DateTime.Today;
            fieldFiltroBusquedaFechaHasta.ValueChanged += OnCambioValorFechaHasta;
            fieldFiltroBusqueda.SelectedIndexChanged += OnCambioIndiceFiltroBusqueda;
            fieldCriterioBusqueda.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode != Keys.Enter)
                    return;

                if (CriteriosBusqueda.Length > 0 && !string.IsNullOrEmpty(CriteriosBusqueda[0]))
                    BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { 
                        fieldFiltroBusquedaFechaDesde.Value.ToString("yyyy-MM-dd"), 
                        fieldFiltroBusquedaFechaHasta.Value.ToString("yyyy-MM-dd"), 
                        CriteriosBusqueda[0] 
                    }));
                else SincronizarDatos?.Invoke(sender, args);

                args.SuppressKeyPress = true;
            };
            btnRegistrar.Click += delegate (object? sender, EventArgs e) { RegistrarEntidad?.Invoke(sender, e); };
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

        private void OnCambioValorFechaDesde(object? sender, EventArgs e) {
            var valorFechaDesde = fieldFiltroBusquedaFechaDesde.Value.Date;
            var valorFechaHasta = fieldFiltroBusquedaFechaHasta.Value.Date;

            if (valorFechaDesde <= valorFechaHasta)
                BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { fieldFiltroBusquedaFechaDesde.Value.ToString("yyyy-MM-dd"), fieldFiltroBusquedaFechaHasta.Value.ToString("yyyy-MM-dd"), CriteriosBusqueda[0] }));
            else {
                fieldFiltroBusquedaFechaDesde.Value = valorFechaHasta;

                CentroNotificaciones.MostrarNotificacion("La fecha de inicio no puede ser mayor que la fecha final o fecha del día de hoy, por favor, corrija los datos de entrada", Core.Modelos.Comun.TipoNotificacionEnum.Advertencia);
            }
        }

        private void OnCambioValorFechaHasta(object? sender, EventArgs e) {
            var valorFechaDesde = fieldFiltroBusquedaFechaDesde.Value.Date;
            var valorFechaHasta = fieldFiltroBusquedaFechaHasta.Value.Date;

            if (valorFechaHasta >= valorFechaDesde && valorFechaHasta <= DateTime.Now)
                BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { fieldFiltroBusquedaFechaDesde.Value.ToString("yyyy-MM-dd"), fieldFiltroBusquedaFechaHasta.Value.ToString("yyyy-MM-dd"), CriteriosBusqueda[0] }));
            else {
                fieldFiltroBusquedaFechaHasta.Value = DateTime.Now;

                CentroNotificaciones.MostrarNotificacion("La fecha final no puede ser menor que la fecha inicial o mayor que la fecha del día de hoy, por favor, corrija los datos de entrada", Core.Modelos.Comun.TipoNotificacionEnum.Advertencia);
            }
        }

        private void OnCambioIndiceFiltroBusqueda(object? sender, EventArgs e) {
            fieldCriterioBusqueda.Text = string.Empty;
            fieldCriterioBusqueda.Visible = fieldFiltroBusqueda.SelectedIndex != 0;

            if (fieldCriterioBusqueda.Visible)
                fieldCriterioBusqueda.Focus();

            BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { fieldFiltroBusquedaFechaDesde.Value.ToString("yyyy-MM-dd"), fieldFiltroBusquedaFechaHasta.Value.ToString("yyyy-MM-dd"), string.Empty }));

            // Ir a la primera página al cambiar el criterio de búsqueda
            PaginaActual = 1;
            HabilitarBotonesPaginacion();
        }

        public void CargarFiltrosBusqueda((string Nombre, string Descripcion)[] filtrosBusqueda) {
            // Evitar que se dispare el evento SelectedIndexChanged al modificar los ítems
            fieldFiltroBusqueda.SelectedIndexChanged -= OnCambioIndiceFiltroBusqueda;

            fieldFiltroBusqueda.Items.Clear();
            fieldFiltroBusqueda.Items.AddRange([.. filtrosBusqueda.Select(f => f.Nombre)]);

            if (fieldFiltroBusqueda.Items.Count > 0) {
                fieldFiltroBusqueda.SelectedIndex = 0;
                fieldCriterioBusqueda.Visible = false;
            }

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