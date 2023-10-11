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

        private void btLimpiarCanchas_Click(object sender, EventArgs e)
        {
            tbNumero.Text = "";
            tbLongitud.Text = "";
            tbLatitud.Text = "";

        }

        private void btAgregarCancha_Click(object sender, EventArgs e)
        {
            if (tbNumero.Text != "" && tbLongitud.Text != "" && tbLatitud.Text != "")
            {
                Querys q = new Querys();
                q.InsertarCancha(tbNumero.Text, tbLatitud.Text, tbLatitud.Text);
                dgvCanchas.DataSource = q.LlenarTablaCanchas();

            }
        }

        private void btEliminarCancha_Click(object sender, EventArgs e)
        {
            if (dgvCanchas.SelectedRows.Count == 1)
            {
                if(MessageBox.Show("¿Está seguro que desea eliminar la cancha seleccionada?", "Torneo Guillermito", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Querys q = new Querys();
                    q.EliminarCancha(dgvCanchas.SelectedRows[0].Cells[0].Value.ToString());
                    dgvCanchas.DataSource = q.LlenarTablaCanchas();
                }
            }
        }
    }
}
