using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Compra.Vistas {
    partial class VistaTuplaCompra {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaTuplaCompra));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldObservaciones = new Label();
            fieldEstado = new Label();
            menuEstados = new ContextMenuStrip(components);
            btnEstadoAprobada = new ToolStripMenuItem();
            btnEstadoRechazada = new ToolStripMenuItem();
            btnEstadoRecibida = new ToolStripMenuItem();
            fieldCondicionPago = new Label();
            fieldCodigo = new Label();
            btnVerFactura = new Guna2Button();
            menuFormatoDocumento = new ContextMenuStrip(components);
            btnExportarPdf = new ToolStripMenuItem();
            btnExportarXlsx = new ToolStripMenuItem();
            btnAnular = new Guna2Button();
            simboloPeso4 = new Label();
            fieldImporteTotal = new Label();
            simboloPeso3 = new Label();
            simboloPeso1 = new Label();
            fieldImpuestoTotal = new Label();
            fieldSubtotal = new Label();
            fieldFechaCompra = new Label();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            menuEstados.SuspendLayout();
            menuFormatoDocumento.SuspendLayout();
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
            layoutVista.ColumnCount = 14;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.Controls.Add(fieldObservaciones, 9, 0);
            layoutVista.Controls.Add(fieldEstado, 10, 0);
            layoutVista.Controls.Add(fieldCondicionPago, 2, 0);
            layoutVista.Controls.Add(fieldCodigo, 0, 0);
            layoutVista.Controls.Add(btnVerFactura, 12, 0);
            layoutVista.Controls.Add(btnAnular, 13, 0);
            layoutVista.Controls.Add(simboloPeso4, 8, 0);
            layoutVista.Controls.Add(fieldImporteTotal, 7, 0);
            layoutVista.Controls.Add(simboloPeso3, 6, 0);
            layoutVista.Controls.Add(simboloPeso1, 4, 0);
            layoutVista.Controls.Add(fieldImpuestoTotal, 5, 0);
            layoutVista.Controls.Add(fieldSubtotal, 3, 0);
            layoutVista.Controls.Add(fieldFechaCompra, 1, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0, 0, 0, 1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(1241, 41);
            layoutVista.TabIndex = 19;
            // 
            // fieldObservaciones
            // 
            fieldObservaciones.AutoEllipsis = true;
            fieldObservaciones.Dock = DockStyle.Fill;
            fieldObservaciones.Font = new Font("Segoe UI", 11.25F);
            fieldObservaciones.ForeColor = Color.DimGray;
            fieldObservaciones.ImeMode = ImeMode.NoControl;
            fieldObservaciones.Location = new Point(792, 1);
            fieldObservaciones.Margin = new Padding(5, 1, 1, 1);
            fieldObservaciones.Name = "fieldObservaciones";
            fieldObservaciones.Size = new Size(137, 39);
            fieldObservaciones.TabIndex = 39;
            fieldObservaciones.Text = "observaciones";
            fieldObservaciones.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldEstado
            // 
            fieldEstado.ContextMenuStrip = menuEstados;
            fieldEstado.Dock = DockStyle.Fill;
            fieldEstado.Font = new Font("Segoe UI", 11.25F, FontStyle.Underline);
            fieldEstado.ForeColor = Color.DodgerBlue;
            fieldEstado.ImeMode = ImeMode.NoControl;
            fieldEstado.Location = new Point(931, 1);
            fieldEstado.Margin = new Padding(1);
            fieldEstado.Name = "fieldEstado";
            fieldEstado.Size = new Size(188, 39);
            fieldEstado.TabIndex = 38;
            fieldEstado.Text = "estado";
            fieldEstado.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // menuEstados
            // 
            menuEstados.BackColor = Color.White;
            menuEstados.Items.AddRange(new ToolStripItem[] { btnEstadoAprobada, btnEstadoRechazada, btnEstadoRecibida });
            menuEstados.Name = "menuGastoIndirecto";
            menuEstados.Size = new Size(153, 82);
            // 
            // btnEstadoAprobada
            // 
            btnEstadoAprobada.BackColor = Color.White;
            btnEstadoAprobada.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point,  0);
            btnEstadoAprobada.Image = (Image) resources.GetObject("btnEstadoAprobada.Image");
            btnEstadoAprobada.ImageAlign = ContentAlignment.MiddleLeft;
            btnEstadoAprobada.ImageScaling = ToolStripItemImageScaling.None;
            btnEstadoAprobada.Name = "btnEstadoAprobada";
            btnEstadoAprobada.Size = new Size(152, 26);
            btnEstadoAprobada.Text = "Aprobada";
            btnEstadoAprobada.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnEstadoRechazada
            // 
            btnEstadoRechazada.BackColor = Color.White;
            btnEstadoRechazada.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point,  0);
            btnEstadoRechazada.Image = (Image) resources.GetObject("btnEstadoRechazada.Image");
            btnEstadoRechazada.ImageAlign = ContentAlignment.MiddleLeft;
            btnEstadoRechazada.ImageScaling = ToolStripItemImageScaling.None;
            btnEstadoRechazada.Name = "btnEstadoRechazada";
            btnEstadoRechazada.Size = new Size(152, 26);
            btnEstadoRechazada.Text = "Rechazada";
            btnEstadoRechazada.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnEstadoRecibida
            // 
            btnEstadoRecibida.BackColor = Color.White;
            btnEstadoRecibida.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point,  0);
            btnEstadoRecibida.Image = (Image) resources.GetObject("btnEstadoRecibida.Image");
            btnEstadoRecibida.ImageAlign = ContentAlignment.MiddleLeft;
            btnEstadoRecibida.ImageScaling = ToolStripItemImageScaling.None;
            btnEstadoRecibida.Name = "btnEstadoRecibida";
            btnEstadoRecibida.Size = new Size(152, 26);
            btnEstadoRecibida.Text = "Recibida";
            btnEstadoRecibida.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldCondicionPago
            // 
            fieldCondicionPago.AutoEllipsis = true;
            fieldCondicionPago.Dock = DockStyle.Fill;
            fieldCondicionPago.Font = new Font("Segoe UI", 11.25F);
            fieldCondicionPago.ForeColor = Color.DimGray;
            fieldCondicionPago.ImeMode = ImeMode.NoControl;
            fieldCondicionPago.Location = new Point(285, 1);
            fieldCondicionPago.Margin = new Padding(5, 1, 1, 1);
            fieldCondicionPago.Name = "fieldCondicionPago";
            fieldCondicionPago.Size = new Size(111, 39);
            fieldCondicionPago.TabIndex = 37;
            fieldCondicionPago.Text = "condicionPago";
            fieldCondicionPago.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldCodigo
            // 
            fieldCodigo.Dock = DockStyle.Fill;
            fieldCodigo.Font = new Font("Segoe UI", 11.25F);
            fieldCodigo.ForeColor = Color.DimGray;
            fieldCodigo.ImeMode = ImeMode.NoControl;
            fieldCodigo.Location = new Point(1, 1);
            fieldCodigo.Margin = new Padding(1);
            fieldCodigo.Name = "fieldCodigo";
            fieldCodigo.Size = new Size(158, 39);
            fieldCodigo.TabIndex = 13;
            fieldCodigo.Text = "codigo";
            fieldCodigo.TextAlign = ContentAlignment.MiddleCenter;
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
            btnVerFactura.Location = new Point(1163, 3);
            btnVerFactura.Name = "btnVerFactura";
            btnVerFactura.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnVerFactura.Size = new Size(34, 35);
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
            btnAnular.CustomizableEdges = customizableEdges1;
            btnAnular.Dock = DockStyle.Fill;
            btnAnular.FillColor = Color.White;
            btnAnular.Font = new Font("Segoe UI", 9.75F);
            btnAnular.ForeColor = Color.White;
            btnAnular.HoverState.BorderColor = Color.PeachPuff;
            btnAnular.HoverState.FillColor = Color.PeachPuff;
            btnAnular.HoverState.ForeColor = Color.White;
            btnAnular.Location = new Point(1203, 3);
            btnAnular.Name = "btnAnular";
            btnAnular.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnAnular.Size = new Size(35, 35);
            btnAnular.TabIndex = 22;
            // 
            // simboloPeso4
            // 
            simboloPeso4.Dock = DockStyle.Fill;
            simboloPeso4.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            simboloPeso4.ForeColor = Color.Black;
            simboloPeso4.ImageAlign = ContentAlignment.MiddleLeft;
            simboloPeso4.ImeMode = ImeMode.NoControl;
            simboloPeso4.Location = new Point(770, 5);
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
            fieldImporteTotal.Location = new Point(658, 1);
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
            simboloPeso3.Location = new Point(640, 5);
            simboloPeso3.Margin = new Padding(3, 5, 3, 3);
            simboloPeso3.Name = "simboloPeso3";
            simboloPeso3.Size = new Size(14, 33);
            simboloPeso3.TabIndex = 31;
            simboloPeso3.Text = "$";
            simboloPeso3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // simboloPeso1
            // 
            simboloPeso1.Dock = DockStyle.Fill;
            simboloPeso1.Font = new Font("Segoe UI", 11.25F);
            simboloPeso1.ForeColor = Color.Black;
            simboloPeso1.ImageAlign = ContentAlignment.MiddleLeft;
            simboloPeso1.ImeMode = ImeMode.NoControl;
            simboloPeso1.Location = new Point(510, 5);
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
            fieldImpuestoTotal.Location = new Point(528, 1);
            fieldImpuestoTotal.Margin = new Padding(1);
            fieldImpuestoTotal.Name = "fieldImpuestoTotal";
            fieldImpuestoTotal.Size = new Size(108, 39);
            fieldImpuestoTotal.TabIndex = 34;
            fieldImpuestoTotal.Text = "impuesto";
            fieldImpuestoTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldSubtotal
            // 
            fieldSubtotal.Dock = DockStyle.Fill;
            fieldSubtotal.Font = new Font("Segoe UI", 11.25F);
            fieldSubtotal.ForeColor = Color.Black;
            fieldSubtotal.ImeMode = ImeMode.NoControl;
            fieldSubtotal.Location = new Point(398, 1);
            fieldSubtotal.Margin = new Padding(1);
            fieldSubtotal.Name = "fieldSubtotal";
            fieldSubtotal.Size = new Size(108, 39);
            fieldSubtotal.TabIndex = 35;
            fieldSubtotal.Text = "subtotal";
            fieldSubtotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldFechaCompra
            // 
            fieldFechaCompra.Dock = DockStyle.Fill;
            fieldFechaCompra.Font = new Font("Segoe UI", 11.25F);
            fieldFechaCompra.ForeColor = Color.DimGray;
            fieldFechaCompra.ImeMode = ImeMode.NoControl;
            fieldFechaCompra.Location = new Point(161, 1);
            fieldFechaCompra.Margin = new Padding(1);
            fieldFechaCompra.Name = "fieldFechaCompra";
            fieldFechaCompra.Size = new Size(118, 39);
            fieldFechaCompra.TabIndex = 17;
            fieldFechaCompra.Text = "fecha";
            fieldFechaCompra.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // VistaTuplaCompra
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1241, 42);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaTuplaCompra";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaTuplaCompra";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            menuEstados.ResumeLayout(false);
            menuFormatoDocumento.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private Label fieldCodigo;
        private Label fieldFechaCompra;
        private Label fieldImporteTotal;
        private Guna2Button btnVerFactura;
        private Label simboloPeso4;
        private Label simboloPeso3;
        private Label simboloPeso1;
        private Label fieldImpuestoTotal;
        private Label fieldSubtotal;
        private Label fieldCondicionPago;
        private Label fieldEstado;
        private Guna2Button btnAnular;
        private ContextMenuStrip menuFormatoDocumento;
        private ToolStripMenuItem btnExportarPdf;
        private ToolStripMenuItem btnExportarXlsx;
        private Label fieldObservaciones;
        private ContextMenuStrip menuEstados;
        private ToolStripMenuItem btnEstadoAprobada;
        private ToolStripMenuItem btnEstadoRechazada;
        private ToolStripMenuItem btnEstadoRecibida;
    }
}