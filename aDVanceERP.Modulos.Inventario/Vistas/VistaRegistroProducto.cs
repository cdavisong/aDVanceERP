using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Helpers.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Properties;

using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaRegistroProducto : Form, IVistaRegistroProducto {
        private bool _modoEdicion = false;
        private decimal _costoAdquisicionUnitario = 0;
        private decimal _costoProduccionUnitario = 0;
        private UnidadMedida[] _unidadesMedida = Array.Empty<UnidadMedida>();
        private ClasificacionProducto[] _clasificacionesProducto = Array.Empty<ClasificacionProducto>();

        public VistaRegistroProducto() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroProducto);

            Inicializar();
        }

        public bool ModoEdicion { 
            get => _modoEdicion;
            set {
                _modoEdicion = value;

                fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
                btnRegistrarActualizar.Text = value ? "Actualizar el producto" : "Registrar el producto";
                layoutTitulos4.Visible = !value;
                layoutDatos4.Visible = !value;
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

        public Image? Imagen {
            get => fieldImagen.BackgroundImage;
            set {
                if (value == null) {
                    fieldImagen.BackgroundImage = Resources.picture_170px;
                    return;
                }

                var imagen = new Bitmap(fieldImagen.Size.Width, fieldImagen.Size.Height);
                var imagenProducto = value.ObtenerRecorteImagen(imagen.Size);

                using (var g = Graphics.FromImage(imagen)) {
                    g.Clear(Color.White);
                    g.DrawImage(imagenProducto, 0, 0, imagen.Width, imagen.Height);
                }

                fieldImagen.BackgroundImage = imagen;
            }
        }

        public CategoriaProducto Categoria { 
            get => (CategoriaProducto) fieldCategoriaProducto.SelectedIndex;
            set {
                fieldCategoriaProducto.SelectedItem = value;

                ActualizarDescripcionCategoria(this, EventArgs.Empty);
                ActualizarVisibilidadCostosPorCategoria(this, EventArgs.Empty);
            }
        }

        public string NombreProducto { 
            get => fieldNombreProducto.Text;
            set => fieldNombreProducto.Text = value;
        }

        public string? Codigo {
            get => fieldCodigo.Text;
            set => fieldCodigo.Text = value;
        }

        public string NombreProveedor { 
            get => fieldNombreProveedor.Text;
            set => fieldNombreProveedor.Text = value;
        }

        public string Descripcion { 
            get => fieldDescripcion.Text;
            set => fieldDescripcion.Text = value;
        }

        public string NombreUnidadMedida {
            get => fieldUnidadMedida.Text; 
            set => fieldUnidadMedida.Text = value;
        }

        public string NombreClasificacionProducto { 
            get => fieldClasificacionProducto.Text;
            set => fieldClasificacionProducto.SelectedItem = value;
        }

        public bool EsVendible { 
            get => fieldEsVendible.Checked;
            set => fieldEsVendible.Checked = value;
        }

        public decimal CostoUnitario {
            get => Categoria == CategoriaProducto.Mercancia || Categoria == CategoriaProducto.MateriaPrima
                ? CostoAdquisicionUnitario
                : Categoria == CategoriaProducto.ProductoTerminado
                    ? CostoProduccionUnitario
                    : 0m;
            set {
                _costoAdquisicionUnitario = Categoria == CategoriaProducto.Mercancia || Categoria == CategoriaProducto.MateriaPrima ? value : 0m;
                _costoProduccionUnitario = Categoria == CategoriaProducto.ProductoTerminado ? value : 0m;

                fieldCostoUnitario.Text = Categoria == CategoriaProducto.Mercancia || Categoria == CategoriaProducto.MateriaPrima
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "0";
            }
        }

        public decimal CostoAdquisicionUnitario { 
            get => _costoAdquisicionUnitario;
        }

        public decimal CostoProduccionUnitario {
            get => _costoProduccionUnitario;
        }

        public decimal ImpuestoVentaPorcentaje {
            get {
                var impuestoVentaPorcentaje = !string.IsNullOrEmpty(fieldImpuestoVentaPorcentaje.Text)
                    ? fieldImpuestoVentaPorcentaje.Text
                    : fieldImpuestoVentaPorcentaje.PlaceholderText;

                return decimal.TryParse(impuestoVentaPorcentaje, CultureInfo.InvariantCulture, out var value) ? value : 0m; 
            }
            set => fieldImpuestoVentaPorcentaje.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public decimal MargenGananciaDeseado {
            get {
                var margenGananciaDeseado = !string.IsNullOrEmpty(fieldMargenGananciaDeseado.Text)
                    ? fieldMargenGananciaDeseado.Text
                    : fieldMargenGananciaDeseado.PlaceholderText;

                return decimal.TryParse(margenGananciaDeseado, CultureInfo.InvariantCulture, out var value) ? value : 0m; 
            }
            set => fieldMargenGananciaDeseado.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public decimal PrecioVentaBase { 
            get => decimal.TryParse(fieldPrecioVentaBase.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            set => fieldPrecioVentaBase.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public string NombreAlmacen {
            get => fieldNombreAlmacen.Text;
            set => fieldNombreAlmacen.Text = value;
        }

        public decimal CantidadInicial { 
            get => decimal.TryParse(fieldCantidadInicial.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            set => fieldCantidadInicial.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public decimal CantidadMinima { 
            get => decimal.TryParse(fieldCantidadMinima.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            set => fieldCantidadMinima.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public bool HabilitarNotificacionesStockBajo {
            get => fieldNotificacionesStockBajo.Checked;
            set => fieldNotificacionesStockBajo.Checked = value;
        }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            fieldCategoriaProducto.SelectedIndexChanged += ActualizarDescripcionCategoria;
            fieldCategoriaProducto.SelectedIndexChanged += ActualizarVisibilidadCostosPorCategoria;
            fieldUnidadMedida.SelectedIndexChanged += ActualizarDescripcionUnidadMedida;
            fieldUnidadMedida.SelectedIndexChanged += ActualizarAbreviaturasUnidadMedida;
            fieldClasificacionProducto.SelectedIndexChanged += ActualizarDescripcionClasificacion;
            fieldCostoUnitario.TextChanged += ActualizarCostoUnitario;
            fieldPrecioVentaBase.IconLeftClick += CalcularPrecioVenta;
            fieldMargenGananciaDeseado.IconLeftClick += CalcularMargenGanancia;
            fieldCodigo.IconRightClick += delegate {
                fieldCodigo.Text = CodigoHelper.GenerarEan13($"{Categoria}.{NombreProducto}");
            };
            btnRegistrarActualizar.Click += delegate (object? sender, EventArgs args) {
                if (ModoEdicion)
                    EditarEntidad?.Invoke(sender, args);
                else
                    RegistrarEntidad?.Invoke(sender, args);
            };
            btnSalir.Click += delegate (object? sender, EventArgs args) { Ocultar(); };
        }

        private void ActualizarDescripcionCategoria(object? sender, EventArgs e) {
            toolTip1.SetToolTip(fieldCategoriaProducto, UtilesCategoriaProducto.DescripcionesProducto[(int) Categoria]);
        }

        private void ActualizarVisibilidadCostosPorCategoria(object? sender, EventArgs e) {
            switch (Categoria) {
                case CategoriaProducto.Mercancia:
                case CategoriaProducto.MateriaPrima:
                    fieldTituloNombreProveedor.Visible = true;
                    fieldNombreProveedor.Visible = true;
                    fieldTituloCostoUnitario.Text = "      Costo de adquisción :";
                    break;
                case CategoriaProducto.ProductoTerminado:
                    fieldTituloNombreProveedor.Visible = false;
                    fieldNombreProveedor.Visible = false;
                    fieldTituloCostoUnitario.Text = "      Costo de producción :";
                    break;
                default:
                    break;
            }
        }

        private void ActualizarDescripcionUnidadMedida(object? sender, EventArgs e) {
            if (_unidadesMedida.Length == 0 || fieldUnidadMedida.SelectedIndex < 0)
                return;

            toolTip2.SetToolTip(fieldUnidadMedida, _unidadesMedida[fieldUnidadMedida.SelectedIndex].Descripcion);
        }

        private void ActualizarAbreviaturasUnidadMedida(object? sender, EventArgs e) {
            if (_unidadesMedida.Length == 0)
                return;

            fieldAbreviaturaUM1.Text = _unidadesMedida[fieldUnidadMedida.SelectedIndex].Abreviatura ?? "U";
            fieldAbreviaturaUM2.Text = _unidadesMedida[fieldUnidadMedida.SelectedIndex].Abreviatura ?? "U";
        }

        private void ActualizarDescripcionClasificacion(object? sender, EventArgs e) {
            if (_clasificacionesProducto.Length == 0 || fieldClasificacionProducto.SelectedIndex < 0)
                return;

            toolTip3.SetToolTip(fieldClasificacionProducto, _clasificacionesProducto[fieldClasificacionProducto.SelectedIndex].Descripcion);
        }

        private void ActualizarCostoUnitario(object? sender, EventArgs e) {
            _costoAdquisicionUnitario = Categoria == CategoriaProducto.Mercancia || Categoria == CategoriaProducto.MateriaPrima ? decimal.TryParse(fieldCostoUnitario.Text, CultureInfo.InvariantCulture, out var valueAdq) ? valueAdq: 0m : 0m;
            _costoProduccionUnitario = Categoria == CategoriaProducto.ProductoTerminado ? decimal.TryParse(fieldCostoUnitario.Text, CultureInfo.InvariantCulture, out var valueProd) ? valueProd : 0m : 0m;
        }

        private void CalcularPrecioVenta(object? sender, EventArgs e) {
            PrecioVentaBase = CostoUnitario + (CostoUnitario * (ImpuestoVentaPorcentaje / 100m)) + (CostoUnitario * (MargenGananciaDeseado / 100m));
        }

        private void CalcularMargenGanancia(object? sender, EventArgs e) {
            MargenGananciaDeseado = (PrecioVentaBase - (CostoUnitario + (CostoUnitario * (ImpuestoVentaPorcentaje / 100m)))) * 100 / CostoUnitario;
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {
            Imagen = null;
            Categoria = CategoriaProducto.Mercancia;
            NombreProducto = string.Empty;
            Codigo = string.Empty;
            NombreProveedor = string.Empty;
            Descripcion = string.Empty;
            NombreUnidadMedida = string.Empty;
            NombreClasificacionProducto = string.Empty;
            EsVendible = true;
            fieldCostoUnitario.Text = string.Empty;
            fieldImpuestoVentaPorcentaje.Text = string.Empty;
            fieldMargenGananciaDeseado.Text = string.Empty;
            fieldPrecioVentaBase.Text = string.Empty;
        }

        public void Cerrar() {
            Dispose();
        }

        public void CargarNombresProveedores(string[] nombresProvedores) {
            if (nombresProvedores == null || nombresProvedores.Length == 0) {
                fieldTituloNombreProveedor.Visible = false;
                fieldNombreProveedor.Visible = false;
                return;
            }

            fieldTituloNombreProveedor.Visible = true;
            fieldNombreProveedor.Visible = true;
            fieldNombreProveedor.Items.Clear();
            fieldNombreProveedor.Items.AddRange(nombresProvedores);
            fieldNombreProveedor.SelectedIndex = -1;
        }

        public void CargarUnidadesMedida(UnidadMedida[] unidadesMedida) {
            _unidadesMedida = unidadesMedida;

            fieldUnidadMedida.Items.Clear();
            fieldUnidadMedida.Items.AddRange(unidadesMedida.Select(um => um.Nombre).ToArray());
            fieldUnidadMedida.SelectedIndex = unidadesMedida.Length > 0 ? 0 : -1;
        }

        public void CargarClasificaciones(ClasificacionProducto[] clasificaciones) {
            _clasificacionesProducto = clasificaciones;

            fieldClasificacionProducto.Items.Clear();
            fieldClasificacionProducto.Items.AddRange(clasificaciones.Select(c => c.Nombre).ToArray());
            fieldClasificacionProducto.SelectedIndex = -1;
        }

        public void CargarNombresAlmacenes(string[] nombresAlmacenes) {
            fieldNombreAlmacen.Items.Clear();
            fieldNombreAlmacen.Items.AddRange(nombresAlmacenes);
            fieldNombreAlmacen.SelectedIndex = -1;
        }
    }
}
