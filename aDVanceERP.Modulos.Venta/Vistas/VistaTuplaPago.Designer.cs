using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Venta.Vistas {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaTuplaPago));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldEstado = new Label();
            fieldFechaConfirmacion = new Label();
            fieldId = new Label();
            btnConfirmar = new Guna2Button();
            btnCancelar = new Guna2Button();
            simboloPeso4 = new Label();
            fieldMonto = new Label();
            fieldNumeroTransferencia = new Label();
            fieldFechaPago = new Label();
            fieldNumeroConfirmacion = new Label();
            fieldMetodoPago = new Label();
            fieldNumeroFactura = new Label();
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
            layoutVista.ColumnCount = 13;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.Controls.Add(fieldNumeroFactura, 1, 0);
            layoutVista.Controls.Add(fieldId, 0, 0);
            layoutVista.Controls.Add(btnConfirmar, 11, 0);
            layoutVista.Controls.Add(btnCancelar, 12, 0);
            layoutVista.Controls.Add(fieldNumeroTransferencia, 4, 0);
            layoutVista.Controls.Add(fieldEstado, 9, 0);
            layoutVista.Controls.Add(simboloPeso4, 8, 0);
            layoutVista.Controls.Add(fieldMonto, 7, 0);
            layoutVista.Controls.Add(fieldFechaConfirmacion, 6, 0);
            layoutVista.Controls.Add(fieldFechaPago, 5, 0);
            layoutVista.Controls.Add(fieldNumeroConfirmacion, 3, 0);
            layoutVista.Controls.Add(fieldMetodoPago, 2, 0);
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
            fieldEstado.Font = new Font("Segoe UI", 11.25F);
            fieldEstado.ForeColor = Color.DimGray;
            fieldEstado.ImeMode = ImeMode.NoControl;
            fieldEstado.Location = new Point(992, 1);
            fieldEstado.Margin = new Padding(1);
            fieldEstado.Name = "fieldEstado";
            fieldEstado.Size = new Size(128, 39);
            fieldEstado.TabIndex = 38;
            fieldEstado.Text = "estado";
            fieldEstado.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldFechaConfirmacion
            // 
            fieldFechaConfirmacion.AutoEllipsis = true;
            fieldFechaConfirmacion.Dock = DockStyle.Fill;
            fieldFechaConfirmacion.Font = new Font("Segoe UI", 11.25F);
            fieldFechaConfirmacion.ForeColor = Color.DimGray;
            fieldFechaConfirmacion.ImeMode = ImeMode.NoControl;
            fieldFechaConfirmacion.Location = new Point(746, 1);
            fieldFechaConfirmacion.Margin = new Padding(5, 1, 1, 1);
            fieldFechaConfirmacion.Name = "fieldFechaConfirmacion";
            fieldFechaConfirmacion.Size = new Size(114, 39);
            fieldFechaConfirmacion.TabIndex = 37;
            fieldFechaConfirmacion.Text = "fechaConf";
            fieldFechaConfirmacion.TextAlign = ContentAlignment.MiddleCenter;
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
            // btnConfirmar
            // 
            btnConfirmar.Animated = true;
            btnConfirmar.BorderColor = Color.Gainsboro;
            btnConfirmar.BorderRadius = 16;
            btnConfirmar.BorderThickness = 1;
            btnConfirmar.CustomImages.HoveredImage = (Image)resources.GetObject("resource.HoveredImage");
            btnConfirmar.CustomImages.Image = (Image)resources.GetObject("resource.Image");
            btnConfirmar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnConfirmar.CustomizableEdges = customizableEdges1;
            btnConfirmar.Dock = DockStyle.Fill;
            btnConfirmar.FillColor = Color.White;
            btnConfirmar.Font = new Font("Segoe UI", 9.75F);
            btnConfirmar.ForeColor = Color.White;
            btnConfirmar.HoverState.BorderColor = Color.PeachPuff;
            btnConfirmar.HoverState.FillColor = Color.PeachPuff;
            btnConfirmar.Location = new Point(1164, 3);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnConfirmar.Size = new Size(34, 35);
            btnConfirmar.TabIndex = 21;
            // 
            // btnCancelar
            // 
            btnCancelar.Animated = true;
            btnCancelar.BorderColor = Color.Gainsboro;
            btnCancelar.BorderRadius = 16;
            btnCancelar.BorderThickness = 1;
            btnCancelar.CustomImages.HoveredImage = (Image)resources.GetObject("resource.HoveredImage1");
            btnCancelar.CustomImages.Image = (Image)resources.GetObject("resource.Image1");
            btnCancelar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnCancelar.CustomizableEdges = customizableEdges3;
            btnCancelar.Dock = DockStyle.Fill;
            btnCancelar.FillColor = Color.White;
            btnCancelar.Font = new Font("Segoe UI", 9.75F);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.HoverState.BorderColor = Color.PeachPuff;
            btnCancelar.HoverState.FillColor = Color.PeachPuff;
            btnCancelar.HoverState.ForeColor = Color.White;
            btnCancelar.Location = new Point(1204, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnCancelar.Size = new Size(34, 35);
            btnCancelar.TabIndex = 22;
            // 
            // simboloPeso4
            // 
            simboloPeso4.Dock = DockStyle.Fill;
            simboloPeso4.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            simboloPeso4.ForeColor = Color.Black;
            simboloPeso4.ImageAlign = ContentAlignment.MiddleLeft;
            simboloPeso4.ImeMode = ImeMode.NoControl;
            simboloPeso4.Location = new Point(974, 5);
            simboloPeso4.Margin = new Padding(3, 5, 3, 3);
            simboloPeso4.Name = "simboloPeso4";
            simboloPeso4.Size = new Size(14, 33);
            simboloPeso4.TabIndex = 30;
            simboloPeso4.Text = "$";
            simboloPeso4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldMonto
            // 
            fieldMonto.Dock = DockStyle.Fill;
            fieldMonto.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldMonto.ForeColor = Color.Black;
            fieldMonto.ImeMode = ImeMode.NoControl;
            fieldMonto.Location = new Point(862, 1);
            fieldMonto.Margin = new Padding(1);
            fieldMonto.Name = "fieldMonto";
            fieldMonto.Size = new Size(108, 39);
            fieldMonto.TabIndex = 20;
            fieldMonto.Text = "monto";
            fieldMonto.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldNumeroTransferencia
            // 
            fieldNumeroTransferencia.Dock = DockStyle.Fill;
            fieldNumeroTransferencia.Font = new Font("Segoe UI", 11.25F);
            fieldNumeroTransferencia.ForeColor = Color.DimGray;
            fieldNumeroTransferencia.ImeMode = ImeMode.NoControl;
            fieldNumeroTransferencia.Location = new Point(502, 1);
            fieldNumeroTransferencia.Margin = new Padding(1);
            fieldNumeroTransferencia.Name = "fieldNumeroTransferencia";
            fieldNumeroTransferencia.Size = new Size(118, 39);
            fieldNumeroTransferencia.TabIndex = 35;
            fieldNumeroTransferencia.Text = "numeroTransf";
            fieldNumeroTransferencia.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldFechaPago
            // 
            fieldFechaPago.Dock = DockStyle.Fill;
            fieldFechaPago.Font = new Font("Segoe UI", 11.25F);
            fieldFechaPago.ForeColor = Color.DimGray;
            fieldFechaPago.ImeMode = ImeMode.NoControl;
            fieldFechaPago.Location = new Point(622, 1);
            fieldFechaPago.Margin = new Padding(1);
            fieldFechaPago.Name = "fieldFechaPago";
            fieldFechaPago.Size = new Size(118, 39);
            fieldFechaPago.TabIndex = 17;
            fieldFechaPago.Text = "fechaPago";
            fieldFechaPago.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldNumeroConfirmacion
            // 
            fieldNumeroConfirmacion.Dock = DockStyle.Fill;
            fieldNumeroConfirmacion.Font = new Font("Segoe UI", 11.25F);
            fieldNumeroConfirmacion.ForeColor = Color.DimGray;
            fieldNumeroConfirmacion.ImeMode = ImeMode.NoControl;
            fieldNumeroConfirmacion.Location = new Point(382, 1);
            fieldNumeroConfirmacion.Margin = new Padding(1);
            fieldNumeroConfirmacion.Name = "fieldNumeroConfirmacion";
            fieldNumeroConfirmacion.Size = new Size(118, 39);
            fieldNumeroConfirmacion.TabIndex = 39;
            fieldNumeroConfirmacion.Text = "numeroConf";
            fieldNumeroConfirmacion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldMetodoPago
            // 
            fieldMetodoPago.Dock = DockStyle.Fill;
            fieldMetodoPago.Font = new Font("Segoe UI", 11.25F);
            fieldMetodoPago.ForeColor = Color.DimGray;
            fieldMetodoPago.ImeMode = ImeMode.NoControl;
            fieldMetodoPago.Location = new Point(221, 1);
            fieldMetodoPago.Margin = new Padding(1);
            fieldMetodoPago.Name = "fieldMetodoPago";
            fieldMetodoPago.Size = new Size(159, 39);
            fieldMetodoPago.TabIndex = 40;
            fieldMetodoPago.Text = "metodoPago";
            fieldMetodoPago.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldNumeroFactura
            // 
            fieldNumeroFactura.Dock = DockStyle.Fill;
            fieldNumeroFactura.Font = new Font("Segoe UI", 11.25F);
            fieldNumeroFactura.ForeColor = Color.DimGray;
            fieldNumeroFactura.ImeMode = ImeMode.NoControl;
            fieldNumeroFactura.Location = new Point(61, 1);
            fieldNumeroFactura.Margin = new Padding(1);
            fieldNumeroFactura.Name = "fieldNumeroFactura";
            fieldNumeroFactura.Size = new Size(158, 39);
            fieldNumeroFactura.TabIndex = 41;
            fieldNumeroFactura.Text = "facturaVenta";
            fieldNumeroFactura.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // VistaTuplaPago
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1241, 42);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaTuplaPago";
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
        private Label fieldFechaPago;
        private Label fieldMonto;
        private Guna2Button btnConfirmar;
        private Guna2Button btnCancelar;
        private Label simboloPeso4;
        private Label fieldNumeroTransferencia;
        private Label fieldFechaConfirmacion;
        private Label fieldEstado;
        private Label fieldNumeroConfirmacion;
        private Label fieldNumeroFactura;
        private Label fieldMetodoPago;
    }
}