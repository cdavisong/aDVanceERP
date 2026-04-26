using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Modulos.Venta.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaProductoCarritoCard : Form, IVistaProductoCarrito {
        private decimal _cantidad;

        public VistaProductoCarritoCard() {
            InitializeComponent();
            Inicializar();
        }

        public string NombreVista {
            get => $"{IdProducto:0000}{Name}";
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

        public long IdProducto { get; set; }

        public long IdPresentacion { get; set; }

        public string Nombre {
            get => fieldNombreProducto.Text;
            set => fieldNombreProducto.Text = value;
        }

        public string Clasificacion {
            get => fieldClasificacion.Text;
            set => fieldClasificacion.Text = value;
        }

        public string Codigo {
            get => fieldCodigo.Text.Split(':')[1].Trim();
            set => fieldCodigo.Text = $"COD : {value}";
        }

        public decimal PrecioVenta {
            get {
                var precioTexto = fieldPrecioVenta.Text.Replace("$ ", "").Trim();
                return decimal.TryParse(precioTexto, CultureInfo.InvariantCulture, out var precio) ? precio : 0m;
            }
            set => fieldPrecioVenta.Text = $"$ {value.ToString("N2", CultureInfo.InvariantCulture)}";
        }

        public decimal Cantidad { 
            get => _cantidad;
            set {
                _cantidad = value; 

                panelBase.BorderColor = value > 0
                    ? Color.Gainsboro
                    : Color.FromArgb(239, 83, 80);
                panelBase.FillColor = value > 0 
                    ? Color.FromArgb(255, 255, 255) 
                    : Color.FromArgb(255, 235, 238);
            }
        }

        public event EventHandler<UnidadMedida>? CambioPresentacion;
        public event EventHandler<long>? ProductoSeleccionado;


        public void Inicializar() {
            // Eventos
            fieldPresentaciones.SelectedIndexChanged += OnCambioPresentacion;
            btnAgregar.Click += OnSeleccionarProducto;
        }

        private void OnCambioPresentacion(object? sender, EventArgs e) {
            if (fieldPresentaciones.SelectedItem is UnidadMedida unidadMedida)
                CambioPresentacion?.Invoke(this, unidadMedida);
        }

        private void OnSeleccionarProducto(object? sender, EventArgs e) {
            ProductoSeleccionado?.Invoke(this, IdProducto);
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {
            IdProducto = 0;
            Nombre = string.Empty;
            Clasificacion = string.Empty;
            Codigo = string.Empty;
            PrecioVenta = 0m;
            Cantidad = 0m;

            if (fieldPresentaciones.Items.Count > 0)
                fieldPresentaciones.SelectedIndex = 0;
        }

        public void Cerrar() {
            fieldPresentaciones.SelectedIndexChanged -= OnCambioPresentacion;
            btnAgregar.Click -= OnSeleccionarProducto;
        }

        public void CargarPresentaciones(UnidadMedida[] unidadesMedida) {
            fieldPresentaciones.Items.Clear();
            fieldPresentaciones.Items.AddRange(unidadesMedida);
            fieldPresentaciones.SelectedIndex = unidadesMedida.Length > 0 ? 0 : -1;
        }
    }
}
