using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Pago {
    partial class VistaTuplaPago {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaTuplaPago));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            simboloPeso = new Label();
            fieldMonto = new Label();
            btnEliminar = new Guna2Button();
            fieldMetodoPago = new Label();
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
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 1;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutBase.Size = new Size(417, 42);
            layoutBase.TabIndex = 1;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.White;
            layoutVista.ColumnCount = 4;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.Controls.Add(simboloPeso, 0, 0);
            layoutVista.Controls.Add(fieldMonto, 0, 0);
            layoutVista.Controls.Add(btnEliminar, 3, 0);
            layoutVista.Controls.Add(fieldMetodoPago, 0, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0, 0, 0, 1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(417, 42);
            layoutVista.TabIndex = 2;
            // 
            // simboloPeso
            // 
            simboloPeso.Dock = DockStyle.Fill;
            simboloPeso.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            simboloPeso.ForeColor = Color.Black;
            simboloPeso.ImeMode = ImeMode.NoControl;
            simboloPeso.Location = new Point(358, 1);
            simboloPeso.Margin = new Padding(1);
            simboloPeso.Name = "simboloPeso";
            simboloPeso.Size = new Size(18, 40);
            simboloPeso.TabIndex = 3;
            simboloPeso.Text = "$";
            simboloPeso.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldMonto
            // 
            fieldMonto.Dock = DockStyle.Fill;
            fieldMonto.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldMonto.ForeColor = Color.Black;
            fieldMonto.ImeMode = ImeMode.NoControl;
            fieldMonto.Location = new Point(248, 1);
            fieldMonto.Margin = new Padding(1);
            fieldMonto.Name = "fieldMonto";
            fieldMonto.Size = new Size(108, 40);
            fieldMonto.TabIndex = 2;
            fieldMonto.Text = "monto";
            fieldMonto.TextAlign = ContentAlignment.MiddleRight;
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
            btnEliminar.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.HoverState.BorderColor = Color.PeachPuff;
            btnEliminar.HoverState.FillColor = Color.PeachPuff;
            btnEliminar.HoverState.ForeColor = Color.White;
            btnEliminar.Location = new Point(380, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnEliminar.Size = new Size(34, 36);
            btnEliminar.TabIndex = 0;
            // 
            // fieldMetodoPago
            // 
            fieldMetodoPago.Dock = DockStyle.Fill;
            fieldMetodoPago.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldMetodoPago.ForeColor = Color.DimGray;
            fieldMetodoPago.ImeMode = ImeMode.NoControl;
            fieldMetodoPago.Location = new Point(1, 1);
            fieldMetodoPago.Margin = new Padding(1);
            fieldMetodoPago.Name = "fieldMetodoPago";
            fieldMetodoPago.Size = new Size(245, 40);
            fieldMetodoPago.TabIndex = 1;
            fieldMetodoPago.Text = "metodoPago";
            fieldMetodoPago.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VistaTuplaPago
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(417, 42);
            Controls.Add(layoutVista);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaTuplaPago";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaTuplaPago";
            layoutVista.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private Label simboloPeso;
        private Label fieldMonto;
        private Guna2Button btnEliminar;
        private Label fieldMetodoPago;
    }
}