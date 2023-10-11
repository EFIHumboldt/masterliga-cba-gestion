namespace Torneo_Guillermito
{
    partial class Equipo
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
            this.comboEquipo1 = new System.Windows.Forms.ComboBox();
            this.dgvEquipo1 = new System.Windows.Forms.DataGridView();
            this.tbEquipos2 = new System.Windows.Forms.TextBox();
            this.dgvEquipo2 = new System.Windows.Forms.DataGridView();
            this.comboEquipo2 = new System.Windows.Forms.ComboBox();
            this.tbEquipos1 = new System.Windows.Forms.TextBox();
            this.btEliminarEquipo = new System.Windows.Forms.Button();
            this.btEquipos1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipo2)).BeginInit();
            this.SuspendLayout();
            // 
            // comboEquipo1
            // 
            this.comboEquipo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEquipo1.FormattingEnabled = true;
            this.comboEquipo1.Location = new System.Drawing.Point(264, 385);
            this.comboEquipo1.Name = "comboEquipo1";
            this.comboEquipo1.Size = new System.Drawing.Size(80, 21);
            this.comboEquipo1.TabIndex = 26;
            this.comboEquipo1.SelectedIndexChanged += new System.EventHandler(this.comboZona1_SelectedIndexChanged);
            // 
            // dgvEquipo1
            // 
            this.dgvEquipo1.AllowUserToAddRows = false;
            this.dgvEquipo1.AllowUserToDeleteRows = false;
            this.dgvEquipo1.AllowUserToResizeRows = false;
            this.dgvEquipo1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEquipo1.Location = new System.Drawing.Point(24, 70);
            this.dgvEquipo1.MultiSelect = false;
            this.dgvEquipo1.Name = "dgvEquipo1";
            this.dgvEquipo1.ReadOnly = true;
            this.dgvEquipo1.RowHeadersVisible = false;
            this.dgvEquipo1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEquipo1.Size = new System.Drawing.Size(373, 299);
            this.dgvEquipo1.TabIndex = 28;
            this.dgvEquipo1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEquipo1_CellContentClick);
            // 
            // tbEquipos2
            // 
            this.tbEquipos2.Enabled = false;
            this.tbEquipos2.Location = new System.Drawing.Point(24, 385);
            this.tbEquipos2.Name = "tbEquipos2";
            this.tbEquipos2.Size = new System.Drawing.Size(234, 20);
            this.tbEquipos2.TabIndex = 29;
            // 
            // dgvEquipo2
            // 
            this.dgvEquipo2.AllowUserToAddRows = false;
            this.dgvEquipo2.AllowUserToDeleteRows = false;
            this.dgvEquipo2.AllowUserToResizeRows = false;
            this.dgvEquipo2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEquipo2.Location = new System.Drawing.Point(453, 33);
            this.dgvEquipo2.MultiSelect = false;
            this.dgvEquipo2.Name = "dgvEquipo2";
            this.dgvEquipo2.ReadOnly = true;
            this.dgvEquipo2.RowHeadersVisible = false;
            this.dgvEquipo2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEquipo2.Size = new System.Drawing.Size(392, 354);
            this.dgvEquipo2.TabIndex = 33;
            this.dgvEquipo2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEquipo2_CellClick);
            // 
            // comboEquipo2
            // 
            this.comboEquipo2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEquipo2.FormattingEnabled = true;
            this.comboEquipo2.Location = new System.Drawing.Point(350, 385);
            this.comboEquipo2.Name = "comboEquipo2";
            this.comboEquipo2.Size = new System.Drawing.Size(47, 21);
            this.comboEquipo2.TabIndex = 25;
            this.comboEquipo2.SelectedIndexChanged += new System.EventHandler(this.comboZona2_SelectedIndexChanged);
            // 
            // tbEquipos1
            // 
            this.tbEquipos1.Location = new System.Drawing.Point(94, 33);
            this.tbEquipos1.Name = "tbEquipos1";
            this.tbEquipos1.Size = new System.Drawing.Size(234, 20);
            this.tbEquipos1.TabIndex = 34;
            this.tbEquipos1.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // btEliminarEquipo
            // 
            this.btEliminarEquipo.Enabled = false;
            this.btEliminarEquipo.Location = new System.Drawing.Point(616, 396);
            this.btEliminarEquipo.Name = "btEliminarEquipo";
            this.btEliminarEquipo.Size = new System.Drawing.Size(95, 25);
            this.btEliminarEquipo.TabIndex = 35;
            this.btEliminarEquipo.Text = "Eliminar equipo";
            this.btEliminarEquipo.UseVisualStyleBackColor = true;
            this.btEliminarEquipo.Click += new System.EventHandler(this.btEliminarEquipo_Click);
            // 
            // btEquipos1
            // 
            this.btEquipos1.Location = new System.Drawing.Point(183, 420);
            this.btEquipos1.Name = "btEquipos1";
            this.btEquipos1.Size = new System.Drawing.Size(75, 23);
            this.btEquipos1.TabIndex = 36;
            this.btEquipos1.Text = "Agregar";
            this.btEquipos1.UseVisualStyleBackColor = true;
            this.btEquipos1.Click += new System.EventHandler(this.btEquipos1_Click);
            // 
            // Equipo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 455);
            this.Controls.Add(this.btEquipos1);
            this.Controls.Add(this.btEliminarEquipo);
            this.Controls.Add(this.tbEquipos1);
            this.Controls.Add(this.dgvEquipo2);
            this.Controls.Add(this.tbEquipos2);
            this.Controls.Add(this.dgvEquipo1);
            this.Controls.Add(this.comboEquipo1);
            this.Controls.Add(this.comboEquipo2);
            this.Name = "Equipo";
            this.Text = "Equipos Torneo Guillermito";
            this.Load += new System.EventHandler(this.Equipo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipo2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboEquipo1;
        private System.Windows.Forms.DataGridView dgvEquipo1;
        private System.Windows.Forms.TextBox tbEquipos2;
        private System.Windows.Forms.DataGridView dgvEquipo2;
        private System.Windows.Forms.ComboBox comboEquipo2;
        private System.Windows.Forms.TextBox tbEquipos1;
        private System.Windows.Forms.Button btEliminarEquipo;
        private System.Windows.Forms.Button btEquipos1;
    }
}