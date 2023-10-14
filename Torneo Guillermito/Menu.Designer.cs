namespace Torneo_Guillermito
{
    partial class Menu
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Zona = new System.Windows.Forms.Button();
            this.Cancha = new System.Windows.Forms.Button();
            this.Encuentro = new System.Windows.Forms.Button();
            this.Club = new System.Windows.Forms.Button();
            this.Equipo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbClubes = new System.Windows.Forms.Label();
            this.lbCyZ = new System.Windows.Forms.Label();
            this.lbEquipos = new System.Windows.Forms.Label();
            this.lbCanchas = new System.Windows.Forms.Label();
            this.lbEncuentros = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Zona
            // 
            this.Zona.Image = global::Torneo_Guillermito.Properties.Resources.Categorias;
            this.Zona.Location = new System.Drawing.Point(310, 164);
            this.Zona.Name = "Zona";
            this.Zona.Size = new System.Drawing.Size(120, 120);
            this.Zona.TabIndex = 0;
            this.Zona.UseVisualStyleBackColor = true;
            this.Zona.Click += new System.EventHandler(this.button1_Click);
            this.Zona.MouseEnter += new System.EventHandler(this.Zona_MouseEnter);
            this.Zona.MouseLeave += new System.EventHandler(this.Zona_MouseLeave);
            // 
            // Cancha
            // 
            this.Cancha.Image = global::Torneo_Guillermito.Properties.Resources.Canchas;
            this.Cancha.Location = new System.Drawing.Point(199, 385);
            this.Cancha.Name = "Cancha";
            this.Cancha.Size = new System.Drawing.Size(120, 120);
            this.Cancha.TabIndex = 1;
            this.Cancha.UseVisualStyleBackColor = true;
            this.Cancha.Click += new System.EventHandler(this.Cancha_Click);
            this.Cancha.MouseEnter += new System.EventHandler(this.Cancha_MouseEnter);
            this.Cancha.MouseLeave += new System.EventHandler(this.Cancha_MouseLeave);
            // 
            // Encuentro
            // 
            this.Encuentro.Image = global::Torneo_Guillermito.Properties.Resources.Encuentros;
            this.Encuentro.Location = new System.Drawing.Point(430, 385);
            this.Encuentro.Name = "Encuentro";
            this.Encuentro.Size = new System.Drawing.Size(120, 120);
            this.Encuentro.TabIndex = 2;
            this.Encuentro.UseVisualStyleBackColor = true;
            this.Encuentro.Click += new System.EventHandler(this.Encuentro_Click);
            this.Encuentro.MouseEnter += new System.EventHandler(this.Encuentro_MouseEnter);
            this.Encuentro.MouseLeave += new System.EventHandler(this.Encuentro_MouseLeave);
            // 
            // Club
            // 
            this.Club.Image = global::Torneo_Guillermito.Properties.Resources.Clubes2;
            this.Club.Location = new System.Drawing.Point(85, 164);
            this.Club.Name = "Club";
            this.Club.Size = new System.Drawing.Size(120, 120);
            this.Club.TabIndex = 3;
            this.Club.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Club.UseVisualStyleBackColor = true;
            this.Club.Click += new System.EventHandler(this.Club_Click);
            this.Club.MouseEnter += new System.EventHandler(this.Club_MouseEnter);
            this.Club.MouseLeave += new System.EventHandler(this.Club_MouseLeave);
            // 
            // Equipo
            // 
            this.Equipo.Image = global::Torneo_Guillermito.Properties.Resources.Equipos;
            this.Equipo.Location = new System.Drawing.Point(533, 164);
            this.Equipo.Name = "Equipo";
            this.Equipo.Size = new System.Drawing.Size(120, 120);
            this.Equipo.TabIndex = 4;
            this.Equipo.UseVisualStyleBackColor = true;
            this.Equipo.Click += new System.EventHandler(this.Equipo_Click);
            this.Equipo.MouseEnter += new System.EventHandler(this.Equipo_MouseEnter);
            this.Equipo.MouseLeave += new System.EventHandler(this.Equipo_MouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(170, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(392, 82);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sistema de gestión\r\nTorneo Guillermito 2023";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbClubes
            // 
            this.lbClubes.AutoSize = true;
            this.lbClubes.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbClubes.Location = new System.Drawing.Point(105, 292);
            this.lbClubes.Name = "lbClubes";
            this.lbClubes.Size = new System.Drawing.Size(80, 28);
            this.lbClubes.TabIndex = 6;
            this.lbClubes.Text = "Clubes";
            this.lbClubes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCyZ
            // 
            this.lbCyZ.AutoSize = true;
            this.lbCyZ.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCyZ.Location = new System.Drawing.Point(309, 288);
            this.lbCyZ.Name = "lbCyZ";
            this.lbCyZ.Size = new System.Drawing.Size(126, 56);
            this.lbCyZ.TabIndex = 7;
            this.lbCyZ.Text = "Categorías \r\ny Zonas";
            this.lbCyZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbEquipos
            // 
            this.lbEquipos.AutoSize = true;
            this.lbEquipos.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEquipos.Location = new System.Drawing.Point(547, 292);
            this.lbEquipos.Name = "lbEquipos";
            this.lbEquipos.Size = new System.Drawing.Size(92, 28);
            this.lbEquipos.TabIndex = 8;
            this.lbEquipos.Text = "Equipos";
            this.lbEquipos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCanchas
            // 
            this.lbCanchas.AutoSize = true;
            this.lbCanchas.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCanchas.Location = new System.Drawing.Point(211, 513);
            this.lbCanchas.Name = "lbCanchas";
            this.lbCanchas.Size = new System.Drawing.Size(97, 28);
            this.lbCanchas.TabIndex = 9;
            this.lbCanchas.Text = "Canchas";
            this.lbCanchas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbEncuentros
            // 
            this.lbEncuentros.AutoSize = true;
            this.lbEncuentros.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEncuentros.Location = new System.Drawing.Point(429, 514);
            this.lbEncuentros.Name = "lbEncuentros";
            this.lbEncuentros.Size = new System.Drawing.Size(125, 28);
            this.lbEncuentros.TabIndex = 10;
            this.lbEncuentros.Text = "Encuentros";
            this.lbEncuentros.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(724, 635);
            this.Controls.Add(this.lbEncuentros);
            this.Controls.Add(this.lbCanchas);
            this.Controls.Add(this.lbEquipos);
            this.Controls.Add(this.lbCyZ);
            this.Controls.Add(this.lbClubes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Equipo);
            this.Controls.Add(this.Club);
            this.Controls.Add(this.Encuentro);
            this.Controls.Add(this.Cancha);
            this.Controls.Add(this.Zona);
            this.Name = "Menu";
            this.Text = "Torneo Guillermito";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Zona;
        private System.Windows.Forms.Button Cancha;
        private System.Windows.Forms.Button Encuentro;
        private System.Windows.Forms.Button Club;
        private System.Windows.Forms.Button Equipo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbClubes;
        private System.Windows.Forms.Label lbCyZ;
        private System.Windows.Forms.Label lbEquipos;
        private System.Windows.Forms.Label lbCanchas;
        private System.Windows.Forms.Label lbEncuentros;
    }
}

