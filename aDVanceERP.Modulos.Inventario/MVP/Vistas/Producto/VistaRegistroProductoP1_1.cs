using aDVanceERP.Core.Modelos.Modulos.Inventario;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto {
    public partial class VistaRegistroProductoP1_1 : Form {
        private CategoriaProducto _categoriaProducto;

        public VistaRegistroProductoP1_1() {
            InitializeComponent();
            Inicializar();
        }

        public event EventHandler? EsVendibleActualizado;

        public CategoriaProducto CategoriaProducto {
            get => _categoriaProducto;
            set {
                _categoriaProducto = value;

                separador1.Visible = value == CategoriaProducto.MateriaPrima;
                layoutEsVendible.Visible = value == CategoriaProducto.MateriaPrima;
            }
        }

        public string RazonSocialProveedor {
            get => fieldNombreProveedor.Text;
            set => fieldNombreProveedor.Text = value;
        }

        public bool EsVendible {
            get => fieldEsVendible.Checked;
            set => fieldEsVendible.Checked = value;
        }

        private void Inicializar() {
            // Eventos
            fieldEsVendible.CheckedChanged += delegate (object? sender, EventArgs args) {
                if (CategoriaProducto == CategoriaProducto.MateriaPrima)
                    EsVendibleActualizado?.Invoke(this, EventArgs.Empty);
            };
        }

        public void CargarRazonesSocialesProveedores(object[] nombresProveedores) {
            fieldNombreProveedor.Items.Clear();
            fieldNombreProveedor.Items.Add("Ninguno");
            fieldNombreProveedor.Items.AddRange(nombresProveedores);
            fieldNombreProveedor.SelectedIndex = nombresProveedores.Length > 0 ? 0 : -1;
        }

        public void Restaurar() {
            RazonSocialProveedor = string.Empty;
            fieldNombreProveedor.SelectedIndex = 0;
        }
    }
}
