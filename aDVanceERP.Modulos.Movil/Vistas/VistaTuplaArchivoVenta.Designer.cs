using Guna.UI2.WinForms;

using System.ComponentModel;

namespace aDVanceERP.Modulos.Movil.Vistas {
    partial class VistaTuplaArchivoVenta {
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            layoutBase = new TableLayoutPanel();
            layoutVista = new TableLayoutPanel();
            fieldTamannoArchivo = new Label();
            fieldFecha = new Label();
            fieldNombreArchivo = new Label();
            iconoArchivo = new PictureBox();
            btnImportar = new Guna2Button();
            layoutBase.SuspendLayout();
            layoutVista.SuspendLayout();
            ((ISupportInitialize) iconoArchivo).BeginInit();
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
            layoutBase.Size = new Size(725, 42);
            layoutBase.TabIndex = 1;
            // 
            // layoutVista
            // 
            layoutVista.BackColor = Color.White;
            layoutVista.ColumnCount = 5;
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 32F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutVista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            layoutVista.Controls.Add(btnImportar, 4, 0);
            layoutVista.Controls.Add(fieldTamannoArchivo, 3, 0);
            layoutVista.Controls.Add(fieldFecha, 2, 0);
            layoutVista.Controls.Add(fieldNombreArchivo, 1, 0);
            layoutVista.Controls.Add(iconoArchivo, 0, 0);
            layoutVista.Dock = DockStyle.Fill;
            layoutVista.Location = new Point(0, 0);
            layoutVista.Margin = new Padding(0, 0, 0, 1);
            layoutVista.Name = "layoutVista";
            layoutVista.RowCount = 1;
            layoutVista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutVista.Size = new Size(725, 41);
            layoutVista.TabIndex = 19;
            // 
            // fieldTamannoArchivo
            // 
            fieldTamannoArchivo.Dock = DockStyle.Fill;
            fieldTamannoArchivo.Font = new Font("Segoe UI", 11.25F);
            fieldTamannoArchivo.ForeColor = Color.DimGray;
            fieldTamannoArchivo.ImeMode = ImeMode.NoControl;
            fieldTamannoArchivo.Location = new Point(480, 1);
            fieldTamannoArchivo.Margin = new Padding(5, 1, 1, 1);
            fieldTamannoArchivo.Name = "fieldTamannoArchivo";
            fieldTamannoArchivo.Size = new Size(104, 39);
            fieldTamannoArchivo.TabIndex = 36;
            fieldTamannoArchivo.Text = "tamaño";
            fieldTamannoArchivo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fieldFecha
            // 
            fieldFecha.Dock = DockStyle.Fill;
            fieldFecha.Font = new Font("Segoe UI", 11.25F);
            fieldFecha.ForeColor = Color.DimGray;
            fieldFecha.ImeMode = ImeMode.NoControl;
            fieldFecha.Location = new Point(360, 1);
            fieldFecha.Margin = new Padding(5, 1, 1, 1);
            fieldFecha.Name = "fieldFecha";
            fieldFecha.Size = new Size(114, 39);
            fieldFecha.TabIndex = 35;
            fieldFecha.Text = "fecha";
            fieldFecha.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fieldNombreArchivo
            // 
            fieldNombreArchivo.AutoEllipsis = true;
            fieldNombreArchivo.Dock = DockStyle.Fill;
            fieldNombreArchivo.Font = new Font("Segoe UI", 11.25F);
            fieldNombreArchivo.ForeColor = Color.DimGray;
            fieldNombreArchivo.ImeMode = ImeMode.NoControl;
            fieldNombreArchivo.Location = new Point(33, 1);
            fieldNombreArchivo.Margin = new Padding(1);
            fieldNombreArchivo.Name = "fieldNombreArchivo";
            fieldNombreArchivo.Size = new Size(321, 39);
            fieldNombreArchivo.TabIndex = 17;
            fieldNombreArchivo.Text = "nombreArchivo";
            fieldNombreArchivo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // iconoArchivo
            // 
            iconoArchivo.BackgroundImage = Properties.Resources.fileG_20px;
            iconoArchivo.BackgroundImageLayout = ImageLayout.Center;
            iconoArchivo.Dock = DockStyle.Fill;
            iconoArchivo.Location = new Point(3, 3);
            iconoArchivo.Name = "iconoArchivo";
            iconoArchivo.Size = new Size(26, 35);
            iconoArchivo.TabIndex = 37;
            iconoArchivo.TabStop = false;
            // 
            // btnImportar
            // 
            btnImportar.Animated = true;
            btnImportar.AutoRoundedCorners = true;
            btnImportar.BorderColor = Color.Gainsboro;
            btnImportar.BorderRadius = 14;
            btnImportar.BorderThickness = 1;
            btnImportar.CustomizableEdges = customizableEdges3;
            btnImportar.Dock = DockStyle.Fill;
            btnImportar.FillColor = Color.White;
            btnImportar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnImportar.ForeColor = Color.Gainsboro;
            btnImportar.HoverState.BorderColor = Color.PeachPuff;
            btnImportar.HoverState.FillColor = Color.PeachPuff;
            btnImportar.HoverState.ForeColor = Color.Black;
            btnImportar.Location = new Point(590, 5);
            btnImportar.Margin = new Padding(5);
            btnImportar.Name = "btnImportar";
            btnImportar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnImportar.Size = new Size(130, 31);
            btnImportar.TabIndex = 38;
            btnImportar.Text = "Importar";
            // 
            // VistaTuplaArchivoVenta
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(725, 42);
            Controls.Add(layoutBase);
            Font = new Font("Segoe UI", 10.8F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VistaTuplaArchivoVenta";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "VistaTuplaMovil";
            layoutBase.ResumeLayout(false);
            layoutVista.ResumeLayout(false);
            ((ISupportInitialize) iconoArchivo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna2BorderlessForm formatoBase;
        private TableLayoutPanel layoutBase;
        private TableLayoutPanel layoutVista;
        private Label fieldNombreArchivo;
        private Label fieldFecha;
        private Label fieldTamannoArchivo;
        private PictureBox iconoArchivo;
        private Guna2Button btnImportar;
    }
}