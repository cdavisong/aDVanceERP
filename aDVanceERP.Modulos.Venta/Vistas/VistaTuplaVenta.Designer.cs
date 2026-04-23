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
            components = new Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaTuplaVenta));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            layoutBase = new TableLayoutPanel();
            separador1 = new Guna2Separator();
            layoutVista = new TableLayoutPanel();
            fieldEstado = new Guna2Button();
            fieldCanalPagoPrincipal = new Guna2Button();
            fieldId = new Label();
            fieldNumeroFactura = new Label();
            btnVerFactura = new Guna2Button();
            menuFormatoDocumento = new ContextMenuStrip(components);
            btnExportarPdf = new ToolStripMenuItem();
            btnExportarXlsx = new ToolStripMenuItem();
            btnAnular = new Guna2Button();
            fieldImporteTotal = new Label();
            fieldImpuestoTotal = new Label();
            fieldTotalBruto = new Label();
            fieldDescuentoTotal = new Label();
            fieldFechaVenta = new Label();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            menuFormatoDocumento.SuspendLayout();
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
            separador1.TabIndex = 75;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.White;
            layoutVista.ColumnCount = 12;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            layoutVista.Controls.Add(fieldEstado, 8, 0);
            layoutVista.Controls.Add(fieldCanalPagoPrincipal, 3, 0);
            layoutVista.Controls.Add(fieldId, 0, 0);
            layoutVista.Controls.Add(fieldNumeroFactura, 1, 0);
            layoutVista.Controls.Add(btnVerFactura, 10, 0);
            layoutVista.Controls.Add(btnAnular, 11, 0);
            layoutVista.Controls.Add(fieldImporteTotal, 7, 0);
            layoutVista.Controls.Add(fieldImpuestoTotal, 6, 0);
            layoutVista.Controls.Add(fieldTotalBruto, 4, 0);
            layoutVista.Controls.Add(fieldDescuentoTotal, 5, 0);
            layoutVista.Controls.Add(fieldFechaVenta, 2, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0, 0, 0, 1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(1241, 36);
            layoutVista.TabIndex = 19;
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
            fieldEstado.Location = new Point(1016, 6);
            fieldEstado.Margin = new Padding(6);
            fieldEstado.Name = "fieldEstado";
            fieldEstado.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fieldEstado.Size = new Size(108, 24);
            fieldEstado.TabIndex = 46;
            fieldEstado.Text = "Pendiente";
            fieldEstado.TextOffset = new Point(0, -1);
            // 
            // fieldMetodoPagoPrincipal
            // 
            fieldCanalPagoPrincipal.AutoRoundedCorners = true;
            fieldCanalPagoPrincipal.BorderColor = Color.Gainsboro;
            fieldCanalPagoPrincipal.BorderRadius = 11;
            fieldCanalPagoPrincipal.BorderThickness = 1;
            fieldCanalPagoPrincipal.Cursor = Cursors.Hand;
            fieldCanalPagoPrincipal.CustomizableEdges = customizableEdges3;
            fieldCanalPagoPrincipal.DisabledState.BorderColor = Color.Gainsboro;
            fieldCanalPagoPrincipal.DisabledState.CustomBorderColor = Color.Gainsboro;
            fieldCanalPagoPrincipal.DisabledState.FillColor = Color.Gainsboro;
            fieldCanalPagoPrincipal.DisabledState.ForeColor = Color.DimGray;
            fieldCanalPagoPrincipal.Dock = DockStyle.Left;
            fieldCanalPagoPrincipal.FillColor = Color.Gainsboro;
            fieldCanalPagoPrincipal.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldCanalPagoPrincipal.ForeColor = Color.DimGray;
            fieldCanalPagoPrincipal.HoverState.BorderColor = Color.PeachPuff;
            fieldCanalPagoPrincipal.HoverState.FillColor = Color.PeachPuff;
            fieldCanalPagoPrincipal.HoverState.ForeColor = Color.Black;
            fieldCanalPagoPrincipal.Location = new Point(346, 6);
            fieldCanalPagoPrincipal.Margin = new Padding(6);
            fieldCanalPagoPrincipal.Name = "fieldMetodoPagoPrincipal";
            fieldCanalPagoPrincipal.ShadowDecoration.CustomizableEdges = customizableEdges4;
            fieldCanalPagoPrincipal.Size = new Size(181, 24);
            fieldCanalPagoPrincipal.TabIndex = 45;
            fieldCanalPagoPrincipal.Text = "Transferencia bancaria";
            fieldCanalPagoPrincipal.TextOffset = new Point(0, -1);
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
            fieldId.TabIndex = 39;
            fieldId.Text = "id";
            fieldId.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldNumeroFactura
            // 
            fieldNumeroFactura.Dock = DockStyle.Fill;
            fieldNumeroFactura.Font = new Font("Segoe UI", 11.25F);
            fieldNumeroFactura.ForeColor = Color.Black;
            fieldNumeroFactura.ImeMode = ImeMode.NoControl;
            fieldNumeroFactura.Location = new Point(61, 1);
            fieldNumeroFactura.Margin = new Padding(1);
            fieldNumeroFactura.Name = "fieldNumeroFactura";
            fieldNumeroFactura.Size = new Size(158, 34);
            fieldNumeroFactura.TabIndex = 13;
            fieldNumeroFactura.Text = "numeroFactura";
            fieldNumeroFactura.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnVerFactura
            // 
            btnVerFactura.Animated = true;
            btnVerFactura.BorderColor = Color.Gainsboro;
            btnVerFactura.BorderRadius = 16;
            btnVerFactura.BorderThickness = 1;
            btnVerFactura.ContextMenuStrip = menuFormatoDocumento;
            btnVerFactura.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage");
            btnVerFactura.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnVerFactura.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnVerFactura.CustomizableEdges = customizableEdges5;
            btnVerFactura.Dock = DockStyle.Fill;
            btnVerFactura.FillColor = Color.White;
            btnVerFactura.Font = new Font("Segoe UI", 9.75F);
            btnVerFactura.ForeColor = Color.White;
            btnVerFactura.HoverState.BorderColor = Color.PeachPuff;
            btnVerFactura.HoverState.FillColor = Color.PeachPuff;
            btnVerFactura.Location = new Point(1170, 3);
            btnVerFactura.Name = "btnVerFactura";
            btnVerFactura.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnVerFactura.Size = new Size(31, 30);
            btnVerFactura.TabIndex = 21;
            btnVerFactura.Visible = false;
            // 
            // menuFormatoDocumento
            // 
            menuFormatoDocumento.BackColor = Color.White;
            menuFormatoDocumento.Items.AddRange(new ToolStripItem[] { btnExportarPdf, btnExportarXlsx });
            menuFormatoDocumento.Name = "menuGastoIndirecto";
            menuFormatoDocumento.Size = new Size(114, 56);
            // 
            // btnExportarPdf
            // 
            btnExportarPdf.BackColor = Color.White;
            btnExportarPdf.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point,  0);
            btnExportarPdf.Image = (Image) resources.GetObject("btnExportarPdf.Image");
            btnExportarPdf.ImageAlign = ContentAlignment.MiddleLeft;
            btnExportarPdf.ImageScaling = ToolStripItemImageScaling.None;
            btnExportarPdf.Name = "btnExportarPdf";
            btnExportarPdf.Size = new Size(113, 26);
            btnExportarPdf.Text = "PDF";
            btnExportarPdf.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnExportarXlsx
            // 
            btnExportarXlsx.BackColor = Color.White;
            btnExportarXlsx.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point,  0);
            btnExportarXlsx.Image = (Image) resources.GetObject("btnExportarXlsx.Image");
            btnExportarXlsx.ImageAlign = ContentAlignment.MiddleLeft;
            btnExportarXlsx.ImageScaling = ToolStripItemImageScaling.None;
            btnExportarXlsx.Name = "btnExportarXlsx";
            btnExportarXlsx.Size = new Size(113, 26);
            btnExportarXlsx.Text = "XLSX";
            btnExportarXlsx.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnAnular
            // 
            btnAnular.Animated = true;
            btnAnular.BorderColor = Color.Gainsboro;
            btnAnular.BorderRadius = 16;
            btnAnular.BorderThickness = 1;
            btnAnular.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage1");
            btnAnular.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnAnular.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnAnular.CustomizableEdges = customizableEdges7;
            btnAnular.Dock = DockStyle.Fill;
            btnAnular.FillColor = Color.White;
            btnAnular.Font = new Font("Segoe UI", 9.75F);
            btnAnular.ForeColor = Color.White;
            btnAnular.HoverState.BorderColor = Color.PeachPuff;
            btnAnular.HoverState.FillColor = Color.PeachPuff;
            btnAnular.HoverState.ForeColor = Color.White;
            btnAnular.Location = new Point(1207, 3);
            btnAnular.Name = "btnAnular";
            btnAnular.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnAnular.Size = new Size(31, 30);
            btnAnular.TabIndex = 22;
            // 
            // fieldImporteTotal
            // 
            fieldImporteTotal.Dock = DockStyle.Fill;
            fieldImporteTotal.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldImporteTotal.ForeColor = Color.Black;
            fieldImporteTotal.ImeMode = ImeMode.NoControl;
            fieldImporteTotal.Location = new Point(901, 1);
            fieldImporteTotal.Margin = new Padding(1);
            fieldImporteTotal.Name = "fieldImporteTotal";
            fieldImporteTotal.Size = new Size(108, 34);
            fieldImporteTotal.TabIndex = 20;
            fieldImporteTotal.Text = "importeTotal";
            fieldImporteTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldImpuestoTotal
            // 
            fieldImpuestoTotal.Dock = DockStyle.Fill;
            fieldImpuestoTotal.Font = new Font("Segoe UI", 11.25F);
            fieldImpuestoTotal.ForeColor = Color.Black;
            fieldImpuestoTotal.ImeMode = ImeMode.NoControl;
            fieldImpuestoTotal.Location = new Point(791, 1);
            fieldImpuestoTotal.Margin = new Padding(1);
            fieldImpuestoTotal.Name = "fieldImpuestoTotal";
            fieldImpuestoTotal.Size = new Size(108, 34);
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
            fieldTotalBruto.Location = new Point(571, 1);
            fieldTotalBruto.Margin = new Padding(1);
            fieldTotalBruto.Name = "fieldTotalBruto";
            fieldTotalBruto.Size = new Size(108, 34);
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
            fieldDescuentoTotal.Location = new Point(681, 1);
            fieldDescuentoTotal.Margin = new Padding(1);
            fieldDescuentoTotal.Name = "fieldDescuentoTotal";
            fieldDescuentoTotal.Size = new Size(108, 34);
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
            fieldFechaVenta.Location = new Point(221, 1);
            fieldFechaVenta.Margin = new Padding(1);
            fieldFechaVenta.Name = "fieldFechaVenta";
            fieldFechaVenta.Size = new Size(118, 34);
            fieldFechaVenta.TabIndex = 17;
            fieldFechaVenta.Text = "fecha";
            fieldFechaVenta.TextAlign = ContentAlignment.MiddleLeft;
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
            menuFormatoDocumento.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private Label fieldNumeroFactura;
        private Label fieldFechaVenta;
        private Label fieldImporteTotal;
        private Guna2Button btnVerFactura;
        private Label fieldImpuestoTotal;
        private Label fieldTotalBruto;
        private Label fieldDescuentoTotal;
        private Guna2Button btnAnular;
        private Guna2Separator separador1;
        private Label fieldId;
        private Guna2Button fieldCanalPagoPrincipal;
        private Guna2Button fieldEstado;
        private ContextMenuStrip menuFormatoDocumento;
        private ToolStripMenuItem btnExportarPdf;
        private ToolStripMenuItem btnExportarXlsx;
    }
}