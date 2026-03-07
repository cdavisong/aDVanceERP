using System.ComponentModel;
using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Compra.Vistas {
    partial class VistaTuplaSolicitudCompra {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaTuplaSolicitudCompra));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            simboloPeso4 = new Label();
            fieldImporteTotal = new Label();
            fieldObservaciones = new Label();
            btnEliminar = new Guna2Button();
            fieldCodigo = new Label();
            btnEditar = new Guna2Button();
            fieldNombreSolicitante = new Label();
            fieldFechaSolicitud = new Label();
            fieldFechaRequerida = new Label();
            fieldEstado = new Label();
            menuEstados = new ContextMenuStrip(components);
            btnEstadoAprobada = new ToolStripMenuItem();
            btnEstadoRechazada = new ToolStripMenuItem();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            menuEstados.SuspendLayout();
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
            layoutVista.ColumnCount = 11;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.Controls.Add(simboloPeso4, 6, 0);
            layoutVista.Controls.Add(fieldImporteTotal, 5, 0);
            layoutVista.Controls.Add(fieldObservaciones, 4, 0);
            layoutVista.Controls.Add(btnEliminar, 10, 0);
            layoutVista.Controls.Add(fieldCodigo, 0, 0);
            layoutVista.Controls.Add(btnEditar, 9, 0);
            layoutVista.Controls.Add(fieldNombreSolicitante, 1, 0);
            layoutVista.Controls.Add(fieldFechaSolicitud, 2, 0);
            layoutVista.Controls.Add(fieldFechaRequerida, 3, 0);
            layoutVista.Controls.Add(fieldEstado, 7, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0, 0, 0, 1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(1241, 41);
            layoutVista.TabIndex = 18;
            // 
            // simboloPeso4
            // 
            simboloPeso4.Dock = DockStyle.Fill;
            simboloPeso4.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            simboloPeso4.ForeColor = Color.Black;
            simboloPeso4.ImageAlign = ContentAlignment.MiddleLeft;
            simboloPeso4.ImeMode = ImeMode.NoControl;
            simboloPeso4.Location = new Point(913, 5);
            simboloPeso4.Margin = new Padding(3, 5, 3, 3);
            simboloPeso4.Name = "simboloPeso4";
            simboloPeso4.Size = new Size(14, 33);
            simboloPeso4.TabIndex = 31;
            simboloPeso4.Text = "$";
            simboloPeso4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldImporteTotal
            // 
            fieldImporteTotal.Dock = DockStyle.Fill;
            fieldImporteTotal.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldImporteTotal.ForeColor = Color.Black;
            fieldImporteTotal.ImeMode = ImeMode.NoControl;
            fieldImporteTotal.Location = new Point(801, 1);
            fieldImporteTotal.Margin = new Padding(1);
            fieldImporteTotal.Name = "fieldImporteTotal";
            fieldImporteTotal.Size = new Size(108, 39);
            fieldImporteTotal.TabIndex = 21;
            fieldImporteTotal.Text = "importeTotal";
            fieldImporteTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldObservaciones
            // 
            fieldObservaciones.AutoEllipsis = true;
            fieldObservaciones.Dock = DockStyle.Fill;
            fieldObservaciones.Font = new Font("Segoe UI", 11.25F);
            fieldObservaciones.ForeColor = Color.DimGray;
            fieldObservaciones.ImeMode = ImeMode.NoControl;
            fieldObservaciones.Location = new Point(581, 1);
            fieldObservaciones.Margin = new Padding(1);
            fieldObservaciones.Name = "fieldObservaciones";
            fieldObservaciones.Size = new Size(218, 39);
            fieldObservaciones.TabIndex = 18;
            fieldObservaciones.Text = "observaciones";
            fieldObservaciones.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnEliminar
            // 
            btnEliminar.Animated = true;
            btnEliminar.BorderColor = Color.FromArgb(  208,   197,   188);
            btnEliminar.BorderRadius = 16;
            btnEliminar.BorderThickness = 1;
            btnEliminar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage");
            btnEliminar.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnEliminar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnEliminar.CustomizableEdges = customizableEdges5;
            btnEliminar.Dock = DockStyle.Fill;
            btnEliminar.FillColor = Color.White;
            btnEliminar.Font = new Font("Segoe UI", 9.75F);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.HoverState.BorderColor = Color.PeachPuff;
            btnEliminar.HoverState.FillColor = Color.PeachPuff;
            btnEliminar.HoverState.ForeColor = Color.White;
            btnEliminar.Location = new Point(1203, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnEliminar.Size = new Size(35, 35);
            btnEliminar.TabIndex = 11;
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
            fieldCodigo.TabIndex = 4;
            fieldCodigo.Text = "codigo";
            fieldCodigo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnEditar
            // 
            btnEditar.Animated = true;
            btnEditar.BorderColor = Color.FromArgb(  208,   197,   188);
            btnEditar.BorderRadius = 16;
            btnEditar.BorderThickness = 1;
            btnEditar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage1");
            btnEditar.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnEditar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnEditar.CustomizableEdges = customizableEdges7;
            btnEditar.Dock = DockStyle.Fill;
            btnEditar.FillColor = Color.White;
            btnEditar.Font = new Font("Segoe UI", 9.75F);
            btnEditar.ForeColor = Color.White;
            btnEditar.HoverState.BorderColor = Color.PeachPuff;
            btnEditar.HoverState.FillColor = Color.PeachPuff;
            btnEditar.Location = new Point(1163, 3);
            btnEditar.Name = "btnEditar";
            btnEditar.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnEditar.Size = new Size(34, 35);
            btnEditar.TabIndex = 9;
            // 
            // fieldNombreSolicitante
            // 
            fieldNombreSolicitante.AutoEllipsis = true;
            fieldNombreSolicitante.Dock = DockStyle.Fill;
            fieldNombreSolicitante.Font = new Font("Segoe UI", 11.25F);
            fieldNombreSolicitante.ForeColor = Color.DimGray;
            fieldNombreSolicitante.ImeMode = ImeMode.NoControl;
            fieldNombreSolicitante.Location = new Point(161, 1);
            fieldNombreSolicitante.Margin = new Padding(1);
            fieldNombreSolicitante.Name = "fieldNombreSolicitante";
            fieldNombreSolicitante.Size = new Size(178, 39);
            fieldNombreSolicitante.TabIndex = 6;
            fieldNombreSolicitante.Text = "nombreSolicitante";
            fieldNombreSolicitante.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldFechaSolicitud
            // 
            fieldFechaSolicitud.Dock = DockStyle.Fill;
            fieldFechaSolicitud.Font = new Font("Segoe UI", 11.25F);
            fieldFechaSolicitud.ForeColor = Color.DimGray;
            fieldFechaSolicitud.ImeMode = ImeMode.NoControl;
            fieldFechaSolicitud.Location = new Point(341, 1);
            fieldFechaSolicitud.Margin = new Padding(1);
            fieldFechaSolicitud.Name = "fieldFechaSolicitud";
            fieldFechaSolicitud.Size = new Size(118, 39);
            fieldFechaSolicitud.TabIndex = 16;
            fieldFechaSolicitud.Text = "fechaRegistro";
            fieldFechaSolicitud.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldFechaRequerida
            // 
            fieldFechaRequerida.Dock = DockStyle.Fill;
            fieldFechaRequerida.Font = new Font("Segoe UI", 11.25F);
            fieldFechaRequerida.ForeColor = Color.DimGray;
            fieldFechaRequerida.ImeMode = ImeMode.NoControl;
            fieldFechaRequerida.Location = new Point(461, 1);
            fieldFechaRequerida.Margin = new Padding(1);
            fieldFechaRequerida.Name = "fieldFechaRequerida";
            fieldFechaRequerida.Size = new Size(118, 39);
            fieldFechaRequerida.TabIndex = 17;
            fieldFechaRequerida.Text = "fechaRequerida";
            fieldFechaRequerida.TextAlign = ContentAlignment.MiddleCenter;
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
            fieldEstado.TabIndex = 20;
            fieldEstado.Text = "estado";
            fieldEstado.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // menuEstados
            // 
            menuEstados.BackColor = Color.White;
            menuEstados.Items.AddRange(new ToolStripItem[] { btnEstadoAprobada, btnEstadoRechazada });
            menuEstados.Name = "menuGastoIndirecto";
            menuEstados.Size = new Size(185, 78);
            // 
            // btnEstadoAprobada
            // 
            btnEstadoAprobada.BackColor = Color.White;
            btnEstadoAprobada.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point,  0);
            btnEstadoAprobada.Image = (Image) resources.GetObject("btnEstadoAprobada.Image");
            btnEstadoAprobada.ImageAlign = ContentAlignment.MiddleLeft;
            btnEstadoAprobada.ImageScaling = ToolStripItemImageScaling.None;
            btnEstadoAprobada.Name = "btnEstadoAprobada";
            btnEstadoAprobada.Size = new Size(184, 26);
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
            btnEstadoRechazada.Size = new Size(184, 26);
            btnEstadoRechazada.Text = "Rechazada";
            btnEstadoRechazada.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VistaTuplaSolicitudCompra
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1241, 42);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaTuplaSolicitudCompra";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaTuplaCliente";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            menuEstados.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private Guna2Button btnEliminar;
        private Label fieldCodigo;
        private Guna2Button btnEditar;
        private Label fieldNombreSolicitante;
        private Label fieldFechaSolicitud;
        private Label fieldFechaRequerida;
        private Label fieldObservaciones;
        private Label fieldEstado;
        private ContextMenuStrip menuEstados;
        private ToolStripMenuItem btnEstadoAprobada;
        private ToolStripMenuItem btnEstadoRechazada;
        private Label fieldImporteTotal;
        private Label simboloPeso4;
    }
}