using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Monedas;
using aDVanceERP.Modulos.Inventario.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaEstadisticasInventario : Form, IVistaEstadisticasInventario {
        public VistaEstadisticasInventario() {
            InitializeComponent();

            NombreVista = nameof(VistaEstadisticasInventario);

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
        public int TotalProductos {
            get => int.Parse(fieldProductosActivos.Text);
            set => fieldProductosActivos.Text = value.ToString();
        }

        public int ProductosBajoStockMinimo {
            get => int.Parse(fieldBajoStockMinimo.Text);
            set => fieldBajoStockMinimo.Text = value.ToString();
        }

        public int ProductosSinStock {
            get => int.Parse(fieldSinStock.Text);
            set => fieldSinStock.Text = value.ToString();
        }

        public decimal ValorTotalInventario {
            get => decimal.Parse(fieldValorTotalInventario.Text, NumberStyles.Any, CultureInfo.InvariantCulture);
            set => fieldValorTotalInventario.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public int TotalAlmacenes {
            get => int.Parse(fieldAlmacenesActivos.Text);
            set => fieldAlmacenesActivos.Text = value.ToString();
        }

        public int MovimientosHoy {
            get => int.Parse(fieldMovimientosHoy.Text);
            set => fieldMovimientosHoy.Text = value.ToString();
        }

        public event EventHandler? ActualizarTodo;
        public event EventHandler<PaintEventArgs>? ActualizarEvolucionMovimientos;
        public event EventHandler<PaintEventArgs>? ActualizarValorAlmacen;

        public void Inicializar() {
            // Eventos
            btnActualizar.Click += (sender, e) => {
                ActualizarTodo?.Invoke(sender, e);
            };
            fieldEvolucionMovimientos.Paint += (sender, e) => {
                ActualizarEvolucionMovimientos?.Invoke(sender, e);
            };
            fieldValorAlmacen.Paint += (sender, e) => {
                ActualizarValorAlmacen?.Invoke(sender, e);
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

        public void CargarTopProductosValor(List<ProductoTopInventario> topProductosValor) {
            // Limpiar filas anteriores del panel de scroll
            panelTuplasTopProductosValor.Controls.Clear();

            if (topProductosValor == null || topProductosValor.Count == 0)
                return;

            decimal maxValor = topProductosValor.Max(p => p.ValorTotal);
            if (maxValor == 0) maxValor = 1m;

            for (int i = 0; i < topProductosValor.Count; i++) {
                var producto = topProductosValor[i];
                var fila = CrearFilaTopProducto(i + 1, producto, maxValor);
                fila.Top = i * 42;

                panelTuplasTopProductosValor.Controls.Add(fila);
                panelTuplasTopProductosValor.Controls.SetChildIndex(fila, i);
            }
        }

        /// <summary>
        /// Crea un control de fila para la tabla "Top Productos — Valor".
        /// Replica el diseño rank-table del HTML de referencia.
        /// </summary>
        private Panel CrearFilaTopProducto(
            int posicion,
            ProductoTopInventario producto,
            decimal maxValor) {
            var fila = new Panel {
                Dock = DockStyle.None,
                Width = panelTuplasTopProductosValor.Width,
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

            // Columna Valor $
            float porcentajeBarra = maxValor > 0
                ? (float) (producto.ValorTotal / maxValor) * 100f
                : 0f;

            var lblValor = new Label {
                Text = $"{RepoMoneda.Instancia.ObtenerMonedaBase().Simbolo} {producto.ValorTotal.ToString("N2", CultureInfo.InvariantCulture)} ",
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = false,
                Location = new Point(fila.Right - 110, 0),
                Size = new Size(110, 40),
                TextAlign = ContentAlignment.MiddleRight
            };

            fila.Controls.AddRange([lblNum, lblNombre, lblValor]);

            // Barra inline proporcional
            fila.Paint += (_, e) => {
                int barMaxW = fila.Width - 64;
                int barW = (int) (barMaxW * porcentajeBarra / 100f);
                int barX = 32;
                int barY = fila.Height - 4;
                var colorBarra = Color.FromArgb(200, 255, 218, 185);

                e.Graphics.FillRectangle(new SolidBrush(colorBarra),
                    new Rectangle(barX, barY, barW, 3));

                // Separador inferior
                e.Graphics.DrawLine(
                    new Pen(Color.FromArgb(228, 228, 228)),
                    0, fila.Height - 1, fila.Width, fila.Height - 1);
            };

            return fila;
        }

        public void RenderizarGraficosMovimientosAlmacenes() {
            fieldEvolucionMovimientos.Refresh();
            fieldValorAlmacen.Refresh();
        }
    }
}