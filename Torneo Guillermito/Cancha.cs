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
    public partial class Cancha : Form
    {
        public Cancha()
        {
            InitializeComponent();
        }

        private void Cancha_Load(object sender, EventArgs e)
        {
            Querys q = new Querys();
            dgvCanchas.DataSource = q.LlenarTablaCanchas();
        }
    }
}
