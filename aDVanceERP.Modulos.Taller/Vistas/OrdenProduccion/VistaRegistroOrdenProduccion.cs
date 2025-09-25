using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Utiles;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.Taller.Interfaces;
using aDVanceERP.Modulos.Taller.Modelos;
using aDVanceERP.Modulos.Taller.Repositorios;

using System.Data;
using System.Globalization;

namespace aDVanceERP.Modulos.Taller.Vistas.OrdenProduccion
{
    public partial class VistaRegistroOrdenProduccion : Form, IVistaRegistroOrdenProduccion {
        private bool _habilitada;
        private bool _modoEdicion;
        private string _numeroOrden = "-";
        private DateTime _fechaApertura = DateTime.Now;

        public VistaRegistroOrdenProduccion() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroOrdenProduccion);
            PanelMateriaPrima = new RepoVistaBase(contenedorVistasMateriaPrima);
            PanelActividadesProduccion = new RepoVistaBase(contenedorVistasActividadesProduccion);
            PanelGastosIndirectos = new RepoVistaBase(contenedorVistasGastosIndirectos);

            Inicializar();
        }

        public string NombreVista {
            get => Name;
            private set => Name = value;
        }

        public bool Habilitada {
            get => _habilitada;
            set {
                fieldNombreProductoTerminado.ReadOnly = !value;
                fieldCantidadProducir.ReadOnly = !value;
                fieldMargenGananciaDeseado.ReadOnly = !value;
                fieldObservaciones.ReadOnly = !value;
                fieldNombreMateriaPrima.ReadOnly = !value;
                fieldCantidadMateriaPrima.ReadOnly = !value;
                fieldNombreActividadProduccion.ReadOnly = !value;
                fieldCantidadActividadesProduccion.ReadOnly = !value;
                fieldConceptoGastoIndirecto.ReadOnly = !value;
                fieldCantidadGastoIndirecto.ReadOnly = !value;
                btnAbrirActualizarOrdenProduccion.Visible = value;

                _habilitada = value;
            }
        }

        public Point Coordenadas {
            get => Location;
            set => Location = value;
        }

        public Size Dimensiones {
            get => Size;
            set => Size = value;
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
                fieldSubtitulo.Text = $"Registro Nro. {_numeroOrden} del {FechaApertura:dd/MM/yyyy}";
                btnAbrirActualizarOrdenProduccion.Text = value ? "Actualizar orden de producción" : "Abrir orden de producción";
                _modoEdicion = value;
            }
        }

        public long Id { get; set; }

        public string NumeroOrden {
            get => _numeroOrden;
            set {
                _numeroOrden = value;
                fieldSubtitulo.Text = $"Registro Nro. {_numeroOrden} del {FechaApertura:dd/MM/yyyy}";
            }
        }

        public DateTime FechaApertura {
            get => _fechaApertura;
            set {
                _fechaApertura = value;
                fieldSubtitulo.Text = $"Registro Nro. {NumeroOrden} del {FechaApertura:dd/MM/yyyy}";
            }
        }

        public string NombreProductoTerminado {
            get => fieldNombreProductoTerminado.Text;
            set => fieldNombreProductoTerminado.Text = value;
        }

        public string NombreAlmacenDestino {
            get => fieldNombreAlmacenDestino.SelectedIndex >= 0 ? fieldNombreAlmacenDestino.SelectedItem?.ToString() ?? string.Empty : string.Empty;
            set {
                if (fieldNombreAlmacenDestino.Items.Contains(value))
                    fieldNombreAlmacenDestino.SelectedItem = value;
                else
                    fieldNombreAlmacenDestino.SelectedIndex = -1;
            }
        }

        public decimal Cantidad {
            get => decimal.TryParse(fieldCantidadProducir.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var cantidad) ? cantidad : 0m;
            set => fieldCantidadProducir.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public string Observaciones {
            get => fieldObservaciones.Text;
            set => fieldObservaciones.Text = value;
        }

        public decimal CostoTotal {
            get => decimal.TryParse(fieldCostoTotalProduccion.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var costoTotal) ? costoTotal : 0m;
            set => fieldCostoTotalProduccion.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public decimal PrecioUnitario {
            get => decimal.TryParse(fieldPrecioUnitarioProducto.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var precioUnitario) ? precioUnitario : 0m;
            set => fieldPrecioUnitarioProducto.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public decimal MargenGanancia {
            get => decimal.TryParse(fieldMargenGananciaDeseado.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var margen) ? margen : 0m;
            set => fieldMargenGananciaDeseado.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public string NombreAlmacenMateriales {
            get => fieldNombreAlmacenMateriales.SelectedIndex >= 0 ? fieldNombreAlmacenMateriales.SelectedItem?.ToString() ?? string.Empty : string.Empty;
            private set {
                if (fieldNombreAlmacenMateriales.Items.Contains(value))
                    fieldNombreAlmacenMateriales.SelectedItem = value;
                else
                    fieldNombreAlmacenMateriales.SelectedIndex = -1;
            }
        }

        public RepoVistaBase PanelMateriaPrima { get; private set; }
        public List<string[]> MateriasPrimas { get; private set; } = new List<string[]>();

        public RepoVistaBase PanelActividadesProduccion { get; private set; }
        public List<string[]> ActividadesProduccion { get; private set; } = new List<string[]>();

        public RepoVistaBase PanelGastosIndirectos { get; private set; }
        public List<string[]> GastosIndirectos { get; private set; } = new List<string[]>();

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;
        public event EventHandler? MateriaPrimaEliminada;
        public event EventHandler? ActividadProduccionEliminada;
        public event EventHandler? GastoIndirectoEliminado;


        public void Inicializar() {
            // Eventos
            #region Materias primas

            fieldNombreMateriaPrima.TextChanged += delegate (object? sender, EventArgs args) {
                btnAdicionarMateriaPrima.Enabled = !string.IsNullOrWhiteSpace(fieldNombreMateriaPrima.Text) && !string.IsNullOrWhiteSpace(fieldCantidadMateriaPrima.Text);
            };
            fieldNombreMateriaPrima.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode == Keys.Enter) {
                    args.SuppressKeyPress = true;
                    fieldCantidadMateriaPrima.Focus();
                }
            };
            fieldCantidadMateriaPrima.TextChanged += delegate (object? sender, EventArgs args) {
                btnAdicionarMateriaPrima.Enabled = !string.IsNullOrWhiteSpace(fieldNombreMateriaPrima.Text) && !string.IsNullOrWhiteSpace(fieldCantidadMateriaPrima.Text);
            };
            fieldCantidadMateriaPrima.KeyPress += delegate (object? sender, KeyPressEventArgs args) {
                if (!char.IsControl(args.KeyChar) && !char.IsDigit(args.KeyChar) && (args.KeyChar != '.')) {
                    args.Handled = true;
                }
                // Permitir un solo punto decimal
                if ((args.KeyChar == '.') && ((sender as TextBox)?.Text.IndexOf('.') > -1)) {
                    args.Handled = true;
                }
            };
            fieldCantidadMateriaPrima.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode == Keys.Enter) {
                    args.SuppressKeyPress = true;
                    AdicionarMateriaPrima();
                }
            };
            btnAdicionarMateriaPrima.Click += delegate (object? sender, EventArgs args) {
                AdicionarMateriaPrima();
            };
            MateriaPrimaEliminada += delegate (object? sender, EventArgs args) {
                ActualizarTuplasMateriaPrima();
            };

            #endregion
            #region Actividades de produccion

            fieldNombreActividadProduccion.TextChanged += delegate (object? sender, EventArgs args) {
                btnAdicionarActividadProduccion.Enabled = !string.IsNullOrWhiteSpace(fieldNombreActividadProduccion.Text) && !string.IsNullOrWhiteSpace(fieldCantidadActividadesProduccion.Text);
            };
            fieldNombreActividadProduccion.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode == Keys.Enter) {
                    args.SuppressKeyPress = true;
                    fieldCantidadActividadesProduccion.Focus();
                }
            };
            fieldCantidadActividadesProduccion.TextChanged += delegate (object? sender, EventArgs args) {
                btnAdicionarActividadProduccion.Enabled = !string.IsNullOrWhiteSpace(fieldNombreActividadProduccion.Text) && !string.IsNullOrWhiteSpace(fieldCantidadActividadesProduccion.Text);
            };
            fieldCantidadActividadesProduccion.KeyPress += delegate (object? sender, KeyPressEventArgs args) {
                if (!char.IsControl(args.KeyChar) && !char.IsDigit(args.KeyChar) && (args.KeyChar != '.')) {
                    args.Handled = true;
                }
                // Permitir un solo punto decimal
                if ((args.KeyChar == '.') && ((sender as TextBox)?.Text.IndexOf('.') > -1)) {
                    args.Handled = true;
                }
            };
            fieldCantidadActividadesProduccion.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode == Keys.Enter) {
                    args.SuppressKeyPress = true;
                    AdicionarActividadProduccion();
                }
            };
            btnAdicionarActividadProduccion.Click += delegate (object? sender, EventArgs args) {
                AdicionarActividadProduccion();
            };
            ActividadProduccionEliminada += delegate (object? sender, EventArgs args) {
                ActualizarTuplasActividadesProduccion();
            };

            #endregion
            #region Gastos indirectos

            fieldConceptoGastoIndirecto.TextChanged += delegate (object? sender, EventArgs args) {
                btnAdicionarGastoIndirecto.Enabled = !string.IsNullOrWhiteSpace(fieldConceptoGastoIndirecto.Text) && !string.IsNullOrWhiteSpace(fieldCantidadGastoIndirecto.Text);
            };
            fieldConceptoGastoIndirecto.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode == Keys.Enter) {
                    args.SuppressKeyPress = true;
                    fieldCantidadGastoIndirecto.Focus();
                }
            };
            fieldCantidadGastoIndirecto.TextChanged += delegate (object? sender, EventArgs args) {
                btnAdicionarGastoIndirecto.Enabled = !string.IsNullOrWhiteSpace(fieldConceptoGastoIndirecto.Text) && !string.IsNullOrWhiteSpace(fieldCantidadGastoIndirecto.Text);
            };
            fieldCantidadGastoIndirecto.KeyPress += delegate (object? sender, KeyPressEventArgs args) {
                if (!char.IsControl(args.KeyChar) && !char.IsDigit(args.KeyChar) && (args.KeyChar != '.')) {
                    args.Handled = true;
                }
                // Permitir un solo punto decimal
                if ((args.KeyChar == '.') && ((sender as TextBox)?.Text.IndexOf('.') > -1)) {
                    args.Handled = true;
                }
            };
            fieldCantidadGastoIndirecto.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode == Keys.Enter) {
                    args.SuppressKeyPress = true;
                    btnAdicionarGastoIndirecto.ContextMenuStrip?.Show(btnAdicionarGastoIndirecto, new Point(
                    btnAdicionarGastoIndirecto.Width - btnAdicionarGastoIndirecto.ContextMenuStrip.Width,
                    45));
                }
            };
            btnAdicionarGastoIndirecto.Click += delegate (object? sender, EventArgs args) {
                btnAdicionarGastoIndirecto.ContextMenuStrip?.Show(btnAdicionarGastoIndirecto, new Point(
                    btnAdicionarGastoIndirecto.Width - btnAdicionarGastoIndirecto.ContextMenuStrip.Width,
                    45));
            };
            btnInsertarGastoNormal.Click += delegate (object? sender, EventArgs args) {
                InsertarGastoIndirectoNormal();
            };
            btnInsertarGastoDinamico.Click += delegate (object? sender, EventArgs args) {
                InsertarGastoIndirectoDinamico();
            };
            GastoIndirectoEliminado += delegate (object? sender, EventArgs args) {
                ActualizarTuplasGastosIndirectos();
            };

            #endregion

            fieldNombreAlmacenMateriales.SelectedIndexChanged += delegate (object? sender, EventArgs args) {
                CargarNombresMateriasPrimas(UtilesProducto.ObtenerNombresProductos(
                    UtilesAlmacen.ObtenerIdAlmacen(NombreAlmacenMateriales).Result,
                    "MateriaPrima").Result);
            };
            fieldNombreAlmacenDestino.SelectedIndexChanged += delegate (object? sender, EventArgs args) {
                CargarNombresProductosTerminados(UtilesProducto.ObtenerNombresProductos(0,
                    "ProductoTerminado").Result);
            };
            fieldCantidadProducir.KeyPress += delegate (object? sender, KeyPressEventArgs args) {
                if (!char.IsControl(args.KeyChar) && !char.IsDigit(args.KeyChar) && (args.KeyChar != '.')) {
                    args.Handled = true;
                }
                // Permitir un solo punto decimal
                if ((args.KeyChar == '.') && ((sender as TextBox)?.Text.IndexOf('.') > -1)) {
                    args.Handled = true;
                }
            };
            fieldCantidadProducir.TextChanged += delegate (object? sender, EventArgs args) {
                ActualizarPrecioUnitarioProducto();
            };
            fieldCantidadProducir.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode == Keys.Enter) {
                    args.SuppressKeyPress = true;
                    fieldMargenGananciaDeseado.Focus();
                }
            };
            fieldMargenGananciaDeseado.KeyPress += delegate (object? sender, KeyPressEventArgs args) {
                if (!char.IsControl(args.KeyChar) && !char.IsDigit(args.KeyChar) && (args.KeyChar != '.')) {
                    args.Handled = true;
                }
                // Permitir un solo punto decimal
                if ((args.KeyChar == '.') && ((sender as TextBox)?.Text.IndexOf('.') > -1)) {
                    args.Handled = true;
                }
            };
            fieldMargenGananciaDeseado.TextChanged += delegate (object? sender, EventArgs args) {
                ActualizarPrecioUnitarioProducto();
            };
            btnAbrirActualizarOrdenProduccion.Click += delegate (object? sender, EventArgs args) {
                if (ModoEdicion)
                    EditarEntidad?.Invoke(sender, args);
                else
                    RegistrarEntidad?.Invoke(sender, args);
            };
            btnSalir.Click += delegate (object? sender, EventArgs args) {
                Ocultar();
            };
            //contenedorVistasMateriaPrima.Resize += delegate {
            //    VistasMateriaPrima?.Vistas?.ForEach(vista => {
            //        if (vista is IVistaTupla tupla)
            //            tupla.Dimensiones = new Size(contenedorVistasMateriaPrima.Width - 20, VariablesGlobales.AlturaTuplaPredeterminada);
            //    });
            //};
        }

        #region Almacenes

        public void CargarNombresAlmacenesMateriales(object[] nombresAlmacenes) {
            fieldNombreAlmacenMateriales.Items.Clear();
            fieldNombreAlmacenMateriales.Items.AddRange(nombresAlmacenes);
            fieldNombreAlmacenMateriales.SelectedIndex = fieldNombreAlmacenMateriales.Items.Count > 0 ? 0 : -1;
        }

        public void CargarNombresAlmacenesDestino(object[] nombresAlmacenes) {
            fieldNombreAlmacenDestino.Items.Clear();
            fieldNombreAlmacenDestino.Items.AddRange(nombresAlmacenes);
            fieldNombreAlmacenDestino.SelectedIndex = fieldNombreAlmacenDestino.Items.Count > 0 ? 0 : -1;
        }

        #endregion
        #region Autocompletamiento en campos

        public void CargarNombresProductosTerminados(string[] nombresProductosTerminados) {
            fieldNombreProductoTerminado.AutoCompleteCustomSource.Clear();
            fieldNombreProductoTerminado.AutoCompleteCustomSource.AddRange(nombresProductosTerminados);
            fieldNombreProductoTerminado.AutoCompleteMode = AutoCompleteMode.Suggest;
            fieldNombreProductoTerminado.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public void CargarNombresMateriasPrimas(string[] nombresMateriasPrimas) {
            fieldNombreMateriaPrima.AutoCompleteCustomSource.Clear();
            fieldNombreMateriaPrima.AutoCompleteCustomSource.AddRange(nombresMateriasPrimas);
            fieldNombreMateriaPrima.AutoCompleteMode = AutoCompleteMode.Suggest;
            fieldNombreMateriaPrima.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public void CargarNombresActividadesProduccion(string[] nombresActividadesProduccion) {
            fieldNombreActividadProduccion.AutoCompleteCustomSource.Clear();
            fieldNombreActividadProduccion.AutoCompleteCustomSource.AddRange(nombresActividadesProduccion);
            fieldNombreActividadProduccion.AutoCompleteMode = AutoCompleteMode.Suggest;
            fieldNombreActividadProduccion.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public void CargarConceptosGastosIndirectos(string[] conceptosGastosIndirectos) {
            fieldConceptoGastoIndirecto.AutoCompleteCustomSource.Clear();
            fieldConceptoGastoIndirecto.AutoCompleteCustomSource.AddRange(conceptosGastosIndirectos);
            fieldConceptoGastoIndirecto.AutoCompleteMode = AutoCompleteMode.Suggest;
            fieldConceptoGastoIndirecto.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        #endregion

        public void AdicionarMateriaPrima(string nombreAlmacen = "", string nombre = "", decimal cantidad = 0) {
            var adNombreAlmacen = string.IsNullOrEmpty(nombreAlmacen) ? NombreAlmacenMateriales : nombreAlmacen;
            var adNombre = string.IsNullOrEmpty(nombre) ? fieldNombreMateriaPrima.Text : nombre;

            if (!string.IsNullOrEmpty(adNombre)) {
                var idProducto = UtilesProducto.ObtenerIdProducto(adNombre).Result;

                if (idProducto <= 0) {
                    CentroNotificaciones.Mostrar($"No se encontró la materia prima '{adNombre}'.", Core.Mensajes.MVP.Modelos.TipoNotificacion.Error);
                    return;
                }

                var stockProducto = UtilesProducto.ObtenerStockProducto(adNombre, adNombreAlmacen).Result;
                var cantidadAcumulada = MateriasPrimas
                    .Where(p => p[1].Equals(adNombre))
                    .Sum(p => decimal.TryParse(p[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var cant) ? cant : 0m);
                var adCantidad = cantidad > 0 ? cantidad : decimal.TryParse(fieldCantidadMateriaPrima.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var cant) ? cant : 0m;
                var adCantidadTotal = adCantidad + cantidadAcumulada;

                if (!ModoEdicion && stockProducto < adCantidadTotal) {
                    CentroNotificaciones.Mostrar($"No hay suficiente cantidad de la materia prima '{adNombre}' para satisfacer la demanda de fabricación especificada. Stock actual: {stockProducto}.", Core.Mensajes.MVP.Modelos.TipoNotificacion.Error);
                    return;
                }

                var adPrecio = UtilesProducto.ObtenerPrecioCompra(idProducto).Result;
                var tuplaMateriaPrimaExistente = MateriasPrimas.FirstOrDefault(p => p[1].Equals(adNombre));
                var tuplaMateriaPrima = tuplaMateriaPrimaExistente
                    ?? [adNombreAlmacen, adNombre, "0", adPrecio.ToString("N2", CultureInfo.InvariantCulture)];
                tuplaMateriaPrima[2] = adCantidadTotal.ToString("N2", CultureInfo.InvariantCulture);

                if (tuplaMateriaPrimaExistente == null)
                    MateriasPrimas.Add(tuplaMateriaPrima);

                fieldNombreMateriaPrima.Text = string.Empty;
                fieldCantidadMateriaPrima.Text = string.Empty;

                ActualizarTuplasMateriaPrima();

                fieldNombreMateriaPrima.Focus();
            } else
                CentroNotificaciones.Mostrar("Debe ingresar un nombre válido para la materia prima.", Core.Mensajes.MVP.Modelos.TipoNotificacion.Error);
        }

        public void AdicionarActividadProduccion(string nombre = "", decimal cantidad = 0) {
            var adNombre = string.IsNullOrEmpty(nombre) ? fieldNombreActividadProduccion.Text : nombre;

            if (!string.IsNullOrEmpty(adNombre)) {
                var cantidadAcumulada = ActividadesProduccion
                    .Where(p => p[0].Equals(adNombre))
                    .Sum(p => decimal.TryParse(p[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var cant) ? cant : 0m);
                var adCantidad = cantidad > 0 ? cantidad : decimal.TryParse(fieldCantidadActividadesProduccion.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var cant) ? cant : 0m;
                var adCantidadTotal = adCantidad + cantidadAcumulada;

                if (adCantidad <= 0) {
                    CentroNotificaciones.Mostrar("La cantidad de la actividades de producción debe ser mayor a cero.", Core.Mensajes.MVP.Modelos.TipoNotificacion.Error);
                    return;
                }

                var tuplaActividadProduccionExistente = ActividadesProduccion.FirstOrDefault(p => p[0].Equals(adNombre));
                var costoActividadProduccion = UtilesOrdenProduccion.ObtenerCostoOrdenActividadProduccion(Id, adNombre);
                var tuplaActividadProduccion = tuplaActividadProduccionExistente
                    ?? [adNombre, "0", costoActividadProduccion <= 0 ? "1.00" : costoActividadProduccion.ToString("N2", CultureInfo.InvariantCulture)];
                tuplaActividadProduccion[1] = adCantidadTotal.ToString("N2", CultureInfo.InvariantCulture);

                if (tuplaActividadProduccionExistente == null)
                    ActividadesProduccion.Add(tuplaActividadProduccion);

                fieldNombreActividadProduccion.Text = string.Empty;
                fieldCantidadActividadesProduccion.Text = string.Empty;

                ActualizarTuplasActividadesProduccion();

                fieldNombreActividadProduccion.Focus();
            } else
                CentroNotificaciones.Mostrar("Debe ingresar un nombre válido para la actividad de producción.", Core.Mensajes.MVP.Modelos.TipoNotificacion.Error);
        }

        public void InsertarGastoIndirectoNormal(string concepto = "", decimal cantidad = 0) {
            var adConcepto = string.IsNullOrEmpty(concepto) ? fieldConceptoGastoIndirecto.Text : concepto;

            if (!string.IsNullOrEmpty(adConcepto)) {
                var cantidadAcumulada = GastosIndirectos
                    .Where(p => p[0].Equals(adConcepto))
                    .Sum(p => decimal.TryParse(p[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var cant) ? cant : 0m);
                var adCantidad = cantidad > 0 ? cantidad : decimal.TryParse(fieldCantidadGastoIndirecto.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var cant) ? cant : 0m;
                var adCantidadTotal = adCantidad + cantidadAcumulada;

                if (adCantidad <= 0) {
                    CentroNotificaciones.Mostrar("La cantidad del gasto indirecto debe ser mayor a cero.", Core.Mensajes.MVP.Modelos.TipoNotificacion.Error);
                    return;
                }

                var tuplaGastoIndirectoExistente = GastosIndirectos.FirstOrDefault(p => p[0].Equals(adConcepto));
                var adMonto = UtilesOrdenProduccion.ObtenerCostoOrdenGastoIndirecto(Id, adConcepto);
                var tuplaGastoIndirecto = tuplaGastoIndirectoExistente
                    ?? [adConcepto, "0", adMonto <= 0 ? "1.00" : adMonto.ToString("N2", CultureInfo.InvariantCulture)];
                tuplaGastoIndirecto[1] = adCantidadTotal.ToString("N2", CultureInfo.InvariantCulture);

                if (tuplaGastoIndirectoExistente == null)
                    GastosIndirectos.Add(tuplaGastoIndirecto);

                fieldConceptoGastoIndirecto.Text = string.Empty;
                fieldCantidadGastoIndirecto.Text = string.Empty;

                ActualizarTuplasGastosIndirectos();

                fieldConceptoGastoIndirecto.Focus();
            } else
                CentroNotificaciones.Mostrar("Debe ingresar un concepto válido para el gasto indirecto.", Core.Mensajes.MVP.Modelos.TipoNotificacion.Error);
        }

        public void InsertarGastoIndirectoDinamico(string concepto = "", decimal cantidad = 0, string ecuacion = "") {
            var adConcepto = string.IsNullOrEmpty(concepto) ? fieldConceptoGastoIndirecto.Text : concepto;
            var adEcuacion = string.IsNullOrEmpty(ecuacion) ? string.Empty : ecuacion;

            if (string.IsNullOrEmpty(concepto) || string.IsNullOrEmpty(ecuacion)) {
                var conceptosValidos = new List<string> {
                    "Costo total en materiales",
                    "Costo total en actividades"
                };

                conceptosValidos.AddRange(GastosIndirectos.Select(g => g[0]));

                var vistaRegistroGastoDinamico = new VistaRegistroGastoDinamico(conceptosValidos);

                vistaRegistroGastoDinamico.Location = new Point((Screen.PrimaryScreen?.Bounds.Right ?? 0) - vistaRegistroGastoDinamico.Width, VariablesGlobales.AlturaBarraTituloPredeterminada);
                vistaRegistroGastoDinamico.Size = new Size(vistaRegistroGastoDinamico.Size.Width, Size.Height + 10);
                vistaRegistroGastoDinamico.RegistrarDatos += delegate (object? sender, EventArgs args) {
                    adEcuacion = vistaRegistroGastoDinamico.Ecuacion;
                };

                vistaRegistroGastoDinamico.ShowDialog();
            }

            if (!string.IsNullOrEmpty(adConcepto)) {
                var cantidadAcumulada = GastosIndirectos
                    .Where(p => p[0].Equals(adConcepto))
                    .Sum(p => decimal.TryParse(p[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var cant) ? cant : 0m);
                var adCantidad = cantidad > 0 ? cantidad : decimal.TryParse(fieldCantidadGastoIndirecto.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var cant) ? cant : 0m;
                var adCantidadTotal = adCantidad + cantidadAcumulada;

                if (adCantidad <= 0) {
                    CentroNotificaciones.Mostrar("La cantidad del gasto indirecto debe ser mayor a cero.", Core.Mensajes.MVP.Modelos.TipoNotificacion.Error);
                    return;
                }

                var tuplaGastoIndirectoExistente = GastosIndirectos.FirstOrDefault(p => p[0].Equals(adConcepto));
                var tuplaGastoIndirecto = tuplaGastoIndirectoExistente ?? new string[4];
                tuplaGastoIndirecto[0] = adConcepto;
                tuplaGastoIndirecto[1] = adCantidad.ToString("N2", CultureInfo.InvariantCulture);
                tuplaGastoIndirecto[2] = "0.00"; // El monto se calculará dinámicamente
                tuplaGastoIndirecto[3] = adEcuacion; // Almacenar la ecuación en la posición 3

                if (tuplaGastoIndirectoExistente == null)
                    GastosIndirectos.Add(tuplaGastoIndirecto);

                fieldConceptoGastoIndirecto.Text = string.Empty;
                fieldCantidadGastoIndirecto.Text = string.Empty;

                ActualizarTuplasGastosIndirectos();

                fieldConceptoGastoIndirecto.Focus();
            } else
                CentroNotificaciones.Mostrar("Debe ingresar un concepto válido para el gasto indirecto.", Core.Mensajes.MVP.Modelos.TipoNotificacion.Error);
        }

        public void Mostrar() {
            // Datos
            FechaApertura = DateTime.Now;
            NumeroOrden = UtilesOrdenProduccion.ObtenerProximoNumeroOrden();

            BringToFront();
            Show();
        }

        public void Restaurar() {
            ModoEdicion = false;
            fieldNombreProductoTerminado.Text = string.Empty;
            fieldNombreAlmacenDestino.SelectedIndex = -1;
            fieldCantidadProducir.Text = string.Empty;
            fieldMargenGananciaDeseado.Text = string.Empty;
            fieldObservaciones.Text = string.Empty;
            fieldCostoTotalMateriales.Text = "0.00";
            fieldCostoTotalActividadesProduccion.Text = "0.00";
            fieldMontoTotalGastosIndirectos.Text = "0.00";
            fieldCostoTotalProduccion.Text = "0.00";
            fieldPrecioUnitarioProducto.Text = "0.00";
            fieldNombreMateriaPrima.Text = string.Empty;
            fieldCantidadMateriaPrima.Text = string.Empty;
            fieldNombreActividadProduccion.Text = string.Empty;
            fieldCantidadActividadesProduccion.Text = string.Empty;
            fieldConceptoGastoIndirecto.Text = string.Empty;
            fieldCantidadGastoIndirecto.Text = string.Empty;
            fieldNombreAlmacenMateriales.SelectedIndex = -1;

            MateriasPrimas.Clear();
            LimpiarTuplasContenedor(PanelMateriaPrima);
            ActividadesProduccion.Clear();
            LimpiarTuplasContenedor(PanelActividadesProduccion);
            GastosIndirectos.Clear();
            LimpiarTuplasContenedor(PanelGastosIndirectos);
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }

        private void LimpiarTuplasContenedor(RepoVistaBase panelTuplas) {
            panelTuplas.CerrarTodos();

            // Restablecer útima coordenada Y de la tupla
            VariablesGlobales.CoordenadaYUltimaTupla = 0;
        }

        private void ActualizarTuplasMateriaPrima() {
            LimpiarTuplasContenedor(PanelMateriaPrima);

            for (int i = 0; i < MateriasPrimas.Count; i++) {
                var materiaPrima = MateriasPrimas[i];
                var tuplaOrdenMateriaPrima = new VistaTuplaOrdenMateriaPrima();

                tuplaOrdenMateriaPrima.Indice = i;
                tuplaOrdenMateriaPrima.Habilitada = Habilitada;
                tuplaOrdenMateriaPrima.NombreAlmacen = materiaPrima[0];
                tuplaOrdenMateriaPrima.NombreMateriaPrima = materiaPrima[1];
                tuplaOrdenMateriaPrima.Cantidad = materiaPrima[2];
                tuplaOrdenMateriaPrima.PrecioUnitario = materiaPrima[3];
                tuplaOrdenMateriaPrima.Dimensiones = new Size(contenedorVistasMateriaPrima.Width - 20, VariablesGlobales.AlturaTuplaPredeterminada);
                tuplaOrdenMateriaPrima.PrecioUnitarioModificado += delegate (object? sender, EventArgs args) {
                    materiaPrima = sender as string[];

                    if (materiaPrima == null || materiaPrima.Length < 4)
                        return;

                    MateriasPrimas[MateriasPrimas.FindIndex(p => p[1].Equals(materiaPrima?[1]))] = materiaPrima;

                    ActualizarCostoTotalMateriales();
                };
                tuplaOrdenMateriaPrima.EliminarDatosTupla += delegate (object? sender, EventArgs args) {
                    materiaPrima = sender as string[];

                    using (var datosObjeto = new RepoOrdenMateriaPrima()) {
                        var materiaPrimaExistente = datosObjeto.Buscar(
                            FiltroBusquedaOrdenMateriaPrima.Producto,
                            $"{(Id == 0 ? UtilesBD.ObtenerUltimoIdTabla("orden_produccion") + 1 : Id)};{UtilesProducto.ObtenerIdProducto(materiaPrima?[0]).Result.ToString()}").resultados.FirstOrDefault();

                        if (materiaPrimaExistente != null)
                            datosObjeto.Eliminar(materiaPrimaExistente.Id);
                    }

                    MateriasPrimas.RemoveAt(MateriasPrimas.FindIndex(p => p[1].Equals(materiaPrima?[1])));
                    MateriaPrimaEliminada?.Invoke(materiaPrima, args);
                };

                // Registro y muestra
                PanelMateriaPrima?.Registrar(
                    tuplaOrdenMateriaPrima,
                    new Point(0, VariablesGlobales.CoordenadaYUltimaTupla),
                    new Size(contenedorVistasMateriaPrima.Width - 20, VariablesGlobales.AlturaTuplaPredeterminada), 
                    TipoRedimensionadoVista.Ninguno);
                tuplaOrdenMateriaPrima.Mostrar();

                // Incremento de la útima coordenada Y de la tupla
                VariablesGlobales.CoordenadaYUltimaTupla += VariablesGlobales.AlturaTuplaPredeterminada;
            }

            // Actualizar el costo total de los materiales
            ActualizarCostoTotalMateriales();
        }

        private void ActualizarCostoTotalMateriales() {
            var costoTotal = MateriasPrimas
                .Sum(p => decimal.TryParse(p[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var cantidad) && decimal.TryParse(p[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var precio) ? cantidad * precio : 0m);

            fieldCostoTotalMateriales.Text = costoTotal.ToString("N2", CultureInfo.InvariantCulture);

            ActualizarCostoTotalGastosIndirectos();
        }

        private void ActualizarTuplasActividadesProduccion() {
            LimpiarTuplasContenedor(PanelActividadesProduccion);

            for (int i = 0; i < ActividadesProduccion.Count; i++) {
                var actividadProduccion = ActividadesProduccion[i];
                var tuplaOrdenActividadProduccion = new VistaTuplaOrdenActividadProduccion();

                tuplaOrdenActividadProduccion.Indice = i;
                tuplaOrdenActividadProduccion.Habilitada = Habilitada;
                tuplaOrdenActividadProduccion.NombreActividadProduccion = actividadProduccion[0];
                tuplaOrdenActividadProduccion.Cantidad = actividadProduccion[1];
                tuplaOrdenActividadProduccion.CostoActividad = actividadProduccion[2];
                tuplaOrdenActividadProduccion.Dimensiones = new Size(contenedorVistasActividadesProduccion.Width - 20, VariablesGlobales.AlturaTuplaPredeterminada);
                tuplaOrdenActividadProduccion.CostoActividadModificado += delegate (object? sender, EventArgs args) {
                    actividadProduccion = sender as string[];

                    if (actividadProduccion == null || actividadProduccion.Length < 3)
                        return;

                    ActividadesProduccion[ActividadesProduccion.FindIndex(p => p[0].Equals(actividadProduccion?[0]))] = actividadProduccion;

                    ActualizarCostoTotalActividadesProduccion();
                };
                tuplaOrdenActividadProduccion.EliminarDatosTupla += delegate (object? sender, EventArgs args) {
                    actividadProduccion = sender as string[];

                    using (var datosObjeto = new RepoOrdenActividadProduccion()) {
                        var actividadProduccionExistente = datosObjeto.Buscar(
                            FiltroBusquedaOrdenActividadProduccion.Nombre,
                            $"{(Id == 0 ? UtilesBD.ObtenerUltimoIdTabla("orden_produccion") + 1 : Id)};{actividadProduccion?[0]}").resultados.FirstOrDefault();

                        if (actividadProduccionExistente != null)
                            datosObjeto.Eliminar(actividadProduccionExistente.Id);
                    }

                    ActividadesProduccion.RemoveAt(ActividadesProduccion.FindIndex(p => p[0].Equals(actividadProduccion?[0])));
                    ActividadProduccionEliminada?.Invoke(actividadProduccion, args);
                };

                // Registro y muestra
                PanelActividadesProduccion?.Registrar(
                    tuplaOrdenActividadProduccion,
                    new Point(0, VariablesGlobales.CoordenadaYUltimaTupla),
                    new Size(contenedorVistasActividadesProduccion.Width - 20, VariablesGlobales.AlturaTuplaPredeterminada), 
                    TipoRedimensionadoVista.Ninguno);

                tuplaOrdenActividadProduccion.Mostrar();

                // Incremento de la útima coordenada Y de la tupla
                VariablesGlobales.CoordenadaYUltimaTupla += VariablesGlobales.AlturaTuplaPredeterminada;
            }

            // Actualizar el costo total de las actividades de producción
            ActualizarCostoTotalActividadesProduccion();
        }

        private void ActualizarCostoTotalActividadesProduccion() {
            var costoTotal = ActividadesProduccion
                .Sum(p => decimal.TryParse(p[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var cantidad) && decimal.TryParse(p[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var costo) ? cantidad * costo : 0m);

            fieldCostoTotalActividadesProduccion.Text = costoTotal.ToString("N2", CultureInfo.InvariantCulture);


            ActualizarCostoTotalGastosIndirectos();
        }

        private void ActualizarTuplasGastosIndirectos() {
            LimpiarTuplasContenedor(PanelGastosIndirectos);

            for (int i = 0; i < GastosIndirectos.Count; i++) {
                var gastoIndirecto = GastosIndirectos[i];
                var tuplaOrdenGastoIndirecto = new VistaTuplaOrdenGastoIndirecto(gastoIndirecto.Length > 3);

                tuplaOrdenGastoIndirecto.Habilitada = Habilitada;
                tuplaOrdenGastoIndirecto.ConceptoGasto = gastoIndirecto[0];
                tuplaOrdenGastoIndirecto.Cantidad = gastoIndirecto[1];
                tuplaOrdenGastoIndirecto.Monto = gastoIndirecto[2];
                tuplaOrdenGastoIndirecto.Dimensiones = new Size(contenedorVistasGastosIndirectos.Width - 20, VariablesGlobales.AlturaTuplaPredeterminada);
                tuplaOrdenGastoIndirecto.MontoGastoModificado += delegate (object? sender, EventArgs args) {
                    gastoIndirecto = sender as string[];

                    if (gastoIndirecto == null || gastoIndirecto.Length < 3)
                        return;

                    GastosIndirectos[GastosIndirectos.FindIndex(p => p[0].Equals(gastoIndirecto?[0]))] = gastoIndirecto;

                    ActualizarCostoTotalGastosIndirectos();
                };
                tuplaOrdenGastoIndirecto.EliminarDatosTupla += delegate (object? sender, EventArgs args) {
                    gastoIndirecto = sender as string[];

                    using (var datosObjeto = new RepoOrdenGastoIndirecto()) {
                        var gastoIndirectoExistente = datosObjeto.Buscar(
                            FiltroBusquedaOrdenGastoIndirecto.Concepto,
                            $"{(Id == 0 ? UtilesBD.ObtenerUltimoIdTabla("orden_produccion") + 1 : Id)};{gastoIndirecto?[0]}").resultados.FirstOrDefault();

                        if (gastoIndirectoExistente != null)
                            datosObjeto.Eliminar(gastoIndirectoExistente.Id);
                    }

                    GastosIndirectos.RemoveAt(GastosIndirectos.FindIndex(p => p[0].Equals(gastoIndirecto?[0])));
                    GastoIndirectoEliminado?.Invoke(gastoIndirecto, args);
                };

                // Registro y muestra
                PanelGastosIndirectos?.Registrar(
                    tuplaOrdenGastoIndirecto,
                    new Point(0, VariablesGlobales.CoordenadaYUltimaTupla),
                    new Size(contenedorVistasGastosIndirectos.Width - 20, VariablesGlobales.AlturaTuplaPredeterminada), TipoRedimensionadoVista.Ninguno);
                tuplaOrdenGastoIndirecto.Mostrar();

                // Incremento de la útima coordenada Y de la tupla
                VariablesGlobales.CoordenadaYUltimaTupla += VariablesGlobales.AlturaTuplaPredeterminada;
            }

            // Actualizar el costo total de los gastos indirectos
            ActualizarCostoTotalGastosIndirectos();
        }

        private void ActualizarCostoTotalGastosIndirectos() {
            decimal montoTotal = 0;

            foreach (var gasto in GastosIndirectos) {
                if (gasto.Length > 3 && !string.IsNullOrEmpty(gasto[3])) {
                    // Es un gasto dinámico - calcular el monto basado en la ecuación
                    decimal montoDinamico = CalcularGastoDinamico(gasto[3]);

                    // Actualizar el monto en la tupla correspondiente
                    gasto[2] = montoDinamico.ToString("N2", CultureInfo.InvariantCulture);

                    var vistaTupla = PanelGastosIndirectos?.ObtenerPorId($"vistaTuplaVistaTuplaOrdenGastoIndirecto{GastosIndirectos.IndexOf(gasto)}") as VistaTuplaOrdenGastoIndirecto;
                    
                    if (vistaTupla != null) {
                        vistaTupla.Monto = gasto[2];
                    }

                    montoTotal += montoDinamico * decimal.Parse(gasto[1], NumberStyles.Any, CultureInfo.InvariantCulture);
                } else {
                    // Gasto normal
                    montoTotal += decimal.Parse(gasto[1], NumberStyles.Any, CultureInfo.InvariantCulture) * decimal.Parse(gasto[2], NumberStyles.Any, CultureInfo.InvariantCulture);
                }
            }

            fieldMontoTotalGastosIndirectos.Text = montoTotal.ToString("N2", CultureInfo.InvariantCulture);

            ActualizarCostoTotalProduccion();
        }

        private decimal CalcularGastoDinamico(string ecuacion) {
            try {
                // Reemplazar variables con sus valores actuales
                string formula = ecuacion;

                // Reemplazar "Costo total en materiales" con el valor actual
                formula = formula.Replace("CostoTotalEnMateriales",
                    fieldCostoTotalMateriales.Text.Replace(",", ""));

                // Reemplazar "Costo total en actividades" con el valor actual
                formula = formula.Replace("CostoTotalEnActividades",
                    fieldCostoTotalActividadesProduccion.Text.Replace(",", ""));

                // Reemplazar otros gastos indirectos si están en la ecuación
                foreach (var gasto in GastosIndirectos) {
                    var gastoSplit = gasto[0].Split(" ");
                    var gastoSinEspacios = string.Join("", gastoSplit.Select(
                            palabra => char.ToUpper(palabra[0]) + palabra.Substring(1).ToLower())
                        );

                    if (formula.Contains(gastoSinEspacios)) {
                        formula = formula.Replace(gastoSinEspacios,
                            gasto[2].Replace(",", ""));
                    }
                }

                // Calcular el resultado usando DataTable.Compute
                var result = new DataTable().Compute(formula, null);
                return Convert.ToDecimal(result);
            } catch {
                // En caso de error en el cálculo, devolver 0
                return 0m;
            }
        }

        private void ActualizarCostoTotalProduccion() {
            var costoTotalMateriales = decimal.TryParse(fieldCostoTotalMateriales.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var costoMat) ? costoMat : 0m;
            var costoTotalActividades = decimal.TryParse(fieldCostoTotalActividadesProduccion.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var costoAct) ? costoAct : 0m;
            var montoTotalGastosIndirectos = decimal.TryParse(fieldMontoTotalGastosIndirectos.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var montoGast) ? montoGast : 0m;

            fieldCostoTotalProduccion.Text = (costoTotalMateriales + costoTotalActividades + montoTotalGastosIndirectos).ToString("N2", CultureInfo.InvariantCulture);

            ActualizarPrecioUnitarioProducto();
        }

        private void ActualizarPrecioUnitarioProducto() {
            var costoTotalProduccion = decimal.TryParse(fieldCostoTotalProduccion.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var costoTotal) ? costoTotal : 0m;
            var cantidadProducir = decimal.TryParse(fieldCantidadProducir.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var cantidad) ? cantidad : 1m;
            var margenGanancia = decimal.TryParse(fieldMargenGananciaDeseado.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var margen) ? margen : 0m;

            fieldPrecioUnitarioProducto.Text = (cantidad > 0 ? (costoTotal / cantidad) * (1 + margen / 100) : 0m).ToString("N2", CultureInfo.InvariantCulture);
        }
    }
}
