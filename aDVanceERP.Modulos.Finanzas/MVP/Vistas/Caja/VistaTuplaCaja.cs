using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Modelos.Modulos.Finanzas;
using aDVanceERP.Core.Repositorios.Modulos.Finanzas;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Utiles;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja.Plantillas;
using aDVanceERP.Modulos.Finanzas.Properties;

using System.Globalization;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja {
    public partial class VistaTuplaCaja : Form, IVistaTuplaCaja {
        private int _estado;

        public VistaTuplaCaja() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaCaja);

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

        public string Id {
            get => fieldId.Text;
            set => fieldId.Text = value;
        }

        public string FechaApertura {
            get => fieldFechaApertura.Text;
            set => fieldFechaApertura.Text = value;
        }

        public string SaldoInicial { 
            get => fieldSaldoInicial.Text;
            set => fieldSaldoInicial.Text = value;
        }

        public string CantidadMovimientos {
            get => fieldCantidadMovimientos.Text;
            set => fieldCantidadMovimientos.Text = value;
        }

        public string SaldoActual { 
            get => fieldSaldoActual.Text;
            set => fieldSaldoActual.Text = value;
        }

        public string FechaCierre { 
            get => fieldFechaCierre.Text;
            set => fieldFechaCierre.Text = value;
        }

        public int Estado { 
            get => _estado;
            set {
                _estado = value;

                btnDescargarInforme.Enabled = _estado == 2;
                fieldEstado.Image = _estado switch {
                    0 => Resources.open_check_20px,
                    1 => Resources.open_sign_20px,
                    2 => Resources.closed_sign_20px,
                    _ => Resources.open_check_20px
                };
                
            }
        }

        public string NombreUsuario {
            get => fieldNombreUsuario.Text;
            set => fieldNombreUsuario.Text = value;
        }

        public Color ColorFondoTupla {
            get => layoutVista.BackColor;
            set => layoutVista.BackColor = value;
        }

        public event EventHandler? TuplaSeleccionada;
        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;
        

        public void Inicializar() {
            // Eventos
            fieldId.Click += delegate (object? sender, EventArgs e) {
                TuplaSeleccionada?.Invoke(this, e);
            };
            fieldFechaApertura.Click += delegate (object? sender, EventArgs e) {
                TuplaSeleccionada?.Invoke(this, e);
            };
            fieldSaldoInicial.Click += delegate (object? sender, EventArgs e) {
                TuplaSeleccionada?.Invoke(this, e);
            };
            fieldSaldoActual.Click += delegate (object? sender, EventArgs e) {
                TuplaSeleccionada?.Invoke(this, e);
            };
            fieldFechaCierre.Click += delegate (object? sender, EventArgs e) {
                TuplaSeleccionada?.Invoke(this, e);
            };
            fieldEstado.Click += delegate (object? sender, EventArgs e) {
                TuplaSeleccionada?.Invoke(this, e);
            };
            fieldNombreUsuario.Click += delegate (object? sender, EventArgs e) {
                TuplaSeleccionada?.Invoke(this, e);
            };

            btnDescargarInforme.Click += delegate (object? sender, EventArgs e) {
                var fechaApertura = DateTime.ParseExact(fieldFechaApertura.Text, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                var fechaCierre = DateTime.ParseExact(fieldFechaCierre.Text, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                var saldoInicial = decimal.Parse(fieldSaldoInicial.Text, CultureInfo.InvariantCulture);
                var saldoFinal = decimal.Parse(fieldSaldoActual.Text, CultureInfo.InvariantCulture);
                var datosMovimientosCaja = new List<string[]>();

                using (var datos = new RepoMovimientoCaja()) {
                    var movimientosCaja = datos.Buscar(FiltroBusquedaMovimientoCaja.IdCaja, Id).entidades;

                    foreach (var movimiento in movimientosCaja) {
                        datosMovimientosCaja.Add([
                            movimiento.Fecha.ToString("yyyy-MM-dd HH:mm:ss"),
                            movimiento.Concepto ?? string.Empty,
                            movimiento.Tipo.ToString(),
                            movimiento.Monto.ToString("N2", CultureInfo.InvariantCulture),
                            movimiento.Observaciones ?? string.Empty
                        ]);
                    }
                }

                if (datosMovimientosCaja.Count > 0)
                    UtilesReportes.GenerarReporteCierreCaja(
                        fechaApertura,
                        fechaCierre,
                        saldoInicial,
                        saldoFinal,
                        int.Parse(Id),
                        datosMovimientosCaja,
                        ((EstadoCaja) Estado).ToString()
                    );
                else
                    CentroNotificaciones.Mostrar("No se puede generar un reporte de una caja cerrada con 0 movimientos.", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
            };
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
            btnEditar.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                                || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_FINANZAS_CAJA_EDITAR")
                                || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_FINANZAS_CAJA_TODOS")
                                || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_FINANZAS_TODOS");
            btnEliminar.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                                  || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_FINANZAS_CAJA_ELIMINAR")
                                  || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_FINANZAS_CAJA_TODOS")
                                  || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_FINANZAS_TODOS");
        }
    }
}
