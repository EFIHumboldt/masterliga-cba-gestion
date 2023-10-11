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
    public partial class Zona : Form
    {

        public Zona()
        {
            InitializeComponent();
        }

        private void Zona_Load(object sender, EventArgs e)
        {
            Querys q = new Querys();
            List<string> zonas = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q"};

            List<String> categorias2 = new List<String>();
            categorias2.Clear();

            dgvCategoria.DataSource = q.LlenarTablaCategoria();
            dgvZona.DataSource = q.LlenarTablaZona();

            dgvZona.Columns[0].Visible = false;

            foreach (DataGridViewRow fila in dgvCategoria.Rows)
            { if (fila.Cells[0].Value != null) { string valor = fila.Cells[0].Value.ToString(); categorias2.Add(valor); } }

            comboZona1.DataSource = categorias2;
            comboZona3.DataSource = categorias2;

            comboZona2.DataSource = zonas;
            comboZona4.DataSource = zonas;

            comboZona3.Text = "";
            comboZona4.Text = "";
        }

        private void btAgregarCategoria_Click(object sender, EventArgs e)
        {
            Querys q = new Querys();
            q.InsertarCategoria(tbAgregarCategoria.Text);
            tbAgregarCategoria.Text = "";
            Zona_Load(null, null);
        }

        private void dgvCategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbModificarCategoria.Enabled = true;
            btModificarCategoria.Enabled = true;
            tbModificarCategoria.Text = dgvCategoria.SelectedRows[0].Cells[0].Value.ToString();
           
        }

        private void btModificarCategoria_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea modificar la categoria seleccionada?", "Toreno Guillermito", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Querys q = new Querys();
                q.actualizarCategoria(tbModificarCategoria.Text, dgvCategoria.SelectedRows[0].Cells[0].Value.ToString());
                tbModificarCategoria.Text = "";
                tbModificarCategoria.Enabled = false;
                btModificarCategoria.Enabled = false;
                Zona_Load(null, null);
            }
        }

        private void btAgregarZona_Click(object sender, EventArgs e)
        {
            Querys q = new Querys();
            q.InsertarZona(comboZona1.SelectedItem.ToString(), comboZona2.SelectedItem.ToString());
            Zona_Load(null, null);
        }

        private void dgvZona_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboZona3.Enabled = true;
            comboZona4.Enabled = true;
            btMoficiarZona.Enabled = true;

            comboZona3.SelectedItem = dgvZona.SelectedRows[0].Cells[1].Value.ToString();
            comboZona4.SelectedItem = dgvZona.SelectedRows[0].Cells[2].Value.ToString();
        }
    }
}
