using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Seguridad.Vistas {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaAutenticacionUsuario));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            panelDatosAutenticacion = new Guna2Panel();
            layoutDatosAutenticacion = new TableLayoutPanel();
            layoutDistBase = new TableLayoutPanel();
            pbBannerTitulo = new PictureBox();
            fieldTituloNombreCorreo = new Label();
            fieldTituloPassword = new Label();
            fieldNombreUsuario = new Guna2TextBox();
            fieldPassword = new Guna2TextBox();
            btnAutenticarUsuario = new Guna2Button();
            btnRecuperarPassword = new Label();
            layoutRegistroCuenta = new TableLayoutPanel();
            fieldTextoCuentaUsuario = new Label();
            btnRegistrarCuenta = new Label();
            fieldCopyright = new Label();
            layoutBase.SuspendLayout();
            panelDatosAutenticacion.SuspendLayout();
            layoutDatosAutenticacion.SuspendLayout();
            layoutDistBase.SuspendLayout();
            ((ISupportInitialize) pbBannerTitulo).BeginInit();
            layoutRegistroCuenta.SuspendLayout();
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
            layoutBase.BackColor = Color.Transparent;
            layoutBase.ColumnCount = 1;
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutBase.Controls.Add(panelDatosAutenticacion, 0, 0);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 1;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutBase.Size = new Size(500, 685);
            layoutBase.TabIndex = 2;
            // 
            // panelDatosAutenticacion
            // 
            panelDatosAutenticacion.BackColor = Color.Transparent;
            panelDatosAutenticacion.BorderColor = Color.Gainsboro;
            panelDatosAutenticacion.BorderRadius = 16;
            panelDatosAutenticacion.BorderThickness = 1;
            panelDatosAutenticacion.Controls.Add(layoutDatosAutenticacion);
            customizableEdges7.BottomLeft = false;
            customizableEdges7.BottomRight = false;
            panelDatosAutenticacion.CustomizableEdges = customizableEdges7;
            panelDatosAutenticacion.Dock = DockStyle.Fill;
            panelDatosAutenticacion.FillColor = Color.White;
            panelDatosAutenticacion.Location = new Point(0, 0);
            panelDatosAutenticacion.Margin = new Padding(0);
            panelDatosAutenticacion.Name = "panelDatosAutenticacion";
            panelDatosAutenticacion.ShadowDecoration.BorderRadius = 8;
            panelDatosAutenticacion.ShadowDecoration.CustomizableEdges = customizableEdges8;
            panelDatosAutenticacion.ShadowDecoration.Depth = 10;
            panelDatosAutenticacion.Size = new Size(500, 685);
            panelDatosAutenticacion.TabIndex = 56;
            // 
            // layoutDatosAutenticacion
            // 
            layoutDatosAutenticacion.BackColor = Color.Transparent;
            layoutDatosAutenticacion.ColumnCount = 3;
            layoutDatosAutenticacion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 25F));
            layoutDatosAutenticacion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDatosAutenticacion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 25F));
            layoutDatosAutenticacion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDatosAutenticacion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDatosAutenticacion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDatosAutenticacion.Controls.Add(layoutDistBase, 1, 1);
            layoutDatosAutenticacion.Dock = DockStyle.Fill;
            layoutDatosAutenticacion.Location = new Point(0, 0);
            layoutDatosAutenticacion.Name = "layoutDatosAutenticacion";
            layoutDatosAutenticacion.RowCount = 3;
            layoutDatosAutenticacion.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutDatosAutenticacion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDatosAutenticacion.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutDatosAutenticacion.Size = new Size(500, 685);
            layoutDatosAutenticacion.TabIndex = 0;
            // 
            // layoutDistBase
            // 
            layoutDistBase.ColumnCount = 1;
            layoutDistBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistBase.Controls.Add(pbBannerTitulo, 0, 1);
            layoutDistBase.Controls.Add(fieldTituloNombreCorreo, 0, 3);
            layoutDistBase.Controls.Add(fieldTituloPassword, 0, 6);
            layoutDistBase.Controls.Add(fieldNombreUsuario, 0, 4);
            layoutDistBase.Controls.Add(fieldPassword, 0, 7);
            layoutDistBase.Controls.Add(btnAutenticarUsuario, 0, 9);
            layoutDistBase.Controls.Add(btnRecuperarPassword, 0, 11);
            layoutDistBase.Controls.Add(layoutRegistroCuenta, 0, 12);
            layoutDistBase.Controls.Add(fieldCopyright, 0, 14);
            layoutDistBase.Dock = DockStyle.Fill;
            layoutDistBase.Location = new Point(25, 5);
            layoutDistBase.Margin = new Padding(0);
            layoutDistBase.Name = "layoutDistBase";
            layoutDistBase.RowCount = 16;
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 160F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
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
            // fieldTituloNombreCorreo
            // 
            fieldTituloNombreCorreo.Dock = DockStyle.Fill;
            fieldTituloNombreCorreo.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldTituloNombreCorreo.ForeColor = Color.Gray;
            fieldTituloNombreCorreo.ImeMode = ImeMode.NoControl;
            fieldTituloNombreCorreo.Location = new Point(1, 201);
            fieldTituloNombreCorreo.Margin = new Padding(1);
            fieldTituloNombreCorreo.Name = "fieldTituloNombreCorreo";
            fieldTituloNombreCorreo.Size = new Size(448, 23);
            fieldTituloNombreCorreo.TabIndex = 49;
            fieldTituloNombreCorreo.Text = "NOMBRE DE USUARIO O CORREO ELECTRÓNICO";
            fieldTituloNombreCorreo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloPassword
            // 
            fieldTituloPassword.Dock = DockStyle.Fill;
            fieldTituloPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldTituloPassword.ForeColor = Color.Gray;
            fieldTituloPassword.ImeMode = ImeMode.NoControl;
            fieldTituloPassword.Location = new Point(1, 281);
            fieldTituloPassword.Margin = new Padding(1);
            fieldTituloPassword.Name = "fieldTituloPassword";
            fieldTituloPassword.Size = new Size(448, 23);
            fieldTituloPassword.TabIndex = 50;
            fieldTituloPassword.Text = "CONTRASEÑA";
            fieldTituloPassword.TextAlign = ContentAlignment.MiddleLeft;
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
            fieldNombreUsuario.IconLeft = (Image) resources.GetObject("fieldNombreUsuario.IconLeft");
            fieldNombreUsuario.IconLeftOffset = new Point(10, 0);
            fieldNombreUsuario.Location = new Point(5, 230);
            fieldNombreUsuario.Margin = new Padding(5);
            fieldNombreUsuario.Name = "fieldNombreUsuario";
            fieldNombreUsuario.PasswordChar = '\0';
            fieldNombreUsuario.PlaceholderForeColor = Color.DimGray;
            fieldNombreUsuario.PlaceholderText = "usuario@advanceerp.cu";
            fieldNombreUsuario.SelectedText = "";
            fieldNombreUsuario.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fieldNombreUsuario.Size = new Size(440, 35);
            fieldNombreUsuario.TabIndex = 58;
            fieldNombreUsuario.TextOffset = new Point(5, -1);
            // 
            // fieldPassword
            // 
            fieldPassword.Animated = true;
            fieldPassword.BorderColor = Color.Gainsboro;
            fieldPassword.BorderRadius = 16;
            fieldPassword.Cursor = Cursors.IBeam;
            fieldPassword.CustomizableEdges = customizableEdges3;
            fieldPassword.DefaultText = "";
            fieldPassword.DisabledState.BorderColor = Color.White;
            fieldPassword.DisabledState.ForeColor = Color.DimGray;
            fieldPassword.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldPassword.Dock = DockStyle.Fill;
            fieldPassword.FocusedState.BorderColor = Color.SandyBrown;
            fieldPassword.Font = new Font("Segoe UI", 11.25F);
            fieldPassword.ForeColor = Color.Black;
            fieldPassword.HoverState.BorderColor = Color.SandyBrown;
            fieldPassword.IconLeft = (Image) resources.GetObject("fieldPassword.IconLeft");
            fieldPassword.IconLeftOffset = new Point(10, 0);
            fieldPassword.IconRight = Properties.Resources.closed_eye_20px;
            fieldPassword.IconRightOffset = new Point(10, 0);
            fieldPassword.Location = new Point(5, 310);
            fieldPassword.Margin = new Padding(5);
            fieldPassword.Name = "fieldPassword";
            fieldPassword.PasswordChar = '●';
            fieldPassword.PlaceholderForeColor = Color.DimGray;
            fieldPassword.PlaceholderText = "●●●●●●●●";
            fieldPassword.SelectedText = "";
            fieldPassword.ShadowDecoration.CustomizableEdges = customizableEdges4;
            fieldPassword.Size = new Size(440, 35);
            fieldPassword.TabIndex = 59;
            fieldPassword.TextOffset = new Point(5, -1);
            // 
            // btnAutenticarUsuario
            // 
            btnAutenticarUsuario.Animated = true;
            btnAutenticarUsuario.BorderRadius = 18;
            btnAutenticarUsuario.CustomizableEdges = customizableEdges5;
            btnAutenticarUsuario.Dock = DockStyle.Fill;
            btnAutenticarUsuario.FillColor = Color.PeachPuff;
            btnAutenticarUsuario.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point,  0);
            btnAutenticarUsuario.ForeColor = Color.Black;
            btnAutenticarUsuario.Location = new Point(5, 362);
            btnAutenticarUsuario.Margin = new Padding(5, 2, 5, 2);
            btnAutenticarUsuario.Name = "btnAutenticarUsuario";
            btnAutenticarUsuario.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnAutenticarUsuario.Size = new Size(440, 41);
            btnAutenticarUsuario.TabIndex = 60;
            btnAutenticarUsuario.Text = "Iniciar sesión";
            // 
            // btnRecuperarPassword
            // 
            btnRecuperarPassword.Cursor = Cursors.Hand;
            btnRecuperarPassword.Dock = DockStyle.Top;
            btnRecuperarPassword.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point,  0);
            btnRecuperarPassword.ForeColor = Color.Firebrick;
            btnRecuperarPassword.ImeMode = ImeMode.NoControl;
            btnRecuperarPassword.Location = new Point(5, 420);
            btnRecuperarPassword.Margin = new Padding(5);
            btnRecuperarPassword.Name = "btnRecuperarPassword";
            btnRecuperarPassword.Size = new Size(440, 25);
            btnRecuperarPassword.TabIndex = 61;
            btnRecuperarPassword.Text = "¿Olvidaste tu contraseña?";
            btnRecuperarPassword.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // layoutRegistroCuenta
            // 
            layoutRegistroCuenta.ColumnCount = 2;
            layoutRegistroCuenta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.77778F));
            layoutRegistroCuenta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.22222F));
            layoutRegistroCuenta.Controls.Add(btnRegistrarCuenta, 1, 0);
            layoutRegistroCuenta.Controls.Add(fieldTextoCuentaUsuario, 0, 0);
            layoutRegistroCuenta.Dock = DockStyle.Fill;
            layoutRegistroCuenta.Location = new Point(0, 450);
            layoutRegistroCuenta.Margin = new Padding(0);
            layoutRegistroCuenta.Name = "layoutRegistroCuenta";
            layoutRegistroCuenta.RowCount = 1;
            layoutRegistroCuenta.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutRegistroCuenta.Size = new Size(450, 35);
            layoutRegistroCuenta.TabIndex = 62;
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
            fieldTextoCuentaUsuario.Size = new Size(228, 25);
            fieldTextoCuentaUsuario.TabIndex = 62;
            fieldTextoCuentaUsuario.Text = "¿No tienes cuenta? ";
            fieldTextoCuentaUsuario.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnRegistrarCuenta
            // 
            btnRegistrarCuenta.Cursor = Cursors.Hand;
            btnRegistrarCuenta.Dock = DockStyle.Left;
            btnRegistrarCuenta.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point,  0);
            btnRegistrarCuenta.ForeColor = Color.Firebrick;
            btnRegistrarCuenta.ImeMode = ImeMode.NoControl;
            btnRegistrarCuenta.Location = new Point(233, 5);
            btnRegistrarCuenta.Margin = new Padding(0, 5, 5, 5);
            btnRegistrarCuenta.Name = "btnRegistrarCuenta";
            btnRegistrarCuenta.Size = new Size(117, 25);
            btnRegistrarCuenta.TabIndex = 63;
            btnRegistrarCuenta.Text = "Regístrate aquí";
            btnRegistrarCuenta.TextAlign = ContentAlignment.MiddleLeft;
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
            // VistaAutenticacionUsuario
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(  250,   249,   246);
            ClientSize = new Size(500, 685);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaAutenticacionUsuario";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaAutenticacionUsuario";
            layoutBase.ResumeLayout(false);
            panelDatosAutenticacion.ResumeLayout(false);
            layoutDatosAutenticacion.ResumeLayout(false);
            layoutDistBase.ResumeLayout(false);
            ((ISupportInitialize) pbBannerTitulo).EndInit();
            layoutRegistroCuenta.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private Guna2Panel panelDatosAutenticacion;
        private TableLayoutPanel layoutDatosAutenticacion;
        private TableLayoutPanel layoutDistBase;
        private PictureBox pbBannerTitulo;
        private Label fieldTituloNombreCorreo;
        private Label fieldTituloPassword;
        private Guna2TextBox fieldNombreUsuario;
        private Guna2TextBox fieldPassword;
        private Guna2Button btnAutenticarUsuario;
        private Label btnRecuperarPassword;
        private TableLayoutPanel layoutRegistroCuenta;
        private Label btnRegistrarCuenta;
        private Label fieldTextoCuentaUsuario;
        private Label fieldCopyright;
    }
}