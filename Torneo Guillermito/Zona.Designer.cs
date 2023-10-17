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
            this.dgvCategoria.Location = new System.Drawing.Point(183, 42);
            this.dgvCategoria.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvCategoria.MultiSelect = false;
            this.dgvCategoria.Name = "dgvCategoria";
            this.dgvCategoria.ReadOnly = true;
            this.dgvCategoria.RowHeadersVisible = false;
            this.dgvCategoria.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategoria.Size = new System.Drawing.Size(131, 394);
            this.dgvCategoria.TabIndex = 1;
            this.dgvCategoria.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategoria_CellClick);
            // 
            // gbAgregarClub
            // 
            this.gbAgregarClub.BackColor = System.Drawing.Color.LemonChiffon;
            this.gbAgregarClub.Controls.Add(this.btEliminarCategoria);
            this.gbAgregarClub.Controls.Add(this.tbModificarCategoria);
            this.gbAgregarClub.Controls.Add(this.btModificarCategoria);
            this.gbAgregarClub.Controls.Add(this.tbAgregarCategoria);
            this.gbAgregarClub.Controls.Add(this.btAgregarCategoria);
            this.gbAgregarClub.Controls.Add(this.dgvCategoria);
            this.gbAgregarClub.Location = new System.Drawing.Point(36, 31);
            this.gbAgregarClub.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbAgregarClub.Name = "gbAgregarClub";
            this.gbAgregarClub.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbAgregarClub.Size = new System.Drawing.Size(345, 538);
            this.gbAgregarClub.TabIndex = 23;
            this.gbAgregarClub.TabStop = false;
            this.gbAgregarClub.Text = "CATEGORIAS";
            // 
            // btEliminarCategoria
            // 
            this.btEliminarCategoria.Location = new System.Drawing.Point(183, 459);
            this.btEliminarCategoria.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btEliminarCategoria.Name = "btEliminarCategoria";
            this.btEliminarCategoria.Size = new System.Drawing.Size(131, 41);
            this.btEliminarCategoria.TabIndex = 20;
            this.btEliminarCategoria.Text = "Eliminar categoría";
            this.btEliminarCategoria.UseVisualStyleBackColor = true;
            // 
            // tbModificarCategoria
            // 
            this.tbModificarCategoria.Enabled = false;
            this.tbModificarCategoria.Location = new System.Drawing.Point(33, 322);
            this.tbModificarCategoria.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbModificarCategoria.Name = "tbModificarCategoria";
            this.tbModificarCategoria.Size = new System.Drawing.Size(121, 21);
            this.tbModificarCategoria.TabIndex = 19;
            // 
            // btModificarCategoria
            // 
            this.btModificarCategoria.Enabled = false;
            this.btModificarCategoria.Location = new System.Drawing.Point(33, 366);
            this.btModificarCategoria.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btModificarCategoria.Name = "btModificarCategoria";
            this.btModificarCategoria.Size = new System.Drawing.Size(121, 41);
            this.btModificarCategoria.TabIndex = 18;
            this.btModificarCategoria.Text = "Modificar categ.";
            this.btModificarCategoria.UseVisualStyleBackColor = true;
            this.btModificarCategoria.Click += new System.EventHandler(this.btModificarCategoria_Click);
            // 
            // tbAgregarCategoria
            // 
            this.tbAgregarCategoria.Location = new System.Drawing.Point(33, 84);
            this.tbAgregarCategoria.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbAgregarCategoria.Name = "tbAgregarCategoria";
            this.tbAgregarCategoria.Size = new System.Drawing.Size(121, 21);
            this.tbAgregarCategoria.TabIndex = 17;
            // 
            // btAgregarCategoria
            // 
            this.btAgregarCategoria.Location = new System.Drawing.Point(33, 127);
            this.btAgregarCategoria.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btAgregarCategoria.Name = "btAgregarCategoria";
            this.btAgregarCategoria.Size = new System.Drawing.Size(121, 41);
            this.btAgregarCategoria.TabIndex = 14;
            this.btAgregarCategoria.Text = "Agregar categ.";
            this.btAgregarCategoria.UseVisualStyleBackColor = true;
            this.btAgregarCategoria.Click += new System.EventHandler(this.btAgregarCategoria_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LemonChiffon;
            this.groupBox1.Controls.Add(this.comboZona3);
            this.groupBox1.Controls.Add(this.comboZona4);
            this.groupBox1.Controls.Add(this.btMoficiarZona);
            this.groupBox1.Controls.Add(this.comboZona1);
            this.groupBox1.Controls.Add(this.comboZona2);
            this.groupBox1.Controls.Add(this.btEliminarZona);
            this.groupBox1.Controls.Add(this.btAgregarZona);
            this.groupBox1.Controls.Add(this.dgvZona);
            this.groupBox1.Location = new System.Drawing.Point(476, 31);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(559, 538);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ZONAS";
            // 
            // comboZona3
            // 
            this.comboZona3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboZona3.Enabled = false;
            this.comboZona3.FormattingEnabled = true;
            this.comboZona3.Location = new System.Drawing.Point(29, 322);
            this.comboZona3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboZona3.Name = "comboZona3";
            this.comboZona3.Size = new System.Drawing.Size(121, 24);
            this.comboZona3.TabIndex = 25;
            // 
            // comboZona4
            // 
            this.comboZona4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboZona4.Enabled = false;
            this.comboZona4.FormattingEnabled = true;
            this.comboZona4.Location = new System.Drawing.Point(158, 322);
            this.comboZona4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboZona4.Name = "comboZona4";
            this.comboZona4.Size = new System.Drawing.Size(121, 24);
            this.comboZona4.TabIndex = 24;
            // 
            // btMoficiarZona
            // 
            this.btMoficiarZona.Enabled = false;
            this.btMoficiarZona.Location = new System.Drawing.Point(95, 373);
            this.btMoficiarZona.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btMoficiarZona.Name = "btMoficiarZona";
            this.btMoficiarZona.Size = new System.Drawing.Size(121, 41);
            this.btMoficiarZona.TabIndex = 23;
            this.btMoficiarZona.Text = "Modificar zona";
            this.btMoficiarZona.UseVisualStyleBackColor = true;
            // 
            // comboZona1
            // 
            this.comboZona1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboZona1.FormattingEnabled = true;
            this.comboZona1.Location = new System.Drawing.Point(29, 84);
            this.comboZona1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboZona1.Name = "comboZona1";
            this.comboZona1.Size = new System.Drawing.Size(121, 24);
            this.comboZona1.TabIndex = 22;
            // 
            // comboZona2
            // 
            this.comboZona2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboZona2.FormattingEnabled = true;
            this.comboZona2.Location = new System.Drawing.Point(158, 84);
            this.comboZona2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboZona2.Name = "comboZona2";
            this.comboZona2.Size = new System.Drawing.Size(121, 24);
            this.comboZona2.TabIndex = 21;
            // 
            // btEliminarZona
            // 
            this.btEliminarZona.Location = new System.Drawing.Point(342, 459);
            this.btEliminarZona.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btEliminarZona.Name = "btEliminarZona";
            this.btEliminarZona.Size = new System.Drawing.Size(157, 41);
            this.btEliminarZona.TabIndex = 20;
            this.btEliminarZona.Text = "Eliminar zona";
            this.btEliminarZona.UseVisualStyleBackColor = true;
            // 
            // btAgregarZona
            // 
            this.btAgregarZona.Location = new System.Drawing.Point(93, 129);
            this.btAgregarZona.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btAgregarZona.Name = "btAgregarZona";
            this.btAgregarZona.Size = new System.Drawing.Size(121, 41);
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
            this.dgvZona.Location = new System.Drawing.Point(303, 42);
            this.dgvZona.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvZona.MultiSelect = false;
            this.dgvZona.Name = "dgvZona";
            this.dgvZona.ReadOnly = true;
            this.dgvZona.RowHeadersVisible = false;
            this.dgvZona.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvZona.Size = new System.Drawing.Size(220, 394);
            this.dgvZona.TabIndex = 1;
            this.dgvZona.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvZona_CellClick);
            // 
            // Zona
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(1059, 604);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbAgregarClub);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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