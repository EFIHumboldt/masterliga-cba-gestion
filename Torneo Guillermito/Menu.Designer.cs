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
            this.SuspendLayout();
            // 
            // Zona
            // 
            this.Zona.Location = new System.Drawing.Point(106, 143);
            this.Zona.Name = "Zona";
            this.Zona.Size = new System.Drawing.Size(119, 75);
            this.Zona.TabIndex = 0;
            this.Zona.Text = "Categorias y Zonas";
            this.Zona.UseVisualStyleBackColor = true;
            this.Zona.Click += new System.EventHandler(this.button1_Click);
            // 
            // Cancha
            // 
            this.Cancha.Location = new System.Drawing.Point(305, 143);
            this.Cancha.Name = "Cancha";
            this.Cancha.Size = new System.Drawing.Size(119, 75);
            this.Cancha.TabIndex = 1;
            this.Cancha.Text = "Canchas";
            this.Cancha.UseVisualStyleBackColor = true;
            this.Cancha.Click += new System.EventHandler(this.Cancha_Click);
            // 
            // Encuentro
            // 
            this.Encuentro.Location = new System.Drawing.Point(509, 143);
            this.Encuentro.Name = "Encuentro";
            this.Encuentro.Size = new System.Drawing.Size(119, 75);
            this.Encuentro.TabIndex = 2;
            this.Encuentro.Text = "Encuentros";
            this.Encuentro.UseVisualStyleBackColor = true;
            this.Encuentro.Click += new System.EventHandler(this.Encuentro_Click);
            // 
            // Club
            // 
            this.Club.Location = new System.Drawing.Point(707, 143);
            this.Club.Name = "Club";
            this.Club.Size = new System.Drawing.Size(119, 75);
            this.Club.TabIndex = 3;
            this.Club.Text = "Clubes";
            this.Club.UseVisualStyleBackColor = true;
            this.Club.Click += new System.EventHandler(this.Club_Click);
            // 
            // Equipo
            // 
            this.Equipo.Location = new System.Drawing.Point(901, 143);
            this.Equipo.Name = "Equipo";
            this.Equipo.Size = new System.Drawing.Size(119, 75);
            this.Equipo.TabIndex = 4;
            this.Equipo.Text = "Equipos";
            this.Equipo.UseVisualStyleBackColor = true;
            this.Equipo.Click += new System.EventHandler(this.Equipo_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 323);
            this.Controls.Add(this.Equipo);
            this.Controls.Add(this.Club);
            this.Controls.Add(this.Encuentro);
            this.Controls.Add(this.Cancha);
            this.Controls.Add(this.Zona);
            this.Name = "Menu";
            this.Text = "Torneo Guillermito";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Zona;
        private System.Windows.Forms.Button Cancha;
        private System.Windows.Forms.Button Encuentro;
        private System.Windows.Forms.Button Club;
        private System.Windows.Forms.Button Equipo;
    }
}

