using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaTuplaUnidadMedida : Form, IVistaTuplaUnidadMedida {
        public VistaTuplaUnidadMedida() {
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

        public string Abreviatura {
            get => fieldAbreviatura.Text;
            set => fieldAbreviatura.Text = value;
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
            btnEditar.Click += delegate (object? sender, EventArgs e) { EditarDatosTupla?.Invoke(Id, e); };
            btnEliminar.Click += delegate (object? sender, EventArgs e) { EliminarDatosTupla?.Invoke(Id, e); };
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

        public override string ToString() {
            return $"{NombreVista}";
        }
    }
}