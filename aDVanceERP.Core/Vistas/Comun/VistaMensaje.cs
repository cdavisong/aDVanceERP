using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Properties;
using aDVanceERP.Core.Vistas.Comun.Interfaces;


namespace aDVanceERP.Core.Vistas.Comun {
    public partial class VistaMensaje : Form, IVistaBase {
        private TipoMensaje _tipo;
        
        public VistaMensaje() {
            InitializeComponent();

            NombreVista = nameof(VistaMensaje);

            Inicializar();
        }

        public string NombreVista { get; }

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

        public TipoMensaje Tipo {
            get => _tipo;
            set {
                _tipo = value;

                //layoutDistribucion1.BackColor = value ? Color.LightSalmon : Color.White;
                fieldIcono.BackgroundImage =
                    value == TipoMensaje.Error
                        ? Resources.error_96px
                        : value == TipoMensaje.Advertencia
                            ? Resources.warning_96px
                            : Resources.info_96px;
                //fieldMensaje.ForeColor = value ? Color.Firebrick : Color.Gray;                
            }
        }

        public void Inicializar() {
            // Localizacion
            var areaTrabajo = Screen.PrimaryScreen?.WorkingArea ?? new Rectangle(0, 0, 1366, 768);
            
            Location = new Point(areaTrabajo.Right / 2 - Size.Width / 2, areaTrabajo.Bottom / 2 -  Size.Height / 2);

            // Eventos
            btnCerrar.Click += delegate { 
                DialogResult = DialogResult.Cancel;
                Cerrar();
            };
            btnOkYes.Click += delegate { 
                DialogResult = DialogResult.Yes;
                Cerrar();
            };
            btnNoCancel.Click += delegate {
                DialogResult = DialogResult.No;
                Cerrar();
            };
        }

        public void Mostrar() {
            BringToFront();
            ShowDialog();
        }

        public DialogResult Mostrar(string mensaje, TipoMensaje tipoMensaje) {
            if (InvokeRequired) {
                Invoke(new Action(() => {
                    Mostrar(mensaje, tipoMensaje);

                    return;
                }));
            }

            Mensaje = mensaje;
            Tipo = tipoMensaje;

            Mostrar();

            return DialogResult;
        }

        public DialogResult Mostrar(string mensaje, TipoMensaje tipoMensaje, BotonesMensaje botones) {
            if (InvokeRequired) {
                Invoke(new Action(() => {
                    Mostrar(mensaje, tipoMensaje, botones);

                    return;
                }));
            }

            btnOkYes.Text = botones switch {
                BotonesMensaje.SiNo => "Si",
                BotonesMensaje.AceptarCancelar => "Aceptar",
                BotonesMensaje.ContinuarAbortar => "Continuar",
            };

            btnNoCancel.Text = botones switch {
                BotonesMensaje.SiNo => "No",
                BotonesMensaje.AceptarCancelar => "Cancelar",
                BotonesMensaje.ContinuarAbortar => "Abortar",
            };

            return Mostrar(mensaje, tipoMensaje);
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {
            throw new NotImplementedException();
        }

        public void Cerrar() {
            Close();
        }

        #region STATIC

        public static VistaMensaje Instancia = new VistaMensaje();

        #endregion
    }
}
