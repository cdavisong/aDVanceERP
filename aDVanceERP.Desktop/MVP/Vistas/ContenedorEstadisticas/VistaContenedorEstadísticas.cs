using System.Drawing.Drawing2D;
using System.Globalization;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Desktop.MVP.Vistas.ContenedorEstadisticas.Plantillas;
using aDVanceERP.Desktop.Properties;

namespace aDVanceERP.Desktop.MVP.Vistas.ContenedorEstadisticas; 

public partial class VistaContenedorEstadísticas : Form, IVistaContenedorEstadisticas {
    private RepoEstadisticosVentas _datosEstadisticosVentas = new();

    public VistaContenedorEstadísticas() {
        InitializeComponent();

        NombreVista = nameof(VistaContenedorEstadísticas);

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

    public decimal CantidadProductosRegistrados {
        get => decimal.TryParse(fieldCantProductosRegistrados.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var cantidad) ? cantidad : 0;
        set => fieldCantProductosRegistrados.Text = value.ToString("N2", CultureInfo.InvariantCulture);
    }

    public decimal MontoInversionProductos {
        get => decimal.TryParse(fieldMontoInversionProductoss.Text.Remove(1, 2), NumberStyles.Any,
            CultureInfo.InvariantCulture, out var monto)
            ? monto
            : 0;
        set => fieldMontoInversionProductoss.Text = $"$ {value.ToString("N2", CultureInfo.InvariantCulture)}";
    }

    public decimal CantidadProductosVendidos {
        get => decimal.TryParse(fieldCantProductosVendidos.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var cantidad) ? cantidad : 0;
        set => fieldCantProductosVendidos.Text = value.ToString("N2", CultureInfo.InvariantCulture);
    }

    public decimal MontoVentaProductosVendidos {
        get => decimal.TryParse(fieldMontoVentaProductosVendidos.Text.Remove(1, 2), NumberStyles.Any,
            CultureInfo.InvariantCulture, out var monto)
            ? monto
            : 0;
        set => fieldMontoVentaProductosVendidos.Text = $"$ {value.ToString("N2", CultureInfo.InvariantCulture)}";
    }

    public decimal MontoGananciaTotalNegocio {
        get => decimal.TryParse(fieldGananciaTotalNegocio.Text.Remove(1, 2), NumberStyles.Any,
            CultureInfo.InvariantCulture, out var monto)
            ? monto
            : 0;
        set => fieldGananciaTotalNegocio.Text = $"$ {value.ToString("N2", CultureInfo.InvariantCulture)}";
    }

    public decimal MontoGananciaAcumuladaDia {
        get => decimal.TryParse(fieldGananciaAcumuladaDia.Text.Remove(1, 2), NumberStyles.Any,
            CultureInfo.InvariantCulture, out var monto)
            ? monto
            : 0;
        set => fieldGananciaAcumuladaDia.Text = $"$ {value.ToString("N2", CultureInfo.InvariantCulture)}";
    }

    public RepoEstadisticosVentas DatosEstadisticosVentas {
        get => _datosEstadisticosVentas;
        set {
            _datosEstadisticosVentas = value;

            fieldGraficoVentas.Invalidate();
        }
    }

    public DateTime FechaEstadisticasVentas {
        get => fieldDatoFecha.Value;
    }

    public event EventHandler? MostrarVistaGestionProductos;
    public event EventHandler? MostrarVistaGestionVentas;
    public event EventHandler? FechaEstadsticasModificada;
    

    public void Inicializar() {
        fieldCriterioEstadisticasVenta.Items.AddRange(new object[] {
            "Diario",
            "Mensual",
            "Anual"
        });

        // Eventos
        btnDescargarAnálisisVentas.Click += delegate {
            /*var filas = new List<string[]>();

            using (var datosVentas = new RepoVenta()) {
                var ventasFecha = UtilesVenta.ObtenerVentas;

                foreach (var venta in ventasFecha) {
                    using (var datosVentaProducto = new RepoDetalleVentaProducto()) {
                        var detalleVentaProducto = datosVentaProducto.Obtener(CriterioDetalleVentaProducto.IdVenta, venta.Id.ToString());

                        foreach (var ventaProducto in detalleVentaProducto) {
                            var fila = new string[6];

                            fila[0] = ventaProducto.Id.ToString();
                            fila[1] = UtilesProducto.ObtenerNombreProducto(ventaProducto.IdProducto).Result ?? string.Empty;
                            fila[2] = "U";
                            fila[3] = ventaProducto.PrecioVentaFinal.ToString("N2", CultureInfo.InvariantCulture);
                            fila[4] = ventaProducto.Cantidad.ToString();
                            fila[5] = (ventaProducto.PrecioVentaFinal * ventaProducto.Cantidad).ToString("N2", CultureInfo.InvariantCulture);

                            filas.Add(fila);
                        }
                    }
                }
            }

            UtilesReportes.GenerarReporteVentas(fieldDatoBusquedaFecha.Value, filas);*/
        };
        subLayout1EstadisticasProducto.Paint += (sender, e) => {
            e.Graphics.Clear(Color.PeachPuff);
            e.Graphics.DrawImageUnscaled(Resources.productE_96px,
                subLayout1EstadisticasProducto.Width - 96,
                subLayout1EstadisticasProducto.Height - 96);
        };
        subLayout1EstadisticasVentaProducto.Paint += (sender, e) => {
            e.Graphics.Clear(Color.PaleGoldenrod);
            e.Graphics.DrawImageUnscaled(Resources.best_sales_96px,
                subLayout1EstadisticasProducto.Width - 96,
                subLayout1EstadisticasProducto.Height - 96);
        };
        subLayout1EstadisticasGanancia.Paint += (sender, e) => {
            e.Graphics.Clear(Color.PaleTurquoise);
            e.Graphics.DrawImageUnscaled(Resources.accountF_96px,
                subLayout1EstadisticasGanancia.Width - 96,
                subLayout1EstadisticasGanancia.Height - 96);
        };
        btnGestionarProductos.Click += delegate(object? sender, EventArgs e) {
            MostrarVistaGestionProductos?.Invoke(sender, e);
        };
        btnGestionarVentas.Click += delegate(object? sender, EventArgs e) {
            MostrarVistaGestionVentas?.Invoke(sender, e);
        };
        // Suscripción al evento SelectedIndexChanged
        fieldCriterioEstadisticasVenta.SelectedIndexChanged += delegate {
            if (fieldCriterioEstadisticasVenta.SelectedItem == null)
                return;

            fieldDatoFecha.Format = DateTimePickerFormat.Custom;

            switch (fieldCriterioEstadisticasVenta.SelectedItem.ToString()) {
                case "Diario":
                    fieldDatoFecha.CustomFormat = "yyyy-MM-dd";
                    break;
                case "Mensual":
                    fieldDatoFecha.CustomFormat = "yyyy-MM";
                    break;
                case "Anual":
                    fieldDatoFecha.CustomFormat = "yyyy";
                    break;
                default:
                    fieldDatoFecha.CustomFormat = "yyyy-MM-dd"; // Formato por defecto
                    break;
            }

            fieldDatoFecha.Value = DateTime.Today; // Reiniciar la fecha al día actual
            fieldGraficoVentas.Invalidate(); // Forzar la actualización del gráfico
        };

        // Suscripción al evento ValueChanged
        fieldDatoFecha.ValueChanged += delegate(object? sender, EventArgs args) {
            // Invocar el evento si está suscrito
            FechaEstadsticasModificada?.Invoke(FechaEstadisticasVentas, args);

            // Forzar la actualización del gráfico
            fieldGraficoVentas.Invalidate();
        };

        // Suscripción al evento Paint
        fieldGraficoVentas.Paint += RenderizarGraficoVentas;

        // Suscripción al evento Resize
        fieldGraficoVentas.Resize += delegate {
            fieldGraficoVentas.Invalidate(); // Redibujar el gráfico al cambiar el tamaño
        };
    }

    public void Mostrar() {
        fieldTituloProductosVendidos.Text = $"Productos vendidos hoy {DateTime.Now.ToString("dd/MM/yyyy")}";
        fieldCriterioEstadisticasVenta.StartIndex = 0;

        BringToFront();
        Show();
    }

    public void Restaurar() { }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() { }

    private void RenderizarGraficoVentas(object? sender, PaintEventArgs e) {
        // Validar que se haya seleccionado un criterio de búsqueda
        if (fieldCriterioEstadisticasVenta.SelectedIndex == -1 || fieldCriterioEstadisticasVenta.SelectedItem == null)
            return;

        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(fieldGraficoVentas.BackColor);

        // Dibujar la imagen de fondo
        g.DrawImageUnscaled(Resources.bar_chartF_96px,
            fieldGraficoVentas.Width - 96, // Posición X (esquina inferior derecha)
            fieldGraficoVentas.Height - 96); // Posición Y (esquina inferior derecha)

        // Configuración inicial
        var margen = 40;
        var ancho = fieldGraficoVentas.Width - 2 * margen;
        var altura = fieldGraficoVentas.Height - 2 * margen;

        // Dibujar ejes
        g.DrawLine(Pens.Gray, margen, margen, margen, margen + altura);
        g.DrawLine(Pens.Gray, margen, margen + altura, margen + ancho, margen + altura);

        // Obtener datos según selección
        var tipoGrafico = fieldCriterioEstadisticasVenta.SelectedItem.ToString();
        var fechaSeleccionada = fieldDatoFecha.Value;

        // Obtener datos para el gráfico
        var datosGrafico = ObtenerDatosGrafico(tipoGrafico, fechaSeleccionada);

        if (datosGrafico.Count == 0)
            return;

        // Calcular escalas
        var valorMaximo = datosGrafico.Values.Max();
        if (valorMaximo == 0) return; // Evitar división por cero

        var escalaY = altura / (float)valorMaximo;
        var pasoX = ancho / (float)datosGrafico.Count;

        // Dibujar barras y montos
        var i = 0;
        foreach (var dato in datosGrafico) {
            var alturaBarra = (float)dato.Value * escalaY;
            var barra = new RectangleF(
                margen + i * pasoX + 2, // Posición X corregida
                margen + altura - alturaBarra, // Posición Y
                pasoX - 4, // Ancho de la barra
                alturaBarra // Altura de la barra
            );

            // Dibujar la barra
            g.FillRectangle(Brushes.DarkGray, barra);

            // Dibujar el monto sobre la barra
            if (dato.Value > 0) {
                var montoFormateado = dato.Value.ToString("N2"); // Formato numérico
                var tamanoTexto = g.MeasureString(montoFormateado, Font);

                // Posición X del texto (centrado sobre la barra)
                var posXTexto = margen + i * pasoX + (pasoX - tamanoTexto.Width) / 2;

                // Verificar si el texto se superpone con la siguiente barra
                if (i < datosGrafico.Count - 1) {
                    // No es la última barra
                    var siguienteBarraX = margen + (i + 1) * pasoX + 2; // Posición X de la siguiente barra
                    if (posXTexto + tamanoTexto.Width > siguienteBarraX)
                        // Ajustar la posición X del texto para evitar superposición
                        posXTexto = siguienteBarraX - tamanoTexto.Width - 2; // Margen de 2 píxeles
                }

                // Posición Y del texto (por encima de la barra)
                var posYTexto = barra.Top - tamanoTexto.Height - 5; // 5 píxeles de margen superior

                // Dibujar el texto
                g.DrawString(montoFormateado, new Font(Font, FontStyle.Bold), Brushes.Black, posXTexto, posYTexto);
            }

            i++;
        }

        // Calcular el promedio de ventas
        var promedioVentas = datosGrafico.Values.Average();
        var alturaPromedio = (float)promedioVentas * escalaY;

        // Dibujar la línea del promedio
        var colorPromedio = Color.Black; // Color de la línea del promedio
        var penPromedio = new Pen(colorPromedio, 2); // Línea de 2 píxeles de grosor
        g.DrawLine(penPromedio, margen, margen + altura - alturaPromedio, margen + ancho,
            margen + altura - alturaPromedio);

        // Dibujar la etiqueta del promedio
        var textoPromedio = $"Promedio: {promedioVentas.ToString("N2")}"; // Formato numérico con 2 decimales
        var tamanoTextoPromedio = g.MeasureString(textoPromedio, Font);
        var posXTextoPromedio = margen + ancho - tamanoTextoPromedio.Width - 10; // Esquina superior derecha
        var posYTextoPromedio = margen + altura - alturaPromedio - tamanoTextoPromedio.Height - 5; // Encima de la línea

        g.DrawString(textoPromedio, new Font(Font, FontStyle.Bold), new SolidBrush(colorPromedio), posXTextoPromedio,
            posYTextoPromedio);

        // Dibujar etiquetas
        i = 0;
        foreach (var dato in datosGrafico) {
            var posX = margen + i * pasoX + pasoX / 2;

            // Ajustar posición de la etiqueta
            var formato = new StringFormat {
                Alignment = StringAlignment.Center // Centrar el texto
            };

            g.DrawString(dato.Key, Font, Brushes.Black, posX, margen + altura + 5, formato);
            i++;
        }
    }

    private Dictionary<string, decimal> ObtenerDatosGrafico(string? tipoGrafico, DateTime fechaSeleccionada) {
        var resultado = new Dictionary<string, decimal>();

        if (_datosEstadisticosVentas == null)
            return resultado;

        switch (tipoGrafico) {
            case "Diario":
                foreach (var dato in _datosEstadisticosVentas.VentasPorHora)
                    resultado.Add(dato.Key.ToString("HH"), dato.Value);
                break;
            case "Mensual":
                foreach (var dato in _datosEstadisticosVentas.VentasPorDia)
                    resultado.Add(dato.Key.Day.ToString("00"), dato.Value);
                break;
            case "Anual":
                foreach (var dato in _datosEstadisticosVentas.VentasPorMes) {
                    var cultura = new CultureInfo("es-ES");
                    var abreviaturaMes = cultura.DateTimeFormat.GetAbbreviatedMonthName(dato.Key.Month);

                    resultado.Add(abreviaturaMes, dato.Value);
                }

                break;
        }

        return resultado;
    }
}