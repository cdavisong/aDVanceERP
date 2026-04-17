using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaTuplaProducto : Form, IVistaTuplaProducto {
        private int _presentaciones;
        private UnidadMedida? _unidadMedida;

        public VistaTuplaProducto() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaProducto);

            Inicializar();
        }

        public string NombreVista {
            get => $"{Name}{Id}{Codigo}";
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

        public long Id {
            get => Convert.ToInt64(fieldId.Text);
            set => fieldId.Text = value.ToString();
        }

        public Almacen? Almacen { get; set; }

        public string Codigo {
            get => fieldCodigo.Text;
            set => fieldCodigo.Text = value;
        }

        public DateTime FechaUltimoMovimiento {
            get => fieldFechaUltimoMovimiento.Text.Equals("-")
                ? DateTime.MinValue
                : DateTime.ParseExact(fieldFechaUltimoMovimiento.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            set => fieldFechaUltimoMovimiento.Text = value.Equals(DateTime.MinValue)
                ? "-"
                : value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public string NombreDescripcion {
            get => fieldNombreDescripcion.Text;
            set {
                fieldNombreDescripcion.Text = value;
                fieldNombreDescripcion.Margin = fieldNombreDescripcion.AjusteAutomaticoMargenTexto();
            }
        }

        public decimal CostoUnitario {
            get => decimal.TryParse(fieldCostoUnitario.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                out var value)
                ? value
                : 0m;
            set => fieldCostoUnitario.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public decimal PrecioVentaBase {
            get => decimal.TryParse(fieldPrecioVentaBase.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                out var value)
                ? value
                : 0m;
            set => fieldPrecioVentaBase.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public int Presentaciones {
            get => _presentaciones;
            set {
                _presentaciones = value;

                var (borde, fondo, fuente) = ObtenerColorPresentacion(value > 0);

                fieldPresentaciones.Text = value > 0 ? $"{value} presentaciones" : "+ Sin presentaciones";
                fieldPresentaciones.BorderColor = borde;
                fieldPresentaciones.FillColor = fondo;
                fieldPresentaciones.ForeColor = fuente;
            }
        }

        public UnidadMedida? UnidadMedida {
            get => _unidadMedida;
            set { 
                _unidadMedida = value;

                fieldUnidadMedida.Text = value.Abreviatura; 
            }
        }

        public decimal Stock {
            get => decimal.TryParse(fieldStock.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                out var value)
                ? value
                : 0;
            set {
                fieldStock.ForeColor = value == 0 ? Color.Firebrick : Color.FromArgb(115, 109, 106);
                fieldStock.Font = new Font(fieldStock.Font, value == 0 ? FontStyle.Bold : FontStyle.Regular);
                fieldStock.Text = value.ToString("N2", CultureInfo.InvariantCulture);
            }
        }

        public event EventHandler? GestionarPresentaciones;
        public event EventHandler<Almacen>? MovimientoPositivoStock;
        public event EventHandler<Almacen>? MovimientoNegativoStock;
        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;

        public void Inicializar() {
            // Eventos
            fieldPresentaciones.Click += delegate {
                GestionarPresentaciones?.Invoke(new object[] { Id, Almacen }, EventArgs.Empty);
            };
            btnMovimientoPositivo.Click += delegate (object? sender, EventArgs e) {
                MovimientoPositivoStock?.Invoke(Id, Almacen);
            };
            btnMovimientoNegativo.Click += delegate (object? sender, EventArgs e) {
                MovimientoNegativoStock?.Invoke(Id, Almacen);
            };
            btnEditar.Click += delegate (object? sender, EventArgs e) {
                EditarDatosTupla?.Invoke(new object[] { Id, Almacen }, e);
            };
            btnEliminar.Click += async delegate (object? sender, EventArgs e) {
                if (RepoMovimiento.Instancia.Buscar(FiltroBusquedaMovimiento.IdProducto, Id.ToString()).cantidad > 0)
                    EliminarDatosTupla?.Invoke(this, e);
                else
                    CentroNotificaciones.MostrarNotificacion(
                        $"No se puede eliminar el producto, existen registros de movimientos asociados al mismo y podría dañar la integridad y trazabilidad de los datos.",
                        TipoNotificacionEnum.Advertencia);
            };
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
            ColorFondoTupla = BackColor;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }

        private (Color borde, Color fondo, Color fuente) ObtenerColorPresentacion(bool estado) {
            return estado
                ? (Color.FromArgb(253, 224, 196), Color.FromArgb(255, 248, 242), Color.FromArgb(232, 149, 74))  // Naranja
                : (Color.FromArgb(228, 228, 228), Color.FromArgb(240, 240, 240), Color.FromArgb(136, 136, 136));// Gris
        }
    }
}