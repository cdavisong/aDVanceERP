using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Utiles;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Properties;

using System.Globalization;

namespace aDVanceERP.Modulos.Taller.Vistas.OrdenProduccion {
    public partial class VistaRegistroProducto : Form, IVistaRegistroProducto {
        private bool _modoEdicion = false;
        private UnidadMedida[] _unidadesMedida = Array.Empty<UnidadMedida>();

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
                fieldDescripcionCategoriaProducto.ToolTipTitle = UtilesCategoriaProducto.DescripcionesProducto[(int)value];

                // Ajustar visibilidad de campos según la categoría seleccionada
                switch (value) {
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

        }

        public string Nombre { 
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
            set => fieldClasificacionProducto.Text = value;
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
                fieldCostoUnitario.Text = Categoria == CategoriaProducto.Mercancia || Categoria == CategoriaProducto.MateriaPrima
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "0";
            }
        }

        public decimal CostoAdquisicionUnitario { 
            get => decimal.TryParse(fieldCostoUnitario.Text, out var value) ? value : 0m;
        }

        public decimal CostoProduccionUnitario {
            get => decimal.TryParse(fieldCostoUnitario.Text, out var value) ? value : 0m;
        }

        public decimal ImpuestoVentaPorcentaje { 
            get => decimal.TryParse(fieldImpuestoVentaPorcentaje.Text, out var value) ? value : 0m;
            set => fieldImpuestoVentaPorcentaje.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public decimal MargenGananciaDeseado { 
            get => decimal.TryParse(fieldMargenGananciaDeseado.Text, out var value) ? value : 0m;
            set => fieldMargenGananciaDeseado.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public decimal PrecioVentaBase { 
            get => decimal.TryParse(fieldPrecioVentaBase.Text, out var value) ? value : 0m;
            set => fieldPrecioVentaBase.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }
        public string NombreAlmacen {
            get => fieldNombreAlmacen.Text;
            set => fieldNombreAlmacen.Text = value;
        }

        public decimal CantidadInicial { 
            get => decimal.TryParse(fieldCantidadInicial.Text, out var value) ? value : 0m;
            set => fieldCantidadInicial.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public decimal CantidadMinima { 
            get => decimal.TryParse(fieldCantidadMinima.Text, out var value) ? value : 0m;
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
            fieldUnidadMedida.SelectedIndexChanged += ActualizarAbreviaturasUnidadMedida;
            btnRegistrarActualizar.Click += delegate (object? sender, EventArgs args) {
                if (ModoEdicion)
                    EditarEntidad?.Invoke(sender, args);
                else
                    RegistrarEntidad?.Invoke(sender, args);
            };
            btnSalir.Click += delegate (object? sender, EventArgs args) { Ocultar(); };
        }

        private void ActualizarAbreviaturasUnidadMedida(object? sender, EventArgs e) {
            if (_unidadesMedida.Length == 0)
                return;

            fieldAbreviaturaUM1.Text = _unidadesMedida[fieldUnidadMedida.SelectedIndex].Abreviatura ?? "u";
            fieldAbreviaturaUM2.Text = _unidadesMedida[fieldUnidadMedida.SelectedIndex].Abreviatura ?? "u";
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
            Nombre = string.Empty;
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

        public void CargarNombresClasificaciones(string[] nombresClasificaciones) {
            fieldClasificacionProducto.Items.Clear();
            fieldClasificacionProducto.Items.AddRange(nombresClasificaciones);
            fieldClasificacionProducto.SelectedIndex = -1;
        }

        public void CargarNombresAlmacenes(string[] nombresAlmacenes) {
            fieldNombreAlmacen.Items.Clear();
            fieldNombreAlmacen.Items.AddRange(nombresAlmacenes);
            fieldNombreAlmacen.SelectedIndex = -1;
        }
    }
}
