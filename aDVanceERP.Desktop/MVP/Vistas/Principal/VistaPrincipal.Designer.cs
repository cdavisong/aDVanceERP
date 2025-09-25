namespace aDVanceERP.Desktop.MVP.Vistas.Principal {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaPrincipal));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            notificacionMensajes = new Guna.UI2.WinForms.Guna2NotificationPaint(components);
            btnMensajes = new Guna.UI2.WinForms.Guna2Button();
            notificacionesModulos = new Guna.UI2.WinForms.Guna2NotificationPaint(components);
            btnNotificaciones = new Guna.UI2.WinForms.Guna2Button();
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            layoutBarraTitulo = new TableLayoutPanel();
            btnMinimizar = new Guna.UI2.WinForms.Guna2ControlBox();
            fieldIcono = new PictureBox();
            barraTitulo = new Panel();
            fieldTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnCerrar = new Guna.UI2.WinForms.Guna2ControlBox();
            btnMenuUsuario = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            btnMaximizarRestaurar = new Guna.UI2.WinForms.Guna2ControlBox();
            layoutBarraEstado = new TableLayoutPanel();
            barraEstado = new Panel();
            panelCentral = new Panel();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            layoutBarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) fieldIcono).BeginInit();
            barraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) btnMenuUsuario).BeginInit();
            layoutBarraEstado.SuspendLayout();
            SuspendLayout();
            // 
            // formatoBase
            // 
            formatoBase.ContainerControl = this;
            formatoBase.DockIndicatorTransparencyValue = 0.6D;
            formatoBase.HasFormShadow = false;
            formatoBase.TransparentWhileDrag = true;
            // 
            // notificacionMensajes
            // 
            notificacionMensajes.BorderColor = Color.FromArgb(  2,   52,   107);
            notificacionMensajes.BorderRadius = 12;
            notificacionMensajes.BorderThickness = 0;
            notificacionMensajes.FillColor = Color.FromArgb(  2,   52,   107);
            notificacionMensajes.Size = new Size(24, 24);
            notificacionMensajes.TargetControl = btnMensajes;
            notificacionMensajes.Visible = false;
            // 
            // btnMensajes
            // 
            btnMensajes.Animated = true;
            btnMensajes.BackgroundImageLayout = ImageLayout.Center;
            btnMensajes.Cursor = Cursors.Hand;
            btnMensajes.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnMensajes.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnMensajes.CustomizableEdges = customizableEdges3;
            btnMensajes.Dock = DockStyle.Fill;
            btnMensajes.FillColor = Color.WhiteSmoke;
            btnMensajes.Font = new Font("Segoe UI", 9F);
            btnMensajes.ForeColor = Color.White;
            btnMensajes.Location = new Point(1107, 1);
            btnMensajes.Margin = new Padding(1);
            btnMensajes.Name = "btnMensajes";
            btnMensajes.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnMensajes.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnMensajes.Size = new Size(48, 51);
            btnMensajes.TabIndex = 1;
            btnMensajes.TabStop = false;
            btnMensajes.Visible = false;
            // 
            // notificacionesModulos
            // 
            notificacionesModulos.BorderColor = Color.FromArgb(  2,   52,   107);
            notificacionesModulos.BorderRadius = 12;
            notificacionesModulos.BorderThickness = 0;
            notificacionesModulos.FillColor = Color.FromArgb(  2,   52,   107);
            notificacionesModulos.Size = new Size(24, 24);
            notificacionesModulos.TargetControl = btnNotificaciones;
            notificacionesModulos.Visible = false;
            // 
            // btnNotificaciones
            // 
            btnNotificaciones.Animated = true;
            btnNotificaciones.BackgroundImageLayout = ImageLayout.Center;
            btnNotificaciones.Cursor = Cursors.Hand;
            btnNotificaciones.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnNotificaciones.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnNotificaciones.CustomizableEdges = customizableEdges1;
            btnNotificaciones.Dock = DockStyle.Fill;
            btnNotificaciones.FillColor = Color.WhiteSmoke;
            btnNotificaciones.Font = new Font("Segoe UI", 9F);
            btnNotificaciones.ForeColor = Color.White;
            btnNotificaciones.Location = new Point(1057, 1);
            btnNotificaciones.Margin = new Padding(1);
            btnNotificaciones.Name = "btnNotificaciones";
            btnNotificaciones.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnNotificaciones.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnNotificaciones.Size = new Size(48, 51);
            btnNotificaciones.TabIndex = 1;
            btnNotificaciones.TabStop = false;
            btnNotificaciones.Visible = false;
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
            layoutBase.Size = new Size(1358, 685);
            layoutBase.TabIndex = 0;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.Gainsboro;
            layoutVista.ColumnCount = 1;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.Controls.Add(layoutBarraTitulo, 0, 0);
            layoutVista.Controls.Add(layoutBarraEstado, 0, 2);
            layoutVista.Controls.Add(panelCentral, 0, 1);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(1, 1);
            layoutVista.Margin = new Padding(1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 3;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            layoutVista.Size = new Size(1356, 683);
            layoutVista.TabIndex = 0;
            // 
            // layoutBarraTitulo
            // 
            layoutBarraTitulo.BackColor = Color.WhiteSmoke;
            layoutBarraTitulo.ColumnCount = 8;
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutBarraTitulo.Controls.Add(btnNotificaciones, 2, 0);
            layoutBarraTitulo.Controls.Add(btnMensajes, 3, 0);
            layoutBarraTitulo.Controls.Add(btnMinimizar, 5, 0);
            layoutBarraTitulo.Controls.Add(fieldIcono, 0, 0);
            layoutBarraTitulo.Controls.Add(barraTitulo, 1, 0);
            layoutBarraTitulo.Controls.Add(btnCerrar, 7, 0);
            layoutBarraTitulo.Controls.Add(btnMenuUsuario, 4, 0);
            layoutBarraTitulo.Controls.Add(btnMaximizarRestaurar, 6, 0);
            layoutBarraTitulo.Dock = DockStyle.Fill;
            layoutBarraTitulo.Location = new Point(0, 0);
            layoutBarraTitulo.Margin = new Padding(0, 0, 0, 2);
            layoutBarraTitulo.Name = "layoutBarraTitulo";
            layoutBarraTitulo.RowCount = 1;
            layoutBarraTitulo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBarraTitulo.Size = new Size(1356, 53);
            layoutBarraTitulo.TabIndex = 0;
            // 
            // btnMinimizar
            // 
            btnMinimizar.BorderRadius = 5;
            btnMinimizar.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            btnMinimizar.CustomizableEdges = customizableEdges5;
            btnMinimizar.Dock = DockStyle.Fill;
            btnMinimizar.FillColor = Color.WhiteSmoke;
            btnMinimizar.IconColor = Color.Black;
            btnMinimizar.Location = new Point(1207, 1);
            btnMinimizar.Margin = new Padding(1);
            btnMinimizar.Name = "btnMinimizar";
            btnMinimizar.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnMinimizar.Size = new Size(48, 51);
            btnMinimizar.TabIndex = 3;
            // 
            // fieldIcono
            // 
            fieldIcono.BackgroundImage = (Image) resources.GetObject("fieldIcono.BackgroundImage");
            fieldIcono.BackgroundImageLayout = ImageLayout.Center;
            fieldIcono.Dock = DockStyle.Fill;
            fieldIcono.Location = new Point(3, 3);
            fieldIcono.Margin = new Padding(3, 3, 0, 3);
            fieldIcono.Name = "fieldIcono";
            fieldIcono.Size = new Size(47, 47);
            fieldIcono.TabIndex = 0;
            fieldIcono.TabStop = false;
            // 
            // barraTitulo
            // 
            barraTitulo.Controls.Add(fieldTitulo);
            barraTitulo.Dock = DockStyle.Top;
            barraTitulo.Location = new Point(50, 0);
            barraTitulo.Margin = new Padding(0);
            barraTitulo.Name = "barraTitulo";
            barraTitulo.Size = new Size(1006, 49);
            barraTitulo.TabIndex = 1;
            // 
            // fieldTitulo
            // 
            fieldTitulo.BackColor = Color.Transparent;
            fieldTitulo.Dock = DockStyle.Fill;
            fieldTitulo.Location = new Point(0, 0);
            fieldTitulo.Margin = new Padding(0);
            fieldTitulo.Name = "fieldTitulo";
            fieldTitulo.Size = new Size(1006, 49);
            fieldTitulo.TabIndex = 0;
            fieldTitulo.Text = null;
            fieldTitulo.TextAlignment = ContentAlignment.MiddleLeft;
            fieldTitulo.UseGdiPlusTextRendering = true;
            // 
            // btnCerrar
            // 
            btnCerrar.BorderRadius = 5;
            btnCerrar.CustomizableEdges = customizableEdges7;
            btnCerrar.Dock = DockStyle.Fill;
            btnCerrar.FillColor = Color.WhiteSmoke;
            btnCerrar.HoverState.FillColor = Color.FromArgb(  192,   0,   0);
            btnCerrar.HoverState.IconColor = Color.White;
            btnCerrar.IconColor = Color.Black;
            btnCerrar.Location = new Point(1307, 1);
            btnCerrar.Margin = new Padding(1);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnCerrar.Size = new Size(48, 51);
            btnCerrar.TabIndex = 2;
            // 
            // btnMenuUsuario
            // 
            btnMenuUsuario.BackgroundImageLayout = ImageLayout.Center;
            btnMenuUsuario.Cursor = Cursors.Hand;
            btnMenuUsuario.Dock = DockStyle.Fill;
            btnMenuUsuario.Image = (Image) resources.GetObject("btnMenuUsuario.Image");
            btnMenuUsuario.ImageRotate = 0F;
            btnMenuUsuario.Location = new Point(1157, 1);
            btnMenuUsuario.Margin = new Padding(1);
            btnMenuUsuario.Name = "btnMenuUsuario";
            btnMenuUsuario.ShadowDecoration.CustomizableEdges = customizableEdges9;
            btnMenuUsuario.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnMenuUsuario.Size = new Size(48, 51);
            btnMenuUsuario.SizeMode = PictureBoxSizeMode.CenterImage;
            btnMenuUsuario.TabIndex = 0;
            btnMenuUsuario.TabStop = false;
            btnMenuUsuario.Visible = false;
            // 
            // btnMaximizarRestaurar
            // 
            btnMaximizarRestaurar.BorderRadius = 5;
            btnMaximizarRestaurar.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
            btnMaximizarRestaurar.CustomizableEdges = customizableEdges10;
            btnMaximizarRestaurar.Dock = DockStyle.Fill;
            btnMaximizarRestaurar.FillColor = Color.WhiteSmoke;
            btnMaximizarRestaurar.IconColor = Color.Black;
            btnMaximizarRestaurar.Location = new Point(1257, 1);
            btnMaximizarRestaurar.Margin = new Padding(1);
            btnMaximizarRestaurar.Name = "btnMaximizarRestaurar";
            btnMaximizarRestaurar.ShadowDecoration.CustomizableEdges = customizableEdges11;
            btnMaximizarRestaurar.Size = new Size(48, 51);
            btnMaximizarRestaurar.TabIndex = 4;
            // 
            // layoutBarraEstado
            // 
            layoutBarraEstado.BackColor = Color.White;
            layoutBarraEstado.ColumnCount = 1;
            layoutBarraEstado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutBarraEstado.Controls.Add(barraEstado, 0, 0);
            layoutBarraEstado.Dock = DockStyle.Fill;
            layoutBarraEstado.Font = new Font("Segoe UI", 9.75F);
            layoutBarraEstado.ForeColor = Color.White;
            layoutBarraEstado.Location = new Point(0, 659);
            layoutBarraEstado.Margin = new Padding(0, 1, 0, 0);
            layoutBarraEstado.Name = "layoutBarraEstado";
            layoutBarraEstado.RowCount = 1;
            layoutBarraEstado.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutBarraEstado.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutBarraEstado.Size = new Size(1356, 24);
            layoutBarraEstado.TabIndex = 1;
            // 
            // barraEstado
            // 
            barraEstado.Dock = DockStyle.Fill;
            barraEstado.Location = new Point(10, 0);
            barraEstado.Margin = new Padding(10, 0, 0, 0);
            barraEstado.Name = "barraEstado";
            barraEstado.Size = new Size(1346, 24);
            barraEstado.TabIndex = 0;
            // 
            // panelCentral
            // 
            panelCentral.BackColor = Color.White;
            panelCentral.Dock = DockStyle.Fill;
            panelCentral.Location = new Point(0, 55);
            panelCentral.Margin = new Padding(0);
            panelCentral.Name = "panelCentral";
            panelCentral.Size = new Size(1356, 603);
            panelCentral.TabIndex = 2;
            // 
            // VistaPrincipal
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1358, 685);
            Controls.Add(layoutBase);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 11.25F);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon) resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "VistaPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "VistaPrincipal";
            WindowState = FormWindowState.Maximized;
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            layoutBarraTitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) fieldIcono).EndInit();
            barraTitulo.ResumeLayout(false);
            barraTitulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) btnMenuUsuario).EndInit();
            layoutBarraEstado.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm formatoBase;
        private Guna.UI2.WinForms.Guna2NotificationPaint notificacionMensajes;
        private Guna.UI2.WinForms.Guna2NotificationPaint notificacionesModulos;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private TableLayoutPanel layoutBarraTitulo;
        private TableLayoutPanel layoutBarraEstado;
        private PictureBox fieldIcono;
        private Panel barraTitulo;
        private Guna.UI2.WinForms.Guna2ControlBox btnCerrar;
        private Panel panelCentral;
        private Guna.UI2.WinForms.Guna2ControlBox btnMinimizar;
        private Guna.UI2.WinForms.Guna2ControlBox btnMaximizarRestaurar;
        private Guna.UI2.WinForms.Guna2Button btnNotificaciones;
        private Guna.UI2.WinForms.Guna2Button btnMensajes;
        private Guna.UI2.WinForms.Guna2CirclePictureBox btnMenuUsuario;
        private Guna.UI2.WinForms.Guna2HtmlLabel fieldTitulo;
        private Panel barraEstado;
    }
}