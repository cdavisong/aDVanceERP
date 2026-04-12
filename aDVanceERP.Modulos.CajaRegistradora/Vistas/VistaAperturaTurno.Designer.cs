namespace aDVanceERP.Modulos.CajaRegistradora.Vistas {
    partial class VistaAperturaTurno {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaAperturaTurno));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges29 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges30 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges31 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges32 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            layoutVista = new TableLayoutPanel();
            layoutTitulo = new TableLayoutPanel();
            fieldTitulo = new Label();
            panelAdvertencia = new Guna.UI2.WinForms.Guna2Panel();
            layoutPanelAdvertencia = new TableLayoutPanel();
            fieldTextoAdvertencia = new Label();
            fieldSubtitulo = new Label();
            layoutBotones = new TableLayoutPanel();
            btnSalir = new Guna.UI2.WinForms.Guna2Button();
            btnRegistrarActualizar = new Guna.UI2.WinForms.Guna2Button();
            layoutDistribucion1 = new TableLayoutPanel();
            layoutDistribucion2 = new TableLayoutPanel();
            fieldObservaciones = new Guna.UI2.WinForms.Guna2TextBox();
            layoutTitulos1 = new TableLayoutPanel();
            fieldTituloOperador = new Label();
            fieldTituloMontoApertura = new Label();
            fieldTituloAlmacen = new Label();
            layoutDatos1 = new TableLayoutPanel();
            layoutDistMontoMoneda1 = new TableLayoutPanel();
            fieldMonedaMonto = new Guna.UI2.WinForms.Guna2ComboBox();
            fieldMontoEfectivo = new Guna.UI2.WinForms.Guna2TextBox();
            fieldOperador = new Guna.UI2.WinForms.Guna2TextBox();
            fieldAlmacen = new Guna.UI2.WinForms.Guna2TextBox();
            fieldTituloObservaciones = new Label();
            fieldIcono = new PictureBox();
            fieldDescripcionCategoriaProducto = new ToolTip(components);
            layoutVista.SuspendLayout();
            layoutTitulo.SuspendLayout();
            panelAdvertencia.SuspendLayout();
            layoutPanelAdvertencia.SuspendLayout();
            layoutBotones.SuspendLayout();
            layoutDistribucion1.SuspendLayout();
            layoutDistribucion2.SuspendLayout();
            layoutTitulos1.SuspendLayout();
            layoutDatos1.SuspendLayout();
            layoutDistMontoMoneda1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) fieldIcono).BeginInit();
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
            layoutVista.Controls.Add(layoutTitulo, 2, 1);
            layoutVista.Controls.Add(fieldSubtitulo, 2, 2);
            layoutVista.Controls.Add(layoutBotones, 2, 6);
            layoutVista.Controls.Add(layoutDistribucion1, 2, 4);
            layoutVista.Controls.Add(fieldIcono, 1, 1);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 8;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 190F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.Size = new Size(1356, 685);
            layoutVista.TabIndex = 5;
            // 
            // layoutTitulo
            // 
            layoutTitulo.ColumnCount = 2;
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTitulo.Controls.Add(fieldTitulo, 0, 0);
            layoutTitulo.Controls.Add(panelAdvertencia, 1, 0);
            layoutTitulo.Dock = DockStyle.Fill;
            layoutTitulo.Location = new Point(50, 10);
            layoutTitulo.Margin = new Padding(0);
            layoutTitulo.Name = "layoutTitulo";
            layoutTitulo.RowCount = 1;
            layoutTitulo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTitulo.Size = new Size(1286, 45);
            layoutTitulo.TabIndex = 14;
            // 
            // fieldTitulo
            // 
            fieldTitulo.Dock = DockStyle.Fill;
            fieldTitulo.Font = new Font("Segoe UI", 20.25F);
            fieldTitulo.ForeColor = Color.Black;
            fieldTitulo.ImeMode = ImeMode.NoControl;
            fieldTitulo.Location = new Point(3, 0);
            fieldTitulo.Name = "fieldTitulo";
            fieldTitulo.Size = new Size(244, 45);
            fieldTitulo.TabIndex = 3;
            fieldTitulo.Text = "Apertura de turno";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelAdvertencia
            // 
            panelAdvertencia.BackColor = Color.Transparent;
            panelAdvertencia.BorderColor = Color.FromArgb(  255,   224,   130);
            panelAdvertencia.BorderRadius = 8;
            panelAdvertencia.BorderThickness = 1;
            panelAdvertencia.Controls.Add(layoutPanelAdvertencia);
            panelAdvertencia.CustomizableEdges = customizableEdges19;
            panelAdvertencia.Dock = DockStyle.Left;
            panelAdvertencia.FillColor = Color.FromArgb(  255,   251,   230);
            panelAdvertencia.Location = new Point(256, 6);
            panelAdvertencia.Margin = new Padding(6);
            panelAdvertencia.Name = "panelAdvertencia";
            panelAdvertencia.ShadowDecoration.BorderRadius = 8;
            panelAdvertencia.ShadowDecoration.CustomizableEdges = customizableEdges20;
            panelAdvertencia.ShadowDecoration.Depth = 10;
            panelAdvertencia.Size = new Size(497, 33);
            panelAdvertencia.TabIndex = 53;
            // 
            // layoutPanelAdvertencia
            // 
            layoutPanelAdvertencia.ColumnCount = 1;
            layoutPanelAdvertencia.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutPanelAdvertencia.Controls.Add(fieldTextoAdvertencia, 0, 0);
            layoutPanelAdvertencia.Dock = DockStyle.Fill;
            layoutPanelAdvertencia.Location = new Point(0, 0);
            layoutPanelAdvertencia.Name = "layoutPanelAdvertencia";
            layoutPanelAdvertencia.RowCount = 1;
            layoutPanelAdvertencia.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutPanelAdvertencia.Size = new Size(497, 33);
            layoutPanelAdvertencia.TabIndex = 0;
            // 
            // fieldTextoAdvertencia
            // 
            fieldTextoAdvertencia.Dock = DockStyle.Fill;
            fieldTextoAdvertencia.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point,  0);
            fieldTextoAdvertencia.ForeColor = Color.FromArgb(  144,   104,   14);
            fieldTextoAdvertencia.Image = (Image) resources.GetObject("fieldTextoAdvertencia.Image");
            fieldTextoAdvertencia.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTextoAdvertencia.ImeMode = ImeMode.NoControl;
            fieldTextoAdvertencia.Location = new Point(15, 5);
            fieldTextoAdvertencia.Margin = new Padding(15, 5, 3, 5);
            fieldTextoAdvertencia.Name = "fieldTextoAdvertencia";
            fieldTextoAdvertencia.Size = new Size(479, 23);
            fieldTextoAdvertencia.TabIndex = 47;
            fieldTextoAdvertencia.Text = "      Solo puede existir un turno abierto por almacén al mismo tiempo.";
            fieldTextoAdvertencia.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldSubtitulo
            // 
            fieldSubtitulo.Dock = DockStyle.Fill;
            fieldSubtitulo.Font = new Font("Segoe UI", 11.25F);
            fieldSubtitulo.ForeColor = Color.Gray;
            fieldSubtitulo.ImeMode = ImeMode.NoControl;
            fieldSubtitulo.Location = new Point(55, 60);
            fieldSubtitulo.Margin = new Padding(5, 5, 1, 1);
            fieldSubtitulo.Name = "fieldSubtitulo";
            fieldSubtitulo.Size = new Size(1280, 29);
            fieldSubtitulo.TabIndex = 2;
            fieldSubtitulo.Text = "Registro de fondo inicial de caja";
            // 
            // layoutBotones
            // 
            layoutBotones.BackColor = Color.White;
            layoutBotones.ColumnCount = 3;
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutBotones.Controls.Add(btnSalir, 2, 0);
            layoutBotones.Controls.Add(btnRegistrarActualizar, 1, 0);
            layoutBotones.Dock = DockStyle.Fill;
            layoutBotones.Location = new Point(53, 620);
            layoutBotones.Margin = new Padding(3, 0, 0, 0);
            layoutBotones.Name = "layoutBotones";
            layoutBotones.RowCount = 1;
            layoutBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBotones.Size = new Size(1283, 45);
            layoutBotones.TabIndex = 45;
            // 
            // btnSalir
            // 
            btnSalir.Animated = true;
            btnSalir.BorderColor = Color.Gainsboro;
            btnSalir.BorderRadius = 18;
            btnSalir.BorderThickness = 1;
            btnSalir.CustomizableEdges = customizableEdges21;
            btnSalir.Dock = DockStyle.Fill;
            btnSalir.FillColor = Color.White;
            btnSalir.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSalir.ForeColor = Color.Gainsboro;
            btnSalir.HoverState.BorderColor = Color.PeachPuff;
            btnSalir.HoverState.FillColor = Color.PeachPuff;
            btnSalir.HoverState.ForeColor = Color.Black;
            btnSalir.Location = new Point(1116, 3);
            btnSalir.Name = "btnSalir";
            btnSalir.ShadowDecoration.CustomizableEdges = customizableEdges22;
            btnSalir.Size = new Size(164, 39);
            btnSalir.TabIndex = 14;
            btnSalir.Text = "Salir";
            // 
            // btnRegistrarActualizar
            // 
            btnRegistrarActualizar.Animated = true;
            btnRegistrarActualizar.BorderRadius = 18;
            btnRegistrarActualizar.CustomizableEdges = customizableEdges23;
            btnRegistrarActualizar.Dock = DockStyle.Fill;
            btnRegistrarActualizar.FillColor = Color.PeachPuff;
            btnRegistrarActualizar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRegistrarActualizar.ForeColor = Color.Black;
            btnRegistrarActualizar.Location = new Point(886, 3);
            btnRegistrarActualizar.Name = "btnRegistrarActualizar";
            btnRegistrarActualizar.ShadowDecoration.CustomizableEdges = customizableEdges24;
            btnRegistrarActualizar.Size = new Size(224, 39);
            btnRegistrarActualizar.TabIndex = 15;
            btnRegistrarActualizar.Text = "Abrir turno";
            // 
            // layoutDistribucion1
            // 
            layoutDistribucion1.ColumnCount = 1;
            layoutDistribucion1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistribucion1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDistribucion1.Controls.Add(layoutDistribucion2, 0, 0);
            layoutDistribucion1.Dock = DockStyle.Fill;
            layoutDistribucion1.Location = new Point(50, 100);
            layoutDistribucion1.Margin = new Padding(0);
            layoutDistribucion1.Name = "layoutDistribucion1";
            layoutDistribucion1.RowCount = 1;
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucion1.Size = new Size(1286, 190);
            layoutDistribucion1.TabIndex = 50;
            // 
            // layoutDistribucion2
            // 
            layoutDistribucion2.ColumnCount = 1;
            layoutDistribucion2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistribucion2.Controls.Add(fieldObservaciones, 0, 4);
            layoutDistribucion2.Controls.Add(layoutTitulos1, 0, 0);
            layoutDistribucion2.Controls.Add(layoutDatos1, 0, 1);
            layoutDistribucion2.Controls.Add(fieldTituloObservaciones, 0, 3);
            layoutDistribucion2.Dock = DockStyle.Fill;
            layoutDistribucion2.Location = new Point(0, 0);
            layoutDistribucion2.Margin = new Padding(0);
            layoutDistribucion2.Name = "layoutDistribucion2";
            layoutDistribucion2.RowCount = 5;
            layoutDistribucion2.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutDistribucion2.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutDistribucion2.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutDistribucion2.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutDistribucion2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucion2.Size = new Size(1286, 190);
            layoutDistribucion2.TabIndex = 0;
            // 
            // fieldObservaciones
            // 
            fieldObservaciones.Animated = true;
            fieldObservaciones.BorderColor = Color.Gainsboro;
            fieldObservaciones.BorderRadius = 16;
            fieldObservaciones.Cursor = Cursors.IBeam;
            fieldObservaciones.CustomizableEdges = customizableEdges17;
            fieldObservaciones.DefaultText = "";
            fieldObservaciones.DisabledState.BorderColor = Color.White;
            fieldObservaciones.DisabledState.ForeColor = Color.DimGray;
            fieldObservaciones.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldObservaciones.Dock = DockStyle.Fill;
            fieldObservaciones.FocusedState.BorderColor = Color.SandyBrown;
            fieldObservaciones.Font = new Font("Segoe UI", 11.25F);
            fieldObservaciones.ForeColor = Color.Black;
            fieldObservaciones.HoverState.BorderColor = Color.SandyBrown;
            fieldObservaciones.IconLeft = (Image) resources.GetObject("fieldObservaciones.IconLeft");
            fieldObservaciones.IconLeftOffset = new Point(10, -19);
            fieldObservaciones.Location = new Point(5, 105);
            fieldObservaciones.Margin = new Padding(5);
            fieldObservaciones.Multiline = true;
            fieldObservaciones.Name = "fieldObservaciones";
            fieldObservaciones.PasswordChar = '\0';
            fieldObservaciones.PlaceholderForeColor = Color.DimGray;
            fieldObservaciones.PlaceholderText = "Notas de apertura...";
            fieldObservaciones.SelectedText = "";
            fieldObservaciones.ShadowDecoration.CustomizableEdges = customizableEdges18;
            fieldObservaciones.Size = new Size(1276, 80);
            fieldObservaciones.TabIndex = 48;
            fieldObservaciones.TextOffset = new Point(5, 0);
            // 
            // layoutTitulos1
            // 
            layoutTitulos1.ColumnCount = 4;
            layoutTitulos1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutTitulos1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutTitulos1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            layoutTitulos1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTitulos1.Controls.Add(fieldTituloOperador, 2, 0);
            layoutTitulos1.Controls.Add(fieldTituloMontoApertura, 1, 0);
            layoutTitulos1.Controls.Add(fieldTituloAlmacen, 0, 0);
            layoutTitulos1.Dock = DockStyle.Fill;
            layoutTitulos1.Location = new Point(0, 0);
            layoutTitulos1.Margin = new Padding(0);
            layoutTitulos1.Name = "layoutTitulos1";
            layoutTitulos1.RowCount = 1;
            layoutTitulos1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTitulos1.Size = new Size(1286, 25);
            layoutTitulos1.TabIndex = 47;
            // 
            // fieldTituloOperador
            // 
            fieldTituloOperador.Dock = DockStyle.Fill;
            fieldTituloOperador.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldTituloOperador.ForeColor = Color.DimGray;
            fieldTituloOperador.ImeMode = ImeMode.NoControl;
            fieldTituloOperador.Location = new Point(501, 1);
            fieldTituloOperador.Margin = new Padding(1);
            fieldTituloOperador.Name = "fieldTituloOperador";
            fieldTituloOperador.Size = new Size(198, 23);
            fieldTituloOperador.TabIndex = 29;
            fieldTituloOperador.Text = "OPERADOR";
            fieldTituloOperador.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloMontoApertura
            // 
            fieldTituloMontoApertura.Dock = DockStyle.Fill;
            fieldTituloMontoApertura.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldTituloMontoApertura.ForeColor = Color.DimGray;
            fieldTituloMontoApertura.ImeMode = ImeMode.NoControl;
            fieldTituloMontoApertura.Location = new Point(251, 1);
            fieldTituloMontoApertura.Margin = new Padding(1);
            fieldTituloMontoApertura.Name = "fieldTituloMontoApertura";
            fieldTituloMontoApertura.Size = new Size(248, 23);
            fieldTituloMontoApertura.TabIndex = 28;
            fieldTituloMontoApertura.Text = "MONTO DE APERTURA (EFECTIVO)";
            fieldTituloMontoApertura.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloAlmacen
            // 
            fieldTituloAlmacen.Dock = DockStyle.Fill;
            fieldTituloAlmacen.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldTituloAlmacen.ForeColor = Color.DimGray;
            fieldTituloAlmacen.ImeMode = ImeMode.NoControl;
            fieldTituloAlmacen.Location = new Point(1, 1);
            fieldTituloAlmacen.Margin = new Padding(1);
            fieldTituloAlmacen.Name = "fieldTituloAlmacen";
            fieldTituloAlmacen.Size = new Size(248, 23);
            fieldTituloAlmacen.TabIndex = 27;
            fieldTituloAlmacen.Text = "ALMACÉN";
            fieldTituloAlmacen.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutDatos1
            // 
            layoutDatos1.ColumnCount = 4;
            layoutDatos1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutDatos1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutDatos1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            layoutDatos1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDatos1.Controls.Add(layoutDistMontoMoneda1, 1, 0);
            layoutDatos1.Controls.Add(fieldOperador, 2, 0);
            layoutDatos1.Controls.Add(fieldAlmacen, 0, 0);
            layoutDatos1.Dock = DockStyle.Fill;
            layoutDatos1.Location = new Point(0, 25);
            layoutDatos1.Margin = new Padding(0);
            layoutDatos1.Name = "layoutDatos1";
            layoutDatos1.RowCount = 1;
            layoutDatos1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDatos1.Size = new Size(1286, 45);
            layoutDatos1.TabIndex = 40;
            // 
            // layoutDistMontoMoneda1
            // 
            layoutDistMontoMoneda1.ColumnCount = 2;
            layoutDistMontoMoneda1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistMontoMoneda1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85F));
            layoutDistMontoMoneda1.Controls.Add(fieldMonedaMonto, 1, 0);
            layoutDistMontoMoneda1.Controls.Add(fieldMontoEfectivo, 0, 0);
            layoutDistMontoMoneda1.Dock = DockStyle.Fill;
            layoutDistMontoMoneda1.Location = new Point(250, 0);
            layoutDistMontoMoneda1.Margin = new Padding(0);
            layoutDistMontoMoneda1.Name = "layoutDistMontoMoneda1";
            layoutDistMontoMoneda1.RowCount = 1;
            layoutDistMontoMoneda1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistMontoMoneda1.Size = new Size(250, 45);
            layoutDistMontoMoneda1.TabIndex = 40;
            // 
            // fieldMonedaMonto
            // 
            fieldMonedaMonto.Animated = true;
            fieldMonedaMonto.AutoRoundedCorners = true;
            fieldMonedaMonto.BackColor = Color.Transparent;
            fieldMonedaMonto.BorderColor = Color.Gainsboro;
            fieldMonedaMonto.BorderRadius = 16;
            customizableEdges25.BottomLeft = false;
            customizableEdges25.TopLeft = false;
            fieldMonedaMonto.CustomizableEdges = customizableEdges25;
            fieldMonedaMonto.Dock = DockStyle.Fill;
            fieldMonedaMonto.DrawMode = DrawMode.OwnerDrawFixed;
            fieldMonedaMonto.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldMonedaMonto.FillColor = Color.Gainsboro;
            fieldMonedaMonto.FocusedColor = Color.Gainsboro;
            fieldMonedaMonto.FocusedState.BorderColor = Color.Gainsboro;
            fieldMonedaMonto.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldMonedaMonto.ForeColor = Color.FromArgb(  64,   64,   64);
            fieldMonedaMonto.ItemHeight = 29;
            fieldMonedaMonto.Items.AddRange(new object[] { "CUP", "USD", "MLC" });
            fieldMonedaMonto.Location = new Point(165, 5);
            fieldMonedaMonto.Margin = new Padding(0, 5, 5, 5);
            fieldMonedaMonto.Name = "fieldMonedaMonto";
            fieldMonedaMonto.ShadowDecoration.CustomizableEdges = customizableEdges26;
            fieldMonedaMonto.Size = new Size(80, 35);
            fieldMonedaMonto.StartIndex = 0;
            fieldMonedaMonto.TabIndex = 40;
            fieldMonedaMonto.TextOffset = new Point(10, 0);
            // 
            // fieldMontoEfectivo
            // 
            fieldMontoEfectivo.Animated = true;
            fieldMontoEfectivo.AutoRoundedCorners = true;
            fieldMontoEfectivo.BorderColor = Color.Gainsboro;
            fieldMontoEfectivo.BorderRadius = 16;
            fieldMontoEfectivo.Cursor = Cursors.IBeam;
            customizableEdges27.BottomRight = false;
            customizableEdges27.TopRight = false;
            fieldMontoEfectivo.CustomizableEdges = customizableEdges27;
            fieldMontoEfectivo.DefaultText = "";
            fieldMontoEfectivo.DisabledState.BorderColor = Color.White;
            fieldMontoEfectivo.DisabledState.ForeColor = Color.DimGray;
            fieldMontoEfectivo.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldMontoEfectivo.Dock = DockStyle.Fill;
            fieldMontoEfectivo.FocusedState.BorderColor = Color.SandyBrown;
            fieldMontoEfectivo.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldMontoEfectivo.ForeColor = Color.Black;
            fieldMontoEfectivo.HoverState.BorderColor = Color.SandyBrown;
            fieldMontoEfectivo.IconLeftOffset = new Point(10, 0);
            fieldMontoEfectivo.IconLeftSize = new Size(12, 12);
            fieldMontoEfectivo.IconRightOffset = new Point(6, 0);
            fieldMontoEfectivo.IconRightSize = new Size(12, 12);
            fieldMontoEfectivo.Location = new Point(5, 5);
            fieldMontoEfectivo.Margin = new Padding(5, 5, 0, 5);
            fieldMontoEfectivo.Name = "fieldMontoEfectivo";
            fieldMontoEfectivo.PasswordChar = '\0';
            fieldMontoEfectivo.PlaceholderForeColor = Color.DimGray;
            fieldMontoEfectivo.PlaceholderText = "0,00";
            fieldMontoEfectivo.SelectedText = "";
            fieldMontoEfectivo.ShadowDecoration.CustomizableEdges = customizableEdges28;
            fieldMontoEfectivo.Size = new Size(160, 35);
            fieldMontoEfectivo.TabIndex = 39;
            fieldMontoEfectivo.TextAlign = HorizontalAlignment.Right;
            fieldMontoEfectivo.TextOffset = new Point(5, 0);
            // 
            // fieldOperador
            // 
            fieldOperador.Animated = true;
            fieldOperador.AutoRoundedCorners = true;
            fieldOperador.BorderColor = Color.Gainsboro;
            fieldOperador.BorderRadius = 16;
            fieldOperador.Cursor = Cursors.IBeam;
            fieldOperador.CustomizableEdges = customizableEdges29;
            fieldOperador.DefaultText = "";
            fieldOperador.DisabledState.BorderColor = Color.White;
            fieldOperador.DisabledState.ForeColor = Color.DimGray;
            fieldOperador.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldOperador.Dock = DockStyle.Fill;
            fieldOperador.Enabled = false;
            fieldOperador.FocusedState.BorderColor = Color.SandyBrown;
            fieldOperador.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldOperador.ForeColor = Color.Black;
            fieldOperador.HoverState.BorderColor = Color.SandyBrown;
            fieldOperador.IconLeftOffset = new Point(10, 0);
            fieldOperador.Location = new Point(505, 5);
            fieldOperador.Margin = new Padding(5);
            fieldOperador.Name = "fieldOperador";
            fieldOperador.PasswordChar = '\0';
            fieldOperador.PlaceholderForeColor = Color.DimGray;
            fieldOperador.PlaceholderText = "";
            fieldOperador.SelectedText = "";
            fieldOperador.ShadowDecoration.CustomizableEdges = customizableEdges30;
            fieldOperador.Size = new Size(190, 35);
            fieldOperador.TabIndex = 39;
            fieldOperador.TextOffset = new Point(5, 0);
            // 
            // fieldAlmacen
            // 
            fieldAlmacen.Animated = true;
            fieldAlmacen.AutoRoundedCorners = true;
            fieldAlmacen.BorderColor = Color.Gainsboro;
            fieldAlmacen.BorderRadius = 16;
            fieldAlmacen.Cursor = Cursors.IBeam;
            fieldAlmacen.CustomizableEdges = customizableEdges31;
            fieldAlmacen.DefaultText = "";
            fieldAlmacen.DisabledState.BorderColor = Color.White;
            fieldAlmacen.DisabledState.ForeColor = Color.DimGray;
            fieldAlmacen.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldAlmacen.Dock = DockStyle.Fill;
            fieldAlmacen.Enabled = false;
            fieldAlmacen.FocusedState.BorderColor = Color.SandyBrown;
            fieldAlmacen.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldAlmacen.ForeColor = Color.Black;
            fieldAlmacen.HoverState.BorderColor = Color.SandyBrown;
            fieldAlmacen.IconLeftOffset = new Point(10, 0);
            fieldAlmacen.Location = new Point(5, 5);
            fieldAlmacen.Margin = new Padding(5);
            fieldAlmacen.Name = "fieldAlmacen";
            fieldAlmacen.PasswordChar = '\0';
            fieldAlmacen.PlaceholderForeColor = Color.DimGray;
            fieldAlmacen.PlaceholderText = "";
            fieldAlmacen.SelectedText = "";
            fieldAlmacen.ShadowDecoration.CustomizableEdges = customizableEdges32;
            fieldAlmacen.Size = new Size(240, 35);
            fieldAlmacen.TabIndex = 37;
            fieldAlmacen.TextOffset = new Point(5, 0);
            // 
            // fieldTituloObservaciones
            // 
            fieldTituloObservaciones.Dock = DockStyle.Fill;
            fieldTituloObservaciones.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldTituloObservaciones.ForeColor = Color.DimGray;
            fieldTituloObservaciones.ImeMode = ImeMode.NoControl;
            fieldTituloObservaciones.Location = new Point(1, 76);
            fieldTituloObservaciones.Margin = new Padding(1);
            fieldTituloObservaciones.Name = "fieldTituloObservaciones";
            fieldTituloObservaciones.Size = new Size(1284, 23);
            fieldTituloObservaciones.TabIndex = 49;
            fieldTituloObservaciones.Text = "OBSERVACIONES (OPCIONAL)";
            fieldTituloObservaciones.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldIcono
            // 
            fieldIcono.BackgroundImage = Properties.Resources.cash_registerB_24px;
            fieldIcono.BackgroundImageLayout = ImageLayout.Center;
            fieldIcono.Dock = DockStyle.Fill;
            fieldIcono.Location = new Point(20, 16);
            fieldIcono.Margin = new Padding(0, 6, 0, 0);
            fieldIcono.Name = "fieldIcono";
            fieldIcono.Size = new Size(30, 39);
            fieldIcono.TabIndex = 52;
            fieldIcono.TabStop = false;
            // 
            // fieldDescripcionCategoriaProducto
            // 
            fieldDescripcionCategoriaProducto.BackColor = Color.PeachPuff;
            // 
            // VistaAperturaTurno
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1356, 685);
            Controls.Add(layoutVista);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "VistaAperturaTurno";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "VistaGestionCostosProduccion";
            layoutVista.ResumeLayout(false);
            layoutTitulo.ResumeLayout(false);
            panelAdvertencia.ResumeLayout(false);
            layoutPanelAdvertencia.ResumeLayout(false);
            layoutBotones.ResumeLayout(false);
            layoutDistribucion1.ResumeLayout(false);
            layoutDistribucion2.ResumeLayout(false);
            layoutTitulos1.ResumeLayout(false);
            layoutDatos1.ResumeLayout(false);
            layoutDistMontoMoneda1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) fieldIcono).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutVista;
        private TableLayoutPanel layoutTitulo;
        private Label fieldTitulo;
        private Label fieldSubtitulo;
        private TableLayoutPanel layoutDatos1;
        private TableLayoutPanel layoutBotones;
        private Guna.UI2.WinForms.Guna2Button btnSalir;
        private Guna.UI2.WinForms.Guna2Button btnRegistrarActualizar;
        private TableLayoutPanel layoutTitulos1;
        private Guna.UI2.WinForms.Guna2TextBox fieldCodigo;
        private TableLayoutPanel layoutEsVendible;
        private Label fieldTextoEsVendible;
        private Guna.UI2.WinForms.Guna2CheckBox fieldEsVendible;
        private Guna.UI2.WinForms.Guna2ComboBox fieldUnidadMedida;
        private TableLayoutPanel layoutDistribucion1;
        private TableLayoutPanel layoutDistribucion2;
        private Guna.UI2.WinForms.Guna2TextBox fieldObservaciones;
        private ToolTip fieldDescripcionCategoriaProducto;
        private PictureBox fieldIcono;
        private Guna.UI2.WinForms.Guna2TextBox fieldAlmacen;
        private Guna.UI2.WinForms.Guna2TextBox fieldOperador;
        private Guna.UI2.WinForms.Guna2Panel panelAdvertencia;
        private TableLayoutPanel layoutPanelAdvertencia;
        private Label fieldTextoAdvertencia;
        private TableLayoutPanel layoutDistMontoMoneda1;
        private Guna.UI2.WinForms.Guna2ComboBox fieldMonedaMonto;
        private Guna.UI2.WinForms.Guna2TextBox fieldMontoEfectivo;
        private Label fieldTituloOperador;
        private Label fieldTituloMontoApertura;
        private Label fieldTituloAlmacen;
        private Label fieldTituloObservaciones;
    }
}