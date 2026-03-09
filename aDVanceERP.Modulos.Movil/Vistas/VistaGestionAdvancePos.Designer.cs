using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Movil.Vistas {
    partial class VistaGestionAdvancePos {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaGestionAdvancePos));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutVista = new TableLayoutPanel();
            layoutTitulo = new TableLayoutPanel();
            btnVerificarConexion = new Guna2Button();
            fieldTitulo = new Label();
            fieldIcono = new PictureBox();
            fieldSubtitulo = new Label();
            layoutTitulos1 = new TableLayoutPanel();
            fieldTituloEstadoCatalogoDispositivo = new Label();
            fieldTituloEstadoInstalciónApp = new Label();
            fieldTituloDispositivoAdb = new Label();
            layoutDatos1 = new TableLayoutPanel();
            fieldEstadoCatalogoDispositivo = new Label();
            fieldEstadoInstalcionApp = new Label();
            fieldEstadoDispositivoAdb = new Label();
            layoutSeparadores1 = new TableLayoutPanel();
            separador1 = new Guna2Separator();
            layoutTitulos2 = new TableLayoutPanel();
            fieldTituloImportacionVentas = new Label();
            fieldTituloCatalogoProductos = new Label();
            layoutDescripcion1 = new TableLayoutPanel();
            fieldDescripcion2 = new Label();
            fieldDescripcion1 = new Label();
            layoutDatos2 = new TableLayoutPanel();
            layoudSubDatos22 = new TableLayoutPanel();
            fieldArchivosDisponiblesDispositivo = new Label();
            fieldTituloArchivosDisponiblesDispositivo = new Label();
            layoudSubDatos21 = new TableLayoutPanel();
            fieldUltimaActualizacionCatalogo = new Label();
            fieldTituloUltimaActualizacionCatalogo = new Label();
            layoutBotones1 = new TableLayoutPanel();
            layoutSubBotones12 = new TableLayoutPanel();
            btnImportarTodasLasVentas = new Guna2Button();
            btnImportarVentas = new Guna2Button();
            layoutSubBotones11 = new TableLayoutPanel();
            btnEliminarCatalogo = new Guna2Button();
            btnEnviarCatalogo = new Guna2Button();
            layoutSeparadores2 = new TableLayoutPanel();
            fieldTituloArchivos = new Label();
            separador2 = new Guna2Separator();
            layoutTablaArchivosVentas = new TableLayoutPanel();
            layoutEncabezadosTabla = new TableLayoutPanel();
            fieldTituloAccion = new Label();
            fieldTituloNombreArchivo = new Label();
            fieldTituloFecha = new Label();
            fieldTituloTamannoAproximado = new Label();
            panelArchivosVenta = new Panel();
            layoutVista.SuspendLayout();
            layoutTitulo.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            layoutTitulos1.SuspendLayout();
            layoutDatos1.SuspendLayout();
            layoutSeparadores1.SuspendLayout();
            layoutTitulos2.SuspendLayout();
            layoutDescripcion1.SuspendLayout();
            layoutDatos2.SuspendLayout();
            layoudSubDatos22.SuspendLayout();
            layoudSubDatos21.SuspendLayout();
            layoutBotones1.SuspendLayout();
            layoutSubBotones12.SuspendLayout();
            layoutSubBotones11.SuspendLayout();
            layoutSeparadores2.SuspendLayout();
            layoutTablaArchivosVentas.SuspendLayout();
            layoutEncabezadosTabla.SuspendLayout();
            SuspendLayout();
            // 
            // formatoBase
            // 
            formatoBase.ContainerControl = this;
            formatoBase.DockIndicatorTransparencyValue = 0.6D;
            formatoBase.DragForm = false;
            formatoBase.HasFormShadow = false;
            formatoBase.TransparentWhileDrag = true;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.White;
            layoutVista.ColumnCount = 4;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.Controls.Add(layoutTitulo, 2, 0);
            layoutVista.Controls.Add(fieldIcono, 1, 0);
            layoutVista.Controls.Add(fieldSubtitulo, 2, 1);
            layoutVista.Controls.Add(layoutTitulos1, 2, 3);
            layoutVista.Controls.Add(layoutDatos1, 2, 4);
            layoutVista.Controls.Add(layoutSeparadores1, 2, 5);
            layoutVista.Controls.Add(layoutTitulos2, 2, 7);
            layoutVista.Controls.Add(layoutDescripcion1, 2, 8);
            layoutVista.Controls.Add(layoutDatos2, 2, 9);
            layoutVista.Controls.Add(layoutBotones1, 2, 11);
            layoutVista.Controls.Add(layoutSeparadores2, 2, 12);
            layoutVista.Controls.Add(layoutTablaArchivosVentas, 2, 13);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 15;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.Size = new Size(1356, 608);
            layoutVista.TabIndex = 4;
            // 
            // layoutTitulo
            // 
            layoutTitulo.ColumnCount = 2;
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            layoutTitulo.Controls.Add(btnVerificarConexion, 1, 0);
            layoutTitulo.Controls.Add(fieldTitulo, 0, 0);
            layoutTitulo.Dock = DockStyle.Fill;
            layoutTitulo.Location = new Point(50, 0);
            layoutTitulo.Margin = new Padding(0);
            layoutTitulo.Name = "layoutTitulo";
            layoutTitulo.RowCount = 1;
            layoutTitulo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTitulo.Size = new Size(1286, 45);
            layoutTitulo.TabIndex = 14;
            // 
            // btnVerificarConexion
            // 
            btnVerificarConexion.Animated = true;
            btnVerificarConexion.BorderColor = Color.Gainsboro;
            btnVerificarConexion.BorderRadius = 18;
            btnVerificarConexion.BorderThickness = 1;
            btnVerificarConexion.CustomizableEdges = customizableEdges1;
            btnVerificarConexion.Dock = DockStyle.Fill;
            btnVerificarConexion.FillColor = Color.White;
            btnVerificarConexion.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnVerificarConexion.ForeColor = Color.Gainsboro;
            btnVerificarConexion.HoverState.BorderColor = Color.PeachPuff;
            btnVerificarConexion.HoverState.FillColor = Color.PeachPuff;
            btnVerificarConexion.HoverState.ForeColor = Color.Black;
            btnVerificarConexion.Location = new Point(1089, 3);
            btnVerificarConexion.Name = "btnVerificarConexion";
            btnVerificarConexion.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnVerificarConexion.Size = new Size(194, 39);
            btnVerificarConexion.TabIndex = 16;
            btnVerificarConexion.Text = "Verificar conexión";
            // 
            // fieldTitulo
            // 
            fieldTitulo.Dock = DockStyle.Fill;
            fieldTitulo.Font = new Font("Segoe UI", 20.25F);
            fieldTitulo.ForeColor = Color.Black;
            fieldTitulo.ImeMode = ImeMode.NoControl;
            fieldTitulo.Location = new Point(3, 0);
            fieldTitulo.Name = "fieldTitulo";
            fieldTitulo.Size = new Size(1080, 45);
            fieldTitulo.TabIndex = 3;
            fieldTitulo.Text = "aDVance POS Mobile";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldIcono
            // 
            fieldIcono.BackgroundImage = (Image) resources.GetObject("fieldIcono.BackgroundImage");
            fieldIcono.BackgroundImageLayout = ImageLayout.Center;
            fieldIcono.Dock = DockStyle.Fill;
            fieldIcono.Location = new Point(20, 6);
            fieldIcono.Margin = new Padding(0, 6, 0, 0);
            fieldIcono.Name = "fieldIcono";
            fieldIcono.Size = new Size(30, 39);
            fieldIcono.TabIndex = 0;
            fieldIcono.TabStop = false;
            // 
            // fieldSubtitulo
            // 
            fieldSubtitulo.Dock = DockStyle.Fill;
            fieldSubtitulo.Font = new Font("Segoe UI", 11.25F);
            fieldSubtitulo.ForeColor = Color.Gray;
            fieldSubtitulo.ImeMode = ImeMode.NoControl;
            fieldSubtitulo.Location = new Point(55, 50);
            fieldSubtitulo.Margin = new Padding(5, 5, 1, 1);
            fieldSubtitulo.Name = "fieldSubtitulo";
            fieldSubtitulo.Size = new Size(1280, 39);
            fieldSubtitulo.TabIndex = 2;
            fieldSubtitulo.Text = "Sincronización de catálogos e importación de ventas desde el dispositivo Android";
            // 
            // layoutTitulos1
            // 
            layoutTitulos1.ColumnCount = 3;
            layoutTitulos1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            layoutTitulos1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            layoutTitulos1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            layoutTitulos1.Controls.Add(fieldTituloEstadoCatalogoDispositivo, 2, 0);
            layoutTitulos1.Controls.Add(fieldTituloEstadoInstalciónApp, 1, 0);
            layoutTitulos1.Controls.Add(fieldTituloDispositivoAdb, 0, 0);
            layoutTitulos1.Dock = DockStyle.Fill;
            layoutTitulos1.Location = new Point(50, 110);
            layoutTitulos1.Margin = new Padding(0);
            layoutTitulos1.Name = "layoutTitulos1";
            layoutTitulos1.RowCount = 1;
            layoutTitulos1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTitulos1.Size = new Size(1286, 35);
            layoutTitulos1.TabIndex = 15;
            // 
            // fieldTituloEstadoCatalogoDispositivo
            // 
            fieldTituloEstadoCatalogoDispositivo.Dock = DockStyle.Fill;
            fieldTituloEstadoCatalogoDispositivo.Font = new Font("Segoe UI", 11.25F);
            fieldTituloEstadoCatalogoDispositivo.ForeColor = Color.DimGray;
            fieldTituloEstadoCatalogoDispositivo.Image = (Image) resources.GetObject("fieldTituloEstadoCatalogoDispositivo.Image");
            fieldTituloEstadoCatalogoDispositivo.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloEstadoCatalogoDispositivo.ImeMode = ImeMode.NoControl;
            fieldTituloEstadoCatalogoDispositivo.Location = new Point(871, 5);
            fieldTituloEstadoCatalogoDispositivo.Margin = new Padding(15, 5, 3, 3);
            fieldTituloEstadoCatalogoDispositivo.Name = "fieldTituloEstadoCatalogoDispositivo";
            fieldTituloEstadoCatalogoDispositivo.Size = new Size(412, 27);
            fieldTituloEstadoCatalogoDispositivo.TabIndex = 27;
            fieldTituloEstadoCatalogoDispositivo.Text = "      Catálogo en dispositivo :";
            fieldTituloEstadoCatalogoDispositivo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloEstadoInstalciónApp
            // 
            fieldTituloEstadoInstalciónApp.Dock = DockStyle.Fill;
            fieldTituloEstadoInstalciónApp.Font = new Font("Segoe UI", 11.25F);
            fieldTituloEstadoInstalciónApp.ForeColor = Color.DimGray;
            fieldTituloEstadoInstalciónApp.Image = (Image) resources.GetObject("fieldTituloEstadoInstalciónApp.Image");
            fieldTituloEstadoInstalciónApp.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloEstadoInstalciónApp.ImeMode = ImeMode.NoControl;
            fieldTituloEstadoInstalciónApp.Location = new Point(443, 5);
            fieldTituloEstadoInstalciónApp.Margin = new Padding(15, 5, 3, 3);
            fieldTituloEstadoInstalciónApp.Name = "fieldTituloEstadoInstalciónApp";
            fieldTituloEstadoInstalciónApp.Size = new Size(410, 27);
            fieldTituloEstadoInstalciónApp.TabIndex = 26;
            fieldTituloEstadoInstalciónApp.Text = "      aDVance POS Mobile Instalada :";
            fieldTituloEstadoInstalciónApp.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloDispositivoAdb
            // 
            fieldTituloDispositivoAdb.Dock = DockStyle.Fill;
            fieldTituloDispositivoAdb.Font = new Font("Segoe UI", 11.25F);
            fieldTituloDispositivoAdb.ForeColor = Color.DimGray;
            fieldTituloDispositivoAdb.Image = (Image) resources.GetObject("fieldTituloDispositivoAdb.Image");
            fieldTituloDispositivoAdb.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloDispositivoAdb.ImeMode = ImeMode.NoControl;
            fieldTituloDispositivoAdb.Location = new Point(15, 5);
            fieldTituloDispositivoAdb.Margin = new Padding(15, 5, 3, 3);
            fieldTituloDispositivoAdb.Name = "fieldTituloDispositivoAdb";
            fieldTituloDispositivoAdb.Size = new Size(410, 27);
            fieldTituloDispositivoAdb.TabIndex = 25;
            fieldTituloDispositivoAdb.Text = "      Dispositivo ADB :";
            fieldTituloDispositivoAdb.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutDatos1
            // 
            layoutDatos1.ColumnCount = 3;
            layoutDatos1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            layoutDatos1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            layoutDatos1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            layoutDatos1.Controls.Add(fieldEstadoCatalogoDispositivo, 2, 0);
            layoutDatos1.Controls.Add(fieldEstadoInstalcionApp, 1, 0);
            layoutDatos1.Controls.Add(fieldEstadoDispositivoAdb, 0, 0);
            layoutDatos1.Dock = DockStyle.Fill;
            layoutDatos1.Location = new Point(50, 145);
            layoutDatos1.Margin = new Padding(0);
            layoutDatos1.Name = "layoutDatos1";
            layoutDatos1.RowCount = 1;
            layoutDatos1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDatos1.Size = new Size(1286, 45);
            layoutDatos1.TabIndex = 16;
            // 
            // fieldEstadoCatalogoDispositivo
            // 
            fieldEstadoCatalogoDispositivo.Dock = DockStyle.Fill;
            fieldEstadoCatalogoDispositivo.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldEstadoCatalogoDispositivo.ForeColor = Color.Peru;
            fieldEstadoCatalogoDispositivo.Image = Properties.Resources.clockY_16px;
            fieldEstadoCatalogoDispositivo.ImageAlign = ContentAlignment.MiddleLeft;
            fieldEstadoCatalogoDispositivo.ImeMode = ImeMode.NoControl;
            fieldEstadoCatalogoDispositivo.Location = new Point(876, 1);
            fieldEstadoCatalogoDispositivo.Margin = new Padding(20, 1, 1, 1);
            fieldEstadoCatalogoDispositivo.Name = "fieldEstadoCatalogoDispositivo";
            fieldEstadoCatalogoDispositivo.Size = new Size(409, 43);
            fieldEstadoCatalogoDispositivo.TabIndex = 18;
            fieldEstadoCatalogoDispositivo.Text = "     Catálogo desactualizado";
            fieldEstadoCatalogoDispositivo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldEstadoInstalcionApp
            // 
            fieldEstadoInstalcionApp.Dock = DockStyle.Fill;
            fieldEstadoInstalcionApp.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldEstadoInstalcionApp.ForeColor = Color.Firebrick;
            fieldEstadoInstalcionApp.Image = Properties.Resources.noR_16px;
            fieldEstadoInstalcionApp.ImageAlign = ContentAlignment.MiddleLeft;
            fieldEstadoInstalcionApp.ImeMode = ImeMode.NoControl;
            fieldEstadoInstalcionApp.Location = new Point(448, 1);
            fieldEstadoInstalcionApp.Margin = new Padding(20, 1, 1, 1);
            fieldEstadoInstalcionApp.Name = "fieldEstadoInstalcionApp";
            fieldEstadoInstalcionApp.Size = new Size(407, 43);
            fieldEstadoInstalcionApp.TabIndex = 17;
            fieldEstadoInstalcionApp.Text = "     App no detectada en el dispositivo";
            fieldEstadoInstalcionApp.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldEstadoDispositivoAdb
            // 
            fieldEstadoDispositivoAdb.Dock = DockStyle.Fill;
            fieldEstadoDispositivoAdb.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldEstadoDispositivoAdb.ForeColor = Color.Firebrick;
            fieldEstadoDispositivoAdb.Image = Properties.Resources.noR_16px;
            fieldEstadoDispositivoAdb.ImageAlign = ContentAlignment.MiddleLeft;
            fieldEstadoDispositivoAdb.ImeMode = ImeMode.NoControl;
            fieldEstadoDispositivoAdb.Location = new Point(20, 1);
            fieldEstadoDispositivoAdb.Margin = new Padding(20, 1, 1, 1);
            fieldEstadoDispositivoAdb.Name = "fieldEstadoDispositivoAdb";
            fieldEstadoDispositivoAdb.Size = new Size(407, 43);
            fieldEstadoDispositivoAdb.TabIndex = 16;
            fieldEstadoDispositivoAdb.Text = "     Desconectado";
            fieldEstadoDispositivoAdb.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutSeparadores1
            // 
            layoutSeparadores1.ColumnCount = 1;
            layoutSeparadores1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            layoutSeparadores1.Controls.Add(separador1, 0, 0);
            layoutSeparadores1.Dock = DockStyle.Fill;
            layoutSeparadores1.Location = new Point(50, 190);
            layoutSeparadores1.Margin = new Padding(0);
            layoutSeparadores1.Name = "layoutSeparadores1";
            layoutSeparadores1.RowCount = 1;
            layoutSeparadores1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutSeparadores1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutSeparadores1.Size = new Size(1286, 20);
            layoutSeparadores1.TabIndex = 17;
            // 
            // separador1
            // 
            separador1.Dock = DockStyle.Fill;
            separador1.FillColor = Color.FromArgb(  208,   197,   188);
            separador1.Location = new Point(3, 3);
            separador1.Name = "separador1";
            separador1.Size = new Size(1280, 14);
            separador1.TabIndex = 44;
            // 
            // layoutTitulos2
            // 
            layoutTitulos2.ColumnCount = 2;
            layoutTitulos2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutTitulos2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutTitulos2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutTitulos2.Controls.Add(fieldTituloImportacionVentas, 1, 0);
            layoutTitulos2.Controls.Add(fieldTituloCatalogoProductos, 0, 0);
            layoutTitulos2.Dock = DockStyle.Fill;
            layoutTitulos2.Location = new Point(50, 220);
            layoutTitulos2.Margin = new Padding(0);
            layoutTitulos2.Name = "layoutTitulos2";
            layoutTitulos2.RowCount = 1;
            layoutTitulos2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTitulos2.Size = new Size(1286, 35);
            layoutTitulos2.TabIndex = 18;
            // 
            // fieldTituloImportacionVentas
            // 
            fieldTituloImportacionVentas.Dock = DockStyle.Fill;
            fieldTituloImportacionVentas.Font = new Font("Segoe UI", 11.25F);
            fieldTituloImportacionVentas.ForeColor = Color.DimGray;
            fieldTituloImportacionVentas.Image = (Image) resources.GetObject("fieldTituloImportacionVentas.Image");
            fieldTituloImportacionVentas.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloImportacionVentas.ImeMode = ImeMode.NoControl;
            fieldTituloImportacionVentas.Location = new Point(658, 5);
            fieldTituloImportacionVentas.Margin = new Padding(15, 5, 3, 3);
            fieldTituloImportacionVentas.Name = "fieldTituloImportacionVentas";
            fieldTituloImportacionVentas.Size = new Size(625, 27);
            fieldTituloImportacionVentas.TabIndex = 26;
            fieldTituloImportacionVentas.Text = "      Importar ventas :";
            fieldTituloImportacionVentas.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloCatalogoProductos
            // 
            fieldTituloCatalogoProductos.Dock = DockStyle.Fill;
            fieldTituloCatalogoProductos.Font = new Font("Segoe UI", 11.25F);
            fieldTituloCatalogoProductos.ForeColor = Color.DimGray;
            fieldTituloCatalogoProductos.Image = (Image) resources.GetObject("fieldTituloCatalogoProductos.Image");
            fieldTituloCatalogoProductos.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloCatalogoProductos.ImeMode = ImeMode.NoControl;
            fieldTituloCatalogoProductos.Location = new Point(15, 5);
            fieldTituloCatalogoProductos.Margin = new Padding(15, 5, 3, 3);
            fieldTituloCatalogoProductos.Name = "fieldTituloCatalogoProductos";
            fieldTituloCatalogoProductos.Size = new Size(625, 27);
            fieldTituloCatalogoProductos.TabIndex = 25;
            fieldTituloCatalogoProductos.Text = "      Catálogo de productos :";
            fieldTituloCatalogoProductos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutDescripcion1
            // 
            layoutDescripcion1.ColumnCount = 2;
            layoutDescripcion1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutDescripcion1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutDescripcion1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDescripcion1.Controls.Add(fieldDescripcion2, 1, 0);
            layoutDescripcion1.Controls.Add(fieldDescripcion1, 0, 0);
            layoutDescripcion1.Dock = DockStyle.Fill;
            layoutDescripcion1.Location = new Point(50, 255);
            layoutDescripcion1.Margin = new Padding(0);
            layoutDescripcion1.Name = "layoutDescripcion1";
            layoutDescripcion1.RowCount = 1;
            layoutDescripcion1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDescripcion1.Size = new Size(1286, 60);
            layoutDescripcion1.TabIndex = 19;
            // 
            // fieldDescripcion2
            // 
            fieldDescripcion2.Dock = DockStyle.Fill;
            fieldDescripcion2.Font = new Font("Segoe UI", 11.25F);
            fieldDescripcion2.ForeColor = Color.FromArgb(  64,   64,   64);
            fieldDescripcion2.ImageAlign = ContentAlignment.MiddleLeft;
            fieldDescripcion2.ImeMode = ImeMode.NoControl;
            fieldDescripcion2.Location = new Point(658, 5);
            fieldDescripcion2.Margin = new Padding(15, 5, 3, 3);
            fieldDescripcion2.Name = "fieldDescripcion2";
            fieldDescripcion2.Size = new Size(625, 52);
            fieldDescripcion2.TabIndex = 26;
            fieldDescripcion2.Text = "Descarga e importa los archivos de ventas registrados en el dispositivo. Los movimientos de inventario y pagos se crean automáticamente.";
            // 
            // fieldDescripcion1
            // 
            fieldDescripcion1.Dock = DockStyle.Fill;
            fieldDescripcion1.Font = new Font("Segoe UI", 11.25F);
            fieldDescripcion1.ForeColor = Color.FromArgb(  64,   64,   64);
            fieldDescripcion1.ImageAlign = ContentAlignment.MiddleLeft;
            fieldDescripcion1.ImeMode = ImeMode.NoControl;
            fieldDescripcion1.Location = new Point(15, 5);
            fieldDescripcion1.Margin = new Padding(15, 5, 3, 3);
            fieldDescripcion1.Name = "fieldDescripcion1";
            fieldDescripcion1.Size = new Size(625, 52);
            fieldDescripcion1.TabIndex = 25;
            fieldDescripcion1.Text = "Genera y envía el catálogo de productos al dispositivo. aDVance.POS usará este archivo para registrar ventas en campo.";
            // 
            // layoutDatos2
            // 
            layoutDatos2.ColumnCount = 2;
            layoutDatos2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutDatos2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutDatos2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDatos2.Controls.Add(layoudSubDatos22, 1, 0);
            layoutDatos2.Controls.Add(layoudSubDatos21, 0, 0);
            layoutDatos2.Dock = DockStyle.Fill;
            layoutDatos2.Location = new Point(50, 315);
            layoutDatos2.Margin = new Padding(0);
            layoutDatos2.Name = "layoutDatos2";
            layoutDatos2.RowCount = 1;
            layoutDatos2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDatos2.Size = new Size(1286, 35);
            layoutDatos2.TabIndex = 20;
            // 
            // layoudSubDatos22
            // 
            layoudSubDatos22.ColumnCount = 2;
            layoudSubDatos22.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 268F));
            layoudSubDatos22.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoudSubDatos22.Controls.Add(fieldArchivosDisponiblesDispositivo, 1, 0);
            layoudSubDatos22.Controls.Add(fieldTituloArchivosDisponiblesDispositivo, 0, 0);
            layoudSubDatos22.Dock = DockStyle.Fill;
            layoudSubDatos22.Location = new Point(643, 0);
            layoudSubDatos22.Margin = new Padding(0);
            layoudSubDatos22.Name = "layoudSubDatos22";
            layoudSubDatos22.RowCount = 1;
            layoudSubDatos22.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoudSubDatos22.Size = new Size(643, 35);
            layoudSubDatos22.TabIndex = 20;
            // 
            // fieldArchivosDisponiblesDispositivo
            // 
            fieldArchivosDisponiblesDispositivo.Dock = DockStyle.Fill;
            fieldArchivosDisponiblesDispositivo.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldArchivosDisponiblesDispositivo.ForeColor = Color.Black;
            fieldArchivosDisponiblesDispositivo.ImeMode = ImeMode.NoControl;
            fieldArchivosDisponiblesDispositivo.Location = new Point(269, 1);
            fieldArchivosDisponiblesDispositivo.Margin = new Padding(1);
            fieldArchivosDisponiblesDispositivo.Name = "fieldArchivosDisponiblesDispositivo";
            fieldArchivosDisponiblesDispositivo.Size = new Size(373, 33);
            fieldArchivosDisponiblesDispositivo.TabIndex = 26;
            fieldArchivosDisponiblesDispositivo.Text = "0 archivos";
            fieldArchivosDisponiblesDispositivo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloArchivosDisponiblesDispositivo
            // 
            fieldTituloArchivosDisponiblesDispositivo.Dock = DockStyle.Fill;
            fieldTituloArchivosDisponiblesDispositivo.Font = new Font("Segoe UI", 11.25F);
            fieldTituloArchivosDisponiblesDispositivo.ForeColor = Color.DimGray;
            fieldTituloArchivosDisponiblesDispositivo.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloArchivosDisponiblesDispositivo.ImeMode = ImeMode.NoControl;
            fieldTituloArchivosDisponiblesDispositivo.Location = new Point(15, 5);
            fieldTituloArchivosDisponiblesDispositivo.Margin = new Padding(15, 5, 3, 3);
            fieldTituloArchivosDisponiblesDispositivo.Name = "fieldTituloArchivosDisponiblesDispositivo";
            fieldTituloArchivosDisponiblesDispositivo.Size = new Size(250, 27);
            fieldTituloArchivosDisponiblesDispositivo.TabIndex = 25;
            fieldTituloArchivosDisponiblesDispositivo.Text = "Archivos disponibles en dispositivo :";
            fieldTituloArchivosDisponiblesDispositivo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoudSubDatos21
            // 
            layoudSubDatos21.ColumnCount = 2;
            layoudSubDatos21.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            layoudSubDatos21.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoudSubDatos21.Controls.Add(fieldUltimaActualizacionCatalogo, 1, 0);
            layoudSubDatos21.Controls.Add(fieldTituloUltimaActualizacionCatalogo, 0, 0);
            layoudSubDatos21.Dock = DockStyle.Fill;
            layoudSubDatos21.Location = new Point(0, 0);
            layoudSubDatos21.Margin = new Padding(0);
            layoudSubDatos21.Name = "layoudSubDatos21";
            layoudSubDatos21.RowCount = 1;
            layoudSubDatos21.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoudSubDatos21.Size = new Size(643, 35);
            layoudSubDatos21.TabIndex = 19;
            // 
            // fieldUltimaActualizacionCatalogo
            // 
            fieldUltimaActualizacionCatalogo.Dock = DockStyle.Fill;
            fieldUltimaActualizacionCatalogo.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldUltimaActualizacionCatalogo.ForeColor = Color.Black;
            fieldUltimaActualizacionCatalogo.ImeMode = ImeMode.NoControl;
            fieldUltimaActualizacionCatalogo.Location = new Point(171, 1);
            fieldUltimaActualizacionCatalogo.Margin = new Padding(1);
            fieldUltimaActualizacionCatalogo.Name = "fieldUltimaActualizacionCatalogo";
            fieldUltimaActualizacionCatalogo.Size = new Size(471, 33);
            fieldUltimaActualizacionCatalogo.TabIndex = 26;
            fieldUltimaActualizacionCatalogo.Text = "Hace mucho tiempo";
            fieldUltimaActualizacionCatalogo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloUltimaActualizacionCatalogo
            // 
            fieldTituloUltimaActualizacionCatalogo.Dock = DockStyle.Fill;
            fieldTituloUltimaActualizacionCatalogo.Font = new Font("Segoe UI", 11.25F);
            fieldTituloUltimaActualizacionCatalogo.ForeColor = Color.DimGray;
            fieldTituloUltimaActualizacionCatalogo.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloUltimaActualizacionCatalogo.ImeMode = ImeMode.NoControl;
            fieldTituloUltimaActualizacionCatalogo.Location = new Point(15, 5);
            fieldTituloUltimaActualizacionCatalogo.Margin = new Padding(15, 5, 3, 3);
            fieldTituloUltimaActualizacionCatalogo.Name = "fieldTituloUltimaActualizacionCatalogo";
            fieldTituloUltimaActualizacionCatalogo.Size = new Size(152, 27);
            fieldTituloUltimaActualizacionCatalogo.TabIndex = 25;
            fieldTituloUltimaActualizacionCatalogo.Text = "Última actualización :";
            fieldTituloUltimaActualizacionCatalogo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutBotones1
            // 
            layoutBotones1.ColumnCount = 2;
            layoutBotones1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutBotones1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutBotones1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutBotones1.Controls.Add(layoutSubBotones12, 1, 0);
            layoutBotones1.Controls.Add(layoutSubBotones11, 0, 0);
            layoutBotones1.Dock = DockStyle.Fill;
            layoutBotones1.Location = new Point(50, 360);
            layoutBotones1.Margin = new Padding(0);
            layoutBotones1.Name = "layoutBotones1";
            layoutBotones1.RowCount = 1;
            layoutBotones1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBotones1.Size = new Size(1286, 45);
            layoutBotones1.TabIndex = 21;
            // 
            // layoutSubBotones12
            // 
            layoutSubBotones12.BackColor = Color.White;
            layoutSubBotones12.ColumnCount = 3;
            layoutSubBotones12.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
            layoutSubBotones12.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            layoutSubBotones12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutSubBotones12.Controls.Add(btnImportarTodasLasVentas, 1, 0);
            layoutSubBotones12.Controls.Add(btnImportarVentas, 0, 0);
            layoutSubBotones12.Dock = DockStyle.Fill;
            layoutSubBotones12.Location = new Point(646, 0);
            layoutSubBotones12.Margin = new Padding(3, 0, 0, 0);
            layoutSubBotones12.Name = "layoutSubBotones12";
            layoutSubBotones12.RowCount = 1;
            layoutSubBotones12.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutSubBotones12.Size = new Size(640, 45);
            layoutSubBotones12.TabIndex = 47;
            // 
            // btnImportarTodasLasVentas
            // 
            btnImportarTodasLasVentas.Animated = true;
            btnImportarTodasLasVentas.BorderColor = Color.Gainsboro;
            btnImportarTodasLasVentas.BorderRadius = 18;
            btnImportarTodasLasVentas.BorderThickness = 1;
            btnImportarTodasLasVentas.CustomizableEdges = customizableEdges3;
            btnImportarTodasLasVentas.Dock = DockStyle.Fill;
            btnImportarTodasLasVentas.FillColor = Color.White;
            btnImportarTodasLasVentas.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnImportarTodasLasVentas.ForeColor = Color.Gainsboro;
            btnImportarTodasLasVentas.HoverState.BorderColor = Color.PeachPuff;
            btnImportarTodasLasVentas.HoverState.FillColor = Color.PeachPuff;
            btnImportarTodasLasVentas.HoverState.ForeColor = Color.Black;
            btnImportarTodasLasVentas.Location = new Point(233, 3);
            btnImportarTodasLasVentas.Name = "btnImportarTodasLasVentas";
            btnImportarTodasLasVentas.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnImportarTodasLasVentas.Size = new Size(164, 39);
            btnImportarTodasLasVentas.TabIndex = 14;
            btnImportarTodasLasVentas.Text = "Importar todas";
            // 
            // btnImportarVentas
            // 
            btnImportarVentas.Animated = true;
            btnImportarVentas.BorderRadius = 18;
            btnImportarVentas.CustomizableEdges = customizableEdges5;
            btnImportarVentas.Dock = DockStyle.Fill;
            btnImportarVentas.FillColor = Color.PeachPuff;
            btnImportarVentas.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnImportarVentas.ForeColor = Color.Black;
            btnImportarVentas.Location = new Point(3, 3);
            btnImportarVentas.Name = "btnImportarVentas";
            btnImportarVentas.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnImportarVentas.Size = new Size(224, 39);
            btnImportarVentas.TabIndex = 15;
            btnImportarVentas.Text = "Importar ventas";
            // 
            // layoutSubBotones11
            // 
            layoutSubBotones11.BackColor = Color.White;
            layoutSubBotones11.ColumnCount = 3;
            layoutSubBotones11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
            layoutSubBotones11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            layoutSubBotones11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutSubBotones11.Controls.Add(btnEliminarCatalogo, 1, 0);
            layoutSubBotones11.Controls.Add(btnEnviarCatalogo, 0, 0);
            layoutSubBotones11.Dock = DockStyle.Fill;
            layoutSubBotones11.Location = new Point(3, 0);
            layoutSubBotones11.Margin = new Padding(3, 0, 0, 0);
            layoutSubBotones11.Name = "layoutSubBotones11";
            layoutSubBotones11.RowCount = 1;
            layoutSubBotones11.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutSubBotones11.Size = new Size(640, 45);
            layoutSubBotones11.TabIndex = 46;
            // 
            // btnEliminarCatalogo
            // 
            btnEliminarCatalogo.Animated = true;
            btnEliminarCatalogo.BorderColor = Color.Gainsboro;
            btnEliminarCatalogo.BorderRadius = 18;
            btnEliminarCatalogo.BorderThickness = 1;
            btnEliminarCatalogo.CustomizableEdges = customizableEdges7;
            btnEliminarCatalogo.Dock = DockStyle.Fill;
            btnEliminarCatalogo.FillColor = Color.White;
            btnEliminarCatalogo.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnEliminarCatalogo.ForeColor = Color.Gainsboro;
            btnEliminarCatalogo.HoverState.BorderColor = Color.PeachPuff;
            btnEliminarCatalogo.HoverState.FillColor = Color.PeachPuff;
            btnEliminarCatalogo.HoverState.ForeColor = Color.Black;
            btnEliminarCatalogo.Location = new Point(233, 3);
            btnEliminarCatalogo.Name = "btnEliminarCatalogo";
            btnEliminarCatalogo.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnEliminarCatalogo.Size = new Size(164, 39);
            btnEliminarCatalogo.TabIndex = 14;
            btnEliminarCatalogo.Text = "Eliminar";
            // 
            // btnEnviarCatalogo
            // 
            btnEnviarCatalogo.Animated = true;
            btnEnviarCatalogo.BorderRadius = 18;
            btnEnviarCatalogo.CustomizableEdges = customizableEdges9;
            btnEnviarCatalogo.Dock = DockStyle.Fill;
            btnEnviarCatalogo.FillColor = Color.PeachPuff;
            btnEnviarCatalogo.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnEnviarCatalogo.ForeColor = Color.Black;
            btnEnviarCatalogo.Location = new Point(3, 3);
            btnEnviarCatalogo.Name = "btnEnviarCatalogo";
            btnEnviarCatalogo.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnEnviarCatalogo.Size = new Size(224, 39);
            btnEnviarCatalogo.TabIndex = 15;
            btnEnviarCatalogo.Text = "Enviar catálogo";
            // 
            // layoutSeparadores2
            // 
            layoutSeparadores2.ColumnCount = 2;
            layoutSeparadores2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 366F));
            layoutSeparadores2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutSeparadores2.Controls.Add(fieldTituloArchivos, 0, 0);
            layoutSeparadores2.Controls.Add(separador2, 1, 0);
            layoutSeparadores2.Dock = DockStyle.Fill;
            layoutSeparadores2.Location = new Point(50, 405);
            layoutSeparadores2.Margin = new Padding(0);
            layoutSeparadores2.Name = "layoutSeparadores2";
            layoutSeparadores2.RowCount = 1;
            layoutSeparadores2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutSeparadores2.Size = new Size(1286, 70);
            layoutSeparadores2.TabIndex = 22;
            // 
            // fieldTituloArchivos
            // 
            fieldTituloArchivos.Dock = DockStyle.Fill;
            fieldTituloArchivos.Font = new Font("Segoe UI", 11.25F);
            fieldTituloArchivos.ForeColor = Color.FromArgb(  208,   197,   188);
            fieldTituloArchivos.Image = (Image) resources.GetObject("fieldTituloArchivos.Image");
            fieldTituloArchivos.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloArchivos.ImeMode = ImeMode.NoControl;
            fieldTituloArchivos.Location = new Point(15, 5);
            fieldTituloArchivos.Margin = new Padding(15, 5, 3, 3);
            fieldTituloArchivos.Name = "fieldTituloArchivos";
            fieldTituloArchivos.Size = new Size(348, 62);
            fieldTituloArchivos.TabIndex = 46;
            fieldTituloArchivos.Text = "      Archivos de ventas disponibles en el dispositivo";
            fieldTituloArchivos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // separador2
            // 
            separador2.Dock = DockStyle.Fill;
            separador2.FillColor = Color.FromArgb(  208,   197,   188);
            separador2.Location = new Point(369, 3);
            separador2.Name = "separador2";
            separador2.Size = new Size(914, 64);
            separador2.TabIndex = 45;
            // 
            // layoutTablaArchivosVentas
            // 
            layoutTablaArchivosVentas.ColumnCount = 1;
            layoutTablaArchivosVentas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTablaArchivosVentas.Controls.Add(layoutEncabezadosTabla, 0, 0);
            layoutTablaArchivosVentas.Controls.Add(panelArchivosVenta, 0, 2);
            layoutTablaArchivosVentas.Dock = DockStyle.Fill;
            layoutTablaArchivosVentas.Location = new Point(50, 475);
            layoutTablaArchivosVentas.Margin = new Padding(0);
            layoutTablaArchivosVentas.Name = "layoutTablaArchivosVentas";
            layoutTablaArchivosVentas.RowCount = 3;
            layoutTablaArchivosVentas.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            layoutTablaArchivosVentas.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutTablaArchivosVentas.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTablaArchivosVentas.Size = new Size(1286, 113);
            layoutTablaArchivosVentas.TabIndex = 23;
            // 
            // layoutEncabezadosTabla
            // 
            layoutEncabezadosTabla.BackColor = Color.WhiteSmoke;
            layoutEncabezadosTabla.ColumnCount = 5;
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutEncabezadosTabla.Controls.Add(fieldTituloAccion, 3, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloNombreArchivo, 0, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloFecha, 1, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloTamannoAproximado, 2, 0);
            layoutEncabezadosTabla.Dock = DockStyle.Fill;
            layoutEncabezadosTabla.Location = new Point(0, 0);
            layoutEncabezadosTabla.Margin = new Padding(0, 0, 0, 2);
            layoutEncabezadosTabla.Name = "layoutEncabezadosTabla";
            layoutEncabezadosTabla.RowCount = 1;
            layoutEncabezadosTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutEncabezadosTabla.Size = new Size(1286, 58);
            layoutEncabezadosTabla.TabIndex = 20;
            // 
            // fieldTituloAccion
            // 
            fieldTituloAccion.Dock = DockStyle.Fill;
            fieldTituloAccion.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloAccion.ForeColor = Color.Black;
            fieldTituloAccion.ImeMode = ImeMode.NoControl;
            fieldTituloAccion.Location = new Point(1127, 1);
            fieldTituloAccion.Margin = new Padding(1);
            fieldTituloAccion.Name = "fieldTituloAccion";
            fieldTituloAccion.Size = new Size(138, 56);
            fieldTituloAccion.TabIndex = 17;
            fieldTituloAccion.Text = "Acción";
            fieldTituloAccion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldTituloNombreArchivo
            // 
            fieldTituloNombreArchivo.Dock = DockStyle.Fill;
            fieldTituloNombreArchivo.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloNombreArchivo.ForeColor = Color.Black;
            fieldTituloNombreArchivo.ImeMode = ImeMode.NoControl;
            fieldTituloNombreArchivo.Location = new Point(1, 1);
            fieldTituloNombreArchivo.Margin = new Padding(1);
            fieldTituloNombreArchivo.Name = "fieldTituloNombreArchivo";
            fieldTituloNombreArchivo.Size = new Size(894, 56);
            fieldTituloNombreArchivo.TabIndex = 16;
            fieldTituloNombreArchivo.Text = "Nombre del archivo";
            fieldTituloNombreArchivo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldTituloFecha
            // 
            fieldTituloFecha.Dock = DockStyle.Fill;
            fieldTituloFecha.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloFecha.ForeColor = Color.Black;
            fieldTituloFecha.ImeMode = ImeMode.NoControl;
            fieldTituloFecha.Location = new Point(897, 1);
            fieldTituloFecha.Margin = new Padding(1);
            fieldTituloFecha.Name = "fieldTituloFecha";
            fieldTituloFecha.Size = new Size(118, 56);
            fieldTituloFecha.TabIndex = 15;
            fieldTituloFecha.Text = "Fecha";
            fieldTituloFecha.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldTituloTamannoAproximado
            // 
            fieldTituloTamannoAproximado.Dock = DockStyle.Fill;
            fieldTituloTamannoAproximado.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloTamannoAproximado.ForeColor = Color.Black;
            fieldTituloTamannoAproximado.ImeMode = ImeMode.NoControl;
            fieldTituloTamannoAproximado.Location = new Point(1017, 1);
            fieldTituloTamannoAproximado.Margin = new Padding(1);
            fieldTituloTamannoAproximado.Name = "fieldTituloTamannoAproximado";
            fieldTituloTamannoAproximado.Size = new Size(108, 56);
            fieldTituloTamannoAproximado.TabIndex = 15;
            fieldTituloTamannoAproximado.Text = "Tamaño aproximado";
            fieldTituloTamannoAproximado.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelArchivosVenta
            // 
            panelArchivosVenta.AutoScroll = true;
            panelArchivosVenta.Dock = DockStyle.Fill;
            panelArchivosVenta.Location = new Point(0, 70);
            panelArchivosVenta.Margin = new Padding(0);
            panelArchivosVenta.Name = "panelArchivosVenta";
            panelArchivosVenta.Size = new Size(1286, 43);
            panelArchivosVenta.TabIndex = 21;
            // 
            // VistaGestionAdvancePos
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1356, 608);
            Controls.Add(layoutVista);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaGestionAdvancePos";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistGestionProveedor";
            layoutVista.ResumeLayout(false);
            layoutTitulo.ResumeLayout(false);
            ((ISupportInitialize) fieldIcono).EndInit();
            layoutTitulos1.ResumeLayout(false);
            layoutDatos1.ResumeLayout(false);
            layoutSeparadores1.ResumeLayout(false);
            layoutTitulos2.ResumeLayout(false);
            layoutDescripcion1.ResumeLayout(false);
            layoutDatos2.ResumeLayout(false);
            layoudSubDatos22.ResumeLayout(false);
            layoudSubDatos21.ResumeLayout(false);
            layoutBotones1.ResumeLayout(false);
            layoutSubBotones12.ResumeLayout(false);
            layoutSubBotones11.ResumeLayout(false);
            layoutSeparadores2.ResumeLayout(false);
            layoutTablaArchivosVentas.ResumeLayout(false);
            layoutEncabezadosTabla.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutVista;
        private TableLayoutPanel layoutTitulo;
        private Label fieldTitulo;
        private PictureBox fieldIcono;
        private Label fieldSubtitulo;
        private TableLayoutPanel layoutTitulos1;
        private TableLayoutPanel layoutDatos1;
        private Guna2Button btnVerificarConexion;
        private Label fieldTituloEstadoCatalogoDispositivo;
        private Label fieldTituloEstadoInstalciónApp;
        private Label fieldTituloDispositivoAdb;
        private Label fieldEstadoDispositivoAdb;
        private Label fieldEstadoInstalcionApp;
        private Label fieldEstadoCatalogoDispositivo;
        private TableLayoutPanel layoutSeparadores1;
        private Guna2Separator separador1;
        private TableLayoutPanel layoutTitulos2;
        private Label fieldTituloImportacionVentas;
        private Label fieldTituloCatalogoProductos;
        private TableLayoutPanel layoutDescripcion1;
        private Label fieldDescripcion1;
        private Label fieldDescripcion2;
        private TableLayoutPanel layoutDatos2;
        private TableLayoutPanel layoudSubDatos22;
        private Label fieldArchivosDisponiblesDispositivo;
        private Label fieldTituloArchivosDisponiblesDispositivo;
        private TableLayoutPanel layoudSubDatos21;
        private Label fieldUltimaActualizacionCatalogo;
        private Label fieldTituloUltimaActualizacionCatalogo;
        private TableLayoutPanel layoutBotones1;
        private TableLayoutPanel layoutSubBotones11;
        private Guna2Button btnEliminarCatalogo;
        private Guna2Button btnEnviarCatalogo;
        private TableLayoutPanel layoutSubBotones12;
        private Guna2Button btnImportarTodasLasVentas;
        private Guna2Button btnImportarVentas;
        private TableLayoutPanel layoutSeparadores2;
        private Guna2Separator separador2;
        private TableLayoutPanel layoutTablaArchivosVentas;
        private TableLayoutPanel layoutEncabezadosTabla;
        private Label fieldTituloNombreArchivo;
        private Label fieldTituloFecha;
        private Label fieldTituloTamannoAproximado;
        private Panel panelArchivosVenta;
        private Label fieldTituloAccion;
        private Label fieldTituloArchivos;
    }
}