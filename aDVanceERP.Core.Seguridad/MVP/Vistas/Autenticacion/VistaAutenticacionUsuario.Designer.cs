using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion {
    partial class VistaAutenticacionUsuario {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaAutenticacionUsuario));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldTitulo = new Label();
            btnCambioConntraseña = new Label();
            fieldCopyright = new Label();
            fieldNombreUsuario = new Guna2TextBox();
            fieldPassword = new Guna2TextBox();
            btnAutenticarUsuario = new Guna2Button();
            btnRegistrarCuenta = new Guna2Button();
            fieldTextoServicioAlternativo = new Label();
            layoutServiciosExternosAutenticacion = new TableLayoutPanel();
            btnAutenticarGoogle = new Guna2CircleButton();
            brtnAutenticarFacebook = new Guna2CircleButton();
            layoutHelp = new TableLayoutPanel();
            fieldInformacion = new Guna2Button();
            infoIcon = new Guna2NotificationPaint(components);
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            layoutServiciosExternosAutenticacion.SuspendLayout();
            layoutHelp.SuspendLayout();
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
            // layoutBase
            // 
            layoutBase.BackColor = Color.Gainsboro;
            layoutBase.ColumnCount = 1;
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutBase.Controls.Add(layoutVista, 0, 0);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 1;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutBase.Size = new Size(500, 685);
            layoutBase.TabIndex = 2;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.FromArgb(  250,   250,   250);
            layoutVista.ColumnCount = 3;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.Controls.Add(fieldTitulo, 1, 1);
            layoutVista.Controls.Add(btnCambioConntraseña, 1, 7);
            layoutVista.Controls.Add(fieldCopyright, 1, 14);
            layoutVista.Controls.Add(fieldNombreUsuario, 1, 4);
            layoutVista.Controls.Add(fieldPassword, 1, 6);
            layoutVista.Controls.Add(btnAutenticarUsuario, 1, 9);
            layoutVista.Controls.Add(btnRegistrarCuenta, 1, 11);
            layoutVista.Controls.Add(fieldTextoServicioAlternativo, 1, 12);
            layoutVista.Controls.Add(layoutServiciosExternosAutenticacion, 1, 13);
            layoutVista.Controls.Add(layoutHelp, 1, 2);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(1, 1);
            layoutVista.Margin = new Padding(1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 16;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 22F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 78F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.Size = new Size(498, 683);
            layoutVista.TabIndex = 0;
            // 
            // fieldTitulo
            // 
            fieldTitulo.Dock = DockStyle.Fill;
            fieldTitulo.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            fieldTitulo.ForeColor = Color.Black;
            fieldTitulo.ImeMode = ImeMode.NoControl;
            fieldTitulo.Location = new Point(23, 20);
            fieldTitulo.Name = "fieldTitulo";
            fieldTitulo.Size = new Size(452, 80);
            fieldTitulo.TabIndex = 0;
            fieldTitulo.Text = "Inicia sesión en tu cuenta";
            fieldTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnCambioConntraseña
            // 
            btnCambioConntraseña.Cursor = Cursors.Hand;
            btnCambioConntraseña.Dock = DockStyle.Fill;
            btnCambioConntraseña.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnCambioConntraseña.ForeColor = Color.Black;
            btnCambioConntraseña.ImeMode = ImeMode.NoControl;
            btnCambioConntraseña.Location = new Point(35, 325);
            btnCambioConntraseña.Margin = new Padding(15, 5, 1, 1);
            btnCambioConntraseña.Name = "btnCambioConntraseña";
            btnCambioConntraseña.Size = new Size(442, 39);
            btnCambioConntraseña.TabIndex = 5;
            btnCambioConntraseña.Text = "¿Has olvidado tu contraseña?";
            // 
            // fieldCopyright
            // 
            fieldCopyright.Dock = DockStyle.Fill;
            fieldCopyright.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            fieldCopyright.ForeColor = Color.DarkGray;
            fieldCopyright.ImeMode = ImeMode.NoControl;
            fieldCopyright.Location = new Point(23, 582);
            fieldCopyright.Name = "fieldCopyright";
            fieldCopyright.Size = new Size(452, 80);
            fieldCopyright.TabIndex = 0;
            fieldCopyright.Text = "Copyright 2025© aDVance ERP®";
            fieldCopyright.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldNombreUsuario
            // 
            fieldNombreUsuario.Animated = true;
            fieldNombreUsuario.BorderColor = Color.Gainsboro;
            fieldNombreUsuario.BorderRadius = 16;
            fieldNombreUsuario.Cursor = Cursors.IBeam;
            fieldNombreUsuario.CustomizableEdges = customizableEdges13;
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
            fieldNombreUsuario.Location = new Point(25, 225);
            fieldNombreUsuario.Margin = new Padding(5);
            fieldNombreUsuario.Name = "fieldNombreUsuario";
            fieldNombreUsuario.PasswordChar = '\0';
            fieldNombreUsuario.PlaceholderForeColor = Color.DimGray;
            fieldNombreUsuario.PlaceholderText = "Nombre de usuario";
            fieldNombreUsuario.SelectedText = "";
            fieldNombreUsuario.ShadowDecoration.CustomizableEdges = customizableEdges14;
            fieldNombreUsuario.Size = new Size(448, 35);
            fieldNombreUsuario.TabIndex = 3;
            fieldNombreUsuario.TextOffset = new Point(5, 0);
            // 
            // fieldPassword
            // 
            fieldPassword.Animated = true;
            fieldPassword.BorderColor = Color.Gainsboro;
            fieldPassword.BorderRadius = 16;
            fieldPassword.Cursor = Cursors.IBeam;
            fieldPassword.CustomizableEdges = customizableEdges15;
            fieldPassword.DefaultText = "";
            fieldPassword.DisabledState.BorderColor = Color.White;
            fieldPassword.DisabledState.ForeColor = Color.DimGray;
            fieldPassword.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldPassword.Dock = DockStyle.Fill;
            fieldPassword.FocusedState.BorderColor = Color.SandyBrown;
            fieldPassword.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldPassword.ForeColor = Color.Black;
            fieldPassword.HoverState.BorderColor = Color.SandyBrown;
            fieldPassword.IconLeft = (Image) resources.GetObject("fieldPassword.IconLeft");
            fieldPassword.IconLeftOffset = new Point(10, 0);
            fieldPassword.IconRight = Properties.Resources.closed_eye_20px;
            fieldPassword.IconRightOffset = new Point(10, 0);
            fieldPassword.Location = new Point(25, 280);
            fieldPassword.Margin = new Padding(5);
            fieldPassword.Name = "fieldPassword";
            fieldPassword.PasswordChar = '●';
            fieldPassword.PlaceholderForeColor = Color.DimGray;
            fieldPassword.PlaceholderText = "Contraseña";
            fieldPassword.SelectedText = "";
            fieldPassword.ShadowDecoration.CustomizableEdges = customizableEdges16;
            fieldPassword.Size = new Size(448, 35);
            fieldPassword.TabIndex = 4;
            fieldPassword.TextOffset = new Point(5, 0);
            fieldPassword.UseSystemPasswordChar = true;
            // 
            // btnAutenticarUsuario
            // 
            btnAutenticarUsuario.Animated = true;
            btnAutenticarUsuario.BorderRadius = 18;
            btnAutenticarUsuario.CustomizableEdges = customizableEdges17;
            btnAutenticarUsuario.Dock = DockStyle.Fill;
            btnAutenticarUsuario.FillColor = Color.PeachPuff;
            btnAutenticarUsuario.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnAutenticarUsuario.ForeColor = Color.Black;
            btnAutenticarUsuario.Location = new Point(23, 384);
            btnAutenticarUsuario.Name = "btnAutenticarUsuario";
            btnAutenticarUsuario.ShadowDecoration.CustomizableEdges = customizableEdges18;
            btnAutenticarUsuario.Size = new Size(452, 39);
            btnAutenticarUsuario.TabIndex = 6;
            btnAutenticarUsuario.Text = "Autenticar";
            // 
            // btnRegistrarCuenta
            // 
            btnRegistrarCuenta.Animated = true;
            btnRegistrarCuenta.BorderColor = Color.Gainsboro;
            btnRegistrarCuenta.BorderRadius = 18;
            btnRegistrarCuenta.BorderThickness = 1;
            btnRegistrarCuenta.CustomizableEdges = customizableEdges19;
            btnRegistrarCuenta.Dock = DockStyle.Fill;
            btnRegistrarCuenta.FillColor = Color.White;
            btnRegistrarCuenta.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnRegistrarCuenta.ForeColor = Color.Gainsboro;
            btnRegistrarCuenta.HoverState.BorderColor = Color.PeachPuff;
            btnRegistrarCuenta.HoverState.FillColor = Color.PeachPuff;
            btnRegistrarCuenta.HoverState.ForeColor = Color.Black;
            btnRegistrarCuenta.Location = new Point(23, 439);
            btnRegistrarCuenta.Name = "btnRegistrarCuenta";
            btnRegistrarCuenta.ShadowDecoration.CustomizableEdges = customizableEdges20;
            btnRegistrarCuenta.Size = new Size(452, 39);
            btnRegistrarCuenta.TabIndex = 7;
            btnRegistrarCuenta.Text = "Eres nuevo? Crea una cuenta";
            // 
            // fieldTextoServicioAlternativo
            // 
            fieldTextoServicioAlternativo.Cursor = Cursors.Hand;
            fieldTextoServicioAlternativo.Dock = DockStyle.Fill;
            fieldTextoServicioAlternativo.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            fieldTextoServicioAlternativo.ForeColor = Color.Black;
            fieldTextoServicioAlternativo.ImeMode = ImeMode.NoControl;
            fieldTextoServicioAlternativo.Location = new Point(21, 486);
            fieldTextoServicioAlternativo.Margin = new Padding(1, 5, 1, 10);
            fieldTextoServicioAlternativo.Name = "fieldTextoServicioAlternativo";
            fieldTextoServicioAlternativo.Size = new Size(456, 42);
            fieldTextoServicioAlternativo.TabIndex = 8;
            fieldTextoServicioAlternativo.Text = "O utilice una de las opciones siguientes";
            fieldTextoServicioAlternativo.TextAlign = ContentAlignment.BottomCenter;
            fieldTextoServicioAlternativo.Visible = false;
            // 
            // layoutServiciosExternosAutenticacion
            // 
            layoutServiciosExternosAutenticacion.ColumnCount = 5;
            layoutServiciosExternosAutenticacion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutServiciosExternosAutenticacion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 44F));
            layoutServiciosExternosAutenticacion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutServiciosExternosAutenticacion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 44F));
            layoutServiciosExternosAutenticacion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutServiciosExternosAutenticacion.Controls.Add(btnAutenticarGoogle, 1, 0);
            layoutServiciosExternosAutenticacion.Controls.Add(brtnAutenticarFacebook, 3, 0);
            layoutServiciosExternosAutenticacion.Dock = DockStyle.Fill;
            layoutServiciosExternosAutenticacion.Location = new Point(20, 538);
            layoutServiciosExternosAutenticacion.Margin = new Padding(0);
            layoutServiciosExternosAutenticacion.Name = "layoutServiciosExternosAutenticacion";
            layoutServiciosExternosAutenticacion.RowCount = 1;
            layoutServiciosExternosAutenticacion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutServiciosExternosAutenticacion.Size = new Size(458, 44);
            layoutServiciosExternosAutenticacion.TabIndex = 9;
            // 
            // btnAutenticarGoogle
            // 
            btnAutenticarGoogle.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnAutenticarGoogle.CheckedState.FillColor = Color.PeachPuff;
            btnAutenticarGoogle.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnAutenticarGoogle.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnAutenticarGoogle.CustomImages.ImageSize = new Size(24, 24);
            btnAutenticarGoogle.Dock = DockStyle.Fill;
            btnAutenticarGoogle.FillColor = Color.White;
            btnAutenticarGoogle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnAutenticarGoogle.ForeColor = Color.White;
            btnAutenticarGoogle.ImageSize = new Size(24, 24);
            btnAutenticarGoogle.Location = new Point(178, 3);
            btnAutenticarGoogle.Name = "btnAutenticarGoogle";
            btnAutenticarGoogle.ShadowDecoration.CustomizableEdges = customizableEdges21;
            btnAutenticarGoogle.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnAutenticarGoogle.Size = new Size(38, 38);
            btnAutenticarGoogle.TabIndex = 0;
            btnAutenticarGoogle.Visible = false;
            // 
            // brtnAutenticarFacebook
            // 
            brtnAutenticarFacebook.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            brtnAutenticarFacebook.CheckedState.FillColor = Color.PeachPuff;
            brtnAutenticarFacebook.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            brtnAutenticarFacebook.CustomImages.ImageAlign = HorizontalAlignment.Center;
            brtnAutenticarFacebook.CustomImages.ImageSize = new Size(24, 24);
            brtnAutenticarFacebook.Dock = DockStyle.Fill;
            brtnAutenticarFacebook.FillColor = Color.White;
            brtnAutenticarFacebook.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            brtnAutenticarFacebook.ForeColor = Color.White;
            brtnAutenticarFacebook.ImageSize = new Size(24, 24);
            brtnAutenticarFacebook.Location = new Point(242, 3);
            brtnAutenticarFacebook.Name = "brtnAutenticarFacebook";
            brtnAutenticarFacebook.ShadowDecoration.CustomizableEdges = customizableEdges22;
            brtnAutenticarFacebook.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            brtnAutenticarFacebook.Size = new Size(38, 38);
            brtnAutenticarFacebook.TabIndex = 1;
            brtnAutenticarFacebook.Visible = false;
            // 
            // layoutHelp
            // 
            layoutHelp.ColumnCount = 1;
            layoutHelp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutHelp.Controls.Add(fieldInformacion, 0, 0);
            layoutHelp.Dock = DockStyle.Fill;
            layoutHelp.Location = new Point(20, 100);
            layoutHelp.Margin = new Padding(0);
            layoutHelp.Name = "layoutHelp";
            layoutHelp.Padding = new Padding(0, 0, 10, 0);
            layoutHelp.RowCount = 1;
            layoutHelp.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutHelp.Size = new Size(458, 100);
            layoutHelp.TabIndex = 2;
            // 
            // fieldInformacion
            // 
            fieldInformacion.BorderColor = Color.LightBlue;
            fieldInformacion.BorderRadius = 16;
            fieldInformacion.BorderThickness = 1;
            customizableEdges23.TopLeft = false;
            fieldInformacion.CustomizableEdges = customizableEdges23;
            fieldInformacion.Dock = DockStyle.Fill;
            fieldInformacion.FillColor = Color.LightBlue;
            fieldInformacion.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            fieldInformacion.ForeColor = Color.SteelBlue;
            fieldInformacion.HoverState.BorderColor = Color.LightBlue;
            fieldInformacion.HoverState.FillColor = Color.LightBlue;
            fieldInformacion.ImageOffset = new Point(-5, 0);
            fieldInformacion.Location = new Point(17, 13);
            fieldInformacion.Margin = new Padding(17, 13, 0, 5);
            fieldInformacion.Name = "fieldInformacion";
            fieldInformacion.PressedColor = Color.LightBlue;
            fieldInformacion.ShadowDecoration.CustomizableEdges = customizableEdges24;
            fieldInformacion.Size = new Size(431, 82);
            fieldInformacion.TabIndex = 0;
            fieldInformacion.Text = "Bienvenido a nuestra plataforma, debe autenticarse antes de comenzar a utilizar nuestros servicios y funcionalidades.";
            fieldInformacion.TextAlign = HorizontalAlignment.Left;
            fieldInformacion.TextOffset = new Point(20, 0);
            // 
            // infoIcon
            // 
            infoIcon.BorderColor = Color.Transparent;
            infoIcon.BorderRadius = 16;
            infoIcon.BorderThickness = 0;
            infoIcon.FillColor = Color.LightBlue;
            infoIcon.Font = new Font("Bodoni MT", 16F, FontStyle.Bold, GraphicsUnit.Point);
            infoIcon.ForeColor = Color.SteelBlue;
            infoIcon.Offset = new Point(0, 50);
            infoIcon.Size = new Size(30, 30);
            infoIcon.TargetControl = layoutHelp;
            infoIcon.Text = "i";
            // 
            // VistaAutenticacionUsuario
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 685);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaAutenticacionUsuario";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaAutenticacionUsuario";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            layoutServiciosExternosAutenticacion.ResumeLayout(false);
            layoutHelp.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private Label fieldTitulo;
        private Label btnCambioConntraseña;
        private Label fieldCopyright;
        private Guna2TextBox fieldNombreUsuario;
        private Guna2TextBox fieldPassword;
        private Guna2Button btnAutenticarUsuario;
        private Guna2Button btnRegistrarCuenta;
        private Label fieldTextoServicioAlternativo;
        private TableLayoutPanel layoutServiciosExternosAutenticacion;
        private Guna2CircleButton btnAutenticarGoogle;
        private Guna2CircleButton brtnAutenticarFacebook;
        private TableLayoutPanel layoutHelp;
        private Guna2Button fieldInformacion;
        private Guna2NotificationPaint infoIcon;
    }
}