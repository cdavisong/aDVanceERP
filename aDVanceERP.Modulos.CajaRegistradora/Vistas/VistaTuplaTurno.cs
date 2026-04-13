using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Modulos.CajaRegistradora.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.CajaRegistradora.Vistas {
    public partial class VistaTuplaTurno : Form, IVistaTuplaTurno {
        private EstadoCajaTurnoEnum _estado;

        public VistaTuplaTurno() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaTurno);

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

        public bool EstadoSeleccion { get; set; }

        public long Id { get; set; }

        public string Codigo {
            get => fieldCodigo.Text;
            set => fieldCodigo.Text = value;
        }

        public string NombreAlmacen {
            get => fieldAlmacen.Text;
            set => fieldAlmacen.Text = value;
        }

        public string NombreUsuarioApertura {
            get => fieldUsuarioApertura.Text;
            set => fieldUsuarioApertura.Text = value;
        }

        public DateTime FechaApertura {
            get => fieldFechaApertura.Text.Equals("-")
                                ? DateTime.MinValue
                                : DateTime.ParseExact(fieldFechaApertura.Text, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            set => fieldFechaApertura.Text = value.Equals(DateTime.MinValue)
                ? "-"
                : value.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
        }

        public DateTime? FechaCierre { get; set; }

        public decimal MontoApertura { get; set; }

        public decimal? MontoEfectivoCalculado {
            get => decimal.TryParse(fieldEfectivoCalculado.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                                        out var value)
                                        ? value
                                        : 0m;
            set => fieldEfectivoCalculado.Text = value != null
                    ? value?.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public decimal? MontoEfectivoDeclarado {
            get => decimal.TryParse(fieldEfectivoDeclarado.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                                        out var value)
                                        ? value
                                        : 0m;
            set => fieldEfectivoDeclarado.Text = value != null
                    ? value?.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public decimal? DiferenciaEfectivo {
            get => decimal.TryParse(fieldDiferenciaEfectivo.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var value)
                ? value
                : 0m;
            set {
                fieldDiferenciaEfectivo.Text = value != null
                    ? value?.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
                fieldDiferenciaEfectivo.ForeColor = fieldDiferenciaEfectivo.Text.Equals("-")
                    ? Color.Black
                    : value > 0
                        ? Color.FromArgb(198, 40, 40) // Rojo para diferencia positiva (falta dinero)
                        : value < 0
                            ? Color.FromArgb(255, 193, 7) // Ámbar para diferencia negativa (sobrante de dinero)
                            : Color.FromArgb(46, 125, 50); // Verde para sin diferencia (balanceado)
                simboloPeso3.ForeColor = fieldDiferenciaEfectivo.ForeColor;
            }
        }

        public decimal? MontoTransferenciasCalculado { get; set; }

        public decimal? MontoTransferenciasDeclarado { get; set; }

        public decimal? DiferenciaTransferencias { get; set; }

        public EstadoCajaTurnoEnum Estado {
            get => _estado;
            set {
                _estado = value;

                var (colorFondo, colorFuente) = ObtenerColorEstado(value);

                fieldEstado.DisabledState.BorderColor = colorFondo;
                fieldEstado.DisabledState.FillColor = colorFondo;
                fieldEstado.DisabledState.ForeColor = colorFuente;
                fieldEstado.Text = $"{(value == EstadoCajaTurnoEnum.Anulado ? "X" : "●")} {value.ObtenerNombreDescripcion()}";
                
                btnAnularTurno.Enabled = value == EstadoCajaTurnoEnum.Abierto;
                btnVerDetalleTurno.Enabled = value != EstadoCajaTurnoEnum.Anulado;
            }
        }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;
        public event EventHandler<long>? VerDetalleTurno;
        public event EventHandler<long>? AnularTurno;

        public void Inicializar() {
            // Eventos
            btnVerDetalleTurno.Click += delegate {
                VerDetalleTurno?.Invoke(this, Id);
            };
            btnAnularTurno.Click += delegate {
                AnularTurno?.Invoke(this, Id);
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

        private (Color colorFondo, Color colorFuente) ObtenerColorEstado(EstadoCajaTurnoEnum estado) {
            return estado switch {
                EstadoCajaTurnoEnum.Abierto => (Color.FromArgb(232, 245, 233), Color.FromArgb(45, 125, 50)),
                EstadoCajaTurnoEnum.Cerrado => (Color.FromArgb(227, 242, 253), Color.FromArgb(21, 101, 192)),
                EstadoCajaTurnoEnum.Anulado => (Color.FromArgb(253, 236, 242), Color.FromArgb(215, 104, 104)),
                _ => (Color.DimGray, Color.Black),
            };
        }
    }
}