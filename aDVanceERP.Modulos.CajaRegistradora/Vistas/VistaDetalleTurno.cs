using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Caja;
using aDVanceERP.Modulos.CajaRegistradora.Interfaces;
using aDVanceERP.Modulos.CajaRegistradora.Properties;

using System.Globalization;

namespace aDVanceERP.Modulos.CajaRegistradora.Vistas {
    public partial class VistaDetalleTurno : Form, IVistaDetalleTurno {
        private int _paginaActual = 1;
        private int _paginasTotales = 1;

        public VistaDetalleTurno() {
            InitializeComponent();

            NombreVista = nameof(VistaDetalleTurno);
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

        public FiltroBusquedaCajaMovimiento FiltroBusqueda {
            get => FiltroBusquedaCajaMovimiento.Todos;
        }

        public string[] CriteriosBusqueda {
            get => [fieldCriterioBusqueda.Text];
            set => fieldCriterioBusqueda.Text = value.Length > 0 ? value[0] : string.Empty;
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

        public event EventHandler<(FiltroBusquedaCajaMovimiento, string[])>? BuscarEntidades;
        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            // Eventos
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
            btnSalir.Click += delegate {
                Ocultar();
            };
        }

        public void CargarFiltrosBusqueda((string Nombre, string Descripcion)[] filtrosBusqueda) {
            // ...
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {


            PanelCentral.CerrarTodos();
            PaginaActual = 1;
            PaginasTotales = 1;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            // ...
        }

        public void CargarDatosGeneralesTurno(CajaTurno turno) {
            var (colorFondo, colorFuente) = ObtenerColorEstado(turno.Estado);
            var totalesCalculados = RepoCajaMovimiento.Instancia.ObtenerTotalesPorCanal(turno.Id);

            fieldSubtitulo.Text = turno.Codigo;
            fieldCodigo.Text = turno.Codigo;
            fieldEstado.DisabledState.BorderColor = colorFondo;
            fieldEstado.DisabledState.FillColor = colorFondo;
            fieldEstado.DisabledState.ForeColor = colorFuente;
            fieldEstado.Text = $"{(turno.Estado == EstadoCajaTurnoEnum.Anulado ? "X" : "●")} {turno.Estado.ObtenerNombreDescripcion()}";
            fieldFechaHoraApertura.Text = turno.FechaApertura.ToString("dd/MM/yyyy HH:mm");
            fieldFechaHoraCierre.Text = turno.FechaCierre.HasValue ? turno.FechaCierre.Value.ToString("dd/MM/yyyy HH:mm") : "N/A";
            fieldEfectivoCalculado.Text = totalesCalculados.TotalEfectivo.ToString("N2", CultureInfo.InvariantCulture);
            fieldDiferenciaEfectivo.Text = turno.DiferenciaEfectivo.HasValue ? turno.DiferenciaEfectivo.Value.ToString("N2", CultureInfo.InvariantCulture) : "N/A";
            
            fieldTotalEfectivo.Text = turno.MontoEfectivoCalculado.HasValue && turno.DiferenciaEfectivo.HasValue
                ? (turno.MontoEfectivoCalculado.Value + turno.DiferenciaEfectivo.Value).ToString("N2", CultureInfo.InvariantCulture)
                : totalesCalculados.TotalEfectivo.ToString("N2", CultureInfo.InvariantCulture);
            fieldTotalTransferencias.Text = turno.MontoTransferenciasCalculado.HasValue && turno.DiferenciaTransferencias.HasValue
                ? (turno.MontoTransferenciasCalculado.Value + turno.DiferenciaTransferencias.Value).ToString("N2", CultureInfo.InvariantCulture)
                : totalesCalculados.TotalTransferencias.ToString("N2", CultureInfo.InvariantCulture);
            fieldTotalGeneral.Text = turno.MontoEfectivoCalculado.HasValue && turno.DiferenciaEfectivo.HasValue && turno.MontoTransferenciasCalculado.HasValue && turno.DiferenciaTransferencias.HasValue
                ? (turno.MontoEfectivoCalculado.Value + turno.DiferenciaEfectivo.Value + turno.MontoTransferenciasCalculado.Value + turno.DiferenciaTransferencias.Value).ToString("N2", CultureInfo.InvariantCulture)
                : (totalesCalculados.TotalEfectivo + totalesCalculados.TotalTransferencias).ToString("N2", CultureInfo.InvariantCulture);
        }

        private (Color colorFondo, Color colorFuente) ObtenerColorEstado(EstadoCajaTurnoEnum estado) {
            return estado switch {
                EstadoCajaTurnoEnum.Abierto => (Color.FromArgb(232, 245, 233), Color.FromArgb(45, 125, 50)),
                EstadoCajaTurnoEnum.Cerrado => (Color.FromArgb(227, 242, 253), Color.FromArgb(21, 101, 192)),
                EstadoCajaTurnoEnum.Anulado => (Color.FromArgb(253, 236, 242), Color.FromArgb(215, 104, 104)),
                _ => (Color.DimGray, Color.Black),
            };
        }

        private void HabilitarBotonesPaginacion() {
            btnPrimeraPagina.Enabled = PaginaActual > 1;
            btnPaginaAnterior.Enabled = PaginaActual > 1;
            btnUltimaPagina.Enabled = PaginaActual < PaginasTotales;
            btnPaginaSiguiente.Enabled = PaginaActual < PaginasTotales;

            // Iconos
            btnPrimeraPagina.CustomImages.Image = PaginaActual > 1
                ? Resources.page_first_24px
                : Resources.page_first_disabled_24px;
            btnPaginaAnterior.CustomImages.Image = PaginaActual > 1
                ? Resources.page_previous_24px
                : Resources.page_previous_disabled_24px;
            btnUltimaPagina.CustomImages.Image = PaginaActual < PaginasTotales
                ? Resources.page_last_24px
                : Resources.page_last_disabled_24px;
            btnPaginaSiguiente.CustomImages.Image = PaginaActual < PaginasTotales
                ? Resources.page_next_24px
                : Resources.page_next_disabled_24px;
        }
    }
}