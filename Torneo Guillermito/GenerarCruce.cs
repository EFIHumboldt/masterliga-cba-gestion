using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIPa
{
    public partial class GenerarCruce : Form
    {
        public string parametro;
        public bool plantilla;
        public bool modificado = false;
        public GenerarCruce(string parametro, bool plantilla)
        {
            InitializeComponent();
            this.parametro = parametro;
            this.plantilla = plantilla;
        }

        public event EventHandler ChildFormClosed;

        private void GenerarCruce_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Realiza las acciones necesarias antes de cerrar la ventana hija, si es necesario
            // ...
            // Dispara el evento ChildFormClosed
            ChildFormClosed?.Invoke(this, EventArgs.Empty);
        }

        private void GenerarCruce_Load(object sender, EventArgs e)
        {
            labelTituloCategoria.Text = "Categoría: " + parametro;

            if (plantilla)
            {
                labelError.Visible = false;
                btGenerarCruce.Enabled = true;
            }
            else
            {
                labelError.Visible = true;
                btGenerarCruce.Enabled = false;
            }

            Querys q = new Querys();
            DataTable dt = new DataTable();
            dt = q.ConsultarEncuentrosPosiciones(parametro);
            dgvEncuentroPosiciones.DataSource = dt;

            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.PrimaryScreen;


            if (screen.Bounds.Width * 9 / 10 < 649)
            {
                this.Width = screen.Bounds.Width * 9 / 10;
            }
            else
            {
                this.Width = 649;
            }
            if (screen.Bounds.Height * 9 / 10 < 831)
            {
                this.Height = screen.Bounds.Height * 9 / 10;
            }
            else
            {
                this.Height = 831;
            }

            /*
             * 0 -> ideq1
             * 1 -> ideq2
             * 2 -> nombre
             * 3 -> nombre2
             * 4 -> goles1
             * 5 -> goles2
             * 6 -> tipo (al pedo es 0)
             * 7 -> escudo1
             * 8 -> escudo2
             */

            //PASO 1 --> Detecto Equipos Participantes

            List<String> equiposParticipantes = new List<String>();
            List<String> idsParticipantes = new List<String>();

            foreach (DataGridViewRow dr in dgvEncuentroPosiciones.Rows)
            {
                if (!equiposParticipantes.Contains(dr.Cells[2].Value.ToString()))
                {
                    equiposParticipantes.Add(dr.Cells[2].Value.ToString());
                    idsParticipantes.Add(dr.Cells[0].Value.ToString());
                }
                if (!equiposParticipantes.Contains(dr.Cells[3].Value.ToString()))
                {
                    equiposParticipantes.Add(dr.Cells[3].Value.ToString());
                    idsParticipantes.Add(dr.Cells[1].Value.ToString());
                }

            }

            //PASO 2 --> Por equipo participante, lleno todos los datos correspondientes
            int c = 0;
            foreach (string equipo in equiposParticipantes)
            {
                int pj = 0;
                int gf = 0;
                int gc = 0;
                int pts = 0;
                int pp = 0;
                int pe = 0;
                int pg = 0;

                foreach (DataGridViewRow dr in dgvEncuentroPosiciones.Rows)
                {
                    string nombreLocal = dr.Cells[2].Value.ToString();
                    string nombreVisita = dr.Cells[3].Value.ToString();

                    int golLocal;
                    int golVisita;

                    if (dr.Cells[4].Value.ToString() == "") { golLocal = 0; }
                    else { golLocal = Int32.Parse(dr.Cells[4].Value.ToString()); }

                    if (dr.Cells[5].Value.ToString() == "") { golVisita = 0; }
                    else { golVisita = Int32.Parse(dr.Cells[5].Value.ToString()); }


                    if (equipo == nombreLocal)
                    {
                        pj++;
                        gf += golLocal;
                        gc += golVisita;

                        if (golLocal > golVisita)
                        {
                            pts += 3;
                            pg++;
                        }
                        else if (golLocal == golVisita)
                        {
                            pts += 1;
                            pe++;
                        }
                        else pp++;
                    }
                    else if (equipo == nombreVisita)
                    {
                        pj++;

                        gf += golVisita;
                        gc += golLocal;

                        if (golVisita > golLocal)
                        {
                            pts += 3;
                            pg++;
                        }
                        else if (golLocal == golVisita)
                        {
                            pts += 1;
                            pe++;
                        }
                        else pp++;
                    }
                }
                dgvTabla.Rows.Add(new string[] { "1", equipo, pts.ToString(), pj.ToString(), pg.ToString(), pe.ToString(), pp.ToString(), gf.ToString(), gc.ToString(), idsParticipantes.ElementAt(c) });
                c++;
            }

            OrdenarDataGridView(dgvTabla);

            int i = 1;
            foreach (DataGridViewRow fila in dgvTabla.Rows)
            {
                fila.Cells[0].Value = i;
                i++;
            }
        }

        public void OrdenarDataGridView(DataGridView dataGridView)
        {
            List<DataGridViewRow> rows = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                rows.Add(row);
            }

            rows.Sort(new CustomComparer());

            dataGridView.Rows.Clear();

            foreach (DataGridViewRow row in rows)
            {
                dataGridView.Rows.Add(row);
            }
        }

        public class CustomComparer : IComparer<DataGridViewRow>
        {
            public int Compare(DataGridViewRow x, DataGridViewRow y)
            {
                // Comparar por puntos (supongamos que los puntos están en la columna 2)
                int pointsX = Convert.ToInt32(x.Cells[2].Value);
                int pointsY = Convert.ToInt32(y.Cells[2].Value);
                int pointsComparison = pointsY.CompareTo(pointsX); // Orden descendente por puntos

                if (pointsComparison != 0)
                {
                    return pointsComparison; // Si los puntos son diferentes, ordenar por puntos
                }
                else
                {
                    int goalDifferenceX = Convert.ToInt32(x.Cells[7].Value) - Convert.ToInt32(x.Cells[8].Value);
                    int goalDifferenceY = Convert.ToInt32(y.Cells[7].Value) - Convert.ToInt32(y.Cells[8].Value);
                    int goalComparison = goalDifferenceY.CompareTo(goalDifferenceX);

                    if (goalComparison != 0)
                    {
                        return goalComparison;
                    }
                    else
                    {
                        int goalfavorX = Convert.ToInt32(x.Cells[7].Value);
                        int goalfavorY = Convert.ToInt32(y.Cells[7].Value);
                        int goalfavorComparison = goalfavorY.CompareTo(goalfavorX);

                        if (goalfavorComparison != 0)
                        {
                            return goalfavorComparison;
                        }
                        else
                        {
                            int goalcontraX = Convert.ToInt32(x.Cells[8].Value);
                            int goalcontraY = Convert.ToInt32(y.Cells[8].Value);
                            return goalcontraY.CompareTo(goalcontraX);
                        }
                    }
                }
            }
        }

        private void flechaArriba_Click(object sender, EventArgs e)
        {
            modificado = true;

            if (dgvTabla.SelectedRows[0].Index > 0)
            {
                DataGridViewRow selectedRow = dgvTabla.SelectedRows[0];
                int index = dgvTabla.SelectedRows[0].Index - 1;
                dgvTabla.Rows.Remove(selectedRow);
                dgvTabla.Rows.Insert(index, selectedRow);
                dgvTabla.CurrentCell = selectedRow.Cells[0];
            }
            int i = 1;
            foreach (DataGridViewRow fila in dgvTabla.Rows)
            {
                fila.Cells[0].Value = i;
                i++;
            }
        }

        private void flechaAbajo_Click(object sender, EventArgs e)
        {
            modificado = true;

            if (dgvTabla.SelectedRows[0].Index != dgvTabla.Rows.Count - 1)
            {
                DataGridViewRow selectedRow = dgvTabla.SelectedRows[0];
                int index = dgvTabla.SelectedRows[0].Index + 1;
                dgvTabla.Rows.Remove(selectedRow);
                dgvTabla.Rows.Insert(index, selectedRow);
                dgvTabla.CurrentCell = selectedRow.Cells[0];
            }
            int i = 1;
            foreach (DataGridViewRow fila in dgvTabla.Rows)
            {
                fila.Cells[0].Value = i;
                i++;
            }
        }

        private void btGenerarCruce_Click(object sender, EventArgs e)
        {
            Querys q = new Querys();
            if (modificado)
            {
                if (MessageBox.Show("Se han realizado modificaciones manuales en las posiciones de los equipos, ¿desea continuar de igual forma?", "Liga Infantil del Paraná", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dgvTabla.Rows)
                    {
                        q.rellenarCruce(row.Cells[9].Value.ToString(), parametro, row.Cells[0].Value.ToString());
                    }
                    MessageBox.Show("Se han generado los cruces de forma correcta, puede salir para ver los cruces en la ventana anterio.", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (MessageBox.Show("¿Está seguro que desea crear los cruces?", "Liga Infantil del Paraná", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dgvTabla.Rows)
                    {
                        q.rellenarCruce(row.Cells[9].Value.ToString(), parametro, row.Cells[0].Value.ToString());
                    }
                    MessageBox.Show("Se han generado los cruces de forma correcta, puede salir para ver los cruces en la ventana anterio.", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
