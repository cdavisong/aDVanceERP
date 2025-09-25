using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Venta {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldId = new Label();
            fieldNombreAlmacen = new Label();
            fieldFecha = new Label();
            fieldNombreCliente = new Label();
            fieldCantidadProductos = new Label();
            fieldMontoTotal = new Label();
            btnEditar = new Guna2Button();
            btnEliminar = new Guna2Button();
            symbolPeso = new Label();
            fieldEstadoEntrega = new Label();
            fieldEstadoPago = new Label();
            btnDescargarFactura = new Guna2Button();
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
            layoutVista.ColumnCount = 12;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.Controls.Add(fieldId, 0, 0);
            layoutVista.Controls.Add(fieldNombreAlmacen, 2, 0);
            layoutVista.Controls.Add(fieldFecha, 1, 0);
            layoutVista.Controls.Add(fieldNombreCliente, 3, 0);
            layoutVista.Controls.Add(fieldCantidadProductos, 4, 0);
            layoutVista.Controls.Add(fieldMontoTotal, 5, 0);
            layoutVista.Controls.Add(btnEditar, 10, 0);
            layoutVista.Controls.Add(btnEliminar, 11, 0);
            layoutVista.Controls.Add(symbolPeso, 6, 0);
            layoutVista.Controls.Add(fieldEstadoEntrega, 7, 0);
            layoutVista.Controls.Add(fieldEstadoPago, 8, 0);
            layoutVista.Controls.Add(btnDescargarFactura, 9, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0, 0, 0, 1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(1241, 41);
            layoutVista.TabIndex = 19;
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
            // fieldNombreAlmacen
            // 
            fieldNombreAlmacen.AutoEllipsis = true;
            fieldNombreAlmacen.Dock = DockStyle.Fill;
            fieldNombreAlmacen.Font = new Font("Segoe UI", 11.25F);
            fieldNombreAlmacen.ForeColor = Color.DimGray;
            fieldNombreAlmacen.ImeMode = ImeMode.NoControl;
            fieldNombreAlmacen.Location = new Point(181, 1);
            fieldNombreAlmacen.Margin = new Padding(1);
            fieldNombreAlmacen.Name = "fieldNombreAlmacen";
            fieldNombreAlmacen.Size = new Size(118, 39);
            fieldNombreAlmacen.TabIndex = 19;
            fieldNombreAlmacen.Text = "nombreAlmacenOrigen";
            fieldNombreAlmacen.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldFecha
            // 
            fieldFecha.Dock = DockStyle.Fill;
            fieldFecha.Font = new Font("Segoe UI", 11.25F);
            fieldFecha.ForeColor = Color.DimGray;
            fieldFecha.ImeMode = ImeMode.NoControl;
            fieldFecha.Location = new Point(61, 1);
            fieldFecha.Margin = new Padding(1);
            fieldFecha.Name = "fieldFecha";
            fieldFecha.Size = new Size(118, 39);
            fieldFecha.TabIndex = 17;
            fieldFecha.Text = "fecha";
            fieldFecha.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldNombreCliente
            // 
            fieldNombreCliente.AutoEllipsis = true;
            fieldNombreCliente.Dock = DockStyle.Fill;
            fieldNombreCliente.Font = new Font("Segoe UI", 11.25F);
            fieldNombreCliente.ForeColor = Color.DimGray;
            fieldNombreCliente.ImeMode = ImeMode.NoControl;
            fieldNombreCliente.Location = new Point(305, 1);
            fieldNombreCliente.Margin = new Padding(5, 1, 1, 1);
            fieldNombreCliente.Name = "fieldNombreCliente";
            fieldNombreCliente.Size = new Size(335, 39);
            fieldNombreCliente.TabIndex = 4;
            fieldNombreCliente.Text = "nombreCliente";
            fieldNombreCliente.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldCantidadProductos
            // 
            fieldCantidadProductos.AutoEllipsis = true;
            fieldCantidadProductos.Dock = DockStyle.Fill;
            fieldCantidadProductos.Font = new Font("Segoe UI", 11.25F);
            fieldCantidadProductos.ForeColor = Color.DimGray;
            fieldCantidadProductos.ImeMode = ImeMode.NoControl;
            fieldCantidadProductos.Location = new Point(642, 1);
            fieldCantidadProductos.Margin = new Padding(1);
            fieldCantidadProductos.Name = "fieldCantidadProductos";
            fieldCantidadProductos.Size = new Size(108, 39);
            fieldCantidadProductos.TabIndex = 6;
            fieldCantidadProductos.Text = "cantidad";
            fieldCantidadProductos.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldMontoTotal
            // 
            fieldMontoTotal.Dock = DockStyle.Fill;
            fieldMontoTotal.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldMontoTotal.ForeColor = Color.Black;
            fieldMontoTotal.ImeMode = ImeMode.NoControl;
            fieldMontoTotal.Location = new Point(752, 1);
            fieldMontoTotal.Margin = new Padding(1);
            fieldMontoTotal.Name = "fieldMontoTotal";
            fieldMontoTotal.Size = new Size(108, 39);
            fieldMontoTotal.TabIndex = 20;
            fieldMontoTotal.Text = "total";
            fieldMontoTotal.TextAlign = ContentAlignment.MiddleRight;
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
            btnEditar.CustomizableEdges = customizableEdges1;
            btnEditar.Dock = DockStyle.Fill;
            btnEditar.FillColor = Color.White;
            btnEditar.Font = new Font("Segoe UI", 9.75F);
            btnEditar.ForeColor = Color.White;
            btnEditar.HoverState.BorderColor = Color.PeachPuff;
            btnEditar.HoverState.FillColor = Color.PeachPuff;
            btnEditar.Location = new Point(1164, 3);
            btnEditar.Name = "btnEditar";
            btnEditar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnEditar.Size = new Size(34, 35);
            btnEditar.TabIndex = 21;
            // 
            // btnEliminar
            // 
            btnEliminar.Animated = true;
            btnEliminar.BorderColor = Color.Gainsboro;
            btnEliminar.BorderRadius = 16;
            btnEliminar.BorderThickness = 1;
            btnEliminar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage1");
            btnEliminar.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnEliminar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnEliminar.CustomizableEdges = customizableEdges3;
            btnEliminar.Dock = DockStyle.Fill;
            btnEliminar.Enabled = false;
            btnEliminar.FillColor = Color.White;
            btnEliminar.Font = new Font("Segoe UI", 9.75F);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.HoverState.BorderColor = Color.PeachPuff;
            btnEliminar.HoverState.FillColor = Color.PeachPuff;
            btnEliminar.HoverState.ForeColor = Color.White;
            btnEliminar.Location = new Point(1204, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnEliminar.Size = new Size(34, 35);
            btnEliminar.TabIndex = 22;
            // 
            // symbolPeso
            // 
            symbolPeso.Dock = DockStyle.Fill;
            symbolPeso.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            symbolPeso.ForeColor = Color.Black;
            symbolPeso.ImageAlign = ContentAlignment.MiddleLeft;
            symbolPeso.ImeMode = ImeMode.NoControl;
            symbolPeso.Location = new Point(864, 5);
            symbolPeso.Margin = new Padding(3, 5, 3, 3);
            symbolPeso.Name = "symbolPeso";
            symbolPeso.Size = new Size(14, 33);
            symbolPeso.TabIndex = 30;
            symbolPeso.Text = "$";
            symbolPeso.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldEstadoEntrega
            // 
            fieldEstadoEntrega.Dock = DockStyle.Fill;
            fieldEstadoEntrega.Font = new Font("Segoe UI", 11.25F);
            fieldEstadoEntrega.ForeColor = Color.DimGray;
            fieldEstadoEntrega.ImeMode = ImeMode.NoControl;
            fieldEstadoEntrega.Location = new Point(886, 1);
            fieldEstadoEntrega.Margin = new Padding(5, 1, 1, 1);
            fieldEstadoEntrega.Name = "fieldEstadoEntrega";
            fieldEstadoEntrega.Size = new Size(114, 39);
            fieldEstadoEntrega.TabIndex = 31;
            fieldEstadoEntrega.Text = "estadoEntrega";
            fieldEstadoEntrega.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldEstadoPago
            // 
            fieldEstadoPago.Dock = DockStyle.Fill;
            fieldEstadoPago.Font = new Font("Segoe UI", 11.25F);
            fieldEstadoPago.ForeColor = Color.DimGray;
            fieldEstadoPago.ImeMode = ImeMode.NoControl;
            fieldEstadoPago.Location = new Point(1006, 1);
            fieldEstadoPago.Margin = new Padding(5, 1, 1, 1);
            fieldEstadoPago.Name = "fieldEstadoPago";
            fieldEstadoPago.Size = new Size(114, 39);
            fieldEstadoPago.TabIndex = 32;
            fieldEstadoPago.Text = "estadoPago";
            fieldEstadoPago.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnDescargarFactura
            // 
            btnDescargarFactura.Animated = true;
            btnDescargarFactura.BorderColor = Color.Gainsboro;
            btnDescargarFactura.BorderRadius = 16;
            btnDescargarFactura.BorderThickness = 1;
            btnDescargarFactura.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage2");
            btnDescargarFactura.CustomImages.Image = (Image) resources.GetObject("resource.Image2");
            btnDescargarFactura.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnDescargarFactura.CustomizableEdges = customizableEdges5;
            btnDescargarFactura.Dock = DockStyle.Fill;
            btnDescargarFactura.FillColor = Color.White;
            btnDescargarFactura.Font = new Font("Segoe UI", 9.75F);
            btnDescargarFactura.ForeColor = Color.White;
            btnDescargarFactura.HoverState.BorderColor = Color.PeachPuff;
            btnDescargarFactura.HoverState.FillColor = Color.PeachPuff;
            btnDescargarFactura.Location = new Point(1124, 3);
            btnDescargarFactura.Name = "btnDescargarFactura";
            btnDescargarFactura.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnDescargarFactura.Size = new Size(34, 35);
            btnDescargarFactura.TabIndex = 33;
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
        private Label fieldNombreAlmacen;
        private Label fieldFecha;
        private Label fieldNombreCliente;
        private Label fieldCantidadProductos;
        private Label fieldMontoTotal;
        private Guna2Button btnEditar;
        private Guna2Button btnEliminar;
        private Label symbolPeso;
        private Label fieldEstadoEntrega;
        private Label fieldEstadoPago;
        private Guna2Button btnDescargarFactura;
    }
}