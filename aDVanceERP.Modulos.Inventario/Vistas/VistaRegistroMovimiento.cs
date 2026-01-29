using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaRegistroMovimiento : Form, IVistaRegistroMovimiento {
        private bool _modoEdicion = false;
        private TipoMovimiento[] _tiposMovimiento = Array.Empty<TipoMovimiento>();
        private Producto? _productoSeleccionado;

        public VistaRegistroMovimiento() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroMovimiento);

            Inicializar();
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                _modoEdicion = value;

                fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
                btnRegistrarActualizar.Text = value ? "Actualizar el movimiento" : "Registrar el movimiento";
                fieldNombreProducto.ReadOnly = value;
                fieldTipoMovimiento.Enabled = !value;
                fieldAlmacenOrigen.Enabled = !value;
                fieldAlmacenDestino.Enabled = !value;
                fieldCantidadMovida.ReadOnly = value;
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

        public string NombreProducto { 
            get => fieldNombreProducto.Text;
            set => fieldNombreProducto.Text = value;
        }

        public string? NombreAlmacenOrigen { 
            get => fieldAlmacenOrigen.Text;
            set => fieldAlmacenOrigen.Text = value;
        }

        public string? NombreAlmacenDestino { 
            get => fieldAlmacenDestino.Text;
            set => fieldAlmacenDestino.Text = value;
        }

        public DateTime Fecha { 
            get => fieldFecha.Value;
            set => fieldFecha.Value = value;
        }

        public decimal CantidadMovida { 
            get => decimal.TryParse(fieldCantidadMovida.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            set => fieldCantidadMovida.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public string NombreTipoMovimiento { 
            get => fieldTipoMovimiento.SelectedItem?.ToString() ?? string.Empty;
            set => fieldTipoMovimiento.SelectedItem = value;
        }

        public string Notas {
            get => fieldNotas.Text;
            set => fieldNotas.Text = value;
        }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            fieldNombreProducto.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode != Keys.Enter)
                    return;

                _productoSeleccionado = RepoProducto.Instancia.Buscar(FiltroBusquedaProducto.Nombre, NombreProducto).resultadosBusqueda.FirstOrDefault(p => p.entidadBase.Nombre.Equals(NombreProducto)).entidadBase;

                if (_productoSeleccionado == null) {
                    CentroNotificaciones.MostrarNotificacion("El producto entrado no se encuentra registrado en la base de datos. Entre el nombre de un producto válido antes de realizar el movimiento.", TipoNotificacion.Advertencia);

                    _productoSeleccionado = null;
                    fieldNombreProducto.Text = string.Empty;                    
                    fieldAbreviaturaUM1.Text = "u";

                    fieldNombreProducto.Focus();
                    return;
                }

                ActualizarUnidadMedidaProductoSeleccionado(_productoSeleccionado);

                args.SuppressKeyPress = true;
            };
            fieldTipoMovimiento.SelectedIndexChanged += delegate {
                ActualizarCamposAlmacenes();
            };
            fieldAlmacenOrigen.SelectedIndexChanged += HabilitaDeshabilitarCampoCantidad;
            fieldAlmacenDestino.SelectedIndexChanged += HabilitaDeshabilitarCampoCantidad;
            btnRegistrarActualizar.Click += delegate (object? sender, EventArgs args) {
                if (ModoEdicion)
                    EditarEntidad?.Invoke(sender, args);
                else
                    RegistrarEntidad?.Invoke(sender, args);
            };
            btnSalir.Click += delegate (object? sender, EventArgs args) { Ocultar(); };
        }

        private void ActualizarCamposAlmacenes() {
            var tipoMovimiento = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Nombre, NombreTipoMovimiento).resultadosBusqueda.FirstOrDefault(tm => tm.entidadBase.Nombre.Equals(NombreTipoMovimiento)).entidadBase;

            if (tipoMovimiento?.Efecto == EfectoMovimiento.Carga) {
                fieldAlmacenOrigen.SelectedIndex = 0;
                fieldAlmacenOrigen.Enabled = false;
                fieldAlmacenDestino.Enabled = !ModoEdicion;
            } else if (tipoMovimiento?.Efecto == EfectoMovimiento.Descarga) {
                fieldAlmacenDestino.SelectedIndex = 0;
                fieldAlmacenDestino.Enabled = false;
                fieldAlmacenOrigen.Enabled = !ModoEdicion;
            } else {
                fieldAlmacenOrigen.Enabled = !ModoEdicion && tipoMovimiento != null;
                fieldAlmacenDestino.Enabled = !ModoEdicion && tipoMovimiento != null;
            }
        }

        private void HabilitaDeshabilitarCampoCantidad(object? sender, EventArgs e) {
            fieldCantidadMovida.ReadOnly = 
                ModoEdicion ||
                (fieldAlmacenOrigen.SelectedIndex == 0 && fieldAlmacenDestino.SelectedIndex == 0) ||
                (fieldAlmacenOrigen.SelectedIndex != 0 && fieldAlmacenDestino.SelectedIndex != 0 && fieldAlmacenOrigen.SelectedItem?.ToString() == fieldAlmacenDestino.SelectedItem?.ToString());
        }

        public void ActualizarUnidadMedidaProductoSeleccionado(Core.Modelos.Modulos.Inventario.Producto producto) {
            var unidadMedida = RepoUnidadMedida.Instancia.ObtenerPorId(producto?.IdUnidadMedida ?? 0);

            fieldAbreviaturaUM1.Text = unidadMedida?.Abreviatura ?? "u";
        }

        public void Mostrar() {
            Fecha = DateTime.Now;

            BringToFront();
            Show();
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {
            NombreProducto = string.Empty;
            fieldAlmacenOrigen.SelectedIndex = fieldAlmacenOrigen.Items.Count > 0 ? 0 : -1;
            fieldAlmacenDestino.SelectedIndex = fieldAlmacenDestino.Items.Count > 0 ? 0 : -1;
            fieldCantidadMovida.Text = string.Empty;
            NombreTipoMovimiento = string.Empty;
            fieldTipoMovimiento.SelectedIndex = -1;
            Fecha = DateTime.Now;
            Notas = string.Empty;
        }

        public void Cerrar() {
            Dispose();
        }

        public void CargarNombresProductos(string[] nombresProductos) {
            fieldNombreProducto.AutoCompleteCustomSource.Clear();
            fieldNombreProducto.AutoCompleteCustomSource.AddRange(nombresProductos);
            fieldNombreProducto.AutoCompleteMode = AutoCompleteMode.Suggest;
            fieldNombreProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public void CargarNombresAlmacenes(object[] nombresAlmacenes) {
            fieldAlmacenOrigen.Items.Clear();
            fieldAlmacenOrigen.Items.Add("Ninguno");
            fieldAlmacenOrigen.Items.AddRange(nombresAlmacenes);
            fieldAlmacenOrigen.SelectedIndex = nombresAlmacenes.Length > 0 ? 0 : -1;

            fieldAlmacenDestino.Items.Clear();
            fieldAlmacenDestino.Items.Add("Ninguno");
            fieldAlmacenDestino.Items.AddRange(nombresAlmacenes);
            fieldAlmacenDestino.SelectedIndex = nombresAlmacenes.Length > 0 ? 0 : -1;
        }

        public void CargarTiposMovimientos(TipoMovimiento[] tiposMovimientos) {
            _tiposMovimiento = tiposMovimientos;

            fieldTipoMovimiento.Items.Clear();

            foreach (var tipoMovimiento in tiposMovimientos) {
                var nombreTipoMovimiento = tipoMovimiento.Nombre;

                if (string.IsNullOrEmpty(nombreTipoMovimiento) ||
                    nombreTipoMovimiento.Equals("Compra") ||
                    nombreTipoMovimiento.Equals("Venta") ||
                    nombreTipoMovimiento.Equals("Entrada de Producción") ||
                    nombreTipoMovimiento.Equals("Salida a Producción") ||
                    nombreTipoMovimiento.Equals("Carga Inicial"))
                    continue;

                fieldTipoMovimiento.Items.Add(tipoMovimiento.Nombre);
            }

            if (fieldTipoMovimiento.Items.Count > 0)
                fieldTipoMovimiento.SelectedIndex = tiposMovimientos.Length > 0 ? 0 : -1;
        }
    }
}
