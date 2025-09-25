using aDVanceERP.Core.Mensajes.MVP.Modelos;
using aDVanceERP.Core.Mensajes.MVP.Vistas.Notificacion.Plantillas;
using aDVanceERP.Core.Mensajes.Properties;
using Timer = System.Windows.Forms.Timer;

namespace aDVanceERP.Core.Mensajes.MVP.Vistas.Notificacion; 

public partial class VistaNotificacion : Form, IVistaNotificacion {
    private bool _estaCerrando;
    private readonly Modelos.Notificacion _notificacion;
    private readonly int _pasoAnimacion = 10;

    // Animaciones
    private Point _posicionObjetivo;

    // Temporizadores
    private Timer? _timerAnimacion;

    private Timer? _timerVisualizacion;

    // Notificacion.
    private TipoNotificacion _tipo;

    public VistaNotificacion(Modelos.Notificacion notificacion) {
        _notificacion = notificacion;

        InitializeComponent();

        NombreVista = nameof(VistaNotificacion);

        Inicializar();
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

    public string? Mensaje {
        get => fieldMensaje.Text;
        set => fieldMensaje.Text = value;
    }

    public TipoNotificacion Tipo {
        get => _tipo;
        set {
            _tipo = value;

            //layoutDistribucion1.BackColor = value ? Color.LightSalmon : Color.White;
            fieldIcono.BackgroundImage =
                value == TipoNotificacion.Error
                    ? Resources.error_96px
                    : value == TipoNotificacion.Advertencia
                        ? Resources.warning_96px
                        : Resources.info_96px;
            //fieldMensaje.ForeColor = value ? Color.Firebrick : Color.Gray;                
        }
    }

    public event EventHandler? Salir;

    public void Inicializar() {
        if (_notificacion != null) {
            Mensaje = _notificacion.Mensaje;
            Tipo = _notificacion.Tipo;
        }

        // Temporizadores
        _timerAnimacion = new Timer();
        _timerAnimacion.Interval = 15; // Aproximadamente 60 FPS
        _timerAnimacion.Tick += delegate {
            var dx = _posicionObjetivo.X - Left;
            var dy = _posicionObjetivo.Y - Top;
            var xAlcanzado = false;
            var yAlcanzado = false;

            // Movimiento horizontal
            if (Math.Abs(dx) >= _pasoAnimacion) {
                Left += dx > 0 ? _pasoAnimacion : -_pasoAnimacion;
            }
            else {
                Left = _posicionObjetivo.X;
                xAlcanzado = true;
            }

            // Movimiento vertical
            if (Math.Abs(dy) >= _pasoAnimacion) {
                Top += dy > 0 ? _pasoAnimacion : -_pasoAnimacion;
            }
            else {
                Top = _posicionObjetivo.Y;
                yAlcanzado = true;
            }

            // Posicion objetivo alcanzada
            if (xAlcanzado && yAlcanzado) {
                _timerAnimacion.Stop();

                if (_estaCerrando) {
                    Close();

                    Salir?.Invoke(this, EventArgs.Empty);
                }
            }
        };

        _timerVisualizacion = new Timer();
        _timerVisualizacion.Interval = _notificacion?.Duracion ?? 3000;
        _timerVisualizacion.Tick += delegate {
            _timerVisualizacion?.Stop();

            Cerrar();
        };

        // Eventos
        Shown += delegate { _timerVisualizacion?.Start(); };
        btnCerrar.Click += delegate {
            if (_timerVisualizacion.Enabled)
                _timerVisualizacion.Stop();

            Cerrar();
        };
    }

    public void Mostrar() {
        BringToFront();
        Show();

        _timerAnimacion?.Start();
    }

    public void ActualizarPosicionObjetivo(Point objetivo) {
        _posicionObjetivo = objetivo;

        if (!(_timerAnimacion?.Enabled ?? false))
            _timerAnimacion?.Start();
    }

    public void EstablecerPosicionObjetivo(Point objetivo) {
        _posicionObjetivo = objetivo;
    }

    public void Ocultar() {
        Hide();
    }

    public void Restaurar() {
        throw new NotImplementedException();
    }

    public void Cerrar() {
        if (!_estaCerrando) {
            _estaCerrando = true;

            // Animamos la salida : deslizamos a la derecha (fuera del área de trabajo)
            var areaTrabajo = Screen.PrimaryScreen?.WorkingArea;

            EstablecerPosicionObjetivo(new Point(areaTrabajo?.Right ?? 1366, Top));

            _timerAnimacion?.Start();
        }
    }
}