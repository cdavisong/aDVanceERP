using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Repositorios.Modulos.Caja;
using aDVanceERP.Modulos.CajaRegistradora.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.CajaRegistradora.Vistas {
    public partial class VistaCierreTurno : Form, IVistaCierreTurno {
        private bool _modoEdicion = false;
        private CajaTurno? _turno = null!;
        private List<CajaArqueo> _arqueosCajaBd = null!;
        private DateTime _fechaApertura;
        private decimal _montoApertura;
        private decimal _totalEfectivoCalculado;
        private decimal _totalTransferenciasCalculado;
        private decimal _montoEfectivoDeclarado;
        private decimal _montoTransferenciasDeclarado;
        private decimal _diferenciaEfectivo;
        private decimal _diferenciaTransferencias;

        public VistaCierreTurno() {
            InitializeComponent();

            NombreVista = nameof(VistaCierreTurno);

            Inicializar();
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                _modoEdicion = value;

                fieldSubtitulo.Text = value ? "" : "Arqueo y conciliación de caja";
                btnRegistrarActualizar.Text = value ? "" : "Confirmar cierre";
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

        public string CodigoTurno {
            get => fieldCodigo.Text;
            set {
                fieldCodigo.Text = value;

                _turno = RepoCajaTurno.Instancia.Buscar(FiltroBusquedaCajaTurno.Codigo, value).resultadosBusqueda.Select(r => r.entidadBase).FirstOrDefault();
                _arqueosCajaBd = RepoCajaArqueo.Instancia.ObtenerPorTurno(_turno?.Id ?? 0);
            }
        }

        public string NombreAlmacen {
            get => fieldAlmacen.Text;
            set => fieldAlmacen.Text = value;
        }

        public DateTime FechaApertura {
            get => _fechaApertura;
            set {
                _fechaApertura = value;

                fieldFechaHoraApertura.Text = value.ToString("dd/MM/yyyy HH:mm");
            }
        }

        public decimal MontoApertura {
            get => _montoApertura;
            set {
                _montoApertura = value;

                fieldFondoInicial.Text = value.ToString("N2", CultureInfo.InvariantCulture);
            }
        }

        public decimal TotalEfectivoCalculado {
            get => _totalEfectivoCalculado;
            set {
                _totalEfectivoCalculado = value;

                fieldEfectivoCalculado.Text = $"$ {value:N2}";
            }
        }

        public decimal TotalTransferenciasCalculado {
            get => _totalTransferenciasCalculado;
            set {
                _totalTransferenciasCalculado = value;

                fieldTransferenciaCalculada.Text = $"$ {value:N2}";
            }
        }

        public decimal MontoEfectivoDeclarado {
            get => _montoEfectivoDeclarado;
            set {
                _montoEfectivoDeclarado = value;

                fieldMontoEfectivoDeclarado.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : string.Empty;
            }
        }

        public decimal MontoTransferenciasDeclarado {
            get => _montoTransferenciasDeclarado;
            set {
                _montoTransferenciasDeclarado = value;

                fieldMontoTransferenciaDeclarado.Text = value > 0 
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : string.Empty;
            }
        }

        public decimal DiferenciaEfectivo {
            get => _diferenciaEfectivo;
            set {
                _diferenciaEfectivo = value;

                fieldDiferenciaEfectivo.Text = $"$ {value:N2}";
                fieldDiferenciaEfectivo.ForeColor = value < 0
                    ? Color.FromArgb(198, 40, 40) // Rojo para diferencia negativa (falta dinero)
                    : value > 0
                        ? Color.FromArgb(255, 193, 7) // Ámbar para diferencia positiva (sobrante de dinero)
                        : Color.FromArgb(46, 125, 50); // Verde para sin diferencia (balanceado)
            }
        }

        public decimal DiferenciaTransferencias {
            get => _diferenciaTransferencias;
            set {
                _diferenciaTransferencias = value;

                fieldDiferenciaTransferencias.Text = $"$ {value:N2}";
                fieldDiferenciaTransferencias.ForeColor = value < 0
                    ? Color.FromArgb(198, 40, 40) // Rojo para diferencia negativa (falta dinero)
                    : value > 0
                        ? Color.FromArgb(255, 193, 7) // Ámbar para diferencia positiva (sobrante de dinero)
                        : Color.FromArgb(46, 125, 50); // Verde para sin diferencia (balanceado)
            }
        }

        public string? Observaciones {
            get => fieldObservaciones.Text;
            set => fieldObservaciones.Text = value;
        }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;
        public event EventHandler? ArqueoModificado;

        public void Inicializar() {
            fieldConteoDenominacion5000.Leave += delegate {
                ActualizarTotalArqueoVista();
            };
            fieldConteoDenominacion2000.Leave += delegate {
                ActualizarTotalArqueoVista();
            };
            fieldConteoDenominacion1000.Leave += delegate {
                ActualizarTotalArqueoVista();
            };
            fieldConteoDenominacion500.Leave += delegate {
                ActualizarTotalArqueoVista();
            };
            fieldConteoDenominacion200.Leave += delegate {
                ActualizarTotalArqueoVista();
            };
            fieldConteoDenominacion100.Leave += delegate {
                ActualizarTotalArqueoVista();
            };
            fieldConteoDenominacion50.Leave += delegate {
                ActualizarTotalArqueoVista();
            };
            fieldConteoDenominacion20.Leave += delegate {
                ActualizarTotalArqueoVista();
            };
            fieldConteoDenominacion10.Leave += delegate {
                ActualizarTotalArqueoVista();
            };
            fieldConteoDenominacion5.Leave += delegate {
                ActualizarTotalArqueoVista();
            };
            fieldConteoDenominacion3.Leave += delegate {
                ActualizarTotalArqueoVista();
            };
            fieldConteoDenominacion1.Leave += delegate {
                ActualizarTotalArqueoVista();
            };
            fieldMontoTransferenciaDeclarado.Leave += delegate {
                _montoTransferenciasDeclarado = decimal.TryParse(fieldMontoTransferenciaDeclarado.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var montoTransferenciaDeclarado)
                    ? montoTransferenciaDeclarado
                    : 0m;

                ArqueoModificado?.Invoke(this, EventArgs.Empty);
            };
            btnRegistrarActualizar.Click += delegate (object? sender, EventArgs args) {
                RegistrarEntidad?.Invoke(sender, args);
            };
            btnSalir.Click += delegate (object? sender, EventArgs args) {
                Ocultar();
            };
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {
            fieldOperador.Text = ContextoSeguridad.UsuarioAutenticado?.Nombre ?? "admin";
        }

        public void Cerrar() {
            Dispose();
        }

        public IEnumerable<CajaArqueo> ObtenerArqueo() {
            var arqueo5000 = new CajaArqueo(0, _turno?.Id ?? 0, 5000, int.TryParse(fieldConteoDenominacion5000.Text, out var result5000) ? result5000 : 0);
            var arqueo2000 = new CajaArqueo(0, _turno?.Id ?? 0, 2000, int.TryParse(fieldConteoDenominacion2000.Text, out var result2000) ? result2000 : 0);
            var arqueo1000 = new CajaArqueo(0, _turno?.Id ?? 0, 1000, int.TryParse(fieldConteoDenominacion1000.Text, out var result1000) ? result1000 : 0);
            var arqueo500 = new CajaArqueo(0, _turno?.Id ?? 0, 500, int.TryParse(fieldConteoDenominacion500.Text, out var result500) ? result500 : 0);
            var arqueo200 = new CajaArqueo(0, _turno?.Id ?? 0, 200, int.TryParse(fieldConteoDenominacion200.Text, out var result200) ? result200 : 0);
            var arqueo100 = new CajaArqueo(0, _turno?.Id ?? 0, 100, int.TryParse(fieldConteoDenominacion100.Text, out var result100) ? result100 : 0);
            var arqueo50 = new CajaArqueo(0, _turno?.Id ?? 0, 50, int.TryParse(fieldConteoDenominacion50.Text, out var result50) ? result50 : 0);
            var arqueo20 = new CajaArqueo(0, _turno?.Id ?? 0, 20, int.TryParse(fieldConteoDenominacion20.Text, out var result20) ? result20 : 0);
            var arqueo10 = new CajaArqueo(0, _turno?.Id ?? 0, 10, int.TryParse(fieldConteoDenominacion10.Text, out var result10) ? result10 : 0);
            var arqueo5 = new CajaArqueo(0, _turno?.Id ?? 0, 5, int.TryParse(fieldConteoDenominacion5.Text, out var result5) ? result5 : 0);
            var arqueo3 = new CajaArqueo(0, _turno?.Id ?? 0, 3, int.TryParse(fieldConteoDenominacion3.Text, out var result3) ? result3 : 0);
            var arqueo1 = new CajaArqueo(0, _turno?.Id ?? 0, 1, int.TryParse(fieldConteoDenominacion1.Text, out var result1) ? result1 : 0);
            var arqueosCaja = new List<CajaArqueo>() {
                { arqueo5000 },
                { arqueo2000 },
                { arqueo1000 },
                { arqueo500  },
                { arqueo200  },
                { arqueo100  },
                { arqueo50   },
                { arqueo20   },
                { arqueo10   },
                { arqueo5    },
                { arqueo3    },
                { arqueo1    },
            };

            // Actualizar Id's de arqueos
            for (int i = 0; i < arqueosCaja.Count; i++)
                arqueosCaja[i].Id = _arqueosCajaBd?.Count > i ? _arqueosCajaBd[i].Id : 0;

            return arqueosCaja;
        }

        public void ActualizarTotalArqueo(decimal totalContado) {
            fieldTotalContado.Text = $"$ {totalContado:N2}";
        }

        private void ActualizarTotalArqueoVista() {
            // Calcular montos
            var monto5000 = (int.TryParse(fieldConteoDenominacion5000.Text, out var result5000) ? result5000 : 0) * 5000;
            var monto2000 = (int.TryParse(fieldConteoDenominacion2000.Text, out var result2000) ? result2000 : 0) * 2000;
            var monto1000 = (int.TryParse(fieldConteoDenominacion1000.Text, out var result1000) ? result1000 : 0) * 1000;
            var monto500 = (int.TryParse(fieldConteoDenominacion500.Text, out var result500) ? result500 : 0) * 500;
            var monto200 = (int.TryParse(fieldConteoDenominacion200.Text, out var result200) ? result200 : 0) * 200;
            var monto100 = (int.TryParse(fieldConteoDenominacion100.Text, out var result100) ? result100 : 0) * 100;
            var monto50 = (int.TryParse(fieldConteoDenominacion50.Text, out var result50) ? result50 : 0) * 50;
            var monto20 = (int.TryParse(fieldConteoDenominacion20.Text, out var result20) ? result20 : 0) * 20;
            var monto10 = (int.TryParse(fieldConteoDenominacion10.Text, out var result10) ? result10 : 0) * 10;
            var monto5 = (int.TryParse(fieldConteoDenominacion5.Text, out var result5) ? result5 : 0) * 5;
            var monto3 = (int.TryParse(fieldConteoDenominacion3.Text, out var result3) ? result3 : 0) * 3;
            var monto1 = (int.TryParse(fieldConteoDenominacion1.Text, out var result1) ? result1 : 0) * 1;

            // Actualizar vista
            fieldMontoTotal5000.Text = $"= $ {monto5000:N2}";
            fieldMontoTotal2000.Text = $"= $ {monto2000:N2}";
            fieldMontoTotal1000.Text = $"= $ {monto1000:N2}";
            fieldMontoTotal500.Text = $"= $ {monto500:N2}";
            fieldMontoTotal200.Text = $"= $ {monto200:N2}";
            fieldMontoTotal100.Text = $"= $ {monto100:N2}";
            fieldMontoTotal50.Text = $"= $ {monto50:N2}";
            fieldMontoTotal20.Text = $"= $ {monto20:N2}";
            fieldMontoTotal10.Text = $"= $ {monto10:N2}";
            fieldMontoTotal5.Text = $"= $ {monto5:N2}";
            fieldMontoTotal3.Text = $"= $ {monto3:N2}";
            fieldMontoTotal1.Text = $"= $ {monto1:N2}";

            ArqueoModificado?.Invoke(this, EventArgs.Empty);
        }
    }
}
