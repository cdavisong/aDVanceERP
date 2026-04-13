using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaGestionVentaPresentacion : Form, IVistaGestionVentaPresentacion {
        private FiltroBusquedaPrecioPresentacion _filtroBusqueda = FiltroBusquedaPrecioPresentacion.IdProducto;
        private string[] _criterioBusqueda = [];
        private int _paginaActual = 1;
        private int _paginasTotales = 1;

        public VistaGestionVentaPresentacion() {
            InitializeComponent();

            NombreVista = nameof(VistaGestionVentaPresentacion);
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

        public FiltroBusquedaPrecioPresentacion FiltroBusqueda {
            get => _filtroBusqueda;
            private set => _filtroBusqueda = value;
        }

        public string[] CriteriosBusqueda {
            get => _criterioBusqueda.Length > 0 ? _criterioBusqueda : [IdProducto.ToString()];
            private set => _criterioBusqueda = value;
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

        public long IdProducto { get; set; }

        public UnidadMedida? UnidadMedida {
            get => fieldUnidadMedida.SelectedItem as UnidadMedida;
            set { 
                fieldUnidadMedida.SelectedItem = value; 


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

        public event EventHandler? AlturaContenedorTuplasModificada;
        public event EventHandler? MostrarPrimeraPagina;
        public event EventHandler? MostrarPaginaAnterior;
        public event EventHandler? MostrarPaginaSiguiente;
        public event EventHandler? MostrarUltimaPagina;
        public event EventHandler? SincronizarDatos;

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;
        public event EventHandler<(FiltroBusquedaPrecioPresentacion, string[])>? BuscarEntidades;

        public void Inicializar() {
            // Eventos
            btnRegistrar.Click += delegate (object? sender, EventArgs e) {
                RegistrarEntidad?.Invoke(new PrecioPresentacion() {
                    Id = 0,
                    IdProducto = IdProducto,
                    IdUnidadMedida = UnidadMedida?.Id ?? throw new ArgumentNullException(nameof(UnidadMedida)),
                    Cantidad = Cantidad,
                    PrecioVenta = PrecioVenta,
                    Activo = true
                }, e);
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
            BringToFront();
            Show();
        }

        public void Restaurar() {
            fieldNombreProducto.Text = "...";
            UnidadMedida = null;
            Cantidad = 0;
            PrecioVenta = 0;
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

        public void CargarUnidadesMedida(UnidadMedida[] unidadesMedida) {
            fieldUnidadMedida.Items.Clear();
            fieldUnidadMedida.Items.AddRange(unidadesMedida);
            fieldUnidadMedida.SelectedIndex = unidadesMedida.Length > 0 ? 0 : -1;
        }

        public void CargarDatosProducto(Producto producto) {
            var unidadMedidaBase = RepoUnidadMedida.Instancia.ObtenerPorId(producto.IdUnidadMedida)!;

            IdProducto = producto.Id;
            fieldNombreProducto.Text = producto.Nombre;
            fieldAbreviaturaUmCantPresentacion.Text = unidadMedidaBase.Abreviatura;
            fieldTituloDescripcion.Text = fieldTituloDescripcion.Text.Replace("[u]", unidadMedidaBase.Abreviatura);
            fieldTextoAdvertencia.Text = fieldTextoAdvertencia.Text.Replace("[u]", unidadMedidaBase.Abreviatura);
        }
    }
}