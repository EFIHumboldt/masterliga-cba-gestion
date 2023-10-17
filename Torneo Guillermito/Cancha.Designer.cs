namespace Torneo_Guillermito
{
    partial class Cancha
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
            this.tbLongitud = new System.Windows.Forms.TextBox();
            this.btAgregarCancha = new System.Windows.Forms.Button();
            this.dgvCanchas = new System.Windows.Forms.DataGridView();
            this.tbLatitud = new System.Windows.Forms.TextBox();
            this.tbNumero = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btLimpiarCanchas = new System.Windows.Forms.Button();
            this.btEliminarCancha = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanchas)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbLongitud
            // 
            this.tbLongitud.Location = new System.Drawing.Point(92, 143);
            this.tbLongitud.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbLongitud.Name = "tbLongitud";
            this.tbLongitud.Size = new System.Drawing.Size(215, 21);
            this.tbLongitud.TabIndex = 5;
            // 
            // btAgregarCancha
            // 
            this.btAgregarCancha.Location = new System.Drawing.Point(186, 194);
            this.btAgregarCancha.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btAgregarCancha.Name = "btAgregarCancha";
            this.btAgregarCancha.Size = new System.Drawing.Size(121, 41);
            this.btAgregarCancha.TabIndex = 4;
            this.btAgregarCancha.Text = "Agregar cancha";
            this.btAgregarCancha.UseVisualStyleBackColor = true;
            this.btAgregarCancha.Click += new System.EventHandler(this.btAgregarCancha_Click);
            // 
            // dgvCanchas
            // 
            this.dgvCanchas.AllowUserToAddRows = false;
            this.dgvCanchas.AllowUserToDeleteRows = false;
            this.dgvCanchas.AllowUserToResizeRows = false;
            this.dgvCanchas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCanchas.Location = new System.Drawing.Point(31, 59);
            this.dgvCanchas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvCanchas.MultiSelect = false;
            this.dgvCanchas.Name = "dgvCanchas";
            this.dgvCanchas.RowHeadersVisible = false;
            this.dgvCanchas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCanchas.Size = new System.Drawing.Size(271, 414);
            this.dgvCanchas.TabIndex = 6;
            // 
            // tbLatitud
            // 
            this.tbLatitud.Location = new System.Drawing.Point(92, 99);
            this.tbLatitud.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbLatitud.Name = "tbLatitud";
            this.tbLatitud.Size = new System.Drawing.Size(215, 21);
            this.tbLatitud.TabIndex = 7;
            // 
            // tbNumero
            // 
            this.tbNumero.Location = new System.Drawing.Point(92, 52);
            this.tbNumero.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbNumero.Name = "tbNumero";
            this.tbNumero.Size = new System.Drawing.Size(215, 21);
            this.tbNumero.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 56);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Numero:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 103);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Latitud:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 148);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Longitud:";
            // 
            // btLimpiarCanchas
            // 
            this.btLimpiarCanchas.Location = new System.Drawing.Point(80, 194);
            this.btLimpiarCanchas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btLimpiarCanchas.Name = "btLimpiarCanchas";
            this.btLimpiarCanchas.Size = new System.Drawing.Size(98, 41);
            this.btLimpiarCanchas.TabIndex = 12;
            this.btLimpiarCanchas.Text = "Limpiar";
            this.btLimpiarCanchas.UseVisualStyleBackColor = true;
            this.btLimpiarCanchas.Click += new System.EventHandler(this.btLimpiarCanchas_Click);
            // 
            // btEliminarCancha
            // 
            this.btEliminarCancha.Location = new System.Drawing.Point(84, 490);
            this.btEliminarCancha.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btEliminarCancha.Name = "btEliminarCancha";
            this.btEliminarCancha.Size = new System.Drawing.Size(148, 41);
            this.btEliminarCancha.TabIndex = 13;
            this.btEliminarCancha.Text = "Eliminar cancha";
            this.btEliminarCancha.UseVisualStyleBackColor = true;
            this.btEliminarCancha.Click += new System.EventHandler(this.btEliminarCancha_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LemonChiffon;
            this.groupBox1.Controls.Add(this.tbLongitud);
            this.groupBox1.Controls.Add(this.btLimpiarCanchas);
            this.groupBox1.Controls.Add(this.tbLatitud);
            this.groupBox1.Controls.Add(this.btAgregarCancha);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbNumero);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(354, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 252);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AGREGRAR CANCHAS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(80, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 21);
            this.label4.TabIndex = 40;
            this.label4.Text = "Lista de las canchas";
            // 
            // Cancha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(702, 554);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btEliminarCancha);
            this.Controls.Add(this.dgvCanchas);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Cancha";
            this.Text = "Canchas Torneo Guillermito";
            this.Load += new System.EventHandler(this.Cancha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanchas)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbLongitud;
        private System.Windows.Forms.Button btAgregarCancha;
        private System.Windows.Forms.DataGridView dgvCanchas;
        private System.Windows.Forms.TextBox tbLatitud;
        private System.Windows.Forms.TextBox tbNumero;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btLimpiarCanchas;
        private System.Windows.Forms.Button btEliminarCancha;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
    }
}