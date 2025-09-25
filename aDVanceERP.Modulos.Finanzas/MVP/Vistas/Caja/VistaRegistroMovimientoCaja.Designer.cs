namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja {
    partial class VistaRegistroMovimientoCaja {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaRegistroMovimientoCaja));
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldIcono = new PictureBox();
            fieldSubtitulo = new Label();
            fieldMonto = new Guna.UI2.WinForms.Guna2TextBox();
            layoutTitulo = new TableLayoutPanel();
            btnCerrar = new Guna.UI2.WinForms.Guna2Button();
            fieldTitulo = new Label();
            fieldTituloTipoMovimiento = new Label();
            fieldTipoMovimiento = new Guna.UI2.WinForms.Guna2ComboBox();
            fieldConcepto = new Guna.UI2.WinForms.Guna2TextBox();
            fieldObservaciones = new Guna.UI2.WinForms.Guna2TextBox();
            layoutBotones = new TableLayoutPanel();
            btnSalir = new Guna.UI2.WinForms.Guna2Button();
            btnRegistrar = new Guna.UI2.WinForms.Guna2Button();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) fieldIcono).BeginInit();
            layoutTitulo.SuspendLayout();
            layoutBotones.SuspendLayout();
            SuspendLayout();
            // 
            // formatoBase
            // 
            formatoBase.AnimateWindow = true;
            formatoBase.AnimationType = Guna.UI2.WinForms.Guna2BorderlessForm.AnimateWindowType.AW_HOR_NEGATIVE;
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
            layoutBase.TabIndex = 3;
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
            layoutVista.Controls.Add(fieldMonto, 2, 4);
            layoutVista.Controls.Add(layoutTitulo, 2, 1);
            layoutVista.Controls.Add(fieldTituloTipoMovimiento, 2, 6);
            layoutVista.Controls.Add(fieldTipoMovimiento, 2, 7);
            layoutVista.Controls.Add(fieldConcepto, 2, 9);
            layoutVista.Controls.Add(fieldObservaciones, 2, 11);
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
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
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
            fieldSubtitulo.Text = "Movimiento de efectivo en caja con fecha 00/00/0000";
            // 
            // fieldMonto
            // 
            fieldMonto.Animated = true;
            fieldMonto.AutoRoundedCorners = true;
            fieldMonto.BorderColor = Color.Gainsboro;
            fieldMonto.BorderRadius = 16;
            fieldMonto.Cursor = Cursors.IBeam;
            fieldMonto.CustomizableEdges = customizableEdges15;
            fieldMonto.DefaultText = "";
            fieldMonto.DisabledState.BorderColor = Color.White;
            fieldMonto.DisabledState.ForeColor = Color.DimGray;
            fieldMonto.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldMonto.Dock = DockStyle.Right;
            fieldMonto.FocusedState.BorderColor = Color.SandyBrown;
            fieldMonto.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldMonto.ForeColor = Color.Black;
            fieldMonto.HoverState.BorderColor = Color.SandyBrown;
            fieldMonto.IconLeftOffset = new Point(10, 0);
            fieldMonto.IconRight = (Image) resources.GetObject("fieldMonto.IconRight");
            fieldMonto.IconRightOffset = new Point(6, 0);
            fieldMonto.IconRightSize = new Size(12, 12);
            fieldMonto.Location = new Point(232, 135);
            fieldMonto.Margin = new Padding(5);
            fieldMonto.Name = "fieldMonto";
            fieldMonto.PasswordChar = '\0';
            fieldMonto.PlaceholderForeColor = Color.DimGray;
            fieldMonto.PlaceholderText = "Monto de dinero";
            fieldMonto.SelectedText = "";
            fieldMonto.ShadowDecoration.CustomizableEdges = customizableEdges16;
            fieldMonto.Size = new Size(230, 35);
            fieldMonto.TabIndex = 1;
            fieldMonto.TextAlign = HorizontalAlignment.Right;
            fieldMonto.TextOffset = new Point(5, 0);
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
            btnCerrar.CustomizableEdges = customizableEdges17;
            btnCerrar.Dock = DockStyle.Fill;
            btnCerrar.FillColor = Color.White;
            btnCerrar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.Black;
            btnCerrar.HoverState.FillColor = Color.White;
            btnCerrar.Image = (Image) resources.GetObject("btnCerrar.Image");
            btnCerrar.Location = new Point(370, 3);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.ShadowDecoration.CustomizableEdges = customizableEdges18;
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
            fieldTitulo.Text = "Movimiento de caja";
            fieldTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTituloTipoMovimiento
            // 
            fieldTituloTipoMovimiento.Dock = DockStyle.Fill;
            fieldTituloTipoMovimiento.Font = new Font("Segoe UI", 11.25F);
            fieldTituloTipoMovimiento.ForeColor = Color.DimGray;
            fieldTituloTipoMovimiento.Image = (Image) resources.GetObject("fieldTituloTipoMovimiento.Image");
            fieldTituloTipoMovimiento.ImageAlign = ContentAlignment.MiddleLeft;
            fieldTituloTipoMovimiento.ImeMode = ImeMode.NoControl;
            fieldTituloTipoMovimiento.Location = new Point(65, 190);
            fieldTituloTipoMovimiento.Margin = new Padding(15, 5, 3, 3);
            fieldTituloTipoMovimiento.Name = "fieldTituloTipoMovimiento";
            fieldTituloTipoMovimiento.Size = new Size(399, 27);
            fieldTituloTipoMovimiento.TabIndex = 34;
            fieldTituloTipoMovimiento.Text = "      Tipo de movimiento :";
            fieldTituloTipoMovimiento.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldTipoMovimiento
            // 
            fieldTipoMovimiento.Animated = true;
            fieldTipoMovimiento.BackColor = Color.Transparent;
            fieldTipoMovimiento.BorderColor = Color.Gainsboro;
            fieldTipoMovimiento.BorderRadius = 16;
            fieldTipoMovimiento.CustomizableEdges = customizableEdges19;
            fieldTipoMovimiento.Dock = DockStyle.Fill;
            fieldTipoMovimiento.DrawMode = DrawMode.OwnerDrawFixed;
            fieldTipoMovimiento.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldTipoMovimiento.FocusedColor = Color.SandyBrown;
            fieldTipoMovimiento.FocusedState.BorderColor = Color.SandyBrown;
            fieldTipoMovimiento.Font = new Font("Segoe UI", 11.25F);
            fieldTipoMovimiento.ForeColor = Color.Black;
            fieldTipoMovimiento.ItemHeight = 29;
            fieldTipoMovimiento.Items.AddRange(new object[] { "Ingreso", "Egreso" });
            fieldTipoMovimiento.Location = new Point(55, 225);
            fieldTipoMovimiento.Margin = new Padding(5);
            fieldTipoMovimiento.Name = "fieldTipoMovimiento";
            fieldTipoMovimiento.ShadowDecoration.CustomizableEdges = customizableEdges20;
            fieldTipoMovimiento.Size = new Size(407, 35);
            fieldTipoMovimiento.StartIndex = 0;
            fieldTipoMovimiento.TabIndex = 35;
            fieldTipoMovimiento.TextOffset = new Point(10, 0);
            // 
            // fieldConcepto
            // 
            fieldConcepto.Animated = true;
            fieldConcepto.BorderColor = Color.Gainsboro;
            fieldConcepto.BorderRadius = 16;
            fieldConcepto.Cursor = Cursors.IBeam;
            fieldConcepto.CustomizableEdges = customizableEdges21;
            fieldConcepto.DefaultText = "";
            fieldConcepto.DisabledState.BorderColor = Color.White;
            fieldConcepto.DisabledState.ForeColor = Color.DimGray;
            fieldConcepto.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldConcepto.Dock = DockStyle.Fill;
            fieldConcepto.FocusedState.BorderColor = Color.SandyBrown;
            fieldConcepto.Font = new Font("Segoe UI", 11.25F);
            fieldConcepto.ForeColor = Color.Black;
            fieldConcepto.HoverState.BorderColor = Color.SandyBrown;
            fieldConcepto.IconLeft = (Image) resources.GetObject("fieldConcepto.IconLeft");
            fieldConcepto.IconLeftOffset = new Point(10, 0);
            fieldConcepto.Location = new Point(55, 280);
            fieldConcepto.Margin = new Padding(5);
            fieldConcepto.Name = "fieldConcepto";
            fieldConcepto.PasswordChar = '\0';
            fieldConcepto.PlaceholderForeColor = Color.DimGray;
            fieldConcepto.PlaceholderText = "Concepto del movimiento";
            fieldConcepto.SelectedText = "";
            fieldConcepto.ShadowDecoration.CustomizableEdges = customizableEdges22;
            fieldConcepto.Size = new Size(407, 35);
            fieldConcepto.TabIndex = 36;
            fieldConcepto.TextOffset = new Point(5, 0);
            // 
            // fieldObservaciones
            // 
            fieldObservaciones.Animated = true;
            fieldObservaciones.BorderColor = Color.Gainsboro;
            fieldObservaciones.BorderRadius = 16;
            fieldObservaciones.Cursor = Cursors.IBeam;
            fieldObservaciones.CustomizableEdges = customizableEdges23;
            fieldObservaciones.DefaultText = "";
            fieldObservaciones.DisabledState.BorderColor = Color.White;
            fieldObservaciones.DisabledState.ForeColor = Color.DimGray;
            fieldObservaciones.DisabledState.PlaceholderForeColor = Color.DimGray;
            fieldObservaciones.Dock = DockStyle.Fill;
            fieldObservaciones.FocusedState.BorderColor = Color.SandyBrown;
            fieldObservaciones.Font = new Font("Segoe UI", 11.25F);
            fieldObservaciones.ForeColor = Color.Black;
            fieldObservaciones.HoverState.BorderColor = Color.SandyBrown;
            fieldObservaciones.IconLeft = (Image) resources.GetObject("fieldObservaciones.IconLeft");
            fieldObservaciones.IconLeftOffset = new Point(10, -11);
            fieldObservaciones.Location = new Point(55, 335);
            fieldObservaciones.Margin = new Padding(5);
            fieldObservaciones.Multiline = true;
            fieldObservaciones.Name = "fieldObservaciones";
            fieldObservaciones.PasswordChar = '\0';
            fieldObservaciones.PlaceholderForeColor = Color.DimGray;
            fieldObservaciones.PlaceholderText = "Observaciones";
            fieldObservaciones.SelectedText = "";
            fieldObservaciones.ShadowDecoration.CustomizableEdges = customizableEdges24;
            fieldObservaciones.Size = new Size(407, 62);
            fieldObservaciones.TabIndex = 37;
            fieldObservaciones.TextOffset = new Point(5, 0);
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
            btnSalir.CustomizableEdges = customizableEdges25;
            btnSalir.Dock = DockStyle.Fill;
            btnSalir.FillColor = Color.White;
            btnSalir.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSalir.ForeColor = Color.Gainsboro;
            btnSalir.HoverState.BorderColor = Color.PeachPuff;
            btnSalir.HoverState.FillColor = Color.PeachPuff;
            btnSalir.HoverState.ForeColor = Color.Black;
            btnSalir.Location = new Point(302, 3);
            btnSalir.Name = "btnSalir";
            btnSalir.ShadowDecoration.CustomizableEdges = customizableEdges26;
            btnSalir.Size = new Size(160, 39);
            btnSalir.TabIndex = 14;
            btnSalir.Text = "Salir";
            // 
            // btnRegistrar
            // 
            btnRegistrar.Animated = true;
            btnRegistrar.BorderRadius = 18;
            btnRegistrar.CustomizableEdges = customizableEdges27;
            btnRegistrar.Dock = DockStyle.Fill;
            btnRegistrar.FillColor = Color.PeachPuff;
            btnRegistrar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.Black;
            btnRegistrar.Location = new Point(53, 3);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.ShadowDecoration.CustomizableEdges = customizableEdges28;
            btnRegistrar.Size = new Size(243, 39);
            btnRegistrar.TabIndex = 15;
            btnRegistrar.Text = "Registrar movimiento";
            // 
            // VistaRegistroMovimientoCaja
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(500, 685);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "VistaRegistroMovimientoCaja";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaRegistroMovimientoCaja";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) fieldIcono).EndInit();
            layoutTitulo.ResumeLayout(false);
            layoutBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private PictureBox fieldIcono;
        private Label fieldSubtitulo;
        private TableLayoutPanel layoutTitulo;
        private Guna.UI2.WinForms.Guna2Button btnCerrar;
        private Label fieldTitulo;
        private TableLayoutPanel layoutBotones;
        private Guna.UI2.WinForms.Guna2Button btnSalir;
        private Guna.UI2.WinForms.Guna2Button btnRegistrar;
        private Guna.UI2.WinForms.Guna2TextBox fieldMonto;
        private Label fieldTituloTipoMovimiento;
        private Guna.UI2.WinForms.Guna2ComboBox fieldTipoMovimiento;
        private Guna.UI2.WinForms.Guna2TextBox fieldConcepto;
        private Guna.UI2.WinForms.Guna2TextBox fieldObservaciones;
    }
}