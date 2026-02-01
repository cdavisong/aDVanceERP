using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Properties;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaTuplaMovimiento : Form, IVistaTuplaMovimiento {
        private EstadoMovimiento _estadoMovimiento;

        public VistaTuplaMovimiento() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaMovimiento);

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
            : ObtenerColorFondoTupla(EstadoMovimiento);
        }

        public bool EstadoSeleccion { get; set; }

        public string Id {
            get => fieldId.Text;
            set => fieldId.Text = value;
        }

        public string NombreProducto {
            get => fieldNombreProducto.Text;
            set => fieldNombreProducto.Text = value;
        }

        public string NombreAlmacenOrigen {
            get => fieldNombreAlmacenOrigen.Text;
            set {
                fieldNombreAlmacenOrigen.Text = string.IsNullOrEmpty(value) ? "Ninguno" : value;
                fieldNombreAlmacenOrigen.Margin = new Padding(1, value?.Length > 16 ? 10 : 1, 1, 1);
            }
        }

        public string NombreAlmacenDestino {
            get => fieldNombreAlmacenDestino.Text;
            set {
                fieldNombreAlmacenDestino.Text = string.IsNullOrEmpty(value) ? "Ninguno" : value;
                fieldNombreAlmacenDestino.Margin = new Padding(1, value?.Length > 16 ? 10 : 1, 1, 1);
            }
        }

        public string SaldoInicial {
            get => fieldSaldoInicial.Text;
            set => fieldSaldoInicial.Text = value;
        }

        public string CantidadMovida {
            get => fieldCantidadMovida.Text;
            set => fieldCantidadMovida.Text = value;
        }

        public string SaldoFinal {
            get => fieldSaldoFinal.Text;
            set => fieldSaldoFinal.Text = value;
        }

        public string TipoMovimiento {
            get => fieldTipoMovimiento.Text;
            set {
                fieldTipoMovimiento.Text = string.IsNullOrEmpty(value) ? "ERROR" : value;
                fieldTipoMovimiento.Margin = new Padding(1, value?.Length > 23 ? 10 : 1, 1, 1);
            }
        }

        public string Fecha {
            get => fieldFecha.Text;
            set => fieldFecha.Text = value;
        }

        public EstadoMovimiento EstadoMovimiento {
            get => _estadoMovimiento;
            set {
                _estadoMovimiento = value;
                layoutVista.BackColor = ObtenerColorFondoTupla(value);
            }
        }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;
    
        public void Inicializar() {
            // Eventos
            btnEditar.Click += delegate(object? sender, EventArgs e) { EditarDatosTupla?.Invoke(this, e); };
            btnEliminar.Click += delegate(object? sender, EventArgs e) { EliminarDatosTupla?.Invoke(this, e); };
        }

        public void ActualizarIconoStock(EfectoMovimiento efecto) {
            fieldIcono.BackgroundImage = efecto switch {
                EfectoMovimiento.Carga => Resources.load_cargo_20px,
                EfectoMovimiento.Descarga => Resources.unload_cargo_20px,
                EfectoMovimiento.Transferencia => Resources.transfer_20px,
                _ => fieldIcono.BackgroundImage
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
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                return;
            }

            btnEditar.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                                || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                    "MOD_INVENTARIO_MOVIMIENTOS_EDITAR")
                                || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                    "MOD_INVENTARIO_MOVIMIENTOS_TODOS")
                                || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
            btnEliminar.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                                  || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                      "MOD_INVENTARIO_MOVIMIENTOS_ELIMINAR")
                                  || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                      "MOD_INVENTARIO_MOVIMIENTOS_TODOS")
                                  || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
        }

        private Color ObtenerColorFondoTupla(EstadoMovimiento estado) {
            return estado switch {
                EstadoMovimiento.Pendiente => ContextoAplicacion.ColorAdvertenciaTupla,
                EstadoMovimiento.Cancelado => ContextoAplicacion.ColorErrorTupla,
                _ => BackColor
            };
        }
    }
}