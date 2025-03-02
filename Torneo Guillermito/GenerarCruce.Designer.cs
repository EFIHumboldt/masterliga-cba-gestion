namespace LIPa
{
    partial class GenerarCruce
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerarCruce));
            this.dgvTabla = new System.Windows.Forms.DataGridView();
            this.nro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreEquipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ptoEquipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idTablaPosicion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelTituloCategoria = new System.Windows.Forms.Label();
            this.btGenerarCruce = new System.Windows.Forms.Button();
            this.labelError = new System.Windows.Forms.Label();
            this.flechaArriba = new System.Windows.Forms.PictureBox();
            this.flechaAbajo = new System.Windows.Forms.PictureBox();
            this.dgvEncuentroPosiciones = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flechaArriba)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flechaAbajo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEncuentroPosiciones)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTabla
            // 
            this.dgvTabla.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTabla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTabla.ColumnHeadersHeight = 30;
            this.dgvTabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nro,
            this.nombreEquipo,
            this.ptoEquipo,
            this.pj,
            this.pg,
            this.pe,
            this.pp,
            this.gf,
            this.gc,
            this.idTablaPosicion});
            this.dgvTabla.Location = new System.Drawing.Point(100, 118);
            this.dgvTabla.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvTabla.Name = "dgvTabla";
            this.dgvTabla.RowHeadersVisible = false;
            this.dgvTabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTabla.Size = new System.Drawing.Size(726, 946);
            this.dgvTabla.TabIndex = 1;
            // 
            // nro
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            this.nro.DefaultCellStyle = dataGridViewCellStyle2;
            this.nro.HeaderText = "Nº";
            this.nro.Name = "nro";
            this.nro.Width = 30;
            // 
            // nombreEquipo
            // 
            this.nombreEquipo.HeaderText = "EQUIPO";
            this.nombreEquipo.Name = "nombreEquipo";
            this.nombreEquipo.Width = 240;
            // 
            // ptoEquipo
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            this.ptoEquipo.DefaultCellStyle = dataGridViewCellStyle3;
            this.ptoEquipo.HeaderText = "PTS";
            this.ptoEquipo.Name = "ptoEquipo";
            this.ptoEquipo.Width = 30;
            // 
            // pj
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.pj.DefaultCellStyle = dataGridViewCellStyle4;
            this.pj.HeaderText = "PJ";
            this.pj.Name = "pj";
            this.pj.Width = 30;
            // 
            // pg
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.pg.DefaultCellStyle = dataGridViewCellStyle5;
            this.pg.HeaderText = "PG";
            this.pg.Name = "pg";
            this.pg.Width = 30;
            // 
            // pe
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.pe.DefaultCellStyle = dataGridViewCellStyle6;
            this.pe.HeaderText = "PE";
            this.pe.Name = "pe";
            this.pe.Width = 30;
            // 
            // pp
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.pp.DefaultCellStyle = dataGridViewCellStyle7;
            this.pp.HeaderText = "PP";
            this.pp.Name = "pp";
            this.pp.Width = 30;
            // 
            // gf
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.gf.DefaultCellStyle = dataGridViewCellStyle8;
            this.gf.HeaderText = "GF";
            this.gf.Name = "gf";
            this.gf.Width = 30;
            // 
            // gc
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.gc.DefaultCellStyle = dataGridViewCellStyle9;
            this.gc.HeaderText = "GC";
            this.gc.Name = "gc";
            this.gc.Width = 30;
            // 
            // idTablaPosicion
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.idTablaPosicion.DefaultCellStyle = dataGridViewCellStyle10;
            this.idTablaPosicion.HeaderText = "ID";
            this.idTablaPosicion.Name = "idTablaPosicion";
            this.idTablaPosicion.Visible = false;
            // 
            // labelTituloCategoria
            // 
            this.labelTituloCategoria.AutoSize = true;
            this.labelTituloCategoria.BackColor = System.Drawing.Color.Khaki;
            this.labelTituloCategoria.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTituloCategoria.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelTituloCategoria.Location = new System.Drawing.Point(368, 43);
            this.labelTituloCategoria.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelTituloCategoria.Name = "labelTituloCategoria";
            this.labelTituloCategoria.Size = new System.Drawing.Size(169, 28);
            this.labelTituloCategoria.TabIndex = 35;
            this.labelTituloCategoria.Text = "Categoría: 2010";
            this.labelTituloCategoria.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btGenerarCruce
            // 
            this.btGenerarCruce.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btGenerarCruce.Location = new System.Drawing.Point(375, 1074);
            this.btGenerarCruce.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btGenerarCruce.Name = "btGenerarCruce";
            this.btGenerarCruce.Size = new System.Drawing.Size(196, 65);
            this.btGenerarCruce.TabIndex = 36;
            this.btGenerarCruce.Text = "Generar cruce";
            this.btGenerarCruce.UseVisualStyleBackColor = true;
            this.btGenerarCruce.Click += new System.EventHandler(this.btGenerarCruce_Click);
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelError.ForeColor = System.Drawing.Color.Red;
            this.labelError.Location = new System.Drawing.Point(30, 1160);
            this.labelError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(583, 19);
            this.labelError.TabIndex = 37;
            this.labelError.Text = "(No se puede generar el cruce si no existe una plantilla para dicha categoría)";
            this.labelError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flechaArriba
            // 
            this.flechaArriba.Image = global::LIPa.Properties.Resources.arriba;
            this.flechaArriba.Location = new System.Drawing.Point(850, 454);
            this.flechaArriba.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flechaArriba.Name = "flechaArriba";
            this.flechaArriba.Size = new System.Drawing.Size(63, 74);
            this.flechaArriba.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.flechaArriba.TabIndex = 39;
            this.flechaArriba.TabStop = false;
            this.flechaArriba.Click += new System.EventHandler(this.flechaArriba_Click);
            // 
            // flechaAbajo
            // 
            this.flechaAbajo.Image = global::LIPa.Properties.Resources.abajo;
            this.flechaAbajo.Location = new System.Drawing.Point(850, 586);
            this.flechaAbajo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flechaAbajo.Name = "flechaAbajo";
            this.flechaAbajo.Size = new System.Drawing.Size(63, 71);
            this.flechaAbajo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.flechaAbajo.TabIndex = 40;
            this.flechaAbajo.TabStop = false;
            this.flechaAbajo.Click += new System.EventHandler(this.flechaAbajo_Click);
            // 
            // dgvEncuentroPosiciones
            // 
            this.dgvEncuentroPosiciones.AllowUserToAddRows = false;
            this.dgvEncuentroPosiciones.AllowUserToDeleteRows = false;
            this.dgvEncuentroPosiciones.AllowUserToResizeColumns = false;
            this.dgvEncuentroPosiciones.AllowUserToResizeRows = false;
            this.dgvEncuentroPosiciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEncuentroPosiciones.Location = new System.Drawing.Point(87, 1160);
            this.dgvEncuentroPosiciones.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvEncuentroPosiciones.Name = "dgvEncuentroPosiciones";
            this.dgvEncuentroPosiciones.RowHeadersVisible = false;
            this.dgvEncuentroPosiciones.Size = new System.Drawing.Size(726, 57);
            this.dgvEncuentroPosiciones.TabIndex = 41;
            this.dgvEncuentroPosiciones.Visible = false;
            // 
            // GenerarCruce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(950, 1061);
            this.Controls.Add(this.dgvEncuentroPosiciones);
            this.Controls.Add(this.flechaAbajo);
            this.Controls.Add(this.flechaArriba);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.btGenerarCruce);
            this.Controls.Add(this.labelTituloCategoria);
            this.Controls.Add(this.dgvTabla);
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximumSize = new System.Drawing.Size(966, 1257);
            this.Name = "GenerarCruce";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GenerarCruce";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GenerarCruce_FormClosed);
            this.Load += new System.EventHandler(this.GenerarCruce_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flechaArriba)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flechaAbajo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEncuentroPosiciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTabla;
        private System.Windows.Forms.Label labelTituloCategoria;
        private System.Windows.Forms.Button btGenerarCruce;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.PictureBox flechaArriba;
        private System.Windows.Forms.PictureBox flechaAbajo;
        private System.Windows.Forms.DataGridView dgvEncuentroPosiciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn nro;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreEquipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ptoEquipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn pj;
        private System.Windows.Forms.DataGridViewTextBoxColumn pg;
        private System.Windows.Forms.DataGridViewTextBoxColumn pe;
        private System.Windows.Forms.DataGridViewTextBoxColumn pp;
        private System.Windows.Forms.DataGridViewTextBoxColumn gf;
        private System.Windows.Forms.DataGridViewTextBoxColumn gc;
        private System.Windows.Forms.DataGridViewTextBoxColumn idTablaPosicion;
    }
}