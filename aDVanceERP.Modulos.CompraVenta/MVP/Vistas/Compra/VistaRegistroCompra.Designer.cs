using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Compra {
    partial class VistaRegistroCompra {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaRegistroCompra));
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
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutBotones = new TableLayoutPanel();
            btnSalir = new Guna2Button();
            btnRegistrar = new Guna2Button();
            layoutVista = new TableLayoutPanel();
            fieldTituloGestionProductos = new Label();
            fieldIcono = new PictureBox();
            fieldSubtitulo = new Label();
            layoutTitulo = new TableLayoutPanel();
            btnCerrar = new Guna2Button();
            fieldTitulo = new Label();
            layoutMontoCompra = new TableLayoutPanel();
            symbolPeso = new Label();
            fieldTotalCompra = new Label();
            fieldTituloTotalCompra = new Label();
            separador1 = new Guna2Separator();
            layoutTituloProveedorAlmacen = new TableLayoutPanel();
            fieldTituloNombreProveedor = new Label();
            fieldTituloNombreAlmacen = new Label();
            layoutProveedorAlmacen = new TableLayoutPanel();
            fieldNombreProveedor = new Guna2ComboBox();
            fieldNombreAlmacen = new Guna2ComboBox();
            guna2Separator1 = new Guna2Separator();
            layoutEncabezadosTabla = new TableLayoutPanel();
            fieldTituloCantidad = new Label();
            fieldTituloPrecio = new Label();
            fieldTituloProducto = new Label();
            contenedorVistas = new Panel();
            layoutGestionProductos = new TableLayoutPanel();
            btnAdicionarProducto = new Guna2Button();
            fieldCantidad = new Guna2TextBox();
            fieldNombreProducto = new Guna2TextBox();
            layoutBase.SuspendLayout();
            layoutBotones.SuspendLayout();
            layoutVista.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            layoutTitulo.SuspendLayout();
            layoutMontoCompra.SuspendLayout();
            layoutTituloProveedorAlmacen.SuspendLayout();
            layoutProveedorAlmacen.SuspendLayout();
            layoutEncabezadosTabla.SuspendLayout();
            layoutGestionProductos.SuspendLayout();
            SuspendLayout();
            // 
            // formatoBase
            // 
            formatoBase.AnimateWindow = true;
            formatoBase.AnimationType = Guna2BorderlessForm.AnimateWindowType.AW_HOR_NEGATIVE;
            formatoBase.ContainerControl = this;
            formatoBase.DockIndicatorTransparencyValue = 0.6D;
            formatoBase.DragForm = false;
            formatoBase.HasFormShadow = false;
            formatoBase.TransparentWhileDrag = true;
            // 
            // layoutBase
            // 
            layoutBase.BackColor = Color.Gainsboro;
            layoutBase.ColumnCount = 2;
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutBase.Controls.Add(layoutBotones, 1, 1);
            layoutBase.Controls.Add(layoutVista, 1, 0);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 2;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            layoutBase.Size = new Size(500, 685);
            layoutBase.TabIndex = 0;
            // 
            // layoutBotones
            // 
            layoutBotones.BackColor = Color.White;
            layoutBotones.ColumnCount = 4;
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 22F));
            layoutBotones.Controls.Add(btnSalir, 2, 0);
            layoutBotones.Controls.Add(btnRegistrar, 1, 0);
            layoutBotones.Dock = DockStyle.Fill;
            layoutBotones.Location = new Point(13, 620);
            layoutBotones.Margin = new Padding(3, 0, 0, 0);
            layoutBotones.Name = "layoutBotones";
            layoutBotones.RowCount = 2;
            layoutBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBotones.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutBotones.Size = new Size(487, 65);
            layoutBotones.TabIndex = 0;
            // 
            // btnSalir
            // 
            btnSalir.Animated = true;
            btnSalir.BorderColor = Color.Gainsboro;
            btnSalir.BorderRadius = 18;
            btnSalir.BorderThickness = 1;
            btnSalir.CustomizableEdges = customizableEdges1;
            btnSalir.Dock = DockStyle.Fill;
            btnSalir.FillColor = Color.White;
            btnSalir.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSalir.ForeColor = Color.Gainsboro;
            btnSalir.HoverState.BorderColor = Color.PeachPuff;
            btnSalir.HoverState.FillColor = Color.PeachPuff;
            btnSalir.HoverState.ForeColor = Color.Black;
            btnSalir.Location = new Point(302, 3);
            btnSalir.Name = "btnSalir";
            btnSalir.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnSalir.Size = new Size(160, 39);
            btnSalir.TabIndex = 1;
            btnSalir.Text = "Salir";
            // 
            // btnRegistrar
            // 
            btnRegistrar.Animated = true;
            btnRegistrar.BorderRadius = 18;
            btnRegistrar.CustomizableEdges = customizableEdges3;
            btnRegistrar.Dock = DockStyle.Fill;
            btnRegistrar.FillColor = Color.PeachPuff;
            btnRegistrar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.Black;
            btnRegistrar.Location = new Point(53, 3);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnRegistrar.Size = new Size(243, 39);
            btnRegistrar.TabIndex = 0;
            btnRegistrar.Text = "Registrar compra";
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.White;
            layoutVista.ColumnCount = 4;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.Controls.Add(fieldTituloGestionProductos, 2, 7);
            layoutVista.Controls.Add(fieldIcono, 1, 1);
            layoutVista.Controls.Add(fieldSubtitulo, 2, 2);
            layoutVista.Controls.Add(layoutTitulo, 2, 1);
            layoutVista.Controls.Add(layoutMontoCompra, 2, 13);
            layoutVista.Controls.Add(separador1, 2, 6);
            layoutVista.Controls.Add(layoutTituloProveedorAlmacen, 2, 4);
            layoutVista.Controls.Add(layoutProveedorAlmacen, 2, 5);
            layoutVista.Controls.Add(guna2Separator1, 2, 12);
            layoutVista.Controls.Add(layoutEncabezadosTabla, 2, 10);
            layoutVista.Controls.Add(contenedorVistas, 2, 11);
            layoutVista.Controls.Add(layoutGestionProductos, 2, 8);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(13, 0);
            layoutVista.Margin = new Padding(3, 0, 0, 0);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 14;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.Size = new Size(487, 620);
            layoutVista.TabIndex = 3;
            // 
            // fieldTituloGestionProductos
            // 
            fieldTituloGestionProductos.Dock = DockStyle.Fill;
            fieldTituloGestionProductos.Font = new Font("Segoe UI", 11.25F);
            fieldTituloGestionProductos.ForeColor = Color.DimGray;
            fieldTituloGestionProductos.Image = (Image) resources.GetObject("fieldTituloGestionProductos.Image");
            fieldTituloGestionProductos.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloGestionProductos.ImeMode = ImeMode.NoControl;
            fieldTituloGestionProductos.Location = new Point(65, 235);
            fieldTituloGestionProductos.Margin = new Padding(15, 5, 3, 3);
            fieldTituloGestionProductos.Name = "fieldTituloGestionProductos";
            fieldTituloGestionProductos.Size = new Size(399, 27);
            fieldTituloGestionProductos.TabIndex = 4;
            fieldTituloGestionProductos.Text = "      Gestión para la compra de productos";
            fieldTituloGestionProductos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldIcono
            // 
            fieldIcono.BackgroundImage = (Image) resources.GetObject("fieldIcono.BackgroundImage");
            fieldIcono.BackgroundImageLayout = ImageLayout.Center;
            fieldIcono.Dock = DockStyle.Fill;
            fieldIcono.Location = new Point(20, 26);
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
            fieldSubtitulo.Location = new Point(55, 70);
            fieldSubtitulo.Margin = new Padding(5, 5, 1, 1);
            fieldSubtitulo.Name = "fieldSubtitulo";
            fieldSubtitulo.Size = new Size(411, 39);
            fieldSubtitulo.TabIndex = 0;
            fieldSubtitulo.Text = "Registro";
            // 
            // layoutTitulo
            // 
            layoutTitulo.ColumnCount = 2;
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutTitulo.Controls.Add(btnCerrar, 1, 0);
            layoutTitulo.Controls.Add(fieldTitulo, 0, 0);
            layoutTitulo.Dock = DockStyle.Fill;
            layoutTitulo.Location = new Point(50, 20);
            layoutTitulo.Margin = new Padding(0);
            layoutTitulo.Name = "layoutTitulo";
            layoutTitulo.RowCount = 1;
            layoutTitulo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTitulo.Size = new Size(417, 45);
            layoutTitulo.TabIndex = 14;
            // 
            // btnCerrar
            // 
            btnCerrar.Animated = true;
            btnCerrar.AutoRoundedCorners = true;
            btnCerrar.BorderColor = Color.Gray;
            btnCerrar.BorderRadius = 18;
            btnCerrar.CustomizableEdges = customizableEdges5;
            btnCerrar.Dock = DockStyle.Fill;
            btnCerrar.FillColor = Color.White;
            btnCerrar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.Black;
            btnCerrar.HoverState.FillColor = Color.White;
            btnCerrar.Image = (Image) resources.GetObject("btnCerrar.Image");
            btnCerrar.Location = new Point(370, 3);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnCerrar.Size = new Size(44, 39);
            btnCerrar.TabIndex = 1;
            // 
            // fieldTitulo
            // 
            fieldTitulo.Dock = DockStyle.Fill;
            fieldTitulo.Font = new Font("Segoe UI", 20.25F);
            fieldTitulo.ForeColor = Color.Black;
            fieldTitulo.ImeMode = ImeMode.NoControl;
            fieldTitulo.Location = new Point(3, 0);
            fieldTitulo.Name = "fieldTitulo";
            fieldTitulo.Size = new Size(361, 45);
            fieldTitulo.TabIndex = 0;
            fieldTitulo.Text = "Compra";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutMontoCompra
            // 
            layoutMontoCompra.ColumnCount = 3;
            layoutMontoCompra.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutMontoCompra.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutMontoCompra.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutMontoCompra.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutMontoCompra.Controls.Add(symbolPeso, 0, 0);
            layoutMontoCompra.Controls.Add(fieldTotalCompra, 0, 0);
            layoutMontoCompra.Controls.Add(fieldTituloTotalCompra, 0, 0);
            layoutMontoCompra.Dock = DockStyle.Fill;
            layoutMontoCompra.Location = new Point(50, 575);
            layoutMontoCompra.Margin = new Padding(0);
            layoutMontoCompra.Name = "layoutMontoCompra";
            layoutMontoCompra.RowCount = 1;
            layoutMontoCompra.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutMontoCompra.Size = new Size(417, 45);
            layoutMontoCompra.TabIndex = 7;
            // 
            // symbolPeso
            // 
            symbolPeso.Dock = DockStyle.Fill;
            symbolPeso.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            symbolPeso.ForeColor = Color.Black;
            symbolPeso.ImageAlign = ContentAlignment.MiddleLeft;
            symbolPeso.ImeMode = ImeMode.NoControl;
            symbolPeso.Location = new Point(400, 5);
            symbolPeso.Margin = new Padding(3, 5, 3, 3);
            symbolPeso.Name = "symbolPeso";
            symbolPeso.Size = new Size(14, 37);
            symbolPeso.TabIndex = 2;
            symbolPeso.Text = "$";
            symbolPeso.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldTotalCompra
            // 
            fieldTotalCompra.Dock = DockStyle.Fill;
            fieldTotalCompra.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTotalCompra.ForeColor = Color.Black;
            fieldTotalCompra.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTotalCompra.ImeMode = ImeMode.NoControl;
            fieldTotalCompra.Location = new Point(302, 5);
            fieldTotalCompra.Margin = new Padding(15, 5, 3, 3);
            fieldTotalCompra.Name = "fieldTotalCompra";
            fieldTotalCompra.Size = new Size(92, 37);
            fieldTotalCompra.TabIndex = 1;
            fieldTotalCompra.Text = "0.00";
            fieldTotalCompra.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldTituloTotalCompra
            // 
            fieldTituloTotalCompra.Dock = DockStyle.Fill;
            fieldTituloTotalCompra.Font = new Font("Segoe UI", 11.25F);
            fieldTituloTotalCompra.ForeColor = Color.DimGray;
            fieldTituloTotalCompra.Image = (Image) resources.GetObject("fieldTituloTotalCompra.Image");
            fieldTituloTotalCompra.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloTotalCompra.ImeMode = ImeMode.NoControl;
            fieldTituloTotalCompra.Location = new Point(15, 5);
            fieldTituloTotalCompra.Margin = new Padding(15, 5, 3, 3);
            fieldTituloTotalCompra.Name = "fieldTituloTotalCompra";
            fieldTituloTotalCompra.Size = new Size(269, 37);
            fieldTituloTotalCompra.TabIndex = 0;
            fieldTituloTotalCompra.Text = "      Monto total de la compra";
            fieldTituloTotalCompra.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // separador1
            // 
            separador1.Dock = DockStyle.Fill;
            separador1.FillColor = Color.FromArgb(  208,   197,   188);
            separador1.Location = new Point(53, 213);
            separador1.Name = "separador1";
            separador1.Size = new Size(411, 14);
            separador1.TabIndex = 3;
            // 
            // layoutTituloProveedorAlmacen
            // 
            layoutTituloProveedorAlmacen.ColumnCount = 2;
            layoutTituloProveedorAlmacen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutTituloProveedorAlmacen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutTituloProveedorAlmacen.Controls.Add(fieldTituloNombreProveedor, 0, 0);
            layoutTituloProveedorAlmacen.Controls.Add(fieldTituloNombreAlmacen, 1, 0);
            layoutTituloProveedorAlmacen.Dock = DockStyle.Fill;
            layoutTituloProveedorAlmacen.Location = new Point(50, 130);
            layoutTituloProveedorAlmacen.Margin = new Padding(0);
            layoutTituloProveedorAlmacen.Name = "layoutTituloProveedorAlmacen";
            layoutTituloProveedorAlmacen.RowCount = 1;
            layoutTituloProveedorAlmacen.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTituloProveedorAlmacen.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutTituloProveedorAlmacen.Size = new Size(417, 35);
            layoutTituloProveedorAlmacen.TabIndex = 1;
            // 
            // fieldTituloNombreProveedor
            // 
            fieldTituloNombreProveedor.Dock = DockStyle.Fill;
            fieldTituloNombreProveedor.Font = new Font("Segoe UI", 11.25F);
            fieldTituloNombreProveedor.ForeColor = Color.DimGray;
            fieldTituloNombreProveedor.Image = (Image) resources.GetObject("fieldTituloNombreProveedor.Image");
            fieldTituloNombreProveedor.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloNombreProveedor.ImeMode = ImeMode.NoControl;
            fieldTituloNombreProveedor.Location = new Point(15, 5);
            fieldTituloNombreProveedor.Margin = new Padding(15, 5, 3, 3);
            fieldTituloNombreProveedor.Name = "fieldTituloNombreProveedor";
            fieldTituloNombreProveedor.Size = new Size(190, 27);
            fieldTituloNombreProveedor.TabIndex = 0;
            fieldTituloNombreProveedor.Text = "      Proveedor :";
            fieldTituloNombreProveedor.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloNombreAlmacen
            // 
            fieldTituloNombreAlmacen.Dock = DockStyle.Fill;
            fieldTituloNombreAlmacen.Font = new Font("Segoe UI", 11.25F);
            fieldTituloNombreAlmacen.ForeColor = Color.DimGray;
            fieldTituloNombreAlmacen.Image = (Image) resources.GetObject("fieldTituloNombreAlmacen.Image");
            fieldTituloNombreAlmacen.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloNombreAlmacen.ImeMode = ImeMode.NoControl;
            fieldTituloNombreAlmacen.Location = new Point(223, 5);
            fieldTituloNombreAlmacen.Margin = new Padding(15, 5, 3, 3);
            fieldTituloNombreAlmacen.Name = "fieldTituloNombreAlmacen";
            fieldTituloNombreAlmacen.Size = new Size(191, 27);
            fieldTituloNombreAlmacen.TabIndex = 1;
            fieldTituloNombreAlmacen.Text = "      Almacén :";
            fieldTituloNombreAlmacen.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutProveedorAlmacen
            // 
            layoutProveedorAlmacen.ColumnCount = 2;
            layoutProveedorAlmacen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutProveedorAlmacen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutProveedorAlmacen.Controls.Add(fieldNombreProveedor, 0, 0);
            layoutProveedorAlmacen.Controls.Add(fieldNombreAlmacen, 1, 0);
            layoutProveedorAlmacen.Dock = DockStyle.Fill;
            layoutProveedorAlmacen.Location = new Point(50, 165);
            layoutProveedorAlmacen.Margin = new Padding(0);
            layoutProveedorAlmacen.Name = "layoutProveedorAlmacen";
            layoutProveedorAlmacen.RowCount = 1;
            layoutProveedorAlmacen.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutProveedorAlmacen.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutProveedorAlmacen.Size = new Size(417, 45);
            layoutProveedorAlmacen.TabIndex = 2;
            // 
            // fieldNombreProveedor
            // 
            fieldNombreProveedor.Animated = true;
            fieldNombreProveedor.BackColor = Color.Transparent;
            fieldNombreProveedor.BorderColor = Color.Gainsboro;
            fieldNombreProveedor.BorderRadius = 16;
            fieldNombreProveedor.CustomizableEdges = customizableEdges7;
            fieldNombreProveedor.Dock = DockStyle.Fill;
            fieldNombreProveedor.DrawMode = DrawMode.OwnerDrawFixed;
            fieldNombreProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldNombreProveedor.FocusedColor = Color.SandyBrown;
            fieldNombreProveedor.FocusedState.BorderColor = Color.SandyBrown;
            fieldNombreProveedor.Font = new Font("Segoe UI", 11.25F);
            fieldNombreProveedor.ForeColor = Color.Black;
            fieldNombreProveedor.ItemHeight = 29;
            fieldNombreProveedor.Location = new Point(5, 5);
            fieldNombreProveedor.Margin = new Padding(5);
            fieldNombreProveedor.Name = "fieldNombreProveedor";
            fieldNombreProveedor.ShadowDecoration.CustomizableEdges = customizableEdges8;
            fieldNombreProveedor.Size = new Size(198, 35);
            fieldNombreProveedor.TabIndex = 0;
            fieldNombreProveedor.TextOffset = new Point(10, 0);
            // 
            // fieldNombreAlmacen
            // 
            fieldNombreAlmacen.Animated = true;
            fieldNombreAlmacen.BackColor = Color.Transparent;
            fieldNombreAlmacen.BorderColor = Color.Gainsboro;
            fieldNombreAlmacen.BorderRadius = 16;
            fieldNombreAlmacen.CustomizableEdges = customizableEdges9;
            fieldNombreAlmacen.Dock = DockStyle.Fill;
            fieldNombreAlmacen.DrawMode = DrawMode.OwnerDrawFixed;
            fieldNombreAlmacen.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldNombreAlmacen.FocusedColor = Color.SandyBrown;
            fieldNombreAlmacen.FocusedState.BorderColor = Color.SandyBrown;
            fieldNombreAlmacen.Font = new Font("Segoe UI", 11.25F);
            fieldNombreAlmacen.ForeColor = Color.Black;
            fieldNombreAlmacen.ItemHeight = 29;
            fieldNombreAlmacen.Location = new Point(213, 5);
            fieldNombreAlmacen.Margin = new Padding(5);
            fieldNombreAlmacen.Name = "fieldNombreAlmacen";
            fieldNombreAlmacen.ShadowDecoration.CustomizableEdges = customizableEdges10;
            fieldNombreAlmacen.Size = new Size(199, 35);
            fieldNombreAlmacen.TabIndex = 1;
            fieldNombreAlmacen.TextOffset = new Point(10, 0);
            // 
            // guna2Separator1
            // 
            guna2Separator1.Dock = DockStyle.Fill;
            guna2Separator1.FillColor = Color.Gainsboro;
            guna2Separator1.Location = new Point(53, 558);
            guna2Separator1.Name = "guna2Separator1";
            guna2Separator1.Size = new Size(411, 14);
            guna2Separator1.TabIndex = 15;
            // 
            // layoutEncabezadosTabla
            // 
            layoutEncabezadosTabla.BackColor = Color.WhiteSmoke;
            layoutEncabezadosTabla.ColumnCount = 5;
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutEncabezadosTabla.Controls.Add(fieldTituloCantidad, 2, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloPrecio, 1, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloProducto, 0, 0);
            layoutEncabezadosTabla.Dock = DockStyle.Fill;
            layoutEncabezadosTabla.Location = new Point(51, 321);
            layoutEncabezadosTabla.Margin = new Padding(1);
            layoutEncabezadosTabla.Name = "layoutEncabezadosTabla";
            layoutEncabezadosTabla.RowCount = 1;
            layoutEncabezadosTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutEncabezadosTabla.Size = new Size(415, 43);
            layoutEncabezadosTabla.TabIndex = 16;
            // 
            // fieldTituloCantidad
            // 
            fieldTituloCantidad.Dock = DockStyle.Fill;
            fieldTituloCantidad.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloCantidad.ForeColor = Color.Black;
            fieldTituloCantidad.ImeMode = ImeMode.NoControl;
            fieldTituloCantidad.Location = new Point(306, 1);
            fieldTituloCantidad.Margin = new Padding(1);
            fieldTituloCantidad.Name = "fieldTituloCantidad";
            fieldTituloCantidad.Size = new Size(48, 41);
            fieldTituloCantidad.TabIndex = 2;
            fieldTituloCantidad.Text = "C";
            fieldTituloCantidad.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldTituloPrecio
            // 
            fieldTituloPrecio.Dock = DockStyle.Fill;
            fieldTituloPrecio.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloPrecio.ForeColor = Color.Black;
            fieldTituloPrecio.ImeMode = ImeMode.NoControl;
            fieldTituloPrecio.Location = new Point(176, 1);
            fieldTituloPrecio.Margin = new Padding(1);
            fieldTituloPrecio.Name = "fieldTituloPrecio";
            fieldTituloPrecio.Size = new Size(128, 41);
            fieldTituloPrecio.TabIndex = 1;
            fieldTituloPrecio.Text = "Precio";
            fieldTituloPrecio.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldTituloProducto
            // 
            fieldTituloProducto.Dock = DockStyle.Fill;
            fieldTituloProducto.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloProducto.ForeColor = Color.Black;
            fieldTituloProducto.ImeMode = ImeMode.NoControl;
            fieldTituloProducto.Location = new Point(1, 1);
            fieldTituloProducto.Margin = new Padding(1);
            fieldTituloProducto.Name = "fieldTituloProducto";
            fieldTituloProducto.Size = new Size(173, 41);
            fieldTituloProducto.TabIndex = 0;
            fieldTituloProducto.Text = "Producto";
            fieldTituloProducto.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // contenedorVistas
            // 
            contenedorVistas.AutoScroll = true;
            contenedorVistas.Dock = DockStyle.Fill;
            contenedorVistas.Location = new Point(50, 365);
            contenedorVistas.Margin = new Padding(0);
            contenedorVistas.Name = "contenedorVistas";
            contenedorVistas.Size = new Size(417, 190);
            contenedorVistas.TabIndex = 17;
            // 
            // layoutGestionProductos
            // 
            layoutGestionProductos.ColumnCount = 3;
            layoutGestionProductos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutGestionProductos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            layoutGestionProductos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutGestionProductos.Controls.Add(btnAdicionarProducto, 2, 0);
            layoutGestionProductos.Controls.Add(fieldCantidad, 1, 0);
            layoutGestionProductos.Controls.Add(fieldNombreProducto, 0, 0);
            layoutGestionProductos.Dock = DockStyle.Fill;
            layoutGestionProductos.Location = new Point(50, 265);
            layoutGestionProductos.Margin = new Padding(0);
            layoutGestionProductos.Name = "layoutGestionProductos";
            layoutGestionProductos.RowCount = 1;
            layoutGestionProductos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutGestionProductos.Size = new Size(417, 45);
            layoutGestionProductos.TabIndex = 18;
            // 
            // btnAdicionarProducto
            // 
            btnAdicionarProducto.Animated = true;
            btnAdicionarProducto.BorderRadius = 18;
            btnAdicionarProducto.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnAdicionarProducto.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnAdicionarProducto.CustomizableEdges = customizableEdges11;
            btnAdicionarProducto.DialogResult = DialogResult.Cancel;
            btnAdicionarProducto.Dock = DockStyle.Fill;
            btnAdicionarProducto.Enabled = false;
            btnAdicionarProducto.FillColor = Color.PeachPuff;
            btnAdicionarProducto.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAdicionarProducto.ForeColor = Color.White;
            btnAdicionarProducto.Location = new Point(372, 5);
            btnAdicionarProducto.Margin = new Padding(5);
            btnAdicionarProducto.Name = "btnAdicionarProducto";
            btnAdicionarProducto.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnAdicionarProducto.Size = new Size(40, 35);
            btnAdicionarProducto.TabIndex = 2;
            // 
            // fieldCantidad
            // 
            fieldCantidad.Animated = true;
            fieldCantidad.BorderColor = Color.Gainsboro;
            fieldCantidad.BorderRadius = 16;
            fieldCantidad.Cursor = Cursors.IBeam;
            fieldCantidad.CustomizableEdges = customizableEdges13;
            fieldCantidad.DefaultText = "";
            fieldCantidad.DisabledState.BorderColor = Color.Gainsboro;
            fieldCantidad.DisabledState.FillColor = Color.White;
            fieldCantidad.DisabledState.ForeColor = Color.DimGray;
            fieldCantidad.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldCantidad.Dock = DockStyle.Fill;
            fieldCantidad.FocusedState.BorderColor = Color.SandyBrown;
            fieldCantidad.Font = new Font("Segoe UI", 11.25F);
            fieldCantidad.ForeColor = Color.Black;
            fieldCantidad.HoverState.BorderColor = Color.SandyBrown;
            fieldCantidad.IconLeftOffset = new Point(10, 0);
            fieldCantidad.IconRight = (Image) resources.GetObject("fieldCantidad.IconRight");
            fieldCantidad.IconRightOffset = new Point(6, 0);
            fieldCantidad.IconRightSize = new Size(12, 12);
            fieldCantidad.Location = new Point(277, 5);
            fieldCantidad.Margin = new Padding(5);
            fieldCantidad.Name = "fieldCantidad";
            fieldCantidad.PasswordChar = '\0';
            fieldCantidad.PlaceholderForeColor = Color.DimGray;
            fieldCantidad.PlaceholderText = "Cant.";
            fieldCantidad.SelectedText = "";
            fieldCantidad.ShadowDecoration.CustomizableEdges = customizableEdges14;
            fieldCantidad.Size = new Size(85, 35);
            fieldCantidad.TabIndex = 1;
            fieldCantidad.TextAlign = HorizontalAlignment.Right;
            fieldCantidad.TextOffset = new Point(5, 0);
            // 
            // fieldNombreProducto
            // 
            fieldNombreProducto.Animated = true;
            fieldNombreProducto.BorderColor = Color.Gainsboro;
            fieldNombreProducto.BorderRadius = 16;
            fieldNombreProducto.Cursor = Cursors.IBeam;
            fieldNombreProducto.CustomizableEdges = customizableEdges15;
            fieldNombreProducto.DefaultText = "";
            fieldNombreProducto.DisabledState.BorderColor = Color.Gainsboro;
            fieldNombreProducto.DisabledState.FillColor = Color.White;
            fieldNombreProducto.DisabledState.ForeColor = Color.DimGray;
            fieldNombreProducto.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldNombreProducto.Dock = DockStyle.Fill;
            fieldNombreProducto.FocusedState.BorderColor = Color.SandyBrown;
            fieldNombreProducto.Font = new Font("Segoe UI", 11.25F);
            fieldNombreProducto.ForeColor = Color.Black;
            fieldNombreProducto.HoverState.BorderColor = Color.SandyBrown;
            fieldNombreProducto.IconLeft = (Image) resources.GetObject("fieldNombreProducto.IconLeft");
            fieldNombreProducto.IconLeftOffset = new Point(10, 0);
            fieldNombreProducto.Location = new Point(5, 5);
            fieldNombreProducto.Margin = new Padding(5);
            fieldNombreProducto.Name = "fieldNombreProducto";
            fieldNombreProducto.PasswordChar = '\0';
            fieldNombreProducto.PlaceholderForeColor = Color.DimGray;
            fieldNombreProducto.PlaceholderText = "Nombre del producto";
            fieldNombreProducto.SelectedText = "";
            fieldNombreProducto.ShadowDecoration.CustomizableEdges = customizableEdges16;
            fieldNombreProducto.Size = new Size(262, 35);
            fieldNombreProducto.TabIndex = 0;
            fieldNombreProducto.TextOffset = new Point(5, 0);
            // 
            // VistaRegistroCompra
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 685);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaRegistroCompra";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaRegistroCompra";
            layoutBase.ResumeLayout(false);
            layoutBotones.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ((ISupportInitialize) fieldIcono).EndInit();
            layoutTitulo.ResumeLayout(false);
            layoutMontoCompra.ResumeLayout(false);
            layoutTituloProveedorAlmacen.ResumeLayout(false);
            layoutProveedorAlmacen.ResumeLayout(false);
            layoutEncabezadosTabla.ResumeLayout(false);
            layoutGestionProductos.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutBotones;
        private Guna2Button btnSalir;
        private Guna2Button btnRegistrar;
        private TableLayoutPanel layoutVista;
        private Label fieldTituloGestionProductos;
        private PictureBox fieldIcono;
        private Label fieldSubtitulo;
        private TableLayoutPanel layoutTitulo;
        private Guna2Button btnCerrar;
        private Label fieldTitulo;
        private TableLayoutPanel layoutMontoCompra;
        private Label symbolPeso;
        private Label fieldTotalCompra;
        private Label fieldTituloTotalCompra;
        private Guna2Separator separador1;
        private TableLayoutPanel layoutTituloProveedorAlmacen;
        private Label fieldTituloNombreProveedor;
        private Label fieldTituloNombreAlmacen;
        private TableLayoutPanel layoutProveedorAlmacen;
        private Guna2ComboBox fieldNombreProveedor;
        private Guna2ComboBox fieldNombreAlmacen;
        private Guna2Separator guna2Separator1;
        private TableLayoutPanel layoutEncabezadosTabla;
        private Label fieldTituloCantidad;
        private Label fieldTituloPrecio;
        private Label fieldTituloProducto;
        private Panel contenedorVistas;
        private TableLayoutPanel layoutGestionProductos;
        private Guna2Button btnAdicionarProducto;
        private Guna2TextBox fieldCantidad;
        private Guna2TextBox fieldNombreProducto;
    }
}