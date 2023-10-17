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
    public partial class Equipo : Form
    {
        public Equipo()
        {
            InitializeComponent();
        }

        private void comboZona2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboZona1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Querys q = new Querys();

            DataTable dt = new DataTable();
            dt = q.LlenarTablaZonaFiltrado(comboEquipo1.Text);

            comboEquipo2.Items.Clear();

            foreach (DataRow fila in dt.Rows)
            { comboEquipo2.Items.Add(fila[0].ToString()); }
            if(comboEquipo2.Items.Count > 0) comboEquipo2.SelectedIndex = 0;
        }

        private async void Equipo_Load(object sender, EventArgs e)
        {
            Querys q = new Querys();
            DataTable dt = new DataTable();

            dt = q.LlenarTablaCategoria();
            foreach (DataRow fila in dt.Rows)
            { comboEquipo1.Items.Add(fila[0].ToString()); }
            comboEquipo1.SelectedIndex = 0;
            dgvEquipo1.DataSource = q.LlenarTablaClubFiltrado("");

            DataTable dt2 =  q.LlenarTablaEquipo();
            dgvEquipo2.DataSource = dt2;

            
            dgvEquipo1.Columns[0].Visible = false;
            dgvEquipo1.Columns[1].Width = 300;
            dgvEquipo2.Columns[0].Visible = false;

            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Querys q = new Querys();
            dgvEquipo1.DataSource = q.LlenarTablaClubFiltrado(tbEquipos1.Text);
        }

        private void dgvEquipo1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbEquipos2.Text = dgvEquipo1.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void btEquipos1_Click(object sender, EventArgs e)
        {
            Querys q = new Querys();

            if (dgvEquipo1.SelectedRows.Count == 1 && comboEquipo1.Text != "" && comboEquipo2.Text != "")
            {
                q.InsertarEquipo(dgvEquipo1.SelectedRows[0].Cells[0].Value.ToString(), comboEquipo1.Text, comboEquipo2.Text);
                dgvEquipo2.DataSource = q.LlenarTablaEquipo();
            }

        }

        private void btEliminarEquipo_Click(object sender, EventArgs e)
        {
           if(MessageBox.Show("¿Está seguro que desea eliminar el equipo seleccionado?", "Torneo Guillermito", MessageBoxButtons.YesNo) == DialogResult.Yes)
           {
                Querys q = new Querys();
                q.EliminarEquipo(dgvEquipo2.SelectedRows[0].Cells[0].Value.ToString());
                dgvEquipo2.DataSource = q.LlenarTablaEquipo();

           }
        }

        private void dgvEquipo2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btEliminarEquipo.Enabled = true;
        }
    }
}
