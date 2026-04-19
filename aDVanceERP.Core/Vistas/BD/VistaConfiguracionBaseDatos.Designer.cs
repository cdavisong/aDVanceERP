using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Core.Vistas.BD {
    partial class VistaConfiguracionBaseDatos {
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
            fieldTituloDireccionServidor = new Label();
            fieldDireccionServidor = new Guna2TextBox();
            btnValidarConexion = new Guna2Button();
            fieldCopyright = new Label();
            fieldTituloNombreBd = new Label();
            fieldNombreBd = new Guna2TextBox();
            fieldPassword = new Guna2TextBox();
            fieldTituloPassword = new Label();
            fieldNombreUsuario = new Guna2TextBox();
            fieldTituloNombreUsuario = new Label();
            panelDatos.SuspendLayout();
            layoutDatos.SuspendLayout();
            layoutDistBase.SuspendLayout();
            ((ISupportInitialize) pbBannerTitulo).BeginInit();
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
            panelDatos.TabIndex = 58;
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
            layoutDistBase.Controls.Add(fieldTituloDireccionServidor, 0, 3);
            layoutDistBase.Controls.Add(fieldDireccionServidor, 0, 4);
            layoutDistBase.Controls.Add(btnValidarConexion, 0, 15);
            layoutDistBase.Controls.Add(fieldCopyright, 0, 17);
            layoutDistBase.Controls.Add(fieldTituloNombreBd, 0, 6);
            layoutDistBase.Controls.Add(fieldNombreBd, 0, 7);
            layoutDistBase.Controls.Add(fieldPassword, 0, 13);
            layoutDistBase.Controls.Add(fieldTituloPassword, 0, 12);
            layoutDistBase.Controls.Add(fieldNombreUsuario, 0, 10);
            layoutDistBase.Controls.Add(fieldTituloNombreUsuario, 0, 9);
            layoutDistBase.Dock = DockStyle.Fill;
            layoutDistBase.Location = new Point(25, 5);
            layoutDistBase.Margin = new Padding(0);
            layoutDistBase.Name = "layoutDistBase";
            layoutDistBase.RowCount = 19;
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
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutDistBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
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
            // fieldTituloDireccionServidor
            // 
            fieldTituloDireccionServidor.Dock = DockStyle.Fill;
            fieldTituloDireccionServidor.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldTituloDireccionServidor.ForeColor = Color.Gray;
            fieldTituloDireccionServidor.ImeMode = ImeMode.NoControl;
            fieldTituloDireccionServidor.Location = new Point(1, 201);
            fieldTituloDireccionServidor.Margin = new Padding(1);
            fieldTituloDireccionServidor.Name = "fieldTituloDireccionServidor";
            fieldTituloDireccionServidor.Size = new Size(448, 23);
            fieldTituloDireccionServidor.TabIndex = 49;
            fieldTituloDireccionServidor.Text = "DIRECCIÓN DEL SERVIDOR";
            fieldTituloDireccionServidor.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldDireccionServidor
            // 
            fieldDireccionServidor.Animated = true;
            fieldDireccionServidor.BorderColor = Color.Gainsboro;
            fieldDireccionServidor.BorderRadius = 16;
            fieldDireccionServidor.Cursor = Cursors.IBeam;
            fieldDireccionServidor.CustomizableEdges = customizableEdges1;
            fieldDireccionServidor.DefaultText = "";
            fieldDireccionServidor.DisabledState.BorderColor = Color.White;
            fieldDireccionServidor.DisabledState.ForeColor = Color.DimGray;
            fieldDireccionServidor.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldDireccionServidor.Dock = DockStyle.Fill;
            fieldDireccionServidor.FocusedState.BorderColor = Color.SandyBrown;
            fieldDireccionServidor.Font = new Font("Segoe UI", 11.25F);
            fieldDireccionServidor.ForeColor = Color.Black;
            fieldDireccionServidor.HoverState.BorderColor = Color.SandyBrown;
            fieldDireccionServidor.IconLeftOffset = new Point(10, 0);
            fieldDireccionServidor.Location = new Point(5, 230);
            fieldDireccionServidor.Margin = new Padding(5);
            fieldDireccionServidor.Name = "fieldDireccionServidor";
            fieldDireccionServidor.PasswordChar = '\0';
            fieldDireccionServidor.PlaceholderForeColor = Color.DimGray;
            fieldDireccionServidor.PlaceholderText = "dirección ip o dominio del servidor";
            fieldDireccionServidor.SelectedText = "";
            fieldDireccionServidor.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fieldDireccionServidor.Size = new Size(440, 35);
            fieldDireccionServidor.TabIndex = 58;
            fieldDireccionServidor.TextOffset = new Point(5, -1);
            // 
            // btnValidarConexion
            // 
            btnValidarConexion.Animated = true;
            btnValidarConexion.BorderRadius = 18;
            btnValidarConexion.CustomizableEdges = customizableEdges3;
            btnValidarConexion.Dock = DockStyle.Fill;
            btnValidarConexion.FillColor = Color.PeachPuff;
            btnValidarConexion.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point,  0);
            btnValidarConexion.ForeColor = Color.Black;
            btnValidarConexion.Location = new Point(5, 522);
            btnValidarConexion.Margin = new Padding(5, 2, 5, 2);
            btnValidarConexion.Name = "btnValidarConexion";
            btnValidarConexion.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnValidarConexion.Size = new Size(440, 41);
            btnValidarConexion.TabIndex = 60;
            btnValidarConexion.Text = "Validar la conexión";
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
            // fieldTituloNombreBd
            // 
            fieldTituloNombreBd.Dock = DockStyle.Fill;
            fieldTituloNombreBd.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldTituloNombreBd.ForeColor = Color.Gray;
            fieldTituloNombreBd.ImeMode = ImeMode.NoControl;
            fieldTituloNombreBd.Location = new Point(1, 281);
            fieldTituloNombreBd.Margin = new Padding(1);
            fieldTituloNombreBd.Name = "fieldTituloNombreBd";
            fieldTituloNombreBd.Size = new Size(448, 23);
            fieldTituloNombreBd.TabIndex = 66;
            fieldTituloNombreBd.Text = "BASE DE DATOS";
            fieldTituloNombreBd.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldNombreBd
            // 
            fieldNombreBd.Animated = true;
            fieldNombreBd.BorderColor = Color.Gainsboro;
            fieldNombreBd.BorderRadius = 16;
            fieldNombreBd.Cursor = Cursors.IBeam;
            fieldNombreBd.CustomizableEdges = customizableEdges5;
            fieldNombreBd.DefaultText = "";
            fieldNombreBd.DisabledState.BorderColor = Color.White;
            fieldNombreBd.DisabledState.ForeColor = Color.DimGray;
            fieldNombreBd.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldNombreBd.Dock = DockStyle.Fill;
            fieldNombreBd.FocusedState.BorderColor = Color.SandyBrown;
            fieldNombreBd.Font = new Font("Segoe UI", 11.25F);
            fieldNombreBd.ForeColor = Color.Black;
            fieldNombreBd.HoverState.BorderColor = Color.SandyBrown;
            fieldNombreBd.IconLeftOffset = new Point(10, 0);
            fieldNombreBd.Location = new Point(5, 310);
            fieldNombreBd.Margin = new Padding(5);
            fieldNombreBd.Name = "fieldNombreBd";
            fieldNombreBd.PasswordChar = '\0';
            fieldNombreBd.PlaceholderForeColor = Color.DimGray;
            fieldNombreBd.PlaceholderText = "advanceerp";
            fieldNombreBd.SelectedText = "";
            fieldNombreBd.ShadowDecoration.CustomizableEdges = customizableEdges6;
            fieldNombreBd.Size = new Size(440, 35);
            fieldNombreBd.TabIndex = 67;
            fieldNombreBd.TextOffset = new Point(5, -1);
            // 
            // fieldPassword
            // 
            fieldPassword.Animated = true;
            fieldPassword.BorderColor = Color.Gainsboro;
            fieldPassword.BorderRadius = 16;
            fieldPassword.Cursor = Cursors.IBeam;
            fieldPassword.CustomizableEdges = customizableEdges7;
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
            fieldPassword.IconRightOffset = new Point(10, 0);
            fieldPassword.Location = new Point(5, 470);
            fieldPassword.Margin = new Padding(5);
            fieldPassword.Name = "fieldPassword";
            fieldPassword.PasswordChar = '●';
            fieldPassword.PlaceholderForeColor = Color.DimGray;
            fieldPassword.PlaceholderText = "●●●●●●●●";
            fieldPassword.SelectedText = "";
            fieldPassword.ShadowDecoration.CustomizableEdges = customizableEdges8;
            fieldPassword.Size = new Size(440, 35);
            fieldPassword.TabIndex = 68;
            fieldPassword.TextOffset = new Point(5, -1);
            // 
            // fieldTituloPassword
            // 
            fieldTituloPassword.Dock = DockStyle.Fill;
            fieldTituloPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldTituloPassword.ForeColor = Color.Gray;
            fieldTituloPassword.ImeMode = ImeMode.NoControl;
            fieldTituloPassword.Location = new Point(1, 441);
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
            fieldNombreUsuario.CustomizableEdges = customizableEdges9;
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
            fieldNombreUsuario.Location = new Point(5, 390);
            fieldNombreUsuario.Margin = new Padding(5);
            fieldNombreUsuario.Name = "fieldNombreUsuario";
            fieldNombreUsuario.PasswordChar = '\0';
            fieldNombreUsuario.PlaceholderForeColor = Color.DimGray;
            fieldNombreUsuario.PlaceholderText = "usuario";
            fieldNombreUsuario.SelectedText = "";
            fieldNombreUsuario.ShadowDecoration.CustomizableEdges = customizableEdges10;
            fieldNombreUsuario.Size = new Size(440, 35);
            fieldNombreUsuario.TabIndex = 69;
            fieldNombreUsuario.TextOffset = new Point(5, -1);
            // 
            // fieldTituloNombreUsuario
            // 
            fieldTituloNombreUsuario.Dock = DockStyle.Fill;
            fieldTituloNombreUsuario.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldTituloNombreUsuario.ForeColor = Color.Gray;
            fieldTituloNombreUsuario.ImeMode = ImeMode.NoControl;
            fieldTituloNombreUsuario.Location = new Point(1, 361);
            fieldTituloNombreUsuario.Margin = new Padding(1);
            fieldTituloNombreUsuario.Name = "fieldTituloNombreUsuario";
            fieldTituloNombreUsuario.Size = new Size(448, 23);
            fieldTituloNombreUsuario.TabIndex = 70;
            fieldTituloNombreUsuario.Text = "NOMBRE DE USUARIO";
            fieldTituloNombreUsuario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VistaConfiguracionBaseDatos
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(  250,   249,   246);
            ClientSize = new Size(500, 685);
            Controls.Add(panelDatos);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaConfiguracionBaseDatos";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaAutenticacionUsuario";
            panelDatos.ResumeLayout(false);
            layoutDatos.ResumeLayout(false);
            layoutDistBase.ResumeLayout(false);
            ((ISupportInitialize) pbBannerTitulo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private Guna2Panel panelDatos;
        private TableLayoutPanel layoutDatos;
        private TableLayoutPanel layoutDistBase;
        private PictureBox pbBannerTitulo;
        private Label fieldTituloDireccionServidor;
        private Guna2TextBox fieldDireccionServidor;
        private Guna2Button btnValidarConexion;
        private Label fieldCopyright;
        private Label fieldTituloPassword;
        private Label fieldTituloNombreBd;
        private Guna2TextBox fieldNombreBd;
        private Guna2TextBox fieldPassword;
        private Guna2TextBox fieldNombreUsuario;
        private Label fieldTituloNombreUsuario;
    }
}