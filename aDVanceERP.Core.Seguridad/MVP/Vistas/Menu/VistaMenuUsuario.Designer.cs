using Guna.UI2.WinForms;

namespace aDVanceERP.Core.Seguridad.MVP.Vistas.Menu {
    partial class VistaMenuUsuario {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaMenuUsuario));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutDistribucion = new TableLayoutPanel();
            layoutBarraTitulo = new TableLayoutPanel();
            btnCerrar = new Guna2Button();
            fieldNomreUsuario = new Label();
            layoutDistribucionDatos = new TableLayoutPanel();
            fieldFotoPerfil = new PictureBox();
            layoutDatosCuenta = new FlowLayoutPanel();
            fieldNombreEmpresa = new Label();
            fieldCorreoElectronico = new Label();
            fieldIdEmpresa = new Label();
            layoutBotones = new FlowLayoutPanel();
            btnConfigurarEmpresa = new Label();
            btnCerrarSesion = new Label();
            layoutBase.SuspendLayout();
            layoutDistribucion.SuspendLayout();
            layoutBarraTitulo.SuspendLayout();
            layoutDistribucionDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) fieldFotoPerfil).BeginInit();
            layoutDatosCuenta.SuspendLayout();
            layoutBotones.SuspendLayout();
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
            layoutBase.BackColor = Color.Gainsboro;
            layoutBase.ColumnCount = 1;
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutBase.Controls.Add(layoutDistribucion, 0, 0);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 1;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutBase.Size = new Size(357, 232);
            layoutBase.TabIndex = 0;
            // 
            // layoutDistribucion
            // 
            layoutDistribucion.BackColor = Color.White;
            layoutDistribucion.ColumnCount = 3;
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            layoutDistribucion.Controls.Add(layoutBarraTitulo, 1, 1);
            layoutDistribucion.Controls.Add(layoutDistribucionDatos, 1, 3);
            layoutDistribucion.Controls.Add(layoutBotones, 1, 4);
            layoutDistribucion.Dock = DockStyle.Fill;
            layoutDistribucion.Location = new Point(1, 0);
            layoutDistribucion.Margin = new Padding(1, 0, 1, 3);
            layoutDistribucion.Name = "layoutDistribucion";
            layoutDistribucion.RowCount = 5;
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucion.Size = new Size(355, 229);
            layoutDistribucion.TabIndex = 1;
            // 
            // layoutBarraTitulo
            // 
            layoutBarraTitulo.ColumnCount = 2;
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBarraTitulo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            layoutBarraTitulo.Controls.Add(btnCerrar, 0, 0);
            layoutBarraTitulo.Controls.Add(fieldNomreUsuario, 0, 0);
            layoutBarraTitulo.Dock = DockStyle.Fill;
            layoutBarraTitulo.Location = new Point(10, 10);
            layoutBarraTitulo.Margin = new Padding(0);
            layoutBarraTitulo.Name = "layoutBarraTitulo";
            layoutBarraTitulo.RowCount = 1;
            layoutBarraTitulo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBarraTitulo.Size = new Size(335, 30);
            layoutBarraTitulo.TabIndex = 0;
            // 
            // btnCerrar
            // 
            btnCerrar.Animated = true;
            btnCerrar.AutoRoundedCorners = true;
            btnCerrar.BorderColor = Color.Gray;
            btnCerrar.BorderRadius = 11;
            btnCerrar.CustomizableEdges = customizableEdges3;
            btnCerrar.Dock = DockStyle.Fill;
            btnCerrar.FillColor = Color.FromArgb(  250,   250,   250);
            btnCerrar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.Black;
            btnCerrar.HoverState.FillColor = Color.FromArgb(  250,   250,   250);
            btnCerrar.Image = (Image) resources.GetObject("btnCerrar.Image");
            btnCerrar.Location = new Point(308, 3);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnCerrar.Size = new Size(24, 24);
            btnCerrar.TabIndex = 9;
            // 
            // fieldNomreUsuario
            // 
            fieldNomreUsuario.BackColor = Color.White;
            fieldNomreUsuario.Dock = DockStyle.Fill;
            fieldNomreUsuario.Font = new Font("Segoe UI", 9.75F);
            fieldNomreUsuario.ForeColor = Color.Black;
            fieldNomreUsuario.Image = (Image) resources.GetObject("fieldNomreUsuario.Image");
            fieldNomreUsuario.ImageAlign = ContentAlignment.MiddleLeft;
            fieldNomreUsuario.ImeMode = ImeMode.NoControl;
            fieldNomreUsuario.Location = new Point(1, 1);
            fieldNomreUsuario.Margin = new Padding(1);
            fieldNomreUsuario.Name = "fieldNomreUsuario";
            fieldNomreUsuario.Size = new Size(303, 28);
            fieldNomreUsuario.TabIndex = 6;
            fieldNomreUsuario.Text = "     Bienvenido!";
            fieldNomreUsuario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutDistribucionDatos
            // 
            layoutDistribucionDatos.ColumnCount = 2;
            layoutDistribucionDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            layoutDistribucionDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistribucionDatos.Controls.Add(fieldFotoPerfil, 0, 0);
            layoutDistribucionDatos.Controls.Add(layoutDatosCuenta, 1, 0);
            layoutDistribucionDatos.Dock = DockStyle.Fill;
            layoutDistribucionDatos.Location = new Point(10, 50);
            layoutDistribucionDatos.Margin = new Padding(0);
            layoutDistribucionDatos.Name = "layoutDistribucionDatos";
            layoutDistribucionDatos.RowCount = 1;
            layoutDistribucionDatos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucionDatos.Size = new Size(335, 100);
            layoutDistribucionDatos.TabIndex = 1;
            // 
            // fieldFotoPerfil
            // 
            fieldFotoPerfil.BackgroundImage = (Image) resources.GetObject("fieldFotoPerfil.BackgroundImage");
            fieldFotoPerfil.BackgroundImageLayout = ImageLayout.Center;
            fieldFotoPerfil.Dock = DockStyle.Fill;
            fieldFotoPerfil.Image = (Image) resources.GetObject("fieldFotoPerfil.Image");
            fieldFotoPerfil.Location = new Point(0, 0);
            fieldFotoPerfil.Margin = new Padding(0);
            fieldFotoPerfil.Name = "fieldFotoPerfil";
            fieldFotoPerfil.Size = new Size(100, 100);
            fieldFotoPerfil.TabIndex = 0;
            fieldFotoPerfil.TabStop = false;
            // 
            // layoutDatosCuenta
            // 
            layoutDatosCuenta.Controls.Add(fieldNombreEmpresa);
            layoutDatosCuenta.Controls.Add(fieldCorreoElectronico);
            layoutDatosCuenta.Controls.Add(fieldIdEmpresa);
            layoutDatosCuenta.Dock = DockStyle.Fill;
            layoutDatosCuenta.Location = new Point(105, 6);
            layoutDatosCuenta.Margin = new Padding(5, 6, 0, 6);
            layoutDatosCuenta.Name = "layoutDatosCuenta";
            layoutDatosCuenta.Size = new Size(230, 88);
            layoutDatosCuenta.TabIndex = 1;
            // 
            // fieldNombreEmpresa
            // 
            fieldNombreEmpresa.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldNombreEmpresa.ForeColor = Color.Black;
            fieldNombreEmpresa.ImeMode = ImeMode.NoControl;
            fieldNombreEmpresa.Location = new Point(1, 1);
            fieldNombreEmpresa.Margin = new Padding(1);
            fieldNombreEmpresa.Name = "fieldNombreEmpresa";
            fieldNombreEmpresa.Size = new Size(233, 28);
            fieldNombreEmpresa.TabIndex = 7;
            fieldNombreEmpresa.Text = "...";
            fieldNombreEmpresa.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldCorreoElectronico
            // 
            fieldCorreoElectronico.Font = new Font("Segoe UI", 9.75F);
            fieldCorreoElectronico.ForeColor = Color.Black;
            fieldCorreoElectronico.ImeMode = ImeMode.NoControl;
            fieldCorreoElectronico.Location = new Point(1, 31);
            fieldCorreoElectronico.Margin = new Padding(1);
            fieldCorreoElectronico.Name = "fieldCorreoElectronico";
            fieldCorreoElectronico.Size = new Size(233, 28);
            fieldCorreoElectronico.TabIndex = 8;
            fieldCorreoElectronico.Text = "...";
            // 
            // fieldIdEmpresa
            // 
            fieldIdEmpresa.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point,  0);
            fieldIdEmpresa.ForeColor = Color.Black;
            fieldIdEmpresa.ImeMode = ImeMode.NoControl;
            fieldIdEmpresa.Location = new Point(1, 61);
            fieldIdEmpresa.Margin = new Padding(1);
            fieldIdEmpresa.Name = "fieldIdEmpresa";
            fieldIdEmpresa.Size = new Size(233, 28);
            fieldIdEmpresa.TabIndex = 9;
            fieldIdEmpresa.Text = "...";
            // 
            // layoutBotones
            // 
            layoutBotones.Controls.Add(btnConfigurarEmpresa);
            layoutBotones.Controls.Add(btnCerrarSesion);
            layoutBotones.Dock = DockStyle.Fill;
            layoutBotones.Location = new Point(13, 153);
            layoutBotones.Name = "layoutBotones";
            layoutBotones.Size = new Size(329, 73);
            layoutBotones.TabIndex = 2;
            // 
            // btnConfigurarEmpresa
            // 
            btnConfigurarEmpresa.Cursor = Cursors.Hand;
            btnConfigurarEmpresa.Font = new Font("Segoe UI", 9.75F);
            btnConfigurarEmpresa.ForeColor = Color.Firebrick;
            btnConfigurarEmpresa.ImeMode = ImeMode.NoControl;
            btnConfigurarEmpresa.Location = new Point(1, 1);
            btnConfigurarEmpresa.Margin = new Padding(1);
            btnConfigurarEmpresa.Name = "btnConfigurarEmpresa";
            btnConfigurarEmpresa.Size = new Size(328, 28);
            btnConfigurarEmpresa.TabIndex = 6;
            btnConfigurarEmpresa.Text = "Configuración de la empresa";
            btnConfigurarEmpresa.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.Cursor = Cursors.Hand;
            btnCerrarSesion.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnCerrarSesion.ForeColor = Color.Firebrick;
            btnCerrarSesion.ImeMode = ImeMode.NoControl;
            btnCerrarSesion.Location = new Point(1, 33);
            btnCerrarSesion.Margin = new Padding(1, 3, 1, 1);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(327, 28);
            btnCerrarSesion.TabIndex = 5;
            btnCerrarSesion.Text = "Cerrar sesión";
            btnCerrarSesion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VistaMenuUsuario
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(  245,   245,   245);
            ClientSize = new Size(357, 232);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 11.25F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaMenuUsuario";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaMenu";
            layoutBase.ResumeLayout(false);
            layoutDistribucion.ResumeLayout(false);
            layoutBarraTitulo.ResumeLayout(false);
            layoutDistribucionDatos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) fieldFotoPerfil).EndInit();
            layoutDatosCuenta.ResumeLayout(false);
            layoutBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutDistribucion;
        private TableLayoutPanel layoutBarraTitulo;
        private Guna2Button btnCerrar;
        private Label fieldNomreUsuario;
        private TableLayoutPanel layoutDistribucionDatos;
        private PictureBox fieldFotoPerfil;
        private FlowLayoutPanel layoutDatosCuenta;
        private Label fieldNombreEmpresa;
        private Label fieldCorreoElectronico;
        private FlowLayoutPanel layoutBotones;
        private Label btnConfigurarEmpresa;
        private Label btnCerrarSesion;
        private Label fieldIdEmpresa;
    }
}