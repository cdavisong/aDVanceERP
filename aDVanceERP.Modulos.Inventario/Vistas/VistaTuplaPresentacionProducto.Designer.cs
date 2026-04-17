using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    partial class VistaTuplaPresentacionProducto {
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VistaTuplaPresentacionProducto));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            separador1 = new Guna2Separator();
            layoutVista = new TableLayoutPanel();
            fieldDescuento = new Label();
            fieldCantidad = new Label();
            fieldEstado = new Guna2Button();
            btnEliminar = new Guna2Button();
            fieldNombreUM = new Label();
            btnEditar = new Guna2Button();
            fieldPrecioUnidad = new Label();
            fieldPrecioVenta = new Label();
            fieldAbreviaturaUM = new Label();
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
            separador1.TabIndex = 73;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.White;
            layoutVista.ColumnCount = 10;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            layoutVista.Controls.Add(fieldDescuento, 5, 0);
            layoutVista.Controls.Add(fieldCantidad, 2, 0);
            layoutVista.Controls.Add(fieldEstado, 6, 0);
            layoutVista.Controls.Add(btnEliminar, 9, 0);
            layoutVista.Controls.Add(fieldNombreUM, 0, 0);
            layoutVista.Controls.Add(btnEditar, 8, 0);
            layoutVista.Controls.Add(fieldPrecioUnidad, 4, 0);
            layoutVista.Controls.Add(fieldPrecioVenta, 3, 0);
            layoutVista.Controls.Add(fieldAbreviaturaUM, 1, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(1241, 37);
            layoutVista.TabIndex = 18;
            // 
            // fieldDescuento
            // 
            fieldDescuento.AutoEllipsis = true;
            fieldDescuento.Dock = DockStyle.Fill;
            fieldDescuento.Font = new Font("Segoe UI", 11.25F);
            fieldDescuento.ForeColor = Color.DimGray;
            fieldDescuento.ImageAlign = ContentAlignment.MiddleLeft;
            fieldDescuento.ImeMode = ImeMode.NoControl;
            fieldDescuento.Location = new Point(841, 1);
            fieldDescuento.Margin = new Padding(1);
            fieldDescuento.Name = "fieldDescuento";
            fieldDescuento.Size = new Size(178, 35);
            fieldDescuento.TabIndex = 45;
            fieldDescuento.Text = "Sin descuento";
            fieldDescuento.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldCantidad
            // 
            fieldCantidad.AutoEllipsis = true;
            fieldCantidad.Dock = DockStyle.Fill;
            fieldCantidad.Font = new Font("Segoe UI", 11.25F);
            fieldCantidad.ForeColor = Color.DimGray;
            fieldCantidad.ImeMode = ImeMode.NoControl;
            fieldCantidad.Location = new Point(441, 1);
            fieldCantidad.Margin = new Padding(1);
            fieldCantidad.Name = "fieldCantidad";
            fieldCantidad.Size = new Size(118, 35);
            fieldCantidad.TabIndex = 44;
            fieldCantidad.Text = "cant";
            fieldCantidad.TextAlign = ContentAlignment.MiddleLeft;
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
            fieldEstado.Location = new Point(1026, 6);
            fieldEstado.Margin = new Padding(6);
            fieldEstado.Name = "fieldEstado";
            fieldEstado.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fieldEstado.Size = new Size(98, 25);
            fieldEstado.TabIndex = 43;
            fieldEstado.Text = "● estado";
            fieldEstado.TextOffset = new Point(0, -1);
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
            btnEliminar.CustomizableEdges = customizableEdges3;
            btnEliminar.Dock = DockStyle.Fill;
            btnEliminar.FillColor = Color.White;
            btnEliminar.Font = new Font("Segoe UI", 9.75F);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.HoverState.BorderColor = Color.PeachPuff;
            btnEliminar.HoverState.FillColor = Color.PeachPuff;
            btnEliminar.HoverState.ForeColor = Color.White;
            btnEliminar.Location = new Point(1207, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnEliminar.Size = new Size(31, 31);
            btnEliminar.TabIndex = 11;
            // 
            // fieldNombreUM
            // 
            fieldNombreUM.Dock = DockStyle.Fill;
            fieldNombreUM.Font = new Font("Segoe UI", 11.25F);
            fieldNombreUM.ForeColor = Color.DimGray;
            fieldNombreUM.ImeMode = ImeMode.NoControl;
            fieldNombreUM.Location = new Point(1, 1);
            fieldNombreUM.Margin = new Padding(1);
            fieldNombreUM.Name = "fieldNombreUM";
            fieldNombreUM.Size = new Size(318, 35);
            fieldNombreUM.TabIndex = 13;
            fieldNombreUM.Text = "nombreUM";
            fieldNombreUM.TextAlign = ContentAlignment.MiddleLeft;
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
            btnEditar.CustomizableEdges = customizableEdges5;
            btnEditar.Dock = DockStyle.Fill;
            btnEditar.FillColor = Color.White;
            btnEditar.Font = new Font("Segoe UI", 9.75F);
            btnEditar.ForeColor = Color.White;
            btnEditar.HoverState.BorderColor = Color.PeachPuff;
            btnEditar.HoverState.FillColor = Color.PeachPuff;
            btnEditar.Location = new Point(1170, 3);
            btnEditar.Name = "btnEditar";
            btnEditar.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnEditar.Size = new Size(31, 31);
            btnEditar.TabIndex = 9;
            // 
            // fieldPrecioUnidad
            // 
            fieldPrecioUnidad.AutoEllipsis = true;
            fieldPrecioUnidad.Dock = DockStyle.Fill;
            fieldPrecioUnidad.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldPrecioUnidad.ForeColor = Color.Black;
            fieldPrecioUnidad.ImeMode = ImeMode.NoControl;
            fieldPrecioUnidad.Location = new Point(701, 1);
            fieldPrecioUnidad.Margin = new Padding(1);
            fieldPrecioUnidad.Name = "fieldPrecioUnidad";
            fieldPrecioUnidad.Size = new Size(138, 35);
            fieldPrecioUnidad.TabIndex = 14;
            fieldPrecioUnidad.Text = "precioUnidad";
            fieldPrecioUnidad.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldPrecioVenta
            // 
            fieldPrecioVenta.AutoEllipsis = true;
            fieldPrecioVenta.Dock = DockStyle.Fill;
            fieldPrecioVenta.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldPrecioVenta.ForeColor = Color.Black;
            fieldPrecioVenta.ImeMode = ImeMode.NoControl;
            fieldPrecioVenta.Location = new Point(561, 1);
            fieldPrecioVenta.Margin = new Padding(1);
            fieldPrecioVenta.Name = "fieldPrecioVenta";
            fieldPrecioVenta.Size = new Size(138, 35);
            fieldPrecioVenta.TabIndex = 6;
            fieldPrecioVenta.Text = "precioVenta";
            fieldPrecioVenta.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldAbreviaturaUM
            // 
            fieldAbreviaturaUM.Dock = DockStyle.Fill;
            fieldAbreviaturaUM.Font = new Font("Segoe UI", 11.25F);
            fieldAbreviaturaUM.ForeColor = Color.DimGray;
            fieldAbreviaturaUM.ImeMode = ImeMode.NoControl;
            fieldAbreviaturaUM.Location = new Point(321, 1);
            fieldAbreviaturaUM.Margin = new Padding(1);
            fieldAbreviaturaUM.Name = "fieldAbreviaturaUM";
            fieldAbreviaturaUM.Size = new Size(118, 35);
            fieldAbreviaturaUM.TabIndex = 4;
            fieldAbreviaturaUM.Text = "abreviaturaUM";
            fieldAbreviaturaUM.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VistaTuplaVentaPresentacion
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1241, 42);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaTuplaVentaPresentacion";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaTuplaVentaPresentacion";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private Guna2Button btnEliminar;
        private Label fieldAbreviaturaUM;
        private Label fieldNombreUM;
        private Guna2Button btnEditar;
        private Label fieldPrecioUnidad;
        private Label fieldPrecioVenta;
        private Guna2Separator separador1;
        private Guna2Button fieldEstado;
        private Label fieldCantidad;
        private Label fieldDescuento;
    }
}