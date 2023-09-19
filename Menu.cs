using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    }
}
