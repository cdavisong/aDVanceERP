using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Core.Seguridad.MVP.Vistas.CuentaUsuario {
    partial class VistaRegistroCuentaUsuario {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaRegistroCuentaUsuario));
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
            fieldNombreUsuario = new Guna2TextBox();
            layoutTitulo = new TableLayoutPanel();
            btnCerrar = new Guna2Button();
            fieldTitulo = new Label();
            fieldPassword = new Guna2TextBox();
            fieldConfirmarPassword = new Guna2TextBox();
            fieldTituloNombreRolUsuario = new Label();
            fieldNombreRolUsuario = new Guna2ComboBox();
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
            layoutVista.Controls.Add(fieldPassword, 2, 6);
            layoutVista.Controls.Add(fieldConfirmarPassword, 2, 8);
            layoutVista.Controls.Add(fieldTituloNombreRolUsuario, 2, 10);
            layoutVista.Controls.Add(fieldNombreRolUsuario, 2, 11);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(13, 0);
            layoutVista.Margin = new Padding(3, 0, 0, 0);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 14;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
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
            fieldNombreUsuario.Location = new Point(55, 135);
            fieldNombreUsuario.Margin = new Padding(5);
            fieldNombreUsuario.Name = "fieldNombreUsuario";
            fieldNombreUsuario.PasswordChar = '\0';
            fieldNombreUsuario.PlaceholderForeColor = Color.DimGray;
            fieldNombreUsuario.PlaceholderText = "Nombre del usuario";
            fieldNombreUsuario.SelectedText = "";
            fieldNombreUsuario.ShadowDecoration.CustomizableEdges = customizableEdges2;
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
            btnCerrar.CustomizableEdges = customizableEdges3;
            btnCerrar.Dock = DockStyle.Fill;
            btnCerrar.FillColor = Color.White;
            btnCerrar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.Black;
            btnCerrar.HoverState.FillColor = Color.White;
            btnCerrar.Image = (Image) resources.GetObject("btnCerrar.Image");
            btnCerrar.Location = new Point(370, 3);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.ShadowDecoration.CustomizableEdges = customizableEdges4;
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
            fieldTitulo.Text = "Usuario";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
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
            fieldPassword.IconLeft = (Image) resources.GetObject("fieldPassword.IconLeft");
            fieldPassword.IconLeftOffset = new Point(10, 0);
            fieldPassword.IconRight = Properties.Resources.closed_eye_20px;
            fieldPassword.IconRightOffset = new Point(10, 0);
            fieldPassword.Location = new Point(55, 190);
            fieldPassword.Margin = new Padding(5);
            fieldPassword.Name = "fieldPassword";
            fieldPassword.PasswordChar = '●';
            fieldPassword.PlaceholderForeColor = Color.DimGray;
            fieldPassword.PlaceholderText = "Contraseña";
            fieldPassword.SelectedText = "";
            fieldPassword.ShadowDecoration.CustomizableEdges = customizableEdges6;
            fieldPassword.Size = new Size(407, 35);
            fieldPassword.TabIndex = 15;
            fieldPassword.TextOffset = new Point(5, 0);
            fieldPassword.UseSystemPasswordChar = true;
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
            fieldConfirmarPassword.IconLeft = (Image) resources.GetObject("fieldConfirmarPassword.IconLeft");
            fieldConfirmarPassword.IconLeftOffset = new Point(10, 0);
            fieldConfirmarPassword.IconRightOffset = new Point(10, 0);
            fieldConfirmarPassword.Location = new Point(55, 245);
            fieldConfirmarPassword.Margin = new Padding(5);
            fieldConfirmarPassword.Name = "fieldConfirmarPassword";
            fieldConfirmarPassword.PasswordChar = '●';
            fieldConfirmarPassword.PlaceholderForeColor = Color.DimGray;
            fieldConfirmarPassword.PlaceholderText = "Confirme la contraseña";
            fieldConfirmarPassword.SelectedText = "";
            fieldConfirmarPassword.ShadowDecoration.CustomizableEdges = customizableEdges8;
            fieldConfirmarPassword.Size = new Size(407, 35);
            fieldConfirmarPassword.TabIndex = 16;
            fieldConfirmarPassword.TextOffset = new Point(5, 0);
            fieldConfirmarPassword.UseSystemPasswordChar = true;
            // 
            // fieldTituloNombreRolUsuario
            // 
            fieldTituloNombreRolUsuario.Dock = DockStyle.Fill;
            fieldTituloNombreRolUsuario.Font = new Font("Segoe UI", 11.25F);
            fieldTituloNombreRolUsuario.ForeColor = Color.DimGray;
            fieldTituloNombreRolUsuario.Image = (Image) resources.GetObject("fieldTituloNombreRolUsuario.Image");
            fieldTituloNombreRolUsuario.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloNombreRolUsuario.ImeMode = ImeMode.NoControl;
            fieldTituloNombreRolUsuario.Location = new Point(65, 300);
            fieldTituloNombreRolUsuario.Margin = new Padding(15, 5, 3, 3);
            fieldTituloNombreRolUsuario.Name = "fieldTituloNombreRolUsuario";
            fieldTituloNombreRolUsuario.Size = new Size(399, 27);
            fieldTituloNombreRolUsuario.TabIndex = 25;
            fieldTituloNombreRolUsuario.Text = "      Asignar un rol de usuario :";
            fieldTituloNombreRolUsuario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldNombreRolUsuario
            // 
            fieldNombreRolUsuario.Animated = true;
            fieldNombreRolUsuario.BackColor = Color.Transparent;
            fieldNombreRolUsuario.BorderColor = Color.Gainsboro;
            fieldNombreRolUsuario.BorderRadius = 16;
            fieldNombreRolUsuario.CustomizableEdges = customizableEdges9;
            fieldNombreRolUsuario.Dock = DockStyle.Fill;
            fieldNombreRolUsuario.DrawMode = DrawMode.OwnerDrawFixed;
            fieldNombreRolUsuario.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldNombreRolUsuario.FocusedColor = Color.SandyBrown;
            fieldNombreRolUsuario.FocusedState.BorderColor = Color.SandyBrown;
            fieldNombreRolUsuario.Font = new Font("Segoe UI", 11.25F);
            fieldNombreRolUsuario.ForeColor = Color.Black;
            fieldNombreRolUsuario.ItemHeight = 29;
            fieldNombreRolUsuario.Location = new Point(55, 335);
            fieldNombreRolUsuario.Margin = new Padding(5);
            fieldNombreRolUsuario.Name = "fieldNombreRolUsuario";
            fieldNombreRolUsuario.ShadowDecoration.CustomizableEdges = customizableEdges10;
            fieldNombreRolUsuario.Size = new Size(407, 35);
            fieldNombreRolUsuario.TabIndex = 26;
            fieldNombreRolUsuario.TextOffset = new Point(10, 0);
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
            btnRegistrar.FillColor = Color.PeachPuff;
            btnRegistrar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.Black;
            btnRegistrar.Location = new Point(53, 3);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnRegistrar.Size = new Size(243, 39);
            btnRegistrar.TabIndex = 15;
            btnRegistrar.Text = "Registrar usuario";
            // 
            // VistaRegistroCuentaUsuario
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 685);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaRegistroCuentaUsuario";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaRegistroUsuario";
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
        private TableLayoutPanel layoutBotones;
        private Guna2Button btnSalir;
        private Guna2Button btnRegistrar;
        private Guna2TextBox fieldConfirmarPassword;
        private Label fieldTituloNombreRolUsuario;
        private Guna2ComboBox fieldNombreRolUsuario;
        private Guna2TextBox fieldPassword;
    }
}