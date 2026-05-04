using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaTuplaClasificacion : Form, IVistaTuplaClasificacion {
        public VistaTuplaClasificacion() {
            InitializeComponent();
            Inicializar();
        }

        public string NombreVista {
            get => $"{Id:0000}{Name}";
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

        public string Nombre {
            get => fieldNombre.Text;
            set => fieldNombre.Text = value;
        }

        public string Descripcion {
            get => fieldDescripcion.Text;
            set {
                fieldDescripcion.Text = value;
                fieldDescripcion.Margin = fieldDescripcion.AjusteAutomaticoMargenTexto();
            }
        }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;

        public void Inicializar() {
            // Eventos
            btnEditar.Click += delegate (object? sender, EventArgs e) { 
                EditarDatosTupla?.Invoke(sender, e); 
            };
            btnEliminar.Click += delegate (object? sender, EventArgs e) { 
                EliminarDatosTupla?.Invoke(sender, e); 
            };
        }

        public void Mostrar() {
            VerificarPermisos();
            BringToFront();
            Show();
        }

        private void VerificarPermisos() {
            if (ContextoSeguridad.EstaAutenticado && ContextoSeguridad.EsAdministrador)
                return;

            btnEditar.Enabled = ContextoSeguridad.GestorPermisos?
                .TienePermiso(
                    ModuloSistemaEnum.MOD_INVENTARIO,
                    AccionModuloEnum.Editar)
                ?? false;
            btnEliminar.Enabled = ContextoSeguridad.GestorPermisos?
                .TienePermiso(
                    ModuloSistemaEnum.MOD_INVENTARIO,
                    AccionModuloEnum.Eliminar)
                ?? false;
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
    }
}