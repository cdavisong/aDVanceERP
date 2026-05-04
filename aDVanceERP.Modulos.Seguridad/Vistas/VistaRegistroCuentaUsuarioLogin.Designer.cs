using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Seguridad.Vistas {
    partial class VistaRegistroCuentaUsuarioLogin {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            formatoBase = new Guna2BorderlessForm(components);
            panelDatos = new Guna2Panel();
            layoutDatos = new TableLayoutPanel();
            layoutDistBase = new TableLayoutPanel();
            pbBannerTitulo = new PictureBox();
            fieldTituloNombreUsuario = new Label();
            fieldNombreUsuario = new Guna2TextBox();
            btnRegistrarCuentaUsuario = new Guna2Button();
            fieldCopyright = new Label();
            fieldPassword = new Guna2TextBox();
            layoutRegistroCuenta = new TableLayoutPanel();
            btnIniciarSesion = new Label();
            fieldTextoCuentaUsuario = new Label();
            fieldConfirmarPassword = new Guna2TextBox();
            fieldTituloNombreCorreo = new Label();
            fieldCorreoElectronico = new Guna2TextBox();
            layoutNombreUsuario = new FlowLayoutPanel();
            lbRequired1 = new Label();
            layoutPassword = new FlowLayoutPanel();
            lbRequired2 = new Label();
            fieldTituloPassword = new Label();
            panelDatos.SuspendLayout();
            layoutDatos.SuspendLayout();
            layoutDistBase.SuspendLayout();
            ((ISupportInitialize) pbBannerTitulo).BeginInit();
            layoutRegistroCuenta.SuspendLayout();
            layoutNombreUsuario.SuspendLayout();
            layoutPassword.SuspendLayout();
            SuspendLayout();
            // 
            // formatoBase
            // 
            formatoBase.AnimationType = Guna2BorderlessForm.AnimateWindowType.AW_HOR_NEGATIVE;
            formatoBase.ContainerControl = this;
            formatoBase.DockIndicatorTransparencyValue = 0.6D;
            formatoBase.DragForm = false;
            formatoBase.HasFormShadow = false;
            formatoBase.TransparentWhileDrag = true;
            // 
            // panelDatos
            // 
            panelDatos.BackColor = Color.Transparent;
            panelDatos.BorderColor = Color.Gainsboro;
            panelDatos.BorderRadius = 16;
            panelDatos.BorderThickness = 1;
            panelDatos.Controls.Add(layoutDatos);
            customizableEdges11.BottomLeft = false;
            customizableEdges11.BottomRight = false;
            panelDatos.CustomizableEdges = customizableEdges11;
            panelDatos.Dock = DockStyle.Fill;
            panelDatos.FillColor = Color.White;
            panelDatos.Location = new Point(0, 0);
            panelDatos.Margin = new Padding(0);
            panelDatos.Name = "panelDatos";
            panelDatos.ShadowDecoration.BorderRadius = 8;
            panelDatos.ShadowDecoration.CustomizableEdges = customizableEdges12;
            panelDatos.ShadowDecoration.Depth = 10;
            panelDatos.Size = new Size(500, 685);
            panelDatos.TabIndex = 57;
            // 
            // layoutDatos
            // 
            layoutDatos.BackColor = Color.Transparent;
            layoutDatos.ColumnCount = 3;
            layoutDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 25F));
            layoutDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 25F));
            layoutDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDatos.Controls.Add(layoutDistBase, 1, 1);
            layoutDatos.Dock = DockStyle.Fill;
            layoutDatos.Location = new Point(0, 0);
            layoutDatos.Name = "layoutDatos";
            layoutDatos.RowCount = 3;
            layoutDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutDatos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutDatos.Size = new Size(500, 685);
            layoutDatos.TabIndex = 0;
            // 
            // layoutDistBase
            // 
            layoutDistBase.ColumnCount = 1;
            layoutDistBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistBase.Controls.Add(pbBannerTitulo, 0, 1);
            layoutDistBase.Controls.Add(fieldNombreUsuario, 0, 4);
            layoutDistBase.Controls.Add(layoutPassword, 0, 9);
            layoutDistBase.Controls.Add(btnRegistrarCuentaUsuario, 0, 14);
            layoutDistBase.Controls.Add(fieldCopyright, 0, 18);
            layoutDistBase.Controls.Add(fieldPassword, 0, 10);
            layoutDistBase.Controls.Add(layoutRegistroCuenta, 0, 16);
            layoutDistBase.Controls.Add(fieldConfirmarPassword, 0, 12);
            layoutDistBase.Controls.Add(fieldTituloNombreCorreo, 0, 6);
            layoutDistBase.Controls.Add(fieldCorreoElectronico, 0, 7);
            layoutDistBase.Controls.Add(layoutNombreUsuario, 0, 3);
            layoutDistBase.Dock = DockStyle.Fill;
            layoutDistBase.Location = new Point(25, 5);
            layoutDistBase.Margin = new Padding(0);
            layoutDistBase.Name = "layoutDistBase";
            layoutDistBase.RowCount = 20;
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 160F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistBase.Size = new Size(450, 675);
            layoutDistBase.TabIndex = 0;
            // 
            // pbBannerTitulo
            // 
            pbBannerTitulo.Dock = DockStyle.Fill;
            pbBannerTitulo.Location = new Point(0, 20);
            pbBannerTitulo.Margin = new Padding(0);
            pbBannerTitulo.Name = "pbBannerTitulo";
            pbBannerTitulo.Size = new Size(450, 160);
            pbBannerTitulo.TabIndex = 0;
            pbBannerTitulo.TabStop = false;
            // 
            // fieldTituloNombreUsuario
            // 
            fieldTituloNombreUsuario.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldTituloNombreUsuario.ForeColor = Color.Gray;
            fieldTituloNombreUsuario.ImeMode = ImeMode.NoControl;
            fieldTituloNombreUsuario.Location = new Point(9, 1);
            fieldTituloNombreUsuario.Margin = new Padding(0, 1, 1, 1);
            fieldTituloNombreUsuario.Name = "fieldTituloNombreUsuario";
            fieldTituloNombreUsuario.Size = new Size(133, 23);
            fieldTituloNombreUsuario.TabIndex = 49;
            fieldTituloNombreUsuario.Text = "NOMBRE DE USUARIO";
            fieldTituloNombreUsuario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldNombreUsuario
            // 
            fieldNombreUsuario.Animated = true;
            fieldNombreUsuario.BorderColor = Color.Gainsboro;
            fieldNombreUsuario.BorderRadius = 16;
            fieldNombreUsuario.Cursor = Cursors.IBeam;
            fieldNombreUsuario.CustomizableEdges = customizableEdges1;
            fieldNombreUsuario.DefaultText = "";
            fieldNombreUsuario.DisabledState.BorderColor = Color.White;
            fieldNombreUsuario.DisabledState.ForeColor = Color.DimGray;
            fieldNombreUsuario.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldNombreUsuario.Dock = DockStyle.Fill;
            fieldNombreUsuario.FocusedState.BorderColor = Color.SandyBrown;
            fieldNombreUsuario.Font = new Font("Segoe UI", 11.25F);
            fieldNombreUsuario.ForeColor = Color.Black;
            fieldNombreUsuario.HoverState.BorderColor = Color.SandyBrown;
            fieldNombreUsuario.IconLeftOffset = new Point(10, 0);
            fieldNombreUsuario.Location = new Point(5, 230);
            fieldNombreUsuario.Margin = new Padding(5);
            fieldNombreUsuario.Name = "fieldNombreUsuario";
            fieldNombreUsuario.PasswordChar = '\0';
            fieldNombreUsuario.PlaceholderForeColor = Color.DimGray;
            fieldNombreUsuario.PlaceholderText = "usuario";
            fieldNombreUsuario.SelectedText = "";
            fieldNombreUsuario.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fieldNombreUsuario.Size = new Size(440, 35);
            fieldNombreUsuario.TabIndex = 58;
            fieldNombreUsuario.TextOffset = new Point(5, -1);
            // 
            // btnRegistrarCuentaUsuario
            // 
            btnRegistrarCuentaUsuario.Animated = true;
            btnRegistrarCuentaUsuario.BorderRadius = 18;
            btnRegistrarCuentaUsuario.CustomizableEdges = customizableEdges3;
            btnRegistrarCuentaUsuario.Dock = DockStyle.Fill;
            btnRegistrarCuentaUsuario.FillColor = Color.PeachPuff;
            btnRegistrarCuentaUsuario.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point,  0);
            btnRegistrarCuentaUsuario.ForeColor = Color.Black;
            btnRegistrarCuentaUsuario.Location = new Point(5, 497);
            btnRegistrarCuentaUsuario.Margin = new Padding(5, 2, 5, 2);
            btnRegistrarCuentaUsuario.Name = "btnRegistrarCuentaUsuario";
            btnRegistrarCuentaUsuario.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnRegistrarCuentaUsuario.Size = new Size(440, 41);
            btnRegistrarCuentaUsuario.TabIndex = 60;
            btnRegistrarCuentaUsuario.Text = "Registrarse";
            // 
            // fieldCopyright
            // 
            fieldCopyright.Dock = DockStyle.Fill;
            fieldCopyright.Font = new Font("Segoe UI", 9.75F);
            fieldCopyright.ForeColor = Color.DarkGray;
            fieldCopyright.ImeMode = ImeMode.NoControl;
            fieldCopyright.Location = new Point(3, 605);
            fieldCopyright.Name = "fieldCopyright";
            fieldCopyright.Size = new Size(444, 50);
            fieldCopyright.TabIndex = 63;
            fieldCopyright.Text = "Copyright 2025© aDVance ERP®";
            fieldCopyright.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldPassword
            // 
            fieldPassword.Animated = true;
            fieldPassword.BorderColor = Color.Gainsboro;
            fieldPassword.BorderRadius = 16;
            fieldPassword.Cursor = Cursors.IBeam;
            fieldPassword.CustomizableEdges = customizableEdges5;
            fieldPassword.DefaultText = "";
            fieldPassword.DisabledState.BorderColor = Color.White;
            fieldPassword.DisabledState.ForeColor = Color.DimGray;
            fieldPassword.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldPassword.Dock = DockStyle.Fill;
            fieldPassword.FocusedState.BorderColor = Color.SandyBrown;
            fieldPassword.Font = new Font("Segoe UI", 11.25F);
            fieldPassword.ForeColor = Color.Black;
            fieldPassword.HoverState.BorderColor = Color.SandyBrown;
            fieldPassword.IconLeftOffset = new Point(10, 0);
            fieldPassword.IconRight = Properties.Resources.closed_eye_20px;
            fieldPassword.IconRightOffset = new Point(10, 0);
            fieldPassword.Location = new Point(5, 390);
            fieldPassword.Margin = new Padding(5);
            fieldPassword.Name = "fieldPassword";
            fieldPassword.PasswordChar = '●';
            fieldPassword.PlaceholderForeColor = Color.DimGray;
            fieldPassword.PlaceholderText = "●●●●●●●●";
            fieldPassword.SelectedText = "";
            fieldPassword.ShadowDecoration.CustomizableEdges = customizableEdges6;
            fieldPassword.Size = new Size(440, 35);
            fieldPassword.TabIndex = 59;
            fieldPassword.TextOffset = new Point(5, -1);
            // 
            // layoutRegistroCuenta
            // 
            layoutRegistroCuenta.ColumnCount = 2;
            layoutRegistroCuenta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.77778F));
            layoutRegistroCuenta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.22222F));
            layoutRegistroCuenta.Controls.Add(btnIniciarSesion, 1, 0);
            layoutRegistroCuenta.Controls.Add(fieldTextoCuentaUsuario, 0, 0);
            layoutRegistroCuenta.Dock = DockStyle.Fill;
            layoutRegistroCuenta.Location = new Point(0, 550);
            layoutRegistroCuenta.Margin = new Padding(0);
            layoutRegistroCuenta.Name = "layoutRegistroCuenta";
            layoutRegistroCuenta.RowCount = 1;
            layoutRegistroCuenta.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutRegistroCuenta.Size = new Size(450, 35);
            layoutRegistroCuenta.TabIndex = 62;
            // 
            // btnIniciarSesion
            // 
            btnIniciarSesion.Cursor = Cursors.Hand;
            btnIniciarSesion.Dock = DockStyle.Left;
            btnIniciarSesion.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point,  0);
            btnIniciarSesion.ForeColor = Color.Firebrick;
            btnIniciarSesion.ImeMode = ImeMode.NoControl;
            btnIniciarSesion.Location = new Point(242, 5);
            btnIniciarSesion.Margin = new Padding(0, 5, 5, 5);
            btnIniciarSesion.Name = "btnIniciarSesion";
            btnIniciarSesion.Size = new Size(101, 25);
            btnIniciarSesion.TabIndex = 63;
            btnIniciarSesion.Text = "Iniciar sesión";
            btnIniciarSesion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTextoCuentaUsuario
            // 
            fieldTextoCuentaUsuario.Dock = DockStyle.Fill;
            fieldTextoCuentaUsuario.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point,  0);
            fieldTextoCuentaUsuario.ForeColor = Color.Gray;
            fieldTextoCuentaUsuario.ImeMode = ImeMode.NoControl;
            fieldTextoCuentaUsuario.Location = new Point(5, 5);
            fieldTextoCuentaUsuario.Margin = new Padding(5, 5, 0, 5);
            fieldTextoCuentaUsuario.Name = "fieldTextoCuentaUsuario";
            fieldTextoCuentaUsuario.Size = new Size(237, 25);
            fieldTextoCuentaUsuario.TabIndex = 62;
            fieldTextoCuentaUsuario.Text = "¿Ya tienes cuenta? ";
            fieldTextoCuentaUsuario.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldConfirmarPassword
            // 
            fieldConfirmarPassword.Animated = true;
            fieldConfirmarPassword.BorderColor = Color.Gainsboro;
            fieldConfirmarPassword.BorderRadius = 16;
            fieldConfirmarPassword.Cursor = Cursors.IBeam;
            fieldConfirmarPassword.CustomizableEdges = customizableEdges7;
            fieldConfirmarPassword.DefaultText = "";
            fieldConfirmarPassword.DisabledState.BorderColor = Color.White;
            fieldConfirmarPassword.DisabledState.ForeColor = Color.DimGray;
            fieldConfirmarPassword.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldConfirmarPassword.Dock = DockStyle.Fill;
            fieldConfirmarPassword.FocusedState.BorderColor = Color.SandyBrown;
            fieldConfirmarPassword.Font = new Font("Segoe UI", 11.25F);
            fieldConfirmarPassword.ForeColor = Color.Black;
            fieldConfirmarPassword.HoverState.BorderColor = Color.SandyBrown;
            fieldConfirmarPassword.IconLeftOffset = new Point(10, 0);
            fieldConfirmarPassword.IconRight = Properties.Resources.closed_eye_20px;
            fieldConfirmarPassword.IconRightOffset = new Point(10, 0);
            fieldConfirmarPassword.Location = new Point(5, 445);
            fieldConfirmarPassword.Margin = new Padding(5);
            fieldConfirmarPassword.Name = "fieldConfirmarPassword";
            fieldConfirmarPassword.PasswordChar = '●';
            fieldConfirmarPassword.PlaceholderForeColor = Color.DimGray;
            fieldConfirmarPassword.PlaceholderText = "Repita la contraseña";
            fieldConfirmarPassword.SelectedText = "";
            fieldConfirmarPassword.ShadowDecoration.CustomizableEdges = customizableEdges8;
            fieldConfirmarPassword.Size = new Size(440, 35);
            fieldConfirmarPassword.TabIndex = 65;
            fieldConfirmarPassword.TextOffset = new Point(5, -1);
            // 
            // fieldTituloNombreCorreo
            // 
            fieldTituloNombreCorreo.Dock = DockStyle.Fill;
            fieldTituloNombreCorreo.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldTituloNombreCorreo.ForeColor = Color.Gray;
            fieldTituloNombreCorreo.ImeMode = ImeMode.NoControl;
            fieldTituloNombreCorreo.Location = new Point(1, 281);
            fieldTituloNombreCorreo.Margin = new Padding(1);
            fieldTituloNombreCorreo.Name = "fieldTituloNombreCorreo";
            fieldTituloNombreCorreo.Size = new Size(448, 23);
            fieldTituloNombreCorreo.TabIndex = 66;
            fieldTituloNombreCorreo.Text = "CORREO ELECTRÓNICO";
            fieldTituloNombreCorreo.TextAlign = ContentAlignment.MiddleLeft;
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
            fieldCorreoElectronico.IconLeftOffset = new Point(10, 0);
            fieldCorreoElectronico.Location = new Point(5, 310);
            fieldCorreoElectronico.Margin = new Padding(5);
            fieldCorreoElectronico.Name = "fieldCorreoElectronico";
            fieldCorreoElectronico.PasswordChar = '\0';
            fieldCorreoElectronico.PlaceholderForeColor = Color.DimGray;
            fieldCorreoElectronico.PlaceholderText = "usuario@ejemplo.mail";
            fieldCorreoElectronico.SelectedText = "";
            fieldCorreoElectronico.ShadowDecoration.CustomizableEdges = customizableEdges10;
            fieldCorreoElectronico.Size = new Size(440, 35);
            fieldCorreoElectronico.TabIndex = 67;
            fieldCorreoElectronico.TextOffset = new Point(5, -1);
            // 
            // layoutNombreUsuario
            // 
            layoutNombreUsuario.Controls.Add(lbRequired1);
            layoutNombreUsuario.Controls.Add(fieldTituloNombreUsuario);
            layoutNombreUsuario.Dock = DockStyle.Fill;
            layoutNombreUsuario.Location = new Point(0, 200);
            layoutNombreUsuario.Margin = new Padding(0);
            layoutNombreUsuario.Name = "layoutNombreUsuario";
            layoutNombreUsuario.Size = new Size(450, 25);
            layoutNombreUsuario.TabIndex = 68;
            // 
            // lbRequired1
            // 
            lbRequired1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point,  0);
            lbRequired1.ForeColor = Color.Firebrick;
            lbRequired1.ImeMode = ImeMode.NoControl;
            lbRequired1.Location = new Point(1, 1);
            lbRequired1.Margin = new Padding(1, 1, 0, 1);
            lbRequired1.Name = "lbRequired1";
            lbRequired1.Size = new Size(8, 23);
            lbRequired1.TabIndex = 50;
            lbRequired1.Text = "*";
            lbRequired1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutPassword
            // 
            layoutPassword.Controls.Add(lbRequired2);
            layoutPassword.Controls.Add(fieldTituloPassword);
            layoutPassword.Dock = DockStyle.Fill;
            layoutPassword.Location = new Point(0, 360);
            layoutPassword.Margin = new Padding(0);
            layoutPassword.Name = "layoutPassword";
            layoutPassword.Size = new Size(450, 25);
            layoutPassword.TabIndex = 69;
            // 
            // lbRequired2
            // 
            lbRequired2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point,  0);
            lbRequired2.ForeColor = Color.Firebrick;
            lbRequired2.ImeMode = ImeMode.NoControl;
            lbRequired2.Location = new Point(1, 1);
            lbRequired2.Margin = new Padding(1, 1, 0, 1);
            lbRequired2.Name = "lbRequired2";
            lbRequired2.Size = new Size(8, 23);
            lbRequired2.TabIndex = 50;
            lbRequired2.Text = "*";
            lbRequired2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloPassword
            // 
            fieldTituloPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldTituloPassword.ForeColor = Color.Gray;
            fieldTituloPassword.ImeMode = ImeMode.NoControl;
            fieldTituloPassword.Location = new Point(9, 1);
            fieldTituloPassword.Margin = new Padding(0, 1, 1, 1);
            fieldTituloPassword.Name = "fieldTituloPassword";
            fieldTituloPassword.Size = new Size(85, 23);
            fieldTituloPassword.TabIndex = 49;
            fieldTituloPassword.Text = "CONTRASEÑA";
            fieldTituloPassword.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VistaRegistroUsuario
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(  250,   249,   246);
            ClientSize = new Size(500, 685);
            Controls.Add(panelDatos);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaRegistroUsuario";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaRegistroUsuario";
            panelDatos.ResumeLayout(false);
            layoutDatos.ResumeLayout(false);
            layoutDistBase.ResumeLayout(false);
            ((ISupportInitialize) pbBannerTitulo).EndInit();
            layoutRegistroCuenta.ResumeLayout(false);
            layoutNombreUsuario.ResumeLayout(false);
            layoutPassword.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private Guna2Panel panelDatos;
        private TableLayoutPanel layoutDatos;
        private TableLayoutPanel layoutDistBase;
        private PictureBox pbBannerTitulo;
        private Label fieldTituloNombreUsuario;
        private Guna2TextBox fieldNombreUsuario;
        private Guna2TextBox fieldPassword;
        private Guna2Button btnRegistrarCuentaUsuario;
        private TableLayoutPanel layoutRegistroCuenta;
        private Label btnIniciarSesion;
        private Label fieldTextoCuentaUsuario;
        private Label fieldCopyright;
        private Guna2TextBox fieldConfirmarPassword;
        private Label fieldTituloNombreCorreo;
        private Guna2TextBox fieldCorreoElectronico;
        private FlowLayoutPanel layoutNombreUsuario;
        private Label lbRequired1;
        private FlowLayoutPanel layoutPassword;
        private Label lbRequired2;
        private Label fieldTituloPassword;
    }
}