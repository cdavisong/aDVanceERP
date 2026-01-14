using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.Vistas.Almacen {
    public partial class VistaRegistroAlmacen : Form, IVistaRegistroAlmacen {
        private bool _modoEdicion = false;

        public VistaRegistroAlmacen() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroAlmacen);

            Inicializar();
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                _modoEdicion = value;

                fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
                btnRegistrarActualizar.Text = value ? "Actualizar el almacén" : "Registrar el almacén";
            }
        }

        public string NombreVista {
            get => Name;
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
        public string NombreAlmacen { 
            get => fieldNombre.Text;
            set => fieldNombre.Text = value;
        }

        public string? Descripcion {
            get => fieldDescripcion.Text;
            set => fieldDescripcion.Text = value;
        }

        public string? Direccion { 
            get => fieldDireccion.Text;
            set => fieldDireccion.Text = value;
        }

        public float? Capacidad {
            get => float.TryParse(fieldCapacidad.Text, out var value) ? value : 0f;
            set => fieldCapacidad.Text = value?.ToString("0.0", CultureInfo.InvariantCulture) ?? string.Empty;
        }

        public TipoAlmacen Tipo { 
            get => (TipoAlmacen) fieldTipo.SelectedIndex;
            set => fieldTipo.SelectedItem = value;
        }

        public bool Estado { 
            get => fieldEstado.Checked;
            set => fieldEstado.Checked = value;
        }

        public CoordenadasGeograficas? CoordenadasGeograficas { 
            get => new CoordenadasGeograficas(
                latitud: string.IsNullOrWhiteSpace(fieldLatitud.Text) ? 0 : double.TryParse(fieldLatitud.Text, out var lat) ? lat : 0,
                longitud: string.IsNullOrWhiteSpace(fieldLongitud.Text) ? 0 : double.TryParse(fieldLongitud.Text, out var lon) ? lon : 0
            );
            set {
                fieldLatitud.Text = value?.Latitud.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
                fieldLongitud.Text = value?.Longitud.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
            }
        }
        public string? Latitud { 
            get => fieldLatitud.Text;
        }

        public string? Longitud { 
            get => fieldLongitud.Text;
        }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            btnRegistrarActualizar.Click += delegate (object? sender, EventArgs args) {
                if (ModoEdicion)
                    EditarEntidad?.Invoke(sender, args);
                else
                    RegistrarEntidad?.Invoke(sender, args);
            };
            btnSalir.Click += delegate (object? sender, EventArgs args) { Ocultar(); };
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {
            NombreAlmacen = string.Empty;
            Descripcion = string.Empty;
            Direccion = string.Empty;
            Capacidad = null;
            Tipo = TipoAlmacen.Secundario;
            Estado = true;
            CoordenadasGeograficas = null;
        }

        public void Cerrar() {
            Dispose();
        }
    }
}
