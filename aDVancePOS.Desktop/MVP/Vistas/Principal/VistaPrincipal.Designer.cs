using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVancePOS.Desktop.MVP.Vistas.Principal {
    partial class VistaPrincipal {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaPrincipal));
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
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            layoutBarraTitulo = new TableLayoutPanel();
            btnNotificaciones = new Guna2Button();
            btnMensajes = new Guna2Button();
            btnMinimizar = new Guna2ControlBox();
            fieldIcono = new PictureBox();
            contenedorMenus = new Panel();
            fieldTitulo = new Guna2HtmlLabel();
            btnCerrar = new Guna2ControlBox();
            btnSubMenuUsuario = new Guna2CirclePictureBox();
            btnMaximizarRestaurar = new Guna2ControlBox();
            layoutBarraEstado = new TableLayoutPanel();
            contenedorVistas = new Panel();
            formatoBase = new Guna2BorderlessForm(components);
            notificacionMensajes = new Guna2NotificationPaint(components);
            notificacionesModulos = new Guna2NotificationPaint(components);
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            layoutBarraTitulo.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
            contenedorMenus.SuspendLayout();
            ((ISupportInitialize) btnSubMenuUsuario).BeginInit();
            SuspendLayout();
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
            layoutBase.TabIndex = 1;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.Gainsboro;
            layoutVista.ColumnCount = 1;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.Controls.Add(layoutBarraTitulo, 0, 0);
            layoutVista.Controls.Add(layoutBarraEstado, 0, 2);
            layoutVista.Controls.Add(contenedorVistas, 0, 1);
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
            layoutBarraTitulo.Controls.Add(contenedorMenus, 1, 0);
            layoutBarraTitulo.Controls.Add(btnCerrar, 7, 0);
            layoutBarraTitulo.Controls.Add(btnSubMenuUsuario, 4, 0);
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
            btnNotificaciones.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            btnMensajes.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            contenedorMenus.Size = new Size(1006, 49);
            contenedorMenus.TabIndex = 1;
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
            fieldTitulo.Text = resources.GetString("fieldTitulo.Text");
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
            // btnSubMenuUsuario
            // 
            btnSubMenuUsuario.BackgroundImageLayout = ImageLayout.Center;
            btnSubMenuUsuario.Cursor = Cursors.Hand;
            btnSubMenuUsuario.Dock = DockStyle.Fill;
            btnSubMenuUsuario.Image = (Image) resources.GetObject("btnSubMenuUsuario.Image");
            btnSubMenuUsuario.ImageRotate = 0F;
            btnSubMenuUsuario.Location = new Point(1157, 1);
            btnSubMenuUsuario.Margin = new Padding(1);
            btnSubMenuUsuario.Name = "btnSubMenuUsuario";
            btnSubMenuUsuario.ShadowDecoration.CustomizableEdges = customizableEdges9;
            btnSubMenuUsuario.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnSubMenuUsuario.Size = new Size(48, 51);
            btnSubMenuUsuario.SizeMode = PictureBoxSizeMode.CenterImage;
            btnSubMenuUsuario.TabIndex = 0;
            btnSubMenuUsuario.TabStop = false;
            btnSubMenuUsuario.Visible = false;
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
            layoutBarraEstado.Dock = DockStyle.Fill;
            layoutBarraEstado.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            layoutBarraEstado.ForeColor = Color.White;
            layoutBarraEstado.Location = new Point(0, 659);
            layoutBarraEstado.Margin = new Padding(0, 1, 0, 0);
            layoutBarraEstado.Name = "layoutBarraEstado";
            layoutBarraEstado.RowCount = 1;
            layoutBarraEstado.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutBarraEstado.Size = new Size(1356, 24);
            layoutBarraEstado.TabIndex = 1;
            // 
            // contenedorVistas
            // 
            contenedorVistas.BackColor = Color.White;
            contenedorVistas.Dock = DockStyle.Fill;
            contenedorVistas.Location = new Point(0, 55);
            contenedorVistas.Margin = new Padding(0);
            contenedorVistas.Name = "contenedorVistas";
            contenedorVistas.Size = new Size(1356, 603);
            contenedorVistas.TabIndex = 2;
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
            // VistaPrincipal
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1358, 685);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon) resources.GetObject("$this.Icon");
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaPrincipal";
            StartPosition = FormStartPosition.Manual;
            Text = "VistaPrincipal";
            WindowState = FormWindowState.Maximized;
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            layoutBarraTitulo.ResumeLayout(false);
            ((ISupportInitialize) fieldIcono).EndInit();
            contenedorMenus.ResumeLayout(false);
            contenedorMenus.PerformLayout();
            ((ISupportInitialize) btnSubMenuUsuario).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private TableLayoutPanel layoutBarraTitulo;
        private Guna2Button btnNotificaciones;
        private Guna2Button btnMensajes;
        private Guna2ControlBox btnMinimizar;
        private PictureBox fieldIcono;
        private Panel contenedorMenus;
        private Guna2HtmlLabel fieldTitulo;
        private Guna2ControlBox btnCerrar;
        private Guna2CirclePictureBox btnSubMenuUsuario;
        private Guna2ControlBox btnMaximizarRestaurar;
        private TableLayoutPanel layoutBarraEstado;
        private Panel contenedorVistas;
        private Guna2BorderlessForm formatoBase;
        private Guna2NotificationPaint notificacionMensajes;
        private Guna2NotificationPaint notificacionesModulos;
    }
}