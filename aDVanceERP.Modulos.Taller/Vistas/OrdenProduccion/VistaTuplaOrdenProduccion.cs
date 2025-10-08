using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Modulos.Taller.Interfaces;
using aDVanceERP.Modulos.Taller.Properties;

using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Taller.Vistas.OrdenProduccion {
    public partial class VistaTuplaOrdenProduccion : Form, IVistaTuplaOrdenProduccion {
        private int _estado;

        public VistaTuplaOrdenProduccion() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaOrdenProduccion);

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
            set => layoutVista.BackColor = value;
        }
        public string Id {
            get => fieldId.Text;
            set => fieldId.Text = value;
        }

        public string NumeroOrden {
            get => fieldNumeroOrden.Text;
            set => fieldNumeroOrden.Text = value;
        }

        public string FechaApertura {
            get => fieldFechaApertura.Text;
            set => fieldFechaApertura.Text = value;
        }

        public string NombreProducto {
            get => fieldNombreProducto.Text;
            set => fieldNombreProducto.Text = value;
        }

        public string CostoTotal {
            get => fieldCostoTotal.Text;
            set => fieldCostoTotal.Text = value;
        }

        public string TotalUnidadesProducidas {
            get => fieldTotalUnidadesProducidas.Text;
            set => fieldTotalUnidadesProducidas.Text = value;
        }

        public string PrecioUnitario {
            get => fieldPrecioUnitario.Text;
            set => fieldPrecioUnitario.Text = value;
        }

        public int Estado {
            get => _estado;
            set {
                _estado = value;
                fieldEstado.Image = _estado switch {
                    0 => Resources.open_sign_20px,
                    1 => Resources.open_check_20px,
                    2 => Resources.closed_sign_20px,
                    _ => Resources.closed_sign_20px
                };
            }
        }

        public string FechaCierre {
            get => fieldFechaCierre.Text;
            set => fieldFechaCierre.Text = value;
        }
                
        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;
        
        public void Inicializar() {
            // Eventos
            btnEditar.Click += delegate (object? sender, EventArgs e) {
                EditarDatosTupla?.Invoke(this, e);
            };
            btnEliminar.Click += delegate (object? sender, EventArgs e) {
                EliminarDatosTupla?.Invoke(this, e);
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
                                    "MOD_TALLER_ORDENES_PRODUCCION_EDITAR")
                                || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                    "MOD_TALLER_ORDENES_PRODUCCION_TODOS")
                                || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_TALLER_TODOS");
            btnEliminar.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                                || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                    "MOD_TALLER_ORDENES_PRODUCCION_ELIMINAR")
                                || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                    "MOD_TALLER_ORDENES_PRODUCCION_TODOS")
                                || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_TALLER_TODOS");
        }
    }
}
