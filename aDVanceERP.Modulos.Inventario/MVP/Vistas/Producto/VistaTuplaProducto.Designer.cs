using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto {
    partial class VistaTuplaProducto {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaTuplaProducto));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldId = new Label();
            btnEditar = new Guna2Button();
            btnMovimientoNegativo = new Guna2Button();
            btnMovimientoPositivo = new Guna2Button();
            fieldDescripcion = new Label();
            fieldPrecioVentaBase = new Label();
            fieldCodigo = new Label();
            btnEliminar = new Guna2Button();
            fieldNombre = new Label();
            fieldCostoUnitario = new Label();
            fieldStock = new Label();
            fieldUnidadMedida = new Label();
            fieldFechaUltimoMovimiento = new Label();
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
            layoutVista.ColumnCount = 13;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.Controls.Add(fieldFechaUltimoMovimiento, 2, 0);
            layoutVista.Controls.Add(fieldId, 0, 0);
            layoutVista.Controls.Add(btnEditar, 11, 0);
            layoutVista.Controls.Add(btnMovimientoNegativo, 10, 0);
            layoutVista.Controls.Add(btnMovimientoPositivo, 9, 0);
            layoutVista.Controls.Add(fieldDescripcion, 4, 0);
            layoutVista.Controls.Add(fieldPrecioVentaBase, 6, 0);
            layoutVista.Controls.Add(fieldCodigo, 1, 0);
            layoutVista.Controls.Add(btnEliminar, 12, 0);
            layoutVista.Controls.Add(fieldCostoUnitario, 4, 0);
            layoutVista.Controls.Add(fieldStock, 7, 0);
            layoutVista.Controls.Add(fieldUnidadMedida, 8, 0);
            layoutVista.Controls.Add(fieldNombre, 3, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0, 0, 0, 1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(1241, 41);
            layoutVista.TabIndex = 18;
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
            btnEditar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage");
            btnEditar.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnEditar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnEditar.CustomizableEdges = customizableEdges9;
            btnEditar.Dock = DockStyle.Fill;
            btnEditar.FillColor = Color.White;
            btnEditar.Font = new Font("Segoe UI", 9.75F);
            btnEditar.ForeColor = Color.White;
            btnEditar.HoverState.BorderColor = Color.PeachPuff;
            btnEditar.HoverState.FillColor = Color.PeachPuff;
            btnEditar.Location = new Point(1163, 3);
            btnEditar.Name = "btnEditar";
            btnEditar.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnEditar.Size = new Size(34, 35);
            btnEditar.TabIndex = 9;
            // 
            // btnMovimientoNegativo
            // 
            btnMovimientoNegativo.Animated = true;
            btnMovimientoNegativo.BorderColor = Color.Gainsboro;
            btnMovimientoNegativo.BorderRadius = 16;
            btnMovimientoNegativo.BorderThickness = 1;
            btnMovimientoNegativo.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage1");
            btnMovimientoNegativo.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnMovimientoNegativo.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnMovimientoNegativo.CustomizableEdges = customizableEdges11;
            btnMovimientoNegativo.Dock = DockStyle.Fill;
            btnMovimientoNegativo.FillColor = Color.White;
            btnMovimientoNegativo.Font = new Font("Segoe UI", 9.75F);
            btnMovimientoNegativo.ForeColor = Color.White;
            btnMovimientoNegativo.HoverState.BorderColor = Color.PeachPuff;
            btnMovimientoNegativo.HoverState.FillColor = Color.PeachPuff;
            btnMovimientoNegativo.Location = new Point(1123, 3);
            btnMovimientoNegativo.Name = "btnMovimientoNegativo";
            btnMovimientoNegativo.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnMovimientoNegativo.Size = new Size(34, 35);
            btnMovimientoNegativo.TabIndex = 18;
            // 
            // btnMovimientoPositivo
            // 
            btnMovimientoPositivo.Animated = true;
            btnMovimientoPositivo.BorderColor = Color.Gainsboro;
            btnMovimientoPositivo.BorderRadius = 16;
            btnMovimientoPositivo.BorderThickness = 1;
            btnMovimientoPositivo.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage2");
            btnMovimientoPositivo.CustomImages.Image = (Image) resources.GetObject("resource.Image2");
            btnMovimientoPositivo.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnMovimientoPositivo.CustomizableEdges = customizableEdges13;
            btnMovimientoPositivo.Dock = DockStyle.Fill;
            btnMovimientoPositivo.FillColor = Color.White;
            btnMovimientoPositivo.Font = new Font("Segoe UI", 9.75F);
            btnMovimientoPositivo.ForeColor = Color.White;
            btnMovimientoPositivo.HoverState.BorderColor = Color.PeachPuff;
            btnMovimientoPositivo.HoverState.FillColor = Color.PeachPuff;
            btnMovimientoPositivo.Location = new Point(1083, 3);
            btnMovimientoPositivo.Name = "btnMovimientoPositivo";
            btnMovimientoPositivo.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnMovimientoPositivo.Size = new Size(34, 35);
            btnMovimientoPositivo.TabIndex = 19;
            // 
            // fieldDescripcion
            // 
            fieldDescripcion.AutoEllipsis = true;
            fieldDescripcion.Dock = DockStyle.Fill;
            fieldDescripcion.Font = new Font("Segoe UI", 11.25F);
            fieldDescripcion.ForeColor = Color.DimGray;
            fieldDescripcion.ImeMode = ImeMode.NoControl;
            fieldDescripcion.Location = new Point(449, 1);
            fieldDescripcion.Margin = new Padding(1);
            fieldDescripcion.Name = "fieldDescripcion";
            fieldDescripcion.Size = new Size(220, 39);
            fieldDescripcion.TabIndex = 6;
            fieldDescripcion.Text = "descripcion";
            fieldDescripcion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldPrecioVentaBase
            // 
            fieldPrecioVentaBase.Dock = DockStyle.Fill;
            fieldPrecioVentaBase.Font = new Font("Segoe UI", 11.25F);
            fieldPrecioVentaBase.ForeColor = Color.DimGray;
            fieldPrecioVentaBase.ImeMode = ImeMode.NoControl;
            fieldPrecioVentaBase.Location = new Point(781, 1);
            fieldPrecioVentaBase.Margin = new Padding(1);
            fieldPrecioVentaBase.Name = "fieldPrecioVentaBase";
            fieldPrecioVentaBase.Size = new Size(108, 39);
            fieldPrecioVentaBase.TabIndex = 17;
            fieldPrecioVentaBase.Text = "precioVenta";
            fieldPrecioVentaBase.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldCodigo
            // 
            fieldCodigo.Dock = DockStyle.Fill;
            fieldCodigo.Font = new Font("Segoe UI", 11.25F);
            fieldCodigo.ForeColor = Color.DimGray;
            fieldCodigo.ImeMode = ImeMode.NoControl;
            fieldCodigo.Location = new Point(61, 1);
            fieldCodigo.Margin = new Padding(1);
            fieldCodigo.Name = "fieldCodigo";
            fieldCodigo.Size = new Size(118, 39);
            fieldCodigo.TabIndex = 15;
            fieldCodigo.Text = "codigo";
            fieldCodigo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnEliminar
            // 
            btnEliminar.Animated = true;
            btnEliminar.BorderColor = Color.Gainsboro;
            btnEliminar.BorderRadius = 16;
            btnEliminar.BorderThickness = 1;
            btnEliminar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage3");
            btnEliminar.CustomImages.Image = (Image) resources.GetObject("resource.Image3");
            btnEliminar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnEliminar.CustomizableEdges = customizableEdges15;
            btnEliminar.Dock = DockStyle.Fill;
            btnEliminar.FillColor = Color.White;
            btnEliminar.Font = new Font("Segoe UI", 9.75F);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.HoverState.BorderColor = Color.PeachPuff;
            btnEliminar.HoverState.FillColor = Color.PeachPuff;
            btnEliminar.HoverState.ForeColor = Color.White;
            btnEliminar.Location = new Point(1203, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnEliminar.Size = new Size(35, 35);
            btnEliminar.TabIndex = 21;
            // 
            // fieldNombre
            // 
            fieldNombre.AutoEllipsis = true;
            fieldNombre.Dock = DockStyle.Fill;
            fieldNombre.Font = new Font("Segoe UI", 11.25F);
            fieldNombre.ForeColor = Color.DimGray;
            fieldNombre.ImeMode = ImeMode.NoControl;
            fieldNombre.Location = new Point(301, 1);
            fieldNombre.Margin = new Padding(1);
            fieldNombre.Name = "fieldNombre";
            fieldNombre.Size = new Size(146, 39);
            fieldNombre.TabIndex = 4;
            fieldNombre.Text = "nombre";
            fieldNombre.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldCostoUnitario
            // 
            fieldCostoUnitario.Dock = DockStyle.Fill;
            fieldCostoUnitario.Font = new Font("Segoe UI", 11.25F);
            fieldCostoUnitario.ForeColor = Color.DimGray;
            fieldCostoUnitario.ImeMode = ImeMode.NoControl;
            fieldCostoUnitario.Location = new Point(671, 1);
            fieldCostoUnitario.Margin = new Padding(1);
            fieldCostoUnitario.Name = "fieldCostoUnitario";
            fieldCostoUnitario.Size = new Size(108, 39);
            fieldCostoUnitario.TabIndex = 14;
            fieldCostoUnitario.Text = "precioCompra.";
            fieldCostoUnitario.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldStock
            // 
            fieldStock.Dock = DockStyle.Fill;
            fieldStock.Font = new Font("Segoe UI", 11.25F);
            fieldStock.ForeColor = Color.DimGray;
            fieldStock.ImeMode = ImeMode.NoControl;
            fieldStock.Location = new Point(891, 1);
            fieldStock.Margin = new Padding(1);
            fieldStock.Name = "fieldStock";
            fieldStock.Size = new Size(108, 39);
            fieldStock.TabIndex = 16;
            fieldStock.Text = "cantidad";
            fieldStock.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldUnidadMedida
            // 
            fieldUnidadMedida.Dock = DockStyle.Fill;
            fieldUnidadMedida.Font = new Font("Segoe UI", 11.25F);
            fieldUnidadMedida.ForeColor = Color.DimGray;
            fieldUnidadMedida.ImeMode = ImeMode.NoControl;
            fieldUnidadMedida.Location = new Point(1001, 1);
            fieldUnidadMedida.Margin = new Padding(1);
            fieldUnidadMedida.Name = "fieldUnidadMedida";
            fieldUnidadMedida.Size = new Size(78, 39);
            fieldUnidadMedida.TabIndex = 22;
            fieldUnidadMedida.Text = "u.m.";
            fieldUnidadMedida.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldFechaUltimoMovimiento
            // 
            fieldFechaUltimoMovimiento.AutoEllipsis = true;
            fieldFechaUltimoMovimiento.Dock = DockStyle.Fill;
            fieldFechaUltimoMovimiento.Font = new Font("Segoe UI", 11.25F);
            fieldFechaUltimoMovimiento.ForeColor = Color.DimGray;
            fieldFechaUltimoMovimiento.ImeMode = ImeMode.NoControl;
            fieldFechaUltimoMovimiento.Location = new Point(181, 1);
            fieldFechaUltimoMovimiento.Margin = new Padding(1);
            fieldFechaUltimoMovimiento.Name = "fieldFechaUltimoMovimiento";
            fieldFechaUltimoMovimiento.Size = new Size(118, 39);
            fieldFechaUltimoMovimiento.TabIndex = 23;
            fieldFechaUltimoMovimiento.Text = "ultimoMov";
            fieldFechaUltimoMovimiento.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // VistaTuplaProducto
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1241, 42);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaTuplaProducto";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaTuplaProducto";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private Label fieldNombre;
        private Label fieldId;
        private Guna2Button btnEditar;
        private Label fieldCostoUnitario;
        private Label fieldDescripcion;
        private Label fieldCodigo;
        private Label fieldStock;
        private Label fieldPrecioVentaBase;
        private Guna2Button btnMovimientoNegativo;
        private Guna2Button btnMovimientoPositivo;
        private Guna2Button btnEliminar;
        private Label fieldUnidadMedida;
        private Label fieldFechaUltimoMovimiento;
    }
}