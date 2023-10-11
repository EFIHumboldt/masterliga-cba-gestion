namespace Torneo_Guillermito
{
    partial class Zona
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
            this.dgvCategoria = new System.Windows.Forms.DataGridView();
            this.gbAgregarClub = new System.Windows.Forms.GroupBox();
            this.btEliminarCategoria = new System.Windows.Forms.Button();
            this.tbModificarCategoria = new System.Windows.Forms.TextBox();
            this.btModificarCategoria = new System.Windows.Forms.Button();
            this.tbAgregarCategoria = new System.Windows.Forms.TextBox();
            this.btAgregarCategoria = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboZona3 = new System.Windows.Forms.ComboBox();
            this.comboZona4 = new System.Windows.Forms.ComboBox();
            this.btMoficiarZona = new System.Windows.Forms.Button();
            this.comboZona1 = new System.Windows.Forms.ComboBox();
            this.comboZona2 = new System.Windows.Forms.ComboBox();
            this.btEliminarZona = new System.Windows.Forms.Button();
            this.btAgregarZona = new System.Windows.Forms.Button();
            this.dgvZona = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategoria)).BeginInit();
            this.gbAgregarClub.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvZona)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCategoria
            // 
            this.dgvCategoria.AllowUserToAddRows = false;
            this.dgvCategoria.AllowUserToDeleteRows = false;
            this.dgvCategoria.AllowUserToResizeRows = false;
            this.dgvCategoria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategoria.Location = new System.Drawing.Point(157, 34);
            this.dgvCategoria.MultiSelect = false;
            this.dgvCategoria.Name = "dgvCategoria";
            this.dgvCategoria.ReadOnly = true;
            this.dgvCategoria.RowHeadersVisible = false;
            this.dgvCategoria.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategoria.Size = new System.Drawing.Size(112, 320);
            this.dgvCategoria.TabIndex = 1;
            this.dgvCategoria.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategoria_CellClick);
            // 
            // gbAgregarClub
            // 
            this.gbAgregarClub.Controls.Add(this.btEliminarCategoria);
            this.gbAgregarClub.Controls.Add(this.tbModificarCategoria);
            this.gbAgregarClub.Controls.Add(this.btModificarCategoria);
            this.gbAgregarClub.Controls.Add(this.tbAgregarCategoria);
            this.gbAgregarClub.Controls.Add(this.btAgregarCategoria);
            this.gbAgregarClub.Controls.Add(this.dgvCategoria);
            this.gbAgregarClub.Location = new System.Drawing.Point(31, 25);
            this.gbAgregarClub.Name = "gbAgregarClub";
            this.gbAgregarClub.Size = new System.Drawing.Size(296, 437);
            this.gbAgregarClub.TabIndex = 23;
            this.gbAgregarClub.TabStop = false;
            this.gbAgregarClub.Text = "CATEGORIAS";
            // 
            // btEliminarCategoria
            // 
            this.btEliminarCategoria.Location = new System.Drawing.Point(157, 373);
            this.btEliminarCategoria.Name = "btEliminarCategoria";
            this.btEliminarCategoria.Size = new System.Drawing.Size(112, 33);
            this.btEliminarCategoria.TabIndex = 20;
            this.btEliminarCategoria.Text = "Eliminar";
            this.btEliminarCategoria.UseVisualStyleBackColor = true;
            // 
            // tbModificarCategoria
            // 
            this.tbModificarCategoria.Enabled = false;
            this.tbModificarCategoria.Location = new System.Drawing.Point(28, 262);
            this.tbModificarCategoria.Name = "tbModificarCategoria";
            this.tbModificarCategoria.Size = new System.Drawing.Size(104, 20);
            this.tbModificarCategoria.TabIndex = 19;
            // 
            // btModificarCategoria
            // 
            this.btModificarCategoria.Enabled = false;
            this.btModificarCategoria.Location = new System.Drawing.Point(28, 297);
            this.btModificarCategoria.Name = "btModificarCategoria";
            this.btModificarCategoria.Size = new System.Drawing.Size(104, 33);
            this.btModificarCategoria.TabIndex = 18;
            this.btModificarCategoria.Text = "Modificar categ.";
            this.btModificarCategoria.UseVisualStyleBackColor = true;
            this.btModificarCategoria.Click += new System.EventHandler(this.btModificarCategoria_Click);
            // 
            // tbAgregarCategoria
            // 
            this.tbAgregarCategoria.Location = new System.Drawing.Point(28, 68);
            this.tbAgregarCategoria.Name = "tbAgregarCategoria";
            this.tbAgregarCategoria.Size = new System.Drawing.Size(104, 20);
            this.tbAgregarCategoria.TabIndex = 17;
            // 
            // btAgregarCategoria
            // 
            this.btAgregarCategoria.Location = new System.Drawing.Point(28, 103);
            this.btAgregarCategoria.Name = "btAgregarCategoria";
            this.btAgregarCategoria.Size = new System.Drawing.Size(104, 33);
            this.btAgregarCategoria.TabIndex = 14;
            this.btAgregarCategoria.Text = "Agregar categ.";
            this.btAgregarCategoria.UseVisualStyleBackColor = true;
            this.btAgregarCategoria.Click += new System.EventHandler(this.btAgregarCategoria_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboZona3);
            this.groupBox1.Controls.Add(this.comboZona4);
            this.groupBox1.Controls.Add(this.btMoficiarZona);
            this.groupBox1.Controls.Add(this.comboZona1);
            this.groupBox1.Controls.Add(this.comboZona2);
            this.groupBox1.Controls.Add(this.btEliminarZona);
            this.groupBox1.Controls.Add(this.btAgregarZona);
            this.groupBox1.Controls.Add(this.dgvZona);
            this.groupBox1.Location = new System.Drawing.Point(408, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 437);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ZONAS";
            // 
            // comboZona3
            // 
            this.comboZona3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboZona3.Enabled = false;
            this.comboZona3.FormattingEnabled = true;
            this.comboZona3.Location = new System.Drawing.Point(25, 262);
            this.comboZona3.Name = "comboZona3";
            this.comboZona3.Size = new System.Drawing.Size(104, 21);
            this.comboZona3.TabIndex = 25;
            // 
            // comboZona4
            // 
            this.comboZona4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboZona4.Enabled = false;
            this.comboZona4.FormattingEnabled = true;
            this.comboZona4.Location = new System.Drawing.Point(135, 262);
            this.comboZona4.Name = "comboZona4";
            this.comboZona4.Size = new System.Drawing.Size(104, 21);
            this.comboZona4.TabIndex = 24;
            // 
            // btMoficiarZona
            // 
            this.btMoficiarZona.Enabled = false;
            this.btMoficiarZona.Location = new System.Drawing.Point(80, 299);
            this.btMoficiarZona.Name = "btMoficiarZona";
            this.btMoficiarZona.Size = new System.Drawing.Size(104, 33);
            this.btMoficiarZona.TabIndex = 23;
            this.btMoficiarZona.Text = "Modificar zona";
            this.btMoficiarZona.UseVisualStyleBackColor = true;
            // 
            // comboZona1
            // 
            this.comboZona1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboZona1.FormattingEnabled = true;
            this.comboZona1.Location = new System.Drawing.Point(25, 68);
            this.comboZona1.Name = "comboZona1";
            this.comboZona1.Size = new System.Drawing.Size(104, 21);
            this.comboZona1.TabIndex = 22;
            // 
            // comboZona2
            // 
            this.comboZona2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboZona2.FormattingEnabled = true;
            this.comboZona2.Location = new System.Drawing.Point(135, 68);
            this.comboZona2.Name = "comboZona2";
            this.comboZona2.Size = new System.Drawing.Size(104, 21);
            this.comboZona2.TabIndex = 21;
            // 
            // btEliminarZona
            // 
            this.btEliminarZona.Location = new System.Drawing.Point(306, 373);
            this.btEliminarZona.Name = "btEliminarZona";
            this.btEliminarZona.Size = new System.Drawing.Size(104, 33);
            this.btEliminarZona.TabIndex = 20;
            this.btEliminarZona.Text = "Eliminar";
            this.btEliminarZona.UseVisualStyleBackColor = true;
            // 
            // btAgregarZona
            // 
            this.btAgregarZona.Location = new System.Drawing.Point(80, 105);
            this.btAgregarZona.Name = "btAgregarZona";
            this.btAgregarZona.Size = new System.Drawing.Size(104, 33);
            this.btAgregarZona.TabIndex = 14;
            this.btAgregarZona.Text = "Agregar zona";
            this.btAgregarZona.UseVisualStyleBackColor = true;
            this.btAgregarZona.Click += new System.EventHandler(this.btAgregarZona_Click);
            // 
            // dgvZona
            // 
            this.dgvZona.AllowUserToAddRows = false;
            this.dgvZona.AllowUserToDeleteRows = false;
            this.dgvZona.AllowUserToResizeRows = false;
            this.dgvZona.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvZona.Location = new System.Drawing.Point(260, 34);
            this.dgvZona.MultiSelect = false;
            this.dgvZona.Name = "dgvZona";
            this.dgvZona.ReadOnly = true;
            this.dgvZona.RowHeadersVisible = false;
            this.dgvZona.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvZona.Size = new System.Drawing.Size(189, 320);
            this.dgvZona.TabIndex = 1;
            this.dgvZona.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvZona_CellClick);
            // 
            // Zona
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 503);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbAgregarClub);
            this.Name = "Zona";
            this.Text = "Categorias y Zonas Torneo Guillermito";
            this.Load += new System.EventHandler(this.Zona_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategoria)).EndInit();
            this.gbAgregarClub.ResumeLayout(false);
            this.gbAgregarClub.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvZona)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvCategoria;
        private System.Windows.Forms.GroupBox gbAgregarClub;
        private System.Windows.Forms.Button btEliminarCategoria;
        private System.Windows.Forms.TextBox tbModificarCategoria;
        private System.Windows.Forms.Button btModificarCategoria;
        private System.Windows.Forms.TextBox tbAgregarCategoria;
        private System.Windows.Forms.Button btAgregarCategoria;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboZona3;
        private System.Windows.Forms.ComboBox comboZona4;
        private System.Windows.Forms.Button btMoficiarZona;
        private System.Windows.Forms.ComboBox comboZona1;
        private System.Windows.Forms.ComboBox comboZona2;
        private System.Windows.Forms.Button btEliminarZona;
        private System.Windows.Forms.Button btAgregarZona;
        private System.Windows.Forms.DataGridView dgvZona;
    }
}