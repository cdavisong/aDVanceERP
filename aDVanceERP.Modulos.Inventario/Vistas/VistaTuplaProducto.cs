using System.Globalization;

using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaTuplaProducto : Form, IVistaTuplaProducto {
        private string? _nombreAlmacen;

        public VistaTuplaProducto() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaProducto);

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

        public long Id {
            get => Convert.ToInt64(fieldId.Text);
            set => fieldId.Text = value.ToString();
        }

        public string NombreAlmacen {
            get => _nombreAlmacen ?? string.Empty;
            set => _nombreAlmacen = string.IsNullOrEmpty(value) ? "-" : value;
        }

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

        public string NombreProducto {
            get => fieldNombre.Text;
            set { 
                fieldNombre.Text = value;
                fieldNombre.Margin = fieldNombre.AjusteAutomaticoMargenTexto();
            }
        }

        public string Descripcion {
            get => fieldDescripcion.Text;
            set {
                fieldDescripcion.Text = value;
                fieldDescripcion.Margin = fieldDescripcion.AjusteAutomaticoMargenTexto();
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

        public string UnidadMedida {
            get => fieldUnidadMedida.Text;
            set => fieldUnidadMedida.Text = value;
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

        public event EventHandler? MovimientoPositivoStock;
        public event EventHandler? MovimientoNegativoStock;
        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;
    
        public void Inicializar() {
            // Eventos        
            btnMovimientoPositivo.Click += delegate (object? sender, EventArgs e) {
                MovimientoPositivoStock?.Invoke(NombreAlmacen, e);
            };
            btnMovimientoNegativo.Click += delegate (object? sender, EventArgs e) {
                MovimientoNegativoStock?.Invoke(NombreAlmacen, e);
            };
            btnEditar.Click += delegate (object? sender, EventArgs e) {
                EditarDatosTupla?.Invoke(new object[] { NombreAlmacen }, e);
            };
            btnEliminar.Click += async delegate (object? sender, EventArgs e) {
                if (RepoMovimiento.Instancia.Buscar(Core.Modelos.Modulos.Inventario.FiltroBusquedaMovimiento.Producto, NombreProducto).cantidad > 0)
                    EliminarDatosTupla?.Invoke(this, e);
                else
                    CentroNotificaciones.MostrarNotificacion(
                        $"No se puede eliminar el producto {NombreProducto}, existen registros de movimientos asociados al mismo y podría dañar la integridad y trazabilidad de los datos.",
                        TipoNotificacion.Advertencia);
            };
        }

        public void Mostrar() {
            VerificarPermisos();
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

        private void VerificarPermisos() {
            if (ContextoSeguridad.UsuarioAutenticado == null || ContextoSeguridad.PermisosUsuario == null) {
                btnMovimientoPositivo.Enabled = false;
                btnMovimientoNegativo.Enabled = false;
                btnEditar.Enabled = false;
                return;
            }

            btnMovimientoPositivo.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                                "MOD_INVENTARIO_MOVIMIENTOS_ADICIONAR")
                                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                                "MOD_INVENTARIO_MOVIMIENTOS_TODOS")
                                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                                "MOD_INVENTARIO_TODOS");
            btnMovimientoNegativo.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                                "MOD_INVENTARIO_MOVIMIENTOS_ADICIONAR")
                                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                                "MOD_INVENTARIO_MOVIMIENTOS_TODOS")
                                            || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                                "MOD_INVENTARIO_TODOS");
            btnEditar.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                                || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                    "MOD_INVENTARIO_PRODUCTOS_EDITAR")
                                || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                    "MOD_INVENTARIO_PRODUCTOS_TODOS")
                                || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
        }
    }
}