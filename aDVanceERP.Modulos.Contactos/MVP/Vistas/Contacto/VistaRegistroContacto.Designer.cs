using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Contacto {
    partial class VistaRegistroContacto {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaRegistroContacto));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges31 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges32 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges29 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges30 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges33 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges34 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges35 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges36 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldIcono = new PictureBox();
            fieldSubtitulo = new Label();
            fieldNombreUsuario = new Guna2TextBox();
            layoutTitulo = new TableLayoutPanel();
            btnCerrar = new Guna2Button();
            fieldTitulo = new Label();
            fieldCorreoElectronico = new Guna2TextBox();
            fieldTelefonoMovil = new Guna2TextBox();
            fieldTelefonoFijo = new Guna2TextBox();
            fieldDireccion = new Guna2TextBox();
            fieldNotas = new Guna2TextBox();
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
            layoutVista.Controls.Add(fieldNombreUsuario, 2, 4);
            layoutVista.Controls.Add(layoutTitulo, 2, 1);
            layoutVista.Controls.Add(fieldCorreoElectronico, 2, 9);
            layoutVista.Controls.Add(fieldTelefonoMovil, 2, 6);
            layoutVista.Controls.Add(fieldTelefonoFijo, 2, 7);
            layoutVista.Controls.Add(fieldDireccion, 2, 11);
            layoutVista.Controls.Add(fieldNotas, 2, 13);
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
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
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
            // fieldNombreUsuario
            // 
            fieldNombreUsuario.Animated = true;
            fieldNombreUsuario.BorderColor = Color.Gainsboro;
            fieldNombreUsuario.BorderRadius = 16;
            fieldNombreUsuario.Cursor = Cursors.IBeam;
            fieldNombreUsuario.CustomizableEdges = customizableEdges19;
            fieldNombreUsuario.DefaultText = "";
            fieldNombreUsuario.DisabledState.BorderColor = Color.White;
            fieldNombreUsuario.DisabledState.ForeColor = Color.DimGray;
            fieldNombreUsuario.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldNombreUsuario.Dock = DockStyle.Fill;
            fieldNombreUsuario.FocusedState.BorderColor = Color.SandyBrown;
            fieldNombreUsuario.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldNombreUsuario.ForeColor = Color.Black;
            fieldNombreUsuario.HoverState.BorderColor = Color.SandyBrown;
            fieldNombreUsuario.IconLeft = (Image) resources.GetObject("fieldNombreUsuario.IconLeft");
            fieldNombreUsuario.IconLeftOffset = new Point(10, 0);
            fieldNombreUsuario.Location = new Point(55, 135);
            fieldNombreUsuario.Margin = new Padding(5);
            fieldNombreUsuario.Name = "fieldNombreUsuario";
            fieldNombreUsuario.PasswordChar = '\0';
            fieldNombreUsuario.PlaceholderForeColor = Color.DimGray;
            fieldNombreUsuario.PlaceholderText = "Nombre del contacto";
            fieldNombreUsuario.SelectedText = "";
            fieldNombreUsuario.ShadowDecoration.CustomizableEdges = customizableEdges20;
            fieldNombreUsuario.Size = new Size(407, 35);
            fieldNombreUsuario.TabIndex = 1;
            fieldNombreUsuario.TextOffset = new Point(5, 0);
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
            btnCerrar.CustomizableEdges = customizableEdges31;
            btnCerrar.Dock = DockStyle.Fill;
            btnCerrar.FillColor = Color.White;
            btnCerrar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnCerrar.ForeColor = Color.Black;
            btnCerrar.HoverState.FillColor = Color.White;
            btnCerrar.Image = (Image) resources.GetObject("btnCerrar.Image");
            btnCerrar.Location = new Point(370, 3);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.ShadowDecoration.CustomizableEdges = customizableEdges32;
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
            fieldTitulo.Text = "Contacto";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldCorreoElectronico
            // 
            fieldCorreoElectronico.Animated = true;
            fieldCorreoElectronico.BorderColor = Color.Gainsboro;
            fieldCorreoElectronico.BorderRadius = 16;
            fieldCorreoElectronico.Cursor = Cursors.IBeam;
            fieldCorreoElectronico.CustomizableEdges = customizableEdges21;
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
            fieldCorreoElectronico.Location = new Point(55, 286);
            fieldCorreoElectronico.Margin = new Padding(5);
            fieldCorreoElectronico.Name = "fieldCorreoElectronico";
            fieldCorreoElectronico.PasswordChar = '\0';
            fieldCorreoElectronico.PlaceholderForeColor = Color.DimGray;
            fieldCorreoElectronico.PlaceholderText = "Correo electrónico";
            fieldCorreoElectronico.SelectedText = "";
            fieldCorreoElectronico.ShadowDecoration.CustomizableEdges = customizableEdges22;
            fieldCorreoElectronico.Size = new Size(407, 35);
            fieldCorreoElectronico.TabIndex = 3;
            fieldCorreoElectronico.TextOffset = new Point(5, 0);
            // 
            // fieldTelefonoMovil
            // 
            fieldTelefonoMovil.Animated = true;
            fieldTelefonoMovil.BorderColor = Color.Gainsboro;
            fieldTelefonoMovil.BorderRadius = 16;
            fieldTelefonoMovil.Cursor = Cursors.IBeam;
            customizableEdges23.BottomLeft = false;
            customizableEdges23.BottomRight = false;
            fieldTelefonoMovil.CustomizableEdges = customizableEdges23;
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
            fieldTelefonoMovil.Location = new Point(55, 189);
            fieldTelefonoMovil.Margin = new Padding(5, 4, 5, 2);
            fieldTelefonoMovil.Name = "fieldTelefonoMovil";
            fieldTelefonoMovil.PasswordChar = '\0';
            fieldTelefonoMovil.PlaceholderForeColor = Color.DimGray;
            fieldTelefonoMovil.PlaceholderText = "Teléfono móvil";
            fieldTelefonoMovil.SelectedText = "";
            fieldTelefonoMovil.ShadowDecoration.CustomizableEdges = customizableEdges24;
            fieldTelefonoMovil.Size = new Size(407, 37);
            fieldTelefonoMovil.TabIndex = 15;
            fieldTelefonoMovil.TextOffset = new Point(5, 0);
            // 
            // fieldTelefonoFijo
            // 
            fieldTelefonoFijo.Animated = true;
            fieldTelefonoFijo.BorderColor = Color.Gainsboro;
            fieldTelefonoFijo.BorderRadius = 16;
            fieldTelefonoFijo.Cursor = Cursors.IBeam;
            customizableEdges25.TopLeft = false;
            customizableEdges25.TopRight = false;
            fieldTelefonoFijo.CustomizableEdges = customizableEdges25;
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
            fieldTelefonoFijo.Location = new Point(55, 230);
            fieldTelefonoFijo.Margin = new Padding(5, 2, 5, 4);
            fieldTelefonoFijo.Name = "fieldTelefonoFijo";
            fieldTelefonoFijo.PasswordChar = '\0';
            fieldTelefonoFijo.PlaceholderForeColor = Color.DimGray;
            fieldTelefonoFijo.PlaceholderText = "Teléfono fijo";
            fieldTelefonoFijo.SelectedText = "";
            fieldTelefonoFijo.ShadowDecoration.CustomizableEdges = customizableEdges26;
            fieldTelefonoFijo.Size = new Size(407, 37);
            fieldTelefonoFijo.TabIndex = 16;
            fieldTelefonoFijo.TextOffset = new Point(5, 0);
            // 
            // fieldDireccion
            // 
            fieldDireccion.Animated = true;
            fieldDireccion.BorderColor = Color.Gainsboro;
            fieldDireccion.BorderRadius = 16;
            fieldDireccion.Cursor = Cursors.IBeam;
            fieldDireccion.CustomizableEdges = customizableEdges27;
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
            fieldDireccion.Location = new Point(55, 341);
            fieldDireccion.Margin = new Padding(5);
            fieldDireccion.Multiline = true;
            fieldDireccion.Name = "fieldDireccion";
            fieldDireccion.PasswordChar = '\0';
            fieldDireccion.PlaceholderForeColor = Color.DimGray;
            fieldDireccion.PlaceholderText = "Dirección";
            fieldDireccion.SelectedText = "";
            fieldDireccion.ShadowDecoration.CustomizableEdges = customizableEdges28;
            fieldDireccion.Size = new Size(407, 62);
            fieldDireccion.TabIndex = 21;
            fieldDireccion.TextOffset = new Point(5, 0);
            // 
            // fieldNotas
            // 
            fieldNotas.Animated = true;
            fieldNotas.BorderColor = Color.Gainsboro;
            fieldNotas.BorderRadius = 16;
            fieldNotas.Cursor = Cursors.IBeam;
            fieldNotas.CustomizableEdges = customizableEdges29;
            fieldNotas.DefaultText = "";
            fieldNotas.DisabledState.BorderColor = Color.White;
            fieldNotas.DisabledState.ForeColor = Color.DimGray;
            fieldNotas.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldNotas.Dock = DockStyle.Fill;
            fieldNotas.FocusedState.BorderColor = Color.SandyBrown;
            fieldNotas.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldNotas.ForeColor = Color.Black;
            fieldNotas.HoverState.BorderColor = Color.SandyBrown;
            fieldNotas.IconLeft = (Image) resources.GetObject("fieldNotas.IconLeft");
            fieldNotas.IconLeftOffset = new Point(10, -11);
            fieldNotas.Location = new Point(55, 423);
            fieldNotas.Margin = new Padding(5);
            fieldNotas.Multiline = true;
            fieldNotas.Name = "fieldNotas";
            fieldNotas.PasswordChar = '\0';
            fieldNotas.PlaceholderForeColor = Color.DimGray;
            fieldNotas.PlaceholderText = "Notas";
            fieldNotas.SelectedText = "";
            fieldNotas.ShadowDecoration.CustomizableEdges = customizableEdges30;
            fieldNotas.Size = new Size(407, 62);
            fieldNotas.TabIndex = 22;
            fieldNotas.TextOffset = new Point(5, 0);
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
            layoutBotones.TabIndex = 2;
            // 
            // btnSalir
            // 
            btnSalir.Animated = true;
            btnSalir.BorderColor = Color.Gainsboro;
            btnSalir.BorderRadius = 18;
            btnSalir.BorderThickness = 1;
            btnSalir.CustomizableEdges = customizableEdges33;
            btnSalir.Dock = DockStyle.Fill;
            btnSalir.FillColor = Color.White;
            btnSalir.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnSalir.ForeColor = Color.Gainsboro;
            btnSalir.HoverState.BorderColor = Color.PeachPuff;
            btnSalir.HoverState.FillColor = Color.PeachPuff;
            btnSalir.HoverState.ForeColor = Color.Black;
            btnSalir.Location = new Point(302, 3);
            btnSalir.Name = "btnSalir";
            btnSalir.ShadowDecoration.CustomizableEdges = customizableEdges34;
            btnSalir.Size = new Size(160, 39);
            btnSalir.TabIndex = 14;
            btnSalir.Text = "Salir";
            // 
            // btnRegistrar
            // 
            btnRegistrar.Animated = true;
            btnRegistrar.BorderRadius = 18;
            btnRegistrar.CustomizableEdges = customizableEdges35;
            btnRegistrar.Dock = DockStyle.Fill;
            btnRegistrar.FillColor = Color.PeachPuff;
            btnRegistrar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnRegistrar.ForeColor = Color.Black;
            btnRegistrar.Location = new Point(53, 3);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.ShadowDecoration.CustomizableEdges = customizableEdges36;
            btnRegistrar.Size = new Size(243, 39);
            btnRegistrar.TabIndex = 15;
            btnRegistrar.Text = "Registrar contacto";
            // 
            // VistaRegistroContacto
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 685);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaRegistroContacto";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaRegistroContacto";
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
        private Guna2TextBox fieldNombreUsuario;
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
        private Guna2TextBox fieldNotas;
    }
}