using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Pago {
    partial class VistaRegistroPago {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaRegistroPago));
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
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldIcono = new PictureBox();
            fieldSubtitulo = new Label();
            layoutTitulo = new TableLayoutPanel();
            btnCerrar = new Guna2Button();
            fieldTitulo = new Label();
            fieldTituloGestionPagos = new Label();
            layoutGestionPagos = new TableLayoutPanel();
            fieldMetodoPago = new Guna2ComboBox();
            btnAdicionarPago = new Guna2Button();
            fieldMonto = new Guna2TextBox();
            fieldTipoMoneda = new Guna2ComboBox();
            layoutEncabezadosTabla = new TableLayoutPanel();
            fieldTituloMonto = new Label();
            fieldTituloMetodo = new Label();
            layoutSuma = new TableLayoutPanel();
            symbolPeso1 = new Label();
            fieldSuma = new Label();
            fieldTituloSuma = new Label();
            separador2 = new Guna2Separator();
            layoutPendiente = new TableLayoutPanel();
            symbolPeso2 = new Label();
            fieldPendiente = new Label();
            fieldTituloPendiente = new Label();
            layoutDevolucion = new TableLayoutPanel();
            symbolPeso3 = new Label();
            fieldDevolucion = new Label();
            lbTituloDevolucion = new Label();
            contenedorVistas = new Panel();
            layoutBotones = new TableLayoutPanel();
            btnSalir = new Guna2Button();
            btnRegistrar = new Guna2Button();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            layoutTitulo.SuspendLayout();
            layoutGestionPagos.SuspendLayout();
            layoutEncabezadosTabla.SuspendLayout();
            layoutSuma.SuspendLayout();
            layoutPendiente.SuspendLayout();
            layoutDevolucion.SuspendLayout();
            layoutBotones.SuspendLayout();
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
            layoutBase.Controls.Add(layoutVista, 1, 0);
            layoutBase.Controls.Add(layoutBotones, 1, 1);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 2;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            layoutBase.Size = new Size(500, 685);
            layoutBase.TabIndex = 2;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.White;
            layoutVista.ColumnCount = 4;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.Controls.Add(fieldIcono, 1, 1);
            layoutVista.Controls.Add(fieldSubtitulo, 2, 2);
            layoutVista.Controls.Add(layoutTitulo, 2, 1);
            layoutVista.Controls.Add(fieldTituloGestionPagos, 2, 4);
            layoutVista.Controls.Add(layoutGestionPagos, 2, 5);
            layoutVista.Controls.Add(layoutEncabezadosTabla, 2, 7);
            layoutVista.Controls.Add(layoutSuma, 2, 10);
            layoutVista.Controls.Add(separador2, 2, 9);
            layoutVista.Controls.Add(layoutPendiente, 2, 11);
            layoutVista.Controls.Add(layoutDevolucion, 2, 12);
            layoutVista.Controls.Add(contenedorVistas, 2, 8);
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
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.Size = new Size(487, 620);
            layoutVista.TabIndex = 0;
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
            btnCerrar.CustomizableEdges = customizableEdges1;
            btnCerrar.Dock = DockStyle.Fill;
            btnCerrar.FillColor = Color.White;
            btnCerrar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.Black;
            btnCerrar.HoverState.FillColor = Color.White;
            btnCerrar.Image = (Image) resources.GetObject("btnCerrar.Image");
            btnCerrar.Location = new Point(370, 3);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.ShadowDecoration.CustomizableEdges = customizableEdges2;
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
            fieldTitulo.Text = "Pago";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloGestionPagos
            // 
            fieldTituloGestionPagos.Dock = DockStyle.Fill;
            fieldTituloGestionPagos.Font = new Font("Segoe UI", 11.25F);
            fieldTituloGestionPagos.ForeColor = Color.DimGray;
            fieldTituloGestionPagos.Image = (Image) resources.GetObject("fieldTituloGestionPagos.Image");
            fieldTituloGestionPagos.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloGestionPagos.ImeMode = ImeMode.NoControl;
            fieldTituloGestionPagos.Location = new Point(65, 135);
            fieldTituloGestionPagos.Margin = new Padding(15, 5, 3, 3);
            fieldTituloGestionPagos.Name = "fieldTituloGestionPagos";
            fieldTituloGestionPagos.Size = new Size(399, 27);
            fieldTituloGestionPagos.TabIndex = 15;
            fieldTituloGestionPagos.Text = "      Gestión para los pagos de la venta de productos :";
            fieldTituloGestionPagos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutGestionPagos
            // 
            layoutGestionPagos.ColumnCount = 4;
            layoutGestionPagos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutGestionPagos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutGestionPagos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            layoutGestionPagos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutGestionPagos.Controls.Add(fieldMetodoPago, 0, 0);
            layoutGestionPagos.Controls.Add(btnAdicionarPago, 3, 0);
            layoutGestionPagos.Controls.Add(fieldMonto, 1, 0);
            layoutGestionPagos.Controls.Add(fieldTipoMoneda, 2, 0);
            layoutGestionPagos.Dock = DockStyle.Fill;
            layoutGestionPagos.Location = new Point(50, 165);
            layoutGestionPagos.Margin = new Padding(0);
            layoutGestionPagos.Name = "layoutGestionPagos";
            layoutGestionPagos.RowCount = 1;
            layoutGestionPagos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutGestionPagos.Size = new Size(417, 45);
            layoutGestionPagos.TabIndex = 16;
            // 
            // fieldMetodoPago
            // 
            fieldMetodoPago.Animated = true;
            fieldMetodoPago.BackColor = Color.Transparent;
            fieldMetodoPago.BorderColor = Color.Gainsboro;
            fieldMetodoPago.BorderRadius = 16;
            fieldMetodoPago.CustomizableEdges = customizableEdges3;
            fieldMetodoPago.Dock = DockStyle.Fill;
            fieldMetodoPago.DrawMode = DrawMode.OwnerDrawFixed;
            fieldMetodoPago.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldMetodoPago.FocusedColor = Color.SandyBrown;
            fieldMetodoPago.FocusedState.BorderColor = Color.SandyBrown;
            fieldMetodoPago.Font = new Font("Segoe UI", 11.25F);
            fieldMetodoPago.ForeColor = Color.Black;
            fieldMetodoPago.ItemHeight = 29;
            fieldMetodoPago.Location = new Point(5, 5);
            fieldMetodoPago.Margin = new Padding(5);
            fieldMetodoPago.Name = "fieldMetodoPago";
            fieldMetodoPago.ShadowDecoration.CustomizableEdges = customizableEdges4;
            fieldMetodoPago.Size = new Size(147, 35);
            fieldMetodoPago.TabIndex = 3;
            fieldMetodoPago.TextOffset = new Point(10, 0);
            // 
            // btnAdicionarPago
            // 
            btnAdicionarPago.Animated = true;
            btnAdicionarPago.BorderRadius = 18;
            btnAdicionarPago.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnAdicionarPago.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnAdicionarPago.CustomizableEdges = customizableEdges5;
            btnAdicionarPago.DialogResult = DialogResult.Cancel;
            btnAdicionarPago.Dock = DockStyle.Fill;
            btnAdicionarPago.Enabled = false;
            btnAdicionarPago.FillColor = Color.PeachPuff;
            btnAdicionarPago.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAdicionarPago.ForeColor = Color.White;
            btnAdicionarPago.Location = new Point(370, 3);
            btnAdicionarPago.Name = "btnAdicionarPago";
            btnAdicionarPago.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnAdicionarPago.Size = new Size(44, 39);
            btnAdicionarPago.TabIndex = 2;
            // 
            // fieldMonto
            // 
            fieldMonto.Animated = true;
            fieldMonto.BorderColor = Color.Gainsboro;
            fieldMonto.BorderRadius = 16;
            fieldMonto.Cursor = Cursors.IBeam;
            fieldMonto.CustomizableEdges = customizableEdges7;
            fieldMonto.DefaultText = "";
            fieldMonto.DisabledState.BorderColor = Color.White;
            fieldMonto.DisabledState.ForeColor = Color.DimGray;
            fieldMonto.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldMonto.Dock = DockStyle.Fill;
            fieldMonto.FocusedState.BorderColor = Color.SandyBrown;
            fieldMonto.Font = new Font("Segoe UI", 11.25F);
            fieldMonto.ForeColor = Color.Black;
            fieldMonto.HoverState.BorderColor = Color.SandyBrown;
            fieldMonto.IconLeftOffset = new Point(10, 0);
            fieldMonto.IconRight = (Image) resources.GetObject("fieldMonto.IconRight");
            fieldMonto.IconRightOffset = new Point(6, 0);
            fieldMonto.IconRightSize = new Size(12, 12);
            fieldMonto.Location = new Point(162, 5);
            fieldMonto.Margin = new Padding(5);
            fieldMonto.Name = "fieldMonto";
            fieldMonto.PasswordChar = '\0';
            fieldMonto.PlaceholderForeColor = Color.DimGray;
            fieldMonto.PlaceholderText = "Monto";
            fieldMonto.SelectedText = "";
            fieldMonto.ShadowDecoration.CustomizableEdges = customizableEdges8;
            fieldMonto.Size = new Size(110, 35);
            fieldMonto.TabIndex = 1;
            fieldMonto.TextAlign = HorizontalAlignment.Right;
            fieldMonto.TextOffset = new Point(5, 0);
            // 
            // fieldTipoMoneda
            // 
            fieldTipoMoneda.Animated = true;
            fieldTipoMoneda.BackColor = Color.Transparent;
            fieldTipoMoneda.BorderColor = Color.Gainsboro;
            fieldTipoMoneda.BorderRadius = 16;
            fieldTipoMoneda.CustomizableEdges = customizableEdges9;
            fieldTipoMoneda.Dock = DockStyle.Fill;
            fieldTipoMoneda.DrawMode = DrawMode.OwnerDrawFixed;
            fieldTipoMoneda.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldTipoMoneda.FocusedColor = Color.SandyBrown;
            fieldTipoMoneda.FocusedState.BorderColor = Color.SandyBrown;
            fieldTipoMoneda.Font = new Font("Segoe UI", 11.25F);
            fieldTipoMoneda.ForeColor = Color.Black;
            fieldTipoMoneda.ItemHeight = 29;
            fieldTipoMoneda.Location = new Point(282, 5);
            fieldTipoMoneda.Margin = new Padding(5);
            fieldTipoMoneda.Name = "fieldTipoMoneda";
            fieldTipoMoneda.ShadowDecoration.CustomizableEdges = customizableEdges10;
            fieldTipoMoneda.Size = new Size(80, 35);
            fieldTipoMoneda.TabIndex = 35;
            fieldTipoMoneda.TextOffset = new Point(10, 0);
            // 
            // layoutEncabezadosTabla
            // 
            layoutEncabezadosTabla.BackColor = Color.WhiteSmoke;
            layoutEncabezadosTabla.ColumnCount = 4;
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutEncabezadosTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutEncabezadosTabla.Controls.Add(fieldTituloMonto, 1, 0);
            layoutEncabezadosTabla.Controls.Add(fieldTituloMetodo, 0, 0);
            layoutEncabezadosTabla.Dock = DockStyle.Fill;
            layoutEncabezadosTabla.Location = new Point(51, 221);
            layoutEncabezadosTabla.Margin = new Padding(1);
            layoutEncabezadosTabla.Name = "layoutEncabezadosTabla";
            layoutEncabezadosTabla.RowCount = 1;
            layoutEncabezadosTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutEncabezadosTabla.Size = new Size(415, 43);
            layoutEncabezadosTabla.TabIndex = 17;
            // 
            // fieldTituloMonto
            // 
            fieldTituloMonto.Dock = DockStyle.Fill;
            fieldTituloMonto.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloMonto.ForeColor = Color.Black;
            fieldTituloMonto.ImeMode = ImeMode.NoControl;
            fieldTituloMonto.Location = new Point(226, 1);
            fieldTituloMonto.Margin = new Padding(1);
            fieldTituloMonto.Name = "fieldTituloMonto";
            fieldTituloMonto.Size = new Size(128, 41);
            fieldTituloMonto.TabIndex = 1;
            fieldTituloMonto.Text = "Monto";
            fieldTituloMonto.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldTituloMetodo
            // 
            fieldTituloMetodo.Dock = DockStyle.Fill;
            fieldTituloMetodo.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldTituloMetodo.ForeColor = Color.Black;
            fieldTituloMetodo.ImeMode = ImeMode.NoControl;
            fieldTituloMetodo.Location = new Point(1, 1);
            fieldTituloMetodo.Margin = new Padding(1);
            fieldTituloMetodo.Name = "fieldTituloMetodo";
            fieldTituloMetodo.Size = new Size(223, 41);
            fieldTituloMetodo.TabIndex = 0;
            fieldTituloMetodo.Text = "Metodo";
            fieldTituloMetodo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // layoutSuma
            // 
            layoutSuma.ColumnCount = 3;
            layoutSuma.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutSuma.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutSuma.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutSuma.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutSuma.Controls.Add(symbolPeso1, 0, 0);
            layoutSuma.Controls.Add(fieldSuma, 0, 0);
            layoutSuma.Controls.Add(fieldTituloSuma, 0, 0);
            layoutSuma.Dock = DockStyle.Fill;
            layoutSuma.Location = new Point(50, 495);
            layoutSuma.Margin = new Padding(0);
            layoutSuma.Name = "layoutSuma";
            layoutSuma.RowCount = 1;
            layoutSuma.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutSuma.Size = new Size(417, 35);
            layoutSuma.TabIndex = 18;
            // 
            // symbolPeso1
            // 
            symbolPeso1.Dock = DockStyle.Fill;
            symbolPeso1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            symbolPeso1.ForeColor = Color.Black;
            symbolPeso1.ImageAlign = ContentAlignment.MiddleLeft;
            symbolPeso1.ImeMode = ImeMode.NoControl;
            symbolPeso1.Location = new Point(400, 5);
            symbolPeso1.Margin = new Padding(3, 5, 3, 3);
            symbolPeso1.Name = "symbolPeso1";
            symbolPeso1.Size = new Size(14, 27);
            symbolPeso1.TabIndex = 2;
            symbolPeso1.Text = "$";
            symbolPeso1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldSuma
            // 
            fieldSuma.Dock = DockStyle.Fill;
            fieldSuma.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldSuma.ForeColor = Color.Black;
            fieldSuma.ImageAlign = ContentAlignment.MiddleLeft;
            fieldSuma.ImeMode = ImeMode.NoControl;
            fieldSuma.Location = new Point(302, 5);
            fieldSuma.Margin = new Padding(15, 5, 3, 3);
            fieldSuma.Name = "fieldSuma";
            fieldSuma.Size = new Size(92, 27);
            fieldSuma.TabIndex = 1;
            fieldSuma.Text = "0";
            fieldSuma.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldTituloSuma
            // 
            fieldTituloSuma.Dock = DockStyle.Fill;
            fieldTituloSuma.Font = new Font("Segoe UI", 11.25F);
            fieldTituloSuma.ForeColor = Color.DimGray;
            fieldTituloSuma.Image = (Image) resources.GetObject("fieldTituloSuma.Image");
            fieldTituloSuma.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloSuma.ImeMode = ImeMode.NoControl;
            fieldTituloSuma.Location = new Point(15, 5);
            fieldTituloSuma.Margin = new Padding(15, 5, 3, 3);
            fieldTituloSuma.Name = "fieldTituloSuma";
            fieldTituloSuma.Size = new Size(269, 27);
            fieldTituloSuma.TabIndex = 0;
            fieldTituloSuma.Text = "      Suma total";
            fieldTituloSuma.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // separador2
            // 
            separador2.Dock = DockStyle.Fill;
            separador2.FillColor = Color.Gainsboro;
            separador2.Location = new Point(53, 478);
            separador2.Name = "separador2";
            separador2.Size = new Size(411, 14);
            separador2.TabIndex = 19;
            // 
            // layoutPendiente
            // 
            layoutPendiente.ColumnCount = 3;
            layoutPendiente.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPendiente.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutPendiente.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutPendiente.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutPendiente.Controls.Add(symbolPeso2, 0, 0);
            layoutPendiente.Controls.Add(fieldPendiente, 0, 0);
            layoutPendiente.Controls.Add(fieldTituloPendiente, 0, 0);
            layoutPendiente.Dock = DockStyle.Fill;
            layoutPendiente.Location = new Point(50, 530);
            layoutPendiente.Margin = new Padding(0);
            layoutPendiente.Name = "layoutPendiente";
            layoutPendiente.RowCount = 1;
            layoutPendiente.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutPendiente.Size = new Size(417, 35);
            layoutPendiente.TabIndex = 20;
            // 
            // symbolPeso2
            // 
            symbolPeso2.Dock = DockStyle.Fill;
            symbolPeso2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            symbolPeso2.ForeColor = Color.Black;
            symbolPeso2.ImageAlign = ContentAlignment.MiddleLeft;
            symbolPeso2.ImeMode = ImeMode.NoControl;
            symbolPeso2.Location = new Point(400, 5);
            symbolPeso2.Margin = new Padding(3, 5, 3, 3);
            symbolPeso2.Name = "symbolPeso2";
            symbolPeso2.Size = new Size(14, 27);
            symbolPeso2.TabIndex = 2;
            symbolPeso2.Text = "$";
            symbolPeso2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldPendiente
            // 
            fieldPendiente.Dock = DockStyle.Fill;
            fieldPendiente.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldPendiente.ForeColor = Color.Black;
            fieldPendiente.ImageAlign = ContentAlignment.MiddleLeft;
            fieldPendiente.ImeMode = ImeMode.NoControl;
            fieldPendiente.Location = new Point(302, 5);
            fieldPendiente.Margin = new Padding(15, 5, 3, 3);
            fieldPendiente.Name = "fieldPendiente";
            fieldPendiente.Size = new Size(92, 27);
            fieldPendiente.TabIndex = 1;
            fieldPendiente.Text = "0";
            fieldPendiente.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldTituloPendiente
            // 
            fieldTituloPendiente.Dock = DockStyle.Fill;
            fieldTituloPendiente.Font = new Font("Segoe UI", 11.25F);
            fieldTituloPendiente.ForeColor = Color.DimGray;
            fieldTituloPendiente.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloPendiente.ImeMode = ImeMode.NoControl;
            fieldTituloPendiente.Location = new Point(15, 5);
            fieldTituloPendiente.Margin = new Padding(15, 5, 3, 3);
            fieldTituloPendiente.Name = "fieldTituloPendiente";
            fieldTituloPendiente.Size = new Size(269, 27);
            fieldTituloPendiente.TabIndex = 0;
            fieldTituloPendiente.Text = "      Pendiente por pagar";
            fieldTituloPendiente.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutDevolucion
            // 
            layoutDevolucion.ColumnCount = 3;
            layoutDevolucion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDevolucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutDevolucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDevolucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDevolucion.Controls.Add(symbolPeso3, 0, 0);
            layoutDevolucion.Controls.Add(fieldDevolucion, 0, 0);
            layoutDevolucion.Controls.Add(lbTituloDevolucion, 0, 0);
            layoutDevolucion.Dock = DockStyle.Fill;
            layoutDevolucion.Location = new Point(50, 565);
            layoutDevolucion.Margin = new Padding(0);
            layoutDevolucion.Name = "layoutDevolucion";
            layoutDevolucion.RowCount = 1;
            layoutDevolucion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDevolucion.Size = new Size(417, 35);
            layoutDevolucion.TabIndex = 21;
            // 
            // symbolPeso3
            // 
            symbolPeso3.Dock = DockStyle.Fill;
            symbolPeso3.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            symbolPeso3.ForeColor = Color.Black;
            symbolPeso3.ImageAlign = ContentAlignment.MiddleLeft;
            symbolPeso3.ImeMode = ImeMode.NoControl;
            symbolPeso3.Location = new Point(400, 5);
            symbolPeso3.Margin = new Padding(3, 5, 3, 3);
            symbolPeso3.Name = "symbolPeso3";
            symbolPeso3.Size = new Size(14, 27);
            symbolPeso3.TabIndex = 2;
            symbolPeso3.Text = "$";
            symbolPeso3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldDevolucion
            // 
            fieldDevolucion.Dock = DockStyle.Fill;
            fieldDevolucion.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldDevolucion.ForeColor = Color.Black;
            fieldDevolucion.ImageAlign = ContentAlignment.MiddleLeft;
            fieldDevolucion.ImeMode = ImeMode.NoControl;
            fieldDevolucion.Location = new Point(302, 5);
            fieldDevolucion.Margin = new Padding(15, 5, 3, 3);
            fieldDevolucion.Name = "fieldDevolucion";
            fieldDevolucion.Size = new Size(92, 27);
            fieldDevolucion.TabIndex = 1;
            fieldDevolucion.Text = "0";
            fieldDevolucion.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lbTituloDevolucion
            // 
            lbTituloDevolucion.Dock = DockStyle.Fill;
            lbTituloDevolucion.Font = new Font("Segoe UI", 11.25F);
            lbTituloDevolucion.ForeColor = Color.DimGray;
            lbTituloDevolucion.ImageAlign = ContentAlignment.MiddleLeft;
            lbTituloDevolucion.ImeMode = ImeMode.NoControl;
            lbTituloDevolucion.Location = new Point(15, 5);
            lbTituloDevolucion.Margin = new Padding(15, 5, 3, 3);
            lbTituloDevolucion.Name = "lbTituloDevolucion";
            lbTituloDevolucion.Size = new Size(269, 27);
            lbTituloDevolucion.TabIndex = 0;
            lbTituloDevolucion.Text = "      Devolución";
            lbTituloDevolucion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // contenedorVistas
            // 
            contenedorVistas.AutoScroll = true;
            contenedorVistas.Dock = DockStyle.Fill;
            contenedorVistas.Location = new Point(50, 265);
            contenedorVistas.Margin = new Padding(0);
            contenedorVistas.Name = "contenedorVistas";
            contenedorVistas.Size = new Size(417, 210);
            contenedorVistas.TabIndex = 22;
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
            layoutBotones.TabIndex = 4;
            // 
            // btnSalir
            // 
            btnSalir.Animated = true;
            btnSalir.BorderColor = Color.Gainsboro;
            btnSalir.BorderRadius = 18;
            btnSalir.BorderThickness = 1;
            btnSalir.CustomizableEdges = customizableEdges11;
            btnSalir.Dock = DockStyle.Fill;
            btnSalir.FillColor = Color.White;
            btnSalir.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSalir.ForeColor = Color.Gainsboro;
            btnSalir.HoverState.BorderColor = Color.PeachPuff;
            btnSalir.HoverState.FillColor = Color.PeachPuff;
            btnSalir.HoverState.ForeColor = Color.Black;
            btnSalir.Location = new Point(302, 3);
            btnSalir.Name = "btnSalir";
            btnSalir.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnSalir.Size = new Size(160, 39);
            btnSalir.TabIndex = 14;
            btnSalir.Text = "Salir";
            // 
            // btnRegistrar
            // 
            btnRegistrar.Animated = true;
            btnRegistrar.BorderRadius = 18;
            btnRegistrar.CustomizableEdges = customizableEdges13;
            btnRegistrar.Dock = DockStyle.Fill;
            btnRegistrar.Enabled = false;
            btnRegistrar.FillColor = Color.PeachPuff;
            btnRegistrar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.Black;
            btnRegistrar.Location = new Point(53, 3);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnRegistrar.Size = new Size(243, 39);
            btnRegistrar.TabIndex = 15;
            btnRegistrar.Text = "Registrar pagos";
            // 
            // VistaRegistroPago
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 685);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaRegistroPago";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaRegistrPago";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ((ISupportInitialize) fieldIcono).EndInit();
            layoutTitulo.ResumeLayout(false);
            layoutGestionPagos.ResumeLayout(false);
            layoutEncabezadosTabla.ResumeLayout(false);
            layoutSuma.ResumeLayout(false);
            layoutPendiente.ResumeLayout(false);
            layoutDevolucion.ResumeLayout(false);
            layoutBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private PictureBox fieldIcono;
        private Label fieldSubtitulo;
        private TableLayoutPanel layoutTitulo;
        private Guna2Button btnCerrar;
        private Label fieldTitulo;
        private TableLayoutPanel layoutBotones;
        private Guna2Button btnSalir;
        private Guna2Button btnRegistrar;
        private Label fieldTituloGestionPagos;
        private TableLayoutPanel layoutGestionPagos;
        private Guna2Button btnAdicionarPago;
        private Guna2TextBox fieldMonto;
        private Guna2ComboBox fieldMetodoPago;
        private TableLayoutPanel layoutEncabezadosTabla;
        private Label fieldTituloMonto;
        private Label fieldTituloMetodo;
        private TableLayoutPanel layoutSuma;
        private Label symbolPeso1;
        private Label fieldSuma;
        private Label fieldTituloSuma;
        private Guna2Separator separador2;
        private TableLayoutPanel layoutPendiente;
        private Label symbolPeso2;
        private Label fieldPendiente;
        private Label fieldTituloPendiente;
        private TableLayoutPanel layoutDevolucion;
        private Label symbolPeso3;
        private Label fieldDevolucion;
        private Label lbTituloDevolucion;
        private Panel contenedorVistas;
        private Guna2ComboBox fieldTipoMoneda;
    }
}