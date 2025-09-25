using aDVanceERP.Core.Seguridad.Utiles;
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

        public event EventHandler? TuplaSeleccionada;
        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;
        

        public void Inicializar() {
            // Eventos
            foreach (var control in layoutVista.Controls) {
                if (control is Guna2CircleButton || control is Guna2Button)
                    continue;

                ((Control) control).Click += OnSeleccionTupla;
            }

            btnEditar.Click += delegate (object? sender, EventArgs e) {
                EditarDatosTupla?.Invoke(this, e);
            };
            btnEliminar.Click += delegate (object? sender, EventArgs e) {
                EliminarDatosTupla?.Invoke(this, e);
            };
        }

        private void OnSeleccionTupla(object? sender, EventArgs e) {
            TuplaSeleccionada?.Invoke(this, e);
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
            if (UtilesCuentaUsuario.UsuarioAutenticado == null || UtilesCuentaUsuario.PermisosUsuario == null) {
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                return;
            }

            btnEditar.Enabled = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                                || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                    "MOD_TALLER_ORDENES_PRODUCCION_EDITAR")
                                || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                    "MOD_TALLER_ORDENES_PRODUCCION_TODOS")
                                || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_TALLER_TODOS");
            btnEliminar.Enabled = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                                || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                    "MOD_TALLER_ORDENES_PRODUCCION_ELIMINAR")
                                || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                    "MOD_TALLER_ORDENES_PRODUCCION_TODOS")
                                || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_TALLER_TODOS");
        }
    }
}
