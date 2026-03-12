using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    partial class VistaEstadisticasInventario {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaEstadisticasInventario));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutVista = new TableLayoutPanel();
            layoutTitulo = new TableLayoutPanel();
            btnActualizar = new Guna2Button();
            fieldTitulo = new Label();
            fieldIcono = new PictureBox();
            fieldSubtitulo = new Label();
            layoutDistribucion1 = new TableLayoutPanel();
            panelMovimientosHoy = new Guna2Panel();
            layoutMovimientosHoy = new TableLayoutPanel();
            fieldTituloMovimientosHoy = new Label();
            fieldMovimientosHoy = new Label();
            panelAlmacenesActivos = new Guna2Panel();
            layoutAlmacenesActivos = new TableLayoutPanel();
            fieldTituloAlmacenesActivos = new Label();
            fieldAlmacenesActivos = new Label();
            panelValorTotalInventario = new Guna2Panel();
            layoutValorTotalInventario = new TableLayoutPanel();
            fieldTexto3 = new Label();
            fieldTituloValorTotalInventario = new Label();
            layoutSubValorTotalInventario = new TableLayoutPanel();
            fieldValorTotalInventario = new Label();
            simboloPeso = new Label();
            panelSinStock = new Guna2Panel();
            layoutSinStock = new TableLayoutPanel();
            fieldTexto2 = new Label();
            fieldTituloSinStock = new Label();
            fieldSinStock = new Label();
            panelBajoStockMinimo = new Guna2Panel();
            layoutBajoStockMinimo = new TableLayoutPanel();
            fieldTexto1 = new Label();
            fieldTituloBajoStockMinimo = new Label();
            fieldBajoStockMinimo = new Label();
            panelProductosActivos = new Guna2Panel();
            layoutPanelProductosActivos = new TableLayoutPanel();
            fieldProductosNuevos = new Label();
            fieldTituloProductosActivos = new Label();
            fieldProductosActivos = new Label();
            layoutDistribucion2 = new TableLayoutPanel();
            panelTopProductosValor = new Guna2Panel();
            layoutTopProductosValor = new TableLayoutPanel();
            fieldTituloTopProductosValor = new Label();
            layoutTablaTopProducosValor = new TableLayoutPanel();
            layoutEncabezadosTabla = new TableLayoutPanel();
            fieldTituloNumeroProducto = new Label();
            fieldTituloNombreProducto = new Label();
            fieldTituloValorProducto = new Label();
            panelValorPorAlmacen = new Guna2Panel();
            layoutValorPorAlmacen = new TableLayoutPanel();
            fieldTituloValorPorAlmacen = new Label();
            fieldValorAlmacen = new PictureBox();
            panelEvolucionMovimientos = new Guna2Panel();
            layoutEvolucionMovimientos = new TableLayoutPanel();
            fieldTituloEvolucionMovimientos = new Label();
            fieldEvolucionMovimientos = new PictureBox();
            panelTuplasTopProductosValor = new Panel();
            layoutVista.SuspendLayout();
            layoutTitulo.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            layoutDistribucion1.SuspendLayout();
            panelMovimientosHoy.SuspendLayout();
            layoutMovimientosHoy.SuspendLayout();
            panelAlmacenesActivos.SuspendLayout();
            layoutAlmacenesActivos.SuspendLayout();
            panelValorTotalInventario.SuspendLayout();
            layoutValorTotalInventario.SuspendLayout();
            layoutSubValorTotalInventario.SuspendLayout();
            panelSinStock.SuspendLayout();
            layoutSinStock.SuspendLayout();
            panelBajoStockMinimo.SuspendLayout();
            layoutBajoStockMinimo.SuspendLayout();
            panelProductosActivos.SuspendLayout();
            layoutPanelProductosActivos.SuspendLayout();
            layoutDistribucion2.SuspendLayout();
            panelTopProductosValor.SuspendLayout();
            layoutTopProductosValor.SuspendLayout();
            layoutTablaTopProducosValor.SuspendLayout();
            layoutEncabezadosTabla.SuspendLayout();
            panelValorPorAlmacen.SuspendLayout();
            layoutValorPorAlmacen.SuspendLayout();
            ((ISupportInitialize) fieldValorAlmacen).BeginInit();
            panelEvolucionMovimientos.SuspendLayout();
            layoutEvolucionMovimientos.SuspendLayout();
            ((ISupportInitialize) fieldEvolucionMovimientos).BeginInit();
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
            layoutVista.BackColor = SystemColors.ButtonFace;
            layoutVista.ColumnCount = 4;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.Controls.Add(layoutTitulo, 2, 1);
            layoutVista.Controls.Add(fieldIcono, 1, 1);
            layoutVista.Controls.Add(fieldSubtitulo, 2, 2);
            layoutVista.Controls.Add(layoutDistribucion1, 2, 4);
            layoutVista.Controls.Add(layoutDistribucion2, 2, 5);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 7;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.Size = new Size(1356, 608);
            layoutVista.TabIndex = 0;
            // 
            // layoutTitulo
            // 
            layoutTitulo.ColumnCount = 2;
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            layoutTitulo.Controls.Add(btnActualizar, 1, 0);
            layoutTitulo.Controls.Add(fieldTitulo, 0, 0);
            layoutTitulo.Dock = DockStyle.Fill;
            layoutTitulo.Location = new Point(50, 10);
            layoutTitulo.Margin = new Padding(0);
            layoutTitulo.Name = "layoutTitulo";
            layoutTitulo.RowCount = 1;
            layoutTitulo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTitulo.Size = new Size(1286, 45);
            layoutTitulo.TabIndex = 14;
            // 
            // btnActualizar
            // 
            btnActualizar.Animated = true;
            btnActualizar.BorderColor = Color.Gainsboro;
            btnActualizar.BorderRadius = 18;
            btnActualizar.BorderThickness = 1;
            btnActualizar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage");
            btnActualizar.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnActualizar.CustomImages.ImageAlign = HorizontalAlignment.Left;
            btnActualizar.CustomImages.ImageOffset = new Point(25, 0);
            btnActualizar.CustomizableEdges = customizableEdges1;
            btnActualizar.Dock = DockStyle.Fill;
            btnActualizar.FillColor = Color.White;
            btnActualizar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnActualizar.ForeColor = Color.Gainsboro;
            btnActualizar.HoverState.BorderColor = Color.PeachPuff;
            btnActualizar.HoverState.FillColor = Color.PeachPuff;
            btnActualizar.HoverState.ForeColor = Color.Black;
            btnActualizar.Location = new Point(1089, 3);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnActualizar.Size = new Size(194, 39);
            btnActualizar.TabIndex = 17;
            btnActualizar.Text = "Actualizar";
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
            fieldTitulo.Text = "Estadísticas generales de inventario";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldIcono
            // 
            fieldIcono.BackgroundImage = Properties.Resources.inventory_24px;
            fieldIcono.BackgroundImageLayout = ImageLayout.Center;
            fieldIcono.Dock = DockStyle.Fill;
            fieldIcono.Location = new Point(20, 16);
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
            fieldSubtitulo.ForeColor = Color.DimGray;
            fieldSubtitulo.ImeMode = ImeMode.NoControl;
            fieldSubtitulo.Location = new Point(55, 60);
            fieldSubtitulo.Margin = new Padding(5, 5, 1, 1);
            fieldSubtitulo.Name = "fieldSubtitulo";
            fieldSubtitulo.Size = new Size(1280, 29);
            fieldSubtitulo.TabIndex = 2;
            fieldSubtitulo.Text = "Resumen operativo actualizado ahora";
            // 
            // layoutDistribucion1
            // 
            layoutDistribucion1.ColumnCount = 6;
            layoutDistribucion1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.4286947F));
            layoutDistribucion1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.4286938F));
            layoutDistribucion1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.4286938F));
            layoutDistribucion1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.85653F));
            layoutDistribucion1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.4286938F));
            layoutDistribucion1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.4286938F));
            layoutDistribucion1.Controls.Add(panelMovimientosHoy, 5, 0);
            layoutDistribucion1.Controls.Add(panelAlmacenesActivos, 4, 0);
            layoutDistribucion1.Controls.Add(panelValorTotalInventario, 3, 0);
            layoutDistribucion1.Controls.Add(panelSinStock, 2, 0);
            layoutDistribucion1.Controls.Add(panelBajoStockMinimo, 1, 0);
            layoutDistribucion1.Controls.Add(panelProductosActivos, 0, 0);
            layoutDistribucion1.Dock = DockStyle.Fill;
            layoutDistribucion1.Location = new Point(50, 110);
            layoutDistribucion1.Margin = new Padding(0);
            layoutDistribucion1.Name = "layoutDistribucion1";
            layoutDistribucion1.RowCount = 1;
            layoutDistribucion1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucion1.Size = new Size(1286, 150);
            layoutDistribucion1.TabIndex = 15;
            // 
            // panelMovimientosHoy
            // 
            panelMovimientosHoy.BackColor = Color.Transparent;
            panelMovimientosHoy.BorderRadius = 8;
            panelMovimientosHoy.Controls.Add(layoutMovimientosHoy);
            panelMovimientosHoy.CustomBorderColor = Color.PeachPuff;
            panelMovimientosHoy.CustomBorderThickness = new Padding(0, 5, 0, 0);
            panelMovimientosHoy.CustomizableEdges = customizableEdges3;
            panelMovimientosHoy.Dock = DockStyle.Fill;
            panelMovimientosHoy.FillColor = Color.White;
            panelMovimientosHoy.Location = new Point(1095, 10);
            panelMovimientosHoy.Margin = new Padding(10);
            panelMovimientosHoy.Name = "panelMovimientosHoy";
            panelMovimientosHoy.ShadowDecoration.BorderRadius = 8;
            panelMovimientosHoy.ShadowDecoration.CustomizableEdges = customizableEdges4;
            panelMovimientosHoy.ShadowDecoration.Depth = 10;
            panelMovimientosHoy.ShadowDecoration.Enabled = true;
            panelMovimientosHoy.Size = new Size(181, 130);
            panelMovimientosHoy.TabIndex = 3;
            // 
            // layoutMovimientosHoy
            // 
            layoutMovimientosHoy.BackColor = Color.Transparent;
            layoutMovimientosHoy.ColumnCount = 1;
            layoutMovimientosHoy.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutMovimientosHoy.Controls.Add(fieldTituloMovimientosHoy, 0, 1);
            layoutMovimientosHoy.Controls.Add(fieldMovimientosHoy, 0, 2);
            layoutMovimientosHoy.Dock = DockStyle.Fill;
            layoutMovimientosHoy.Location = new Point(0, 0);
            layoutMovimientosHoy.Name = "layoutMovimientosHoy";
            layoutMovimientosHoy.RowCount = 5;
            layoutMovimientosHoy.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutMovimientosHoy.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutMovimientosHoy.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutMovimientosHoy.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutMovimientosHoy.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutMovimientosHoy.Size = new Size(181, 130);
            layoutMovimientosHoy.TabIndex = 0;
            // 
            // fieldTituloMovimientosHoy
            // 
            fieldTituloMovimientosHoy.Dock = DockStyle.Fill;
            fieldTituloMovimientosHoy.Font = new Font("Segoe UI", 11.25F);
            fieldTituloMovimientosHoy.ForeColor = Color.DimGray;
            fieldTituloMovimientosHoy.ImeMode = ImeMode.NoControl;
            fieldTituloMovimientosHoy.Location = new Point(10, 10);
            fieldTituloMovimientosHoy.Margin = new Padding(10, 5, 1, 1);
            fieldTituloMovimientosHoy.Name = "fieldTituloMovimientosHoy";
            fieldTituloMovimientosHoy.Size = new Size(170, 29);
            fieldTituloMovimientosHoy.TabIndex = 5;
            fieldTituloMovimientosHoy.Text = "Movimientos hoy";
            fieldTituloMovimientosHoy.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldMovimientosHoy
            // 
            fieldMovimientosHoy.Dock = DockStyle.Fill;
            fieldMovimientosHoy.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold);
            fieldMovimientosHoy.ForeColor = Color.FromArgb(  64,   64,   64);
            fieldMovimientosHoy.ImeMode = ImeMode.NoControl;
            fieldMovimientosHoy.Location = new Point(10, 43);
            fieldMovimientosHoy.Margin = new Padding(10, 3, 3, 3);
            fieldMovimientosHoy.Name = "fieldMovimientosHoy";
            fieldMovimientosHoy.Size = new Size(168, 39);
            fieldMovimientosHoy.TabIndex = 4;
            fieldMovimientosHoy.Text = "0";
            fieldMovimientosHoy.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelAlmacenesActivos
            // 
            panelAlmacenesActivos.BackColor = Color.Transparent;
            panelAlmacenesActivos.BorderRadius = 8;
            panelAlmacenesActivos.Controls.Add(layoutAlmacenesActivos);
            panelAlmacenesActivos.CustomBorderColor = Color.LightBlue;
            panelAlmacenesActivos.CustomBorderThickness = new Padding(0, 5, 0, 0);
            panelAlmacenesActivos.CustomizableEdges = customizableEdges5;
            panelAlmacenesActivos.Dock = DockStyle.Fill;
            panelAlmacenesActivos.FillColor = Color.White;
            panelAlmacenesActivos.Location = new Point(897, 10);
            panelAlmacenesActivos.Margin = new Padding(10);
            panelAlmacenesActivos.Name = "panelAlmacenesActivos";
            panelAlmacenesActivos.ShadowDecoration.BorderRadius = 8;
            panelAlmacenesActivos.ShadowDecoration.CustomizableEdges = customizableEdges6;
            panelAlmacenesActivos.ShadowDecoration.Depth = 10;
            panelAlmacenesActivos.ShadowDecoration.Enabled = true;
            panelAlmacenesActivos.Size = new Size(178, 130);
            panelAlmacenesActivos.TabIndex = 2;
            // 
            // layoutAlmacenesActivos
            // 
            layoutAlmacenesActivos.BackColor = Color.Transparent;
            layoutAlmacenesActivos.ColumnCount = 1;
            layoutAlmacenesActivos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutAlmacenesActivos.Controls.Add(fieldTituloAlmacenesActivos, 0, 1);
            layoutAlmacenesActivos.Controls.Add(fieldAlmacenesActivos, 0, 2);
            layoutAlmacenesActivos.Dock = DockStyle.Fill;
            layoutAlmacenesActivos.Location = new Point(0, 0);
            layoutAlmacenesActivos.Name = "layoutAlmacenesActivos";
            layoutAlmacenesActivos.RowCount = 5;
            layoutAlmacenesActivos.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutAlmacenesActivos.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutAlmacenesActivos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutAlmacenesActivos.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutAlmacenesActivos.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutAlmacenesActivos.Size = new Size(178, 130);
            layoutAlmacenesActivos.TabIndex = 0;
            // 
            // fieldTituloAlmacenesActivos
            // 
            fieldTituloAlmacenesActivos.Dock = DockStyle.Fill;
            fieldTituloAlmacenesActivos.Font = new Font("Segoe UI", 11.25F);
            fieldTituloAlmacenesActivos.ForeColor = Color.DimGray;
            fieldTituloAlmacenesActivos.ImeMode = ImeMode.NoControl;
            fieldTituloAlmacenesActivos.Location = new Point(10, 10);
            fieldTituloAlmacenesActivos.Margin = new Padding(10, 5, 1, 1);
            fieldTituloAlmacenesActivos.Name = "fieldTituloAlmacenesActivos";
            fieldTituloAlmacenesActivos.Size = new Size(167, 29);
            fieldTituloAlmacenesActivos.TabIndex = 5;
            fieldTituloAlmacenesActivos.Text = "Almacenes activos";
            fieldTituloAlmacenesActivos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldAlmacenesActivos
            // 
            fieldAlmacenesActivos.Dock = DockStyle.Fill;
            fieldAlmacenesActivos.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold);
            fieldAlmacenesActivos.ForeColor = Color.FromArgb(  64,   64,   64);
            fieldAlmacenesActivos.ImeMode = ImeMode.NoControl;
            fieldAlmacenesActivos.Location = new Point(10, 43);
            fieldAlmacenesActivos.Margin = new Padding(10, 3, 3, 3);
            fieldAlmacenesActivos.Name = "fieldAlmacenesActivos";
            fieldAlmacenesActivos.Size = new Size(165, 39);
            fieldAlmacenesActivos.TabIndex = 4;
            fieldAlmacenesActivos.Text = "0";
            fieldAlmacenesActivos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelValorTotalInventario
            // 
            panelValorTotalInventario.BackColor = Color.Transparent;
            panelValorTotalInventario.BorderRadius = 8;
            panelValorTotalInventario.Controls.Add(layoutValorTotalInventario);
            panelValorTotalInventario.CustomBorderColor = Color.PeachPuff;
            panelValorTotalInventario.CustomBorderThickness = new Padding(0, 5, 0, 0);
            panelValorTotalInventario.CustomizableEdges = customizableEdges7;
            panelValorTotalInventario.Dock = DockStyle.Fill;
            panelValorTotalInventario.FillColor = Color.White;
            panelValorTotalInventario.Location = new Point(604, 10);
            panelValorTotalInventario.Margin = new Padding(10);
            panelValorTotalInventario.Name = "panelValorTotalInventario";
            panelValorTotalInventario.ShadowDecoration.BorderRadius = 8;
            panelValorTotalInventario.ShadowDecoration.CustomizableEdges = customizableEdges8;
            panelValorTotalInventario.ShadowDecoration.Depth = 10;
            panelValorTotalInventario.ShadowDecoration.Enabled = true;
            panelValorTotalInventario.Size = new Size(273, 130);
            panelValorTotalInventario.TabIndex = 4;
            // 
            // layoutValorTotalInventario
            // 
            layoutValorTotalInventario.BackColor = Color.Transparent;
            layoutValorTotalInventario.ColumnCount = 1;
            layoutValorTotalInventario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutValorTotalInventario.Controls.Add(fieldTexto3, 0, 3);
            layoutValorTotalInventario.Controls.Add(fieldTituloValorTotalInventario, 0, 1);
            layoutValorTotalInventario.Controls.Add(layoutSubValorTotalInventario, 0, 2);
            layoutValorTotalInventario.Dock = DockStyle.Fill;
            layoutValorTotalInventario.Location = new Point(0, 0);
            layoutValorTotalInventario.Name = "layoutValorTotalInventario";
            layoutValorTotalInventario.RowCount = 5;
            layoutValorTotalInventario.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutValorTotalInventario.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutValorTotalInventario.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutValorTotalInventario.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutValorTotalInventario.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutValorTotalInventario.Size = new Size(273, 130);
            layoutValorTotalInventario.TabIndex = 0;
            // 
            // fieldTexto3
            // 
            fieldTexto3.Dock = DockStyle.Fill;
            fieldTexto3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point,  0);
            fieldTexto3.ForeColor = Color.DimGray;
            fieldTexto3.ImeMode = ImeMode.NoControl;
            fieldTexto3.Location = new Point(10, 90);
            fieldTexto3.Margin = new Padding(10, 5, 1, 1);
            fieldTexto3.Name = "fieldTexto3";
            fieldTexto3.Size = new Size(262, 19);
            fieldTexto3.TabIndex = 6;
            fieldTexto3.Text = "en almacenes activos";
            fieldTexto3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloValorTotalInventario
            // 
            fieldTituloValorTotalInventario.Dock = DockStyle.Fill;
            fieldTituloValorTotalInventario.Font = new Font("Segoe UI", 11.25F);
            fieldTituloValorTotalInventario.ForeColor = Color.DimGray;
            fieldTituloValorTotalInventario.ImeMode = ImeMode.NoControl;
            fieldTituloValorTotalInventario.Location = new Point(10, 10);
            fieldTituloValorTotalInventario.Margin = new Padding(10, 5, 1, 1);
            fieldTituloValorTotalInventario.Name = "fieldTituloValorTotalInventario";
            fieldTituloValorTotalInventario.Size = new Size(262, 29);
            fieldTituloValorTotalInventario.TabIndex = 5;
            fieldTituloValorTotalInventario.Text = "Valor total del inventario";
            fieldTituloValorTotalInventario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutSubValorTotalInventario
            // 
            layoutSubValorTotalInventario.ColumnCount = 2;
            layoutSubValorTotalInventario.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 22F));
            layoutSubValorTotalInventario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutSubValorTotalInventario.Controls.Add(fieldValorTotalInventario, 1, 0);
            layoutSubValorTotalInventario.Controls.Add(simboloPeso, 0, 0);
            layoutSubValorTotalInventario.Dock = DockStyle.Fill;
            layoutSubValorTotalInventario.Location = new Point(10, 40);
            layoutSubValorTotalInventario.Margin = new Padding(10, 0, 0, 0);
            layoutSubValorTotalInventario.Name = "layoutSubValorTotalInventario";
            layoutSubValorTotalInventario.RowCount = 1;
            layoutSubValorTotalInventario.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutSubValorTotalInventario.Size = new Size(263, 45);
            layoutSubValorTotalInventario.TabIndex = 7;
            // 
            // fieldValorTotalInventario
            // 
            fieldValorTotalInventario.Dock = DockStyle.Fill;
            fieldValorTotalInventario.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold);
            fieldValorTotalInventario.ForeColor = Color.FromArgb(  64,   64,   64);
            fieldValorTotalInventario.ImeMode = ImeMode.NoControl;
            fieldValorTotalInventario.Location = new Point(25, 3);
            fieldValorTotalInventario.Margin = new Padding(3);
            fieldValorTotalInventario.Name = "fieldValorTotalInventario";
            fieldValorTotalInventario.Size = new Size(235, 39);
            fieldValorTotalInventario.TabIndex = 7;
            fieldValorTotalInventario.Text = "0";
            fieldValorTotalInventario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // simboloPeso
            // 
            simboloPeso.Dock = DockStyle.Fill;
            simboloPeso.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point,  0);
            simboloPeso.ForeColor = Color.DimGray;
            simboloPeso.ImeMode = ImeMode.NoControl;
            simboloPeso.Location = new Point(7, 3);
            simboloPeso.Margin = new Padding(7, 3, 1, 1);
            simboloPeso.Name = "simboloPeso";
            simboloPeso.Size = new Size(14, 41);
            simboloPeso.TabIndex = 6;
            simboloPeso.Text = "$";
            simboloPeso.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelSinStock
            // 
            panelSinStock.BackColor = Color.Transparent;
            panelSinStock.BorderRadius = 8;
            panelSinStock.Controls.Add(layoutSinStock);
            panelSinStock.CustomBorderColor = Color.Firebrick;
            panelSinStock.CustomBorderThickness = new Padding(0, 5, 0, 0);
            panelSinStock.CustomizableEdges = customizableEdges9;
            panelSinStock.Dock = DockStyle.Fill;
            panelSinStock.FillColor = Color.White;
            panelSinStock.Location = new Point(406, 10);
            panelSinStock.Margin = new Padding(10);
            panelSinStock.Name = "panelSinStock";
            panelSinStock.ShadowDecoration.BorderRadius = 8;
            panelSinStock.ShadowDecoration.CustomizableEdges = customizableEdges10;
            panelSinStock.ShadowDecoration.Depth = 10;
            panelSinStock.ShadowDecoration.Enabled = true;
            panelSinStock.Size = new Size(178, 130);
            panelSinStock.TabIndex = 3;
            // 
            // layoutSinStock
            // 
            layoutSinStock.BackColor = Color.Transparent;
            layoutSinStock.ColumnCount = 1;
            layoutSinStock.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutSinStock.Controls.Add(fieldTexto2, 0, 3);
            layoutSinStock.Controls.Add(fieldTituloSinStock, 0, 1);
            layoutSinStock.Controls.Add(fieldSinStock, 0, 2);
            layoutSinStock.Dock = DockStyle.Fill;
            layoutSinStock.Location = new Point(0, 0);
            layoutSinStock.Name = "layoutSinStock";
            layoutSinStock.RowCount = 5;
            layoutSinStock.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutSinStock.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutSinStock.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutSinStock.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutSinStock.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutSinStock.Size = new Size(178, 130);
            layoutSinStock.TabIndex = 0;
            // 
            // fieldTexto2
            // 
            fieldTexto2.Dock = DockStyle.Fill;
            fieldTexto2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point,  0);
            fieldTexto2.ForeColor = Color.DimGray;
            fieldTexto2.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTexto2.ImeMode = ImeMode.NoControl;
            fieldTexto2.Location = new Point(10, 90);
            fieldTexto2.Margin = new Padding(10, 5, 1, 1);
            fieldTexto2.Name = "fieldTexto2";
            fieldTexto2.Size = new Size(167, 19);
            fieldTexto2.TabIndex = 6;
            fieldTexto2.Text = "en todos los almacenes";
            fieldTexto2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloSinStock
            // 
            fieldTituloSinStock.Dock = DockStyle.Fill;
            fieldTituloSinStock.Font = new Font("Segoe UI", 11.25F);
            fieldTituloSinStock.ForeColor = Color.DimGray;
            fieldTituloSinStock.ImeMode = ImeMode.NoControl;
            fieldTituloSinStock.Location = new Point(10, 10);
            fieldTituloSinStock.Margin = new Padding(10, 5, 1, 1);
            fieldTituloSinStock.Name = "fieldTituloSinStock";
            fieldTituloSinStock.Size = new Size(167, 29);
            fieldTituloSinStock.TabIndex = 5;
            fieldTituloSinStock.Text = "Sin stock";
            fieldTituloSinStock.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldSinStock
            // 
            fieldSinStock.Dock = DockStyle.Fill;
            fieldSinStock.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold);
            fieldSinStock.ForeColor = Color.Firebrick;
            fieldSinStock.ImeMode = ImeMode.NoControl;
            fieldSinStock.Location = new Point(10, 43);
            fieldSinStock.Margin = new Padding(10, 3, 3, 3);
            fieldSinStock.Name = "fieldSinStock";
            fieldSinStock.Size = new Size(165, 39);
            fieldSinStock.TabIndex = 4;
            fieldSinStock.Text = "0";
            fieldSinStock.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelBajoStockMinimo
            // 
            panelBajoStockMinimo.BackColor = Color.Transparent;
            panelBajoStockMinimo.BorderRadius = 8;
            panelBajoStockMinimo.Controls.Add(layoutBajoStockMinimo);
            panelBajoStockMinimo.CustomBorderColor = Color.Peru;
            panelBajoStockMinimo.CustomBorderThickness = new Padding(0, 5, 0, 0);
            panelBajoStockMinimo.CustomizableEdges = customizableEdges11;
            panelBajoStockMinimo.Dock = DockStyle.Fill;
            panelBajoStockMinimo.FillColor = Color.White;
            panelBajoStockMinimo.Location = new Point(208, 10);
            panelBajoStockMinimo.Margin = new Padding(10);
            panelBajoStockMinimo.Name = "panelBajoStockMinimo";
            panelBajoStockMinimo.ShadowDecoration.BorderRadius = 8;
            panelBajoStockMinimo.ShadowDecoration.CustomizableEdges = customizableEdges12;
            panelBajoStockMinimo.ShadowDecoration.Depth = 10;
            panelBajoStockMinimo.ShadowDecoration.Enabled = true;
            panelBajoStockMinimo.Size = new Size(178, 130);
            panelBajoStockMinimo.TabIndex = 2;
            // 
            // layoutBajoStockMinimo
            // 
            layoutBajoStockMinimo.BackColor = Color.Transparent;
            layoutBajoStockMinimo.ColumnCount = 1;
            layoutBajoStockMinimo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBajoStockMinimo.Controls.Add(fieldTexto1, 0, 3);
            layoutBajoStockMinimo.Controls.Add(fieldTituloBajoStockMinimo, 0, 1);
            layoutBajoStockMinimo.Controls.Add(fieldBajoStockMinimo, 0, 2);
            layoutBajoStockMinimo.Dock = DockStyle.Fill;
            layoutBajoStockMinimo.Location = new Point(0, 0);
            layoutBajoStockMinimo.Name = "layoutBajoStockMinimo";
            layoutBajoStockMinimo.RowCount = 5;
            layoutBajoStockMinimo.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutBajoStockMinimo.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutBajoStockMinimo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBajoStockMinimo.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutBajoStockMinimo.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutBajoStockMinimo.Size = new Size(178, 130);
            layoutBajoStockMinimo.TabIndex = 0;
            // 
            // fieldTexto1
            // 
            fieldTexto1.Dock = DockStyle.Fill;
            fieldTexto1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point,  0);
            fieldTexto1.ForeColor = Color.DimGray;
            fieldTexto1.Image = (Image) resources.GetObject("fieldTexto1.Image");
            fieldTexto1.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTexto1.ImeMode = ImeMode.NoControl;
            fieldTexto1.Location = new Point(10, 90);
            fieldTexto1.Margin = new Padding(10, 5, 1, 1);
            fieldTexto1.Name = "fieldTexto1";
            fieldTexto1.Size = new Size(167, 19);
            fieldTexto1.TabIndex = 6;
            fieldTexto1.Text = "     requieren abastecer";
            fieldTexto1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloBajoStockMinimo
            // 
            fieldTituloBajoStockMinimo.Dock = DockStyle.Fill;
            fieldTituloBajoStockMinimo.Font = new Font("Segoe UI", 11.25F);
            fieldTituloBajoStockMinimo.ForeColor = Color.DimGray;
            fieldTituloBajoStockMinimo.ImeMode = ImeMode.NoControl;
            fieldTituloBajoStockMinimo.Location = new Point(10, 10);
            fieldTituloBajoStockMinimo.Margin = new Padding(10, 5, 1, 1);
            fieldTituloBajoStockMinimo.Name = "fieldTituloBajoStockMinimo";
            fieldTituloBajoStockMinimo.Size = new Size(167, 29);
            fieldTituloBajoStockMinimo.TabIndex = 5;
            fieldTituloBajoStockMinimo.Text = "Bajo stock mínimo";
            fieldTituloBajoStockMinimo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldBajoStockMinimo
            // 
            fieldBajoStockMinimo.Dock = DockStyle.Fill;
            fieldBajoStockMinimo.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold);
            fieldBajoStockMinimo.ForeColor = Color.Peru;
            fieldBajoStockMinimo.ImeMode = ImeMode.NoControl;
            fieldBajoStockMinimo.Location = new Point(10, 43);
            fieldBajoStockMinimo.Margin = new Padding(10, 3, 3, 3);
            fieldBajoStockMinimo.Name = "fieldBajoStockMinimo";
            fieldBajoStockMinimo.Size = new Size(165, 39);
            fieldBajoStockMinimo.TabIndex = 4;
            fieldBajoStockMinimo.Text = "0";
            fieldBajoStockMinimo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelProductosActivos
            // 
            panelProductosActivos.BackColor = Color.Transparent;
            panelProductosActivos.BorderRadius = 8;
            panelProductosActivos.Controls.Add(layoutPanelProductosActivos);
            panelProductosActivos.CustomBorderColor = Color.PeachPuff;
            panelProductosActivos.CustomBorderThickness = new Padding(0, 5, 0, 0);
            panelProductosActivos.CustomizableEdges = customizableEdges13;
            panelProductosActivos.Dock = DockStyle.Fill;
            panelProductosActivos.FillColor = Color.White;
            panelProductosActivos.Location = new Point(10, 10);
            panelProductosActivos.Margin = new Padding(10);
            panelProductosActivos.Name = "panelProductosActivos";
            panelProductosActivos.ShadowDecoration.BorderRadius = 8;
            panelProductosActivos.ShadowDecoration.CustomizableEdges = customizableEdges14;
            panelProductosActivos.ShadowDecoration.Depth = 10;
            panelProductosActivos.ShadowDecoration.Enabled = true;
            panelProductosActivos.Size = new Size(178, 130);
            panelProductosActivos.TabIndex = 1;
            // 
            // layoutPanelProductosActivos
            // 
            layoutPanelProductosActivos.BackColor = Color.Transparent;
            layoutPanelProductosActivos.ColumnCount = 1;
            layoutPanelProductosActivos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPanelProductosActivos.Controls.Add(fieldProductosNuevos, 0, 3);
            layoutPanelProductosActivos.Controls.Add(fieldTituloProductosActivos, 0, 1);
            layoutPanelProductosActivos.Controls.Add(fieldProductosActivos, 0, 2);
            layoutPanelProductosActivos.Dock = DockStyle.Fill;
            layoutPanelProductosActivos.Location = new Point(0, 0);
            layoutPanelProductosActivos.Name = "layoutPanelProductosActivos";
            layoutPanelProductosActivos.RowCount = 5;
            layoutPanelProductosActivos.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutPanelProductosActivos.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutPanelProductosActivos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutPanelProductosActivos.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutPanelProductosActivos.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutPanelProductosActivos.Size = new Size(178, 130);
            layoutPanelProductosActivos.TabIndex = 0;
            // 
            // fieldProductosNuevos
            // 
            fieldProductosNuevos.Dock = DockStyle.Fill;
            fieldProductosNuevos.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point,  0);
            fieldProductosNuevos.ForeColor = Color.DimGray;
            fieldProductosNuevos.ImeMode = ImeMode.NoControl;
            fieldProductosNuevos.Location = new Point(10, 90);
            fieldProductosNuevos.Margin = new Padding(10, 5, 1, 1);
            fieldProductosNuevos.Name = "fieldProductosNuevos";
            fieldProductosNuevos.Size = new Size(167, 19);
            fieldProductosNuevos.TabIndex = 6;
            fieldProductosNuevos.Text = "0 añadidos este mes";
            fieldProductosNuevos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloProductosActivos
            // 
            fieldTituloProductosActivos.Dock = DockStyle.Fill;
            fieldTituloProductosActivos.Font = new Font("Segoe UI", 11.25F);
            fieldTituloProductosActivos.ForeColor = Color.DimGray;
            fieldTituloProductosActivos.ImeMode = ImeMode.NoControl;
            fieldTituloProductosActivos.Location = new Point(10, 10);
            fieldTituloProductosActivos.Margin = new Padding(10, 5, 1, 1);
            fieldTituloProductosActivos.Name = "fieldTituloProductosActivos";
            fieldTituloProductosActivos.Size = new Size(167, 29);
            fieldTituloProductosActivos.TabIndex = 5;
            fieldTituloProductosActivos.Text = "Productos activos";
            fieldTituloProductosActivos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldProductosActivos
            // 
            fieldProductosActivos.Dock = DockStyle.Fill;
            fieldProductosActivos.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldProductosActivos.ForeColor = Color.FromArgb(  64,   64,   64);
            fieldProductosActivos.ImeMode = ImeMode.NoControl;
            fieldProductosActivos.Location = new Point(10, 43);
            fieldProductosActivos.Margin = new Padding(10, 3, 3, 3);
            fieldProductosActivos.Name = "fieldProductosActivos";
            fieldProductosActivos.Size = new Size(165, 39);
            fieldProductosActivos.TabIndex = 4;
            fieldProductosActivos.Text = "0";
            fieldProductosActivos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutDistribucion2
            // 
            layoutDistribucion2.ColumnCount = 3;
            layoutDistribucion2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.294632F));
            layoutDistribucion2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.8526859F));
            layoutDistribucion2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.8526859F));
            layoutDistribucion2.Controls.Add(panelTopProductosValor, 2, 0);
            layoutDistribucion2.Controls.Add(panelValorPorAlmacen, 1, 0);
            layoutDistribucion2.Controls.Add(panelEvolucionMovimientos, 0, 0);
            layoutDistribucion2.Dock = DockStyle.Fill;
            layoutDistribucion2.Location = new Point(50, 260);
            layoutDistribucion2.Margin = new Padding(0);
            layoutDistribucion2.Name = "layoutDistribucion2";
            layoutDistribucion2.RowCount = 1;
            layoutDistribucion2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutDistribucion2.Size = new Size(1286, 328);
            layoutDistribucion2.TabIndex = 16;
            // 
            // panelTopProductosValor
            // 
            panelTopProductosValor.BackColor = Color.Transparent;
            panelTopProductosValor.BorderRadius = 8;
            panelTopProductosValor.Controls.Add(layoutTopProductosValor);
            panelTopProductosValor.CustomizableEdges = customizableEdges15;
            panelTopProductosValor.Dock = DockStyle.Fill;
            panelTopProductosValor.FillColor = Color.White;
            panelTopProductosValor.Location = new Point(950, 10);
            panelTopProductosValor.Margin = new Padding(10);
            panelTopProductosValor.Name = "panelTopProductosValor";
            panelTopProductosValor.ShadowDecoration.BorderRadius = 8;
            panelTopProductosValor.ShadowDecoration.CustomizableEdges = customizableEdges16;
            panelTopProductosValor.ShadowDecoration.Depth = 10;
            panelTopProductosValor.ShadowDecoration.Enabled = true;
            panelTopProductosValor.Size = new Size(326, 308);
            panelTopProductosValor.TabIndex = 2;
            // 
            // layoutTopProductosValor
            // 
            layoutTopProductosValor.BackColor = Color.Transparent;
            layoutTopProductosValor.ColumnCount = 1;
            layoutTopProductosValor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTopProductosValor.Controls.Add(fieldTituloTopProductosValor, 0, 1);
            layoutTopProductosValor.Controls.Add(layoutTablaTopProducosValor, 0, 2);
            layoutTopProductosValor.Dock = DockStyle.Fill;
            layoutTopProductosValor.Location = new Point(0, 0);
            layoutTopProductosValor.Name = "layoutTopProductosValor";
            layoutTopProductosValor.RowCount = 4;
            layoutTopProductosValor.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutTopProductosValor.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutTopProductosValor.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTopProductosValor.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutTopProductosValor.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutTopProductosValor.Size = new Size(326, 308);
            layoutTopProductosValor.TabIndex = 0;
            // 
            // fieldTituloTopProductosValor
            // 
            fieldTituloTopProductosValor.Dock = DockStyle.Fill;
            fieldTituloTopProductosValor.Font = new Font("Segoe UI", 11.25F);
            fieldTituloTopProductosValor.ForeColor = Color.DimGray;
            fieldTituloTopProductosValor.ImeMode = ImeMode.NoControl;
            fieldTituloTopProductosValor.Location = new Point(10, 10);
            fieldTituloTopProductosValor.Margin = new Padding(10, 5, 1, 1);
            fieldTituloTopProductosValor.Name = "fieldTituloTopProductosValor";
            fieldTituloTopProductosValor.Size = new Size(315, 29);
            fieldTituloTopProductosValor.TabIndex = 5;
            fieldTituloTopProductosValor.Text = "Top Productos - Valor";
            fieldTituloTopProductosValor.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutTablaTopProducosValor
            // 
            layoutTablaTopProducosValor.ColumnCount = 1;
            layoutTablaTopProducosValor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTablaTopProducosValor.Controls.Add(layoutEncabezadosTabla, 0, 0);
            layoutTablaTopProducosValor.Controls.Add(panelTuplasTopProductosValor, 0, 2);
            layoutTablaTopProducosValor.Dock = DockStyle.Fill;
            layoutTablaTopProducosValor.Location = new Point(15, 40);
            layoutTablaTopProducosValor.Margin = new Padding(15, 0, 15, 0);
            layoutTablaTopProducosValor.Name = "layoutTablaTopProducosValor";
            layoutTablaTopProducosValor.RowCount = 3;
            layoutTablaTopProducosValor.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutTablaTopProducosValor.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutTablaTopProducosValor.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTablaTopProducosValor.Size = new Size(296, 258);
            layoutTablaTopProducosValor.TabIndex = 24;
            // 
            // layoutEncabezadosTabla
            // 
            layoutEncabezadosTabla.BackColor = Color.White;
            layoutEncabezadosTabla.ColumnCount = 3;
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutEncabezadosTabla.Controls.Add(fieldTituloNumeroProducto, 0, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloNombreProducto, 1, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloValorProducto, 2, 0);
            layoutEncabezadosTabla.Dock = DockStyle.Fill;
            layoutEncabezadosTabla.Location = new Point(0, 0);
            layoutEncabezadosTabla.Margin = new Padding(0, 0, 0, 2);
            layoutEncabezadosTabla.Name = "layoutEncabezadosTabla";
            layoutEncabezadosTabla.RowCount = 1;
            layoutEncabezadosTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutEncabezadosTabla.Size = new Size(296, 28);
            layoutEncabezadosTabla.TabIndex = 20;
            // 
            // fieldTituloNumeroProducto
            // 
            fieldTituloNumeroProducto.Dock = DockStyle.Fill;
            fieldTituloNumeroProducto.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloNumeroProducto.ForeColor = Color.DimGray;
            fieldTituloNumeroProducto.ImeMode = ImeMode.NoControl;
            fieldTituloNumeroProducto.Location = new Point(1, 1);
            fieldTituloNumeroProducto.Margin = new Padding(1);
            fieldTituloNumeroProducto.Name = "fieldTituloNumeroProducto";
            fieldTituloNumeroProducto.Size = new Size(28, 26);
            fieldTituloNumeroProducto.TabIndex = 16;
            fieldTituloNumeroProducto.Text = "#";
            fieldTituloNumeroProducto.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldTituloNombreProducto
            // 
            fieldTituloNombreProducto.Dock = DockStyle.Fill;
            fieldTituloNombreProducto.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloNombreProducto.ForeColor = Color.DimGray;
            fieldTituloNombreProducto.ImeMode = ImeMode.NoControl;
            fieldTituloNombreProducto.Location = new Point(31, 1);
            fieldTituloNombreProducto.Margin = new Padding(1);
            fieldTituloNombreProducto.Name = "fieldTituloNombreProducto";
            fieldTituloNombreProducto.Size = new Size(154, 26);
            fieldTituloNombreProducto.TabIndex = 15;
            fieldTituloNombreProducto.Text = "Producto";
            fieldTituloNombreProducto.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloValorProducto
            // 
            fieldTituloValorProducto.Dock = DockStyle.Fill;
            fieldTituloValorProducto.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloValorProducto.ForeColor = Color.DimGray;
            fieldTituloValorProducto.ImeMode = ImeMode.NoControl;
            fieldTituloValorProducto.Location = new Point(187, 1);
            fieldTituloValorProducto.Margin = new Padding(1);
            fieldTituloValorProducto.Name = "fieldTituloValorProducto";
            fieldTituloValorProducto.Size = new Size(108, 26);
            fieldTituloValorProducto.TabIndex = 15;
            fieldTituloValorProducto.Text = "Valor $";
            fieldTituloValorProducto.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panelValorPorAlmacen
            // 
            panelValorPorAlmacen.BackColor = Color.Transparent;
            panelValorPorAlmacen.BorderRadius = 8;
            panelValorPorAlmacen.Controls.Add(layoutValorPorAlmacen);
            panelValorPorAlmacen.CustomizableEdges = customizableEdges17;
            panelValorPorAlmacen.Dock = DockStyle.Fill;
            panelValorPorAlmacen.FillColor = Color.White;
            panelValorPorAlmacen.Location = new Point(605, 10);
            panelValorPorAlmacen.Margin = new Padding(10);
            panelValorPorAlmacen.Name = "panelValorPorAlmacen";
            panelValorPorAlmacen.ShadowDecoration.BorderRadius = 8;
            panelValorPorAlmacen.ShadowDecoration.CustomizableEdges = customizableEdges18;
            panelValorPorAlmacen.ShadowDecoration.Depth = 10;
            panelValorPorAlmacen.ShadowDecoration.Enabled = true;
            panelValorPorAlmacen.Size = new Size(325, 308);
            panelValorPorAlmacen.TabIndex = 0;
            // 
            // layoutValorPorAlmacen
            // 
            layoutValorPorAlmacen.BackColor = Color.Transparent;
            layoutValorPorAlmacen.ColumnCount = 1;
            layoutValorPorAlmacen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutValorPorAlmacen.Controls.Add(fieldTituloValorPorAlmacen, 0, 1);
            layoutValorPorAlmacen.Controls.Add(fieldValorAlmacen, 0, 2);
            layoutValorPorAlmacen.Dock = DockStyle.Fill;
            layoutValorPorAlmacen.Location = new Point(0, 0);
            layoutValorPorAlmacen.Name = "layoutValorPorAlmacen";
            layoutValorPorAlmacen.RowCount = 4;
            layoutValorPorAlmacen.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutValorPorAlmacen.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutValorPorAlmacen.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutValorPorAlmacen.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutValorPorAlmacen.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutValorPorAlmacen.Size = new Size(325, 308);
            layoutValorPorAlmacen.TabIndex = 0;
            // 
            // fieldTituloValorPorAlmacen
            // 
            fieldTituloValorPorAlmacen.Dock = DockStyle.Fill;
            fieldTituloValorPorAlmacen.Font = new Font("Segoe UI", 11.25F);
            fieldTituloValorPorAlmacen.ForeColor = Color.DimGray;
            fieldTituloValorPorAlmacen.ImeMode = ImeMode.NoControl;
            fieldTituloValorPorAlmacen.Location = new Point(10, 10);
            fieldTituloValorPorAlmacen.Margin = new Padding(10, 5, 1, 1);
            fieldTituloValorPorAlmacen.Name = "fieldTituloValorPorAlmacen";
            fieldTituloValorPorAlmacen.Size = new Size(314, 29);
            fieldTituloValorPorAlmacen.TabIndex = 5;
            fieldTituloValorPorAlmacen.Text = "Valor por almacén";
            fieldTituloValorPorAlmacen.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldValorAlmacen
            // 
            fieldValorAlmacen.Dock = DockStyle.Fill;
            fieldValorAlmacen.Location = new Point(15, 43);
            fieldValorAlmacen.Margin = new Padding(15, 3, 15, 3);
            fieldValorAlmacen.Name = "fieldValorAlmacen";
            fieldValorAlmacen.Size = new Size(295, 252);
            fieldValorAlmacen.TabIndex = 6;
            fieldValorAlmacen.TabStop = false;
            // 
            // panelEvolucionMovimientos
            // 
            panelEvolucionMovimientos.BackColor = Color.Transparent;
            panelEvolucionMovimientos.BorderRadius = 8;
            panelEvolucionMovimientos.Controls.Add(layoutEvolucionMovimientos);
            panelEvolucionMovimientos.CustomizableEdges = customizableEdges19;
            panelEvolucionMovimientos.Dock = DockStyle.Fill;
            panelEvolucionMovimientos.FillColor = Color.White;
            panelEvolucionMovimientos.Location = new Point(10, 10);
            panelEvolucionMovimientos.Margin = new Padding(10);
            panelEvolucionMovimientos.Name = "panelEvolucionMovimientos";
            panelEvolucionMovimientos.ShadowDecoration.BorderRadius = 8;
            panelEvolucionMovimientos.ShadowDecoration.CustomizableEdges = customizableEdges20;
            panelEvolucionMovimientos.ShadowDecoration.Depth = 10;
            panelEvolucionMovimientos.ShadowDecoration.Enabled = true;
            panelEvolucionMovimientos.Size = new Size(575, 308);
            panelEvolucionMovimientos.TabIndex = 1;
            // 
            // layoutEvolucionMovimientos
            // 
            layoutEvolucionMovimientos.BackColor = Color.Transparent;
            layoutEvolucionMovimientos.ColumnCount = 1;
            layoutEvolucionMovimientos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutEvolucionMovimientos.Controls.Add(fieldTituloEvolucionMovimientos, 0, 1);
            layoutEvolucionMovimientos.Controls.Add(fieldEvolucionMovimientos, 0, 2);
            layoutEvolucionMovimientos.Dock = DockStyle.Fill;
            layoutEvolucionMovimientos.Location = new Point(0, 0);
            layoutEvolucionMovimientos.Name = "layoutEvolucionMovimientos";
            layoutEvolucionMovimientos.RowCount = 4;
            layoutEvolucionMovimientos.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutEvolucionMovimientos.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutEvolucionMovimientos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutEvolucionMovimientos.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutEvolucionMovimientos.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutEvolucionMovimientos.Size = new Size(575, 308);
            layoutEvolucionMovimientos.TabIndex = 0;
            // 
            // fieldTituloEvolucionMovimientos
            // 
            fieldTituloEvolucionMovimientos.Dock = DockStyle.Fill;
            fieldTituloEvolucionMovimientos.Font = new Font("Segoe UI", 11.25F);
            fieldTituloEvolucionMovimientos.ForeColor = Color.DimGray;
            fieldTituloEvolucionMovimientos.ImeMode = ImeMode.NoControl;
            fieldTituloEvolucionMovimientos.Location = new Point(10, 10);
            fieldTituloEvolucionMovimientos.Margin = new Padding(10, 5, 1, 1);
            fieldTituloEvolucionMovimientos.Name = "fieldTituloEvolucionMovimientos";
            fieldTituloEvolucionMovimientos.Size = new Size(564, 29);
            fieldTituloEvolucionMovimientos.TabIndex = 5;
            fieldTituloEvolucionMovimientos.Text = "Evolución de movimientos - Últimos 30 días";
            fieldTituloEvolucionMovimientos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldEvolucionMovimientos
            // 
            fieldEvolucionMovimientos.Dock = DockStyle.Fill;
            fieldEvolucionMovimientos.Location = new Point(15, 43);
            fieldEvolucionMovimientos.Margin = new Padding(15, 3, 15, 3);
            fieldEvolucionMovimientos.Name = "fieldEvolucionMovimientos";
            fieldEvolucionMovimientos.Size = new Size(545, 252);
            fieldEvolucionMovimientos.TabIndex = 6;
            fieldEvolucionMovimientos.TabStop = false;
            // 
            // panelTuplasTopProductosValor
            // 
            panelTuplasTopProductosValor.Dock = DockStyle.Fill;
            panelTuplasTopProductosValor.Location = new Point(0, 40);
            panelTuplasTopProductosValor.Margin = new Padding(0);
            panelTuplasTopProductosValor.Name = "panelTuplasTopProductosValor";
            panelTuplasTopProductosValor.Size = new Size(296, 218);
            panelTuplasTopProductosValor.TabIndex = 21;
            // 
            // VistaEstadisticasInventario
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1356, 608);
            Controls.Add(layoutVista);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaEstadisticasInventario";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaEstadisticasGenerales";
            layoutVista.ResumeLayout(false);
            layoutTitulo.ResumeLayout(false);
            ((ISupportInitialize) fieldIcono).EndInit();
            layoutDistribucion1.ResumeLayout(false);
            panelMovimientosHoy.ResumeLayout(false);
            layoutMovimientosHoy.ResumeLayout(false);
            panelAlmacenesActivos.ResumeLayout(false);
            layoutAlmacenesActivos.ResumeLayout(false);
            panelValorTotalInventario.ResumeLayout(false);
            layoutValorTotalInventario.ResumeLayout(false);
            layoutSubValorTotalInventario.ResumeLayout(false);
            panelSinStock.ResumeLayout(false);
            layoutSinStock.ResumeLayout(false);
            panelBajoStockMinimo.ResumeLayout(false);
            layoutBajoStockMinimo.ResumeLayout(false);
            panelProductosActivos.ResumeLayout(false);
            layoutPanelProductosActivos.ResumeLayout(false);
            layoutDistribucion2.ResumeLayout(false);
            panelTopProductosValor.ResumeLayout(false);
            layoutTopProductosValor.ResumeLayout(false);
            layoutTablaTopProducosValor.ResumeLayout(false);
            layoutEncabezadosTabla.ResumeLayout(false);
            panelValorPorAlmacen.ResumeLayout(false);
            layoutValorPorAlmacen.ResumeLayout(false);
            ((ISupportInitialize) fieldValorAlmacen).EndInit();
            panelEvolucionMovimientos.ResumeLayout(false);
            layoutEvolucionMovimientos.ResumeLayout(false);
            ((ISupportInitialize) fieldEvolucionMovimientos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutVista;
        private TableLayoutPanel layoutTitulo;
        private Label fieldTitulo;
        private PictureBox fieldIcono;
        private Label fieldSubtitulo;
        private TableLayoutPanel layoutDistribucion1;
        private Guna2Panel panelProductosActivos;
        private Guna2Panel panelBajoStockMinimo;
        private TableLayoutPanel layoutBajoStockMinimo;
        private Label fieldTexto1;
        private Label fieldTituloBajoStockMinimo;
        private Label fieldBajoStockMinimo;
        private Guna2Panel panelSinStock;
        private TableLayoutPanel layoutSinStock;
        private Label fieldTexto2;
        private Label fieldTituloSinStock;
        private Label fieldSinStock;
        private Guna2Panel panelValorTotalInventario;
        private TableLayoutPanel layoutValorTotalInventario;
        private Label fieldTexto3;
        private Label fieldTituloValorTotalInventario;
        private TableLayoutPanel layoutSubValorTotalInventario;
        private Label fieldValorTotalInventario;
        private Label simboloPeso;
        private Guna2Panel panelAlmacenesActivos;
        private TableLayoutPanel layoutAlmacenesActivos;
        private Label fieldTituloAlmacenesActivos;
        private Label fieldAlmacenesActivos;
        private Guna2Panel panelMovimientosHoy;
        private TableLayoutPanel layoutMovimientosHoy;
        private Label fieldTituloMovimientosHoy;
        private Label fieldMovimientosHoy;
        private TableLayoutPanel layoutDistribucion2;
        private Guna2Panel panelEvolucionMovimientos;
        private TableLayoutPanel layoutEvolucionMovimientos;
        private Label fieldTituloEvolucionMovimientos;
        private Guna2Panel panelValorPorAlmacen;
        private TableLayoutPanel layoutValorPorAlmacen;
        private Label fieldTituloValorPorAlmacen;
        private Guna2Panel panelTopProductosValor;
        private TableLayoutPanel layoutTopProductosValor;
        private Label fieldTituloTopProductosValor;
        private TableLayoutPanel layoutPanelProductosActivos;
        private Label fieldProductosNuevos;
        private Label fieldTituloProductosActivos;
        private Label fieldProductosActivos;
        private Guna2Button btnActualizar;
        private PictureBox fieldEvolucionMovimientos;
        private PictureBox fieldValorAlmacen;
        private TableLayoutPanel layoutTablaTopProducosValor;
        private TableLayoutPanel layoutEncabezadosTabla;
        private Label fieldTituloNumeroProducto;
        private Label fieldTituloNombreProducto;
        private Label fieldTituloValorProducto;
        private Panel panelTuplasTopProductosValor;
    }
}