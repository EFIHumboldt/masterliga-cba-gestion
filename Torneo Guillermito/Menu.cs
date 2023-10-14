using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Torneo_Guillermito
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var zona = new Zona();
            zona.Show();
        }

        private void Cancha_Click(object sender, EventArgs e)
        {
            var cancha = new Cancha();
            cancha.Show();
        }

        private void Encuentro_Click(object sender, EventArgs e)
        {
            var encuentro = new Encuentro(); 
            encuentro.Show();
        }

        private void Club_Click(object sender, EventArgs e)
        {
            var club = new Club(); 
            club.Show();
        }

        private void Equipo_Click(object sender, EventArgs e)
        {
            var equipo = new Equipo(); 
            equipo.Show();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
        }

        private void Club_MouseEnter(object sender, EventArgs e)
        {
            lbClubes.Font = new Font(lbClubes.Font, FontStyle.Bold);
        }

        private void Club_MouseLeave(object sender, EventArgs e)
        {
            lbClubes.Font = new Font(lbClubes.Font, FontStyle.Regular);

        }

        private void Zona_MouseEnter(object sender, EventArgs e)
        {
            lbCyZ.Font = new Font(lbCyZ.Font, FontStyle.Bold);

        }

        private void Zona_MouseLeave(object sender, EventArgs e)
        {
            lbCyZ.Font = new Font(lbCyZ.Font, FontStyle.Regular);

        }

        private void Equipo_MouseEnter(object sender, EventArgs e)
        {
            lbEquipos.Font = new Font(lbEquipos.Font, FontStyle.Bold);

        }

        private void Equipo_MouseLeave(object sender, EventArgs e)
        {
            lbEquipos.Font = new Font(lbEquipos.Font, FontStyle.Regular);

        }

        private void Encuentro_MouseEnter(object sender, EventArgs e)
        {
            lbEncuentros.Font = new Font(lbEncuentros.Font, FontStyle.Bold);

        }

        private void Encuentro_MouseLeave(object sender, EventArgs e)
        {
            lbEncuentros.Font = new Font(lbEncuentros.Font, FontStyle.Regular);

        }

        private void Cancha_MouseEnter(object sender, EventArgs e)
        {
            lbCanchas.Font = new Font(lbCanchas.Font, FontStyle.Bold);

        }

        private void Cancha_MouseLeave(object sender, EventArgs e)
        {
            lbCanchas.Font = new Font(lbCanchas.Font, FontStyle.Regular);

        }
    }
}
