using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Modulos.Compra.Interfaces;
using aDVanceERP.Modulos.Compra.Properties;

namespace aDVanceERP.Modulos.Compra.Vistas {
    public partial class VistaEstadisticasCompra : Form, IVistaEstadisticasCompra {
        public VistaEstadisticasCompra() {
            InitializeComponent();

            NombreVista = nameof(VistaEstadisticasCompra);

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

        public int OrdenesPendientesAprobacion {
            get => int.Parse(fieldPendientesAprobacion.Text);
            set => fieldPendientesAprobacion.Text = value.ToString();
        }

        public int OrdenesAprobadas {
            get => int.Parse(fieldAprobadas.Text);
            set => fieldAprobadas.Text = value.ToString();
        }

        public int OrdenesRecibidasParcial {
            get => int.Parse(fieldRecibidasParcial.Text);
            set => fieldRecibidasParcial.Text = value.ToString();
        }

        public int SolicitudesPendientes {
            get => int.Parse(fieldSolicitudesPendientes.Text);
            set => fieldSolicitudesPendientes.Text = value.ToString();
        }

        public decimal GastoMesActual {
            get => decimal.Parse(fieldGastoMesActual.Text);
            set => fieldGastoMesActual.Text = value.ToString("N2");
        }

        public decimal GastoMesAnterior {
            get => decimal.Parse(fieldGastoMesAnterior.Text);
            set => fieldGastoMesAnterior.Text = value.ToString("N2");
        }

        public event EventHandler? ActualizarTodo;
        public event EventHandler<PaintEventArgs>? ActualizarEvolucionGasto6Meses;
        public event EventHandler<PaintEventArgs>? ActualizarDistribucionPorEstado;

        public void Inicializar() {
            // Eventos
            btnActualizar.Click += (sender, e) => {
                ActualizarTodo?.Invoke(sender, e);
            };
            fieldEvolucionGasto6Meses.Paint += (sender, e) => {
                ActualizarEvolucionGasto6Meses?.Invoke(sender, e);
            };
            fieldDistribucionPorEstado.Paint += (sender, e) => {
                ActualizarDistribucionPorEstado?.Invoke(sender, e);
            };
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            // ...
        }

        public void CargarTopProveedores(List<ProveedorTopCompras> topProveedores) {
            // Limpiar filas anteriores del panel de scroll
            panelTuplasTopProveedores.Controls.Clear();

            if (topProveedores == null || topProveedores.Count == 0)
                return;

            decimal maxValor = topProveedores.Max(p => p.MontoTotal);
            if (maxValor == 0) maxValor = 1m;

            for (int i = 0; i < topProveedores.Count; i++) {
                var prveedor = topProveedores[i];
                var fila = CrearFilaTopProveedor(i + 1, prveedor, maxValor);
                fila.Top = i * 42;

                panelTuplasTopProveedores.Controls.Add(fila);
                panelTuplasTopProveedores.Controls.SetChildIndex(fila, i);
            }
        }

        /// <summary>
        /// Crea un control de fila para la tabla "Top Proveedores — 12 meses".
        /// Replica el diseño rank-table del HTML de referencia.
        /// </summary>
        private Panel CrearFilaTopProveedor(
            int posicion,
            ProveedorTopCompras proveedor,
            decimal maxValor) {
            var fila = new Panel {
                Dock = DockStyle.None,
                Width = panelTuplasTopProveedores.Width,
                Height = 40,
                Padding = new Padding(0)
            };

            // Columna #
            var lblNum = new Label {
                Text = posicion.ToString("D2"),
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(136, 136, 136),
                Size = new Size(30, 40),
                Location = new Point(0, 0),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Columna Nombre
            var lblNombre = new Label {
                Text = proveedor.NombreProveedor,
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(85, 85, 85),
                AutoSize = false,
                Location = new Point(32, 0),
                Size = new Size(fila.Width - 30 - 110, 40),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Columna Monto $
            float porcentajeBarra = maxValor > 0
                ? (float) (proveedor.MontoTotal / maxValor) * 100f
                : 0f;

            var lblMonto = new Label {
                Text = proveedor.MontoTotal.ToString("N0"),
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = false,
                Location = new Point(fila.Right - 110, 0),
                Size = new Size(110, 40),
                TextAlign = ContentAlignment.MiddleRight
            };

            fila.Controls.AddRange([lblNum, lblNombre, lblMonto]);

            // Barra inline proporcional (como el HTML: bar-inline)
            fila.Paint += (_, e) => {
                int barMaxW = 60;
                int barW = (int) (barMaxW * porcentajeBarra / 100f);
                int barX = lblNombre.Right;
                int barY = fila.Height - 6;
                var colorBarra = Color.FromArgb(100, 255, 218, 185); // peachpuff semitransparente
                e.Graphics.FillRectangle(new SolidBrush(colorBarra),
                    new Rectangle(barX, barY, barW, 3));

                // Separador inferior
                e.Graphics.DrawLine(
                    new Pen(Color.FromArgb(228, 228, 228)),
                    0, fila.Height - 1, fila.Width, fila.Height - 1);
            };

            return fila;
        }

        public void ActualizarPorcentajeVsMesAnterior(decimal actual, decimal anterior) {
            if (anterior == 0)
                return;

            decimal pct = (actual - anterior) / anterior * 100m;

            fieldPorcientoGastoMesActualVsAnterior.Text = $"    {pct.ToString("00")}% vs mes anterior";
            fieldPorcientoGastoMesActualVsAnterior.Image = pct switch {
                  0 => Resources.sort_16px,
                > 0 => Resources.sort_up_16px,
                  _ => Resources.sort_down_16px
            };
        }

        public void RenderizarGraficosGastosDistribuciones() {
            fieldEvolucionGasto6Meses.Refresh();
            fieldDistribucionPorEstado.Refresh();
        }
    }
}