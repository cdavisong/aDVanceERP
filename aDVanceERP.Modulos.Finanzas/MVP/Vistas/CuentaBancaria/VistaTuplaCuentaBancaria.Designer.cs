using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.CuentaBancaria {
    partial class VistaTuplaCuentaBancaria {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaTuplaCuentaBancaria));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldAlias = new Label();
            btnEliminar = new Guna2Button();
            fieldId = new Label();
            btnEditar = new Guna2Button();
            fieldNumeroTarjeta = new Label();
            fieldTipoMoneda = new Label();
            fieldNombrePropietario = new Label();
            btnQR = new Guna2Button();
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
            layoutVista.ColumnCount = 9;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutVista.Controls.Add(fieldAlias, 0, 0);
            layoutVista.Controls.Add(btnEliminar, 8, 0);
            layoutVista.Controls.Add(fieldId, 0, 0);
            layoutVista.Controls.Add(btnEditar, 7, 0);
            layoutVista.Controls.Add(fieldNumeroTarjeta, 2, 0);
            layoutVista.Controls.Add(fieldTipoMoneda, 3, 0);
            layoutVista.Controls.Add(fieldNombrePropietario, 4, 0);
            layoutVista.Controls.Add(btnQR, 6, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0, 0, 0, 1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(1241, 40);
            layoutVista.TabIndex = 18;
            // 
            // fieldAlias
            // 
            fieldAlias.Dock = DockStyle.Fill;
            fieldAlias.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldAlias.ForeColor = Color.DimGray;
            fieldAlias.ImeMode = ImeMode.NoControl;
            fieldAlias.Location = new Point(61, 1);
            fieldAlias.Margin = new Padding(1);
            fieldAlias.Name = "fieldAlias";
            fieldAlias.Size = new Size(218, 38);
            fieldAlias.TabIndex = 20;
            fieldAlias.Text = "alias";
            fieldAlias.TextAlign = ContentAlignment.MiddleLeft;
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
            btnEliminar.Location = new Point(1204, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnEliminar.Size = new Size(34, 34);
            btnEliminar.TabIndex = 11;
            // 
            // fieldId
            // 
            fieldId.Dock = DockStyle.Fill;
            fieldId.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldId.ForeColor = Color.DimGray;
            fieldId.ImeMode = ImeMode.NoControl;
            fieldId.Location = new Point(1, 1);
            fieldId.Margin = new Padding(1);
            fieldId.Name = "fieldId";
            fieldId.Size = new Size(58, 38);
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
            btnEditar.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage1");
            btnEditar.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnEditar.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnEditar.CustomizableEdges = customizableEdges3;
            btnEditar.Dock = DockStyle.Fill;
            btnEditar.FillColor = Color.White;
            btnEditar.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnEditar.ForeColor = Color.White;
            btnEditar.HoverState.BorderColor = Color.PeachPuff;
            btnEditar.HoverState.FillColor = Color.PeachPuff;
            btnEditar.Location = new Point(1164, 3);
            btnEditar.Name = "btnEditar";
            btnEditar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnEditar.Size = new Size(34, 34);
            btnEditar.TabIndex = 9;
            // 
            // fieldNumeroTarjeta
            // 
            fieldNumeroTarjeta.Dock = DockStyle.Fill;
            fieldNumeroTarjeta.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldNumeroTarjeta.ForeColor = Color.DimGray;
            fieldNumeroTarjeta.ImeMode = ImeMode.NoControl;
            fieldNumeroTarjeta.Location = new Point(281, 1);
            fieldNumeroTarjeta.Margin = new Padding(1);
            fieldNumeroTarjeta.Name = "fieldNumeroTarjeta";
            fieldNumeroTarjeta.Size = new Size(158, 38);
            fieldNumeroTarjeta.TabIndex = 19;
            fieldNumeroTarjeta.Text = "numeroTarjeta";
            fieldNumeroTarjeta.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldTipoMoneda
            // 
            fieldTipoMoneda.Dock = DockStyle.Fill;
            fieldTipoMoneda.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldTipoMoneda.ForeColor = Color.DimGray;
            fieldTipoMoneda.ImeMode = ImeMode.NoControl;
            fieldTipoMoneda.Location = new Point(441, 1);
            fieldTipoMoneda.Margin = new Padding(1);
            fieldTipoMoneda.Name = "fieldTipoMoneda";
            fieldTipoMoneda.Size = new Size(93, 38);
            fieldTipoMoneda.TabIndex = 4;
            fieldTipoMoneda.Text = "tipoMoneda";
            fieldTipoMoneda.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldNombrePropietario
            // 
            fieldNombrePropietario.AutoEllipsis = true;
            fieldNombrePropietario.Dock = DockStyle.Fill;
            fieldNombrePropietario.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            fieldNombrePropietario.ForeColor = Color.DimGray;
            fieldNombrePropietario.ImeMode = ImeMode.NoControl;
            fieldNombrePropietario.Location = new Point(536, 1);
            fieldNombrePropietario.Margin = new Padding(1);
            fieldNombrePropietario.Name = "fieldNombrePropietario";
            fieldNombrePropietario.Size = new Size(218, 38);
            fieldNombrePropietario.TabIndex = 6;
            fieldNombrePropietario.Text = "nombrePropietario";
            fieldNombrePropietario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnQR
            // 
            btnQR.Animated = true;
            btnQR.BorderColor = Color.Gainsboro;
            btnQR.BorderRadius = 16;
            btnQR.BorderThickness = 1;
            btnQR.CustomImages.HoveredImage = (Image) resources.GetObject("resource.HoveredImage2");
            btnQR.CustomImages.Image = (Image) resources.GetObject("resource.Image2");
            btnQR.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnQR.CustomizableEdges = customizableEdges5;
            btnQR.Dock = DockStyle.Fill;
            btnQR.FillColor = Color.White;
            btnQR.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnQR.ForeColor = Color.White;
            btnQR.HoverState.BorderColor = Color.PeachPuff;
            btnQR.HoverState.FillColor = Color.PeachPuff;
            btnQR.Location = new Point(1124, 3);
            btnQR.Name = "btnQR";
            btnQR.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnQR.Size = new Size(34, 34);
            btnQR.TabIndex = 21;
            // 
            // VistaTuplaCuenta
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1241, 42);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaTuplaCuenta";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaTuplaCuenta";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private Guna2Button btnEliminar;
        private Label fieldTipoMoneda;
        private Label fieldId;
        private Guna2Button btnEditar;
        private Label fieldNombrePropietario;
        private Label fieldNumeroTarjeta;
        private Label fieldAlias;
        private Guna2Button btnQR;
    }
}