using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Monedas;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaGestionPresentacionesProducto : Form, IVistaGestionPresentacionProducto {
        private int _paginaActual = 1;
        private int _paginasTotales = 1;
        private bool _modoEdicion;

        public VistaGestionPresentacionesProducto() {
            InitializeComponent();

            NombreVista = nameof(VistaGestionPresentacionesProducto);
            PanelCentral = new RepoVistaBase(contenedorVistas);
            FiltroBusqueda = FiltroBusquedaPresentacionProducto.IdProducto;
            CriteriosBusqueda = [IdProducto.ToString()];

            Inicializar();
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                _modoEdicion = value;

                btnRegistrarActualizar.Text = value ? "Actualizar la presentación" : "Agregar nueva presentación";
            }
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

        public FiltroBusquedaPresentacionProducto FiltroBusqueda { get; set; }

        public string[] CriteriosBusqueda { get; set; }

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

        public long IdProducto { get; set; }

        public UnidadMedida? UnidadMedida {
            get => fieldUnidadMedida.SelectedItem as UnidadMedida;
            set { 
                fieldUnidadMedida.SelectedItem = value;
                fieldAbreviaturaUmCantPresentacion.Text = value != null ? value.Abreviatura : string.Empty;
            }
        }

        public decimal Cantidad {
            get => decimal.TryParse(fieldCantidadPresentacion.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                        out var value)
                        ? value
                        : 0m;
            set => fieldCantidadPresentacion.Text = value > 0
                    ? value.ToString("N1", CultureInfo.InvariantCulture)
                    : string.Empty;
        }

        public decimal PrecioVenta {
            get => decimal.TryParse(fieldPrecioVentaPresentacion.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                        out var value)
                        ? value
                        : 0m;
            set => fieldPrecioVentaPresentacion.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : string.Empty;
        }

        public Moneda? MonedaPrecioVenta {
            get => fieldMonedaPrecioVentaPresentacion.SelectedItem as Moneda;
            set => fieldMonedaPrecioVentaPresentacion.SelectedItem = value;
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
        public event EventHandler<(FiltroBusquedaPresentacionProducto, string[])>? BuscarEntidades;

        public void Inicializar() {
            // Eventos
            btnRegistrarActualizar.Click += delegate (object? sender, EventArgs args) {
                if (ModoEdicion)
                    EditarEntidad?.Invoke(sender, args);
                else
                    RegistrarEntidad?.Invoke(sender, args);
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
            btnSalir.Click += delegate {
                Ocultar();
            };
        }

        public void CargarFiltrosBusqueda((string Nombre, string Descripcion)[] filtrosBusqueda) {
            // ...
        }

        public void Mostrar() {
            VerificarPermisos();
            BringToFront();
            Show();
        }

        private void VerificarPermisos() {
            if (ContextoSeguridad.EstaAutenticado && ContextoSeguridad.EsAdministrador)
                return;

            btnRegistrarActualizar.Enabled = ContextoSeguridad.GestorPermisos?
                .TienePermiso(
                    ModuloSistemaEnum.MOD_INVENTARIO,
                    AccionModuloEnum.Crear)
                ?? false;
        }

        public void Restaurar() {
            fieldNombreProducto.Text = "...";
            UnidadMedida = null;
            Cantidad = 0;
            PrecioVenta = 0;
            if (fieldMonedaPrecioVentaPresentacion.Items.Count > 0)
                fieldMonedaPrecioVentaPresentacion.SelectedIndex = 0;
            fieldTituloDescripcion.Text = "PRECIO POR [u]";
            fieldTextoAdvertencia.Text = "      El stock siempre se gestiona en la unidad base del producto. Al registrar una venta de cualquier presentación, el sistema descuenta la cantidad correspondiente en [u] del inventario.";

            PaginaActual = 1;
            PaginasTotales = 1;
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

        public void CargarDatosProducto(Producto producto) {
            var unidadMedidaBase = RepoUnidadMedida.Instancia.ObtenerPorId(producto.IdUnidadMedida)!;

            IdProducto = producto.Id;
            fieldNombreProducto.Text = producto.Nombre;
            fieldAbreviaturaUmCantPresentacion.Text = unidadMedidaBase.Abreviatura;
            fieldTituloDescripcion.Text = fieldTituloDescripcion.Text.Replace("[u]", unidadMedidaBase.Abreviatura);
            fieldTextoAdvertencia.Text = fieldTextoAdvertencia.Text.Replace("[u]", unidadMedidaBase.Abreviatura);
        }

        public void CargarUnidadesMedida(UnidadMedida[] unidadesMedida) {
            fieldUnidadMedida.Items.Clear();
            fieldUnidadMedida.Items.AddRange(unidadesMedida);
            fieldUnidadMedida.SelectedIndex = unidadesMedida.Length > 0 ? 0 : -1;
        }

        public void CargarMonedas(Moneda[] monedas) {
            fieldMonedaPrecioVentaPresentacion.Items.Clear();
            fieldMonedaPrecioVentaPresentacion.Items.AddRange(monedas);

            var monedaBase = monedas.FirstOrDefault(m => m.EsBase);

            if (monedaBase != null)
                fieldMonedaPrecioVentaPresentacion.SelectedItem = monedaBase;
            else if (monedas.Length > 0)
                fieldMonedaPrecioVentaPresentacion.SelectedIndex = 0;
        }
    }
}