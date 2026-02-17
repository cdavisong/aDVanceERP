using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;
using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaTuplaEnvio : Form, IVistaTuplaEnvio {
        private EstadoPagoEnum _estadoPago;

        public VistaTuplaEnvio() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaEnvio);

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
            set => layoutVista.BackColor = layoutVista.BackColor = value == Color.Gainsboro
                ? value
                : ObtenerColorFondoTupla(EstadoEntrega);
        }

        public bool EstadoSeleccion { get; set; }
        public long Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public long IdVenta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string NumeroFacturaVenta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public long? IdMensajero { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string? NombreMensajero { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TipoEnvioEnum TipoEnvio { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime FechaAsignacion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime FechaEntregaRealizada { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string? ObservacionesEntrega { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal MontoCobradoAlCliente { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public EstadoEntregaEnum EstadoEntrega { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;

        public void Inicializar() {
            // Eventos
            btnConfirmar.Click += delegate (object? sender, EventArgs e) {
                
            };
            btnCancelar.Click += delegate (object? sender, EventArgs e) {
                
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

        private Color ObtenerColorFondoTupla(EstadoEntregaEnum estado) {
            return estado switch {
                EstadoEntregaEnum.PendienteAsignacion => ContextoAplicacion.ColorAdvertenciaTupla,
                EstadoEntregaEnum.Fallido => ContextoAplicacion.ColorErrorTupla,
                EstadoEntregaEnum.Cancelado => ContextoAplicacion.ColorErrorTupla,
                _ => BackColor
            };
        }
    }
}