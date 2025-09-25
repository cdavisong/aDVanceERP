namespace aDVanceINSTALL.Desktop.MVP.Vistas {
    partial class VistaPrincipal {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaPrincipal));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            layoutBotones = new TableLayoutPanel();
            btnSalir = new Guna.UI2.WinForms.Guna2Button();
            btnSiguiente = new Guna.UI2.WinForms.Guna2Button();
            layoutBarraTitulo = new TableLayoutPanel();
            fieldIcono = new PictureBox();
            contenedorMenus = new Panel();
            fieldTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnCerrar = new Guna.UI2.WinForms.Guna2ControlBox();
            contenedorVistas = new Panel();
            separador1 = new Guna.UI2.WinForms.Guna2Separator();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            layoutBotones.SuspendLayout();
            layoutBarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) fieldIcono).BeginInit();
            contenedorMenus.SuspendLayout();
            SuspendLayout();
            // 
            // formatoBase
            // 
            formatoBase.ContainerControl = this;
            formatoBase.DockIndicatorTransparencyValue = 0.6D;
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
            layoutBase.Size = new Size(798, 519);
            layoutBase.TabIndex = 1;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.Gainsboro;
            layoutVista.ColumnCount = 1;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.Controls.Add(layoutBotones, 0, 2);
            layoutVista.Controls.Add(layoutBarraTitulo, 0, 0);
            layoutVista.Controls.Add(contenedorVistas, 0, 1);
            layoutVista.Controls.Add(separador1, 0, 3);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(1, 1);
            layoutVista.Margin = new Padding(1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 5;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutVista.Size = new Size(796, 517);
            layoutVista.TabIndex = 0;
            // 
            // layoutBotones
            // 
            layoutBotones.BackColor = Color.White;
            layoutBotones.ColumnCount = 4;
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            layoutBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 22F));
            layoutBotones.Controls.Add(btnSalir, 2, 0);
            layoutBotones.Controls.Add(btnSiguiente, 1, 0);
            layoutBotones.Dock = DockStyle.Fill;
            layoutBotones.Location = new Point(0, 442);
            layoutBotones.Margin = new Padding(0);
            layoutBotones.Name = "layoutBotones";
            layoutBotones.RowCount = 1;
            layoutBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBotones.Size = new Size(796, 50);
            layoutBotones.TabIndex = 4;
            // 
            // btnSalir
            // 
            btnSalir.Animated = true;
            btnSalir.BorderColor = Color.Gainsboro;
            btnSalir.BorderRadius = 18;
            btnSalir.BorderThickness = 1;
            btnSalir.CustomizableEdges = customizableEdges1;
            btnSalir.Dock = DockStyle.Fill;
            btnSalir.FillColor = Color.White;
            btnSalir.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSalir.ForeColor = Color.Gainsboro;
            btnSalir.HoverState.BorderColor = Color.PeachPuff;
            btnSalir.HoverState.FillColor = Color.PeachPuff;
            btnSalir.HoverState.ForeColor = Color.Black;
            btnSalir.Location = new Point(567, 3);
            btnSalir.Name = "btnSalir";
            btnSalir.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnSalir.Size = new Size(203, 44);
            btnSalir.TabIndex = 14;
            btnSalir.Text = "Salir";
            // 
            // btnSiguiente
            // 
            btnSiguiente.Animated = true;
            btnSiguiente.BorderRadius = 18;
            btnSiguiente.CustomizableEdges = customizableEdges3;
            btnSiguiente.Dock = DockStyle.Fill;
            btnSiguiente.FillColor = Color.PeachPuff;
            btnSiguiente.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSiguiente.ForeColor = Color.Black;
            btnSiguiente.Location = new Point(253, 3);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnSiguiente.Size = new Size(308, 44);
            btnSiguiente.TabIndex = 15;
            btnSiguiente.Text = "Siguiente";
            // 
            // layoutBarraTitulo
            // 
            layoutBarraTitulo.BackColor = Color.WhiteSmoke;
            layoutBarraTitulo.ColumnCount = 3;
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutBarraTitulo.Controls.Add(fieldIcono, 0, 0);
            layoutBarraTitulo.Controls.Add(contenedorMenus, 1, 0);
            layoutBarraTitulo.Controls.Add(btnCerrar, 2, 0);
            layoutBarraTitulo.Dock = DockStyle.Fill;
            layoutBarraTitulo.Location = new Point(0, 0);
            layoutBarraTitulo.Margin = new Padding(0, 0, 0, 2);
            layoutBarraTitulo.Name = "layoutBarraTitulo";
            layoutBarraTitulo.RowCount = 1;
            layoutBarraTitulo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBarraTitulo.Size = new Size(796, 53);
            layoutBarraTitulo.TabIndex = 0;
            // 
            // fieldIcono
            // 
            fieldIcono.BackgroundImage = (Image) resources.GetObject("fieldIcono.BackgroundImage");
            fieldIcono.BackgroundImageLayout = ImageLayout.Center;
            fieldIcono.Dock = DockStyle.Fill;
            fieldIcono.Location = new Point(3, 3);
            fieldIcono.Name = "fieldIcono";
            fieldIcono.Size = new Size(44, 47);
            fieldIcono.TabIndex = 0;
            fieldIcono.TabStop = false;
            // 
            // contenedorMenus
            // 
            contenedorMenus.Controls.Add(fieldTitulo);
            contenedorMenus.Dock = DockStyle.Top;
            contenedorMenus.Location = new Point(50, 0);
            contenedorMenus.Margin = new Padding(0);
            contenedorMenus.Name = "contenedorMenus";
            contenedorMenus.Size = new Size(696, 49);
            contenedorMenus.TabIndex = 1;
            // 
            // fieldTitulo
            // 
            fieldTitulo.BackColor = Color.Transparent;
            fieldTitulo.Dock = DockStyle.Fill;
            fieldTitulo.Location = new Point(0, 0);
            fieldTitulo.Margin = new Padding(0);
            fieldTitulo.Name = "fieldTitulo";
            fieldTitulo.Size = new Size(183, 53);
            fieldTitulo.TabIndex = 0;
            fieldTitulo.Text = resources.GetString("fieldTitulo.Text");
            fieldTitulo.TextAlignment = ContentAlignment.MiddleLeft;
            fieldTitulo.UseGdiPlusTextRendering = true;
            // 
            // btnCerrar
            // 
            btnCerrar.BorderRadius = 5;
            btnCerrar.CustomizableEdges = customizableEdges5;
            btnCerrar.Dock = DockStyle.Fill;
            btnCerrar.FillColor = Color.WhiteSmoke;
            btnCerrar.HoverState.FillColor = Color.FromArgb(  192,   0,   0);
            btnCerrar.HoverState.IconColor = Color.White;
            btnCerrar.IconColor = Color.Black;
            btnCerrar.Location = new Point(747, 1);
            btnCerrar.Margin = new Padding(1);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnCerrar.Size = new Size(48, 51);
            btnCerrar.TabIndex = 2;
            // 
            // contenedorVistas
            // 
            contenedorVistas.BackColor = Color.White;
            contenedorVistas.Dock = DockStyle.Fill;
            contenedorVistas.Location = new Point(0, 55);
            contenedorVistas.Margin = new Padding(0);
            contenedorVistas.Name = "contenedorVistas";
            contenedorVistas.Size = new Size(796, 387);
            contenedorVistas.TabIndex = 2;
            // 
            // separador1
            // 
            separador1.BackColor = Color.White;
            separador1.Dock = DockStyle.Fill;
            separador1.FillColor = Color.Gainsboro;
            separador1.Location = new Point(0, 492);
            separador1.Margin = new Padding(0);
            separador1.Name = "separador1";
            separador1.Size = new Size(796, 20);
            separador1.TabIndex = 39;
            // 
            // VistaPrincipal
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(798, 519);
            Controls.Add(layoutBase);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon) resources.GetObject("$this.Icon");
            Name = "VistaPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "VistaPrincipal";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            layoutBotones.ResumeLayout(false);
            layoutBarraTitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) fieldIcono).EndInit();
            contenedorMenus.ResumeLayout(false);
            contenedorMenus.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private TableLayoutPanel layoutBarraTitulo;
        private PictureBox fieldIcono;
        private Panel contenedorMenus;
        private Guna.UI2.WinForms.Guna2HtmlLabel fieldTitulo;
        private Guna.UI2.WinForms.Guna2ControlBox btnCerrar;
        private Panel contenedorVistas;
        private TableLayoutPanel layoutBotones;
        private Guna.UI2.WinForms.Guna2Button btnSalir;
        private Guna.UI2.WinForms.Guna2Button btnSiguiente;
        private Guna.UI2.WinForms.Guna2Separator separador1;
    }
}