using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Modulos.Venta.Interfaces;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;

using System.Globalization;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Inventario;

namespace aDVanceERP.Modulos.Venta.Vistas {
    // TODO: Implementar los botones de cancelar la venta e imprimir la factura, recordar que una venta se cancela solo cuando tiene un estado distinto a completada
    public partial class VistaTuplaVenta : Form, IVistaTuplaVenta {
        private EstadoVenta _estadoVenta;

        public VistaTuplaVenta() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaVenta);

            Inicializar();
        }

        public string NombreVista {
            get => $"{Name}{Id}";
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
            set => layoutVista.BackColor = value == Color.Gainsboro 
                ? value 
                : ObtenerColorFondoTupla(EstadoVenta);
        }

        public bool EstadoSeleccion { get; set; }

        public long Id { get; set; }

        public string NumeroFacturaVenta {
            get => fieldNumeroFactura.Text;
            set => fieldNumeroFactura.Text = value;
        }

        public DateTime FechaVenta {
            get => fieldFechaVenta.Text.Equals("-")
                    ? DateTime.MinValue
                    : DateTime.ParseExact(fieldFechaVenta.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            set => fieldFechaVenta.Text = value.Equals(DateTime.MinValue)
                ? "-"
                : value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public string NombreCliente { get; set; }

        public string? MetodoPagoPrincipal {
            get => fieldMetodoPagoPrincipal.Text;
            set { 
                fieldMetodoPagoPrincipal.Text = value;
                fieldMetodoPagoPrincipal.Margin = fieldMetodoPagoPrincipal.AjusteAutomaticoMargenTexto();
            }
        }

        public decimal TotalBruto {
            get => decimal.TryParse(fieldTotalBruto.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                        out var value)
                        ? value
                        : 0m;
            set => fieldTotalBruto.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public decimal DescuentoTotal {
            get => decimal.TryParse(fieldDescuentoTotal.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                            out var value)
                            ? value
                            : 0m;
            set => fieldDescuentoTotal.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public decimal ImpuestoTotal {
            get => decimal.TryParse(fieldImpuestoTotal.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                                out var value)
                                ? value
                                : 0m;
            set => fieldImpuestoTotal.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public decimal ImporteTotal {
            get => decimal.TryParse(fieldImporteTotal.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                                    out var value)
                                    ? value
                                    : 0m;
            set => fieldImporteTotal.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public EstadoVenta EstadoVenta {
            get => _estadoVenta;
            set {
                _estadoVenta = value;
                fieldEstado.Text = value.ObtenerDisplayName();
                btnVerFactura.Visible = value == EstadoVenta.Completada;
                btnAnular.Enabled = value == EstadoVenta.Pendiente || value == EstadoVenta.Entregada;
                layoutVista.BackColor = ObtenerColorFondoTupla(value);
            }
        }

        public bool Activo { get; set; }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;

        public void Inicializar() {
            // Eventos
            btnAnular.Click += delegate (object? sender, EventArgs e) {
                RepoVenta.Instancia.CambiarEstadoVenta(Id, EstadoVenta.Anulada);
                EstadoVenta = EstadoVenta.Anulada;

                // Crear movimiento de inventario
                var venta = RepoVenta.Instancia.ObtenerPorId(Id);
                var detallesVenta = RepoDetalleVentaProducto.Instancia.ObtenerDetallesConProducto(venta!.Id);

                foreach (var detalleVenta in detallesVenta) {
                    var producto = RepoProducto.Instancia.ObtenerPorId(detalleVenta.IdProducto);
                    var inventarioProducto = RepoInventario.Instancia.Buscar(FiltroBusquedaInventario.IdProducto, producto!.Id.ToString()).resultadosBusqueda.FirstOrDefault(p => p.entidadBase.IdAlmacen.Equals(venta.IdAlmacen)).entidadBase;
                    var movimiento = new Movimiento() {
                        Id = 0,
                        IdProducto = producto!.Id,
                        CostoUnitario = producto.Categoria == CategoriaProducto.ProductoTerminado ? producto.CostoProduccionUnitario : producto.CostoAdquisicionUnitario,
                        IdAlmacenOrigen = 0,
                        IdAlmacenDestino = venta.IdAlmacen,
                        Estado = EstadoMovimiento.Completado,
                        FechaCreacion = DateTime.Now,
                        SaldoInicial = inventarioProducto.Cantidad,
                        FechaTermino = DateTime.Now,
                        CantidadMovida = detalleVenta.Cantidad,
                        SaldoFinal = inventarioProducto.Cantidad + detalleVenta.Cantidad,
                        IdTipoMovimiento = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Nombre, "Devolución de Venta").resultadosBusqueda.FirstOrDefault().entidadBase?.Id ?? 0,
                        IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                        Notas = "Devolución para una venta de producto.",
                    };

                    // Adicionar a la base de datos local
                    RepoMovimiento.Instancia.Adicionar(movimiento);

                    // Modificar inventario
                    RepoInventario.Instancia.ModificarInventario(
                        producto!.Id,
                        0,
                        venta.IdAlmacen,
                        detalleVenta.Cantidad);
                }
            
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

        private Color ObtenerColorFondoTupla(EstadoVenta estado) {
            if (!Activo)
                return BackColor;

            return estado switch { 
                EstadoVenta.Pendiente => ContextoAplicacion.ColorAdvertenciaTupla,
                EstadoVenta.Anulada => ContextoAplicacion.ColorErrorTupla, 
                EstadoVenta.Entregada => ContextoAplicacion.ColorAdvertenciaTupla, 
                _ => BackColor
            };
        }
    }
}