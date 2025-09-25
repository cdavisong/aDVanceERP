using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.Menu {
    partial class VistaMenuFinanzas {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaMenuFinanzas));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            formatoBase = new Guna2BorderlessForm(components);
            layoutBase = new TableLayoutPanel();
            layoutDistribucion = new TableLayoutPanel();
            fieldTitulo = new Label();
            panelRelleno = new Panel();
            btnCuentasBancarias = new Guna2Button();
            btnCajas = new Guna2Button();
            layoutBase.SuspendLayout();
            layoutDistribucion.SuspendLayout();
            panelRelleno.SuspendLayout();
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
            layoutBase.BackColor = Color.WhiteSmoke;
            layoutBase.ColumnCount = 1;
            layoutBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutBase.Controls.Add(layoutDistribucion, 0, 0);
            layoutBase.Dock = DockStyle.Fill;
            layoutBase.Location = new Point(0, 0);
            layoutBase.Name = "layoutBase";
            layoutBase.RowCount = 1;
            layoutBase.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutBase.Size = new Size(994, 50);
            layoutBase.TabIndex = 0;
            // 
            // layoutDistribucion
            // 
            layoutDistribucion.BackColor = Color.WhiteSmoke;
            layoutDistribucion.ColumnCount = 2;
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDistribucion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            layoutDistribucion.Controls.Add(fieldTitulo, 0, 0);
            layoutDistribucion.Controls.Add(panelRelleno, 1, 0);
            layoutDistribucion.Dock = DockStyle.Fill;
            layoutDistribucion.Location = new Point(0, 0);
            layoutDistribucion.Margin = new Padding(0);
            layoutDistribucion.Name = "layoutDistribucion";
            layoutDistribucion.RowCount = 1;
            layoutDistribucion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDistribucion.Size = new Size(994, 50);
            layoutDistribucion.TabIndex = 0;
            // 
            // fieldTitulo
            // 
            fieldTitulo.BackColor = Color.WhiteSmoke;
            fieldTitulo.Dock = DockStyle.Fill;
            fieldTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            fieldTitulo.ForeColor = Color.Black;
            fieldTitulo.ImeMode = ImeMode.NoControl;
            fieldTitulo.Location = new Point(0, 0);
            fieldTitulo.Margin = new Padding(0);
            fieldTitulo.Name = "fieldTitulo";
            fieldTitulo.Size = new Size(160, 50);
            fieldTitulo.TabIndex = 4;
            fieldTitulo.Text = "Finanzas";
            fieldTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelRelleno
            // 
            panelRelleno.BackColor = Color.WhiteSmoke;
            panelRelleno.Controls.Add(btnCajas);
            panelRelleno.Controls.Add(btnCuentasBancarias);
            panelRelleno.Dock = DockStyle.Fill;
            panelRelleno.Font = new Font("Segoe UI", 11.25F);
            panelRelleno.Location = new Point(160, 0);
            panelRelleno.Margin = new Padding(0);
            panelRelleno.Name = "panelRelleno";
            panelRelleno.Size = new Size(834, 50);
            panelRelleno.TabIndex = 0;
            // 
            // btnCuentasBancarias
            // 
            btnCuentasBancarias.Animated = true;
            btnCuentasBancarias.BackColor = Color.WhiteSmoke;
            btnCuentasBancarias.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnCuentasBancarias.CheckedState.FillColor = Color.WhiteSmoke;
            btnCuentasBancarias.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnCuentasBancarias.CustomImages.CheckedImage = (Image) resources.GetObject("resource.CheckedImage");
            btnCuentasBancarias.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnCuentasBancarias.CustomImages.ImageOffset = new Point(0, 32);
            btnCuentasBancarias.CustomImages.ImageSize = new Size(131, 8);
            btnCuentasBancarias.CustomizableEdges = customizableEdges5;
            btnCuentasBancarias.Dock = DockStyle.Left;
            btnCuentasBancarias.FillColor = Color.WhiteSmoke;
            btnCuentasBancarias.Font = new Font("Segoe UI", 11.25F);
            btnCuentasBancarias.ForeColor = Color.Black;
            btnCuentasBancarias.Location = new Point(0, 0);
            btnCuentasBancarias.Margin = new Padding(0);
            btnCuentasBancarias.Name = "btnCuentasBancarias";
            btnCuentasBancarias.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnCuentasBancarias.Size = new Size(160, 50);
            btnCuentasBancarias.TabIndex = 10;
            btnCuentasBancarias.Text = "Cuentas bancarias";
            // 
            // btnCajas
            // 
            btnCajas.Animated = true;
            btnCajas.BackColor = Color.WhiteSmoke;
            btnCajas.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnCajas.CheckedState.FillColor = Color.WhiteSmoke;
            btnCajas.CheckedState.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnCajas.CustomImages.CheckedImage = (Image) resources.GetObject("resource.CheckedImage1");
            btnCajas.CustomImages.ImageAlign = HorizontalAlignment.Center;
            btnCajas.CustomImages.ImageOffset = new Point(0, 32);
            btnCajas.CustomImages.ImageSize = new Size(131, 8);
            btnCajas.CustomizableEdges = customizableEdges7;
            btnCajas.Dock = DockStyle.Left;
            btnCajas.FillColor = Color.WhiteSmoke;
            btnCajas.Font = new Font("Segoe UI", 11.25F);
            btnCajas.ForeColor = Color.Black;
            btnCajas.Location = new Point(160, 0);
            btnCajas.Margin = new Padding(0);
            btnCajas.Name = "btnCajas";
            btnCajas.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnCajas.Size = new Size(160, 50);
            btnCajas.TabIndex = 11;
            btnCajas.Text = "Cajas";
            // 
            // VistaMenuFinanzas
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(  245,   245,   245);
            ClientSize = new Size(994, 50);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 11.25F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaMenuFinanzas";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaMenu";
            layoutBase.ResumeLayout(false);
            layoutDistribucion.ResumeLayout(false);
            panelRelleno.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutDistribucion;
        private Panel panelRelleno;
        private Label fieldTitulo;
        private Guna2Button btnCuentasBancarias;
        private Guna2Button btnCajas;
    }
}