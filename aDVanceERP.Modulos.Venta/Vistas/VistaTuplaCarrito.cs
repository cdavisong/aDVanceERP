using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Venta.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaTuplaCarrito : Form, IVistaTuplaCarrito {
        private PrecioPresentacion[] _presentacionesVenta = null!;
        private UnidadMedida? _unidadMedida = null!;

        public VistaTuplaCarrito() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaCarrito);

            Inicializar();
        }

        public string NombreVista {
            get => $"{Name}{Codigo}";
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

        public Color ColorFondoTupla {
            get => layoutVista.BackColor;
            set => layoutVista.BackColor = value;
        }

        public bool EstadoSeleccion { get; set; }

        public long IdProducto { get; set; }

        public string Codigo {
            get => fieldCodigo.Text;
            set => fieldCodigo.Text = value;
        }

        public string NombreProducto {
            get => fieldNombreProducto.Text;
            set => fieldNombreProducto.Text = value;
        }

        public decimal PrecioUnitario {
            get => decimal.TryParse(fieldCostoGeneral.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                        out var value)
                        ? value
                        : 0m;
            set => fieldCostoGeneral.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public decimal Cantidad {
            get => decimal.TryParse(fieldCantidad.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                            out var value)
                            ? value
                            : 0m;
            set => fieldCantidad.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public UnidadMedida? UnidadMedida {
            get => _unidadMedida;
            set {
                _unidadMedida = value;

                var (borde, fondo, fuente) = ObtenerColorUnidadMedida(_presentacionesVenta != null && _presentacionesVenta.Length > 0);

                fieldUnidadMedida.BorderColor = borde;
                fieldUnidadMedida.FillColor = fondo;
                fieldUnidadMedida.ForeColor = fuente;
            }
        }

        public long IdPresentacion { get; internal set; }

        public decimal Descuento { get; set; }

        public decimal ImpuestoAdicional { get; set; }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;

        public void Inicializar() {
            // Eventos
            fieldUnidadMedida.SelectedIndexChanged += delegate (object? sender, EventArgs e) {
                var producto = RepoProducto.Instancia.ObtenerPorId(IdProducto)!;
                var unidadMedidaProducto = RepoUnidadMedida.Instancia.ObtenerPorId(producto.IdUnidadMedida);
                var unidadMedidaPresentacion = fieldUnidadMedida.SelectedIndex > 0
                    ? RepoUnidadMedida.Instancia.ObtenerPorId(_presentacionesVenta[fieldUnidadMedida.SelectedIndex - 1].IdUnidadMedida)
                    : null;

                PrecioUnitario = fieldUnidadMedida.SelectedIndex == 0
                    ? producto.PrecioVentaBase
                    : _presentacionesVenta[fieldUnidadMedida.SelectedIndex - 1].PrecioVenta;
                UnidadMedida = fieldUnidadMedida.SelectedIndex == 0
                    ? unidadMedidaProducto
                    : unidadMedidaPresentacion;
                IdPresentacion = fieldUnidadMedida.SelectedIndex > 0
                    ? _presentacionesVenta[fieldUnidadMedida.SelectedIndex - 1].Id
                    : 0;

                // MOSTRAR INFORMACIÓN DE LA PRESENTACIÓN
                decimal cantidadPorPresentacion = fieldUnidadMedida.SelectedIndex > 0
                    ? _presentacionesVenta[fieldUnidadMedida.SelectedIndex - 1].Cantidad
                    : 1m;

                // Actualizar tooltip o label informativo
                toolTipPresentacion.SetToolTip(fieldUnidadMedida,
                    $"{cantidadPorPresentacion} {unidadMedidaProducto.Abreviatura} por presentación");

                EditarDatosTupla?.Invoke(this, e);
            };
            btnEliminar.Click += delegate (object? sender, EventArgs e) { EliminarDatosTupla?.Invoke(this, e); };
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }

        public void CargarPresentacionesVenta(PrecioPresentacion[] presentaciones) {
            _presentacionesVenta = presentaciones;

            fieldUnidadMedida.Items.Clear();
            fieldUnidadMedida.Items.Add(UnidadMedida?.Abreviatura ?? "u");

            var repoUnidadMedida = RepoUnidadMedida.Instancia;

            foreach (var presentacion in presentaciones) {
                var unidadMedida = repoUnidadMedida.ObtenerPorId(presentacion.IdUnidadMedida);

                if (unidadMedida == null)
                    continue;

                fieldUnidadMedida.Items.Add(unidadMedida.Abreviatura);
            }

            fieldUnidadMedida.SelectedIndex = 0;
        }

        public bool ValidarStockParaPresentacionSeleccionada(long idAlmacen) {
            if (IdProducto == 0) return false;

            var producto = RepoProducto.Instancia.ObtenerPorId(IdProducto);
            if (producto == null) return false;

            // Obtener presentación seleccionada
            PrecioPresentacion? presentacionSeleccionada = null;
            if (fieldUnidadMedida.SelectedIndex > 0 && _presentacionesVenta != null) {
                presentacionSeleccionada = _presentacionesVenta[fieldUnidadMedida.SelectedIndex - 1];
            }

            // Calcular cantidad total en unidades base
            decimal unidadesPorPresentacion = presentacionSeleccionada?.Cantidad ?? 1m;
            decimal cantidadTotalUnidades = Cantidad * unidadesPorPresentacion;

            // Obtener stock disponible
            var disponibilidad = RepoProducto.Instancia.ObtenerDisponibilidadProducto(IdProducto, idAlmacen);

            return disponibilidad.disponible >= cantidadTotalUnidades;
        }

        private (Color borde, Color fondo, Color fuente) ObtenerColorUnidadMedida(bool estado) {
            return estado
                ? (Color.FromArgb(253, 224, 196), Color.FromArgb(255, 248, 242), Color.FromArgb(232, 149, 74))  // Naranja
                : (Color.FromArgb(228, 228, 228), Color.FromArgb(240, 240, 240), Color.FromArgb(136, 136, 136));// Gris
        }

        private void simboloPeso1_Click(object sender, EventArgs e) {

        }
    }
}