using Guna.UI2.WinForms;

namespace aDVancePOS.Desktop.MVP.Vistas.ContenedorModulos {
    partial class VistaContenedorModulos {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaContenedorModulos));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutDistribucion = new TableLayoutPanel();
            layoutMenuLateral = new TableLayoutPanel();
            layoutModulos = new FlowLayoutPanel();
            btnInicio = new Guna2CircleButton();
            btnPuntoVenta = new Guna2CircleButton();
            contenedorVistas = new Panel();
            layoutMensajeBienvenida = new TableLayoutPanel();
            fieldTextoBienvenida = new Guna2HtmlLabel();
            layoutBase.SuspendLayout();
            layoutDistribucion.SuspendLayout();
            layoutMenuLateral.SuspendLayout();
            layoutModulos.SuspendLayout();
            contenedorVistas.SuspendLayout();
            layoutMensajeBienvenida.SuspendLayout();
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
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutBase.Controls.Add(layoutDistribucion, 0, 0);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 1;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutBase.Size = new Size(1356, 608);
            layoutBase.TabIndex = 1;
            // 
            // layoutDistribucion
            // 
            layoutDistribucion.BackColor = Color.White;
            layoutDistribucion.ColumnCount = 2;
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistribucion.Controls.Add(layoutMenuLateral, 0, 0);
            layoutDistribucion.Controls.Add(contenedorVistas, 1, 0);
            layoutDistribucion.Dock = DockStyle.Fill;
            layoutDistribucion.Location = new Point(0, 0);
            layoutDistribucion.Margin = new Padding(0);
            layoutDistribucion.Name = "layoutDistribucion";
            layoutDistribucion.RowCount = 1;
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutDistribucion.Size = new Size(1356, 608);
            layoutDistribucion.TabIndex = 0;
            // 
            // layoutMenuLateral
            // 
            layoutMenuLateral.BackColor = Color.White;
            layoutMenuLateral.ColumnCount = 1;
            layoutMenuLateral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutMenuLateral.Controls.Add(layoutModulos, 0, 0);
            layoutMenuLateral.Dock = DockStyle.Fill;
            layoutMenuLateral.Location = new Point(0, 10);
            layoutMenuLateral.Margin = new Padding(0, 10, 0, 10);
            layoutMenuLateral.Name = "layoutMenuLateral";
            layoutMenuLateral.RowCount = 1;
            layoutMenuLateral.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutMenuLateral.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutMenuLateral.Size = new Size(50, 588);
            layoutMenuLateral.TabIndex = 0;
            // 
            // layoutModulos
            // 
            layoutModulos.BackColor = Color.White;
            layoutModulos.Controls.Add(btnInicio);
            layoutModulos.Controls.Add(btnPuntoVenta);
            layoutModulos.Dock = DockStyle.Fill;
            layoutModulos.Location = new Point(0, 0);
            layoutModulos.Margin = new Padding(0);
            layoutModulos.Name = "layoutModulos";
            layoutModulos.Size = new Size(50, 588);
            layoutModulos.TabIndex = 0;
            // 
            // btnInicio
            // 
            btnInicio.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnInicio.CheckedState.FillColor = Color.PeachPuff;
            btnInicio.CustomImages.Image = (Image) resources.GetObject("resource.Image");
            btnInicio.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnInicio.CustomImages.ImageSize = new Size(24, 24);
            btnInicio.FillColor = Color.White;
            btnInicio.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnInicio.ForeColor = Color.White;
            btnInicio.ImageSize = new Size(24, 24);
            btnInicio.Location = new Point(3, 3);
            btnInicio.Name = "btnInicio";
            btnInicio.ShadowDecoration.CustomizableEdges = customizableEdges3;
            btnInicio.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnInicio.Size = new Size(44, 44);
            btnInicio.TabIndex = 0;
            // 
            // btnPuntoVenta
            // 
            btnPuntoVenta.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnPuntoVenta.CheckedState.FillColor = Color.PeachPuff;
            btnPuntoVenta.CustomImages.Image = (Image) resources.GetObject("resource.Image1");
            btnPuntoVenta.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnPuntoVenta.CustomImages.ImageSize = new Size(24, 24);
            btnPuntoVenta.FillColor = Color.White;
            btnPuntoVenta.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnPuntoVenta.ForeColor = Color.White;
            btnPuntoVenta.ImageSize = new Size(24, 24);
            btnPuntoVenta.Location = new Point(3, 53);
            btnPuntoVenta.Name = "btnPuntoVenta";
            btnPuntoVenta.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnPuntoVenta.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnPuntoVenta.Size = new Size(44, 44);
            btnPuntoVenta.TabIndex = 4;
            // 
            // contenedorVistas
            // 
            contenedorVistas.Controls.Add(layoutMensajeBienvenida);
            contenedorVistas.Dock = DockStyle.Fill;
            contenedorVistas.Location = new Point(50, 10);
            contenedorVistas.Margin = new Padding(0, 10, 0, 0);
            contenedorVistas.Name = "contenedorVistas";
            contenedorVistas.Size = new Size(1306, 598);
            contenedorVistas.TabIndex = 1;
            // 
            // layoutMensajeBienvenida
            // 
            layoutMensajeBienvenida.ColumnCount = 3;
            layoutMensajeBienvenida.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutMensajeBienvenida.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 1000F));
            layoutMensajeBienvenida.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutMensajeBienvenida.Controls.Add(fieldTextoBienvenida, 1, 1);
            layoutMensajeBienvenida.Dock = DockStyle.Fill;
            layoutMensajeBienvenida.Location = new Point(0, 0);
            layoutMensajeBienvenida.Name = "layoutMensajeBienvenida";
            layoutMensajeBienvenida.RowCount = 3;
            layoutMensajeBienvenida.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutMensajeBienvenida.RowStyles.Add(new RowStyle(SizeType.Absolute, 500F));
            layoutMensajeBienvenida.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutMensajeBienvenida.Size = new Size(1306, 598);
            layoutMensajeBienvenida.TabIndex = 1;
            // 
            // fieldTextoBienvenida
            // 
            fieldTextoBienvenida.BackColor = Color.Transparent;
            fieldTextoBienvenida.Dock = DockStyle.Fill;
            fieldTextoBienvenida.Location = new Point(156, 52);
            fieldTextoBienvenida.Name = "fieldTextoBienvenida";
            fieldTextoBienvenida.Size = new Size(994, 494);
            fieldTextoBienvenida.TabIndex = 0;
            fieldTextoBienvenida.Text = null;
            // 
            // VistaContenedorModulos
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1356, 608);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.Black;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaContenedorModulos";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "FormularioFormatoBase1";
            layoutBase.ResumeLayout(false);
            layoutDistribucion.ResumeLayout(false);
            layoutMenuLateral.ResumeLayout(false);
            layoutModulos.ResumeLayout(false);
            contenedorVistas.ResumeLayout(false);
            layoutMensajeBienvenida.ResumeLayout(false);
            layoutMensajeBienvenida.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutDistribucion;
        private TableLayoutPanel layoutMenuLateral;
        private FlowLayoutPanel layoutModulos;
        private Panel contenedorVistas;
        private Guna2CircleButton btnInicio;
        private Guna2CircleButton btnPuntoVenta;
        private Guna2HtmlLabel fieldTextoBienvenida;
        private TableLayoutPanel layoutMensajeBienvenida;
    }
}