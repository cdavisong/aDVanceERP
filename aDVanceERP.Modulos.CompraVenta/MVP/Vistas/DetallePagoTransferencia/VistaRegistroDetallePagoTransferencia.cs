using aDVanceERP.Core.Utiles;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.DetallePagoTransferencia.Plantillas;
using aDVanceERP.Modulos.CompraVenta.Properties;

using QRCoder;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.DetallePagoTransferencia;

public partial class VistaRegistroDetallePagoTransferencia : Form, IVistaRegistroDetallePagoTransferencia {
    private bool _modoEdicion;
    private string _numeroTarjeta = "0000 0000 0000 0000";

    public VistaRegistroDetallePagoTransferencia() {
        InitializeComponent();

        NombreVista = nameof(VistaRegistroDetallePagoTransferencia);

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

    public string Alias {
        get => fieldAlias.Text;
        set => fieldAlias.Text = value;
    }

    public string NumeroConfirmacion {
        get => fieldNumeroMovilConfirmacion.Text;
        set => fieldNumeroMovilConfirmacion.Text = value;
    }

    public string NumeroTransaccion {
        get => fieldNumeroTransaccion.Text;
        set => fieldNumeroTransaccion.Text = value;
    }

    public bool RecordarNumeroConfirmacion {
        get => fieldRecordarNumeroConfirmacion.Checked;
        set => fieldRecordarNumeroConfirmacion.Checked = value;
    }

    public Image? QR {
        get => fieldCodigoQr.BackgroundImage;
        set => fieldCodigoQr.BackgroundImage = value;
    }

    public bool ModoEdicion {
        get => _modoEdicion;
        set {
            fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
            btnRegistrar.Text = value ? "Actualizar transferencia" : "Confirmar transferencia";
            _modoEdicion = value;
        }
    }

    public event EventHandler? RegistrarEntidad;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarEntidad;


    public void Inicializar() {
        separador1.Visible = false;
        layoutQrDatos.Visible = false;

        // Eventos            
        btnCerrar.Click += delegate (object? sender, EventArgs args) { Close(); };
        fieldAlias.SelectedIndexChanged += delegate {
            var idCuenta = UtilesCuentaBancaria.ObtenerIdCuenta(Alias);
            var idPropietario = UtilesCuentaBancaria.ObtenerIdPropietario(idCuenta);
            var movilPropietario = UtilesTelefonoContacto.ObtenerTelefonoContacto(idPropietario, true);
            var numeroConfirmacion = string.IsNullOrEmpty(UtilesCuentaBancaria.NumeroConfirmacion)
                ? movilPropietario
                : UtilesCuentaBancaria.NumeroConfirmacion;

            _numeroTarjeta = UtilesCuentaBancaria.ObtenerNumeroTarjeta(idCuenta) ?? string.Empty;
            NumeroConfirmacion = numeroConfirmacion ?? string.Empty;

            fieldRecordarNumeroConfirmacion.Checked =
                NumeroConfirmacion.Equals(UtilesCuentaBancaria.NumeroConfirmacion);
        };
        fieldNumeroMovilConfirmacion.TextChanged += delegate {
            if (NumeroConfirmacion.Length == 8 && !string.IsNullOrEmpty(Alias)) {
                CargarCodigoQR($"{Alias},{_numeroTarjeta.Replace(" ", "")},{NumeroConfirmacion}");
            } else {
                separador1.Visible = false;
                layoutQrDatos.Visible = false;
                fieldRecordarNumeroConfirmacion.Checked = false;
            }
        };
        fieldNumeroTransaccion.TextChanged += delegate { btnRegistrar.Enabled = NumeroTransaccion.Length == 13; };
        fieldRecordarNumeroConfirmacion.CheckedChanged += delegate {
            UtilesCuentaBancaria.NumeroConfirmacion = !RecordarNumeroConfirmacion
                ? string.Empty
                : NumeroConfirmacion;
        };
        btnRegistrar.Click += delegate (object? sender, EventArgs args) {
            if (ModoEdicion)
                EditarEntidad?.Invoke(sender, args);
            else
                Close();
        };
        btnSalir.Click += delegate (object? sender, EventArgs args) { Close(); };
    }

    public void CargarAliasTarjetas(string[] aliasTarjetas) {
        fieldAlias.Items.AddRange(aliasTarjetas);
        fieldAlias.SelectedIndex = -1;
    }

    public void CargarCodigoQR(string datos) {
        using (var qrGenerator = new QRCodeGenerator()) {
            var datosSplit = datos.Split(',');
            var datosTransferencia = $"TRANSFERMOVIL_ETECSA,TRANSFERENCIA,{datosSplit[1]},{datosSplit[2]},";

            fieldAliasQR.Text = datosSplit[0];
            fieldTarjetaQR.Text = datosSplit[1].AgregarEspacioCadaXCaracteres(4);
            fieldNumeroMovilConfirmacionQR.Text = $@"+53 {datosSplit[2]}";

            using (var qrCodeData = qrGenerator.CreateQrCode(datosTransferencia, QRCodeGenerator.ECCLevel.Q)) {
                using (var qrCode = new QRCode(qrCodeData)) {
                    var qrCodeImage = qrCode.GetGraphic(3, Color.FromArgb(40, 37, 35), Color.White,
                        Resources.images2, 20);

                    QR = qrCodeImage;
                    separador1.Visible = true;
                    layoutQrDatos.Visible = true;
                }
            }
        }
    }

    public void Mostrar() {
        BringToFront();
        ShowDialog();
    }

    public void Restaurar() {
        Alias = string.Empty;
        fieldAlias.SelectedIndex = -1;
        NumeroConfirmacion = string.Empty;
        NumeroTransaccion = string.Empty;
        RecordarNumeroConfirmacion = false;
        separador1.Visible = false;
        layoutQrDatos.Visible = false;
        ModoEdicion = false;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }
}