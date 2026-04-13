using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaRegistroMovimiento : Form, IVistaRegistroMovimiento {
        private bool _modoEdicion = false;
        private Producto? _producto;

        public VistaRegistroMovimiento() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroMovimiento);

            Inicializar();
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                _modoEdicion = value;

                fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
                btnRegistrarActualizar.Text = value ? "Actualizar el movimiento" : "Registrar el movimiento";
                fieldNombreProducto.ReadOnly = value;
                fieldTipoMovimiento.Enabled = !value;
                fieldAlmacenOrigen.Enabled = !value;
                fieldAlmacenDestino.Enabled = !value;
                fieldCantidadMovida.ReadOnly = value;
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

        public Producto? Producto {
            get => _producto;
            set {
                _producto = value;

                if (value == null) {
                    CentroNotificaciones.MostrarNotificacion("El producto entrado no se encuentra registrado en la base de datos. Entre el nombre de un producto válido antes de realizar el movimiento.", TipoNotificacionEnum.Advertencia);

                    panelDatosProducto.Visible = false;
                    layoutVista.RowStyles[4].Height = 0;
                    layoutVista.RowStyles[5].Height = 0;
                    fieldNombreProducto.Text = string.Empty;
                    fieldAbreviaturaUM1.Text = "u";
                    fieldAbreviaturaUM2.Text = "u";
                } else {
                    panelDatosProducto.Visible = true;
                    layoutVista.RowStyles[4].Height = 70;
                    layoutVista.RowStyles[5].Height = 20;

                    ActualizarInformacionProductoSeleccionado(value);
                }
            }
        }

        public Almacen? AlmacenOrigen {
            get => fieldAlmacenOrigen.SelectedItem as Almacen;
            set => fieldAlmacenOrigen.SelectedItem = value;
        }

        public Almacen? AlmacenDestino {
            get => fieldAlmacenDestino.SelectedItem as Almacen;
            set => fieldAlmacenDestino.SelectedItem = value;
        }

        public DateTime Fecha {
            get => fieldFecha.Value;
            set => fieldFecha.Value = value;
        }

        public decimal CantidadMovida {
            get => decimal.TryParse(fieldCantidadMovida.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            set => fieldCantidadMovida.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public TipoMovimiento? TipoMovimiento {
            get => fieldTipoMovimiento.SelectedItem as TipoMovimiento;
            set => fieldTipoMovimiento.SelectedItem = value;
        }

        public string Notas {
            get => fieldNotas.Text;
            set => fieldNotas.Text = value;
        }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            fieldNombreProducto.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode != Keys.Enter)
                    return;

                Producto = RepoProducto.Instancia
                    .Buscar(FiltroBusquedaProducto.Nombre, fieldNombreProducto.Text)
                    .resultadosBusqueda
                    .Select(r => r.entidadBase)
                    .FirstOrDefault(p => p.Nombre.Equals(fieldNombreProducto.Text));

                ActualizarCantidadFinal(sender, EventArgs.Empty);

                args.SuppressKeyPress = true;
            };
            fieldTipoMovimiento.SelectedIndexChanged += delegate (object? sender, EventArgs e) {
                ActualizarCamposAlmacenes();
                ActualizarCantidadFinal(sender, e);
            };
            fieldAlmacenOrigen.SelectedIndexChanged += HabilitaDeshabilitarCampoCantidad;
            fieldAlmacenDestino.SelectedIndexChanged += HabilitaDeshabilitarCampoCantidad;
            fieldCantidadMovida.TextChanged += ActualizarCantidadFinal;
            btnRegistrarActualizar.Click += delegate (object? sender, EventArgs args) {
                if (ModoEdicion)
                    EditarEntidad?.Invoke(sender, args);
                else
                    RegistrarEntidad?.Invoke(sender, args);
            };
            btnSalir.Click += delegate (object? sender, EventArgs args) { Ocultar(); };
        }

        private void ActualizarCamposAlmacenes() {
            if (TipoMovimiento?.Efecto == EfectoMovimientoEnum.Carga) {
                fieldAlmacenOrigen.SelectedIndex = 0;
                fieldAlmacenOrigen.Enabled = false;
                fieldAlmacenDestino.Enabled = !ModoEdicion;
                fieldTextoAdvertencia.Text = "      Para movimientos de tipo Carga seleccione el almacén destino";
            } else if (TipoMovimiento?.Efecto == EfectoMovimientoEnum.Descarga) {
                fieldAlmacenDestino.SelectedIndex = 0;
                fieldAlmacenDestino.Enabled = false;
                fieldAlmacenOrigen.Enabled = !ModoEdicion;
                fieldTextoAdvertencia.Text = "      Para movimientos de tipo Descarga seleccione el almacén origen";
            } else {
                fieldAlmacenOrigen.Enabled = !ModoEdicion && TipoMovimiento != null;
                fieldAlmacenDestino.Enabled = !ModoEdicion && TipoMovimiento != null;
                fieldTextoAdvertencia.Text = "      Para movimientos de tipo Transferencia seleccione ambos almacenes: origen y destino";
            }
        }

        private void HabilitaDeshabilitarCampoCantidad(object? sender, EventArgs e) {
            fieldCantidadMovida.ReadOnly =
                ModoEdicion ||
                (fieldAlmacenOrigen.SelectedIndex == 0 && fieldAlmacenDestino.SelectedIndex == 0) ||
                (fieldAlmacenOrigen.SelectedIndex != 0 && fieldAlmacenDestino.SelectedIndex != 0 && fieldAlmacenOrigen.SelectedItem?.ToString() == fieldAlmacenDestino.SelectedItem?.ToString());
        }

        public void ActualizarInformacionProductoSeleccionado(Producto producto) {
            var unidadMedida = RepoUnidadMedida.Instancia
                .ObtenerPorId(producto!.IdUnidadMedida);
            var inventarioProducto = RepoInventario.Instancia
                .Buscar(FiltroBusquedaInventario.IdProducto, producto!.Id.ToString())
                .resultadosBusqueda;

            // Banner de producto
            fieldCodigoBanner.Text = producto.Codigo;
            fieldNombreProductoBanner.Text = producto.Nombre;
            fieldFechaUltimoMovimientoBanner.Text = inventarioProducto.Count > 0
                ? inventarioProducto.Min(inv => inv.entidadBase.UltimaActualizacion).ToString("dd/MM/yyyy HH:mm")
                : "-";
            fieldStockTotalBanner.Text = $"{inventarioProducto.Sum(inv => inv.entidadBase.Cantidad).ToString("N2", CultureInfo.InvariantCulture)} {unidadMedida.Abreviatura}";
            fieldOperadorBanner.Text = ContextoSeguridad.UsuarioAutenticado!.Nombre;

            fieldAbreviaturaUM1.Text = unidadMedida?.Abreviatura ?? "u";
            fieldAbreviaturaUM2.Text = unidadMedida?.Abreviatura ?? "u";
        }

        private void ActualizarCantidadFinal(object? sender, EventArgs e) {
            var inventarioProducto = RepoInventario.Instancia
                    .Buscar(FiltroBusquedaInventario.IdProducto, Producto?.Id.ToString())
                    .resultadosBusqueda
                    .Select(r => r.entidadBase);

            if (TipoMovimiento == null) {
                fieldCantidadFinal.Text = string.Empty;
                return;
            }

            var almacen = TipoMovimiento.Efecto == EfectoMovimientoEnum.Carga
                    ? AlmacenDestino
                    : TipoMovimiento.Efecto == EfectoMovimientoEnum.Descarga
                        ? AlmacenOrigen
                        : null;

            if (almacen == null) {
                fieldCantidadFinal.Text = TipoMovimiento.Efecto == EfectoMovimientoEnum.Transferencia
                    ? inventarioProducto
                        .Sum(i => i.Cantidad)
                        .ToString("N2", CultureInfo.InvariantCulture)
                    : string.Empty;
                return;
            }

            var cantidadActual = inventarioProducto
                .FirstOrDefault(i => i.IdAlmacen.Equals(almacen?.Id))?.
                Cantidad;
            var cantidadMovida = CantidadMovida;
            var cantidadFinal = TipoMovimiento.Efecto == EfectoMovimientoEnum.Carga
                ? cantidadActual + cantidadMovida
                : TipoMovimiento.Efecto == EfectoMovimientoEnum.Descarga
                    ? cantidadActual - cantidadMovida
                    : TipoMovimiento.Efecto == EfectoMovimientoEnum.Transferencia
                        ? cantidadActual
                        : 0m;

            fieldCantidadFinal.Text = cantidadFinal?.ToString("N2", CultureInfo.InvariantCulture);
        }

        public void Mostrar() {
            Fecha = DateTime.Now;

            BringToFront();
            Show();
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {
            Producto = null;
            fieldNombreProducto.Text = string.Empty;
            panelDatosProducto.Visible = false;
            layoutVista.RowStyles[4].Height = 0;
            layoutVista.RowStyles[5].Height = 0;
            AlmacenOrigen = null;
            fieldAlmacenOrigen.SelectedIndex = fieldAlmacenOrigen.Items.Count > 0 ? 0 : -1;
            AlmacenDestino = null;
            fieldAlmacenDestino.SelectedIndex = fieldAlmacenDestino.Items.Count > 0 ? 0 : -1;
            fieldCantidadMovida.Text = string.Empty;
            fieldCantidadFinal.Text = string.Empty;
            TipoMovimiento = null;
            fieldTipoMovimiento.SelectedIndex = -1;
            Fecha = DateTime.Now;
            Notas = string.Empty;
        }

        public void Cerrar() {
            Dispose();
        }

        public void CargarProductos(Producto[] productos) {
            fieldNombreProducto.AutoCompleteCustomSource.Clear();
            fieldNombreProducto.AutoCompleteCustomSource.AddRange([.. productos.Select(p => p.Nombre)]);
            fieldNombreProducto.AutoCompleteMode = AutoCompleteMode.Suggest;
            fieldNombreProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public void CargarAlmacenes(Almacen[] almacenes) {
            fieldAlmacenOrigen.Items.Clear();
            fieldAlmacenOrigen.Items.Add(new Almacen(0, "Ninguno", string.Empty, string.Empty, TipoAlmacen.Especial, false));
            fieldAlmacenOrigen.Items.AddRange(almacenes);
            fieldAlmacenOrigen.SelectedIndex = almacenes.Length > 0 ? 0 : -1;

            fieldAlmacenDestino.Items.Clear();
            fieldAlmacenDestino.Items.Add(new Almacen(0, "Ninguno", string.Empty, string.Empty, TipoAlmacen.Especial, false));
            fieldAlmacenDestino.Items.AddRange(almacenes);
            fieldAlmacenDestino.SelectedIndex = almacenes.Length > 0 ? 0 : -1;
        }

        public void CargarTiposMovimientos(TipoMovimiento[] tiposMovimientos) {
            fieldTipoMovimiento.Items.Clear();
            fieldTipoMovimiento.Items.AddRange(tiposMovimientos);
            fieldTipoMovimiento.SelectedIndex = tiposMovimientos.Length > 0 ? 0 : -1;
        }
    }
}
