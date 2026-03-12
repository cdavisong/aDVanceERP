using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;
using aDVanceERP.Modulos.Venta.Properties;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaEstadisticasVenta : Form, IVistaEstadisticasVenta {
        public VistaEstadisticasVenta() {
            InitializeComponent();

            NombreVista = nameof(VistaEstadisticasVenta);

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

        public int VentasHoy {
            get => int.Parse(fieldVentasHoy.Text);
            set => fieldVentasHoy.Text = value.ToString();
        }

        public decimal IngresosHoy {
            get => decimal.Parse(fieldIngresosHoy.Text);
            set => fieldIngresosHoy.Text = value.ToString("N2");
        }

        public decimal IngresosMes {
            get => decimal.Parse(fieldIngresosMes.Text);
            set => fieldIngresosMes.Text = value.ToString("N2");
        }

        public decimal IngresosMesAnterior { get; set; }

        public int VentasPendientes {
            get => int.Parse(fieldVentasPendientes.Text);
            set => fieldVentasPendientes.Text = value.ToString();
        }

        public int PagosPendientesConfirmacion {
            get => int.Parse(fieldPagosSinConfirmar.Text);
            set => fieldPagosSinConfirmar.Text = value.ToString();
        }

        public int PedidosActivos {
            get => int.Parse(fieldPedidosActivos.Text);
            set => fieldPedidosActivos.Text = value.ToString();
        }

        public event EventHandler? ActualizarTodo;
        public event EventHandler<PaintEventArgs>? ActualizarIngresosDiarios;
        public event EventHandler<PaintEventArgs>? ActualizarMetodosPago;

        public void Inicializar() {
            // Eventos
            btnActualizar.Click += (sender, e) => {
                ActualizarTodo?.Invoke(sender, e);
            };
            fieldIngresosDiarios.Paint += (sender, e) => {
                ActualizarIngresosDiarios?.Invoke(sender, e);
            };
            fieldMetodosPago.Paint += (sender, e) => {
                ActualizarMetodosPago?.Invoke(sender, e);
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

        public void CargarTopProductos(List<ProductoTopVenta> topProductos) {
            // Limpiar filas anteriores del panel de scroll
            panelTuplasTopProductos.Controls.Clear();

            if (topProductos == null || topProductos.Count == 0)
                return;

            decimal maxValor = topProductos.Max(p => p.CantidadTotal);
            if (maxValor == 0) maxValor = 1m;

            for (int i = 0; i < topProductos.Count; i++) {
                var producto = topProductos[i];
                var fila = CrearFilaTopProducto(i + 1, producto, maxValor);
                fila.Top = i * 42;

                panelTuplasTopProductos.Controls.Add(fila);
                panelTuplasTopProductos.Controls.SetChildIndex(fila, i);
            }
        }

        /// <summary>
        /// Crea un control de fila para la tabla "Top Producto — "Último mes".
        /// Replica el diseño rank-table del HTML de referencia.
        /// </summary>
        private Panel CrearFilaTopProducto(
            int posicion,
            ProductoTopVenta producto,
            decimal maxValor) {
            var fila = new Panel {
                Dock = DockStyle.None,
                Width = panelTuplasTopProductos.Width,
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
                Text = producto.Nombre,
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(85, 85, 85),
                AutoSize = false,
                Location = new Point(32, 0),
                Size = new Size(fila.Width - 30 - 110, 40),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Columna Unidades
            float porcentajeBarra = maxValor > 0
                ? (float) (producto.CantidadTotal / maxValor) * 100f
                : 0f;

            var lblMonto = new Label {
                Text = producto.CantidadTotal.ToString("N0"),
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

        public void ActualizarPorcentajeVentasHoyVsAyer(decimal actual, decimal anterior) {
            if (anterior == 0)
                return;

            decimal pct = (actual - anterior) / anterior * 100m;

            fieldVentasHoyVsAyer.Text = $"    {pct.ToString("00")}% vs ayer";
            fieldVentasHoyVsAyer.Image = pct switch {
                  0 => Resources.sort_16px,
                > 0 => Resources.sort_up_16px,
                  _ => Resources.sort_down_16px
            };
        }

        public void ActualizarPorcentajeIngresosVsMesAnterior(decimal actual, decimal anterior) {
            if (anterior == 0)
                return;

            decimal pct = (actual - anterior) / anterior * 100m;

            fieldIngresosMesVsAnterior.Text = $"    {pct.ToString("00")}% vs mes anterior";
            fieldIngresosMesVsAnterior.Image = pct switch {
                  0 => Resources.sort_16px,
                > 0 => Resources.sort_up_16px,
                  _ => Resources.sort_down_16px
            };
        }

        public void RenderizarGraficosGastosDistribuciones() {
            fieldIngresosDiarios.Refresh();
            fieldMetodosPago.Refresh();
        }
    }
}