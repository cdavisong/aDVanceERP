using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Helpers.Comun;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Monedas;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Properties;

using System.Drawing.Imaging;
using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaRegistroProducto : Form, IVistaRegistroProducto {
        private bool _modoEdicion = false;
        private string _rutaImagen = string.Empty;
        private decimal _costoAdquisicionUnitario = 0;
        private decimal _costoProduccionUnitario = 0;

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
                fieldTituloNombreAlmacen.Visible = !value;
                fieldAlmacen.Visible = !value;
                layoutTituloCantidadInicialMinima.Visible = !value;
                layoutDatosCantidadInicialMinima.Visible = !value;
                layoutHabilitarAlertas.Visible = !value;
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

        public string RutaImagen { get => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "res", "imagenes", "productos", $"{Codigo}{Path.GetExtension(_rutaImagen)}"); }

        public CategoriaProductoEnum Categoria {
            get => fieldCategoriaMercancia.Checked
                ? CategoriaProductoEnum.Mercancia
                : fieldCategoriaProductoTerminado.Checked
                    ? CategoriaProductoEnum.ProductoTerminado
                    : fieldCategoriaMateriaPrima.Checked
                        ? CategoriaProductoEnum.MateriaPrima
                        : throw new ArgumentNullException();
            set {
                if (value == CategoriaProductoEnum.Mercancia)
                    fieldCategoriaMercancia.PerformClick();
                else if (value == CategoriaProductoEnum.ProductoTerminado)
                    fieldCategoriaProductoTerminado.PerformClick();
                else fieldCategoriaMateriaPrima.PerformClick();

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

        public Proveedor? Proveedor {
            get => fieldNombreProveedor.SelectedItem as Proveedor;
            set => fieldNombreProveedor.SelectedItem = value;
        }

        public string Descripcion {
            get => fieldDescripcion.Text;
            set => fieldDescripcion.Text = value;
        }

        public UnidadMedida? UnidadMedida {
            get => fieldUnidadMedida.SelectedItem as UnidadMedida;
            set => fieldUnidadMedida.SelectedItem = value;
        }

        public ClasificacionProducto? ClasificacionProducto {
            get => fieldClasificacionProducto.SelectedItem as ClasificacionProducto;
            set => fieldClasificacionProducto.SelectedItem = value;
        }

        public bool EsVendible {
            get => fieldEsVendible.Checked;
            set => fieldEsVendible.Checked = value;
        }

        public decimal CostoUnitario {
            get => Categoria == CategoriaProductoEnum.Mercancia || Categoria == CategoriaProductoEnum.MateriaPrima
                ? CostoAdquisicionUnitario
                : Categoria == CategoriaProductoEnum.ProductoTerminado
                    ? CostoProduccionUnitario
                    : 0m;
            set {
                _costoAdquisicionUnitario = Categoria == CategoriaProductoEnum.Mercancia || Categoria == CategoriaProductoEnum.MateriaPrima ? value : 0m;
                _costoProduccionUnitario = Categoria == CategoriaProductoEnum.ProductoTerminado ? value : 0m;

                fieldCostoUnitario.Text = Categoria == CategoriaProductoEnum.Mercancia || Categoria == CategoriaProductoEnum.MateriaPrima
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

        public Almacen? Almacen {
            get => fieldAlmacen.SelectedItem as Almacen;
            set => fieldAlmacen.SelectedItem = value;
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

        public Moneda? MonedaCosto {
            get => fieldMonedaCostoUnitario.SelectedItem as Moneda;
            set => fieldMonedaCostoUnitario.SelectedItem = value;
        }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            fieldImagen.Click += ObtenerImagenProducto;
            fieldCategoriaMercancia.Click += ActualizarVisibilidadCostosPorCategoria;
            fieldCategoriaProductoTerminado.Click += ActualizarVisibilidadCostosPorCategoria;
            fieldCategoriaMateriaPrima.Click += ActualizarVisibilidadCostosPorCategoria;
            fieldUnidadMedida.SelectedIndexChanged += ActualizarDescripcionUnidadMedida;
            fieldUnidadMedida.SelectedIndexChanged += ActualizarAbreviaturasUnidadMedida;
            fieldClasificacionProducto.SelectedIndexChanged += ActualizarDescripcionClasificacion;
            fieldCostoUnitario.TextChanged += ActualizarCostoUnitario;
            fieldMonedaCostoUnitario.SelectedIndexChanged += (s, e) => {
                if (fieldMonedaCostoUnitario.SelectedItem is Moneda moneda)
                    ActualizarSimboloMoneda(moneda.Simbolo);
            };
            fieldPrecioVentaBase.IconLeftClick += CalcularPrecioVenta;
            fieldMonedaPrecioVentaBase.SelectedIndexChanged += (s, e) => {
                if (fieldMonedaPrecioVentaBase.SelectedItem is Moneda moneda)
                    ActualizarSimboloMoneda(moneda.Simbolo);
            };
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
            btnSalir.Click += delegate (object? sender, EventArgs args) {
                // Evento de cancelación del registro
                AgregadorEventos.Publicar("RegistroProductoCancelado", string.Empty);

                Ocultar();
            };
        }

        private void ObtenerImagenProducto(object? sender, EventArgs e) {
            // Crear el directorio base si no existe en la ruta res/imagenes/productos
            var directorioBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "res", "imagenes", "productos");

            if (!Directory.Exists(directorioBase))
                Directory.CreateDirectory(directorioBase);

            if (fieldDialogoImagen.ShowDialog() == DialogResult.OK) {
                _rutaImagen = fieldDialogoImagen.FileName;

                Imagen = Image.FromFile(_rutaImagen);
            }
        }

        private void ActualizarVisibilidadCostosPorCategoria(object? sender, EventArgs e) {
            switch (Categoria) {
                case CategoriaProductoEnum.Mercancia:
                case CategoriaProductoEnum.MateriaPrima:
                    fieldTituloNombreProveedor.Visible = true;
                    fieldNombreProveedor.Visible = true;
                    layoutBaseDist0.RowStyles[3].Height = 25;
                    layoutBaseDist0.RowStyles[4].Height = 35;
                    fieldTituloCostoUnitario.Text = "COSTO DE ADQUISICIÓN";
                    break;
                case CategoriaProductoEnum.ProductoTerminado:
                    fieldTituloNombreProveedor.Visible = false;
                    fieldNombreProveedor.Visible = false;
                    layoutBaseDist0.RowStyles[3].Height = 0;
                    layoutBaseDist0.RowStyles[4].Height = 0;
                    fieldTituloCostoUnitario.Text = "COSTO DE PRODUCCIÓN";
                    break;
                default:
                    break;
            }
        }

        private void ActualizarDescripcionUnidadMedida(object? sender, EventArgs e) {
            if (fieldUnidadMedida.Items.Count == 0 || fieldUnidadMedida.SelectedIndex < 0)
                return;

            toolTip2.SetToolTip(fieldUnidadMedida, fieldUnidadMedida.SelectedItem is UnidadMedida unidad
                ? unidad.Descripcion
                : string.Empty);
        }

        private void ActualizarAbreviaturasUnidadMedida(object? sender, EventArgs e) {
            if (fieldUnidadMedida.Items.Count == 0 || fieldUnidadMedida.SelectedIndex < 0)
                return;

            fieldAbreviaturaUM1.Text = fieldUnidadMedida.SelectedItem is UnidadMedida unidad1
                ? unidad1.Abreviatura ?? "u"
                : "u";
            fieldAbreviaturaUM2.Text = fieldUnidadMedida.SelectedItem is UnidadMedida unidad2
                ? unidad2.Abreviatura ?? "u"
                : "u";
        }

        private void ActualizarDescripcionClasificacion(object? sender, EventArgs e) {
            if (fieldClasificacionProducto.Items.Count == 0 || fieldClasificacionProducto.SelectedIndex < 0)
                return;

            toolTip3.SetToolTip(fieldClasificacionProducto, fieldClasificacionProducto.SelectedItem is ClasificacionProducto clasificacion
                ? clasificacion.Descripcion
                : string.Empty);
        }

        private void ActualizarCostoUnitario(object? sender, EventArgs e) {
            _costoAdquisicionUnitario = Categoria == CategoriaProductoEnum.Mercancia || Categoria == CategoriaProductoEnum.MateriaPrima 
                ? decimal.TryParse(fieldCostoUnitario.Text, CultureInfo.InvariantCulture, out var valueAdq) 
                    ? valueAdq 
                    : 0m 
                : 0m;
            _costoProduccionUnitario = Categoria == CategoriaProductoEnum.ProductoTerminado 
                ? decimal.TryParse(fieldCostoUnitario.Text, CultureInfo.InvariantCulture, out var valueProd) 
                    ? valueProd 
                    : 0m 
                : 0m;
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
            Categoria = CategoriaProductoEnum.Mercancia;
            NombreProducto = string.Empty;
            Codigo = string.Empty;
            Proveedor = null;
            Descripcion = string.Empty;
            UnidadMedida = null;
            ClasificacionProducto = null;
            Almacen = null;
            EsVendible = true;
            fieldCostoUnitario.Text = string.Empty;
            if (fieldMonedaCostoUnitario.Items.Count > 0)
                fieldMonedaCostoUnitario.SelectedIndex = 0;
            if (fieldMonedaPrecioVentaBase.Items.Count > 0)
                fieldMonedaPrecioVentaBase.SelectedIndex = 0;
            fieldImpuestoVentaPorcentaje.Text = string.Empty;
            fieldMargenGananciaDeseado.Text = string.Empty;
            fieldPrecioVentaBase.Text = string.Empty;
            fieldCantidadInicial.Text = string.Empty;
            fieldCantidadMinima.Text = string.Empty;
        }

        public void Cerrar() {
            Dispose();
        }

        public void SalvarImagenEnDirectorioLocal() {
            if (string.IsNullOrEmpty(_rutaImagen) || Imagen == null)
                return;

            if (File.Exists(RutaImagen))
                File.Delete(RutaImagen);

            // Convertir la imagen original del producto a un formato compatible con el guardado (por ejemplo, JPEG o PNG)
            var formatoImagen = Path.GetExtension(_rutaImagen).ToLower() switch {
                ".jpg" or ".jpeg" => ImageFormat.Jpeg,
                ".png" => ImageFormat.Png,
                _ => ImageFormat.Png
            };

            // Cargar la imagen sin bloquear el archivo y guardarla en la ruta destino
            using (var bitmap = CargarBitmapSinBloquear(_rutaImagen)) {
                bitmap.Save(RutaImagen, formatoImagen);
            }
        }

        // Método auxiliar que carga un Bitmap desde archivo sin bloquear el archivo en disco.
        // Lee todos los bytes, crea un MemoryStream, obtiene una Image desde el stream y
        // devuelve un nuevo Bitmap copiado en memoria. Esto permite cerrar el stream y
        // liberar el archivo original inmediatamente.
        private static Bitmap CargarBitmapSinBloquear(string ruta) {
            var bytes = File.ReadAllBytes(ruta);
            using (var ms = new MemoryStream(bytes)) {
                using (var img = Image.FromStream(ms)) {
                    return new Bitmap(img);
                }
            }
        }

        public void CargarProveedores(Proveedor[] proveedores) {
            fieldTituloNombreProveedor.Visible = true;
            fieldNombreProveedor.Visible = true;
            layoutBaseDist0.RowStyles[3].Height = 25;
            layoutBaseDist0.RowStyles[4].Height = 35;

            fieldNombreProveedor.Items.Clear();
            fieldNombreProveedor.Items.AddRange(proveedores);
            fieldNombreProveedor.SelectedIndex = -1;
        }

        public void CargarUnidadesMedida(UnidadMedida[] unidadesMedida) {
            fieldUnidadMedida.Items.Clear();
            fieldUnidadMedida.Items.AddRange(unidadesMedida);
            fieldUnidadMedida.SelectedIndex = unidadesMedida.Length > 0 ? 0 : -1;
        }

        public void CargarClasificaciones(ClasificacionProducto[] clasificaciones) {
            fieldClasificacionProducto.Items.Clear();
            fieldClasificacionProducto.Items.AddRange(clasificaciones);
            fieldClasificacionProducto.SelectedIndex = -1;
        }

        public void CargarMonedas(Moneda[] monedas) {
            fieldMonedaCostoUnitario.Items.Clear();
            fieldMonedaCostoUnitario.Items.AddRange(monedas);

            fieldMonedaPrecioVentaBase.Items.Clear();
            fieldMonedaPrecioVentaBase.Items.AddRange(monedas);

            // Preseleccionar la moneda base
            var monedaBase = monedas.FirstOrDefault(m => m.EsBase);

            if (monedaBase != null) {
                fieldMonedaCostoUnitario.SelectedItem = monedaBase;
                fieldMonedaPrecioVentaBase.SelectedItem = monedaBase;
            } else if (monedas.Length > 0) {
                fieldMonedaCostoUnitario.SelectedIndex = 0;
                fieldMonedaPrecioVentaBase.SelectedIndex = 0;
            }
        }

        public void ActualizarSimboloMoneda(string simbolo) {
            // fieldSimboloMoneda es un Label decorativo junto al TextBox de costo.
            // Nombre real a ajustar según el Designer del proyecto.
            //if (fieldSimboloMoneda != null)
            //    fieldSimboloMoneda.Text = simbolo;
        }

        public void CargarAlmacenes(Almacen[] almacenes) {
            fieldAlmacen.Items.Clear();
            fieldAlmacen.Items.AddRange(almacenes);
            fieldAlmacen.SelectedIndex = -1;
        }
    }
}
