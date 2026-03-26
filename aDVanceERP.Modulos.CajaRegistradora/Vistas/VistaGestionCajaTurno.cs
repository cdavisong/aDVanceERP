using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Caja;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.CajaRegistradora.Interfaces;

namespace aDVanceERP.Modulos.CajaRegistradora.Vistas {
    public partial class VistaGestionCajaTurno : Form, IVistaGestionCaja {
        private int _paginaActual = 1;
        private int _paginasTotales = 1;

        public VistaGestionCajaTurno() {
            InitializeComponent();

            NombreVista = nameof(VistaGestionCajaTurno);
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

        public long IdAlmacenSeleccionado { get; set; }

        public FiltroBusquedaCajaTurno FiltroBusqueda {
            get => fieldFiltroBusqueda.SelectedIndex >= 0
                ? (FiltroBusquedaCajaTurno) fieldFiltroBusqueda.SelectedIndex
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
                fieldPaginaActual.Text = $"PÁGINA {value}";
            }
        }

        public int PaginasTotales {
            get => _paginasTotales;
            set {
                _paginasTotales = value;
                fieldPaginasTotales.Text = $"DE {value}";
                HabilitarBotonesPaginacion();
            }
        }

        public RepoVistaBase PanelCentral { get; private set; }

        public event EventHandler? AlturaContenedorTuplasModificada;
        public event EventHandler? MostrarPrimeraPagina;
        public event EventHandler? MostrarPaginaAnterior;
        public event EventHandler? MostrarPaginaSiguiente;
        public event EventHandler? MostrarUltimaPagina;
        public event EventHandler? SincronizarDatos;

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;
        public event EventHandler<(FiltroBusquedaCajaTurno, string[])>? BuscarEntidades;

        public event EventHandler? AbrirTurno;
        public event EventHandler? CerrarTurno;
        public event EventHandler? RegistrarMovimiento;

        public void Inicializar() {
            // Eventos
            fieldFiltroAlmacen.SelectedIndexChanged += OnCambioIndiceFiltroAlmacen;
            fieldFiltroBusquedaFechaDesde.Value = DateTime.Today;
            fieldFiltroBusquedaFechaDesde.ValueChanged += OnCambioValorFechaDesde;
            fieldFiltroBusquedaFechaHasta.Value = DateTime.Today;
            fieldFiltroBusquedaFechaHasta.ValueChanged += OnCambioValorFechaHasta;
            fieldFiltroBusqueda.SelectedIndexChanged += OnCambioIndiceFiltroBusqueda;
            fieldDatoBusqueda.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode != Keys.Enter)
                    return;

                if (CriteriosBusqueda.Length > 0 && !string.IsNullOrEmpty(CriteriosBusqueda[0]))
                    BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { IdAlmacenSeleccionado.ToString(), fieldFiltroBusquedaFechaDesde.Value.ToString("yyyy-MM-dd HH:mm:ss"), fieldFiltroBusquedaFechaHasta.Value.ToString("yyyy-MM-dd HH:mm:ss"), CriteriosBusqueda[0] }));
                else SincronizarDatos?.Invoke(sender, args);

                args.SuppressKeyPress = true;
            };
            btnAbrirTurno.Click += delegate (object? sender, EventArgs e) {
                AbrirTurno?.Invoke(sender, e);
            };
            btnCerrarTurno.Click += delegate (object? sender, EventArgs e) {
                CerrarTurno?.Invoke(fieldCodigoTurnoActivo.Text, e);
            };
            btnRegistrarMovimiento.Click += delegate (object? sender, EventArgs e) {
                RegistrarMovimiento?.Invoke(fieldCodigoTurnoActivo.Text, e);
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

        private void OnCambioIndiceFiltroAlmacen(object? sender, EventArgs e) {
            var almacenSeleccionado = fieldFiltroAlmacen.SelectedItem as Almacen;

            IdAlmacenSeleccionado = almacenSeleccionado?.Id ?? 0;

            if (IdAlmacenSeleccionado != 0)
                BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { IdAlmacenSeleccionado.ToString(), fieldFiltroBusquedaFechaDesde.Value.ToString("yyyy-MM-dd HH:mm:ss"), fieldFiltroBusquedaFechaHasta.Value.ToString("yyyy-MM-dd HH:mm:ss"), CriteriosBusqueda[0] }));
            else SincronizarDatos?.Invoke(sender, e);
        }

        private void OnCambioValorFechaDesde(object? sender, EventArgs e) {
            var valorFechaDesde = fieldFiltroBusquedaFechaDesde.Value.Date;
            var valorFechaHasta = fieldFiltroBusquedaFechaHasta.Value.Date;

            if (valorFechaDesde <= valorFechaHasta)
                BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { IdAlmacenSeleccionado.ToString(), fieldFiltroBusquedaFechaDesde.Value.ToString("yyyy-MM-dd"), fieldFiltroBusquedaFechaHasta.Value.ToString("yyyy-MM-dd"), CriteriosBusqueda[0] }));
            else {
                fieldFiltroBusquedaFechaDesde.Value = valorFechaHasta;

                CentroNotificaciones.MostrarNotificacion("La fecha de inicio no puede ser mayor que la fecha final o fecha del día de hoy, por favor, corrija los datos de entrada", Core.Modelos.Comun.TipoNotificacionEnum.Advertencia);
            }
        }

        private void OnCambioValorFechaHasta(object? sender, EventArgs e) {
            var valorFechaDesde = fieldFiltroBusquedaFechaDesde.Value.Date;
            var valorFechaHasta = fieldFiltroBusquedaFechaHasta.Value.Date;

            if (valorFechaHasta >= valorFechaDesde && valorFechaHasta <= DateTime.Now)
                BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { IdAlmacenSeleccionado.ToString(), fieldFiltroBusquedaFechaDesde.Value.ToString("yyyy-MM-dd"), fieldFiltroBusquedaFechaHasta.Value.ToString("yyyy-MM-dd"), CriteriosBusqueda[0] }));
            else {
                fieldFiltroBusquedaFechaHasta.Value = DateTime.Now;

                CentroNotificaciones.MostrarNotificacion("La fecha final no puede ser menor que la fecha inicial o mayor que la fecha del día de hoy, por favor, corrija los datos de entrada", Core.Modelos.Comun.TipoNotificacionEnum.Advertencia);
            }
        }

        private void OnCambioIndiceFiltroBusqueda(object? sender, EventArgs e) {
            fieldDatoBusqueda.Text = string.Empty;
            fieldDatoBusqueda.Visible = fieldFiltroBusqueda.SelectedIndex != 0;

            if (fieldDatoBusqueda.Visible)
                fieldDatoBusqueda.Focus();

            BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { IdAlmacenSeleccionado.ToString(), fieldFiltroBusquedaFechaDesde.Value.ToString("yyyy-MM-dd HH:mm:ss"), fieldFiltroBusquedaFechaHasta.Value.ToString("yyyy-MM-dd HH:mm:ss"), CriteriosBusqueda[0] }));

            // Ir a la primera página al cambiar el criterio de búsqueda
            PaginaActual = 1;
            HabilitarBotonesPaginacion();
        }

        public void CargarFiltroAlmacenes(Almacen[] almacenes) {
            // Evitar que se dispare el evento SelectedIndexChanged al modificar los ítems
            fieldFiltroAlmacen.SelectedIndexChanged -= OnCambioIndiceFiltroAlmacen;

            fieldFiltroAlmacen.Items.Clear();
            fieldFiltroAlmacen.Items.AddRange(almacenes);
            fieldFiltroAlmacen.SelectedIndex = -1;

            // Reasignar el evento SelectedIndexChanged
            fieldFiltroAlmacen.SelectedIndexChanged += OnCambioIndiceFiltroAlmacen;
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

            if (fieldFiltroAlmacen.Items.Count > 0)
                fieldFiltroAlmacen.SelectedIndex = 0;
            if (fieldFiltroBusqueda.Items.Count > 0)
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

        public void RefrescarEstadoTurnoActivo() {
            var turnoActivo = RepoCajaTurno.Instancia
                .Buscar(FiltroBusquedaCajaTurno.Estado, "Abierto").resultadosBusqueda
                .Select(r => r.entidadBase)
                .FirstOrDefault(t => t.IdAlmacen == IdAlmacenSeleccionado);

            btnAbrirTurno.Enabled = turnoActivo == null;
            layoutVista.SuspendLayout();

            if (turnoActivo != null) {
                fieldCodigoTurnoActivo.Text = turnoActivo.Codigo;
                fieldFechaHoraApertura.Text = turnoActivo.FechaApertura.ToString("dd/MM/yyyy HH:mm");
                fieldFondoInicial.Text = turnoActivo.MontoApertura.ToString("N2");
                fieldOperador.Text = RepoCuentaUsuario.Instancia.ObtenerPorId(turnoActivo.IdCuentaApertura)!.Nombre;
            }

            layoutVista.RowStyles[7].Height = turnoActivo == null ? 0 : 70;
            panelTurnoActual.Visible = turnoActivo != null;
            layoutVista.ResumeLayout();
        }
    }
}