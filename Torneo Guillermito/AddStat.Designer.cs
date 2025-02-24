namespace Torneo_Guillermito
{
    partial class AddStat
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvJugadoresPartido = new System.Windows.Forms.DataGridView();
            this.id_jugador_actions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dorsal_jugador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAcciones = new System.Windows.Forms.DataGridView();
            this.btEliminarEncuentro = new System.Windows.Forms.Button();
            this.plusStat = new System.Windows.Forms.PictureBox();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dorsal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJugadoresPartido)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAcciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plusStat)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(26, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Accion: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(117, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 26);
            this.label2.TabIndex = 1;
            // 
            // dgvJugadoresPartido
            // 
            this.dgvJugadoresPartido.AllowUserToAddRows = false;
            this.dgvJugadoresPartido.AllowUserToDeleteRows = false;
            this.dgvJugadoresPartido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJugadoresPartido.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_jugador_actions,
            this.Nombre,
            this.dorsal_jugador});
            this.dgvJugadoresPartido.Location = new System.Drawing.Point(31, 90);
            this.dgvJugadoresPartido.Name = "dgvJugadoresPartido";
            this.dgvJugadoresPartido.ReadOnly = true;
            this.dgvJugadoresPartido.RowHeadersVisible = false;
            this.dgvJugadoresPartido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvJugadoresPartido.Size = new System.Drawing.Size(255, 475);
            this.dgvJugadoresPartido.TabIndex = 2;
            // 
            // id_jugador_actions
            // 
            this.id_jugador_actions.HeaderText = "ID";
            this.id_jugador_actions.Name = "id_jugador_actions";
            this.id_jugador_actions.ReadOnly = true;
            this.id_jugador_actions.Visible = false;
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            this.Nombre.Width = 200;
            // 
            // dorsal_jugador
            // 
            this.dorsal_jugador.HeaderText = "Dorsal";
            this.dorsal_jugador.Name = "dorsal_jugador";
            this.dorsal_jugador.ReadOnly = true;
            this.dorsal_jugador.Width = 50;
            // 
            // dgvAcciones
            // 
            this.dgvAcciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAcciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ID2,
            this.name,
            this.dorsal});
            this.dgvAcciones.Location = new System.Drawing.Point(376, 90);
            this.dgvAcciones.Name = "dgvAcciones";
            this.dgvAcciones.RowHeadersVisible = false;
            this.dgvAcciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAcciones.Size = new System.Drawing.Size(255, 408);
            this.dgvAcciones.TabIndex = 3;
            // 
            // btEliminarEncuentro
            // 
            this.btEliminarEncuentro.BackColor = System.Drawing.Color.DarkRed;
            this.btEliminarEncuentro.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btEliminarEncuentro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btEliminarEncuentro.Location = new System.Drawing.Point(413, 521);
            this.btEliminarEncuentro.Margin = new System.Windows.Forms.Padding(4);
            this.btEliminarEncuentro.Name = "btEliminarEncuentro";
            this.btEliminarEncuentro.Size = new System.Drawing.Size(182, 44);
            this.btEliminarEncuentro.TabIndex = 67;
            this.btEliminarEncuentro.Text = "Eliminar acción selec.";
            this.btEliminarEncuentro.UseVisualStyleBackColor = false;
            this.btEliminarEncuentro.Click += new System.EventHandler(this.btEliminarEncuentro_Click);
            // 
            // plusStat
            // 
            this.plusStat.Image = global::Torneo_Guillermito.Properties.Resources.plus;
            this.plusStat.Location = new System.Drawing.Point(302, 265);
            this.plusStat.Name = "plusStat";
            this.plusStat.Size = new System.Drawing.Size(60, 50);
            this.plusStat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.plusStat.TabIndex = 68;
            this.plusStat.TabStop = false;
            this.plusStat.Click += new System.EventHandler(this.plusStat_Click);
            // 
            // ID
            // 
            this.ID.HeaderText = "IDa";
            this.ID.Name = "ID";
            // 
            // ID2
            // 
            this.ID2.HeaderText = "IDj";
            this.ID2.Name = "ID2";
            // 
            // name
            // 
            this.name.HeaderText = "Nombre";
            this.name.Name = "name";
            this.name.Width = 200;
            // 
            // dorsal
            // 
            this.dorsal.HeaderText = "Dorsal";
            this.dorsal.Name = "dorsal";
            this.dorsal.Width = 50;
            // 
            // AddStat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(255)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(689, 619);
            this.Controls.Add(this.plusStat);
            this.Controls.Add(this.btEliminarEncuentro);
            this.Controls.Add(this.dgvAcciones);
            this.Controls.Add(this.dgvJugadoresPartido);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddStat";
            this.Text = "AddStat";
            this.Load += new System.EventHandler(this.AddStat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJugadoresPartido)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAcciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plusStat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvJugadoresPartido;
        private System.Windows.Forms.DataGridView dgvAcciones;
        private System.Windows.Forms.Button btEliminarEncuentro;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_jugador_actions;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn dorsal_jugador;
        private System.Windows.Forms.PictureBox plusStat;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID2;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dorsal;
    }
}