using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Torneo_Guillermito
{
    public partial class PlantillaCruce : Form
    {

        private string parametro;

        public event EventHandler ChildFormClosed;
        public List<String> listaAutoComplete = new List<String>();

        public PlantillaCruce(string parametro)
        {
            InitializeComponent();
            this.parametro = parametro;
        }

        private void PlantillaCruce_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Realiza las acciones necesarias antes de cerrar la ventana hija, si es necesario
            // ...
            // Dispara el evento ChildFormClosed
            ChildFormClosed?.Invoke(this, EventArgs.Empty);
        }

        private void dgv8vosOro_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PlantillaCruce_Load(object sender, EventArgs e)
        {

            Screen screen = Screen.PrimaryScreen;

            if (screen.Bounds.Width * 9 / 10 < 1776)
            {
                this.Width = screen.Bounds.Width * 9 / 10;
            }
            else
            {
                this.Width = 1776;
            }
            if (screen.Bounds.Height * 9 / 10 < 950)
            {
                this.Height = screen.Bounds.Height * 9 / 10;
            }
            else
            {
                this.Height = 950;
            }

            labelTituloCategoria.Text = "Plantilla para Categoría " + parametro;
            Querys q = new Querys();
            dgvPlantillaEquipos.DataSource = q.ConsultarPlantillaCategoria(parametro);
            llenarPlantillasCruces();
        }

        public void llenarPlantillasCruces()
        {
            Querys q = new Querys();
            List<String> list = new List<String>();
            int i = 1;
            DataTable dt = q.LlenarTablaCanchas();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row[0].ToString());
            }
            dgvFinalesOro.EditingControlShowing += dgv_EditingControlShowing;
            dgvFinalesPlata.EditingControlShowing += dgv_EditingControlShowing;
            dgv4tosOro.EditingControlShowing += dgv_EditingControlShowing;
            dgv4tosPlata.EditingControlShowing += dgv_EditingControlShowing;
            dgv8vosOro.EditingControlShowing += dgv_EditingControlShowing;
            dgv8vosPlata.EditingControlShowing += dgv_EditingControlShowing;
            dgv16vosOro.EditingControlShowing += dgv_EditingControlShowing;
            dgv16vosPlata.EditingControlShowing += dgv_EditingControlShowing;

            for (; i <= 4; i++)
            {
                DataGridViewRow fila = new DataGridViewRow();
                dgvFinalesOro.Rows.Add(fila);
                dgvFinalesOro.Rows[i - 1].Cells[0].Value = i;
                dgvFinalesOro.Rows[i - 1].Cells[2].Value = "VS";
                ((DataGridViewComboBoxCell)dgvFinalesOro.Rows[i - 1].Cells[5]).DataSource = list;
            }

            for (; i <= 8; i++)
            {
                DataGridViewRow fila = new DataGridViewRow();
                dgv4tosOro.Rows.Add(fila);
                dgv4tosOro.Rows[i - 5].Cells[0].Value = i;
                dgv4tosOro.Rows[i - 5].Cells[2].Value = "VS";
                ((DataGridViewComboBoxCell)dgv4tosOro.Rows[i - 5].Cells[5]).DataSource = list;
            }

            for (; i <= 16; i++)
            {
                DataGridViewRow fila = new DataGridViewRow();
                dgv8vosOro.Rows.Add(fila);
                dgv8vosOro.Rows[i - 9].Cells[0].Value = i;
                dgv8vosOro.Rows[i - 9].Cells[2].Value = "VS";
                ((DataGridViewComboBoxCell)dgv8vosOro.Rows[i - 9].Cells[5]).DataSource = list;
            }

            for (; i <= 32; i++)
            {
                DataGridViewRow fila = new DataGridViewRow();
                dgv16vosOro.Rows.Add(fila);
                dgv16vosOro.Rows[i - 17].Cells[0].Value = i;
                dgv16vosOro.Rows[i - 17].Cells[2].Value = "VS";
                ((DataGridViewComboBoxCell)dgv16vosOro.Rows[i - 17].Cells[5]).DataSource = list;
            }

            for (; i <= 36; i++)
            {
                DataGridViewRow fila = new DataGridViewRow();
                dgvFinalesPlata.Rows.Add(fila);
                dgvFinalesPlata.Rows[i - 33].Cells[0].Value = i;
                dgvFinalesPlata.Rows[i - 33].Cells[2].Value = "VS";
                ((DataGridViewComboBoxCell)dgvFinalesPlata.Rows[i - 33].Cells[5]).DataSource = list;
            }

            for (; i <= 40; i++)
            {
                DataGridViewRow fila = new DataGridViewRow();
                dgv4tosPlata.Rows.Add(fila);
                dgv4tosPlata.Rows[i - 37].Cells[0].Value = i;
                dgv4tosPlata.Rows[i - 37].Cells[2].Value = "VS";
                ((DataGridViewComboBoxCell)dgv4tosPlata.Rows[i - 37].Cells[5]).DataSource = list;
            }

            for (; i <= 48; i++)
            {
                DataGridViewRow fila = new DataGridViewRow();
                dgv8vosPlata.Rows.Add(fila);
                dgv8vosPlata.Rows[i - 41].Cells[0].Value = i;
                dgv8vosPlata.Rows[i - 41].Cells[2].Value = "VS";
                ((DataGridViewComboBoxCell)dgv8vosPlata.Rows[i - 41].Cells[5]).DataSource = list;
            }

            for (; i <= 64; i++)
            {
                DataGridViewRow fila = new DataGridViewRow();
                dgv16vosPlata.Rows.Add(fila);
                dgv16vosPlata.Rows[i - 49].Cells[0].Value = i;
                dgv16vosPlata.Rows[i - 49].Cells[2].Value = "VS";
                ((DataGridViewComboBoxCell)dgv16vosPlata.Rows[i - 49].Cells[5]).DataSource = list;
            }
        }
        private void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if ((((DataGridView)sender).CurrentCell.ColumnIndex == 1 || ((DataGridView)sender).CurrentCell.ColumnIndex == 3) && e.Control is System.Windows.Forms.TextBox textBox)
            {

                List<String> list = new List<String>();

                list.AddRange(TablaGeneral());
                list.AddRange(Ganadores());
                list.AddRange(Perdedores());

                listaAutoComplete = list;

                AutoCompleteStringCollection autoCompleteCollection = new AutoCompleteStringCollection();
                autoCompleteCollection.AddRange(list.ToArray());
                textBox.AutoCompleteCustomSource = autoCompleteCollection;
                textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            else if (((DataGridView)sender).CurrentCell.ColumnIndex == 4 && e.Control is System.Windows.Forms.TextBox textBox1)
            {
                textBox1.AutoCompleteMode = AutoCompleteMode.None;
                textBox1.AutoCompleteSource = AutoCompleteSource.None;
                textBox1.AutoCompleteCustomSource = null;
            }
        }

        static List<string> TablaGeneral()
        {
            List<string> lista = new List<string>();
            String elemento = null;
            for (int x = 1; x <= 64; x++)
            {
                elemento = x + "º TABLA GENERAL";
                lista.Add(elemento);
            }
            return lista;
        }


        static List<string> Ganadores()
        {
            List<string> lista = new List<string>();
            String elemento = null;
            for (int x = 1; x <= 64; x++)
            {
                elemento = "GANADOR " + x;
                lista.Add(elemento);
            }
            return lista;
        }


        static List<string> Perdedores()
        {
            List<string> lista = new List<string>();
            String elemento = null;
            for (int x = 1; x <= 64; x++)
            {
                elemento = "PERDEDOR " + x;
                lista.Add(elemento);
            }
            return lista;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            List<String> equiposPuestos = new List<String>();
            Carga carga = new Carga();
            carga.Show();
            carga.Hide();
            bool todo_correcto = true;

            for (int i = 0; i <= 63; i++) //8 para cuartos completos nomas
            {
                DataGridView tabla = new DataGridView();
                int mod = 0;

                if (i >= 0 && i <= 3)
                {
                    tabla = dgvFinalesOro;
                    mod = 4;
                }
                else if (i >= 4 && i <= 7)
                {
                    tabla = dgv4tosOro;
                    mod = 4;
                }
                else if (i >= 8 && i <= 15)
                {
                    tabla = dgv8vosOro;
                    mod = 8;
                }
                else if (i >= 16 && i <= 31)
                {
                    tabla = dgv16vosOro;
                    mod = 16;
                }
                else if (i >= 32 && i <= 35)
                {
                    tabla = dgvFinalesPlata;
                    mod = 4;
                }
                else if (i >= 36 && i <= 39)
                {
                    tabla = dgv4tosPlata;
                    mod = 4;
                }
                else if (i >= 40 && i <= 47)
                {
                    tabla = dgv8vosPlata;
                    mod = 8;
                }
                else if (i >= 48 && i <= 63)
                {
                    tabla = dgv16vosPlata;
                    mod = 16;
                }



                //TANTEO SI HAY ERRORES:

                //CASO 1 --> SI HAY AL MENOS UNO VACIO EN TOTAL, Y AL MENOS UNO LLENO (ENTRE LOS DISTINTO DE CANCHA)

                if ((string.IsNullOrEmpty((string)tabla.Rows[i % mod].Cells[1].Value) || string.IsNullOrEmpty((string)tabla.Rows[i % mod].Cells[3].Value) || string.IsNullOrEmpty((string)tabla.Rows[i % mod].Cells[4].Value) || string.IsNullOrEmpty((string)tabla.Rows[i % mod].Cells[5].Value))
                &&
                (!string.IsNullOrEmpty((string)tabla.Rows[i % mod].Cells[1].Value) || !string.IsNullOrEmpty((string)tabla.Rows[i % mod].Cells[3].Value) || !string.IsNullOrEmpty((string)tabla.Rows[i % mod].Cells[4].Value)))
                {
                    todo_correcto = false;
                    break; //?
                }

                //CASO 2 --> QUE PONGAS UNO QUE NO EXISTE (LOCAL)
                if (!(string.IsNullOrEmpty((string)tabla.Rows[i % mod].Cells[1].Value)))
                {
                    if (!(listaAutoComplete.Contains(((string)tabla.Rows[i % mod].Cells[1].Value))))
                    {
                        todo_correcto = false;
                        break; //?
                    }

                    //CASO 3 --> QUE PONGAS UNO QUE YA PUSISTE
                    if (equiposPuestos.Contains(((string)tabla.Rows[i % mod].Cells[1].Value)))
                    {
                        todo_correcto = false;
                        break; //?
                    }
                    else
                    {
                        equiposPuestos.Add(tabla.Rows[i % mod].Cells[1].Value.ToString());
                    }
                }

                //CASO 3 --> QUE PONGAS UNO QUE NO EXISTE (VISITA)
                if (!(string.IsNullOrEmpty((string)tabla.Rows[i % mod].Cells[3].Value)))
                {
                    if (!(listaAutoComplete.Contains(((string)tabla.Rows[i % mod].Cells[3].Value))))
                    {
                        todo_correcto = false;
                        break; //?
                    }

                    //CASO 3 --> QUE PONGAS UNO QUE YA PUSISTE
                    if (equiposPuestos.Contains(((string)tabla.Rows[i % mod].Cells[3].Value)))
                    {
                        todo_correcto = false;
                        break; //?
                    }
                    else
                    {
                        equiposPuestos.Add(tabla.Rows[i % mod].Cells[3].Value.ToString());
                    }
                }
                //CASO 4 --> QUE HAYA GANADOR X
                if (!(string.IsNullOrEmpty((string)tabla.Rows[i % mod].Cells[3].Value)) && !(string.IsNullOrEmpty((string)tabla.Rows[i % mod].Cells[1].Value)))
                {
                    if (tabla.Rows[i % mod].Cells[3].Value.ToString().Substring(0, 1) == "G")
                    {
                        string numero = tabla.Rows[i % mod].Cells[3].Value.ToString().Substring(8, tabla.Rows[i % mod].Cells[3].Value.ToString().Length - 8);
                        int n = Int32.Parse(numero) - 1;
                        DataGridView tablan = new DataGridView();
                        int modn = 0;
                        if (n >= 0 && n <= 3)
                        {
                            tablan = dgvFinalesOro;
                            modn = 4;
                        }
                        else if (n >= 4 && n <= 7)
                        {
                            tablan = dgv4tosOro;
                            modn = 4;
                        }
                        else if (n >= 8 && n <= 15)
                        {
                            tablan = dgv8vosOro;
                            modn = 8;
                        }
                        else if (n >= 16 && n <= 31)
                        {
                            tablan = dgv16vosOro;
                            modn = 16;
                        }
                        else if (n >= 32 && n <= 35)
                        {
                            tablan = dgvFinalesPlata;
                            modn = 4;
                        }
                        else if (n >= 36 && n <= 39)
                        {
                            tablan = dgv4tosPlata;
                            modn = 4;
                        }
                        else if (n >= 40 && n <= 47)
                        {
                            tablan = dgv8vosPlata;
                            modn = 8;
                        }
                        else if (n >= 48 && n <= 63)
                        {
                            tablan = dgv16vosPlata;
                            modn = 16;
                        }
                        if (string.IsNullOrEmpty((string)tablan.Rows[n % modn].Cells[1].Value))
                        {
                            todo_correcto = false;
                            break;
                        }
                    }
                    else if (tabla.Rows[i % mod].Cells[3].Value.ToString().Substring(0, 1) == "P")
                    {
                        string numero = tabla.Rows[i % mod].Cells[3].Value.ToString().Substring(9, tabla.Rows[i % mod].Cells[3].Value.ToString().Length - 9);
                        int n = Int32.Parse(numero) - 1;
                        DataGridView tablan = new DataGridView();
                        int modn = 0;
                        if (n >= 0 && n <= 3)
                        {
                            tablan = dgvFinalesOro;
                            modn = 4;
                        }
                        else if (n >= 4 && n <= 7)
                        {
                            tablan = dgv4tosOro;
                            modn = 4;
                        }
                        else if (n >= 8 && n <= 15)
                        {
                            tablan = dgv8vosOro;
                            modn = 8;
                        }
                        else if (n >= 16 && n <= 31)
                        {
                            tablan = dgv16vosOro;
                            modn = 16;
                        }
                        else if (n >= 32 && n <= 35)
                        {
                            tablan = dgvFinalesPlata;
                            modn = 4;
                        }
                        else if (n >= 36 && n <= 39)
                        {
                            tablan = dgv4tosPlata;
                            modn = 4;
                        }
                        else if (n >= 40 && n <= 47)
                        {
                            tablan = dgv8vosPlata;
                            modn = 8;
                        }
                        else if (n >= 48 && n <= 63)
                        {
                            tablan = dgv16vosPlata;
                            modn = 16;
                        }
                        if (string.IsNullOrEmpty((string)tablan.Rows[n % modn].Cells[1].Value))
                        {
                            todo_correcto = false;
                            break;
                        }
                    }

                    if (tabla.Rows[i % mod].Cells[1].Value.ToString().Substring(0, 1) == "G")
                    {
                        string numero = tabla.Rows[i % mod].Cells[1].Value.ToString().Substring(8, tabla.Rows[i % mod].Cells[1].Value.ToString().Length - 8);
                        int n = Int32.Parse(numero) - 1;
                        DataGridView tablan = new DataGridView();
                        int modn = 0;
                        if (n >= 0 && n <= 3)
                        {
                            tablan = dgvFinalesOro;
                            modn = 4;
                        }
                        else if (n >= 4 && n <= 7)
                        {
                            tablan = dgv4tosOro;
                            modn = 4;
                        }
                        else if (n >= 8 && n <= 15)
                        {
                            tablan = dgv8vosOro;
                            modn = 8;
                        }
                        else if (n >= 16 && n <= 31)
                        {
                            tablan = dgv16vosOro;
                            modn = 16;
                        }
                        else if (n >= 32 && n <= 35)
                        {
                            tablan = dgvFinalesPlata;
                            modn = 4;
                        }
                        else if (n >= 36 && n <= 39)
                        {
                            tablan = dgv4tosPlata;
                            modn = 4;
                        }
                        else if (n >= 40 && n <= 47)
                        {
                            tablan = dgv8vosPlata;
                            modn = 8;
                        }
                        else if (n >= 48 && n <= 63)
                        {
                            tablan = dgv16vosPlata;
                            modn = 16;
                        }
                        if (string.IsNullOrEmpty((string)tablan.Rows[n % modn].Cells[1].Value))
                        {
                            todo_correcto = false;
                            break;
                        }
                    }
                    else if (tabla.Rows[i % mod].Cells[1].Value.ToString().Substring(0, 1) == "P")
                    {
                        string numero = tabla.Rows[i % mod].Cells[1].Value.ToString().Substring(9, tabla.Rows[i % mod].Cells[1].Value.ToString().Length - 9);
                        int n = Int32.Parse(numero) - 1;
                        DataGridView tablan = new DataGridView();
                        int modn = 0;
                        if (n >= 0 && n <= 3)
                        {
                            tablan = dgvFinalesOro;
                            modn = 4;
                        }
                        else if (n >= 4 && n <= 7)
                        {
                            tablan = dgv4tosOro;
                            modn = 4;
                        }
                        else if (n >= 8 && n <= 15)
                        {
                            tablan = dgv8vosOro;
                            modn = 8;
                        }
                        else if (n >= 16 && n <= 31)
                        {
                            tablan = dgv16vosOro;
                            modn = 16;
                        }
                        else if (n >= 32 && n <= 35)
                        {
                            tablan = dgvFinalesPlata;
                            modn = 4;
                        }
                        else if (n >= 36 && n <= 39)
                        {
                            tablan = dgv4tosPlata;
                            modn = 4;
                        }
                        else if (n >= 40 && n <= 47)
                        {
                            tablan = dgv8vosPlata;
                            modn = 8;
                        }
                        else if (n >= 48 && n <= 63)
                        {
                            tablan = dgv16vosPlata;
                            modn = 16;
                        }
                        if (string.IsNullOrEmpty((string)tablan.Rows[n % modn].Cells[1].Value))
                        {
                            todo_correcto = false;
                            break;
                        }
                    }
                }

                //SI LLEGASTE HASTA ACA ES PORQUE NO MORISTE





            }

            if (todo_correcto) // SI SAFA TODO LO DE ARRIBA --> PIOOOOLA 
            {

                //MessageBox.Show("Planilla creada correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);


                Querys q = new Querys();
                q.FletearPlantilla(parametro);
                carga.Show();
                carga.label.Text = "Generando plantilla, aguarde un momento";
                carga.progress.Value = 0;

                for (int i = 0; i <= 63; i++)
                {
                    DataGridView tabla = new DataGridView();
                    int mod = 0;

                    if (i >= 0 && i <= 3)
                    {
                        tabla = dgvFinalesOro;
                        mod = 4;
                    }
                    else if (i >= 4 && i <= 7)
                    {
                        tabla = dgv4tosOro;
                        mod = 4;
                    }
                    else if (i >= 8 && i <= 15)
                    {
                        tabla = dgv8vosOro;
                        mod = 8;
                    }
                    else if (i >= 16 && i <= 31)
                    {
                        tabla = dgv16vosOro;
                        mod = 16;
                    }
                    else if (i >= 32 && i <= 35)
                    {
                        tabla = dgvFinalesPlata;
                        mod = 4;
                    }
                    else if (i >= 36 && i <= 39)
                    {
                        tabla = dgv4tosPlata;
                        mod = 4;
                    }
                    else if (i >= 40 && i <= 47)
                    {
                        tabla = dgv8vosPlata;
                        mod = 8;
                    }
                    else if (i >= 48 && i <= 63)
                    {
                        tabla = dgv16vosPlata;
                        mod = 16;
                    }
                    if (!string.IsNullOrEmpty((string)tabla.Rows[i % mod].Cells[1].Value))
                    {
                        int idLocal = Int32.Parse(dgvPlantillaEquipos.Rows.Cast<DataGridViewRow>()
                                  .Where(row => row.Cells[1].Value.ToString() == tabla.Rows[i % mod].Cells[1].Value.ToString()).FirstOrDefault()
                                  .Cells[0].Value.ToString());

                        int idVisita = Int32.Parse(dgvPlantillaEquipos.Rows.Cast<DataGridViewRow>()
                                      .Where(row => row.Cells[1].Value.ToString() == tabla.Rows[i % mod].Cells[3].Value.ToString()).FirstOrDefault()
                                      .Cells[0].Value.ToString());

                        string hora = tabla.Rows[i % mod].Cells[4].Value.ToString();
                        string cancha = tabla.Rows[i % mod].Cells[5].Value.ToString();
                        string tipo = tabla.Rows[i % mod].Cells[0].Value.ToString();

                        q.InsertarEncuentroPlantilla(idLocal.ToString(), idVisita.ToString(), hora, cancha, tipo);
                    }
                    carga.progress.Value = i;
                }

                carga.progress.Value = 64;
                carga.Close();

                MessageBox.Show("Planilla creada correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Se ha producido un error al generar la plantilla, verifique que los datos estén ingresados correctamente." +
                    " Debe tener en cuenta motivos como: " +
                    "\n\na) Algún problema de formato en el ingreso de la hora." +
                    "\n\nb) Si algun cruce tiene algún dato, el mismo debe tener todos completos." +
                    "\n\nc) El nombre de los equipos deben pertenecer a los nombres ya existentes." +
                    "\n\nd) Cada nombre debe ser ingresado solo una vez, es decir, no se permiten duplicados." +
                    "\n\ne) En caso de ingresar GANADOR/PEREDOR 'X', el partido 'X' debe estar completo.", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }



        }
    }
}
