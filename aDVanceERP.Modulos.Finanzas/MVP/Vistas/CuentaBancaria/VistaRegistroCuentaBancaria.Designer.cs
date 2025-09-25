using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.CuentaBancaria {
    partial class VistaRegistroCuentaBancaria {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaRegistroCuentaBancaria));
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
            fieldNombrePropietario = new Guna2ComboBox();
            fieldSubtitulo = new Label();
            layoutTitulo = new TableLayoutPanel();
            btnCerrar = new Guna2Button();
            fieldTitulo = new Label();
            fieldTituloNombrePropietario = new Label();
            fieldAlias = new Guna2TextBox();
            layoutTarjetaMoneda = new TableLayoutPanel();
            fieldTipoMoneda = new Guna2ComboBox();
            fieldNumeroCuenta = new Guna2TextBox();
            layoutBotones = new TableLayoutPanel();
            btnSalir = new Guna2Button();
            btnRegistrar = new Guna2Button();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            ((ISupportInitialize)fieldIcono).BeginInit();
            layoutTitulo.SuspendLayout();
            layoutTarjetaMoneda.SuspendLayout();
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
            layoutVista.Controls.Add(fieldNombrePropietario, 2, 9);
            layoutVista.Controls.Add(fieldSubtitulo, 2, 2);
            layoutVista.Controls.Add(layoutTitulo, 2, 1);
            layoutVista.Controls.Add(fieldTituloNombrePropietario, 2, 8);
            layoutVista.Controls.Add(fieldAlias, 2, 4);
            layoutVista.Controls.Add(layoutTarjetaMoneda, 2, 6);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(13, 0);
            layoutVista.Margin = new Padding(3, 0, 0, 0);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 12;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.Size = new Size(487, 620);
            layoutVista.TabIndex = 0;
            // 
            // fieldIcono
            // 
            fieldIcono.BackgroundImage = (Image)resources.GetObject("fieldIcono.BackgroundImage");
            fieldIcono.BackgroundImageLayout = ImageLayout.Center;
            fieldIcono.Dock = DockStyle.Fill;
            fieldIcono.Location = new Point(20, 26);
            fieldIcono.Margin = new Padding(0, 6, 0, 0);
            fieldIcono.Name = "fieldIcono";
            fieldIcono.Size = new Size(30, 39);
            fieldIcono.TabIndex = 0;
            fieldIcono.TabStop = false;
            // 
            // fieldNombrePropietario
            // 
            fieldNombrePropietario.Animated = true;
            fieldNombrePropietario.BackColor = Color.Transparent;
            fieldNombrePropietario.BorderColor = Color.Gainsboro;
            fieldNombrePropietario.BorderRadius = 16;
            fieldNombrePropietario.CustomizableEdges = customizableEdges1;
            fieldNombrePropietario.Dock = DockStyle.Fill;
            fieldNombrePropietario.DrawMode = DrawMode.OwnerDrawFixed;
            fieldNombrePropietario.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldNombrePropietario.FocusedColor = Color.SandyBrown;
            fieldNombrePropietario.FocusedState.BorderColor = Color.SandyBrown;
            fieldNombrePropietario.Font = new Font("Segoe UI", 11.25F);
            fieldNombrePropietario.ForeColor = Color.Black;
            fieldNombrePropietario.ItemHeight = 29;
            fieldNombrePropietario.Location = new Point(55, 280);
            fieldNombrePropietario.Margin = new Padding(5);
            fieldNombrePropietario.Name = "fieldNombrePropietario";
            fieldNombrePropietario.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fieldNombrePropietario.Size = new Size(407, 35);
            fieldNombrePropietario.TabIndex = 32;
            fieldNombrePropietario.TextOffset = new Point(10, 0);
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
            btnCerrar.CustomizableEdges = customizableEdges3;
            btnCerrar.Dock = DockStyle.Fill;
            btnCerrar.FillColor = Color.White;
            btnCerrar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.Black;
            btnCerrar.HoverState.FillColor = Color.White;
            btnCerrar.Image = (Image)resources.GetObject("btnCerrar.Image");
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
            fieldTitulo.Text = "Cuenta bancaia";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloNombrePropietario
            // 
            fieldTituloNombrePropietario.Dock = DockStyle.Fill;
            fieldTituloNombrePropietario.Font = new Font("Segoe UI", 11.25F);
            fieldTituloNombrePropietario.ForeColor = Color.DimGray;
            fieldTituloNombrePropietario.Image = (Image)resources.GetObject("fieldTituloNombrePropietario.Image");
            fieldTituloNombrePropietario.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloNombrePropietario.ImeMode = ImeMode.NoControl;
            fieldTituloNombrePropietario.Location = new Point(65, 245);
            fieldTituloNombrePropietario.Margin = new Padding(15, 5, 3, 3);
            fieldTituloNombrePropietario.Name = "fieldTituloNombrePropietario";
            fieldTituloNombrePropietario.Size = new Size(399, 27);
            fieldTituloNombrePropietario.TabIndex = 33;
            fieldTituloNombrePropietario.Text = "      Propietario :";
            fieldTituloNombrePropietario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldAlias
            // 
            fieldAlias.Animated = true;
            fieldAlias.BorderColor = Color.Gainsboro;
            fieldAlias.BorderRadius = 16;
            fieldAlias.Cursor = Cursors.IBeam;
            fieldAlias.CustomizableEdges = customizableEdges5;
            fieldAlias.DefaultText = "";
            fieldAlias.DisabledState.BorderColor = Color.White;
            fieldAlias.DisabledState.ForeColor = Color.DimGray;
            fieldAlias.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldAlias.Dock = DockStyle.Fill;
            fieldAlias.FocusedState.BorderColor = Color.SandyBrown;
            fieldAlias.Font = new Font("Segoe UI", 11.25F);
            fieldAlias.ForeColor = Color.Black;
            fieldAlias.HoverState.BorderColor = Color.SandyBrown;
            fieldAlias.IconLeft = (Image)resources.GetObject("fieldAlias.IconLeft");
            fieldAlias.IconLeftOffset = new Point(10, 0);
            fieldAlias.Location = new Point(55, 135);
            fieldAlias.Margin = new Padding(5);
            fieldAlias.Name = "fieldAlias";
            fieldAlias.PasswordChar = '\0';
            fieldAlias.PlaceholderForeColor = Color.DimGray;
            fieldAlias.PlaceholderText = "Alias o identificador";
            fieldAlias.SelectedText = "";
            fieldAlias.ShadowDecoration.CustomizableEdges = customizableEdges6;
            fieldAlias.Size = new Size(407, 35);
            fieldAlias.TabIndex = 35;
            fieldAlias.TextOffset = new Point(5, 0);
            // 
            // layoutTarjetaMoneda
            // 
            layoutTarjetaMoneda.ColumnCount = 2;
            layoutTarjetaMoneda.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTarjetaMoneda.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            layoutTarjetaMoneda.Controls.Add(fieldTipoMoneda, 1, 0);
            layoutTarjetaMoneda.Controls.Add(fieldNumeroCuenta, 0, 0);
            layoutTarjetaMoneda.Dock = DockStyle.Fill;
            layoutTarjetaMoneda.Location = new Point(50, 185);
            layoutTarjetaMoneda.Margin = new Padding(0);
            layoutTarjetaMoneda.Name = "layoutTarjetaMoneda";
            layoutTarjetaMoneda.RowCount = 1;
            layoutTarjetaMoneda.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTarjetaMoneda.Size = new Size(417, 45);
            layoutTarjetaMoneda.TabIndex = 31;
            // 
            // fieldTipoMoneda
            // 
            fieldTipoMoneda.Animated = true;
            fieldTipoMoneda.BackColor = Color.Transparent;
            fieldTipoMoneda.BorderColor = Color.Gainsboro;
            fieldTipoMoneda.BorderRadius = 16;
            fieldTipoMoneda.CustomizableEdges = customizableEdges7;
            fieldTipoMoneda.Dock = DockStyle.Fill;
            fieldTipoMoneda.DrawMode = DrawMode.OwnerDrawFixed;
            fieldTipoMoneda.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldTipoMoneda.FocusedColor = Color.SandyBrown;
            fieldTipoMoneda.FocusedState.BorderColor = Color.SandyBrown;
            fieldTipoMoneda.Font = new Font("Segoe UI", 11.25F);
            fieldTipoMoneda.ForeColor = Color.Black;
            fieldTipoMoneda.ItemHeight = 29;
            fieldTipoMoneda.Location = new Point(327, 5);
            fieldTipoMoneda.Margin = new Padding(5);
            fieldTipoMoneda.Name = "fieldTipoMoneda";
            fieldTipoMoneda.ShadowDecoration.CustomizableEdges = customizableEdges8;
            fieldTipoMoneda.Size = new Size(85, 35);
            fieldTipoMoneda.TabIndex = 34;
            fieldTipoMoneda.TextOffset = new Point(10, 0);
            // 
            // fieldNumeroCuenta
            // 
            fieldNumeroCuenta.Animated = true;
            fieldNumeroCuenta.BorderColor = Color.Gainsboro;
            fieldNumeroCuenta.BorderRadius = 16;
            fieldNumeroCuenta.Cursor = Cursors.IBeam;
            fieldNumeroCuenta.CustomizableEdges = customizableEdges9;
            fieldNumeroCuenta.DefaultText = "";
            fieldNumeroCuenta.DisabledState.BorderColor = Color.White;
            fieldNumeroCuenta.DisabledState.ForeColor = Color.DimGray;
            fieldNumeroCuenta.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldNumeroCuenta.Dock = DockStyle.Fill;
            fieldNumeroCuenta.FocusedState.BorderColor = Color.SandyBrown;
            fieldNumeroCuenta.Font = new Font("Segoe UI", 11.25F);
            fieldNumeroCuenta.ForeColor = Color.Black;
            fieldNumeroCuenta.HoverState.BorderColor = Color.SandyBrown;
            fieldNumeroCuenta.IconLeft = (Image)resources.GetObject("fieldNumeroCuenta.IconLeft");
            fieldNumeroCuenta.IconLeftOffset = new Point(10, 0);
            fieldNumeroCuenta.Location = new Point(5, 5);
            fieldNumeroCuenta.Margin = new Padding(5);
            fieldNumeroCuenta.Name = "fieldNumeroCuenta";
            fieldNumeroCuenta.PasswordChar = '\0';
            fieldNumeroCuenta.PlaceholderForeColor = Color.DimGray;
            fieldNumeroCuenta.PlaceholderText = "Tarjeta o cuenta del destinatario";
            fieldNumeroCuenta.SelectedText = "";
            fieldNumeroCuenta.ShadowDecoration.CustomizableEdges = customizableEdges10;
            fieldNumeroCuenta.Size = new Size(312, 35);
            fieldNumeroCuenta.TabIndex = 36;
            fieldNumeroCuenta.TextOffset = new Point(5, 0);
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
            btnRegistrar.Text = "Registrar cuenta";
            // 
            // VistaRegistroCuentaBancaria
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 685);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaRegistroCuentaBancaria";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaRegistroCuenta";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ((ISupportInitialize)fieldIcono).EndInit();
            layoutTitulo.ResumeLayout(false);
            layoutTarjetaMoneda.ResumeLayout(false);
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
        private Guna2ComboBox fieldNombrePropietario;
        private TableLayoutPanel layoutTarjetaMoneda;
        private Label fieldTituloNombrePropietario;
        private Guna2ComboBox fieldTipoMoneda;
        private Guna2TextBox fieldAlias;
        private Guna2TextBox fieldNumeroCuenta;
    }
}