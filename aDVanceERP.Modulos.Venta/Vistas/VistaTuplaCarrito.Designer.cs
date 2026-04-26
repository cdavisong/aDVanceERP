using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Venta.Vistas {
    partial class VistaTuplaCarrito {
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
            layoutBase = new TableLayoutPanel();
            separador1 = new Guna2Separator();
            layoutVista = new TableLayoutPanel();
            btnEliminar = new Label();
            fieldCantidad = new Label();
            fieldSubtotal = new Label();
            fieldNombreProducto = new Label();
            toolTipPresentacion = new ToolTip(components);
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
            layoutBase.Size = new Size(315, 30);
            layoutBase.TabIndex = 1;
            // 
            // separador1
            // 
            separador1.Dock = DockStyle.Fill;
            separador1.FillColor = Color.Gainsboro;
            separador1.Location = new Point(1, 26);
            separador1.Margin = new Padding(1);
            separador1.Name = "separador1";
            separador1.Size = new Size(313, 3);
            separador1.TabIndex = 75;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.White;
            layoutVista.ColumnCount = 4;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutVista.Controls.Add(btnEliminar, 3, 0);
            layoutVista.Controls.Add(fieldCantidad, 1, 0);
            layoutVista.Controls.Add(fieldSubtotal, 2, 0);
            layoutVista.Controls.Add(fieldNombreProducto, 0, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0, 0, 0, 1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(315, 24);
            layoutVista.TabIndex = 19;
            // 
            // btnEliminar
            // 
            btnEliminar.AutoEllipsis = true;
            btnEliminar.Dock = DockStyle.Fill;
            btnEliminar.Font = new Font("Segoe UI", 11.25F);
            btnEliminar.ForeColor = Color.DarkGray;
            btnEliminar.ImeMode = ImeMode.NoControl;
            btnEliminar.Location = new Point(296, 1);
            btnEliminar.Margin = new Padding(1);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(18, 22);
            btnEliminar.TabIndex = 36;
            btnEliminar.Text = "X";
            btnEliminar.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldCantidad
            // 
            fieldCantidad.AutoEllipsis = true;
            fieldCantidad.Dock = DockStyle.Fill;
            fieldCantidad.Font = new Font("Segoe UI", 11.25F);
            fieldCantidad.ForeColor = Color.DimGray;
            fieldCantidad.ImeMode = ImeMode.NoControl;
            fieldCantidad.Location = new Point(151, 1);
            fieldCantidad.Margin = new Padding(1);
            fieldCantidad.Name = "fieldCantidad";
            fieldCantidad.Size = new Size(43, 22);
            fieldCantidad.TabIndex = 35;
            fieldCantidad.Text = "cant";
            fieldCantidad.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldPrecioUnitario
            // 
            fieldSubtotal.Dock = DockStyle.Fill;
            fieldSubtotal.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            fieldSubtotal.ForeColor = Color.Black;
            fieldSubtotal.ImeMode = ImeMode.NoControl;
            fieldSubtotal.Location = new Point(196, 1);
            fieldSubtotal.Margin = new Padding(1);
            fieldSubtotal.Name = "fieldPrecioUnitario";
            fieldSubtotal.Size = new Size(98, 22);
            fieldSubtotal.TabIndex = 34;
            fieldSubtotal.Text = "$ 0.00";
            fieldSubtotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // fieldNombreProducto
            // 
            fieldNombreProducto.AutoEllipsis = true;
            fieldNombreProducto.Dock = DockStyle.Fill;
            fieldNombreProducto.Font = new Font("Segoe UI", 11.25F);
            fieldNombreProducto.ForeColor = Color.DimGray;
            fieldNombreProducto.ImeMode = ImeMode.NoControl;
            fieldNombreProducto.Location = new Point(1, 1);
            fieldNombreProducto.Margin = new Padding(1);
            fieldNombreProducto.Name = "fieldNombreProducto";
            fieldNombreProducto.Size = new Size(148, 22);
            fieldNombreProducto.TabIndex = 17;
            fieldNombreProducto.Text = "nombreProducto";
            fieldNombreProducto.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VistaTuplaCarrito
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(315, 30);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaTuplaCarrito";
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
        private Label fieldNombreProducto;
        private Label fieldSubtotal;
        private ToolTip toolTipPresentacion;
        private Label fieldCantidad;
        private Label btnEliminar;
        private Guna2Separator separador1;
    }
}