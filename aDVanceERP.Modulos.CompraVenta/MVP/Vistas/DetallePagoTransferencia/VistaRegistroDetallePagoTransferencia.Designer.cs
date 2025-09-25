using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.DetallePagoTransferencia {
    partial class VistaRegistroDetallePagoTransferencia {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaRegistroDetallePagoTransferencia));
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
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldIcono = new PictureBox();
            fieldSubtitulo = new Label();
            layoutTitulo = new TableLayoutPanel();
            btnCerrar = new Guna2Button();
            fieldTitulo = new Label();
            layoutNumeros = new TableLayoutPanel();
            fieldNumeroTransaccion = new Guna2TextBox();
            fieldNumeroMovilConfirmacion = new Guna2TextBox();
            fieldTituloAlias = new Label();
            fieldAlias = new Guna2ComboBox();
            layoutQrDatos = new TableLayoutPanel();
            fieldCodigoQr = new PictureBox();
            layoutDatos = new TableLayoutPanel();
            fieldTituloNumeroMovilConfirmacionQR = new Label();
            fieldTituloAliasQR = new Label();
            fieldTituloTarjetaQR = new Label();
            fieldAliasQR = new Label();
            fieldTarjetaQR = new Label();
            fieldNumeroMovilConfirmacionQR = new Label();
            separador1 = new Guna2Separator();
            layoutRecordarNumeroConfirmacion = new TableLayoutPanel();
            fieldTituloRecordarNumeroConfirmacion = new Label();
            fieldRecordarNumeroConfirmacion = new Guna2CheckBox();
            layoutBotones = new TableLayoutPanel();
            btnSalir = new Guna2Button();
            btnRegistrar = new Guna2Button();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            layoutTitulo.SuspendLayout();
            layoutNumeros.SuspendLayout();
            layoutQrDatos.SuspendLayout();
            ((ISupportInitialize) fieldCodigoQr).BeginInit();
            layoutDatos.SuspendLayout();
            layoutRecordarNumeroConfirmacion.SuspendLayout();
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
            layoutVista.Controls.Add(layoutNumeros, 2, 7);
            layoutVista.Controls.Add(fieldTituloAlias, 2, 4);
            layoutVista.Controls.Add(fieldAlias, 2, 5);
            layoutVista.Controls.Add(layoutQrDatos, 2, 11);
            layoutVista.Controls.Add(separador1, 2, 10);
            layoutVista.Controls.Add(layoutRecordarNumeroConfirmacion, 2, 9);
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
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 145F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
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
            fieldTitulo.Text = "Transferencia bancaria";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutNumeros
            // 
            layoutNumeros.ColumnCount = 2;
            layoutNumeros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutNumeros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutNumeros.Controls.Add(fieldNumeroTransaccion, 0, 0);
            layoutNumeros.Controls.Add(fieldNumeroMovilConfirmacion, 0, 0);
            layoutNumeros.Dock = DockStyle.Fill;
            layoutNumeros.Location = new Point(50, 220);
            layoutNumeros.Margin = new Padding(0);
            layoutNumeros.Name = "layoutNumeros";
            layoutNumeros.RowCount = 1;
            layoutNumeros.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutNumeros.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutNumeros.Size = new Size(417, 45);
            layoutNumeros.TabIndex = 32;
            // 
            // fieldNumeroTransaccion
            // 
            fieldNumeroTransaccion.Animated = true;
            fieldNumeroTransaccion.BorderColor = Color.Gainsboro;
            fieldNumeroTransaccion.BorderRadius = 16;
            fieldNumeroTransaccion.Cursor = Cursors.IBeam;
            fieldNumeroTransaccion.CustomizableEdges = customizableEdges3;
            fieldNumeroTransaccion.DefaultText = "";
            fieldNumeroTransaccion.DisabledState.BorderColor = Color.White;
            fieldNumeroTransaccion.DisabledState.ForeColor = Color.DimGray;
            fieldNumeroTransaccion.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldNumeroTransaccion.Dock = DockStyle.Fill;
            fieldNumeroTransaccion.FocusedState.BorderColor = Color.SandyBrown;
            fieldNumeroTransaccion.Font = new Font("Segoe UI", 11.25F);
            fieldNumeroTransaccion.ForeColor = Color.Black;
            fieldNumeroTransaccion.HoverState.BorderColor = Color.SandyBrown;
            fieldNumeroTransaccion.IconLeft = (Image) resources.GetObject("fieldNumeroTransaccion.IconLeft");
            fieldNumeroTransaccion.IconLeftOffset = new Point(10, 0);
            fieldNumeroTransaccion.IconRightOffset = new Point(6, 0);
            fieldNumeroTransaccion.IconRightSize = new Size(12, 12);
            fieldNumeroTransaccion.Location = new Point(213, 5);
            fieldNumeroTransaccion.Margin = new Padding(5);
            fieldNumeroTransaccion.Name = "fieldNumeroTransaccion";
            fieldNumeroTransaccion.PasswordChar = '\0';
            fieldNumeroTransaccion.PlaceholderForeColor = Color.DimGray;
            fieldNumeroTransaccion.PlaceholderText = "Nro. de transacción";
            fieldNumeroTransaccion.SelectedText = "";
            fieldNumeroTransaccion.ShadowDecoration.CustomizableEdges = customizableEdges4;
            fieldNumeroTransaccion.Size = new Size(199, 35);
            fieldNumeroTransaccion.TabIndex = 3;
            fieldNumeroTransaccion.TextOffset = new Point(5, 0);
            // 
            // fieldNumeroMovilConfirmacion
            // 
            fieldNumeroMovilConfirmacion.Animated = true;
            fieldNumeroMovilConfirmacion.BorderColor = Color.Gainsboro;
            fieldNumeroMovilConfirmacion.BorderRadius = 16;
            fieldNumeroMovilConfirmacion.Cursor = Cursors.IBeam;
            fieldNumeroMovilConfirmacion.CustomizableEdges = customizableEdges5;
            fieldNumeroMovilConfirmacion.DefaultText = "";
            fieldNumeroMovilConfirmacion.DisabledState.BorderColor = Color.White;
            fieldNumeroMovilConfirmacion.DisabledState.ForeColor = Color.DimGray;
            fieldNumeroMovilConfirmacion.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldNumeroMovilConfirmacion.Dock = DockStyle.Fill;
            fieldNumeroMovilConfirmacion.FocusedState.BorderColor = Color.SandyBrown;
            fieldNumeroMovilConfirmacion.Font = new Font("Segoe UI", 11.25F);
            fieldNumeroMovilConfirmacion.ForeColor = Color.Black;
            fieldNumeroMovilConfirmacion.HoverState.BorderColor = Color.SandyBrown;
            fieldNumeroMovilConfirmacion.IconLeft = (Image) resources.GetObject("fieldNumeroMovilConfirmacion.IconLeft");
            fieldNumeroMovilConfirmacion.IconLeftOffset = new Point(10, 0);
            fieldNumeroMovilConfirmacion.IconRightOffset = new Point(6, 0);
            fieldNumeroMovilConfirmacion.IconRightSize = new Size(12, 12);
            fieldNumeroMovilConfirmacion.Location = new Point(5, 5);
            fieldNumeroMovilConfirmacion.Margin = new Padding(5);
            fieldNumeroMovilConfirmacion.Name = "fieldNumeroMovilConfirmacion";
            fieldNumeroMovilConfirmacion.PasswordChar = '\0';
            fieldNumeroMovilConfirmacion.PlaceholderForeColor = Color.DimGray;
            fieldNumeroMovilConfirmacion.PlaceholderText = "Nro. a confirmar";
            fieldNumeroMovilConfirmacion.SelectedText = "";
            fieldNumeroMovilConfirmacion.ShadowDecoration.CustomizableEdges = customizableEdges6;
            fieldNumeroMovilConfirmacion.Size = new Size(198, 35);
            fieldNumeroMovilConfirmacion.TabIndex = 2;
            fieldNumeroMovilConfirmacion.TextOffset = new Point(5, 0);
            // 
            // fieldTituloAlias
            // 
            fieldTituloAlias.Dock = DockStyle.Fill;
            fieldTituloAlias.Font = new Font("Segoe UI", 11.25F);
            fieldTituloAlias.ForeColor = Color.DimGray;
            fieldTituloAlias.Image = (Image) resources.GetObject("fieldTituloAlias.Image");
            fieldTituloAlias.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloAlias.ImeMode = ImeMode.NoControl;
            fieldTituloAlias.Location = new Point(65, 135);
            fieldTituloAlias.Margin = new Padding(15, 5, 3, 3);
            fieldTituloAlias.Name = "fieldTituloAlias";
            fieldTituloAlias.Size = new Size(399, 27);
            fieldTituloAlias.TabIndex = 33;
            fieldTituloAlias.Text = "      Alias o identificador de la tarjeta";
            fieldTituloAlias.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldAlias
            // 
            fieldAlias.Animated = true;
            fieldAlias.BackColor = Color.Transparent;
            fieldAlias.BorderColor = Color.Gainsboro;
            fieldAlias.BorderRadius = 16;
            fieldAlias.CustomizableEdges = customizableEdges7;
            fieldAlias.Dock = DockStyle.Fill;
            fieldAlias.DrawMode = DrawMode.OwnerDrawFixed;
            fieldAlias.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldAlias.FocusedColor = Color.SandyBrown;
            fieldAlias.FocusedState.BorderColor = Color.SandyBrown;
            fieldAlias.Font = new Font("Segoe UI", 11.25F);
            fieldAlias.ForeColor = Color.Black;
            fieldAlias.ItemHeight = 29;
            fieldAlias.Location = new Point(55, 170);
            fieldAlias.Margin = new Padding(5);
            fieldAlias.Name = "fieldAlias";
            fieldAlias.ShadowDecoration.CustomizableEdges = customizableEdges8;
            fieldAlias.Size = new Size(407, 35);
            fieldAlias.TabIndex = 34;
            fieldAlias.TextOffset = new Point(10, 0);
            // 
            // layoutQrDatos
            // 
            layoutQrDatos.ColumnCount = 2;
            layoutQrDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145F));
            layoutQrDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutQrDatos.Controls.Add(fieldCodigoQr, 0, 0);
            layoutQrDatos.Controls.Add(layoutDatos, 1, 0);
            layoutQrDatos.Dock = DockStyle.Fill;
            layoutQrDatos.Location = new Point(50, 345);
            layoutQrDatos.Margin = new Padding(0);
            layoutQrDatos.Name = "layoutQrDatos";
            layoutQrDatos.RowCount = 1;
            layoutQrDatos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutQrDatos.Size = new Size(417, 145);
            layoutQrDatos.TabIndex = 35;
            // 
            // fieldCodigoQr
            // 
            fieldCodigoQr.BackgroundImageLayout = ImageLayout.Center;
            fieldCodigoQr.Location = new Point(5, 5);
            fieldCodigoQr.Margin = new Padding(5);
            fieldCodigoQr.Name = "fieldCodigoQr";
            fieldCodigoQr.Size = new Size(135, 135);
            fieldCodigoQr.TabIndex = 15;
            fieldCodigoQr.TabStop = false;
            // 
            // layoutDatos
            // 
            layoutDatos.ColumnCount = 2;
            layoutDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 92F));
            layoutDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDatos.Controls.Add(fieldTituloNumeroMovilConfirmacionQR, 0, 3);
            layoutDatos.Controls.Add(fieldTituloAliasQR, 0, 1);
            layoutDatos.Controls.Add(fieldTituloTarjetaQR, 0, 2);
            layoutDatos.Controls.Add(fieldAliasQR, 1, 1);
            layoutDatos.Controls.Add(fieldTarjetaQR, 1, 2);
            layoutDatos.Controls.Add(fieldNumeroMovilConfirmacionQR, 1, 3);
            layoutDatos.Dock = DockStyle.Fill;
            layoutDatos.Location = new Point(145, 0);
            layoutDatos.Margin = new Padding(0);
            layoutDatos.Name = "layoutDatos";
            layoutDatos.RowCount = 5;
            layoutDatos.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutDatos.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutDatos.Size = new Size(272, 145);
            layoutDatos.TabIndex = 16;
            // 
            // fieldTituloNumeroMovilConfirmacionQR
            // 
            fieldTituloNumeroMovilConfirmacionQR.Dock = DockStyle.Fill;
            fieldTituloNumeroMovilConfirmacionQR.Font = new Font("Segoe UI", 11.25F);
            fieldTituloNumeroMovilConfirmacionQR.ForeColor = Color.DimGray;
            fieldTituloNumeroMovilConfirmacionQR.Image = (Image) resources.GetObject("fieldTituloNumeroMovilConfirmacionQR.Image");
            fieldTituloNumeroMovilConfirmacionQR.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloNumeroMovilConfirmacionQR.ImeMode = ImeMode.NoControl;
            fieldTituloNumeroMovilConfirmacionQR.Location = new Point(3, 92);
            fieldTituloNumeroMovilConfirmacionQR.Margin = new Padding(3, 5, 3, 3);
            fieldTituloNumeroMovilConfirmacionQR.Name = "fieldTituloNumeroMovilConfirmacionQR";
            fieldTituloNumeroMovilConfirmacionQR.Size = new Size(86, 22);
            fieldTituloNumeroMovilConfirmacionQR.TabIndex = 35;
            fieldTituloNumeroMovilConfirmacionQR.Text = "      Móvil :";
            fieldTituloNumeroMovilConfirmacionQR.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldTituloAliasQR
            // 
            fieldTituloAliasQR.Dock = DockStyle.Fill;
            fieldTituloAliasQR.Font = new Font("Segoe UI", 11.25F);
            fieldTituloAliasQR.ForeColor = Color.DimGray;
            fieldTituloAliasQR.Image = (Image) resources.GetObject("fieldTituloAliasQR.Image");
            fieldTituloAliasQR.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloAliasQR.ImeMode = ImeMode.NoControl;
            fieldTituloAliasQR.Location = new Point(3, 32);
            fieldTituloAliasQR.Margin = new Padding(3, 5, 3, 3);
            fieldTituloAliasQR.Name = "fieldTituloAliasQR";
            fieldTituloAliasQR.Size = new Size(86, 22);
            fieldTituloAliasQR.TabIndex = 34;
            fieldTituloAliasQR.Text = "      Alias :";
            fieldTituloAliasQR.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldTituloTarjetaQR
            // 
            fieldTituloTarjetaQR.Dock = DockStyle.Fill;
            fieldTituloTarjetaQR.Font = new Font("Segoe UI", 11.25F);
            fieldTituloTarjetaQR.ForeColor = Color.DimGray;
            fieldTituloTarjetaQR.Image = (Image) resources.GetObject("fieldTituloTarjetaQR.Image");
            fieldTituloTarjetaQR.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloTarjetaQR.ImeMode = ImeMode.NoControl;
            fieldTituloTarjetaQR.Location = new Point(3, 62);
            fieldTituloTarjetaQR.Margin = new Padding(3, 5, 3, 3);
            fieldTituloTarjetaQR.Name = "fieldTituloTarjetaQR";
            fieldTituloTarjetaQR.Size = new Size(86, 22);
            fieldTituloTarjetaQR.TabIndex = 36;
            fieldTituloTarjetaQR.Text = "      Tarjeta :";
            fieldTituloTarjetaQR.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldAliasQR
            // 
            fieldAliasQR.Dock = DockStyle.Fill;
            fieldAliasQR.Font = new Font("Segoe UI", 11.25F);
            fieldAliasQR.ForeColor = Color.Black;
            fieldAliasQR.ImeMode = ImeMode.NoControl;
            fieldAliasQR.Location = new Point(95, 30);
            fieldAliasQR.Margin = new Padding(3);
            fieldAliasQR.Name = "fieldAliasQR";
            fieldAliasQR.Size = new Size(174, 24);
            fieldAliasQR.TabIndex = 37;
            fieldAliasQR.Text = "-";
            fieldAliasQR.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTarjetaQR
            // 
            fieldTarjetaQR.Dock = DockStyle.Fill;
            fieldTarjetaQR.Font = new Font("Segoe UI", 11.25F);
            fieldTarjetaQR.ForeColor = Color.Black;
            fieldTarjetaQR.ImeMode = ImeMode.NoControl;
            fieldTarjetaQR.Location = new Point(95, 60);
            fieldTarjetaQR.Margin = new Padding(3);
            fieldTarjetaQR.Name = "fieldTarjetaQR";
            fieldTarjetaQR.Size = new Size(174, 24);
            fieldTarjetaQR.TabIndex = 38;
            fieldTarjetaQR.Text = "-";
            fieldTarjetaQR.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldNumeroMovilConfirmacionQR
            // 
            fieldNumeroMovilConfirmacionQR.Dock = DockStyle.Fill;
            fieldNumeroMovilConfirmacionQR.Font = new Font("Segoe UI", 11.25F);
            fieldNumeroMovilConfirmacionQR.ForeColor = Color.Black;
            fieldNumeroMovilConfirmacionQR.ImeMode = ImeMode.NoControl;
            fieldNumeroMovilConfirmacionQR.Location = new Point(95, 90);
            fieldNumeroMovilConfirmacionQR.Margin = new Padding(3);
            fieldNumeroMovilConfirmacionQR.Name = "fieldNumeroMovilConfirmacionQR";
            fieldNumeroMovilConfirmacionQR.Size = new Size(174, 24);
            fieldNumeroMovilConfirmacionQR.TabIndex = 39;
            fieldNumeroMovilConfirmacionQR.Text = "-";
            fieldNumeroMovilConfirmacionQR.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // separador1
            // 
            separador1.Dock = DockStyle.Fill;
            separador1.FillColor = Color.Gainsboro;
            separador1.Location = new Point(53, 328);
            separador1.Name = "separador1";
            separador1.Size = new Size(411, 14);
            separador1.TabIndex = 36;
            // 
            // layoutRecordarNumeroConfirmacion
            // 
            layoutRecordarNumeroConfirmacion.ColumnCount = 2;
            layoutRecordarNumeroConfirmacion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 26F));
            layoutRecordarNumeroConfirmacion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutRecordarNumeroConfirmacion.Controls.Add(fieldTituloRecordarNumeroConfirmacion, 1, 0);
            layoutRecordarNumeroConfirmacion.Controls.Add(fieldRecordarNumeroConfirmacion, 0, 0);
            layoutRecordarNumeroConfirmacion.Dock = DockStyle.Fill;
            layoutRecordarNumeroConfirmacion.Location = new Point(65, 275);
            layoutRecordarNumeroConfirmacion.Margin = new Padding(15, 0, 0, 0);
            layoutRecordarNumeroConfirmacion.Name = "layoutRecordarNumeroConfirmacion";
            layoutRecordarNumeroConfirmacion.RowCount = 1;
            layoutRecordarNumeroConfirmacion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutRecordarNumeroConfirmacion.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutRecordarNumeroConfirmacion.Size = new Size(402, 50);
            layoutRecordarNumeroConfirmacion.TabIndex = 37;
            // 
            // fieldTituloRecordarNumeroConfirmacion
            // 
            fieldTituloRecordarNumeroConfirmacion.Dock = DockStyle.Fill;
            fieldTituloRecordarNumeroConfirmacion.Font = new Font("Segoe UI", 11.25F);
            fieldTituloRecordarNumeroConfirmacion.ForeColor = Color.Black;
            fieldTituloRecordarNumeroConfirmacion.ImeMode = ImeMode.NoControl;
            fieldTituloRecordarNumeroConfirmacion.Location = new Point(31, 5);
            fieldTituloRecordarNumeroConfirmacion.Margin = new Padding(5, 5, 1, 1);
            fieldTituloRecordarNumeroConfirmacion.Name = "fieldTituloRecordarNumeroConfirmacion";
            fieldTituloRecordarNumeroConfirmacion.Size = new Size(370, 44);
            fieldTituloRecordarNumeroConfirmacion.TabIndex = 1;
            fieldTituloRecordarNumeroConfirmacion.Text = "Recordar el número de confirmación para futuras \r\ntransacciones.";
            // 
            // fieldRecordarNumeroConfirmacion
            // 
            fieldRecordarNumeroConfirmacion.BackColor = Color.White;
            fieldRecordarNumeroConfirmacion.CheckedState.BorderColor = Color.Gainsboro;
            fieldRecordarNumeroConfirmacion.CheckedState.BorderRadius = 4;
            fieldRecordarNumeroConfirmacion.CheckedState.BorderThickness = 1;
            fieldRecordarNumeroConfirmacion.CheckedState.FillColor = Color.WhiteSmoke;
            fieldRecordarNumeroConfirmacion.CheckMarkColor = Color.Black;
            fieldRecordarNumeroConfirmacion.Dock = DockStyle.Fill;
            fieldRecordarNumeroConfirmacion.Font = new Font("Segoe UI", 12F);
            fieldRecordarNumeroConfirmacion.Location = new Point(5, 5);
            fieldRecordarNumeroConfirmacion.Margin = new Padding(5, 5, 5, 20);
            fieldRecordarNumeroConfirmacion.Name = "fieldRecordarNumeroConfirmacion";
            fieldRecordarNumeroConfirmacion.Size = new Size(16, 25);
            fieldRecordarNumeroConfirmacion.TabIndex = 0;
            fieldRecordarNumeroConfirmacion.UncheckedState.BorderColor = Color.Gainsboro;
            fieldRecordarNumeroConfirmacion.UncheckedState.BorderRadius = 4;
            fieldRecordarNumeroConfirmacion.UncheckedState.BorderThickness = 1;
            fieldRecordarNumeroConfirmacion.UncheckedState.FillColor = Color.PeachPuff;
            fieldRecordarNumeroConfirmacion.UseVisualStyleBackColor = false;
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
            btnSalir.CustomizableEdges = customizableEdges9;
            btnSalir.Dock = DockStyle.Fill;
            btnSalir.FillColor = Color.White;
            btnSalir.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSalir.ForeColor = Color.Gainsboro;
            btnSalir.HoverState.BorderColor = Color.PeachPuff;
            btnSalir.HoverState.FillColor = Color.PeachPuff;
            btnSalir.HoverState.ForeColor = Color.Black;
            btnSalir.Location = new Point(302, 3);
            btnSalir.Name = "btnSalir";
            btnSalir.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnSalir.Size = new Size(160, 39);
            btnSalir.TabIndex = 14;
            btnSalir.Text = "Salir";
            // 
            // btnRegistrar
            // 
            btnRegistrar.Animated = true;
            btnRegistrar.BorderRadius = 18;
            btnRegistrar.CustomizableEdges = customizableEdges11;
            btnRegistrar.Dock = DockStyle.Fill;
            btnRegistrar.Enabled = false;
            btnRegistrar.FillColor = Color.PeachPuff;
            btnRegistrar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.Black;
            btnRegistrar.Location = new Point(53, 3);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnRegistrar.Size = new Size(243, 39);
            btnRegistrar.TabIndex = 15;
            btnRegistrar.Text = "Confirmar transferencia";
            // 
            // VistaRegistroDetallePagoTransferencia
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 685);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaRegistroDetallePagoTransferencia";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaRegistroDetallePagoTransferencia";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ((ISupportInitialize) fieldIcono).EndInit();
            layoutTitulo.ResumeLayout(false);
            layoutNumeros.ResumeLayout(false);
            layoutQrDatos.ResumeLayout(false);
            ((ISupportInitialize) fieldCodigoQr).EndInit();
            layoutDatos.ResumeLayout(false);
            layoutRecordarNumeroConfirmacion.ResumeLayout(false);
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
        private TableLayoutPanel layoutNumeros;
        private Label fieldTituloAlias;
        private Guna2ComboBox fieldAlias;
        private Guna2TextBox fieldNumeroTransaccion;
        private Guna2TextBox fieldNumeroMovilConfirmacion;
        private TableLayoutPanel layoutQrDatos;
        private PictureBox fieldCodigoQr;
        private TableLayoutPanel layoutDatos;
        private Label fieldTituloNumeroMovilConfirmacionQR;
        private Label fieldTituloAliasQR;
        private Label fieldTituloTarjetaQR;
        private Label fieldAliasQR;
        private Label fieldTarjetaQR;
        private Label fieldNumeroMovilConfirmacionQR;
        private Guna2Separator separador1;
        private TableLayoutPanel layoutRecordarNumeroConfirmacion;
        private Label fieldTituloRecordarNumeroConfirmacion;
        private Guna2CheckBox fieldRecordarNumeroConfirmacion;
    }
}