using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Utiles.Datos;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto {
    public partial class VistaRegistroProductoP1 : Form {
        private VistaRegistroProductoP1_1 P1RepoProveedorVentaDirecta = new VistaRegistroProductoP1_1();

        public VistaRegistroProductoP1() {
            InitializeComponent();
            InicializarVistas();
            Inicializar();

            CargarCategoriasProducto();
        }

        public event EventHandler? CategoriaProductoCambiada;
        public event EventHandler? EsVendibleActualizado;

        public CategoriaProducto CategoriaProducto {
            get => (CategoriaProducto)fieldCategoriaProducto.SelectedIndex;
            set {
                fieldCategoriaProducto.SelectedIndex = (int)value;
                fieldDescripcionCategoriaProducto.Text = UtilesCategoriaProducto.DescripcionesProducto[(int)value];
            }
        }

        public string Nombre {
            get => fieldNombreProducto.Text;
            set => fieldNombreProducto.Text = value;
        }

        public string Codigo {
            get => fieldCodigo.Text;
            set => fieldCodigo.Text = value;
        }

        public string Descripcion {
            get => fieldDescripcion.Text;
            set => fieldDescripcion.Text = value;
        }

        public string RazonSocialProveedor {
            get => P1RepoProveedorVentaDirecta.RazonSocialProveedor;
            set => P1RepoProveedorVentaDirecta.RazonSocialProveedor = value;
        }

        public bool EsVendible {
            get => P1RepoProveedorVentaDirecta.EsVendible;
            set => P1RepoProveedorVentaDirecta.EsVendible = value;
        }

        private void InicializarVistas() {
            // 1. Datos del proveedor y venta directa de materia prima
            P1RepoProveedorVentaDirecta.Dock = DockStyle.Fill;
            P1RepoProveedorVentaDirecta.TopLevel = false;

            contenedorVistas.Controls.Clear();
            contenedorVistas.Controls.Add(P1RepoProveedorVentaDirecta);
        }

        private void Inicializar() {
            // Eventos
            fieldCategoriaProducto.SelectedIndexChanged += delegate (object? sender, EventArgs args) {
                fieldDescripcionCategoriaProducto.Text = UtilesCategoriaProducto.DescripcionesProducto[fieldCategoriaProducto.SelectedIndex];

                // Actualizar visibilidad de campos según la categoría seleccionada
                P1RepoProveedorVentaDirecta.Visible =
                    CategoriaProducto == CategoriaProducto.Mercancia ||
                    CategoriaProducto == CategoriaProducto.MateriaPrima;
                P1RepoProveedorVentaDirecta.CategoriaProducto = CategoriaProducto;

                CategoriaProductoCambiada?.Invoke(this, EventArgs.Empty);

                // Actualizar la propiedad vendible del producto
                EsVendible = CategoriaProducto == CategoriaProducto.Mercancia || 
                    CategoriaProducto == CategoriaProducto.ProductoTerminado;
            };
            fieldNombreProducto.TextChanged += delegate (object? sender, EventArgs args) {
                //TODO: Si el producto existe, popular sus datos
                
            };
            btnGenerarCodigo.Click += delegate (object? sender, EventArgs args) {
                if (string.IsNullOrEmpty(Nombre))
                    CentroNotificaciones.Mostrar("Debe especificar un nombre único para el producto antes de generar un nuevo código de barras. Llene los campos correspondientes y presione nuevaente el botón.", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
                else
                    Codigo = UtilesCodigoBarras.GenerarEan13(Nombre);
            };
            P1RepoProveedorVentaDirecta.EsVendibleActualizado += delegate (object? sender, EventArgs args) {
                EsVendibleActualizado?.Invoke(this, EventArgs.Empty);
            };
        }

        private void CargarCategoriasProducto() {
            fieldCategoriaProducto.Items.Clear();
            fieldCategoriaProducto.Items.AddRange(UtilesCategoriaProducto.CategoriaProducto);
            fieldCategoriaProducto.SelectedIndex = 0;
            fieldDescripcionCategoriaProducto.Text = UtilesCategoriaProducto.DescripcionesProducto[0];
        }

        public void CargarNombresProductos(string[] nombresProductos) {
            fieldNombreProducto.AutoCompleteCustomSource.Clear();
            fieldNombreProducto.AutoCompleteCustomSource.AddRange(nombresProductos);
            fieldNombreProducto.AutoCompleteMode = AutoCompleteMode.Suggest;
            fieldNombreProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public void CargarRazonesSocialesProveedores(object[] nombresProveedores) {
            P1RepoProveedorVentaDirecta.CargarRazonesSocialesProveedores(nombresProveedores);
        }

        public void Restaurar() {
            CategoriaProducto = CategoriaProducto.Mercancia;
            Nombre = string.Empty;
            Codigo = string.Empty;
            Descripcion = string.Empty;

            P1RepoProveedorVentaDirecta.Restaurar();
        }
    }
}
