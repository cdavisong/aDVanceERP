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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaConfiguracionBaseDatos));
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
            fieldTitulo = new Label();
            fieldCopyright = new Label();
            fieldDireccionServidor = new Guna2TextBox();
            fieldNombreBd = new Guna2TextBox();
            btnValidarConexion = new Guna2Button();
            layoutHelp = new TableLayoutPanel();
            fieldInformacion = new Guna2Button();
            layoutRecordarConfiguracion = new TableLayoutPanel();
            fieldTextoRecordarConfiguracionBd = new Label();
            fieldRecordarConfiguracionBd = new Guna2CheckBox();
            fieldNombreUsuario = new Guna2TextBox();
            fieldPassword = new Guna2TextBox();
            infoIcon = new Guna2NotificationPaint(components);
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            layoutHelp.SuspendLayout();
            layoutRecordarConfiguracion.SuspendLayout();
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
            layoutVista.BackColor = Color.FromArgb(250, 250, 250);
            layoutVista.ColumnCount = 3;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.Controls.Add(fieldTitulo, 1, 1);
            layoutVista.Controls.Add(fieldCopyright, 1, 15);
            layoutVista.Controls.Add(fieldDireccionServidor, 1, 4);
            layoutVista.Controls.Add(fieldNombreBd, 1, 6);
            layoutVista.Controls.Add(btnValidarConexion, 1, 13);
            layoutVista.Controls.Add(layoutHelp, 1, 2);
            layoutVista.Controls.Add(layoutRecordarConfiguracion, 1, 11);
            layoutVista.Controls.Add(fieldNombreUsuario, 1, 8);
            layoutVista.Controls.Add(fieldPassword, 1, 10);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(1, 1);
            layoutVista.Margin = new Padding(1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 17;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 22F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 78F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.Size = new Size(498, 683);
            layoutVista.TabIndex = 0;
            // 
            // fieldTitulo
            // 
            fieldTitulo.Dock = DockStyle.Fill;
            fieldTitulo.Font = new Font("Segoe UI", 24F);
            fieldTitulo.ForeColor = Color.Black;
            fieldTitulo.ImeMode = ImeMode.NoControl;
            fieldTitulo.Location = new Point(23, 20);
            fieldTitulo.Name = "fieldTitulo";
            fieldTitulo.Size = new Size(452, 80);
            fieldTitulo.TabIndex = 0;
            fieldTitulo.Text = "Conexión al servidor";
            fieldTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldCopyright
            // 
            fieldCopyright.Dock = DockStyle.Fill;
            fieldCopyright.Font = new Font("Segoe UI", 9.75F);
            fieldCopyright.ForeColor = Color.DarkGray;
            fieldCopyright.ImeMode = ImeMode.NoControl;
            fieldCopyright.Location = new Point(23, 582);
            fieldCopyright.Name = "fieldCopyright";
            fieldCopyright.Size = new Size(452, 80);
            fieldCopyright.TabIndex = 0;
            fieldCopyright.Text = "Copyright 2025© aDVance ERP®";
            fieldCopyright.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldDireccionServidor
            // 
            fieldDireccionServidor.Animated = true;
            fieldDireccionServidor.AutoRoundedCorners = true;
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
            fieldDireccionServidor.IconLeft = (Image)resources.GetObject("fieldDireccionServidor.IconLeft");
            fieldDireccionServidor.IconLeftOffset = new Point(10, 0);
            fieldDireccionServidor.Location = new Point(25, 225);
            fieldDireccionServidor.Margin = new Padding(5);
            fieldDireccionServidor.Name = "fieldDireccionServidor";
            fieldDireccionServidor.PasswordChar = '\0';
            fieldDireccionServidor.PlaceholderForeColor = Color.DimGray;
            fieldDireccionServidor.PlaceholderText = "Nombre o dirección del servidor";
            fieldDireccionServidor.SelectedText = "";
            fieldDireccionServidor.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fieldDireccionServidor.Size = new Size(448, 35);
            fieldDireccionServidor.TabIndex = 3;
            fieldDireccionServidor.TextOffset = new Point(5, 0);
            // 
            // fieldNombreBd
            // 
            fieldNombreBd.Animated = true;
            fieldNombreBd.AutoRoundedCorners = true;
            fieldNombreBd.BorderColor = Color.Gainsboro;
            fieldNombreBd.BorderRadius = 16;
            fieldNombreBd.Cursor = Cursors.IBeam;
            fieldNombreBd.CustomizableEdges = customizableEdges3;
            fieldNombreBd.DefaultText = "";
            fieldNombreBd.DisabledState.BorderColor = Color.White;
            fieldNombreBd.DisabledState.ForeColor = Color.DimGray;
            fieldNombreBd.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldNombreBd.Dock = DockStyle.Fill;
            fieldNombreBd.FocusedState.BorderColor = Color.SandyBrown;
            fieldNombreBd.Font = new Font("Segoe UI", 11.25F);
            fieldNombreBd.ForeColor = Color.Black;
            fieldNombreBd.HoverState.BorderColor = Color.SandyBrown;
            fieldNombreBd.IconLeft = (Image)resources.GetObject("fieldNombreBd.IconLeft");
            fieldNombreBd.IconLeftOffset = new Point(10, 0);
            fieldNombreBd.IconRightOffset = new Point(10, 0);
            fieldNombreBd.Location = new Point(25, 280);
            fieldNombreBd.Margin = new Padding(5);
            fieldNombreBd.Name = "fieldNombreBd";
            fieldNombreBd.PasswordChar = '\0';
            fieldNombreBd.PlaceholderForeColor = Color.DimGray;
            fieldNombreBd.PlaceholderText = "Nombre de la base de datos (advanceerp por defecto)";
            fieldNombreBd.SelectedText = "";
            fieldNombreBd.ShadowDecoration.CustomizableEdges = customizableEdges4;
            fieldNombreBd.Size = new Size(448, 35);
            fieldNombreBd.TabIndex = 4;
            fieldNombreBd.TextOffset = new Point(5, 0);
            // 
            // btnValidarConexion
            // 
            btnValidarConexion.Animated = true;
            btnValidarConexion.AutoRoundedCorners = true;
            btnValidarConexion.BorderRadius = 18;
            btnValidarConexion.CustomizableEdges = customizableEdges5;
            btnValidarConexion.Dock = DockStyle.Fill;
            btnValidarConexion.FillColor = Color.PeachPuff;
            btnValidarConexion.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnValidarConexion.ForeColor = Color.Black;
            btnValidarConexion.Location = new Point(23, 491);
            btnValidarConexion.Name = "btnValidarConexion";
            btnValidarConexion.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnValidarConexion.Size = new Size(452, 39);
            btnValidarConexion.TabIndex = 6;
            btnValidarConexion.Text = "Validar la conexión";
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
            customizableEdges7.TopLeft = false;
            fieldInformacion.CustomizableEdges = customizableEdges7;
            fieldInformacion.Dock = DockStyle.Fill;
            fieldInformacion.FillColor = Color.LightBlue;
            fieldInformacion.Font = new Font("Segoe UI", 9.75F);
            fieldInformacion.ForeColor = Color.SteelBlue;
            fieldInformacion.HoverState.BorderColor = Color.LightBlue;
            fieldInformacion.HoverState.FillColor = Color.LightBlue;
            fieldInformacion.ImageOffset = new Point(-5, 0);
            fieldInformacion.Location = new Point(17, 13);
            fieldInformacion.Margin = new Padding(17, 13, 0, 5);
            fieldInformacion.Name = "fieldInformacion";
            fieldInformacion.PressedColor = Color.LightBlue;
            fieldInformacion.ShadowDecoration.CustomizableEdges = customizableEdges8;
            fieldInformacion.Size = new Size(431, 82);
            fieldInformacion.TabIndex = 0;
            fieldInformacion.Text = "Configure una nueva conexión al servidor que contiene la base de datos del programa y de click en el botón de \"Validar\"";
            fieldInformacion.TextAlign = HorizontalAlignment.Left;
            fieldInformacion.TextOffset = new Point(20, 0);
            // 
            // layoutRecordarConfiguracion
            // 
            layoutRecordarConfiguracion.ColumnCount = 2;
            layoutRecordarConfiguracion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 26F));
            layoutRecordarConfiguracion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutRecordarConfiguracion.Controls.Add(fieldTextoRecordarConfiguracionBd, 1, 0);
            layoutRecordarConfiguracion.Controls.Add(fieldRecordarConfiguracionBd, 0, 0);
            layoutRecordarConfiguracion.Dock = DockStyle.Fill;
            layoutRecordarConfiguracion.Location = new Point(35, 430);
            layoutRecordarConfiguracion.Margin = new Padding(15, 0, 0, 0);
            layoutRecordarConfiguracion.Name = "layoutRecordarConfiguracion";
            layoutRecordarConfiguracion.RowCount = 1;
            layoutRecordarConfiguracion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutRecordarConfiguracion.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutRecordarConfiguracion.Size = new Size(443, 45);
            layoutRecordarConfiguracion.TabIndex = 25;
            // 
            // fieldTextoRecordarConfiguracionBd
            // 
            fieldTextoRecordarConfiguracionBd.Dock = DockStyle.Fill;
            fieldTextoRecordarConfiguracionBd.Font = new Font("Segoe UI", 11.25F);
            fieldTextoRecordarConfiguracionBd.ForeColor = Color.Black;
            fieldTextoRecordarConfiguracionBd.ImeMode = ImeMode.NoControl;
            fieldTextoRecordarConfiguracionBd.Location = new Point(31, 5);
            fieldTextoRecordarConfiguracionBd.Margin = new Padding(5, 5, 1, 1);
            fieldTextoRecordarConfiguracionBd.Name = "fieldTextoRecordarConfiguracionBd";
            fieldTextoRecordarConfiguracionBd.Size = new Size(411, 39);
            fieldTextoRecordarConfiguracionBd.TabIndex = 1;
            fieldTextoRecordarConfiguracionBd.Text = "Recordar la configuración para el próximo inicio";
            // 
            // fieldRecordarConfiguracionBd
            // 
            fieldRecordarConfiguracionBd.BackColor = Color.White;
            fieldRecordarConfiguracionBd.CheckedState.BorderColor = Color.Gainsboro;
            fieldRecordarConfiguracionBd.CheckedState.BorderRadius = 4;
            fieldRecordarConfiguracionBd.CheckedState.BorderThickness = 1;
            fieldRecordarConfiguracionBd.CheckedState.FillColor = Color.WhiteSmoke;
            fieldRecordarConfiguracionBd.CheckMarkColor = Color.Black;
            fieldRecordarConfiguracionBd.Dock = DockStyle.Top;
            fieldRecordarConfiguracionBd.Font = new Font("Segoe UI", 12F);
            fieldRecordarConfiguracionBd.Location = new Point(5, 5);
            fieldRecordarConfiguracionBd.Margin = new Padding(5, 5, 5, 15);
            fieldRecordarConfiguracionBd.Name = "fieldRecordarConfiguracionBd";
            fieldRecordarConfiguracionBd.Size = new Size(16, 25);
            fieldRecordarConfiguracionBd.TabIndex = 0;
            fieldRecordarConfiguracionBd.UncheckedState.BorderColor = Color.Gainsboro;
            fieldRecordarConfiguracionBd.UncheckedState.BorderRadius = 4;
            fieldRecordarConfiguracionBd.UncheckedState.BorderThickness = 1;
            fieldRecordarConfiguracionBd.UncheckedState.FillColor = Color.PeachPuff;
            fieldRecordarConfiguracionBd.UseVisualStyleBackColor = false;
            // 
            // fieldNombreUsuario
            // 
            fieldNombreUsuario.Animated = true;
            fieldNombreUsuario.AutoRoundedCorners = true;
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
            fieldNombreUsuario.IconLeft = (Image)resources.GetObject("fieldNombreUsuario.IconLeft");
            fieldNombreUsuario.IconLeftOffset = new Point(10, 0);
            fieldNombreUsuario.IconRightOffset = new Point(10, 0);
            fieldNombreUsuario.Location = new Point(25, 335);
            fieldNombreUsuario.Margin = new Padding(5);
            fieldNombreUsuario.Name = "fieldNombreUsuario";
            fieldNombreUsuario.PasswordChar = '\0';
            fieldNombreUsuario.PlaceholderForeColor = Color.DimGray;
            fieldNombreUsuario.PlaceholderText = "Nombre de usuario";
            fieldNombreUsuario.SelectedText = "";
            fieldNombreUsuario.ShadowDecoration.CustomizableEdges = customizableEdges10;
            fieldNombreUsuario.Size = new Size(448, 35);
            fieldNombreUsuario.TabIndex = 26;
            fieldNombreUsuario.TextOffset = new Point(5, 0);
            // 
            // fieldPassword
            // 
            fieldPassword.Animated = true;
            fieldPassword.AutoRoundedCorners = true;
            fieldPassword.BorderColor = Color.Gainsboro;
            fieldPassword.BorderRadius = 16;
            fieldPassword.Cursor = Cursors.IBeam;
            fieldPassword.CustomizableEdges = customizableEdges11;
            fieldPassword.DefaultText = "";
            fieldPassword.DisabledState.BorderColor = Color.White;
            fieldPassword.DisabledState.ForeColor = Color.DimGray;
            fieldPassword.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldPassword.Dock = DockStyle.Fill;
            fieldPassword.FocusedState.BorderColor = Color.SandyBrown;
            fieldPassword.Font = new Font("Segoe UI", 11.25F);
            fieldPassword.ForeColor = Color.Black;
            fieldPassword.HoverState.BorderColor = Color.SandyBrown;
            fieldPassword.IconLeft = (Image)resources.GetObject("fieldPassword.IconLeft");
            fieldPassword.IconLeftOffset = new Point(10, 0);
            fieldPassword.IconRight = Properties.Resources.closed_eye_20px;
            fieldPassword.IconRightOffset = new Point(10, 0);
            fieldPassword.Location = new Point(25, 390);
            fieldPassword.Margin = new Padding(5);
            fieldPassword.Name = "fieldPassword";
            fieldPassword.PasswordChar = '●';
            fieldPassword.PlaceholderForeColor = Color.DimGray;
            fieldPassword.PlaceholderText = "Contraseña";
            fieldPassword.SelectedText = "";
            fieldPassword.ShadowDecoration.CustomizableEdges = customizableEdges12;
            fieldPassword.Size = new Size(448, 35);
            fieldPassword.TabIndex = 27;
            fieldPassword.TextOffset = new Point(5, 0);
            fieldPassword.UseSystemPasswordChar = true;
            // 
            // infoIcon
            // 
            infoIcon.BorderColor = Color.Transparent;
            infoIcon.BorderRadius = 16;
            infoIcon.BorderThickness = 0;
            infoIcon.FillColor = Color.LightBlue;
            infoIcon.Font = new Font("Bodoni MT", 16F, FontStyle.Bold);
            infoIcon.ForeColor = Color.SteelBlue;
            infoIcon.Offset = new Point(0, 50);
            infoIcon.Size = new Size(30, 30);
            infoIcon.TargetControl = layoutHelp;
            infoIcon.Text = "i";
            // 
            // VistaConfiguracionBaseDatos
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 685);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaConfiguracionBaseDatos";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaAutenticacionUsuario";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            layoutHelp.ResumeLayout(false);
            layoutRecordarConfiguracion.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private Label fieldTitulo;
        private Label fieldCopyright;
        private Guna2TextBox fieldDireccionServidor;
        private Guna2TextBox fieldNombreBd;
        private Guna2Button btnValidarConexion;
        private TableLayoutPanel layoutHelp;
        private Guna2Button fieldInformacion;
        private Guna2NotificationPaint infoIcon;
        private TableLayoutPanel layoutRecordarConfiguracion;
        private Label fieldTextoRecordarConfiguracionBd;
        private Guna2CheckBox fieldRecordarConfiguracionBd;
        private Guna2TextBox fieldNombreUsuario;
        private Guna2TextBox fieldPassword;
    }
}