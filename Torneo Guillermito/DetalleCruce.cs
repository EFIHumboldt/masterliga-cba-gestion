using System;
using System.Collections;
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
    public partial class DetalleCruce : Form
    {
        public string categoria;
        public string tipoCruce;
        public string nombreL;
        public string nombreV;
        public string hora;
        public string fecha;
        public string cancha;
        public string gl;
        public string gv;
        public string pl;
        public string pv;
        public string idPartido;
        public string tipoNumero;
        public string idLocal;
        public string idVisita;

        public event EventHandler ChildFormClosed;

        public DetalleCruce(string categoria, string tipoCruce, string nombreL, string nombreV, string hora, string fecha, string cancha, string gl, string gv, string pl, string pv, string idPartido, string tipoNumero, string idLocal, string idVisita)
        {
            this.categoria = categoria;
            this.tipoCruce = tipoCruce;
            this.nombreL = nombreL;
            this.nombreV = nombreV;
            this.hora = hora;
            this.fecha = fecha;
            this.cancha = cancha;
            this.gl = gl;
            this.gv = gv;
            this.pl = pl;
            this.pv = pv;
            this.idPartido = idPartido;
            this.tipoNumero = tipoNumero;
            this.idLocal = idLocal;
            this.idVisita = idVisita;


            InitializeComponent();

        }
        private void DetalleCruce_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Realiza las acciones necesarias antes de cerrar la ventana hija, si es necesario
            // ...
            // Dispara el evento ChildFormClosed
            ChildFormClosed?.Invoke(this, EventArgs.Empty);
        }

        private void DetalleCruce_Load(object sender, EventArgs e)
        {
            //LLENAR COMBO Y CANCHA
            Querys q = new Querys();
            DataTable dt = new DataTable();

            dt = q.LlenarTablaCanchas();
            comboCanchaEncuentro.Items.Clear();
            foreach (DataRow row in dt.Rows) { comboCanchaEncuentro.Items.Add(row[0].ToString()); }

            dt = q.LlenarTablaFechas();
            comboFechaEncuentro.Items.Clear();
            foreach (DataRow row in dt.Rows) { comboFechaEncuentro.Items.Add(row[0].ToString()); }

            tbCategoriaEncuentro.Text = "Categoria " + categoria;
            tbZonaEncuentro.Text = tipoCruce;
            tbLocalEncuentro.Text = nombreL;
            tbVisitaEncuentro.Text = nombreV;
            tbHoraEncuentro.Text = hora;

            comboFechaEncuentro.SelectedItem = fecha;
            comboCanchaEncuentro.SelectedItem = cancha;

            tbGolesLocal.Text = gl;
            tbGolesVisita.Text = gv;
            tbPenalesLocal.Text = pl;
            tbPenalesVisita.Text = pv;
        }

        private void btModificarPartido_Click(object sender, EventArgs e)
        {
            if ((tbGolesVisita.Text != "" && tbGolesLocal.Text == "") || (tbGolesVisita.Text == "" && tbGolesLocal.Text != "") || (tbGolesLocal.Text == tbGolesVisita.Text && tbGolesLocal.Text != "" && (tbPenalesLocal.Text == "" || tbPenalesVisita.Text == "")) || (tbGolesLocal.Text != tbGolesVisita.Text && (tbPenalesLocal.Text != "" || tbPenalesVisita.Text != "")) || tbHoraEncuentro.Text == "" || comboFechaEncuentro.Text == "" || comboCanchaEncuentro.Text == "")
            {
                MessageBox.Show("Los datos son inválidos, revise e intente nuevamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("¿Seguro que desea modificar el cruce?", "Torneo Guillermito", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Querys q = new Querys();
                q.ModificarYRellenarCruce(idPartido, tipoNumero, tbHoraEncuentro.Text, comboFechaEncuentro.SelectedItem.ToString(), comboCanchaEncuentro.SelectedItem.ToString()
                    , tbGolesLocal.Text, tbGolesVisita.Text, tbPenalesLocal.Text, tbPenalesVisita.Text, idLocal, idVisita, categoria);

                this.Close();
            }
        }
    }
}
