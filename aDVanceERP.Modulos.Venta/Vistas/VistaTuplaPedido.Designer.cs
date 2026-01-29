using Guna.UI2.WinForms;

using System.ComponentModel;

namespace DVanceERP.Modulos.Venta.Vistas {
    partial class VistaTuplaPedido {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaTuplaPedido));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldEstado = new Label();
            fieldFechaEntrega = new Label();
            fieldId = new Label();
            btnEditar = new Guna2Button();
            btnEliminar = new Guna2Button();
            simboloPeso4 = new Label();
            fieldImporteTotal = new Label();
            fieldFechaPedido = new Label();
            fieldNombreCliente = new Label();
            fieldDireccionaEntrega = new Label();
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
            layoutVista.ColumnCount = 11;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.Controls.Add(fieldEstado, 7, 0);
            layoutVista.Controls.Add(fieldFechaEntrega, 3, 0);
            layoutVista.Controls.Add(fieldId, 0, 0);
            layoutVista.Controls.Add(btnEditar, 9, 0);
            layoutVista.Controls.Add(btnEliminar, 10, 0);
            layoutVista.Controls.Add(simboloPeso4, 6, 0);
            layoutVista.Controls.Add(fieldImporteTotal, 5, 0);
            layoutVista.Controls.Add(fieldFechaPedido, 1, 0);
            layoutVista.Controls.Add(fieldNombreCliente, 1, 0);
            layoutVista.Controls.Add(fieldDireccionaEntrega, 4, 0);
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
            fieldEstado.Location = new Point(1001, 1);
            fieldEstado.Margin = new Padding(1);
            fieldEstado.Name = "fieldEstado";
            fieldEstado.Size = new Size(118, 39);
            fieldEstado.TabIndex = 38;
            fieldEstado.Text = "estado";
            fieldEstado.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldFechaEntrega
            // 
            fieldFechaEntrega.AutoEllipsis = true;
            fieldFechaEntrega.Dock = DockStyle.Fill;
            fieldFechaEntrega.Font = new Font("Segoe UI", 11.25F);
            fieldFechaEntrega.ForeColor = Color.DimGray;
            fieldFechaEntrega.ImeMode = ImeMode.NoControl;
            fieldFechaEntrega.Location = new Point(413, 1);
            fieldFechaEntrega.Margin = new Padding(5, 1, 1, 1);
            fieldFechaEntrega.Name = "fieldFechaEntrega";
            fieldFechaEntrega.Size = new Size(114, 39);
            fieldFechaEntrega.TabIndex = 37;
            fieldFechaEntrega.Text = "fechaEntrega";
            fieldFechaEntrega.TextAlign = ContentAlignment.MiddleCenter;
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
            btnEditar.CustomizableEdges = customizableEdges5;
            btnEditar.Dock = DockStyle.Fill;
            btnEditar.FillColor = Color.White;
            btnEditar.Font = new Font("Segoe UI", 9.75F);
            btnEditar.ForeColor = Color.White;
            btnEditar.HoverState.BorderColor = Color.PeachPuff;
            btnEditar.HoverState.FillColor = Color.PeachPuff;
            btnEditar.Location = new Point(1163, 3);
            btnEditar.Name = "btnEditar";
            btnEditar.ShadowDecoration.CustomizableEdges = customizableEdges6;
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
            btnEliminar.CustomizableEdges = customizableEdges7;
            btnEliminar.Dock = DockStyle.Fill;
            btnEliminar.Enabled = false;
            btnEliminar.FillColor = Color.White;
            btnEliminar.Font = new Font("Segoe UI", 9.75F);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.HoverState.BorderColor = Color.PeachPuff;
            btnEliminar.HoverState.FillColor = Color.PeachPuff;
            btnEliminar.HoverState.ForeColor = Color.White;
            btnEliminar.Location = new Point(1203, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnEliminar.Size = new Size(35, 35);
            btnEliminar.TabIndex = 22;
            // 
            // simboloPeso4
            // 
            simboloPeso4.Dock = DockStyle.Fill;
            simboloPeso4.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            simboloPeso4.ForeColor = Color.Black;
            simboloPeso4.ImageAlign = ContentAlignment.MiddleLeft;
            simboloPeso4.ImeMode = ImeMode.NoControl;
            simboloPeso4.Location = new Point(983, 5);
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
            fieldImporteTotal.Location = new Point(871, 1);
            fieldImporteTotal.Margin = new Padding(1);
            fieldImporteTotal.Name = "fieldImporteTotal";
            fieldImporteTotal.Size = new Size(108, 39);
            fieldImporteTotal.TabIndex = 20;
            fieldImporteTotal.Text = "importeTotal";
            fieldImporteTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldFechaPedido
            // 
            fieldFechaPedido.Dock = DockStyle.Fill;
            fieldFechaPedido.Font = new Font("Segoe UI", 11.25F);
            fieldFechaPedido.ForeColor = Color.DimGray;
            fieldFechaPedido.ImeMode = ImeMode.NoControl;
            fieldFechaPedido.Location = new Point(61, 1);
            fieldFechaPedido.Margin = new Padding(1);
            fieldFechaPedido.Name = "fieldFechaPedido";
            fieldFechaPedido.Size = new Size(118, 39);
            fieldFechaPedido.TabIndex = 17;
            fieldFechaPedido.Text = "fecha";
            fieldFechaPedido.TextAlign = ContentAlignment.MiddleCenter;
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
            fieldNombreCliente.Size = new Size(222, 39);
            fieldNombreCliente.TabIndex = 4;
            fieldNombreCliente.Text = "nombreCliente";
            fieldNombreCliente.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldDireccionaEntrega
            // 
            fieldDireccionaEntrega.AutoEllipsis = true;
            fieldDireccionaEntrega.Dock = DockStyle.Fill;
            fieldDireccionaEntrega.Font = new Font("Segoe UI", 11.25F);
            fieldDireccionaEntrega.ForeColor = Color.Black;
            fieldDireccionaEntrega.ImeMode = ImeMode.NoControl;
            fieldDireccionaEntrega.Location = new Point(529, 1);
            fieldDireccionaEntrega.Margin = new Padding(1);
            fieldDireccionaEntrega.Name = "fieldDireccionaEntrega";
            fieldDireccionaEntrega.Size = new Size(340, 39);
            fieldDireccionaEntrega.TabIndex = 35;
            fieldDireccionaEntrega.Text = "direccionEntrega";
            fieldDireccionaEntrega.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VistaTuplaPedido
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1241, 42);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaTuplaPedido";
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
        private Label fieldFechaPedido;
        private Label fieldNombreCliente;
        private Label fieldImporteTotal;
        private Guna2Button btnEditar;
        private Guna2Button btnEliminar;
        private Label simboloPeso4;
        private Label fieldDireccionaEntrega;
        private Label fieldFechaEntrega;
        private Label fieldEstado;
    }
}