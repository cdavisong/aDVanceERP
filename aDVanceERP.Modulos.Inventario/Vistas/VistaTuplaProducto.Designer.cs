using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Inventario.Vistas {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            separador1 = new Guna2Separator();
            layoutVista = new TableLayoutPanel();
            fieldFechaUltimoMovimiento = new Label();
            fieldId = new Label();
            btnEditar = new Guna2Button();
            btnMovimientoNegativo = new Guna2Button();
            btnMovimientoPositivo = new Guna2Button();
            fieldNombreDescripcion = new Label();
            fieldPrecioVentaBase = new Label();
            fieldCodigo = new Label();
            btnEliminar = new Guna2Button();
            fieldCostoUnitario = new Label();
            fieldStock = new Label();
            fieldUnidadMedida = new Label();
            fieldPresentaciones = new Guna2Button();
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
            separador1.TabIndex = 74;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.White;
            layoutVista.ColumnCount = 13;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            layoutVista.Controls.Add(fieldPresentaciones, 6, 0);
            layoutVista.Controls.Add(fieldFechaUltimoMovimiento, 2, 0);
            layoutVista.Controls.Add(fieldId, 0, 0);
            layoutVista.Controls.Add(btnEditar, 11, 0);
            layoutVista.Controls.Add(btnMovimientoNegativo, 10, 0);
            layoutVista.Controls.Add(btnMovimientoPositivo, 9, 0);
            layoutVista.Controls.Add(fieldNombreDescripcion, 3, 0);
            layoutVista.Controls.Add(fieldPrecioVentaBase, 5, 0);
            layoutVista.Controls.Add(fieldCodigo, 1, 0);
            layoutVista.Controls.Add(btnEliminar, 12, 0);
            layoutVista.Controls.Add(fieldStock, 7, 0);
            layoutVista.Controls.Add(fieldUnidadMedida, 8, 0);
            layoutVista.Controls.Add(fieldCostoUnitario, 4, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(1241, 37);
            layoutVista.TabIndex = 18;
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
            fieldFechaUltimoMovimiento.Size = new Size(118, 35);
            fieldFechaUltimoMovimiento.TabIndex = 23;
            fieldFechaUltimoMovimiento.Text = "ultimoMov";
            fieldFechaUltimoMovimiento.TextAlign = ContentAlignment.MiddleLeft;
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
            fieldId.Size = new Size(58, 35);
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
            btnEditar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage");
            btnEditar.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnEditar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnEditar.CustomizableEdges = customizableEdges3;
            btnEditar.Dock = DockStyle.Fill;
            btnEditar.FillColor = Color.White;
            btnEditar.Font = new Font("Segoe UI", 9.75F);
            btnEditar.ForeColor = Color.White;
            btnEditar.HoverState.BorderColor = Color.PeachPuff;
            btnEditar.HoverState.FillColor = Color.PeachPuff;
            btnEditar.Location = new Point(1170, 3);
            btnEditar.Name = "btnEditar";
            btnEditar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnEditar.Size = new Size(31, 31);
            btnEditar.TabIndex = 9;
            // 
            // btnMovimientoNegativo
            // 
            btnMovimientoNegativo.Animated = true;
            btnMovimientoNegativo.AutoRoundedCorners = true;
            btnMovimientoNegativo.BorderColor = Color.Gainsboro;
            btnMovimientoNegativo.BorderRadius = 14;
            btnMovimientoNegativo.BorderThickness = 1;
            btnMovimientoNegativo.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage1");
            btnMovimientoNegativo.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnMovimientoNegativo.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnMovimientoNegativo.CustomizableEdges = customizableEdges5;
            btnMovimientoNegativo.Dock = DockStyle.Fill;
            btnMovimientoNegativo.FillColor = Color.White;
            btnMovimientoNegativo.Font = new Font("Segoe UI", 9.75F);
            btnMovimientoNegativo.ForeColor = Color.White;
            btnMovimientoNegativo.HoverState.BorderColor = Color.PeachPuff;
            btnMovimientoNegativo.HoverState.FillColor = Color.PeachPuff;
            btnMovimientoNegativo.Location = new Point(1133, 3);
            btnMovimientoNegativo.Name = "btnMovimientoNegativo";
            btnMovimientoNegativo.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnMovimientoNegativo.Size = new Size(31, 31);
            btnMovimientoNegativo.TabIndex = 18;
            // 
            // btnMovimientoPositivo
            // 
            btnMovimientoPositivo.Animated = true;
            btnMovimientoPositivo.AutoRoundedCorners = true;
            btnMovimientoPositivo.BorderColor = Color.Gainsboro;
            btnMovimientoPositivo.BorderRadius = 14;
            btnMovimientoPositivo.BorderThickness = 1;
            btnMovimientoPositivo.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage2");
            btnMovimientoPositivo.CustomImages.Image = (Image) resources.GetObject("resource.Image2");
            btnMovimientoPositivo.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnMovimientoPositivo.CustomizableEdges = customizableEdges7;
            btnMovimientoPositivo.Dock = DockStyle.Fill;
            btnMovimientoPositivo.FillColor = Color.White;
            btnMovimientoPositivo.Font = new Font("Segoe UI", 9.75F);
            btnMovimientoPositivo.ForeColor = Color.White;
            btnMovimientoPositivo.HoverState.BorderColor = Color.PeachPuff;
            btnMovimientoPositivo.HoverState.FillColor = Color.PeachPuff;
            btnMovimientoPositivo.Location = new Point(1096, 3);
            btnMovimientoPositivo.Name = "btnMovimientoPositivo";
            btnMovimientoPositivo.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnMovimientoPositivo.Size = new Size(31, 31);
            btnMovimientoPositivo.TabIndex = 19;
            // 
            // fieldDescripcion
            // 
            fieldNombreDescripcion.AutoEllipsis = true;
            fieldNombreDescripcion.Dock = DockStyle.Fill;
            fieldNombreDescripcion.Font = new Font("Segoe UI", 11.25F);
            fieldNombreDescripcion.ForeColor = Color.DimGray;
            fieldNombreDescripcion.ImeMode = ImeMode.NoControl;
            fieldNombreDescripcion.Location = new Point(301, 1);
            fieldNombreDescripcion.Margin = new Padding(1);
            fieldNombreDescripcion.Name = "fieldDescripcion";
            fieldNombreDescripcion.Size = new Size(201, 35);
            fieldNombreDescripcion.TabIndex = 6;
            fieldNombreDescripcion.Text = "descripcion";
            fieldNombreDescripcion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldPrecioVentaBase
            // 
            fieldPrecioVentaBase.Dock = DockStyle.Fill;
            fieldPrecioVentaBase.Font = new Font("Segoe UI", 11.25F);
            fieldPrecioVentaBase.ForeColor = Color.Black;
            fieldPrecioVentaBase.ImeMode = ImeMode.NoControl;
            fieldPrecioVentaBase.Location = new Point(614, 1);
            fieldPrecioVentaBase.Margin = new Padding(1);
            fieldPrecioVentaBase.Name = "fieldPrecioVentaBase";
            fieldPrecioVentaBase.Size = new Size(108, 35);
            fieldPrecioVentaBase.TabIndex = 17;
            fieldPrecioVentaBase.Text = "precioVenta";
            fieldPrecioVentaBase.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldCodigo
            // 
            fieldCodigo.Dock = DockStyle.Fill;
            fieldCodigo.Font = new Font("Segoe UI", 11.25F);
            fieldCodigo.ForeColor = Color.Black;
            fieldCodigo.ImeMode = ImeMode.NoControl;
            fieldCodigo.Location = new Point(61, 1);
            fieldCodigo.Margin = new Padding(1);
            fieldCodigo.Name = "fieldCodigo";
            fieldCodigo.Size = new Size(118, 35);
            fieldCodigo.TabIndex = 15;
            fieldCodigo.Text = "codigo";
            fieldCodigo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnEliminar
            // 
            btnEliminar.Animated = true;
            btnEliminar.AutoRoundedCorners = true;
            btnEliminar.BorderColor = Color.Gainsboro;
            btnEliminar.BorderRadius = 14;
            btnEliminar.BorderThickness = 1;
            btnEliminar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage3");
            btnEliminar.CustomImages.Image = (Image) resources.GetObject("resource.Image3");
            btnEliminar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnEliminar.CustomizableEdges = customizableEdges9;
            btnEliminar.Dock = DockStyle.Fill;
            btnEliminar.FillColor = Color.White;
            btnEliminar.Font = new Font("Segoe UI", 9.75F);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.HoverState.BorderColor = Color.PeachPuff;
            btnEliminar.HoverState.FillColor = Color.PeachPuff;
            btnEliminar.HoverState.ForeColor = Color.White;
            btnEliminar.Location = new Point(1207, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnEliminar.Size = new Size(31, 31);
            btnEliminar.TabIndex = 21;
            // 
            // fieldCostoUnitario
            // 
            fieldCostoUnitario.Dock = DockStyle.Fill;
            fieldCostoUnitario.Font = new Font("Segoe UI", 11.25F);
            fieldCostoUnitario.ForeColor = Color.Black;
            fieldCostoUnitario.ImeMode = ImeMode.NoControl;
            fieldCostoUnitario.Location = new Point(504, 1);
            fieldCostoUnitario.Margin = new Padding(1);
            fieldCostoUnitario.Name = "fieldCostoUnitario";
            fieldCostoUnitario.Size = new Size(108, 35);
            fieldCostoUnitario.TabIndex = 14;
            fieldCostoUnitario.Text = "precioCompra.";
            fieldCostoUnitario.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldStock
            // 
            fieldStock.Dock = DockStyle.Fill;
            fieldStock.Font = new Font("Segoe UI", 11.25F);
            fieldStock.ForeColor = Color.DimGray;
            fieldStock.ImeMode = ImeMode.NoControl;
            fieldStock.Location = new Point(904, 1);
            fieldStock.Margin = new Padding(1);
            fieldStock.Name = "fieldStock";
            fieldStock.Size = new Size(108, 35);
            fieldStock.TabIndex = 16;
            fieldStock.Text = "cantidad";
            fieldStock.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldUnidadMedida
            // 
            fieldUnidadMedida.Dock = DockStyle.Fill;
            fieldUnidadMedida.Font = new Font("Segoe UI", 11.25F);
            fieldUnidadMedida.ForeColor = Color.DimGray;
            fieldUnidadMedida.ImeMode = ImeMode.NoControl;
            fieldUnidadMedida.Location = new Point(1014, 1);
            fieldUnidadMedida.Margin = new Padding(1);
            fieldUnidadMedida.Name = "fieldUnidadMedida";
            fieldUnidadMedida.Size = new Size(78, 35);
            fieldUnidadMedida.TabIndex = 22;
            fieldUnidadMedida.Text = "u.m.";
            fieldUnidadMedida.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldPresentaciones
            // 
            fieldPresentaciones.AutoRoundedCorners = true;
            fieldPresentaciones.BorderColor = Color.Gainsboro;
            fieldPresentaciones.BorderRadius = 11;
            fieldPresentaciones.BorderThickness = 1;
            fieldPresentaciones.Cursor = Cursors.Hand;
            fieldPresentaciones.CustomizableEdges = customizableEdges1;
            fieldPresentaciones.DisabledState.BorderColor = Color.Gainsboro;
            fieldPresentaciones.DisabledState.CustomBorderColor = Color.Gainsboro;
            fieldPresentaciones.DisabledState.FillColor = Color.Gainsboro;
            fieldPresentaciones.DisabledState.ForeColor = Color.DimGray;
            fieldPresentaciones.Dock = DockStyle.Left;
            fieldPresentaciones.FillColor = Color.Gainsboro;
            fieldPresentaciones.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldPresentaciones.ForeColor = Color.DimGray;
            fieldPresentaciones.HoverState.BorderColor = Color.PeachPuff;
            fieldPresentaciones.HoverState.FillColor = Color.PeachPuff;
            fieldPresentaciones.HoverState.ForeColor = Color.Black;
            fieldPresentaciones.Location = new Point(729, 6);
            fieldPresentaciones.Margin = new Padding(6);
            fieldPresentaciones.Name = "fieldPresentaciones";
            fieldPresentaciones.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fieldPresentaciones.Size = new Size(168, 25);
            fieldPresentaciones.TabIndex = 44;
            fieldPresentaciones.Text = "+ Sin presentaciones";
            fieldPresentaciones.TextOffset = new Point(0, -1);
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
        private Label fieldFechaUltimoMovimiento;
        private Label fieldId;
        private Guna2Button btnEditar;
        private Guna2Button btnMovimientoNegativo;
        private Guna2Button btnMovimientoPositivo;
        private Label fieldNombreDescripcion;
        private Label fieldPrecioVentaBase;
        private Label fieldCodigo;
        private Guna2Button btnEliminar;
        private Label fieldCostoUnitario;
        private Label fieldStock;
        private Label fieldUnidadMedida;
        private Guna2Separator separador1;
        private Guna2Button fieldPresentaciones;
    }
}