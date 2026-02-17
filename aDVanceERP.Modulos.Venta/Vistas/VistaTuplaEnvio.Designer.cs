using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Venta.Vistas {
    partial class VistaTuplaEnvio {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaTuplaEnvio));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldNumeroFactura = new Label();
            fieldId = new Label();
            btnConfirmar = new Guna2Button();
            btnCancelar = new Guna2Button();
            fieldFechaAsignacion = new Label();
            fieldEstado = new Label();
            simboloPeso4 = new Label();
            fieldMonto = new Label();
            fieldObservaciones = new Label();
            fieldFechaEntrega = new Label();
            fieldTipoEnvio = new Label();
            fieldNombreMensajero = new Label();
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
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.Controls.Add(fieldNumeroFactura, 1, 0);
            layoutVista.Controls.Add(fieldId, 0, 0);
            layoutVista.Controls.Add(btnConfirmar, 11, 0);
            layoutVista.Controls.Add(btnCancelar, 12, 0);
            layoutVista.Controls.Add(fieldFechaAsignacion, 4, 0);
            layoutVista.Controls.Add(fieldEstado, 9, 0);
            layoutVista.Controls.Add(simboloPeso4, 8, 0);
            layoutVista.Controls.Add(fieldMonto, 7, 0);
            layoutVista.Controls.Add(fieldObservaciones, 6, 0);
            layoutVista.Controls.Add(fieldFechaEntrega, 5, 0);
            layoutVista.Controls.Add(fieldTipoEnvio, 3, 0);
            layoutVista.Controls.Add(fieldNombreMensajero, 2, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0, 0, 0, 1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(1241, 41);
            layoutVista.TabIndex = 19;
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
            btnConfirmar.Location = new Point(1163, 3);
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
            btnCancelar.Location = new Point(1203, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnCancelar.Size = new Size(35, 35);
            btnCancelar.TabIndex = 22;
            // 
            // fieldFechaAsignacion
            // 
            fieldFechaAsignacion.Dock = DockStyle.Fill;
            fieldFechaAsignacion.Font = new Font("Segoe UI", 11.25F);
            fieldFechaAsignacion.ForeColor = Color.DimGray;
            fieldFechaAsignacion.ImeMode = ImeMode.NoControl;
            fieldFechaAsignacion.Location = new Point(525, 1);
            fieldFechaAsignacion.Margin = new Padding(1);
            fieldFechaAsignacion.Name = "fieldFechaAsignacion";
            fieldFechaAsignacion.Size = new Size(118, 39);
            fieldFechaAsignacion.TabIndex = 35;
            fieldFechaAsignacion.Text = "fechaAsignacion";
            fieldFechaAsignacion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldEstado
            // 
            fieldEstado.Dock = DockStyle.Fill;
            fieldEstado.Font = new Font("Segoe UI", 11.25F);
            fieldEstado.ForeColor = Color.DimGray;
            fieldEstado.ImeMode = ImeMode.NoControl;
            fieldEstado.Location = new Point(991, 1);
            fieldEstado.Margin = new Padding(1);
            fieldEstado.Name = "fieldEstado";
            fieldEstado.Size = new Size(128, 39);
            fieldEstado.TabIndex = 38;
            fieldEstado.Text = "estado";
            fieldEstado.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // simboloPeso4
            // 
            simboloPeso4.Dock = DockStyle.Fill;
            simboloPeso4.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            simboloPeso4.ForeColor = Color.Black;
            simboloPeso4.ImageAlign = ContentAlignment.MiddleLeft;
            simboloPeso4.ImeMode = ImeMode.NoControl;
            simboloPeso4.Location = new Point(973, 5);
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
            fieldMonto.Location = new Point(861, 1);
            fieldMonto.Margin = new Padding(1);
            fieldMonto.Name = "fieldMonto";
            fieldMonto.Size = new Size(108, 39);
            fieldMonto.TabIndex = 20;
            fieldMonto.Text = "monto";
            fieldMonto.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldObservaciones
            // 
            fieldObservaciones.AutoEllipsis = true;
            fieldObservaciones.Dock = DockStyle.Fill;
            fieldObservaciones.Font = new Font("Segoe UI", 11.25F);
            fieldObservaciones.ForeColor = Color.DimGray;
            fieldObservaciones.ImeMode = ImeMode.NoControl;
            fieldObservaciones.Location = new Point(769, 1);
            fieldObservaciones.Margin = new Padding(5, 1, 1, 1);
            fieldObservaciones.Name = "fieldObservaciones";
            fieldObservaciones.Size = new Size(90, 39);
            fieldObservaciones.TabIndex = 37;
            fieldObservaciones.Text = "observaciones";
            fieldObservaciones.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldFechaEntrega
            // 
            fieldFechaEntrega.Dock = DockStyle.Fill;
            fieldFechaEntrega.Font = new Font("Segoe UI", 11.25F);
            fieldFechaEntrega.ForeColor = Color.DimGray;
            fieldFechaEntrega.ImeMode = ImeMode.NoControl;
            fieldFechaEntrega.Location = new Point(645, 1);
            fieldFechaEntrega.Margin = new Padding(1);
            fieldFechaEntrega.Name = "fieldFechaEntrega";
            fieldFechaEntrega.Size = new Size(118, 39);
            fieldFechaEntrega.TabIndex = 17;
            fieldFechaEntrega.Text = "fechaEntrega";
            fieldFechaEntrega.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldTipoEnvio
            // 
            fieldTipoEnvio.Dock = DockStyle.Fill;
            fieldTipoEnvio.Font = new Font("Segoe UI", 11.25F);
            fieldTipoEnvio.ForeColor = Color.DimGray;
            fieldTipoEnvio.ImeMode = ImeMode.NoControl;
            fieldTipoEnvio.Location = new Point(365, 1);
            fieldTipoEnvio.Margin = new Padding(1);
            fieldTipoEnvio.Name = "fieldTipoEnvio";
            fieldTipoEnvio.Size = new Size(158, 39);
            fieldTipoEnvio.TabIndex = 39;
            fieldTipoEnvio.Text = "tipoEnvio";
            fieldTipoEnvio.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldNombreMensajero
            // 
            fieldNombreMensajero.AutoEllipsis = true;
            fieldNombreMensajero.Dock = DockStyle.Fill;
            fieldNombreMensajero.Font = new Font("Segoe UI", 11.25F);
            fieldNombreMensajero.ForeColor = Color.DimGray;
            fieldNombreMensajero.ImeMode = ImeMode.NoControl;
            fieldNombreMensajero.Location = new Point(221, 1);
            fieldNombreMensajero.Margin = new Padding(1);
            fieldNombreMensajero.Name = "fieldNombreMensajero";
            fieldNombreMensajero.Size = new Size(142, 39);
            fieldNombreMensajero.TabIndex = 40;
            fieldNombreMensajero.Text = "nombreMensajero";
            fieldNombreMensajero.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VistaTuplaEnvio
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1241, 42);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaTuplaEnvio";
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
        private Label fieldFechaEntrega;
        private Label fieldMonto;
        private Guna2Button btnConfirmar;
        private Guna2Button btnCancelar;
        private Label simboloPeso4;
        private Label fieldFechaAsignacion;
        private Label fieldObservaciones;
        private Label fieldEstado;
        private Label fieldTipoEnvio;
        private Label fieldNumeroFactura;
        private Label fieldNombreMensajero;
    }
}