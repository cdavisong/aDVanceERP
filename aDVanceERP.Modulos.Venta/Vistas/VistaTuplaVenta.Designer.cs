using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Venta.Vistas {
    partial class VistaTuplaVenta {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaTuplaVenta));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldEstado = new Label();
            fieldMetodoPagoPrincipal = new Label();
            fieldId = new Label();
            btnVerFactura = new Guna2Button();
            btnAnular = new Guna2Button();
            simboloPeso4 = new Label();
            fieldImporteTotal = new Label();
            simboloPeso3 = new Label();
            simboloPeso2 = new Label();
            simboloPeso1 = new Label();
            fieldImpuestoTotal = new Label();
            fieldTotalBruto = new Label();
            fieldDescuentoTotal = new Label();
            fieldFechaVenta = new Label();
            fieldNombreCliente = new Label();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
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
            layoutBase.Size = new Size(1241, 42);
            layoutBase.TabIndex = 1;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.White;
            layoutVista.ColumnCount = 16;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.Controls.Add(fieldEstado, 12, 0);
            layoutVista.Controls.Add(fieldMetodoPagoPrincipal, 3, 0);
            layoutVista.Controls.Add(fieldId, 0, 0);
            layoutVista.Controls.Add(btnVerFactura, 14, 0);
            layoutVista.Controls.Add(btnAnular, 15, 0);
            layoutVista.Controls.Add(simboloPeso4, 11, 0);
            layoutVista.Controls.Add(fieldImporteTotal, 10, 0);
            layoutVista.Controls.Add(simboloPeso3, 9, 0);
            layoutVista.Controls.Add(simboloPeso2, 7, 0);
            layoutVista.Controls.Add(simboloPeso1, 5, 0);
            layoutVista.Controls.Add(fieldImpuestoTotal, 8, 0);
            layoutVista.Controls.Add(fieldTotalBruto, 4, 0);
            layoutVista.Controls.Add(fieldDescuentoTotal, 6, 0);
            layoutVista.Controls.Add(fieldFechaVenta, 1, 0);
            layoutVista.Controls.Add(fieldNombreCliente, 2, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0, 0, 0, 1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(1241, 41);
            layoutVista.TabIndex = 19;
            // 
            // fieldEstado
            // 
            fieldEstado.Dock = DockStyle.Fill;
            fieldEstado.Font = new Font("Segoe UI", 11.25F);
            fieldEstado.ForeColor = Color.DimGray;
            fieldEstado.ImeMode = ImeMode.NoControl;
            fieldEstado.Location = new Point(1022, 1);
            fieldEstado.Margin = new Padding(1);
            fieldEstado.Name = "fieldEstado";
            fieldEstado.Size = new Size(98, 39);
            fieldEstado.TabIndex = 38;
            fieldEstado.Text = "estado";
            fieldEstado.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldMetodoPagoPrincipal
            // 
            fieldMetodoPagoPrincipal.AutoEllipsis = true;
            fieldMetodoPagoPrincipal.Dock = DockStyle.Fill;
            fieldMetodoPagoPrincipal.Font = new Font("Segoe UI", 11.25F);
            fieldMetodoPagoPrincipal.ForeColor = Color.DimGray;
            fieldMetodoPagoPrincipal.ImeMode = ImeMode.NoControl;
            fieldMetodoPagoPrincipal.Location = new Point(356, 1);
            fieldMetodoPagoPrincipal.Margin = new Padding(5, 1, 1, 1);
            fieldMetodoPagoPrincipal.Name = "fieldMetodoPagoPrincipal";
            fieldMetodoPagoPrincipal.Size = new Size(144, 39);
            fieldMetodoPagoPrincipal.TabIndex = 37;
            fieldMetodoPagoPrincipal.Text = "metodoPago";
            fieldMetodoPagoPrincipal.TextAlign = ContentAlignment.MiddleLeft;
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
            // btnVerFactura
            // 
            btnVerFactura.Animated = true;
            btnVerFactura.BorderColor = Color.Gainsboro;
            btnVerFactura.BorderRadius = 16;
            btnVerFactura.BorderThickness = 1;
            btnVerFactura.CustomImages.HoveredImage = (Image)resources.GetObject("resource.HoveredImage");
            btnVerFactura.CustomImages.Image = (Image)resources.GetObject("resource.Image");
            btnVerFactura.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnVerFactura.CustomizableEdges = customizableEdges5;
            btnVerFactura.Dock = DockStyle.Fill;
            btnVerFactura.FillColor = Color.White;
            btnVerFactura.Font = new Font("Segoe UI", 9.75F);
            btnVerFactura.ForeColor = Color.White;
            btnVerFactura.HoverState.BorderColor = Color.PeachPuff;
            btnVerFactura.HoverState.FillColor = Color.PeachPuff;
            btnVerFactura.Location = new Point(1164, 3);
            btnVerFactura.Name = "btnVerFactura";
            btnVerFactura.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnVerFactura.Size = new Size(34, 35);
            btnVerFactura.TabIndex = 21;
            btnVerFactura.Visible = false;
            // 
            // btnAnular
            // 
            btnAnular.Animated = true;
            btnAnular.BorderColor = Color.Gainsboro;
            btnAnular.BorderRadius = 16;
            btnAnular.BorderThickness = 1;
            btnAnular.CustomImages.HoveredImage = (Image)resources.GetObject("resource.HoveredImage1");
            btnAnular.CustomImages.Image = (Image)resources.GetObject("resource.Image1");
            btnAnular.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnAnular.CustomizableEdges = customizableEdges7;
            btnAnular.Dock = DockStyle.Fill;
            btnAnular.FillColor = Color.White;
            btnAnular.Font = new Font("Segoe UI", 9.75F);
            btnAnular.ForeColor = Color.White;
            btnAnular.HoverState.BorderColor = Color.PeachPuff;
            btnAnular.HoverState.FillColor = Color.PeachPuff;
            btnAnular.HoverState.ForeColor = Color.White;
            btnAnular.Location = new Point(1204, 3);
            btnAnular.Name = "btnAnular";
            btnAnular.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnAnular.Size = new Size(34, 35);
            btnAnular.TabIndex = 22;
            // 
            // simboloPeso4
            // 
            simboloPeso4.Dock = DockStyle.Fill;
            simboloPeso4.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            simboloPeso4.ForeColor = Color.Black;
            simboloPeso4.ImageAlign = ContentAlignment.MiddleLeft;
            simboloPeso4.ImeMode = ImeMode.NoControl;
            simboloPeso4.Location = new Point(1004, 5);
            simboloPeso4.Margin = new Padding(3, 5, 3, 3);
            simboloPeso4.Name = "simboloPeso4";
            simboloPeso4.Size = new Size(14, 33);
            simboloPeso4.TabIndex = 30;
            simboloPeso4.Text = "$";
            simboloPeso4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldImporteTotal
            // 
            fieldImporteTotal.Dock = DockStyle.Fill;
            fieldImporteTotal.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldImporteTotal.ForeColor = Color.Black;
            fieldImporteTotal.ImeMode = ImeMode.NoControl;
            fieldImporteTotal.Location = new Point(892, 1);
            fieldImporteTotal.Margin = new Padding(1);
            fieldImporteTotal.Name = "fieldImporteTotal";
            fieldImporteTotal.Size = new Size(108, 39);
            fieldImporteTotal.TabIndex = 20;
            fieldImporteTotal.Text = "importeTotal";
            fieldImporteTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // simboloPeso3
            // 
            simboloPeso3.Dock = DockStyle.Fill;
            simboloPeso3.Font = new Font("Segoe UI", 11.25F);
            simboloPeso3.ForeColor = Color.Black;
            simboloPeso3.ImageAlign = ContentAlignment.MiddleLeft;
            simboloPeso3.ImeMode = ImeMode.NoControl;
            simboloPeso3.Location = new Point(874, 5);
            simboloPeso3.Margin = new Padding(3, 5, 3, 3);
            simboloPeso3.Name = "simboloPeso3";
            simboloPeso3.Size = new Size(14, 33);
            simboloPeso3.TabIndex = 31;
            simboloPeso3.Text = "$";
            simboloPeso3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // simboloPeso2
            // 
            simboloPeso2.Dock = DockStyle.Fill;
            simboloPeso2.Font = new Font("Segoe UI", 11.25F);
            simboloPeso2.ForeColor = Color.Black;
            simboloPeso2.ImageAlign = ContentAlignment.MiddleLeft;
            simboloPeso2.ImeMode = ImeMode.NoControl;
            simboloPeso2.Location = new Point(744, 5);
            simboloPeso2.Margin = new Padding(3, 5, 3, 3);
            simboloPeso2.Name = "simboloPeso2";
            simboloPeso2.Size = new Size(14, 33);
            simboloPeso2.TabIndex = 32;
            simboloPeso2.Text = "$";
            simboloPeso2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // simboloPeso1
            // 
            simboloPeso1.Dock = DockStyle.Fill;
            simboloPeso1.Font = new Font("Segoe UI", 11.25F);
            simboloPeso1.ForeColor = Color.Black;
            simboloPeso1.ImageAlign = ContentAlignment.MiddleLeft;
            simboloPeso1.ImeMode = ImeMode.NoControl;
            simboloPeso1.Location = new Point(614, 5);
            simboloPeso1.Margin = new Padding(3, 5, 3, 3);
            simboloPeso1.Name = "simboloPeso1";
            simboloPeso1.Size = new Size(14, 33);
            simboloPeso1.TabIndex = 33;
            simboloPeso1.Text = "$";
            simboloPeso1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldImpuestoTotal
            // 
            fieldImpuestoTotal.Dock = DockStyle.Fill;
            fieldImpuestoTotal.Font = new Font("Segoe UI", 11.25F);
            fieldImpuestoTotal.ForeColor = Color.Black;
            fieldImpuestoTotal.ImeMode = ImeMode.NoControl;
            fieldImpuestoTotal.Location = new Point(762, 1);
            fieldImpuestoTotal.Margin = new Padding(1);
            fieldImpuestoTotal.Name = "fieldImpuestoTotal";
            fieldImpuestoTotal.Size = new Size(108, 39);
            fieldImpuestoTotal.TabIndex = 34;
            fieldImpuestoTotal.Text = "impuesto";
            fieldImpuestoTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldTotalBruto
            // 
            fieldTotalBruto.Dock = DockStyle.Fill;
            fieldTotalBruto.Font = new Font("Segoe UI", 11.25F);
            fieldTotalBruto.ForeColor = Color.Black;
            fieldTotalBruto.ImeMode = ImeMode.NoControl;
            fieldTotalBruto.Location = new Point(502, 1);
            fieldTotalBruto.Margin = new Padding(1);
            fieldTotalBruto.Name = "fieldTotalBruto";
            fieldTotalBruto.Size = new Size(108, 39);
            fieldTotalBruto.TabIndex = 35;
            fieldTotalBruto.Text = "totalBruto";
            fieldTotalBruto.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldDescuentoTotal
            // 
            fieldDescuentoTotal.Dock = DockStyle.Fill;
            fieldDescuentoTotal.Font = new Font("Segoe UI", 11.25F);
            fieldDescuentoTotal.ForeColor = Color.Black;
            fieldDescuentoTotal.ImeMode = ImeMode.NoControl;
            fieldDescuentoTotal.Location = new Point(632, 1);
            fieldDescuentoTotal.Margin = new Padding(1);
            fieldDescuentoTotal.Name = "fieldDescuentoTotal";
            fieldDescuentoTotal.Size = new Size(108, 39);
            fieldDescuentoTotal.TabIndex = 36;
            fieldDescuentoTotal.Text = "descuento";
            fieldDescuentoTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldFechaVenta
            // 
            fieldFechaVenta.Dock = DockStyle.Fill;
            fieldFechaVenta.Font = new Font("Segoe UI", 11.25F);
            fieldFechaVenta.ForeColor = Color.DimGray;
            fieldFechaVenta.ImeMode = ImeMode.NoControl;
            fieldFechaVenta.Location = new Point(61, 1);
            fieldFechaVenta.Margin = new Padding(1);
            fieldFechaVenta.Name = "fieldFechaVenta";
            fieldFechaVenta.Size = new Size(118, 39);
            fieldFechaVenta.TabIndex = 17;
            fieldFechaVenta.Text = "fecha";
            fieldFechaVenta.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldNombreCliente
            // 
            fieldNombreCliente.AutoEllipsis = true;
            fieldNombreCliente.Dock = DockStyle.Fill;
            fieldNombreCliente.Font = new Font("Segoe UI", 11.25F);
            fieldNombreCliente.ForeColor = Color.DimGray;
            fieldNombreCliente.ImeMode = ImeMode.NoControl;
            fieldNombreCliente.Location = new Point(185, 1);
            fieldNombreCliente.Margin = new Padding(5, 1, 1, 1);
            fieldNombreCliente.Name = "fieldNombreCliente";
            fieldNombreCliente.Size = new Size(165, 39);
            fieldNombreCliente.TabIndex = 4;
            fieldNombreCliente.Text = "nombreCliente";
            fieldNombreCliente.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VistaTuplaVenta
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1241, 42);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaTuplaVenta";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaTuplaVenta";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private Label fieldId;
        private Label fieldFechaVenta;
        private Label fieldNombreCliente;
        private Label fieldImporteTotal;
        private Guna2Button btnVerFactura;
        private Label simboloPeso4;
        private Label simboloPeso3;
        private Label simboloPeso2;
        private Label simboloPeso1;
        private Label fieldImpuestoTotal;
        private Label fieldTotalBruto;
        private Label fieldDescuentoTotal;
        private Label fieldMetodoPagoPrincipal;
        private Label fieldEstado;
        private Guna2Button btnAnular;
    }
}