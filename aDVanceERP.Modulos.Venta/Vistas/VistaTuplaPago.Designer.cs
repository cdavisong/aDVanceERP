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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaTuplaPago));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            layoutBase = new TableLayoutPanel();
            separador1 = new Guna2Separator();
            layoutVista = new TableLayoutPanel();
            fieldCanalPago = new Guna2Button();
            fieldNumeroFactura = new Label();
            fieldId = new Label();
            btnConfirmar = new Guna2Button();
            btnCancelar = new Guna2Button();
            fieldNumeroTransferencia = new Label();
            fieldMonto = new Label();
            fieldFechaConfirmacion = new Label();
            fieldFechaPago = new Label();
            fieldNumeroTelefonoRemitente = new Label();
            fieldEstado = new Guna2Button();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            SuspendLayout();
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
            layoutVista.ColumnCount = 11;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.Controls.Add(fieldEstado, 8, 0);
            layoutVista.Controls.Add(fieldCanalPago, 2, 0);
            layoutVista.Controls.Add(fieldNumeroFactura, 1, 0);
            layoutVista.Controls.Add(fieldId, 0, 0);
            layoutVista.Controls.Add(btnConfirmar, 9, 0);
            layoutVista.Controls.Add(btnCancelar, 10, 0);
            layoutVista.Controls.Add(fieldNumeroTransferencia, 4, 0);
            layoutVista.Controls.Add(fieldMonto, 7, 0);
            layoutVista.Controls.Add(fieldFechaConfirmacion, 6, 0);
            layoutVista.Controls.Add(fieldFechaPago, 5, 0);
            layoutVista.Controls.Add(fieldNumeroTelefonoRemitente, 3, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0, 0, 0, 1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(1241, 36);
            layoutVista.TabIndex = 19;
            // 
            // fieldCanalPago
            // 
            fieldCanalPago.AutoRoundedCorners = true;
            fieldCanalPago.BorderColor = Color.Gainsboro;
            fieldCanalPago.BorderRadius = 11;
            fieldCanalPago.BorderThickness = 1;
            fieldCanalPago.Cursor = Cursors.Hand;
            fieldCanalPago.CustomizableEdges = customizableEdges3;
            fieldCanalPago.DisabledState.BorderColor = Color.Gainsboro;
            fieldCanalPago.DisabledState.CustomBorderColor = Color.Gainsboro;
            fieldCanalPago.DisabledState.FillColor = Color.Gainsboro;
            fieldCanalPago.DisabledState.ForeColor = Color.DimGray;
            fieldCanalPago.Dock = DockStyle.Left;
            fieldCanalPago.Enabled = false;
            fieldCanalPago.FillColor = Color.Gainsboro;
            fieldCanalPago.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldCanalPago.ForeColor = Color.DimGray;
            fieldCanalPago.HoverState.BorderColor = Color.PeachPuff;
            fieldCanalPago.HoverState.FillColor = Color.PeachPuff;
            fieldCanalPago.HoverState.ForeColor = Color.Black;
            fieldCanalPago.Location = new Point(226, 6);
            fieldCanalPago.Margin = new Padding(6);
            fieldCanalPago.Name = "fieldCanalPago";
            fieldCanalPago.ShadowDecoration.CustomizableEdges = customizableEdges4;
            fieldCanalPago.Size = new Size(181, 24);
            fieldCanalPago.TabIndex = 46;
            fieldCanalPago.Text = "Transferencia bancaria";
            fieldCanalPago.TextOffset = new Point(0, -1);
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
            fieldNumeroFactura.Size = new Size(158, 34);
            fieldNumeroFactura.TabIndex = 41;
            fieldNumeroFactura.Text = "facturaVenta";
            fieldNumeroFactura.TextAlign = ContentAlignment.MiddleLeft;
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
            // btnConfirmar
            // 
            btnConfirmar.Animated = true;
            btnConfirmar.AutoRoundedCorners = true;
            btnConfirmar.BorderColor = Color.Gainsboro;
            btnConfirmar.BorderRadius = 14;
            btnConfirmar.BorderThickness = 1;
            btnConfirmar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage");
            btnConfirmar.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnConfirmar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnConfirmar.CustomizableEdges = customizableEdges5;
            btnConfirmar.Dock = DockStyle.Fill;
            btnConfirmar.FillColor = Color.White;
            btnConfirmar.Font = new Font("Segoe UI", 9.75F);
            btnConfirmar.ForeColor = Color.White;
            btnConfirmar.HoverState.BorderColor = Color.PeachPuff;
            btnConfirmar.HoverState.FillColor = Color.PeachPuff;
            btnConfirmar.Location = new Point(1170, 3);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnConfirmar.Size = new Size(31, 30);
            btnConfirmar.TabIndex = 21;
            // 
            // btnCancelar
            // 
            btnCancelar.Animated = true;
            btnCancelar.AutoRoundedCorners = true;
            btnCancelar.BorderColor = Color.Gainsboro;
            btnCancelar.BorderRadius = 14;
            btnCancelar.BorderThickness = 1;
            btnCancelar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage1");
            btnCancelar.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnCancelar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnCancelar.CustomizableEdges = customizableEdges7;
            btnCancelar.Dock = DockStyle.Fill;
            btnCancelar.FillColor = Color.White;
            btnCancelar.Font = new Font("Segoe UI", 9.75F);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.HoverState.BorderColor = Color.PeachPuff;
            btnCancelar.HoverState.FillColor = Color.PeachPuff;
            btnCancelar.HoverState.ForeColor = Color.White;
            btnCancelar.Location = new Point(1207, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnCancelar.Size = new Size(31, 30);
            btnCancelar.TabIndex = 22;
            // 
            // fieldNumeroTransferencia
            // 
            fieldNumeroTransferencia.AutoEllipsis = true;
            fieldNumeroTransferencia.Dock = DockStyle.Fill;
            fieldNumeroTransferencia.Font = new Font("Segoe UI", 11.25F);
            fieldNumeroTransferencia.ForeColor = Color.DimGray;
            fieldNumeroTransferencia.ImeMode = ImeMode.NoControl;
            fieldNumeroTransferencia.Location = new Point(548, 1);
            fieldNumeroTransferencia.Margin = new Padding(1);
            fieldNumeroTransferencia.Name = "fieldNumeroTransferencia";
            fieldNumeroTransferencia.Size = new Size(118, 34);
            fieldNumeroTransferencia.TabIndex = 35;
            fieldNumeroTransferencia.Text = "numeroTransf";
            fieldNumeroTransferencia.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldMonto
            // 
            fieldMonto.Dock = DockStyle.Fill;
            fieldMonto.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldMonto.ForeColor = Color.Black;
            fieldMonto.ImeMode = ImeMode.NoControl;
            fieldMonto.Location = new Point(908, 1);
            fieldMonto.Margin = new Padding(1);
            fieldMonto.Name = "fieldMonto";
            fieldMonto.Size = new Size(128, 34);
            fieldMonto.TabIndex = 20;
            fieldMonto.Text = "monto";
            fieldMonto.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldFechaConfirmacion
            // 
            fieldFechaConfirmacion.AutoEllipsis = true;
            fieldFechaConfirmacion.Dock = DockStyle.Fill;
            fieldFechaConfirmacion.Font = new Font("Segoe UI", 11.25F);
            fieldFechaConfirmacion.ForeColor = Color.DimGray;
            fieldFechaConfirmacion.ImeMode = ImeMode.NoControl;
            fieldFechaConfirmacion.Location = new Point(792, 1);
            fieldFechaConfirmacion.Margin = new Padding(5, 1, 1, 1);
            fieldFechaConfirmacion.Name = "fieldFechaConfirmacion";
            fieldFechaConfirmacion.Size = new Size(114, 34);
            fieldFechaConfirmacion.TabIndex = 37;
            fieldFechaConfirmacion.Text = "fechaConf";
            fieldFechaConfirmacion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldFechaPago
            // 
            fieldFechaPago.Dock = DockStyle.Fill;
            fieldFechaPago.Font = new Font("Segoe UI", 11.25F);
            fieldFechaPago.ForeColor = Color.DimGray;
            fieldFechaPago.ImeMode = ImeMode.NoControl;
            fieldFechaPago.Location = new Point(668, 1);
            fieldFechaPago.Margin = new Padding(1);
            fieldFechaPago.Name = "fieldFechaPago";
            fieldFechaPago.Size = new Size(118, 34);
            fieldFechaPago.TabIndex = 17;
            fieldFechaPago.Text = "fechaPago";
            fieldFechaPago.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldNumeroTelefonoRemitente
            // 
            fieldNumeroTelefonoRemitente.Dock = DockStyle.Fill;
            fieldNumeroTelefonoRemitente.Font = new Font("Segoe UI", 11.25F);
            fieldNumeroTelefonoRemitente.ForeColor = Color.DimGray;
            fieldNumeroTelefonoRemitente.ImeMode = ImeMode.NoControl;
            fieldNumeroTelefonoRemitente.Location = new Point(428, 1);
            fieldNumeroTelefonoRemitente.Margin = new Padding(1);
            fieldNumeroTelefonoRemitente.Name = "fieldNumeroTelefonoRemitente";
            fieldNumeroTelefonoRemitente.Size = new Size(118, 34);
            fieldNumeroTelefonoRemitente.TabIndex = 39;
            fieldNumeroTelefonoRemitente.Text = "numeroRem";
            fieldNumeroTelefonoRemitente.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldEstado
            // 
            fieldEstado.AutoRoundedCorners = true;
            fieldEstado.BorderColor = Color.Gainsboro;
            fieldEstado.BorderRadius = 11;
            fieldEstado.BorderThickness = 1;
            fieldEstado.CustomizableEdges = customizableEdges1;
            fieldEstado.DisabledState.BorderColor = Color.Gainsboro;
            fieldEstado.DisabledState.CustomBorderColor = Color.Gainsboro;
            fieldEstado.DisabledState.FillColor = Color.Gainsboro;
            fieldEstado.DisabledState.ForeColor = Color.DimGray;
            fieldEstado.Dock = DockStyle.Left;
            fieldEstado.Enabled = false;
            fieldEstado.FillColor = Color.Gainsboro;
            fieldEstado.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldEstado.ForeColor = Color.DimGray;
            fieldEstado.HoverState.BorderColor = Color.PeachPuff;
            fieldEstado.HoverState.FillColor = Color.PeachPuff;
            fieldEstado.HoverState.ForeColor = Color.Black;
            fieldEstado.Location = new Point(1043, 6);
            fieldEstado.Margin = new Padding(6);
            fieldEstado.Name = "fieldEstado";
            fieldEstado.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fieldEstado.Size = new Size(108, 24);
            fieldEstado.TabIndex = 47;
            fieldEstado.Text = "Pendiente";
            fieldEstado.TextOffset = new Point(0, -1);
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
        private Label fieldNumeroTransferencia;
        private Label fieldFechaConfirmacion;
        private Label fieldNumeroTelefonoRemitente;
        private Label fieldNumeroFactura;
        private Guna2Separator separador1;
        private Guna2Button fieldCanalPago;
        private Guna2Button fieldEstado;
    }
}