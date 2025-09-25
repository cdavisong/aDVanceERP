using Guna.UI2.WinForms;

namespace aDVanceERP.Desktop.MVP.Vistas.Modulos {
    partial class VistaModulos {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges29 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges30 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges31 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges32 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaModulos));
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutDistribucion = new TableLayoutPanel();
            layoutMenuLateral = new TableLayoutPanel();
            layoutModulos = new FlowLayoutPanel();
            btnInicio = new Guna2CircleButton();
            btnEstadisticas = new Guna2CircleButton();
            btnModuloContactos = new Guna2CircleButton();
            btnModuloFinanzas = new Guna2CircleButton();
            btnModuloInventario = new Guna2CircleButton();
            btnModuloTaller = new Guna2CircleButton();
            btnModuloVentas = new Guna2CircleButton();
            btnModuloSeguridad = new Guna2CircleButton();
            panelCentral = new Panel();
            layoutMensajeBienvenida = new TableLayoutPanel();
            panelMensajeBienvenida = new Panel();
            fieldTextoBienvenida = new Guna2HtmlLabel();
            layoutLogotipos = new TableLayoutPanel();
            fieldEmpresa1 = new PictureBox();
            fieldEmpresa2 = new PictureBox();
            fieldEmpresa3 = new PictureBox();
            layoutBase.SuspendLayout();
            layoutDistribucion.SuspendLayout();
            layoutMenuLateral.SuspendLayout();
            layoutModulos.SuspendLayout();
            panelCentral.SuspendLayout();
            layoutMensajeBienvenida.SuspendLayout();
            panelMensajeBienvenida.SuspendLayout();
            layoutLogotipos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) fieldEmpresa1).BeginInit();
            ((System.ComponentModel.ISupportInitialize) fieldEmpresa2).BeginInit();
            ((System.ComponentModel.ISupportInitialize) fieldEmpresa3).BeginInit();
            SuspendLayout();
            // 
            // formatoBase
            // 
            formatoBase.ContainerControl = this;
            formatoBase.DockIndicatorTransparencyValue = 0.6D;
            formatoBase.DragForm = false;
            formatoBase.HasFormShadow = false;
            formatoBase.TransparentWhileDrag = true;
            // 
            // layoutBase
            // 
            layoutBase.BackColor = Color.White;
            layoutBase.ColumnCount = 1;
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutBase.Controls.Add(layoutDistribucion, 0, 0);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 1;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutBase.Size = new Size(1356, 608);
            layoutBase.TabIndex = 1;
            // 
            // layoutDistribucion
            // 
            layoutDistribucion.BackColor = Color.White;
            layoutDistribucion.ColumnCount = 2;
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistribucion.Controls.Add(layoutMenuLateral, 0, 0);
            layoutDistribucion.Controls.Add(panelCentral, 1, 0);
            layoutDistribucion.Dock = DockStyle.Fill;
            layoutDistribucion.Location = new Point(0, 0);
            layoutDistribucion.Margin = new Padding(0);
            layoutDistribucion.Name = "layoutDistribucion";
            layoutDistribucion.RowCount = 1;
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistribucion.Size = new Size(1356, 608);
            layoutDistribucion.TabIndex = 0;
            // 
            // layoutMenuLateral
            // 
            layoutMenuLateral.BackColor = Color.White;
            layoutMenuLateral.ColumnCount = 1;
            layoutMenuLateral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutMenuLateral.Controls.Add(layoutModulos, 0, 0);
            layoutMenuLateral.Dock = DockStyle.Fill;
            layoutMenuLateral.Location = new Point(0, 10);
            layoutMenuLateral.Margin = new Padding(0, 10, 0, 10);
            layoutMenuLateral.Name = "layoutMenuLateral";
            layoutMenuLateral.RowCount = 1;
            layoutMenuLateral.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutMenuLateral.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutMenuLateral.Size = new Size(50, 588);
            layoutMenuLateral.TabIndex = 0;
            // 
            // layoutModulos
            // 
            layoutModulos.BackColor = Color.White;
            layoutModulos.Controls.Add(btnInicio);
            layoutModulos.Controls.Add(btnEstadisticas);
            layoutModulos.Controls.Add(btnModuloContactos);
            layoutModulos.Controls.Add(btnModuloFinanzas);
            layoutModulos.Controls.Add(btnModuloInventario);
            layoutModulos.Controls.Add(btnModuloTaller);
            layoutModulos.Controls.Add(btnModuloVentas);
            layoutModulos.Controls.Add(btnModuloSeguridad);
            layoutModulos.Dock = DockStyle.Fill;
            layoutModulos.Location = new Point(0, 0);
            layoutModulos.Margin = new Padding(0);
            layoutModulos.Name = "layoutModulos";
            layoutModulos.Size = new Size(50, 588);
            layoutModulos.TabIndex = 0;
            // 
            // btnInicio
            // 
            btnInicio.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnInicio.CheckedState.FillColor = Color.PeachPuff;
            btnInicio.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnInicio.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnInicio.CustomImages.ImageSize = new Size(24, 24);
            btnInicio.FillColor = Color.White;
            btnInicio.Font = new Font("Segoe UI", 9F);
            btnInicio.ForeColor = Color.White;
            btnInicio.ImageSize = new Size(24, 24);
            btnInicio.Location = new Point(3, 3);
            btnInicio.Name = "btnInicio";
            btnInicio.ShadowDecoration.CustomizableEdges = customizableEdges25;
            btnInicio.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnInicio.Size = new Size(44, 44);
            btnInicio.TabIndex = 0;
            // 
            // btnEstadisticas
            // 
            btnEstadisticas.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnEstadisticas.CheckedState.FillColor = Color.PeachPuff;
            btnEstadisticas.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnEstadisticas.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnEstadisticas.CustomImages.ImageSize = new Size(24, 24);
            btnEstadisticas.FillColor = Color.White;
            btnEstadisticas.Font = new Font("Segoe UI", 9F);
            btnEstadisticas.ForeColor = Color.White;
            btnEstadisticas.ImageSize = new Size(24, 24);
            btnEstadisticas.Location = new Point(3, 53);
            btnEstadisticas.Name = "btnEstadisticas";
            btnEstadisticas.ShadowDecoration.CustomizableEdges = customizableEdges26;
            btnEstadisticas.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnEstadisticas.Size = new Size(44, 44);
            btnEstadisticas.TabIndex = 1;
            // 
            // btnModuloContactos
            // 
            btnModuloContactos.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnModuloContactos.CheckedState.FillColor = Color.PeachPuff;
            btnModuloContactos.CustomImages.Image = (Image) resources.GetObject("resource.Image2");
            btnModuloContactos.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnModuloContactos.CustomImages.ImageSize = new Size(24, 24);
            btnModuloContactos.FillColor = Color.White;
            btnModuloContactos.Font = new Font("Segoe UI", 9F);
            btnModuloContactos.ForeColor = Color.White;
            btnModuloContactos.ImageSize = new Size(24, 24);
            btnModuloContactos.Location = new Point(3, 103);
            btnModuloContactos.Name = "btnModuloContactos";
            btnModuloContactos.ShadowDecoration.CustomizableEdges = customizableEdges27;
            btnModuloContactos.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnModuloContactos.Size = new Size(44, 44);
            btnModuloContactos.TabIndex = 3;
            // 
            // btnModuloFinanzas
            // 
            btnModuloFinanzas.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnModuloFinanzas.CheckedState.FillColor = Color.PeachPuff;
            btnModuloFinanzas.CustomImages.Image = (Image) resources.GetObject("resource.Image3");
            btnModuloFinanzas.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnModuloFinanzas.CustomImages.ImageSize = new Size(24, 24);
            btnModuloFinanzas.FillColor = Color.White;
            btnModuloFinanzas.Font = new Font("Segoe UI", 9F);
            btnModuloFinanzas.ForeColor = Color.White;
            btnModuloFinanzas.ImageSize = new Size(24, 24);
            btnModuloFinanzas.Location = new Point(3, 153);
            btnModuloFinanzas.Name = "btnModuloFinanzas";
            btnModuloFinanzas.ShadowDecoration.CustomizableEdges = customizableEdges28;
            btnModuloFinanzas.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnModuloFinanzas.Size = new Size(44, 44);
            btnModuloFinanzas.TabIndex = 5;
            // 
            // btnModuloInventario
            // 
            btnModuloInventario.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnModuloInventario.CheckedState.FillColor = Color.PeachPuff;
            btnModuloInventario.CustomImages.Image = (Image) resources.GetObject("resource.Image4");
            btnModuloInventario.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnModuloInventario.CustomImages.ImageSize = new Size(24, 24);
            btnModuloInventario.FillColor = Color.White;
            btnModuloInventario.Font = new Font("Segoe UI", 9F);
            btnModuloInventario.ForeColor = Color.White;
            btnModuloInventario.ImageSize = new Size(24, 24);
            btnModuloInventario.Location = new Point(3, 203);
            btnModuloInventario.Name = "btnModuloInventario";
            btnModuloInventario.ShadowDecoration.CustomizableEdges = customizableEdges29;
            btnModuloInventario.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnModuloInventario.Size = new Size(44, 44);
            btnModuloInventario.TabIndex = 2;
            // 
            // btnModuloTaller
            // 
            btnModuloTaller.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnModuloTaller.CheckedState.FillColor = Color.PeachPuff;
            btnModuloTaller.CustomImages.Image = (Image) resources.GetObject("resource.Image5");
            btnModuloTaller.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnModuloTaller.CustomImages.ImageSize = new Size(24, 24);
            btnModuloTaller.FillColor = Color.White;
            btnModuloTaller.Font = new Font("Segoe UI", 9F);
            btnModuloTaller.ForeColor = Color.White;
            btnModuloTaller.ImageSize = new Size(24, 24);
            btnModuloTaller.Location = new Point(3, 253);
            btnModuloTaller.Name = "btnModuloTaller";
            btnModuloTaller.ShadowDecoration.CustomizableEdges = customizableEdges30;
            btnModuloTaller.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnModuloTaller.Size = new Size(44, 44);
            btnModuloTaller.TabIndex = 7;
            // 
            // btnModuloVentas
            // 
            btnModuloVentas.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnModuloVentas.CheckedState.FillColor = Color.PeachPuff;
            btnModuloVentas.CustomImages.Image = (Image) resources.GetObject("resource.Image6");
            btnModuloVentas.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnModuloVentas.CustomImages.ImageSize = new Size(24, 24);
            btnModuloVentas.FillColor = Color.White;
            btnModuloVentas.Font = new Font("Segoe UI", 9F);
            btnModuloVentas.ForeColor = Color.White;
            btnModuloVentas.ImageSize = new Size(24, 24);
            btnModuloVentas.Location = new Point(3, 303);
            btnModuloVentas.Name = "btnModuloVentas";
            btnModuloVentas.ShadowDecoration.CustomizableEdges = customizableEdges31;
            btnModuloVentas.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnModuloVentas.Size = new Size(44, 44);
            btnModuloVentas.TabIndex = 4;
            // 
            // btnModuloSeguridad
            // 
            btnModuloSeguridad.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnModuloSeguridad.CheckedState.FillColor = Color.PeachPuff;
            btnModuloSeguridad.CustomImages.Image = (Image) resources.GetObject("resource.Image7");
            btnModuloSeguridad.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnModuloSeguridad.CustomImages.ImageSize = new Size(24, 24);
            btnModuloSeguridad.FillColor = Color.White;
            btnModuloSeguridad.Font = new Font("Segoe UI", 9F);
            btnModuloSeguridad.ForeColor = Color.White;
            btnModuloSeguridad.ImageSize = new Size(24, 24);
            btnModuloSeguridad.Location = new Point(3, 353);
            btnModuloSeguridad.Name = "btnModuloSeguridad";
            btnModuloSeguridad.ShadowDecoration.CustomizableEdges = customizableEdges32;
            btnModuloSeguridad.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnModuloSeguridad.Size = new Size(44, 44);
            btnModuloSeguridad.TabIndex = 6;
            // 
            // contenedorVistas
            // 
            panelCentral.Controls.Add(layoutMensajeBienvenida);
            panelCentral.Dock = DockStyle.Fill;
            panelCentral.Location = new Point(50, 10);
            panelCentral.Margin = new Padding(0, 10, 0, 0);
            panelCentral.Name = "contenedorVistas";
            panelCentral.Size = new Size(1306, 598);
            panelCentral.TabIndex = 1;
            // 
            // layoutMensajeBienvenida
            // 
            layoutMensajeBienvenida.ColumnCount = 3;
            layoutMensajeBienvenida.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutMensajeBienvenida.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 1000F));
            layoutMensajeBienvenida.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutMensajeBienvenida.Controls.Add(panelMensajeBienvenida, 1, 0);
            layoutMensajeBienvenida.Controls.Add(layoutLogotipos, 1, 1);
            layoutMensajeBienvenida.Dock = DockStyle.Fill;
            layoutMensajeBienvenida.Location = new Point(0, 0);
            layoutMensajeBienvenida.Name = "layoutMensajeBienvenida";
            layoutMensajeBienvenida.RowCount = 3;
            layoutMensajeBienvenida.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutMensajeBienvenida.RowStyles.Add(new RowStyle(SizeType.Absolute, 160F));
            layoutMensajeBienvenida.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            layoutMensajeBienvenida.Size = new Size(1306, 598);
            layoutMensajeBienvenida.TabIndex = 1;
            // 
            // panelMensajeBienvenida
            // 
            panelMensajeBienvenida.Controls.Add(fieldTextoBienvenida);
            panelMensajeBienvenida.Dock = DockStyle.Fill;
            panelMensajeBienvenida.Location = new Point(156, 3);
            panelMensajeBienvenida.Name = "panelMensajeBienvenida";
            panelMensajeBienvenida.Size = new Size(994, 382);
            panelMensajeBienvenida.TabIndex = 2;
            // 
            // fieldTextoBienvenida
            // 
            fieldTextoBienvenida.AutoSize = false;
            fieldTextoBienvenida.BackColor = Color.White;
            fieldTextoBienvenida.Dock = DockStyle.Fill;
            fieldTextoBienvenida.Location = new Point(0, 0);
            fieldTextoBienvenida.Name = "fieldTextoBienvenida";
            fieldTextoBienvenida.Size = new Size(994, 382);
            fieldTextoBienvenida.TabIndex = 1;
            fieldTextoBienvenida.Text = null;
            // 
            // layoutLogotipos
            // 
            layoutLogotipos.ColumnCount = 7;
            layoutLogotipos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutLogotipos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutLogotipos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutLogotipos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutLogotipos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutLogotipos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutLogotipos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutLogotipos.Controls.Add(fieldEmpresa1, 1, 0);
            layoutLogotipos.Controls.Add(fieldEmpresa2, 3, 0);
            layoutLogotipos.Controls.Add(fieldEmpresa3, 5, 0);
            layoutLogotipos.Dock = DockStyle.Fill;
            layoutLogotipos.Location = new Point(153, 388);
            layoutLogotipos.Margin = new Padding(0);
            layoutLogotipos.Name = "layoutLogotipos";
            layoutLogotipos.RowCount = 1;
            layoutLogotipos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutLogotipos.Size = new Size(1000, 160);
            layoutLogotipos.TabIndex = 1;
            // 
            // fieldEmpresa1
            // 
            fieldEmpresa1.BackgroundImage = Properties.Resources.empresa1;
            fieldEmpresa1.BackgroundImageLayout = ImageLayout.Center;
            fieldEmpresa1.Dock = DockStyle.Right;
            fieldEmpresa1.Location = new Point(240, 0);
            fieldEmpresa1.Margin = new Padding(0);
            fieldEmpresa1.Name = "fieldEmpresa1";
            fieldEmpresa1.Size = new Size(160, 160);
            fieldEmpresa1.TabIndex = 0;
            fieldEmpresa1.TabStop = false;
            // 
            // fieldEmpresa2
            // 
            fieldEmpresa2.BackgroundImage = Properties.Resources.empresa2;
            fieldEmpresa2.BackgroundImageLayout = ImageLayout.Center;
            fieldEmpresa2.Dock = DockStyle.Fill;
            fieldEmpresa2.Image = (Image) resources.GetObject("fieldEmpresa2.Image");
            fieldEmpresa2.Location = new Point(420, 0);
            fieldEmpresa2.Margin = new Padding(0);
            fieldEmpresa2.Name = "fieldEmpresa2";
            fieldEmpresa2.Size = new Size(160, 160);
            fieldEmpresa2.TabIndex = 1;
            fieldEmpresa2.TabStop = false;
            // 
            // fieldEmpresa3
            // 
            fieldEmpresa3.BackgroundImageLayout = ImageLayout.Center;
            fieldEmpresa3.Dock = DockStyle.Fill;
            fieldEmpresa3.Image = (Image) resources.GetObject("fieldEmpresa3.Image");
            fieldEmpresa3.Location = new Point(600, 0);
            fieldEmpresa3.Margin = new Padding(0);
            fieldEmpresa3.Name = "fieldEmpresa3";
            fieldEmpresa3.Size = new Size(160, 160);
            fieldEmpresa3.TabIndex = 2;
            fieldEmpresa3.TabStop = false;
            // 
            // VistaContenedorModulos
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1356, 608);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 11.25F);
            ForeColor = Color.Black;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaContenedorModulos";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "FormularioFormatoBase1";
            layoutBase.ResumeLayout(false);
            layoutDistribucion.ResumeLayout(false);
            layoutMenuLateral.ResumeLayout(false);
            layoutModulos.ResumeLayout(false);
            panelCentral.ResumeLayout(false);
            layoutMensajeBienvenida.ResumeLayout(false);
            panelMensajeBienvenida.ResumeLayout(false);
            layoutLogotipos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) fieldEmpresa1).EndInit();
            ((System.ComponentModel.ISupportInitialize) fieldEmpresa2).EndInit();
            ((System.ComponentModel.ISupportInitialize) fieldEmpresa3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutDistribucion;
        private TableLayoutPanel layoutMenuLateral;
        private FlowLayoutPanel layoutModulos;
        private Panel panelCentral;
        private Guna2CircleButton btnInicio;
        private Guna2CircleButton btnEstadisticas;
        private Guna2CircleButton btnModuloInventario;
        private Guna2CircleButton btnModuloContactos;
        private Guna2CircleButton btnModuloVentas;
        private Guna2CircleButton btnModuloFinanzas;
        private TableLayoutPanel layoutMensajeBienvenida;
        private Guna2CircleButton btnModuloSeguridad;
        private Guna2CircleButton btnModuloTaller;
        private TableLayoutPanel layoutLogotipos;
        private PictureBox fieldEmpresa1;
        private PictureBox fieldEmpresa2;
        private PictureBox fieldEmpresa3;
        private Panel panelMensajeBienvenida;
        private Guna2HtmlLabel fieldTextoBienvenida;
    }
}