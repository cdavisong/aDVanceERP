using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Proveedor {
    partial class VistaRegistroProveedor {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaRegistroProveedor));
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
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldIcono = new PictureBox();
            fieldSubtitulo = new Label();
            layoutTitulo = new TableLayoutPanel();
            btnCerrar = new Guna2Button();
            fieldTitulo = new Label();
            fieldNumero = new Guna2TextBox();
            fieldRazonSocial = new Guna2TextBox();
            fieldTelefonoMovil = new Guna2TextBox();
            fieldTelefonoFijo = new Guna2TextBox();
            fieldCorreoElectronico = new Guna2TextBox();
            fieldDireccion = new Guna2TextBox();
            layoutBotones = new TableLayoutPanel();
            btnSalir = new Guna2Button();
            btnRegistrar = new Guna2Button();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            layoutTitulo.SuspendLayout();
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
            layoutVista.Controls.Add(fieldNumero, 2, 6);
            layoutVista.Controls.Add(fieldRazonSocial, 2, 4);
            layoutVista.Controls.Add(fieldTelefonoMovil, 2, 8);
            layoutVista.Controls.Add(fieldTelefonoFijo, 2, 9);
            layoutVista.Controls.Add(fieldCorreoElectronico, 2, 11);
            layoutVista.Controls.Add(fieldDireccion, 2, 13);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(13, 0);
            layoutVista.Margin = new Padding(3, 0, 0, 0);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 15;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
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
            fieldSubtitulo.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldSubtitulo.ForeColor = Color.Gray;
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
            btnCerrar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
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
            fieldTitulo.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldTitulo.ForeColor = Color.Black;
            fieldTitulo.ImeMode = ImeMode.NoControl;
            fieldTitulo.Location = new Point(3, 0);
            fieldTitulo.Name = "fieldTitulo";
            fieldTitulo.Size = new Size(361, 45);
            fieldTitulo.TabIndex = 0;
            fieldTitulo.Text = "Proveedor";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldNumero
            // 
            fieldNumero.Animated = true;
            fieldNumero.BorderColor = Color.Gainsboro;
            fieldNumero.BorderRadius = 16;
            fieldNumero.Cursor = Cursors.IBeam;
            fieldNumero.CustomizableEdges = customizableEdges3;
            fieldNumero.DefaultText = "";
            fieldNumero.DisabledState.BorderColor = Color.White;
            fieldNumero.DisabledState.ForeColor = Color.DimGray;
            fieldNumero.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldNumero.Dock = DockStyle.Fill;
            fieldNumero.FocusedState.BorderColor = Color.SandyBrown;
            fieldNumero.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldNumero.ForeColor = Color.Black;
            fieldNumero.HoverState.BorderColor = Color.SandyBrown;
            fieldNumero.IconLeft = (Image) resources.GetObject("fieldNumero.IconLeft");
            fieldNumero.IconLeftOffset = new Point(10, 0);
            fieldNumero.Location = new Point(55, 190);
            fieldNumero.Margin = new Padding(5);
            fieldNumero.Name = "fieldNumero";
            fieldNumero.PasswordChar = '\0';
            fieldNumero.PlaceholderForeColor = Color.DimGray;
            fieldNumero.PlaceholderText = "Número de Identificación Tributaria (NIT)";
            fieldNumero.SelectedText = "";
            fieldNumero.ShadowDecoration.CustomizableEdges = customizableEdges4;
            fieldNumero.Size = new Size(407, 35);
            fieldNumero.TabIndex = 1;
            fieldNumero.TextOffset = new Point(5, 0);
            // 
            // fieldRazonSocial
            // 
            fieldRazonSocial.Animated = true;
            fieldRazonSocial.BorderColor = Color.Gainsboro;
            fieldRazonSocial.BorderRadius = 16;
            fieldRazonSocial.Cursor = Cursors.IBeam;
            fieldRazonSocial.CustomizableEdges = customizableEdges5;
            fieldRazonSocial.DefaultText = "";
            fieldRazonSocial.DisabledState.BorderColor = Color.White;
            fieldRazonSocial.DisabledState.ForeColor = Color.DimGray;
            fieldRazonSocial.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldRazonSocial.Dock = DockStyle.Fill;
            fieldRazonSocial.FocusedState.BorderColor = Color.SandyBrown;
            fieldRazonSocial.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldRazonSocial.ForeColor = Color.Black;
            fieldRazonSocial.HoverState.BorderColor = Color.SandyBrown;
            fieldRazonSocial.IconLeft = (Image) resources.GetObject("fieldRazonSocial.IconLeft");
            fieldRazonSocial.IconLeftOffset = new Point(10, 0);
            fieldRazonSocial.Location = new Point(55, 135);
            fieldRazonSocial.Margin = new Padding(5);
            fieldRazonSocial.Name = "fieldRazonSocial";
            fieldRazonSocial.PasswordChar = '\0';
            fieldRazonSocial.PlaceholderForeColor = Color.DimGray;
            fieldRazonSocial.PlaceholderText = "Razón social";
            fieldRazonSocial.SelectedText = "";
            fieldRazonSocial.ShadowDecoration.CustomizableEdges = customizableEdges6;
            fieldRazonSocial.Size = new Size(407, 35);
            fieldRazonSocial.TabIndex = 3;
            fieldRazonSocial.TextOffset = new Point(5, 0);
            // 
            // fieldTelefonoMovil
            // 
            fieldTelefonoMovil.Animated = true;
            fieldTelefonoMovil.BorderColor = Color.Gainsboro;
            fieldTelefonoMovil.BorderRadius = 16;
            fieldTelefonoMovil.Cursor = Cursors.IBeam;
            customizableEdges7.BottomLeft = false;
            customizableEdges7.BottomRight = false;
            fieldTelefonoMovil.CustomizableEdges = customizableEdges7;
            fieldTelefonoMovil.DefaultText = "";
            fieldTelefonoMovil.DisabledState.BorderColor = Color.White;
            fieldTelefonoMovil.DisabledState.ForeColor = Color.DimGray;
            fieldTelefonoMovil.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldTelefonoMovil.Dock = DockStyle.Fill;
            fieldTelefonoMovil.FocusedState.BorderColor = Color.SandyBrown;
            fieldTelefonoMovil.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldTelefonoMovil.ForeColor = Color.Black;
            fieldTelefonoMovil.HoverState.BorderColor = Color.SandyBrown;
            fieldTelefonoMovil.IconLeft = (Image) resources.GetObject("fieldTelefonoMovil.IconLeft");
            fieldTelefonoMovil.IconLeftOffset = new Point(10, 0);
            fieldTelefonoMovil.Location = new Point(55, 244);
            fieldTelefonoMovil.Margin = new Padding(5, 4, 5, 2);
            fieldTelefonoMovil.Name = "fieldTelefonoMovil";
            fieldTelefonoMovil.PasswordChar = '\0';
            fieldTelefonoMovil.PlaceholderForeColor = Color.DimGray;
            fieldTelefonoMovil.PlaceholderText = "Teléfono móvil";
            fieldTelefonoMovil.SelectedText = "";
            fieldTelefonoMovil.ShadowDecoration.CustomizableEdges = customizableEdges8;
            fieldTelefonoMovil.Size = new Size(407, 37);
            fieldTelefonoMovil.TabIndex = 26;
            fieldTelefonoMovil.TextOffset = new Point(5, 0);
            // 
            // fieldTelefonoFijo
            // 
            fieldTelefonoFijo.Animated = true;
            fieldTelefonoFijo.BorderColor = Color.Gainsboro;
            fieldTelefonoFijo.BorderRadius = 16;
            fieldTelefonoFijo.Cursor = Cursors.IBeam;
            customizableEdges9.TopLeft = false;
            customizableEdges9.TopRight = false;
            fieldTelefonoFijo.CustomizableEdges = customizableEdges9;
            fieldTelefonoFijo.DefaultText = "";
            fieldTelefonoFijo.DisabledState.BorderColor = Color.White;
            fieldTelefonoFijo.DisabledState.ForeColor = Color.DimGray;
            fieldTelefonoFijo.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldTelefonoFijo.Dock = DockStyle.Fill;
            fieldTelefonoFijo.FocusedState.BorderColor = Color.SandyBrown;
            fieldTelefonoFijo.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldTelefonoFijo.ForeColor = Color.Black;
            fieldTelefonoFijo.HoverState.BorderColor = Color.SandyBrown;
            fieldTelefonoFijo.IconLeft = (Image) resources.GetObject("fieldTelefonoFijo.IconLeft");
            fieldTelefonoFijo.IconLeftOffset = new Point(10, 0);
            fieldTelefonoFijo.Location = new Point(55, 285);
            fieldTelefonoFijo.Margin = new Padding(5, 2, 5, 4);
            fieldTelefonoFijo.Name = "fieldTelefonoFijo";
            fieldTelefonoFijo.PasswordChar = '\0';
            fieldTelefonoFijo.PlaceholderForeColor = Color.DimGray;
            fieldTelefonoFijo.PlaceholderText = "Teléfono fijo";
            fieldTelefonoFijo.SelectedText = "";
            fieldTelefonoFijo.ShadowDecoration.CustomizableEdges = customizableEdges10;
            fieldTelefonoFijo.Size = new Size(407, 37);
            fieldTelefonoFijo.TabIndex = 27;
            fieldTelefonoFijo.TextOffset = new Point(5, 0);
            // 
            // fieldCorreoElectronico
            // 
            fieldCorreoElectronico.Animated = true;
            fieldCorreoElectronico.BorderColor = Color.Gainsboro;
            fieldCorreoElectronico.BorderRadius = 16;
            fieldCorreoElectronico.Cursor = Cursors.IBeam;
            fieldCorreoElectronico.CustomizableEdges = customizableEdges11;
            fieldCorreoElectronico.DefaultText = "";
            fieldCorreoElectronico.DisabledState.BorderColor = Color.White;
            fieldCorreoElectronico.DisabledState.ForeColor = Color.DimGray;
            fieldCorreoElectronico.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldCorreoElectronico.Dock = DockStyle.Fill;
            fieldCorreoElectronico.FocusedState.BorderColor = Color.SandyBrown;
            fieldCorreoElectronico.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldCorreoElectronico.ForeColor = Color.Black;
            fieldCorreoElectronico.HoverState.BorderColor = Color.SandyBrown;
            fieldCorreoElectronico.IconLeft = (Image) resources.GetObject("fieldCorreoElectronico.IconLeft");
            fieldCorreoElectronico.IconLeftOffset = new Point(10, 0);
            fieldCorreoElectronico.Location = new Point(55, 341);
            fieldCorreoElectronico.Margin = new Padding(5);
            fieldCorreoElectronico.Name = "fieldCorreoElectronico";
            fieldCorreoElectronico.PasswordChar = '\0';
            fieldCorreoElectronico.PlaceholderForeColor = Color.DimGray;
            fieldCorreoElectronico.PlaceholderText = "Correo electrónico";
            fieldCorreoElectronico.SelectedText = "";
            fieldCorreoElectronico.ShadowDecoration.CustomizableEdges = customizableEdges12;
            fieldCorreoElectronico.Size = new Size(407, 35);
            fieldCorreoElectronico.TabIndex = 28;
            fieldCorreoElectronico.TextOffset = new Point(5, 0);
            // 
            // fieldDireccion
            // 
            fieldDireccion.Animated = true;
            fieldDireccion.BorderColor = Color.Gainsboro;
            fieldDireccion.BorderRadius = 16;
            fieldDireccion.Cursor = Cursors.IBeam;
            fieldDireccion.CustomizableEdges = customizableEdges13;
            fieldDireccion.DefaultText = "";
            fieldDireccion.DisabledState.BorderColor = Color.White;
            fieldDireccion.DisabledState.ForeColor = Color.DimGray;
            fieldDireccion.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldDireccion.Dock = DockStyle.Fill;
            fieldDireccion.FocusedState.BorderColor = Color.SandyBrown;
            fieldDireccion.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldDireccion.ForeColor = Color.Black;
            fieldDireccion.HoverState.BorderColor = Color.SandyBrown;
            fieldDireccion.IconLeft = (Image) resources.GetObject("fieldDireccion.IconLeft");
            fieldDireccion.IconLeftOffset = new Point(10, -11);
            fieldDireccion.Location = new Point(55, 396);
            fieldDireccion.Margin = new Padding(5);
            fieldDireccion.Multiline = true;
            fieldDireccion.Name = "fieldDireccion";
            fieldDireccion.PasswordChar = '\0';
            fieldDireccion.PlaceholderForeColor = Color.DimGray;
            fieldDireccion.PlaceholderText = "Dirección";
            fieldDireccion.SelectedText = "";
            fieldDireccion.ShadowDecoration.CustomizableEdges = customizableEdges14;
            fieldDireccion.Size = new Size(407, 62);
            fieldDireccion.TabIndex = 29;
            fieldDireccion.TextOffset = new Point(5, 0);
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
            layoutBotones.TabIndex = 3;
            // 
            // btnSalir
            // 
            btnSalir.Animated = true;
            btnSalir.BorderColor = Color.Gainsboro;
            btnSalir.BorderRadius = 18;
            btnSalir.BorderThickness = 1;
            btnSalir.CustomizableEdges = customizableEdges15;
            btnSalir.Dock = DockStyle.Fill;
            btnSalir.FillColor = Color.White;
            btnSalir.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnSalir.ForeColor = Color.Gainsboro;
            btnSalir.HoverState.BorderColor = Color.PeachPuff;
            btnSalir.HoverState.FillColor = Color.PeachPuff;
            btnSalir.HoverState.ForeColor = Color.Black;
            btnSalir.Location = new Point(302, 3);
            btnSalir.Name = "btnSalir";
            btnSalir.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnSalir.Size = new Size(160, 39);
            btnSalir.TabIndex = 14;
            btnSalir.Text = "Salir";
            // 
            // btnRegistrar
            // 
            btnRegistrar.Animated = true;
            btnRegistrar.BorderRadius = 18;
            btnRegistrar.CustomizableEdges = customizableEdges17;
            btnRegistrar.Dock = DockStyle.Fill;
            btnRegistrar.FillColor = Color.PeachPuff;
            btnRegistrar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnRegistrar.ForeColor = Color.Black;
            btnRegistrar.Location = new Point(53, 3);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.ShadowDecoration.CustomizableEdges = customizableEdges18;
            btnRegistrar.Size = new Size(243, 39);
            btnRegistrar.TabIndex = 15;
            btnRegistrar.Text = "Crear proveedor";
            // 
            // VistaRegistroProveedor
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 685);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaRegistroProveedor";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaRegistroProveedor";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ((ISupportInitialize) fieldIcono).EndInit();
            layoutTitulo.ResumeLayout(false);
            layoutBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private PictureBox fieldIcono;
        private Label fieldSubtitulo;
        private Guna2TextBox fieldNumero;
        private TableLayoutPanel layoutTitulo;
        private Guna2Button btnCerrar;
        private Label fieldTitulo;
        private Guna2TextBox fieldRazonSocial;
        private TableLayoutPanel layoutBotones;
        private Guna2Button btnSalir;
        private Guna2Button btnRegistrar;
        private Guna2TextBox fieldTelefonoMovil;
        private Guna2TextBox fieldTelefonoFijo;
        private Guna2TextBox fieldCorreoElectronico;
        private Guna2TextBox fieldDireccion;
    }
}