using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    partial class VistaEstadisticasGenerales {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaEstadisticasGenerales));
            formatoBase = new Guna2BorderlessForm(components);
            layoutVista = new TableLayoutPanel();
            layoutTitulo = new TableLayoutPanel();
            fieldTitulo = new Label();
            fieldIcono = new PictureBox();
            fieldSubtitulo = new Label();
            layoutDistribucion1 = new TableLayoutPanel();
            panelMovimientosHoy = new Panel();
            layoutMovimientosHoy = new TableLayoutPanel();
            fieldTituloMovimientosHoy = new Label();
            linea6 = new PictureBox();
            fieldMovimientosHoy = new Label();
            panelAlmacenesActivos = new Panel();
            layoutAlmacenesActivos = new TableLayoutPanel();
            fieldTituloAlmacenesActivos = new Label();
            linea5 = new PictureBox();
            fieldAlmacenesActivos = new Label();
            panelValorTotalInventario = new Panel();
            layoutValorTotalInventario = new TableLayoutPanel();
            fieldTexto3 = new Label();
            fieldTituloValorTotalInventario = new Label();
            linea4 = new PictureBox();
            layoutSubValorTotalInventario = new TableLayoutPanel();
            fieldValorTotalInventario = new Label();
            simboloPeso = new Label();
            panelSinStock = new Panel();
            layoutSinStock = new TableLayoutPanel();
            fieldTexto2 = new Label();
            fieldTituloSinStock = new Label();
            linea3 = new PictureBox();
            label3 = new Label();
            panelBajoStockMinimo = new Panel();
            layoutBajoStockMinimo = new TableLayoutPanel();
            fieldTexto1 = new Label();
            fieldTituloBajoStockMinimo = new Label();
            linea2 = new PictureBox();
            fieldBajoStockMinimo = new Label();
            panelProductosActivos = new Panel();
            layoutPanelProductosActivos = new TableLayoutPanel();
            fieldProductosNuevos = new Label();
            fieldTituloProductosActivos = new Label();
            linea1 = new PictureBox();
            fieldProductosActivos = new Label();
            layoutDistribucion2 = new TableLayoutPanel();
            panelTopProductosValor = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            fieldTituloTopProductosValor = new Label();
            panelValorPorAlmacen = new Panel();
            layoutValorPorAlmacen = new TableLayoutPanel();
            fieldTituloValorPorAlmacen = new Label();
            panelEvolucionMovimientos = new Panel();
            layoutEvolucionMovimientos = new TableLayoutPanel();
            fieldTituloEvolucionMovimientos = new Label();
            layoutVista.SuspendLayout();
            layoutTitulo.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            layoutDistribucion1.SuspendLayout();
            panelMovimientosHoy.SuspendLayout();
            layoutMovimientosHoy.SuspendLayout();
            ((ISupportInitialize) linea6).BeginInit();
            panelAlmacenesActivos.SuspendLayout();
            layoutAlmacenesActivos.SuspendLayout();
            ((ISupportInitialize) linea5).BeginInit();
            panelValorTotalInventario.SuspendLayout();
            layoutValorTotalInventario.SuspendLayout();
            ((ISupportInitialize) linea4).BeginInit();
            layoutSubValorTotalInventario.SuspendLayout();
            panelSinStock.SuspendLayout();
            layoutSinStock.SuspendLayout();
            ((ISupportInitialize) linea3).BeginInit();
            panelBajoStockMinimo.SuspendLayout();
            layoutBajoStockMinimo.SuspendLayout();
            ((ISupportInitialize) linea2).BeginInit();
            panelProductosActivos.SuspendLayout();
            layoutPanelProductosActivos.SuspendLayout();
            ((ISupportInitialize) linea1).BeginInit();
            layoutDistribucion2.SuspendLayout();
            panelTopProductosValor.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panelValorPorAlmacen.SuspendLayout();
            layoutValorPorAlmacen.SuspendLayout();
            panelEvolucionMovimientos.SuspendLayout();
            layoutEvolucionMovimientos.SuspendLayout();
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
            layoutVista.TabIndex = 4;
            // 
            // layoutTitulo
            // 
            layoutTitulo.ColumnCount = 2;
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
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
            // fieldTitulo
            // 
            fieldTitulo.Dock = DockStyle.Fill;
            fieldTitulo.Font = new Font("Segoe UI", 20.25F);
            fieldTitulo.ForeColor = Color.Black;
            fieldTitulo.ImeMode = ImeMode.NoControl;
            fieldTitulo.Location = new Point(3, 0);
            fieldTitulo.Name = "fieldTitulo";
            fieldTitulo.Size = new Size(1230, 45);
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
            panelMovimientosHoy.BackColor = Color.White;
            panelMovimientosHoy.Controls.Add(layoutMovimientosHoy);
            panelMovimientosHoy.Dock = DockStyle.Fill;
            panelMovimientosHoy.Location = new Point(1090, 5);
            panelMovimientosHoy.Margin = new Padding(5);
            panelMovimientosHoy.Name = "panelMovimientosHoy";
            panelMovimientosHoy.Size = new Size(191, 140);
            panelMovimientosHoy.TabIndex = 5;
            // 
            // layoutMovimientosHoy
            // 
            layoutMovimientosHoy.ColumnCount = 1;
            layoutMovimientosHoy.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutMovimientosHoy.Controls.Add(fieldTituloMovimientosHoy, 0, 1);
            layoutMovimientosHoy.Controls.Add(linea6, 0, 0);
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
            layoutMovimientosHoy.Size = new Size(191, 140);
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
            fieldTituloMovimientosHoy.Size = new Size(180, 29);
            fieldTituloMovimientosHoy.TabIndex = 5;
            fieldTituloMovimientosHoy.Text = "Movimientos hoy";
            fieldTituloMovimientosHoy.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // linea6
            // 
            linea6.BackColor = Color.PeachPuff;
            linea6.Dock = DockStyle.Fill;
            linea6.Location = new Point(0, 0);
            linea6.Margin = new Padding(0);
            linea6.Name = "linea6";
            linea6.Size = new Size(191, 5);
            linea6.TabIndex = 0;
            linea6.TabStop = false;
            // 
            // fieldMovimientosHoy
            // 
            fieldMovimientosHoy.Dock = DockStyle.Fill;
            fieldMovimientosHoy.Font = new Font("Impact", 27.75F, FontStyle.Bold);
            fieldMovimientosHoy.ForeColor = Color.FromArgb(  64,   64,   64);
            fieldMovimientosHoy.ImeMode = ImeMode.NoControl;
            fieldMovimientosHoy.Location = new Point(3, 43);
            fieldMovimientosHoy.Margin = new Padding(3);
            fieldMovimientosHoy.Name = "fieldMovimientosHoy";
            fieldMovimientosHoy.Size = new Size(185, 49);
            fieldMovimientosHoy.TabIndex = 4;
            fieldMovimientosHoy.Text = "0";
            fieldMovimientosHoy.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelAlmacenesActivos
            // 
            panelAlmacenesActivos.BackColor = Color.White;
            panelAlmacenesActivos.Controls.Add(layoutAlmacenesActivos);
            panelAlmacenesActivos.Dock = DockStyle.Fill;
            panelAlmacenesActivos.Location = new Point(892, 5);
            panelAlmacenesActivos.Margin = new Padding(5);
            panelAlmacenesActivos.Name = "panelAlmacenesActivos";
            panelAlmacenesActivos.Size = new Size(188, 140);
            panelAlmacenesActivos.TabIndex = 4;
            // 
            // layoutAlmacenesActivos
            // 
            layoutAlmacenesActivos.ColumnCount = 1;
            layoutAlmacenesActivos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutAlmacenesActivos.Controls.Add(fieldTituloAlmacenesActivos, 0, 1);
            layoutAlmacenesActivos.Controls.Add(linea5, 0, 0);
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
            layoutAlmacenesActivos.Size = new Size(188, 140);
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
            fieldTituloAlmacenesActivos.Size = new Size(177, 29);
            fieldTituloAlmacenesActivos.TabIndex = 5;
            fieldTituloAlmacenesActivos.Text = "Almacenes activos";
            fieldTituloAlmacenesActivos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // linea5
            // 
            linea5.BackColor = Color.LightBlue;
            linea5.Dock = DockStyle.Fill;
            linea5.Location = new Point(0, 0);
            linea5.Margin = new Padding(0);
            linea5.Name = "linea5";
            linea5.Size = new Size(188, 5);
            linea5.TabIndex = 0;
            linea5.TabStop = false;
            // 
            // fieldAlmacenesActivos
            // 
            fieldAlmacenesActivos.Dock = DockStyle.Fill;
            fieldAlmacenesActivos.Font = new Font("Impact", 27.75F, FontStyle.Bold);
            fieldAlmacenesActivos.ForeColor = Color.FromArgb(  64,   64,   64);
            fieldAlmacenesActivos.ImeMode = ImeMode.NoControl;
            fieldAlmacenesActivos.Location = new Point(3, 43);
            fieldAlmacenesActivos.Margin = new Padding(3);
            fieldAlmacenesActivos.Name = "fieldAlmacenesActivos";
            fieldAlmacenesActivos.Size = new Size(182, 49);
            fieldAlmacenesActivos.TabIndex = 4;
            fieldAlmacenesActivos.Text = "0";
            fieldAlmacenesActivos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelValorTotalInventario
            // 
            panelValorTotalInventario.BackColor = Color.White;
            panelValorTotalInventario.Controls.Add(layoutValorTotalInventario);
            panelValorTotalInventario.Dock = DockStyle.Fill;
            panelValorTotalInventario.Location = new Point(599, 5);
            panelValorTotalInventario.Margin = new Padding(5);
            panelValorTotalInventario.Name = "panelValorTotalInventario";
            panelValorTotalInventario.Size = new Size(283, 140);
            panelValorTotalInventario.TabIndex = 3;
            // 
            // layoutValorTotalInventario
            // 
            layoutValorTotalInventario.ColumnCount = 1;
            layoutValorTotalInventario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutValorTotalInventario.Controls.Add(fieldTexto3, 0, 3);
            layoutValorTotalInventario.Controls.Add(fieldTituloValorTotalInventario, 0, 1);
            layoutValorTotalInventario.Controls.Add(linea4, 0, 0);
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
            layoutValorTotalInventario.Size = new Size(283, 140);
            layoutValorTotalInventario.TabIndex = 0;
            // 
            // fieldTexto3
            // 
            fieldTexto3.Dock = DockStyle.Fill;
            fieldTexto3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point,  0);
            fieldTexto3.ForeColor = Color.DimGray;
            fieldTexto3.ImeMode = ImeMode.NoControl;
            fieldTexto3.Location = new Point(10, 100);
            fieldTexto3.Margin = new Padding(10, 5, 1, 1);
            fieldTexto3.Name = "fieldTexto3";
            fieldTexto3.Size = new Size(272, 19);
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
            fieldTituloValorTotalInventario.Size = new Size(272, 29);
            fieldTituloValorTotalInventario.TabIndex = 5;
            fieldTituloValorTotalInventario.Text = "Valor total del inventario";
            fieldTituloValorTotalInventario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // linea4
            // 
            linea4.BackColor = Color.PeachPuff;
            linea4.Dock = DockStyle.Fill;
            linea4.Location = new Point(0, 0);
            linea4.Margin = new Padding(0);
            linea4.Name = "linea4";
            linea4.Size = new Size(283, 5);
            linea4.TabIndex = 0;
            linea4.TabStop = false;
            // 
            // layoutSubValorTotalInventario
            // 
            layoutSubValorTotalInventario.ColumnCount = 2;
            layoutSubValorTotalInventario.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 22F));
            layoutSubValorTotalInventario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutSubValorTotalInventario.Controls.Add(fieldValorTotalInventario, 1, 0);
            layoutSubValorTotalInventario.Controls.Add(simboloPeso, 0, 0);
            layoutSubValorTotalInventario.Dock = DockStyle.Fill;
            layoutSubValorTotalInventario.Location = new Point(6, 40);
            layoutSubValorTotalInventario.Margin = new Padding(6, 0, 0, 0);
            layoutSubValorTotalInventario.Name = "layoutSubValorTotalInventario";
            layoutSubValorTotalInventario.RowCount = 1;
            layoutSubValorTotalInventario.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutSubValorTotalInventario.Size = new Size(277, 55);
            layoutSubValorTotalInventario.TabIndex = 7;
            // 
            // fieldValorTotalInventario
            // 
            fieldValorTotalInventario.Dock = DockStyle.Fill;
            fieldValorTotalInventario.Font = new Font("Impact", 27.75F, FontStyle.Regular, GraphicsUnit.Point,  0);
            fieldValorTotalInventario.ForeColor = Color.FromArgb(  64,   64,   64);
            fieldValorTotalInventario.ImeMode = ImeMode.NoControl;
            fieldValorTotalInventario.Location = new Point(25, 3);
            fieldValorTotalInventario.Margin = new Padding(3);
            fieldValorTotalInventario.Name = "fieldValorTotalInventario";
            fieldValorTotalInventario.Size = new Size(249, 49);
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
            simboloPeso.Location = new Point(1, 1);
            simboloPeso.Margin = new Padding(1, 1, 1, 5);
            simboloPeso.Name = "simboloPeso";
            simboloPeso.Size = new Size(20, 49);
            simboloPeso.TabIndex = 6;
            simboloPeso.Text = "$";
            simboloPeso.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panelSinStock
            // 
            panelSinStock.BackColor = Color.White;
            panelSinStock.Controls.Add(layoutSinStock);
            panelSinStock.Dock = DockStyle.Fill;
            panelSinStock.Location = new Point(401, 5);
            panelSinStock.Margin = new Padding(5);
            panelSinStock.Name = "panelSinStock";
            panelSinStock.Size = new Size(188, 140);
            panelSinStock.TabIndex = 2;
            // 
            // layoutSinStock
            // 
            layoutSinStock.ColumnCount = 1;
            layoutSinStock.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutSinStock.Controls.Add(fieldTexto2, 0, 3);
            layoutSinStock.Controls.Add(fieldTituloSinStock, 0, 1);
            layoutSinStock.Controls.Add(linea3, 0, 0);
            layoutSinStock.Controls.Add(label3, 0, 2);
            layoutSinStock.Dock = DockStyle.Fill;
            layoutSinStock.Location = new Point(0, 0);
            layoutSinStock.Name = "layoutSinStock";
            layoutSinStock.RowCount = 5;
            layoutSinStock.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutSinStock.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutSinStock.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutSinStock.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutSinStock.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutSinStock.Size = new Size(188, 140);
            layoutSinStock.TabIndex = 0;
            // 
            // fieldTexto2
            // 
            fieldTexto2.Dock = DockStyle.Fill;
            fieldTexto2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point,  0);
            fieldTexto2.ForeColor = Color.DimGray;
            fieldTexto2.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTexto2.ImeMode = ImeMode.NoControl;
            fieldTexto2.Location = new Point(10, 100);
            fieldTexto2.Margin = new Padding(10, 5, 1, 1);
            fieldTexto2.Name = "fieldTexto2";
            fieldTexto2.Size = new Size(177, 19);
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
            fieldTituloSinStock.Size = new Size(177, 29);
            fieldTituloSinStock.TabIndex = 5;
            fieldTituloSinStock.Text = "Sin stock";
            fieldTituloSinStock.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // linea3
            // 
            linea3.BackColor = Color.Firebrick;
            linea3.Dock = DockStyle.Fill;
            linea3.Location = new Point(0, 0);
            linea3.Margin = new Padding(0);
            linea3.Name = "linea3";
            linea3.Size = new Size(188, 5);
            linea3.TabIndex = 0;
            linea3.TabStop = false;
            // 
            // label3
            // 
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Impact", 27.75F, FontStyle.Bold);
            label3.ForeColor = Color.Firebrick;
            label3.ImeMode = ImeMode.NoControl;
            label3.Location = new Point(3, 43);
            label3.Margin = new Padding(3);
            label3.Name = "label3";
            label3.Size = new Size(182, 49);
            label3.TabIndex = 4;
            label3.Text = "0";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelBajoStockMinimo
            // 
            panelBajoStockMinimo.BackColor = Color.White;
            panelBajoStockMinimo.Controls.Add(layoutBajoStockMinimo);
            panelBajoStockMinimo.Dock = DockStyle.Fill;
            panelBajoStockMinimo.Location = new Point(203, 5);
            panelBajoStockMinimo.Margin = new Padding(5);
            panelBajoStockMinimo.Name = "panelBajoStockMinimo";
            panelBajoStockMinimo.Size = new Size(188, 140);
            panelBajoStockMinimo.TabIndex = 1;
            // 
            // layoutBajoStockMinimo
            // 
            layoutBajoStockMinimo.ColumnCount = 1;
            layoutBajoStockMinimo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBajoStockMinimo.Controls.Add(fieldTexto1, 0, 3);
            layoutBajoStockMinimo.Controls.Add(fieldTituloBajoStockMinimo, 0, 1);
            layoutBajoStockMinimo.Controls.Add(linea2, 0, 0);
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
            layoutBajoStockMinimo.Size = new Size(188, 140);
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
            fieldTexto1.Location = new Point(10, 100);
            fieldTexto1.Margin = new Padding(10, 5, 1, 1);
            fieldTexto1.Name = "fieldTexto1";
            fieldTexto1.Size = new Size(177, 19);
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
            fieldTituloBajoStockMinimo.Size = new Size(177, 29);
            fieldTituloBajoStockMinimo.TabIndex = 5;
            fieldTituloBajoStockMinimo.Text = "Bajo stock mínimo";
            fieldTituloBajoStockMinimo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // linea2
            // 
            linea2.BackColor = Color.Peru;
            linea2.Dock = DockStyle.Fill;
            linea2.Location = new Point(0, 0);
            linea2.Margin = new Padding(0);
            linea2.Name = "linea2";
            linea2.Size = new Size(188, 5);
            linea2.TabIndex = 0;
            linea2.TabStop = false;
            // 
            // fieldBajoStockMinimo
            // 
            fieldBajoStockMinimo.Dock = DockStyle.Fill;
            fieldBajoStockMinimo.Font = new Font("Impact", 27.75F, FontStyle.Bold);
            fieldBajoStockMinimo.ForeColor = Color.Peru;
            fieldBajoStockMinimo.ImeMode = ImeMode.NoControl;
            fieldBajoStockMinimo.Location = new Point(3, 43);
            fieldBajoStockMinimo.Margin = new Padding(3);
            fieldBajoStockMinimo.Name = "fieldBajoStockMinimo";
            fieldBajoStockMinimo.Size = new Size(182, 49);
            fieldBajoStockMinimo.TabIndex = 4;
            fieldBajoStockMinimo.Text = "0";
            fieldBajoStockMinimo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelProductosActivos
            // 
            panelProductosActivos.BackColor = Color.White;
            panelProductosActivos.Controls.Add(layoutPanelProductosActivos);
            panelProductosActivos.Dock = DockStyle.Fill;
            panelProductosActivos.Location = new Point(5, 5);
            panelProductosActivos.Margin = new Padding(5);
            panelProductosActivos.Name = "panelProductosActivos";
            panelProductosActivos.Size = new Size(188, 140);
            panelProductosActivos.TabIndex = 0;
            // 
            // layoutPanelProductosActivos
            // 
            layoutPanelProductosActivos.ColumnCount = 1;
            layoutPanelProductosActivos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPanelProductosActivos.Controls.Add(fieldProductosNuevos, 0, 3);
            layoutPanelProductosActivos.Controls.Add(fieldTituloProductosActivos, 0, 1);
            layoutPanelProductosActivos.Controls.Add(linea1, 0, 0);
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
            layoutPanelProductosActivos.Size = new Size(188, 140);
            layoutPanelProductosActivos.TabIndex = 0;
            // 
            // fieldProductosNuevos
            // 
            fieldProductosNuevos.Dock = DockStyle.Fill;
            fieldProductosNuevos.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point,  0);
            fieldProductosNuevos.ForeColor = Color.DimGray;
            fieldProductosNuevos.ImeMode = ImeMode.NoControl;
            fieldProductosNuevos.Location = new Point(10, 100);
            fieldProductosNuevos.Margin = new Padding(10, 5, 1, 1);
            fieldProductosNuevos.Name = "fieldProductosNuevos";
            fieldProductosNuevos.Size = new Size(177, 19);
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
            fieldTituloProductosActivos.Size = new Size(177, 29);
            fieldTituloProductosActivos.TabIndex = 5;
            fieldTituloProductosActivos.Text = "Productos activos";
            fieldTituloProductosActivos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // linea1
            // 
            linea1.BackColor = Color.PeachPuff;
            linea1.Dock = DockStyle.Fill;
            linea1.Location = new Point(0, 0);
            linea1.Margin = new Padding(0);
            linea1.Name = "linea1";
            linea1.Size = new Size(188, 5);
            linea1.TabIndex = 0;
            linea1.TabStop = false;
            // 
            // fieldProductosActivos
            // 
            fieldProductosActivos.Dock = DockStyle.Fill;
            fieldProductosActivos.Font = new Font("Impact", 27.75F, FontStyle.Bold);
            fieldProductosActivos.ForeColor = Color.FromArgb(  64,   64,   64);
            fieldProductosActivos.ImeMode = ImeMode.NoControl;
            fieldProductosActivos.Location = new Point(3, 43);
            fieldProductosActivos.Margin = new Padding(3);
            fieldProductosActivos.Name = "fieldProductosActivos";
            fieldProductosActivos.Size = new Size(182, 49);
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
            panelTopProductosValor.BackColor = Color.White;
            panelTopProductosValor.Controls.Add(tableLayoutPanel1);
            panelTopProductosValor.Dock = DockStyle.Fill;
            panelTopProductosValor.Location = new Point(945, 5);
            panelTopProductosValor.Margin = new Padding(5);
            panelTopProductosValor.Name = "panelTopProductosValor";
            panelTopProductosValor.Size = new Size(336, 318);
            panelTopProductosValor.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(fieldTituloTopProductosValor, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(336, 318);
            tableLayoutPanel1.TabIndex = 0;
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
            fieldTituloTopProductosValor.Size = new Size(325, 29);
            fieldTituloTopProductosValor.TabIndex = 5;
            fieldTituloTopProductosValor.Text = "Top Productos - Valor";
            fieldTituloTopProductosValor.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelValorPorAlmacen
            // 
            panelValorPorAlmacen.BackColor = Color.White;
            panelValorPorAlmacen.Controls.Add(layoutValorPorAlmacen);
            panelValorPorAlmacen.Dock = DockStyle.Fill;
            panelValorPorAlmacen.Location = new Point(600, 5);
            panelValorPorAlmacen.Margin = new Padding(5);
            panelValorPorAlmacen.Name = "panelValorPorAlmacen";
            panelValorPorAlmacen.Size = new Size(335, 318);
            panelValorPorAlmacen.TabIndex = 2;
            // 
            // layoutValorPorAlmacen
            // 
            layoutValorPorAlmacen.ColumnCount = 1;
            layoutValorPorAlmacen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutValorPorAlmacen.Controls.Add(fieldTituloValorPorAlmacen, 0, 1);
            layoutValorPorAlmacen.Dock = DockStyle.Fill;
            layoutValorPorAlmacen.Location = new Point(0, 0);
            layoutValorPorAlmacen.Name = "layoutValorPorAlmacen";
            layoutValorPorAlmacen.RowCount = 5;
            layoutValorPorAlmacen.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutValorPorAlmacen.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutValorPorAlmacen.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutValorPorAlmacen.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutValorPorAlmacen.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutValorPorAlmacen.Size = new Size(335, 318);
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
            fieldTituloValorPorAlmacen.Size = new Size(324, 29);
            fieldTituloValorPorAlmacen.TabIndex = 5;
            fieldTituloValorPorAlmacen.Text = "Valor por almacén";
            fieldTituloValorPorAlmacen.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelEvolucionMovimientos
            // 
            panelEvolucionMovimientos.BackColor = Color.White;
            panelEvolucionMovimientos.Controls.Add(layoutEvolucionMovimientos);
            panelEvolucionMovimientos.Dock = DockStyle.Fill;
            panelEvolucionMovimientos.Location = new Point(5, 5);
            panelEvolucionMovimientos.Margin = new Padding(5);
            panelEvolucionMovimientos.Name = "panelEvolucionMovimientos";
            panelEvolucionMovimientos.Size = new Size(585, 318);
            panelEvolucionMovimientos.TabIndex = 1;
            // 
            // layoutEvolucionMovimientos
            // 
            layoutEvolucionMovimientos.ColumnCount = 1;
            layoutEvolucionMovimientos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutEvolucionMovimientos.Controls.Add(fieldTituloEvolucionMovimientos, 0, 1);
            layoutEvolucionMovimientos.Dock = DockStyle.Fill;
            layoutEvolucionMovimientos.Location = new Point(0, 0);
            layoutEvolucionMovimientos.Name = "layoutEvolucionMovimientos";
            layoutEvolucionMovimientos.RowCount = 5;
            layoutEvolucionMovimientos.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutEvolucionMovimientos.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutEvolucionMovimientos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutEvolucionMovimientos.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutEvolucionMovimientos.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutEvolucionMovimientos.Size = new Size(585, 318);
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
            fieldTituloEvolucionMovimientos.Size = new Size(574, 29);
            fieldTituloEvolucionMovimientos.TabIndex = 5;
            fieldTituloEvolucionMovimientos.Text = "Evolución de movimientos - Últimos 30 días";
            fieldTituloEvolucionMovimientos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VistaEstadisticasGenerales
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1356, 608);
            Controls.Add(layoutVista);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaEstadisticasGenerales";
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
            ((ISupportInitialize) linea6).EndInit();
            panelAlmacenesActivos.ResumeLayout(false);
            layoutAlmacenesActivos.ResumeLayout(false);
            ((ISupportInitialize) linea5).EndInit();
            panelValorTotalInventario.ResumeLayout(false);
            layoutValorTotalInventario.ResumeLayout(false);
            ((ISupportInitialize) linea4).EndInit();
            layoutSubValorTotalInventario.ResumeLayout(false);
            panelSinStock.ResumeLayout(false);
            layoutSinStock.ResumeLayout(false);
            ((ISupportInitialize) linea3).EndInit();
            panelBajoStockMinimo.ResumeLayout(false);
            layoutBajoStockMinimo.ResumeLayout(false);
            ((ISupportInitialize) linea2).EndInit();
            panelProductosActivos.ResumeLayout(false);
            layoutPanelProductosActivos.ResumeLayout(false);
            ((ISupportInitialize) linea1).EndInit();
            layoutDistribucion2.ResumeLayout(false);
            panelTopProductosValor.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panelValorPorAlmacen.ResumeLayout(false);
            layoutValorPorAlmacen.ResumeLayout(false);
            panelEvolucionMovimientos.ResumeLayout(false);
            layoutEvolucionMovimientos.ResumeLayout(false);
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
        private Panel panelProductosActivos;
        private TableLayoutPanel layoutPanelProductosActivos;
        private PictureBox linea1;
        private Label fieldProductosActivos;
        private Label fieldTituloProductosActivos;
        private Label fieldProductosNuevos;
        private Panel panelBajoStockMinimo;
        private TableLayoutPanel layoutBajoStockMinimo;
        private Label fieldTexto1;
        private Label fieldTituloBajoStockMinimo;
        private PictureBox linea2;
        private Label fieldBajoStockMinimo;
        private Panel panelSinStock;
        private TableLayoutPanel layoutSinStock;
        private Label fieldTexto2;
        private Label fieldTituloSinStock;
        private PictureBox linea3;
        private Label label3;
        private Panel panelValorTotalInventario;
        private TableLayoutPanel layoutValorTotalInventario;
        private Label fieldTexto3;
        private Label fieldTituloValorTotalInventario;
        private PictureBox linea4;
        private TableLayoutPanel layoutSubValorTotalInventario;
        private Label fieldValorTotalInventario;
        private Label simboloPeso;
        private Panel panelAlmacenesActivos;
        private TableLayoutPanel layoutAlmacenesActivos;
        private Label fieldTituloAlmacenesActivos;
        private PictureBox linea5;
        private Label fieldAlmacenesActivos;
        private Panel panelMovimientosHoy;
        private TableLayoutPanel layoutMovimientosHoy;
        private Label fieldTituloMovimientosHoy;
        private PictureBox linea6;
        private Label fieldMovimientosHoy;
        private TableLayoutPanel layoutDistribucion2;
        private Panel panelEvolucionMovimientos;
        private TableLayoutPanel layoutEvolucionMovimientos;
        private Label fieldTituloEvolucionMovimientos;
        private Panel panelValorPorAlmacen;
        private TableLayoutPanel layoutValorPorAlmacen;
        private Label fieldTituloValorPorAlmacen;
        private Panel panelTopProductosValor;
        private TableLayoutPanel tableLayoutPanel1;
        private Label fieldTituloTopProductosValor;
    }
}