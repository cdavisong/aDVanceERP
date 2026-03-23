using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.CajaRegistradora.Vistas {
    partial class VistaTuplaDetalleTurno {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            layoutBase = new TableLayoutPanel();
            layoutDatosTupla = new TableLayoutPanel();
            fieldMonto = new Label();
            fieldOperador = new Label();
            fieldDescripcionFactura = new Label();
            fieldCanal = new Guna2Button();
            fieldTipo = new Guna2Button();
            fieldFechaHora = new Label();
            separador1 = new Guna2Separator();
            layoutBase.SuspendLayout();
            layoutDatosTupla.SuspendLayout();
            SuspendLayout();
            // 
            // layoutBase
            // 
            layoutBase.BackColor = Color.White;
            layoutBase.ColumnCount = 1;
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBase.Controls.Add(layoutDatosTupla, 0, 0);
            layoutBase.Controls.Add(separador1, 0, 1);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 2;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            layoutBase.Size = new Size(1241, 42);
            layoutBase.TabIndex = 1;
            // 
            // layoutDatosTupla
            // 
            layoutDatosTupla.ColumnCount = 6;
            layoutDatosTupla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19F));
            layoutDatosTupla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13F));
            layoutDatosTupla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13F));
            layoutDatosTupla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            layoutDatosTupla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            layoutDatosTupla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            layoutDatosTupla.Controls.Add(fieldMonto, 5, 0);
            layoutDatosTupla.Controls.Add(fieldOperador, 4, 0);
            layoutDatosTupla.Controls.Add(fieldDescripcionFactura, 3, 0);
            layoutDatosTupla.Controls.Add(fieldCanal, 2, 0);
            layoutDatosTupla.Controls.Add(fieldTipo, 1, 0);
            layoutDatosTupla.Controls.Add(fieldFechaHora, 0, 0);
            layoutDatosTupla.Dock = DockStyle.Fill;
            layoutDatosTupla.Location = new Point(0, 0);
            layoutDatosTupla.Margin = new Padding(0);
            layoutDatosTupla.Name = "layoutDatosTupla";
            layoutDatosTupla.RowCount = 1;
            layoutDatosTupla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDatosTupla.Size = new Size(1241, 37);
            layoutDatosTupla.TabIndex = 73;
            // 
            // fieldMonto
            // 
            fieldMonto.Dock = DockStyle.Fill;
            fieldMonto.Font = new Font("Segoe UI", 11.25F);
            fieldMonto.ForeColor = Color.DimGray;
            fieldMonto.ImeMode = ImeMode.NoControl;
            fieldMonto.Location = new Point(1058, 1);
            fieldMonto.Margin = new Padding(5, 1, 1, 1);
            fieldMonto.Name = "fieldMonto";
            fieldMonto.Size = new Size(182, 35);
            fieldMonto.TabIndex = 46;
            fieldMonto.Text = "$ 0,00";
            fieldMonto.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldOperador
            // 
            fieldOperador.Dock = DockStyle.Fill;
            fieldOperador.Font = new Font("Segoe UI", 11.25F);
            fieldOperador.ForeColor = Color.DimGray;
            fieldOperador.ImeMode = ImeMode.NoControl;
            fieldOperador.Location = new Point(934, 1);
            fieldOperador.Margin = new Padding(5, 1, 1, 1);
            fieldOperador.Name = "fieldOperador";
            fieldOperador.Size = new Size(118, 35);
            fieldOperador.TabIndex = 45;
            fieldOperador.Text = "operador";
            fieldOperador.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldDescripcionFactura
            // 
            fieldDescripcionFactura.Dock = DockStyle.Fill;
            fieldDescripcionFactura.Font = new Font("Segoe UI", 11.25F);
            fieldDescripcionFactura.ForeColor = Color.DimGray;
            fieldDescripcionFactura.ImeMode = ImeMode.NoControl;
            fieldDescripcionFactura.Location = new Point(562, 1);
            fieldDescripcionFactura.Margin = new Padding(5, 1, 1, 1);
            fieldDescripcionFactura.Name = "fieldDescripcionFactura";
            fieldDescripcionFactura.Size = new Size(366, 35);
            fieldDescripcionFactura.TabIndex = 44;
            fieldDescripcionFactura.Text = "descripcion/factura";
            fieldDescripcionFactura.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldCanal
            // 
            fieldCanal.AutoRoundedCorners = true;
            fieldCanal.BorderColor = Color.Gainsboro;
            fieldCanal.BorderRadius = 11;
            fieldCanal.BorderThickness = 1;
            fieldCanal.CustomizableEdges = customizableEdges1;
            fieldCanal.DisabledState.BorderColor = Color.Gainsboro;
            fieldCanal.DisabledState.CustomBorderColor = Color.Gainsboro;
            fieldCanal.DisabledState.FillColor = Color.Gainsboro;
            fieldCanal.DisabledState.ForeColor = Color.DimGray;
            fieldCanal.Dock = DockStyle.Left;
            fieldCanal.Enabled = false;
            fieldCanal.FillColor = Color.Gainsboro;
            fieldCanal.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldCanal.ForeColor = Color.DimGray;
            fieldCanal.HoverState.BorderColor = Color.PeachPuff;
            fieldCanal.HoverState.FillColor = Color.PeachPuff;
            fieldCanal.HoverState.ForeColor = Color.Black;
            fieldCanal.Location = new Point(402, 6);
            fieldCanal.Margin = new Padding(6);
            fieldCanal.Name = "fieldCanal";
            fieldCanal.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fieldCanal.Size = new Size(142, 25);
            fieldCanal.TabIndex = 43;
            fieldCanal.Text = "● canal";
            fieldCanal.TextOffset = new Point(0, -1);
            // 
            // fieldTipo
            // 
            fieldTipo.AutoRoundedCorners = true;
            fieldTipo.BorderColor = Color.Gainsboro;
            fieldTipo.BorderRadius = 11;
            fieldTipo.BorderThickness = 1;
            fieldTipo.CustomizableEdges = customizableEdges3;
            fieldTipo.DisabledState.BorderColor = Color.Gainsboro;
            fieldTipo.DisabledState.CustomBorderColor = Color.Gainsboro;
            fieldTipo.DisabledState.FillColor = Color.Gainsboro;
            fieldTipo.DisabledState.ForeColor = Color.DimGray;
            fieldTipo.Dock = DockStyle.Left;
            fieldTipo.Enabled = false;
            fieldTipo.FillColor = Color.Gainsboro;
            fieldTipo.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            fieldTipo.ForeColor = Color.DimGray;
            fieldTipo.HoverState.BorderColor = Color.PeachPuff;
            fieldTipo.HoverState.FillColor = Color.PeachPuff;
            fieldTipo.HoverState.ForeColor = Color.Black;
            fieldTipo.Location = new Point(241, 6);
            fieldTipo.Margin = new Padding(6);
            fieldTipo.Name = "fieldTipo";
            fieldTipo.ShadowDecoration.CustomizableEdges = customizableEdges4;
            fieldTipo.Size = new Size(142, 25);
            fieldTipo.TabIndex = 42;
            fieldTipo.Text = "● tipo";
            fieldTipo.TextOffset = new Point(0, -1);
            // 
            // fieldFechaHora
            // 
            fieldFechaHora.Dock = DockStyle.Fill;
            fieldFechaHora.Font = new Font("Segoe UI", 11.25F);
            fieldFechaHora.ForeColor = Color.DimGray;
            fieldFechaHora.ImeMode = ImeMode.NoControl;
            fieldFechaHora.Location = new Point(5, 1);
            fieldFechaHora.Margin = new Padding(5, 1, 1, 1);
            fieldFechaHora.Name = "fieldFechaHora";
            fieldFechaHora.Size = new Size(229, 35);
            fieldFechaHora.TabIndex = 40;
            fieldFechaHora.Text = "00/00/0000 00:00";
            fieldFechaHora.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // separador1
            // 
            separador1.Dock = DockStyle.Fill;
            separador1.FillColor = Color.Gainsboro;
            separador1.Location = new Point(1, 38);
            separador1.Margin = new Padding(1);
            separador1.Name = "separador1";
            separador1.Size = new Size(1239, 3);
            separador1.TabIndex = 72;
            // 
            // VistaTuplaDetalleTurno
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1241, 42);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaTuplaDetalleTurno";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaTuplaCajaRegistradora";
            layoutBase.ResumeLayout(false);
            layoutDatosTupla.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private Guna2Separator separador1;
        private TableLayoutPanel layoutDatosTupla;
        private Label fieldFechaHora;
        private Guna2Button fieldTipo;
        private Label fieldDescripcionFactura;
        private Guna2Button fieldCanal;
        private Label fieldMonto;
        private Label fieldOperador;
    }
}