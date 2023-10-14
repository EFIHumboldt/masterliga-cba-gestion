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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbAdvertencia = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipo2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboEquipo1
            // 
            this.comboEquipo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEquipo1.FormattingEnabled = true;
            this.comboEquipo1.Location = new System.Drawing.Point(97, 73);
            this.comboEquipo1.Name = "comboEquipo1";
            this.comboEquipo1.Size = new System.Drawing.Size(80, 24);
            this.comboEquipo1.TabIndex = 26;
            this.comboEquipo1.SelectedIndexChanged += new System.EventHandler(this.comboZona1_SelectedIndexChanged);
            // 
            // dgvEquipo1
            // 
            this.dgvEquipo1.AllowUserToAddRows = false;
            this.dgvEquipo1.AllowUserToDeleteRows = false;
            this.dgvEquipo1.AllowUserToResizeRows = false;
            this.dgvEquipo1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEquipo1.Location = new System.Drawing.Point(12, 72);
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
            this.tbEquipos2.Location = new System.Drawing.Point(97, 28);
            this.tbEquipos2.Name = "tbEquipos2";
            this.tbEquipos2.Size = new System.Drawing.Size(288, 21);
            this.tbEquipos2.TabIndex = 29;
            // 
            // dgvEquipo2
            // 
            this.dgvEquipo2.AllowUserToAddRows = false;
            this.dgvEquipo2.AllowUserToDeleteRows = false;
            this.dgvEquipo2.AllowUserToResizeRows = false;
            this.dgvEquipo2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEquipo2.Location = new System.Drawing.Point(466, 44);
            this.dgvEquipo2.MultiSelect = false;
            this.dgvEquipo2.Name = "dgvEquipo2";
            this.dgvEquipo2.ReadOnly = true;
            this.dgvEquipo2.RowHeadersVisible = false;
            this.dgvEquipo2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEquipo2.Size = new System.Drawing.Size(392, 566);
            this.dgvEquipo2.TabIndex = 33;
            this.dgvEquipo2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEquipo2_CellClick);
            // 
            // comboEquipo2
            // 
            this.comboEquipo2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEquipo2.FormattingEnabled = true;
            this.comboEquipo2.Location = new System.Drawing.Point(299, 73);
            this.comboEquipo2.Name = "comboEquipo2";
            this.comboEquipo2.Size = new System.Drawing.Size(86, 24);
            this.comboEquipo2.TabIndex = 25;
            this.comboEquipo2.SelectedIndexChanged += new System.EventHandler(this.comboZona2_SelectedIndexChanged);
            // 
            // tbEquipos1
            // 
            this.tbEquipos1.Location = new System.Drawing.Point(97, 30);
            this.tbEquipos1.Name = "tbEquipos1";
            this.tbEquipos1.Size = new System.Drawing.Size(288, 21);
            this.tbEquipos1.TabIndex = 34;
            this.tbEquipos1.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // btEliminarEquipo
            // 
            this.btEliminarEquipo.Enabled = false;
            this.btEliminarEquipo.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btEliminarEquipo.Location = new System.Drawing.Point(629, 620);
            this.btEliminarEquipo.Name = "btEliminarEquipo";
            this.btEliminarEquipo.Size = new System.Drawing.Size(116, 41);
            this.btEliminarEquipo.TabIndex = 35;
            this.btEliminarEquipo.Text = "Eliminar equipo";
            this.btEliminarEquipo.UseVisualStyleBackColor = true;
            this.btEliminarEquipo.Click += new System.EventHandler(this.btEliminarEquipo_Click);
            // 
            // btEquipos1
            // 
            this.btEquipos1.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btEquipos1.Location = new System.Drawing.Point(283, 124);
            this.btEquipos1.Name = "btEquipos1";
            this.btEquipos1.Size = new System.Drawing.Size(103, 34);
            this.btEquipos1.TabIndex = 36;
            this.btEquipos1.Text = "Agregar equipo";
            this.btEquipos1.UseVisualStyleBackColor = true;
            this.btEquipos1.Click += new System.EventHandler(this.btEquipos1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LemonChiffon;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dgvEquipo1);
            this.groupBox1.Controls.Add(this.tbEquipos1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(407, 390);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LISTAR Y FILTRAR CLUB";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LemonChiffon;
            this.groupBox2.Controls.Add(this.lbAdvertencia);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btEquipos1);
            this.groupBox2.Controls.Add(this.tbEquipos2);
            this.groupBox2.Controls.Add(this.comboEquipo1);
            this.groupBox2.Controls.Add(this.comboEquipo2);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 440);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(407, 170);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CREAR EQUIPO";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(570, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 21);
            this.label3.TabIndex = 39;
            this.label3.Text = "Lista de equipos creados";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 35;
            this.label1.Text = "Nombre club:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 36;
            this.label2.Text = "Nombre club:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 16);
            this.label4.TabIndex = 37;
            this.label4.Text = "Categoria:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(256, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 16);
            this.label5.TabIndex = 38;
            this.label5.Text = "Zona:";
            // 
            // lbAdvertencia
            // 
            this.lbAdvertencia.AutoSize = true;
            this.lbAdvertencia.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAdvertencia.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbAdvertencia.Location = new System.Drawing.Point(34, 121);
            this.lbAdvertencia.Name = "lbAdvertencia";
            this.lbAdvertencia.Size = new System.Drawing.Size(223, 34);
            this.lbAdvertencia.TabIndex = 40;
            this.lbAdvertencia.Text = "(La zona y la categoria deben estar\r\npreviamente creadas)";
            this.lbAdvertencia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Equipo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(878, 675);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btEliminarEquipo);
            this.Controls.Add(this.dgvEquipo2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "Equipo";
            this.Text = "Equipos Torneo Guillermito";
            this.Load += new System.EventHandler(this.Equipo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipo2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbAdvertencia;
    }
}