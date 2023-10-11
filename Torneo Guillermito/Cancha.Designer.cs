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
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanchas)).BeginInit();
            this.SuspendLayout();
            // 
            // tbLongitud
            // 
            this.tbLongitud.Location = new System.Drawing.Point(494, 226);
            this.tbLongitud.Name = "tbLongitud";
            this.tbLongitud.Size = new System.Drawing.Size(185, 20);
            this.tbLongitud.TabIndex = 5;
            // 
            // btAgregarCancha
            // 
            this.btAgregarCancha.Location = new System.Drawing.Point(469, 282);
            this.btAgregarCancha.Name = "btAgregarCancha";
            this.btAgregarCancha.Size = new System.Drawing.Size(104, 33);
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
            this.dgvCanchas.Location = new System.Drawing.Point(51, 58);
            this.dgvCanchas.MultiSelect = false;
            this.dgvCanchas.Name = "dgvCanchas";
            this.dgvCanchas.RowHeadersVisible = false;
            this.dgvCanchas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCanchas.Size = new System.Drawing.Size(328, 326);
            this.dgvCanchas.TabIndex = 6;
            // 
            // tbLatitud
            // 
            this.tbLatitud.Location = new System.Drawing.Point(494, 190);
            this.tbLatitud.Name = "tbLatitud";
            this.tbLatitud.Size = new System.Drawing.Size(185, 20);
            this.tbLatitud.TabIndex = 7;
            // 
            // tbNumero
            // 
            this.tbNumero.Location = new System.Drawing.Point(494, 152);
            this.tbNumero.Name = "tbNumero";
            this.tbNumero.Size = new System.Drawing.Size(185, 20);
            this.tbNumero.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(441, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Numero:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(443, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Latitud:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(432, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Longitud:";
            // 
            // btLimpiarCanchas
            // 
            this.btLimpiarCanchas.Location = new System.Drawing.Point(595, 282);
            this.btLimpiarCanchas.Name = "btLimpiarCanchas";
            this.btLimpiarCanchas.Size = new System.Drawing.Size(84, 33);
            this.btLimpiarCanchas.TabIndex = 12;
            this.btLimpiarCanchas.Text = "Limpiar";
            this.btLimpiarCanchas.UseVisualStyleBackColor = true;
            this.btLimpiarCanchas.Click += new System.EventHandler(this.btLimpiarCanchas_Click);
            // 
            // btEliminarCancha
            // 
            this.btEliminarCancha.Location = new System.Drawing.Point(152, 405);
            this.btEliminarCancha.Name = "btEliminarCancha";
            this.btEliminarCancha.Size = new System.Drawing.Size(127, 33);
            this.btEliminarCancha.TabIndex = 13;
            this.btEliminarCancha.Text = "Eliminar cancha";
            this.btEliminarCancha.UseVisualStyleBackColor = true;
            this.btEliminarCancha.Click += new System.EventHandler(this.btEliminarCancha_Click);
            // 
            // Cancha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 450);
            this.Controls.Add(this.btEliminarCancha);
            this.Controls.Add(this.btLimpiarCanchas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbNumero);
            this.Controls.Add(this.tbLatitud);
            this.Controls.Add(this.dgvCanchas);
            this.Controls.Add(this.tbLongitud);
            this.Controls.Add(this.btAgregarCancha);
            this.Name = "Cancha";
            this.Text = "Canchas Torneo Guillermito";
            this.Load += new System.EventHandler(this.Cancha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanchas)).EndInit();
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
    }
}