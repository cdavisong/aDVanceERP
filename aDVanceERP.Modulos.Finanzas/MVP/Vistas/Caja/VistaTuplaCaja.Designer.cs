using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja {
    partial class VistaTuplaCaja {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaTuplaCaja));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldNombreUsuario = new Label();
            fieldFechaCierre = new Label();
            simboloPeso2 = new Label();
            fieldSaldoActual = new Label();
            simboloPeso1 = new Label();
            fieldSaldoInicial = new Label();
            btnEliminar = new Guna2Button();
            fieldId = new Label();
            btnEditar = new Guna2Button();
            fieldFechaApertura = new Label();
            fieldEstado = new Label();
            btnDescargarInforme = new Guna2Button();
            fieldCantidadMovimientos = new Label();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
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
            layoutBase.Controls.Add(layoutVista, 0, 0);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 1;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutBase.Size = new Size(1241, 42);
            layoutBase.TabIndex = 1;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.White;
            layoutVista.ColumnCount = 14;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.Controls.Add(fieldCantidadMovimientos, 4, 0);
            layoutVista.Controls.Add(fieldNombreUsuario, 9, 0);
            layoutVista.Controls.Add(fieldFechaCierre, 7, 0);
            layoutVista.Controls.Add(simboloPeso2, 6, 0);
            layoutVista.Controls.Add(fieldSaldoActual, 5, 0);
            layoutVista.Controls.Add(simboloPeso1, 3, 0);
            layoutVista.Controls.Add(fieldSaldoInicial, 2, 0);
            layoutVista.Controls.Add(btnEliminar, 13, 0);
            layoutVista.Controls.Add(fieldId, 0, 0);
            layoutVista.Controls.Add(btnEditar, 12, 0);
            layoutVista.Controls.Add(fieldFechaApertura, 1, 0);
            layoutVista.Controls.Add(fieldEstado, 8, 0);
            layoutVista.Controls.Add(btnDescargarInforme, 11, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0, 0, 0, 1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(1241, 41);
            layoutVista.TabIndex = 18;
            // 
            // fieldNombreUsuario
            // 
            fieldNombreUsuario.Dock = DockStyle.Fill;
            fieldNombreUsuario.Font = new Font("Segoe UI", 11.25F);
            fieldNombreUsuario.ForeColor = Color.DimGray;
            fieldNombreUsuario.ImeMode = ImeMode.NoControl;
            fieldNombreUsuario.Location = new Point(851, 1);
            fieldNombreUsuario.Margin = new Padding(1);
            fieldNombreUsuario.Name = "fieldNombreUsuario";
            fieldNombreUsuario.Size = new Size(148, 39);
            fieldNombreUsuario.TabIndex = 37;
            fieldNombreUsuario.Text = "nombreUsuario";
            fieldNombreUsuario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldFechaCierre
            // 
            fieldFechaCierre.Dock = DockStyle.Fill;
            fieldFechaCierre.Font = new Font("Segoe UI", 11.25F);
            fieldFechaCierre.ForeColor = Color.DimGray;
            fieldFechaCierre.ImeMode = ImeMode.NoControl;
            fieldFechaCierre.Location = new Point(591, 1);
            fieldFechaCierre.Margin = new Padding(1);
            fieldFechaCierre.Name = "fieldFechaCierre";
            fieldFechaCierre.Size = new Size(148, 39);
            fieldFechaCierre.TabIndex = 35;
            fieldFechaCierre.Text = "fechaCierre";
            fieldFechaCierre.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // simboloPeso2
            // 
            simboloPeso2.Dock = DockStyle.Fill;
            simboloPeso2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            simboloPeso2.ForeColor = Color.Black;
            simboloPeso2.ImageAlign = ContentAlignment.MiddleLeft;
            simboloPeso2.ImeMode = ImeMode.NoControl;
            simboloPeso2.Location = new Point(573, 5);
            simboloPeso2.Margin = new Padding(3, 5, 3, 3);
            simboloPeso2.Name = "simboloPeso2";
            simboloPeso2.Size = new Size(14, 33);
            simboloPeso2.TabIndex = 33;
            simboloPeso2.Text = "$";
            simboloPeso2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldSaldoActual
            // 
            fieldSaldoActual.Dock = DockStyle.Fill;
            fieldSaldoActual.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldSaldoActual.ForeColor = Color.Black;
            fieldSaldoActual.ImeMode = ImeMode.NoControl;
            fieldSaldoActual.Location = new Point(461, 1);
            fieldSaldoActual.Margin = new Padding(1);
            fieldSaldoActual.Name = "fieldSaldoActual";
            fieldSaldoActual.Size = new Size(108, 39);
            fieldSaldoActual.TabIndex = 32;
            fieldSaldoActual.Text = "saldoActual";
            fieldSaldoActual.TextAlign = ContentAlignment.MiddleRight;
            // 
            // simboloPeso1
            // 
            simboloPeso1.Dock = DockStyle.Fill;
            simboloPeso1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            simboloPeso1.ForeColor = Color.Black;
            simboloPeso1.ImageAlign = ContentAlignment.MiddleLeft;
            simboloPeso1.ImeMode = ImeMode.NoControl;
            simboloPeso1.Location = new Point(323, 5);
            simboloPeso1.Margin = new Padding(3, 5, 3, 3);
            simboloPeso1.Name = "simboloPeso1";
            simboloPeso1.Size = new Size(14, 33);
            simboloPeso1.TabIndex = 31;
            simboloPeso1.Text = "$";
            simboloPeso1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldSaldoInicial
            // 
            fieldSaldoInicial.Dock = DockStyle.Fill;
            fieldSaldoInicial.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldSaldoInicial.ForeColor = Color.Black;
            fieldSaldoInicial.ImeMode = ImeMode.NoControl;
            fieldSaldoInicial.Location = new Point(211, 1);
            fieldSaldoInicial.Margin = new Padding(1);
            fieldSaldoInicial.Name = "fieldSaldoInicial";
            fieldSaldoInicial.Size = new Size(108, 39);
            fieldSaldoInicial.TabIndex = 21;
            fieldSaldoInicial.Text = "saldoInicial";
            fieldSaldoInicial.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnEliminar
            // 
            btnEliminar.Animated = true;
            btnEliminar.BorderColor = Color.Gainsboro;
            btnEliminar.BorderRadius = 16;
            btnEliminar.BorderThickness = 1;
            btnEliminar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage");
            btnEliminar.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnEliminar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnEliminar.CustomizableEdges = customizableEdges1;
            btnEliminar.Dock = DockStyle.Fill;
            btnEliminar.FillColor = Color.White;
            btnEliminar.Font = new Font("Segoe UI", 9.75F);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.HoverState.BorderColor = Color.PeachPuff;
            btnEliminar.HoverState.FillColor = Color.PeachPuff;
            btnEliminar.HoverState.ForeColor = Color.White;
            btnEliminar.Location = new Point(1204, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnEliminar.Size = new Size(34, 35);
            btnEliminar.TabIndex = 11;
            // 
            // fieldId
            // 
            fieldId.Dock = DockStyle.Fill;
            fieldId.Font = new Font("Segoe UI", 11.25F);
            fieldId.ForeColor = Color.DimGray;
            fieldId.ImeMode = ImeMode.NoControl;
            fieldId.Location = new Point(1, 1);
            fieldId.Margin = new Padding(1);
            fieldId.Name = "fieldId";
            fieldId.Size = new Size(58, 39);
            fieldId.TabIndex = 13;
            fieldId.Text = "id";
            fieldId.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnEditar
            // 
            btnEditar.Animated = true;
            btnEditar.BorderColor = Color.Gainsboro;
            btnEditar.BorderRadius = 16;
            btnEditar.BorderThickness = 1;
            btnEditar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage1");
            btnEditar.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnEditar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnEditar.CustomizableEdges = customizableEdges3;
            btnEditar.Dock = DockStyle.Fill;
            btnEditar.FillColor = Color.White;
            btnEditar.Font = new Font("Segoe UI", 9.75F);
            btnEditar.ForeColor = Color.White;
            btnEditar.HoverState.BorderColor = Color.PeachPuff;
            btnEditar.HoverState.FillColor = Color.PeachPuff;
            btnEditar.Location = new Point(1164, 3);
            btnEditar.Name = "btnEditar";
            btnEditar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnEditar.Size = new Size(34, 35);
            btnEditar.TabIndex = 9;
            // 
            // fieldFechaApertura
            // 
            fieldFechaApertura.Dock = DockStyle.Fill;
            fieldFechaApertura.Font = new Font("Segoe UI", 11.25F);
            fieldFechaApertura.ForeColor = Color.DimGray;
            fieldFechaApertura.ImeMode = ImeMode.NoControl;
            fieldFechaApertura.Location = new Point(61, 1);
            fieldFechaApertura.Margin = new Padding(1);
            fieldFechaApertura.Name = "fieldFechaApertura";
            fieldFechaApertura.Size = new Size(148, 39);
            fieldFechaApertura.TabIndex = 4;
            fieldFechaApertura.Text = "fechaApertura";
            fieldFechaApertura.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldEstado
            // 
            fieldEstado.Dock = DockStyle.Fill;
            fieldEstado.Font = new Font("Segoe UI", 11.25F);
            fieldEstado.ForeColor = Color.DimGray;
            fieldEstado.Image = Properties.Resources.open_sign_20px;
            fieldEstado.ImeMode = ImeMode.NoControl;
            fieldEstado.Location = new Point(741, 1);
            fieldEstado.Margin = new Padding(1);
            fieldEstado.Name = "fieldEstado";
            fieldEstado.Size = new Size(108, 39);
            fieldEstado.TabIndex = 34;
            fieldEstado.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnDescargarInforme
            // 
            btnDescargarInforme.Animated = true;
            btnDescargarInforme.BorderColor = Color.Gainsboro;
            btnDescargarInforme.BorderRadius = 16;
            btnDescargarInforme.BorderThickness = 1;
            btnDescargarInforme.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage2");
            btnDescargarInforme.CustomImages.Image = (Image) resources.GetObject("resource.Image2");
            btnDescargarInforme.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnDescargarInforme.CustomizableEdges = customizableEdges5;
            btnDescargarInforme.Dock = DockStyle.Fill;
            btnDescargarInforme.FillColor = Color.White;
            btnDescargarInforme.Font = new Font("Segoe UI", 9.75F);
            btnDescargarInforme.ForeColor = Color.White;
            btnDescargarInforme.HoverState.BorderColor = Color.PeachPuff;
            btnDescargarInforme.HoverState.FillColor = Color.PeachPuff;
            btnDescargarInforme.Location = new Point(1124, 3);
            btnDescargarInforme.Name = "btnDescargarInforme";
            btnDescargarInforme.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnDescargarInforme.Size = new Size(34, 35);
            btnDescargarInforme.TabIndex = 36;
            // 
            // fieldCantidadMovimientos
            // 
            fieldCantidadMovimientos.Dock = DockStyle.Fill;
            fieldCantidadMovimientos.Font = new Font("Segoe UI", 11.25F);
            fieldCantidadMovimientos.ForeColor = Color.DimGray;
            fieldCantidadMovimientos.ImeMode = ImeMode.NoControl;
            fieldCantidadMovimientos.Location = new Point(341, 1);
            fieldCantidadMovimientos.Margin = new Padding(1);
            fieldCantidadMovimientos.Name = "fieldCantidadMovimientos";
            fieldCantidadMovimientos.Size = new Size(118, 39);
            fieldCantidadMovimientos.TabIndex = 38;
            fieldCantidadMovimientos.Text = "cantMovimientos";
            fieldCantidadMovimientos.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // VistaTuplaCaja
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1241, 42);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaTuplaCaja";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaTuplaCaja";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private Guna2Button btnEliminar;
        private Label fieldFechaApertura;
        private Label fieldId;
        private Guna2Button btnEditar;
        private Label fieldSaldoInicial;
        private Label simboloPeso1;
        private Label fieldSaldoActual;
        private Label simboloPeso2;
        private Label fieldFechaCierre;
        private Label fieldEstado;
        private Guna2Button btnDescargarInforme;
        private Label fieldNombreUsuario;
        private Label fieldCantidadMovimientos;
    }
}