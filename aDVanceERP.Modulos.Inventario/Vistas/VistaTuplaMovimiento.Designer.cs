using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    partial class VistaTuplaMovimiento {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaTuplaMovimiento));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            separador1 = new Guna2Separator();
            layoutVista = new TableLayoutPanel();
            fieldTipoMovimiento = new Guna2Button();
            fieldSaldoFinal = new Label();
            fieldSaldoInicial = new Label();
            btnEliminar = new Guna2Button();
            fieldNombreAlmacenDestino = new Label();
            fieldId = new Label();
            btnEditar = new Guna2Button();
            fieldCantidadMovida = new Label();
            fieldFecha = new Label();
            fieldIcono = new PictureBox();
            fieldNombreAlmacenOrigen = new Label();
            fieldNombreProducto = new Label();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            ((ISupportInitialize) fieldIcono).BeginInit();
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
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBase.Controls.Add(separador1, 0, 1);
            layoutBase.Controls.Add(layoutVista, 0, 0);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 2;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutBase.Size = new Size(1241, 42);
            layoutBase.TabIndex = 1;
            // 
            // separador1
            // 
            separador1.Dock = DockStyle.Fill;
            separador1.FillColor = Color.Gainsboro;
            separador1.Location = new Point(1, 38);
            separador1.Margin = new Padding(1);
            separador1.Name = "separador1";
            separador1.Size = new Size(1239, 3);
            separador1.TabIndex = 76;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.White;
            layoutVista.ColumnCount = 12;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            layoutVista.Controls.Add(fieldTipoMovimiento, 8, 0);
            layoutVista.Controls.Add(fieldSaldoFinal, 7, 0);
            layoutVista.Controls.Add(fieldSaldoInicial, 5, 0);
            layoutVista.Controls.Add(btnEliminar, 11, 0);
            layoutVista.Controls.Add(fieldNombreAlmacenDestino, 4, 0);
            layoutVista.Controls.Add(fieldId, 0, 0);
            layoutVista.Controls.Add(btnEditar, 10, 0);
            layoutVista.Controls.Add(fieldCantidadMovida, 6, 0);
            layoutVista.Controls.Add(fieldFecha, 9, 0);
            layoutVista.Controls.Add(fieldIcono, 3, 0);
            layoutVista.Controls.Add(fieldNombreAlmacenOrigen, 2, 0);
            layoutVista.Controls.Add(fieldNombreProducto, 1, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0, 0, 0, 1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(1241, 36);
            layoutVista.TabIndex = 18;
            // 
            // fieldTipoMovimiento
            // 
            fieldTipoMovimiento.AutoRoundedCorners = true;
            fieldTipoMovimiento.BorderColor = Color.Gainsboro;
            fieldTipoMovimiento.BorderRadius = 11;
            fieldTipoMovimiento.BorderThickness = 1;
            fieldTipoMovimiento.CustomizableEdges = customizableEdges1;
            fieldTipoMovimiento.DisabledState.BorderColor = Color.Gainsboro;
            fieldTipoMovimiento.DisabledState.CustomBorderColor = Color.Gainsboro;
            fieldTipoMovimiento.DisabledState.FillColor = Color.Gainsboro;
            fieldTipoMovimiento.DisabledState.ForeColor = Color.DimGray;
            fieldTipoMovimiento.Dock = DockStyle.Left;
            fieldTipoMovimiento.Enabled = false;
            fieldTipoMovimiento.FillColor = Color.Gainsboro;
            fieldTipoMovimiento.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTipoMovimiento.ForeColor = Color.DimGray;
            fieldTipoMovimiento.HoverState.BorderColor = Color.PeachPuff;
            fieldTipoMovimiento.HoverState.FillColor = Color.PeachPuff;
            fieldTipoMovimiento.HoverState.ForeColor = Color.Black;
            fieldTipoMovimiento.Location = new Point(853, 6);
            fieldTipoMovimiento.Margin = new Padding(6);
            fieldTipoMovimiento.Name = "fieldTipoMovimiento";
            fieldTipoMovimiento.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fieldTipoMovimiento.Size = new Size(188, 24);
            fieldTipoMovimiento.TabIndex = 43;
            fieldTipoMovimiento.Text = "tipo";
            fieldTipoMovimiento.TextOffset = new Point(0, -1);
            // 
            // fieldSaldoFinal
            // 
            fieldSaldoFinal.AutoEllipsis = true;
            fieldSaldoFinal.Dock = DockStyle.Fill;
            fieldSaldoFinal.Font = new Font("Segoe UI", 11.25F);
            fieldSaldoFinal.ForeColor = Color.DimGray;
            fieldSaldoFinal.ImeMode = ImeMode.NoControl;
            fieldSaldoFinal.Location = new Point(738, 1);
            fieldSaldoFinal.Margin = new Padding(1);
            fieldSaldoFinal.Name = "fieldSaldoFinal";
            fieldSaldoFinal.Size = new Size(108, 34);
            fieldSaldoFinal.TabIndex = 22;
            fieldSaldoFinal.Text = "saldoFinal";
            fieldSaldoFinal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldSaldoInicial
            // 
            fieldSaldoInicial.AutoEllipsis = true;
            fieldSaldoInicial.Dock = DockStyle.Fill;
            fieldSaldoInicial.Font = new Font("Segoe UI", 11.25F);
            fieldSaldoInicial.ForeColor = Color.DimGray;
            fieldSaldoInicial.ImeMode = ImeMode.NoControl;
            fieldSaldoInicial.Location = new Point(518, 1);
            fieldSaldoInicial.Margin = new Padding(1);
            fieldSaldoInicial.Name = "fieldSaldoInicial";
            fieldSaldoInicial.Size = new Size(108, 34);
            fieldSaldoInicial.TabIndex = 21;
            fieldSaldoInicial.Text = "saldoInicial";
            fieldSaldoInicial.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnEliminar
            // 
            btnEliminar.Animated = true;
            btnEliminar.AutoRoundedCorners = true;
            btnEliminar.BorderColor = Color.Gainsboro;
            btnEliminar.BorderRadius = 14;
            btnEliminar.BorderThickness = 1;
            btnEliminar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage");
            btnEliminar.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnEliminar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnEliminar.CustomizableEdges = customizableEdges3;
            btnEliminar.Dock = DockStyle.Fill;
            btnEliminar.FillColor = Color.White;
            btnEliminar.Font = new Font("Segoe UI", 9.75F);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.HoverState.BorderColor = Color.PeachPuff;
            btnEliminar.HoverState.FillColor = Color.PeachPuff;
            btnEliminar.HoverState.ForeColor = Color.White;
            btnEliminar.Location = new Point(1207, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnEliminar.Size = new Size(31, 30);
            btnEliminar.TabIndex = 11;
            // 
            // fieldNombreAlmacenDestino
            // 
            fieldNombreAlmacenDestino.AutoEllipsis = true;
            fieldNombreAlmacenDestino.Dock = DockStyle.Fill;
            fieldNombreAlmacenDestino.Font = new Font("Segoe UI", 11.25F);
            fieldNombreAlmacenDestino.ForeColor = Color.DimGray;
            fieldNombreAlmacenDestino.ImeMode = ImeMode.NoControl;
            fieldNombreAlmacenDestino.Location = new Point(398, 1);
            fieldNombreAlmacenDestino.Margin = new Padding(1);
            fieldNombreAlmacenDestino.Name = "fieldNombreAlmacenDestino";
            fieldNombreAlmacenDestino.Size = new Size(118, 34);
            fieldNombreAlmacenDestino.TabIndex = 4;
            fieldNombreAlmacenDestino.Text = "nombreAlmacenDestino";
            fieldNombreAlmacenDestino.TextAlign = ContentAlignment.MiddleLeft;
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
            fieldId.Size = new Size(58, 34);
            fieldId.TabIndex = 13;
            fieldId.Text = "id";
            fieldId.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnEditar
            // 
            btnEditar.Animated = true;
            btnEditar.AutoRoundedCorners = true;
            btnEditar.BorderColor = Color.Gainsboro;
            btnEditar.BorderRadius = 14;
            btnEditar.BorderThickness = 1;
            btnEditar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage1");
            btnEditar.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnEditar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnEditar.CustomizableEdges = customizableEdges5;
            btnEditar.Dock = DockStyle.Fill;
            btnEditar.FillColor = Color.White;
            btnEditar.Font = new Font("Segoe UI", 9.75F);
            btnEditar.ForeColor = Color.White;
            btnEditar.HoverState.BorderColor = Color.PeachPuff;
            btnEditar.HoverState.FillColor = Color.PeachPuff;
            btnEditar.Location = new Point(1170, 3);
            btnEditar.Name = "btnEditar";
            btnEditar.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnEditar.Size = new Size(31, 30);
            btnEditar.TabIndex = 9;
            // 
            // fieldCantidadMovida
            // 
            fieldCantidadMovida.AutoEllipsis = true;
            fieldCantidadMovida.Dock = DockStyle.Fill;
            fieldCantidadMovida.Font = new Font("Segoe UI", 11.25F);
            fieldCantidadMovida.ForeColor = Color.Black;
            fieldCantidadMovida.ImeMode = ImeMode.NoControl;
            fieldCantidadMovida.Location = new Point(628, 1);
            fieldCantidadMovida.Margin = new Padding(1);
            fieldCantidadMovida.Name = "fieldCantidadMovida";
            fieldCantidadMovida.Size = new Size(108, 34);
            fieldCantidadMovida.TabIndex = 6;
            fieldCantidadMovida.Text = "cantidadMov";
            fieldCantidadMovida.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldFecha
            // 
            fieldFecha.Dock = DockStyle.Fill;
            fieldFecha.Font = new Font("Segoe UI", 11.25F);
            fieldFecha.ForeColor = Color.DimGray;
            fieldFecha.ImeMode = ImeMode.NoControl;
            fieldFecha.Location = new Point(1048, 1);
            fieldFecha.Margin = new Padding(1);
            fieldFecha.Name = "fieldFecha";
            fieldFecha.Size = new Size(118, 34);
            fieldFecha.TabIndex = 17;
            fieldFecha.Text = "fecha";
            fieldFecha.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldIcono
            // 
            fieldIcono.BackgroundImage = (Image) resources.GetObject("fieldIcono.BackgroundImage");
            fieldIcono.BackgroundImageLayout = ImageLayout.Center;
            fieldIcono.Dock = DockStyle.Fill;
            fieldIcono.Location = new Point(357, 3);
            fieldIcono.Margin = new Padding(0, 3, 0, 0);
            fieldIcono.Name = "fieldIcono";
            fieldIcono.Size = new Size(40, 33);
            fieldIcono.TabIndex = 18;
            fieldIcono.TabStop = false;
            // 
            // fieldNombreAlmacenOrigen
            // 
            fieldNombreAlmacenOrigen.AutoEllipsis = true;
            fieldNombreAlmacenOrigen.Dock = DockStyle.Fill;
            fieldNombreAlmacenOrigen.Font = new Font("Segoe UI", 11.25F);
            fieldNombreAlmacenOrigen.ForeColor = Color.DimGray;
            fieldNombreAlmacenOrigen.ImeMode = ImeMode.NoControl;
            fieldNombreAlmacenOrigen.Location = new Point(238, 1);
            fieldNombreAlmacenOrigen.Margin = new Padding(1);
            fieldNombreAlmacenOrigen.Name = "fieldNombreAlmacenOrigen";
            fieldNombreAlmacenOrigen.Size = new Size(118, 34);
            fieldNombreAlmacenOrigen.TabIndex = 19;
            fieldNombreAlmacenOrigen.Text = "nombreAlmacenOrigen";
            fieldNombreAlmacenOrigen.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldNombreProducto
            // 
            fieldNombreProducto.Dock = DockStyle.Fill;
            fieldNombreProducto.Font = new Font("Segoe UI", 11.25F);
            fieldNombreProducto.ForeColor = Color.DimGray;
            fieldNombreProducto.ImeMode = ImeMode.NoControl;
            fieldNombreProducto.Location = new Point(61, 1);
            fieldNombreProducto.Margin = new Padding(1);
            fieldNombreProducto.Name = "fieldNombreProducto";
            fieldNombreProducto.Size = new Size(175, 34);
            fieldNombreProducto.TabIndex = 20;
            fieldNombreProducto.Text = "nombreProducto";
            fieldNombreProducto.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VistaTuplaMovimiento
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1241, 42);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaTuplaMovimiento";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaTuplaMovimiento";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ((ISupportInitialize) fieldIcono).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private Guna2Button btnEliminar;
        private Label fieldNombreAlmacenDestino;
        private Label fieldId;
        private Guna2Button btnEditar;
        private Label fieldCantidadMovida;
        private Label fieldFecha;
        private PictureBox fieldIcono;
        private Label fieldNombreAlmacenOrigen;
        private Label fieldNombreProducto;
        private Label fieldSaldoInicial;
        private Label fieldSaldoFinal;
        private Guna2Separator separador1;
        private Guna2Button fieldTipoMovimiento;
    }
}