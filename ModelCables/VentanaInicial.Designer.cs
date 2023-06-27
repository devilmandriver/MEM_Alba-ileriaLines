using System.Runtime.CompilerServices;

namespace MEM_AlbañileriaLines
{
    partial class VentanaInicial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonCrearRegiones = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxSelector = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.boolCol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.wallType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboBoxMaterialDescripción = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxMaterialParam = new System.Windows.Forms.ComboBox();
            this.Parametros = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.labelparam = new System.Windows.Forms.Label();
            this.labelIntExt = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBoxParameter = new System.Windows.Forms.GroupBox();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.Parametros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCrearRegiones
            // 
            this.buttonCrearRegiones.Location = new System.Drawing.Point(458, 529);
            this.buttonCrearRegiones.Name = "buttonCrearRegiones";
            this.buttonCrearRegiones.Size = new System.Drawing.Size(109, 23);
            this.buttonCrearRegiones.TabIndex = 2;
            this.buttonCrearRegiones.Text = "Crear Memoria";
            this.buttonCrearRegiones.UseVisualStyleBackColor = true;
            this.buttonCrearRegiones.Click += new System.EventHandler(this.buttonCrearRegiones_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxSelector);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.dataGridView1);
            this.groupBox4.Controls.Add(this.comboBoxMaterialDescripción);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.comboBoxMaterialParam);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(304, 558);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            // 
            // textBoxSelector
            // 
            this.textBoxSelector.Location = new System.Drawing.Point(3, 113);
            this.textBoxSelector.Name = "textBoxSelector";
            this.textBoxSelector.Size = new System.Drawing.Size(298, 20);
            this.textBoxSelector.TabIndex = 30;
            this.textBoxSelector.TextChanged += new System.EventHandler(this.textBoxSelector_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Location = new System.Drawing.Point(3, 50);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(83, 13);
            this.label14.TabIndex = 36;
            this.label14.Text = "Material Código:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.boolCol,
            this.wallType});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(3, 139);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(298, 416);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // boolCol
            // 
            this.boolCol.HeaderText = "Y/N";
            this.boolCol.Name = "boolCol";
            this.boolCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.boolCol.Width = 20;
            // 
            // wallType
            // 
            this.wallType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.wallType.HeaderText = "Tipo de muro";
            this.wallType.Name = "wallType";
            // 
            // comboBoxMaterialDescripción
            // 
            this.comboBoxMaterialDescripción.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxMaterialDescripción.FormattingEnabled = true;
            this.comboBoxMaterialDescripción.Location = new System.Drawing.Point(3, 29);
            this.comboBoxMaterialDescripción.Name = "comboBoxMaterialDescripción";
            this.comboBoxMaterialDescripción.Size = new System.Drawing.Size(298, 21);
            this.comboBoxMaterialDescripción.TabIndex = 35;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(3, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(106, 13);
            this.label13.TabIndex = 34;
            this.label13.Tag = "fguhfsdfsfsdfsdrf";
            this.label13.Text = "Material Descripción:";
            // 
            // comboBoxMaterialParam
            // 
            this.comboBoxMaterialParam.FormattingEnabled = true;
            this.comboBoxMaterialParam.Location = new System.Drawing.Point(0, 66);
            this.comboBoxMaterialParam.Name = "comboBoxMaterialParam";
            this.comboBoxMaterialParam.Size = new System.Drawing.Size(301, 21);
            this.comboBoxMaterialParam.TabIndex = 33;
            // 
            // Parametros
            // 
            this.Parametros.Controls.Add(this.pictureBox2);
            this.Parametros.Controls.Add(this.pictureBox1);
            this.Parametros.Controls.Add(this.button1);
            this.Parametros.Controls.Add(this.buttonCrearRegiones);
            this.Parametros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Parametros.Location = new System.Drawing.Point(304, 0);
            this.Parametros.Name = "Parametros";
            this.Parametros.Size = new System.Drawing.Size(573, 558);
            this.Parametros.TabIndex = 23;
            this.Parametros.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(6, 284);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(366, 268);
            this.pictureBox2.TabIndex = 30;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(6, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(366, 268);
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(377, 529);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "Acotar Muros";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // labelparam
            // 
            this.labelparam.AutoSize = true;
            this.labelparam.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelparam.Location = new System.Drawing.Point(86, 34);
            this.labelparam.Name = "labelparam";
            this.labelparam.Size = new System.Drawing.Size(213, 34);
            this.labelparam.TabIndex = 31;
            this.labelparam.Text = "Muros agrupados por parámetro\r\n y ordenados por anchura";
            this.labelparam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelIntExt
            // 
            this.labelIntExt.AutoSize = true;
            this.labelIntExt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIntExt.Location = new System.Drawing.Point(14, 34);
            this.labelIntExt.Name = "labelIntExt";
            this.labelIntExt.Size = new System.Drawing.Size(373, 34);
            this.labelIntExt.TabIndex = 30;
            this.labelIntExt.Text = "Muros agrupados por Función (Int / Ext)\r\n y ordenados por listado de materiales y" +
    " anchura de muro";
            this.labelIntExt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(0, 0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 0;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(0, 0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 0;
            // 
            // groupBoxParameter
            // 
            this.groupBoxParameter.Location = new System.Drawing.Point(0, 0);
            this.groupBoxParameter.Name = "groupBoxParameter";
            this.groupBoxParameter.Size = new System.Drawing.Size(200, 100);
            this.groupBoxParameter.TabIndex = 0;
            this.groupBoxParameter.TabStop = false;
            // 
            // VentanaInicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(877, 558);
            this.Controls.Add(this.Parametros);
            this.Controls.Add(this.groupBox4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "VentanaInicial";
            this.Text = "Memoria de Albañileria Lines";
            this.Load += new System.EventHandler(this.VentanaInicial_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.Parametros.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonCrearRegiones;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox Parametros;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBoxParameter;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label labelIntExt;
        private System.Windows.Forms.Label labelparam;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxSelector;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn boolCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn wallType;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBoxMaterialDescripción;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBoxMaterialParam;
        public System.Windows.Forms.PictureBox pictureBox2;
    }
}