using aDVanceERP.Core.Utiles;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.QR.Plantillas;
using aDVanceERP.Modulos.Finanzas.Properties;
using QRCoder;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.QR; 

public partial class VistaQR : Form, IVistaQR {
    public VistaQR() {
        InitializeComponent();

        NombreVista = nameof(VistaQR);

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

    public Image? QR {
        get => fieldCodigoQr.BackgroundImage;
        set => fieldCodigoQr.BackgroundImage = value;
    }

    public void Inicializar() {
        // Eventos
        btnCerrar.Click += delegate(object? sender, EventArgs args) { Close(); };
    }

    public void Mostrar() {
        BringToFront();
        ShowDialog();
    }

    public void Restaurar() { }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }

    public void CargarCodigoQR(string? datos) {
        using (var qrGenerator = new QRCodeGenerator()) {
            var datosSplit = datos.Split(',');
            var datosTransferencia = $"TRANSFERMOVIL_ETECSA,TRANSFERENCIA,{datosSplit[1]},{datosSplit[2]},";

            fieldAlias.Text = datosSplit[0];
            fieldTarjeta.Text = datosSplit[1].AgregarEspacioCadaXCaracteres(4);
            fieldMovil.Text = $@"+53 {datosSplit[2]}";

            using (var qrCodeData = qrGenerator.CreateQrCode(datosTransferencia, QRCodeGenerator.ECCLevel.Q)) {
                using (var qrCode = new QRCode(qrCodeData)) {
                    var qrCodeImage = qrCode.GetGraphic(3, Color.FromArgb(40, 37, 35), Color.White,
                        Resources.images2, 20);

                    QR = qrCodeImage;
                }
            }
        }
    }
}