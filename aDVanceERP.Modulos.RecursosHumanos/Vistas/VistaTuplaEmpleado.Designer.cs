using System.ComponentModel;
using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.RecursosHumanos.Vistas {
    partial class VistaTuplaEmpleado {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaTuplaEmpleado));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldEstado = new Label();
            fieldFechaContrato = new Label();
            btnEliminar = new Guna2Button();
            fieldCodigo = new Label();
            fieldId = new Label();
            btnEditar = new Guna2Button();
            fieldNombreCompleto = new Label();
            fieldCargo = new Label();
            fieldDepartamento = new Label();
            layoutBase.SuspendLayout();
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
            layoutVista.ColumnCount = 10;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.Controls.Add(fieldEstado, 6, 0);
            layoutVista.Controls.Add(fieldFechaContrato, 5, 0);
            layoutVista.Controls.Add(btnEliminar, 9, 0);
            layoutVista.Controls.Add(fieldCodigo, 1, 0);
            layoutVista.Controls.Add(fieldId, 0, 0);
            layoutVista.Controls.Add(btnEditar, 8, 0);
            layoutVista.Controls.Add(fieldNombreCompleto, 2, 0);
            layoutVista.Controls.Add(fieldCargo, 3, 0);
            layoutVista.Controls.Add(fieldDepartamento, 4, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0, 0, 0, 1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(1241, 41);
            layoutVista.TabIndex = 18;
            // 
            // fieldEstado
            // 
            fieldEstado.Dock = DockStyle.Fill;
            fieldEstado.Font = new Font("Segoe UI", 11.25F);
            fieldEstado.ForeColor = Color.DimGray;
            fieldEstado.ImeMode = ImeMode.NoControl;
            fieldEstado.Location = new Point(1022, 1);
            fieldEstado.Margin = new Padding(1);
            fieldEstado.Name = "fieldEstado";
            fieldEstado.Size = new Size(98, 39);
            fieldEstado.TabIndex = 19;
            fieldEstado.Text = "estado";
            fieldEstado.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldFechaContrato
            // 
            fieldFechaContrato.Dock = DockStyle.Fill;
            fieldFechaContrato.Font = new Font("Segoe UI", 11.25F);
            fieldFechaContrato.ForeColor = Color.DimGray;
            fieldFechaContrato.ImeMode = ImeMode.NoControl;
            fieldFechaContrato.Location = new Point(902, 1);
            fieldFechaContrato.Margin = new Padding(1);
            fieldFechaContrato.Name = "fieldFechaContrato";
            fieldFechaContrato.Size = new Size(118, 39);
            fieldFechaContrato.TabIndex = 18;
            fieldFechaContrato.Text = "fechaContrato";
            fieldFechaContrato.TextAlign = ContentAlignment.MiddleCenter;
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
            btnEliminar.CustomizableEdges = customizableEdges1;
            btnEliminar.Dock = DockStyle.Fill;
            btnEliminar.FillColor = Color.White;
            btnEliminar.Font = new Font("Segoe UI", 9.75F);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.HoverState.BorderColor = Color.PeachPuff;
            btnEliminar.HoverState.FillColor = Color.PeachPuff;
            btnEliminar.HoverState.ForeColor = Color.White;
            btnEliminar.Location = new Point(1204, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnEliminar.Size = new Size(34, 35);
            btnEliminar.TabIndex = 11;
            // 
            // fieldCodigo
            // 
            fieldCodigo.Dock = DockStyle.Fill;
            fieldCodigo.Font = new Font("Segoe UI", 11.25F);
            fieldCodigo.ForeColor = Color.DimGray;
            fieldCodigo.ImeMode = ImeMode.NoControl;
            fieldCodigo.Location = new Point(61, 1);
            fieldCodigo.Margin = new Padding(1);
            fieldCodigo.Name = "fieldCodigo";
            fieldCodigo.Size = new Size(128, 39);
            fieldCodigo.TabIndex = 4;
            fieldCodigo.Text = "codigo";
            fieldCodigo.TextAlign = ContentAlignment.MiddleCenter;
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
            btnEditar.BorderColor = Color.FromArgb(  208,   197,   188);
            btnEditar.BorderRadius = 16;
            btnEditar.BorderThickness = 1;
            btnEditar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage1");
            btnEditar.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnEditar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnEditar.CustomizableEdges = customizableEdges3;
            btnEditar.Dock = DockStyle.Fill;
            btnEditar.FillColor = Color.White;
            btnEditar.Font = new Font("Segoe UI", 9.75F);
            btnEditar.ForeColor = Color.White;
            btnEditar.HoverState.BorderColor = Color.PeachPuff;
            btnEditar.HoverState.FillColor = Color.PeachPuff;
            btnEditar.Location = new Point(1164, 3);
            btnEditar.Name = "btnEditar";
            btnEditar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnEditar.Size = new Size(34, 35);
            btnEditar.TabIndex = 9;
            // 
            // fieldNombreCompleto
            // 
            fieldNombreCompleto.AutoEllipsis = true;
            fieldNombreCompleto.Dock = DockStyle.Fill;
            fieldNombreCompleto.Font = new Font("Segoe UI", 11.25F);
            fieldNombreCompleto.ForeColor = Color.DimGray;
            fieldNombreCompleto.ImeMode = ImeMode.NoControl;
            fieldNombreCompleto.Location = new Point(191, 1);
            fieldNombreCompleto.Margin = new Padding(1);
            fieldNombreCompleto.Name = "fieldNombreCompleto";
            fieldNombreCompleto.Size = new Size(259, 39);
            fieldNombreCompleto.TabIndex = 6;
            fieldNombreCompleto.Text = "nombreCompleto";
            fieldNombreCompleto.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldCargo
            // 
            fieldCargo.AutoEllipsis = true;
            fieldCargo.Dock = DockStyle.Fill;
            fieldCargo.Font = new Font("Segoe UI", 11.25F);
            fieldCargo.ForeColor = Color.DimGray;
            fieldCargo.ImeMode = ImeMode.NoControl;
            fieldCargo.Location = new Point(452, 1);
            fieldCargo.Margin = new Padding(1);
            fieldCargo.Name = "fieldCargo";
            fieldCargo.Size = new Size(198, 39);
            fieldCargo.TabIndex = 16;
            fieldCargo.Text = "cargo";
            fieldCargo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldDepartamento
            // 
            fieldDepartamento.Dock = DockStyle.Fill;
            fieldDepartamento.Font = new Font("Segoe UI", 11.25F);
            fieldDepartamento.ForeColor = Color.DimGray;
            fieldDepartamento.ImeMode = ImeMode.NoControl;
            fieldDepartamento.Location = new Point(652, 1);
            fieldDepartamento.Margin = new Padding(1);
            fieldDepartamento.Name = "fieldDepartamento";
            fieldDepartamento.Size = new Size(248, 39);
            fieldDepartamento.TabIndex = 17;
            fieldDepartamento.Text = "departamento";
            fieldDepartamento.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VistaTuplaEmpleado
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1241, 42);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaTuplaEmpleado";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaTuplaCliente";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private Guna2Button btnEliminar;
        private Label fieldCodigo;
        private Label fieldId;
        private Guna2Button btnEditar;
        private Label fieldNombreCompleto;
        private Label fieldCargo;
        private Label fieldDepartamento;
        private Label fieldEstado;
        private Label fieldFechaContrato;
    }
}