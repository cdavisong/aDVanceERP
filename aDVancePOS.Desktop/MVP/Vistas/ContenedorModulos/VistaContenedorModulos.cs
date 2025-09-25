using aDVanceERP.Core.MVP.Modelos.Repositorios;
using aDVanceERP.Core.MVP.Modelos.Repositorios.Plantillas;
using aDVanceERP.Core.Utiles;

using aDVancePOS.Desktop.MVP.Vistas.ContenedorModulos.Plantillas;

using TheArtOfDevHtmlRenderer.Core.Entities;

namespace aDVancePOS.Desktop.MVP.Vistas.ContenedorModulos;

public partial class VistaContenedorModulos : Form, IVistaContenedorModulos {
    public VistaContenedorModulos() {
        InitializeComponent();
        Inicializar();
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

    public int AlturaContenedorVistas {
        get => contenedorVistas.Height;
    }

    public int TuplasMaximasContenedor {
        get => AlturaContenedorVistas / VariablesGlobales.AlturaTuplaPredeterminada;
    }

    //public bool BtnModuloAdministracionVisible {
    //    get => btnModuloAdministracion.Visible;
    //    set => btnModuloAdministracion.Visible = value;
    //}

    public IRepositorioVista? Vistas { get; private set; }

    public event EventHandler? MostrarVistaInicio;
    public event EventHandler? MostrarVistaPuntoVenta;
    public event EventHandler? CambioModulo;
    public event EventHandler? Salir;

    public void Inicializar() {
        // Propiedades locales
        Vistas = new RepositorioVistaBase(contenedorVistas);
        btnInicio.Checked = true;

        // Eventos
        btnInicio.Click += delegate (object? sender, EventArgs e) { PresionarBotonModulo(1, e); };
        btnPuntoVenta.Click += delegate (object? sender, EventArgs e) { PresionarBotonModulo(2, e); };
        CambioModulo += delegate { Restaurar(); };

        MostrarMensajePortada();
    }

    public void PresionarBotonModulo(object? sender, EventArgs e) {
        var indiceValido = int.TryParse(sender?.ToString() ?? string.Empty, out var indice);

        if (!indiceValido)
            return;

        CambioModulo?.Invoke(sender, e);

        switch (indice) {
            case 1:
                MostrarVistaInicio?.Invoke(btnInicio, e);
                if (!btnInicio.Checked)
                    btnInicio.Checked = true;
                break;
            case 2:
                MostrarVistaPuntoVenta?.Invoke(btnPuntoVenta, e);
                if (!btnPuntoVenta.Checked)
                    btnPuntoVenta.Checked = true;
                break;
        }
    }

    public void Mostrar() {
        BringToFront();
        Show();
    }

    private void MostrarMensajePortada() {
        var version = "v0.0.0.1-alpha"; // Valor por defecto

        if (File.Exists(@".\app.ver"))
            using (var fs = new FileStream(@".\app.ver", FileMode.Open)) {
                using (var sr = new StreamReader(fs)) {
                    version = sr.ReadToEnd().Trim();
                }
            }

        var textoHTML = @"
<!DOCTYPE html>
<html lang=""es"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Bienvenido a aDVance POS</title>
    <style>
        body {
            font-family: Segoe UI, sans-serif;
            text-align: center;
            padding: 50px;
        }
        .header {
            color: #FFFFFF;
            padding: 20px;
            border-radius: 8px;
        }
        .logo {
            font-family: Segoe UI, sans-serif;
            font-size: 24px;
        }
        .dv {
            color: Gray;
            font-weight: bold;
        }
        .advance {
            color: #333333;
            font-weight: bold;
        }
        .pos {
            background-color: Firebrick;
            color: white;
            font-weight: bold;
            padding: 2px;
        }
        .version {
            color: Gray;
            font-size: 10px;
        }
        .welcome-text {
            margin-top: 20px;
            font-size: 24px;
        }
        .description {
            margin-top: 10px;
            font-size: 16px;
        }
        .start-button {
            display: inline-block;
            margin-top: 30px;
            padding: 10px 30px;
            background-color: #FFDAB9;
            color: #333333;
            text-decoration: none;
            border-radius: 16px;
            font-size: 18px;
            font-weight: bold;
        }
        .start-button:hover {
            background-color: #FFDAB9;
        }
    </style>
</head>
<body>
    <div class=""header"">
        <div class=""logo"">
            <span class=""advance"">a</span><span class=""dv"">DV</span><span class=""advance"">ance</span> <span class=""pos"">POS</span> <span class=""version"">" + version + @"</span>  
        </div>
    </div>
    <div class=""welcome-text"">
        <p>¡Bienvenido al Sistema de Punto de Venta!</p>
        <p>La solución perfecta para agilizar tus ventas y mejorar la experiencia de tus clientes.</p>
    </div>
    <div class=""description"">
        <p>Con aDVance POS, gestiona tus transacciones de forma rápida, controla tu inventario en tiempo real y ofrece un servicio excepcional.</p>
        <p>Comienza a vender de manera más inteligente desde el primer momento.</p>
    </div>
    <p></p>
    <p></p>
    <a href=""/ventas"" class=""start-button"">Iniciar Ventas</a>
</body>
</html>
";

        fieldTextoBienvenida.Text = textoHTML;
        fieldTextoBienvenida.LinkClicked += delegate (object? sender, HtmlLinkClickedEventArgs e) {
            CambioModulo?.Invoke(sender, e);
            MostrarVistaPuntoVenta?.Invoke(sender, e);
            btnPuntoVenta.Checked = true;
        };
        fieldTextoBienvenida.Visible = true;
    }

    public void Restaurar() {
        Vistas.Ocultar(true);
    }

    public void Ocultar() {
        btnInicio.Checked = true;

        Hide();
    }

    public void Cerrar() {
        Vistas.Cerrar();
    }
}