using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Empresa {
    partial class VistaRegistroEmpresa {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaRegistroEmpresa));
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
            fieldIcono = new PictureBox();
            fieldSubtitulo = new Label();
            fieldNombre = new Guna2TextBox();
            layoutTitulo = new TableLayoutPanel();
            btnCerrar = new Guna2Button();
            fieldTitulo = new Label();
            fieldCorreoElectronico = new Guna2TextBox();
            fieldTelefonoMovil = new Guna2TextBox();
            fieldTelefonoFijo = new Guna2TextBox();
            fieldDireccion = new Guna2TextBox();
            fieldTituloLogotipo = new Label();
            fieldLogotipo = new PictureBox();
            buscadorImagen = new OpenFileDialog();
            layoutBase.SuspendLayout();
            layoutBotones.SuspendLayout();
            layoutVista.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            layoutTitulo.SuspendLayout();
            ((ISupportInitialize) fieldLogotipo).BeginInit();
            SuspendLayout();
            // 
            // formatoBase
            // 
            formatoBase.AnimateWindow = true;
            formatoBase.AnimationType = Guna2BorderlessForm.AnimateWindowType.AW_CENTER;
            formatoBase.ContainerControl = this;
            formatoBase.DockIndicatorTransparencyValue = 0.6D;
            formatoBase.DragForm = false;
            formatoBase.HasFormShadow = false;
            formatoBase.TransparentWhileDrag = true;
            // 
            // layoutBase
            // 
            layoutBase.BackColor = Color.Gainsboro;
            layoutBase.ColumnCount = 3;
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 13F));
            layoutBase.Controls.Add(layoutBotones, 1, 2);
            layoutBase.Controls.Add(layoutVista, 1, 1);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 4;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutBase.Size = new Size(500, 657);
            layoutBase.TabIndex = 2;
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
            layoutBotones.Location = new Point(13, 587);
            layoutBotones.Margin = new Padding(3, 0, 0, 0);
            layoutBotones.Name = "layoutBotones";
            layoutBotones.RowCount = 2;
            layoutBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBotones.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutBotones.Size = new Size(474, 65);
            layoutBotones.TabIndex = 2;
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
            btnSalir.Location = new Point(294, 3);
            btnSalir.Name = "btnSalir";
            btnSalir.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnSalir.Size = new Size(154, 39);
            btnSalir.TabIndex = 14;
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
            btnRegistrar.Size = new Size(235, 39);
            btnRegistrar.TabIndex = 15;
            btnRegistrar.Text = "Registrar empresa";
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
            layoutVista.Controls.Add(fieldNombre, 2, 6);
            layoutVista.Controls.Add(layoutTitulo, 2, 1);
            layoutVista.Controls.Add(fieldCorreoElectronico, 2, 11);
            layoutVista.Controls.Add(fieldTelefonoMovil, 2, 8);
            layoutVista.Controls.Add(fieldTelefonoFijo, 2, 9);
            layoutVista.Controls.Add(fieldDireccion, 2, 13);
            layoutVista.Controls.Add(fieldTituloLogotipo, 2, 4);
            layoutVista.Controls.Add(fieldLogotipo, 2, 5);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(13, 5);
            layoutVista.Margin = new Padding(3, 0, 0, 0);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 15;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 102F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(474, 582);
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
            fieldSubtitulo.ForeColor = Color.Gray;
            fieldSubtitulo.ImeMode = ImeMode.NoControl;
            fieldSubtitulo.Location = new Point(55, 70);
            fieldSubtitulo.Margin = new Padding(5, 5, 1, 1);
            fieldSubtitulo.Name = "fieldSubtitulo";
            fieldSubtitulo.Size = new Size(398, 39);
            fieldSubtitulo.TabIndex = 0;
            fieldSubtitulo.Text = "Registro";
            // 
            // fieldNombre
            // 
            fieldNombre.Animated = true;
            fieldNombre.BorderColor = Color.Gainsboro;
            fieldNombre.BorderRadius = 16;
            fieldNombre.Cursor = Cursors.IBeam;
            fieldNombre.CustomizableEdges = customizableEdges5;
            fieldNombre.DefaultText = "";
            fieldNombre.DisabledState.BorderColor = Color.White;
            fieldNombre.DisabledState.ForeColor = Color.DimGray;
            fieldNombre.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldNombre.Dock = DockStyle.Fill;
            fieldNombre.FocusedState.BorderColor = Color.SandyBrown;
            fieldNombre.Font = new Font("Segoe UI", 11.25F);
            fieldNombre.ForeColor = Color.Black;
            fieldNombre.HoverState.BorderColor = Color.SandyBrown;
            fieldNombre.IconLeft = (Image) resources.GetObject("fieldNombre.IconLeft");
            fieldNombre.IconLeftOffset = new Point(10, 0);
            fieldNombre.Location = new Point(55, 272);
            fieldNombre.Margin = new Padding(5);
            fieldNombre.Name = "fieldNombre";
            fieldNombre.PasswordChar = '\0';
            fieldNombre.PlaceholderForeColor = Color.DimGray;
            fieldNombre.PlaceholderText = "Nombre de la empresa";
            fieldNombre.SelectedText = "";
            fieldNombre.ShadowDecoration.CustomizableEdges = customizableEdges6;
            fieldNombre.Size = new Size(394, 35);
            fieldNombre.TabIndex = 1;
            fieldNombre.TextOffset = new Point(5, 0);
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
            layoutTitulo.Size = new Size(404, 45);
            layoutTitulo.TabIndex = 14;
            // 
            // btnCerrar
            // 
            btnCerrar.Animated = true;
            btnCerrar.AutoRoundedCorners = true;
            btnCerrar.BorderColor = Color.Gray;
            btnCerrar.BorderRadius = 18;
            btnCerrar.CustomizableEdges = customizableEdges7;
            btnCerrar.Dock = DockStyle.Fill;
            btnCerrar.FillColor = Color.White;
            btnCerrar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.Black;
            btnCerrar.HoverState.FillColor = Color.White;
            btnCerrar.Image = (Image) resources.GetObject("btnCerrar.Image");
            btnCerrar.Location = new Point(357, 3);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.ShadowDecoration.CustomizableEdges = customizableEdges8;
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
            fieldTitulo.Size = new Size(348, 45);
            fieldTitulo.TabIndex = 0;
            fieldTitulo.Text = "Empresa";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldCorreoElectronico
            // 
            fieldCorreoElectronico.Animated = true;
            fieldCorreoElectronico.BorderColor = Color.Gainsboro;
            fieldCorreoElectronico.BorderRadius = 16;
            fieldCorreoElectronico.Cursor = Cursors.IBeam;
            fieldCorreoElectronico.CustomizableEdges = customizableEdges9;
            fieldCorreoElectronico.DefaultText = "";
            fieldCorreoElectronico.DisabledState.BorderColor = Color.White;
            fieldCorreoElectronico.DisabledState.ForeColor = Color.DimGray;
            fieldCorreoElectronico.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldCorreoElectronico.Dock = DockStyle.Fill;
            fieldCorreoElectronico.FocusedState.BorderColor = Color.SandyBrown;
            fieldCorreoElectronico.Font = new Font("Segoe UI", 11.25F);
            fieldCorreoElectronico.ForeColor = Color.Black;
            fieldCorreoElectronico.HoverState.BorderColor = Color.SandyBrown;
            fieldCorreoElectronico.IconLeft = (Image) resources.GetObject("fieldCorreoElectronico.IconLeft");
            fieldCorreoElectronico.IconLeftOffset = new Point(10, 0);
            fieldCorreoElectronico.Location = new Point(55, 423);
            fieldCorreoElectronico.Margin = new Padding(5);
            fieldCorreoElectronico.Name = "fieldCorreoElectronico";
            fieldCorreoElectronico.PasswordChar = '\0';
            fieldCorreoElectronico.PlaceholderForeColor = Color.DimGray;
            fieldCorreoElectronico.PlaceholderText = "Correo electrónico";
            fieldCorreoElectronico.SelectedText = "";
            fieldCorreoElectronico.ShadowDecoration.CustomizableEdges = customizableEdges10;
            fieldCorreoElectronico.Size = new Size(394, 35);
            fieldCorreoElectronico.TabIndex = 3;
            fieldCorreoElectronico.TextOffset = new Point(5, 0);
            // 
            // fieldTelefonoMovil
            // 
            fieldTelefonoMovil.Animated = true;
            fieldTelefonoMovil.BorderColor = Color.Gainsboro;
            fieldTelefonoMovil.BorderRadius = 16;
            fieldTelefonoMovil.Cursor = Cursors.IBeam;
            customizableEdges11.BottomLeft = false;
            customizableEdges11.BottomRight = false;
            fieldTelefonoMovil.CustomizableEdges = customizableEdges11;
            fieldTelefonoMovil.DefaultText = "";
            fieldTelefonoMovil.DisabledState.BorderColor = Color.White;
            fieldTelefonoMovil.DisabledState.ForeColor = Color.DimGray;
            fieldTelefonoMovil.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldTelefonoMovil.Dock = DockStyle.Fill;
            fieldTelefonoMovil.FocusedState.BorderColor = Color.SandyBrown;
            fieldTelefonoMovil.Font = new Font("Segoe UI", 11.25F);
            fieldTelefonoMovil.ForeColor = Color.Black;
            fieldTelefonoMovil.HoverState.BorderColor = Color.SandyBrown;
            fieldTelefonoMovil.IconLeft = (Image) resources.GetObject("fieldTelefonoMovil.IconLeft");
            fieldTelefonoMovil.IconLeftOffset = new Point(10, 0);
            fieldTelefonoMovil.Location = new Point(55, 326);
            fieldTelefonoMovil.Margin = new Padding(5, 4, 5, 2);
            fieldTelefonoMovil.Name = "fieldTelefonoMovil";
            fieldTelefonoMovil.PasswordChar = '\0';
            fieldTelefonoMovil.PlaceholderForeColor = Color.DimGray;
            fieldTelefonoMovil.PlaceholderText = "Teléfono móvil";
            fieldTelefonoMovil.SelectedText = "";
            fieldTelefonoMovil.ShadowDecoration.CustomizableEdges = customizableEdges12;
            fieldTelefonoMovil.Size = new Size(394, 37);
            fieldTelefonoMovil.TabIndex = 15;
            fieldTelefonoMovil.TextOffset = new Point(5, 0);
            // 
            // fieldTelefonoFijo
            // 
            fieldTelefonoFijo.Animated = true;
            fieldTelefonoFijo.BorderColor = Color.Gainsboro;
            fieldTelefonoFijo.BorderRadius = 16;
            fieldTelefonoFijo.Cursor = Cursors.IBeam;
            customizableEdges13.TopLeft = false;
            customizableEdges13.TopRight = false;
            fieldTelefonoFijo.CustomizableEdges = customizableEdges13;
            fieldTelefonoFijo.DefaultText = "";
            fieldTelefonoFijo.DisabledState.BorderColor = Color.White;
            fieldTelefonoFijo.DisabledState.ForeColor = Color.DimGray;
            fieldTelefonoFijo.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldTelefonoFijo.Dock = DockStyle.Fill;
            fieldTelefonoFijo.FocusedState.BorderColor = Color.SandyBrown;
            fieldTelefonoFijo.Font = new Font("Segoe UI", 11.25F);
            fieldTelefonoFijo.ForeColor = Color.Black;
            fieldTelefonoFijo.HoverState.BorderColor = Color.SandyBrown;
            fieldTelefonoFijo.IconLeft = (Image) resources.GetObject("fieldTelefonoFijo.IconLeft");
            fieldTelefonoFijo.IconLeftOffset = new Point(10, 0);
            fieldTelefonoFijo.Location = new Point(55, 367);
            fieldTelefonoFijo.Margin = new Padding(5, 2, 5, 4);
            fieldTelefonoFijo.Name = "fieldTelefonoFijo";
            fieldTelefonoFijo.PasswordChar = '\0';
            fieldTelefonoFijo.PlaceholderForeColor = Color.DimGray;
            fieldTelefonoFijo.PlaceholderText = "Teléfono fijo";
            fieldTelefonoFijo.SelectedText = "";
            fieldTelefonoFijo.ShadowDecoration.CustomizableEdges = customizableEdges14;
            fieldTelefonoFijo.Size = new Size(394, 37);
            fieldTelefonoFijo.TabIndex = 16;
            fieldTelefonoFijo.TextOffset = new Point(5, 0);
            // 
            // fieldDireccion
            // 
            fieldDireccion.Animated = true;
            fieldDireccion.BorderColor = Color.Gainsboro;
            fieldDireccion.BorderRadius = 16;
            fieldDireccion.Cursor = Cursors.IBeam;
            fieldDireccion.CustomizableEdges = customizableEdges15;
            fieldDireccion.DefaultText = "";
            fieldDireccion.DisabledState.BorderColor = Color.White;
            fieldDireccion.DisabledState.ForeColor = Color.DimGray;
            fieldDireccion.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldDireccion.Dock = DockStyle.Fill;
            fieldDireccion.FocusedState.BorderColor = Color.SandyBrown;
            fieldDireccion.Font = new Font("Segoe UI", 11.25F);
            fieldDireccion.ForeColor = Color.Black;
            fieldDireccion.HoverState.BorderColor = Color.SandyBrown;
            fieldDireccion.IconLeft = (Image) resources.GetObject("fieldDireccion.IconLeft");
            fieldDireccion.IconLeftOffset = new Point(10, -11);
            fieldDireccion.Location = new Point(55, 478);
            fieldDireccion.Margin = new Padding(5);
            fieldDireccion.Multiline = true;
            fieldDireccion.Name = "fieldDireccion";
            fieldDireccion.PasswordChar = '\0';
            fieldDireccion.PlaceholderForeColor = Color.DimGray;
            fieldDireccion.PlaceholderText = "Dirección";
            fieldDireccion.SelectedText = "";
            fieldDireccion.ShadowDecoration.CustomizableEdges = customizableEdges16;
            fieldDireccion.Size = new Size(394, 62);
            fieldDireccion.TabIndex = 21;
            fieldDireccion.TextOffset = new Point(5, 0);
            // 
            // fieldTituloLogotipo
            // 
            fieldTituloLogotipo.Dock = DockStyle.Fill;
            fieldTituloLogotipo.Font = new Font("Segoe UI", 11.25F);
            fieldTituloLogotipo.ForeColor = Color.DimGray;
            fieldTituloLogotipo.Image = (Image) resources.GetObject("fieldTituloLogotipo.Image");
            fieldTituloLogotipo.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloLogotipo.ImeMode = ImeMode.NoControl;
            fieldTituloLogotipo.Location = new Point(65, 135);
            fieldTituloLogotipo.Margin = new Padding(15, 5, 3, 3);
            fieldTituloLogotipo.Name = "fieldTituloLogotipo";
            fieldTituloLogotipo.Size = new Size(386, 27);
            fieldTituloLogotipo.TabIndex = 34;
            fieldTituloLogotipo.Text = "      Logotipo :";
            fieldTituloLogotipo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldLogotipo
            // 
            fieldLogotipo.BackgroundImage = Properties.Resources.logoF_96px;
            fieldLogotipo.BackgroundImageLayout = ImageLayout.None;
            fieldLogotipo.Dock = DockStyle.Left;
            fieldLogotipo.Location = new Point(51, 166);
            fieldLogotipo.Margin = new Padding(1);
            fieldLogotipo.Name = "fieldLogotipo";
            fieldLogotipo.Size = new Size(100, 100);
            fieldLogotipo.TabIndex = 35;
            fieldLogotipo.TabStop = false;
            // 
            // buscadorImagen
            // 
            buscadorImagen.Filter = "Archivos PNG|*.png|Archivos JPG|*.jpg|Archivos JPEG|*.jpeg";
            buscadorImagen.Title = "Escoger imagen para el logotipo de la empresa";
            // 
            // VistaRegistroEmpresa
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 657);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaRegistroEmpresa";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaRegistroEmpresa";
            layoutBase.ResumeLayout(false);
            layoutBotones.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ((ISupportInitialize) fieldIcono).EndInit();
            layoutTitulo.ResumeLayout(false);
            ((ISupportInitialize) fieldLogotipo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private PictureBox fieldIcono;
        private Label fieldSubtitulo;
        private Guna2TextBox fieldNombre;
        private TableLayoutPanel layoutTitulo;
        private Guna2Button btnCerrar;
        private Label fieldTitulo;
        private Guna2TextBox fieldCorreoElectronico;
        private TableLayoutPanel layoutBotones;
        private Guna2Button btnSalir;
        private Guna2Button btnRegistrar;
        private Guna2TextBox fieldTelefonoMovil;
        private Guna2TextBox fieldTelefonoFijo;
        private Guna2TextBox fieldDireccion;
        private Label fieldTituloLogotipo;
        private OpenFileDialog buscadorImagen;
        private PictureBox fieldLogotipo;
    }
}