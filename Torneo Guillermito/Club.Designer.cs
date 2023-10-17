namespace Torneo_Guillermito
{
    partial class Club
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
            this.btLimpiarCanchas = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbNombreClub1 = new System.Windows.Forms.TextBox();
            this.dgvClub = new System.Windows.Forms.DataGridView();
            this.btAgregarCancha = new System.Windows.Forms.Button();
            this.gbAgregarClub = new System.Windows.Forms.GroupBox();
            this.pbCancha1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbCancha2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNombreClub2 = new System.Windows.Forms.TextBox();
            this.btModificarClub = new System.Windows.Forms.Button();
            this.btEliminarClub = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbAdvertencia = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClub)).BeginInit();
            this.gbAgregarClub.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancha1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancha2)).BeginInit();
            this.SuspendLayout();
            // 
            // btLimpiarCanchas
            // 
            this.btLimpiarCanchas.Location = new System.Drawing.Point(165, 218);
            this.btLimpiarCanchas.Name = "btLimpiarCanchas";
            this.btLimpiarCanchas.Size = new System.Drawing.Size(84, 33);
            this.btLimpiarCanchas.TabIndex = 20;
            this.btLimpiarCanchas.Text = "Limpiar";
            this.btLimpiarCanchas.UseVisualStyleBackColor = true;
            this.btLimpiarCanchas.Click += new System.EventHandler(this.btLimpiarCanchas_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Nombre:";
            // 
            // tbNombreClub1
            // 
            this.tbNombreClub1.Location = new System.Drawing.Point(69, 29);
            this.tbNombreClub1.Name = "tbNombreClub1";
            this.tbNombreClub1.Size = new System.Drawing.Size(203, 21);
            this.tbNombreClub1.TabIndex = 17;
            // 
            // dgvClub
            // 
            this.dgvClub.AllowUserToAddRows = false;
            this.dgvClub.AllowUserToDeleteRows = false;
            this.dgvClub.AllowUserToResizeRows = false;
            this.dgvClub.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClub.Location = new System.Drawing.Point(43, 66);
            this.dgvClub.MultiSelect = false;
            this.dgvClub.Name = "dgvClub";
            this.dgvClub.ReadOnly = true;
            this.dgvClub.RowHeadersVisible = false;
            this.dgvClub.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClub.Size = new System.Drawing.Size(328, 532);
            this.dgvClub.TabIndex = 15;
            this.dgvClub.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClub_CellContentClick);
            // 
            // btAgregarCancha
            // 
            this.btAgregarCancha.Location = new System.Drawing.Point(39, 218);
            this.btAgregarCancha.Name = "btAgregarCancha";
            this.btAgregarCancha.Size = new System.Drawing.Size(104, 33);
            this.btAgregarCancha.TabIndex = 14;
            this.btAgregarCancha.Text = "Agregar club";
            this.btAgregarCancha.UseVisualStyleBackColor = true;
            this.btAgregarCancha.Click += new System.EventHandler(this.btAgregarCancha_Click);
            // 
            // gbAgregarClub
            // 
            this.gbAgregarClub.BackColor = System.Drawing.Color.LemonChiffon;
            this.gbAgregarClub.Controls.Add(this.pbCancha1);
            this.gbAgregarClub.Controls.Add(this.label1);
            this.gbAgregarClub.Controls.Add(this.tbNombreClub1);
            this.gbAgregarClub.Controls.Add(this.btLimpiarCanchas);
            this.gbAgregarClub.Controls.Add(this.btAgregarCancha);
            this.gbAgregarClub.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAgregarClub.Location = new System.Drawing.Point(478, 35);
            this.gbAgregarClub.Name = "gbAgregarClub";
            this.gbAgregarClub.Size = new System.Drawing.Size(296, 261);
            this.gbAgregarClub.TabIndex = 22;
            this.gbAgregarClub.TabStop = false;
            this.gbAgregarClub.Text = "AGREGAR CLUB";
            // 
            // pbCancha1
            // 
            this.pbCancha1.Image = global::Torneo_Guillermito.Properties.Resources.nada;
            this.pbCancha1.Location = new System.Drawing.Point(77, 58);
            this.pbCancha1.Name = "pbCancha1";
            this.pbCancha1.Size = new System.Drawing.Size(150, 150);
            this.pbCancha1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCancha1.TabIndex = 19;
            this.pbCancha1.TabStop = false;
            this.pbCancha1.Click += new System.EventHandler(this.pbCancha1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LemonChiffon;
            this.groupBox1.Controls.Add(this.pbCancha2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbNombreClub2);
            this.groupBox1.Controls.Add(this.btModificarClub);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(478, 337);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 261);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MODIFICAR CLUB";
            // 
            // pbCancha2
            // 
            this.pbCancha2.Image = global::Torneo_Guillermito.Properties.Resources.nada;
            this.pbCancha2.Location = new System.Drawing.Point(76, 58);
            this.pbCancha2.Name = "pbCancha2";
            this.pbCancha2.Size = new System.Drawing.Size(150, 150);
            this.pbCancha2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCancha2.TabIndex = 19;
            this.pbCancha2.TabStop = false;
            this.pbCancha2.Click += new System.EventHandler(this.pbCancha2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = "Nombre:";
            // 
            // tbNombreClub2
            // 
            this.tbNombreClub2.Enabled = false;
            this.tbNombreClub2.Location = new System.Drawing.Point(67, 27);
            this.tbNombreClub2.Name = "tbNombreClub2";
            this.tbNombreClub2.Size = new System.Drawing.Size(203, 21);
            this.tbNombreClub2.TabIndex = 17;
            // 
            // btModificarClub
            // 
            this.btModificarClub.Enabled = false;
            this.btModificarClub.Location = new System.Drawing.Point(98, 216);
            this.btModificarClub.Name = "btModificarClub";
            this.btModificarClub.Size = new System.Drawing.Size(104, 33);
            this.btModificarClub.TabIndex = 14;
            this.btModificarClub.Text = "Modificar club";
            this.btModificarClub.UseVisualStyleBackColor = true;
            this.btModificarClub.Click += new System.EventHandler(this.btModificarClub_Click);
            // 
            // btEliminarClub
            // 
            this.btEliminarClub.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btEliminarClub.Location = new System.Drawing.Point(154, 617);
            this.btEliminarClub.Name = "btEliminarClub";
            this.btEliminarClub.Size = new System.Drawing.Size(104, 33);
            this.btEliminarClub.TabIndex = 24;
            this.btEliminarClub.Text = "Eliminar club";
            this.btEliminarClub.UseVisualStyleBackColor = true;
            this.btEliminarClub.Click += new System.EventHandler(this.btEliminarClub_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(108, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 21);
            this.label3.TabIndex = 20;
            this.label3.Text = "Lista de todos los clubes";
            // 
            // lbAdvertencia
            // 
            this.lbAdvertencia.AutoSize = true;
            this.lbAdvertencia.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAdvertencia.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbAdvertencia.Location = new System.Drawing.Point(490, 608);
            this.lbAdvertencia.Name = "lbAdvertencia";
            this.lbAdvertencia.Size = new System.Drawing.Size(277, 38);
            this.lbAdvertencia.TabIndex = 25;
            this.lbAdvertencia.Text = "(Debe seleccionar un club de la lista\r\npara poder modificarlo)";
            this.lbAdvertencia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Club
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(812, 697);
            this.Controls.Add(this.lbAdvertencia);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btEliminarClub);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbAgregarClub);
            this.Controls.Add(this.dgvClub);
            this.Name = "Club";
            this.Text = "Clubes Torneo Guillermito";
            this.Load += new System.EventHandler(this.Club_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClub)).EndInit();
            this.gbAgregarClub.ResumeLayout(false);
            this.gbAgregarClub.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancha1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancha2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btLimpiarCanchas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbNombreClub1;
        private System.Windows.Forms.DataGridView dgvClub;
        private System.Windows.Forms.Button btAgregarCancha;
        private System.Windows.Forms.GroupBox gbAgregarClub;
        private System.Windows.Forms.PictureBox pbCancha1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pbCancha2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbNombreClub2;
        private System.Windows.Forms.Button btModificarClub;
        private System.Windows.Forms.Button btEliminarClub;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbAdvertencia;
    }
}