using MySql.Data.MySqlClient;
using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Torneo_Guillermito.Properties;
using System.Drawing.Imaging;
using System.Net.Sockets;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Torneo_Guillermito
{


    public partial class gbEncuentros : Form
    {

        public string rutaLocalArchivo = "";
        private DataTable dtFiltro;
        private DataTable dtFiltro2; //solo para el de equipos por ahora, que tiene dos datagrid al mismo tiempo
        private String conexionFoto;


        public gbEncuentros()
        {
            InitializeComponent();
        }

        private async void Menu_Load(object sender, EventArgs e)
        {
            string filePath = "conexionFoto.txt";
            Screen screen = Screen.PrimaryScreen;

            //MessageBox.Show((screen.Bounds.Width * 9 / 10).ToString() + " " + screen.Bounds.Height.ToString());
            if (screen.Bounds.Width * 9 / 10 < 1376)
            {
                this.Width = screen.Bounds.Width * 9 / 10;
            }
            else
            {
                this.Width = 1376;
            }
            if (screen.Bounds.Height * 9 / 10 < 961)
            {
                this.Height = screen.Bounds.Height * 9 / 10;
            }
            else
            {
                this.Height = 961;
            }

            gbCanchas.Visible = true;
            gbClubes.Visible = true;
            gbCruces.Visible = true;
            gbPartidos.Visible = true;
            gbEquipos.Visible = true;
            gbCyZ.Visible = true;
            gbCanchas.Visible = false;
            gbClubes.Visible = false;
            gbCruces.Visible = false;
            gbPartidos.Visible = false;
            gbEquipos.Visible = false;
            gbCyZ.Visible = false;
            try
            {
                // Lee el contenido del archivo de bloc de notas.
                conexionFoto = File.ReadAllText(filePath);
                // Imprime el contenido en la consola.

            }
            catch (Exception ef)
            {
                MessageBox.Show(ef.Message);
            }

            Querys q = new Querys();
            dtFiltro = new DataTable();

            List<String> list = new List<String>();
            DataTable dt = q.LlenarTablaCategoria();


            foreach (DataRow row in dt.Rows)
            {
                list.Add(row[0].ToString());
            }
            comboCategoriaCruce.DataSource = list;

            label2.BackColor = Color.FromArgb(240, 240, 240);
        }

        private async void subirFoto1(string nombre)
        {
            string serverUrl = conexionFoto + "/imagen.php"; // Reemplaza con la URL de tu API
            string fieldName = "archivo"; // Nombre del campo de entrada del archivo en el formulario HTML

            Image image = pbCancha1.Image;
            byte[] fileBytes;
            // Crea un objeto MemoryStream para almacenar los bytes de la imagen
            using (MemoryStream stream = new MemoryStream())
            {
                // Convierte la imagen en formato PNG y guárdala en el MemoryStream
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                // Obtiene los bytes de la imagen
                fileBytes = stream.ToArray();

                // Ahora, 'fileBytes' contiene la imagen en formato PNG en forma de arreglo de bytes
            }
            using (HttpClient client = new HttpClient())
            using (var content = new MultipartFormDataContent())
            {
                var fileContent = new ByteArrayContent(fileBytes);

                content.Add(fileContent, fieldName, nombre);

                var response = await client.PostAsync(serverUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    //MessageBox.Show("Archivo cargado con éxito.");
                }
                else
                {
                    MessageBox.Show("Error al subir la imagen al servidor", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void subirFoto2(string nombre)
        {
            string serverUrl = conexionFoto + "/imagen.php"; // Reemplaza con la URL de tu API
            string fieldName = "archivo"; // Nombre del campo de entrada del archivo en el formulario HTML

            Image image = pbCancha2.Image;
            byte[] fileBytes;
            // Crea un objeto MemoryStream para almacenar los bytes de la imagen
            using (MemoryStream stream = new MemoryStream())
            {
                // Convierte la imagen en formato PNG y guárdala en el MemoryStream
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                // Obtiene los bytes de la imagen
                fileBytes = stream.ToArray();

                // Ahora, 'fileBytes' contiene la imagen en formato PNG en forma de arreglo de bytes
            }
            using (HttpClient client = new HttpClient())
            using (var content = new MultipartFormDataContent())
            {
                var fileContent = new ByteArrayContent(fileBytes);

                content.Add(fileContent, fieldName, nombre);

                var response = await client.PostAsync(serverUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    //MessageBox.Show("Archivo cargado con éxito.");
                }
                else
                {
                    MessageBox.Show("Error al subir la imagen al servidor", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void gbEquipos_Enter(object sender, EventArgs e)
        {

        }

        private void btClubes_Click(object sender, EventArgs e)
        {
            //
            pbCancha1.Image = Resources.nada;
            pbCancha2.Image = Resources.nada;


            btModificarClub.Enabled = false;
            tbNombreClub2.Text = "";
            tbNombreClub2.Enabled = false;
            label4.Visible = true;
            label2.Visible = true;
            label2.BackColor = Color.FromArgb(240, 240, 240);
            lbAdvertencia.Visible = true;
            //
            dgvClub.Rows.Clear();
            gbClubes.Visible = true;
            gbCanchas.Visible = false;
            gbCyZ.Visible = false;
            gbEquipos.Visible = false;
            gbPartidos.Visible = false;
            gbCruces.Visible = false;
            //

            Querys q = new Querys();

            dtFiltro = q.LlenarTablaClub();

            foreach (DataRow row in dtFiltro.Rows)
            {
                dgvClub.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
            }
            lbAdvertencia.Visible = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            btClubes_Click(null, null);
        }

        private void btCyZ_Click(object sender, EventArgs e)
        {
            gbClubes.Visible = false;
            gbCanchas.Visible = false;
            gbCyZ.Visible = true;
            gbEquipos.Visible = false;
            gbPartidos.Visible = false;
            gbCruces.Visible = false;

            Querys q = new Querys();
            List<string> zonas = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q" };
            List<String> categorias2 = new List<String>();
            categorias2.Clear();

            dtFiltro = q.LlenarTablaCategoria();

            dgvCategoria.Rows.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                dgvCategoria.Rows.Add(new string[] { row[0].ToString() });
            }

            dtFiltro = q.LlenarTablaZona();

            dgvZona.Rows.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                dgvZona.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
            }

            foreach (DataGridViewRow fila in dgvCategoria.Rows)
            { if (fila.Cells[0].Value != null) { string valor = fila.Cells[0].Value.ToString(); categorias2.Add(valor); } }

            comboZona1.DataSource = categorias2;
            comboZona2.DataSource = zonas;

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            btCyZ_Click(null, null);
        }

        private void btEquipos_Click(object sender, EventArgs e)
        {
            gbClubes.Visible = false;
            gbCanchas.Visible = false;
            gbCyZ.Visible = false;
            gbEquipos.Visible = true;
            gbPartidos.Visible = false;
            gbCruces.Visible = false;

            tbEquipos1.Text = "";
            textBox2.Text = "";


            Querys q = new Querys();
            DataTable dt = new DataTable();

            comboEquipo1.Items.Clear();
            dt = q.LlenarTablaCategoria();
            foreach (DataRow fila in dt.Rows)
            { comboEquipo1.Items.Add(fila[0].ToString()); }
            q.LlenarTablaClubFiltrado("");

            dtFiltro = q.LlenarTablaClubFiltrado("");

            dgvEquipo1.Rows.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                dgvEquipo1.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
            }

            dtFiltro2 = q.LlenarTablaEquipo();

            dgvEquipo2.Rows.Clear();
            foreach (DataRow row in dtFiltro2.Rows)
            {
                dgvEquipo2.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString() });
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            btEquipos_Click(null, null);
        }

        private void btCanchas_Click(object sender, EventArgs e)
        {
            gbClubes.Visible = false;
            gbCanchas.Visible = true;
            gbCyZ.Visible = false;
            gbEquipos.Visible = false;
            gbPartidos.Visible = false;
            gbCruces.Visible = false;

            Querys q = new Querys();
            dtFiltro = q.LlenarTablaCanchas();

            dgvCanchas.Rows.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                dgvCanchas.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
            }

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            btCanchas_Click(null, null);
        }

        private void btEncuentros_Click(object sender, EventArgs e)
        {
            gbClubes.Visible = false;
            gbCanchas.Visible = false;
            gbCyZ.Visible = false;
            gbEquipos.Visible = false;
            gbPartidos.Visible = true;
            gbCruces.Visible = false;

            comboFiltroEncuentros1.Items.Clear();
            comboFiltroEncuentros3.Items.Clear();

            List<String> list = new List<String>();
            Querys query = new Querys();

            dtFiltro = query.LlenarTablaCategoria();

            comboCategoriaEncuentro.Items.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                comboCategoriaEncuentro.Items.Add(row[0].ToString());
                comboFiltroEncuentros1.Items.Add(row[0].ToString());
            }

            dtFiltro = query.LlenarTablaCanchas();

            comboCanchaEncuentro1.Items.Clear();
            comboCanchaEncuentro2.Items.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                comboCanchaEncuentro1.Items.Add(row[0].ToString());
                comboCanchaEncuentro2.Items.Add(row[0].ToString());

            }
            comboCanchaEncuentro2.SelectedItem = null;

            dtFiltro = query.LlenarTablaFechas();

            comboFechaEncuentro1.Items.Clear();
            comboFechaEncuentro2.Items.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                comboFechaEncuentro1.Items.Add(row[0].ToString());
                comboFechaEncuentro2.Items.Add(row[0].ToString());
                comboFiltroEncuentros3.Items.Add(row[0].ToString());
            }
            comboFechaEncuentro2.SelectedItem = null;

            dtFiltro = query.LlenarTablaEncuentro();
            dgvEncuentros.Rows.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                dgvEncuentros.Rows.Add(new string[] {
                    row[0].ToString(), // ID
                    row[1].ToString(), // Nombre Local
                    row[2].ToString(), // Resultado Local
                    "-",
                    row[3].ToString(), // Resultado Visita
                    row[4].ToString(), // Nombre Visita
                    row[5].ToString(), // Categoria
                    row[6].ToString(), // Zona Local
                    row[7].ToString(), // Zona Visita
                    row[8].ToString(), // Fecha
                    row[9].ToString(), // Hora
                    row[10].ToString(), // Cancha
                });
            }


        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            btEncuentros_Click(null, null);
        }

        private void btCruces_Click(object sender, EventArgs e)
        {
            gbClubes.Visible = false;
            gbCanchas.Visible = false;
            gbCyZ.Visible = false;
            gbEquipos.Visible = false;
            gbPartidos.Visible = false;
            gbCruces.Visible = true;


        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            btCruces_Click(null, null);
        }

        private void pbCancha1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbCancha1.Image = new Bitmap(openFileDialog.FileName);
            }
        }

        private void btAgregarCancha_Click(object sender, EventArgs e)
        {
            if (tbNombreClub1.Text != "")
            {
                byte[] imageBytes1;
                byte[] imageBytes2;

                MemoryStream ms1 = new MemoryStream();
                MemoryStream ms2 = new MemoryStream();
                pbCancha1.Image.Save(ms1, ImageFormat.Png);
                Resources.nada.Save(ms2, ImageFormat.Png);

                imageBytes1 = ms1.ToArray();
                imageBytes2 = ms2.ToArray();

                if (!imageBytes1.SequenceEqual(imageBytes2))
                {
                    string nombreArchivo = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";

                    Querys q = new Querys();
                    q.InsertarClub(tbNombreClub1.Text.ToUpper(), nombreArchivo);

                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

                    string rutaCompleta = Path.Combine(Resources.carpetaFotosServer, nombreArchivo);

                    subirFoto1(nombreArchivo);


                    DataTable dt = new DataTable();
                    dt = q.LlenarTablaClub();
                    dgvClub.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dgvClub.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                    }
                    lbAdvertencia.Visible = true;
                    tbNombreClub1.Text = "";
                    pbCancha1.Image = Resources.nada;
                }
                else
                {
                    if (MessageBox.Show("No ha insertado una foto para el escudo, ¿Desea agregar el club sin escudo? Se pondra un escudo por defecto", "Torneo Guillermito", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string nombreArchivo = "0.png";

                        Querys q = new Querys();
                        q.InsertarClub(tbNombreClub1.Text.ToUpper(), nombreArchivo);


                        DataTable dt = new DataTable();
                        dt = q.LlenarTablaClub();
                        dgvClub.Rows.Clear();
                        foreach (DataRow row in dt.Rows)
                        {
                            dgvClub.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                        }
                        lbAdvertencia.Visible = true;
                        tbNombreClub1.Text = "";
                        pbCancha1.Image = Resources.nada;
                    }

                }




            }
            else
            {
                MessageBox.Show("Debe completar todos los datos obligatorios para insertar un club", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        // HAY QUE PROBAR SI SE LIBERA EL IF, SINO NI BORRAR LA FOTO NO CREO QUE ESTEN AHI CAMBIANDO FOTOS A DOS MANOS.

        private void btEliminarClub_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea eliminar el club seleccionado?", "Torneo Guillermito", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Querys q = new Querys();
                q.EliminarClub(dgvClub.SelectedRows[0].Cells[0].Value.ToString());

                tbNombreClub2.Text = "";
                pbCancha2.Image = Resources.nada;

                btClubes_Click(null, null);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PlantillaCruce plantillaCruce = new PlantillaCruce(comboCategoriaCruce.SelectedItem.ToString());
            plantillaCruce.ChildFormClosed += ChildFormClosedHandlerPlantilla;
            plantillaCruce.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dgvCruces.Rows.Count > 0)
            {
                GenerarCruce generarCruce = new GenerarCruce(comboCategoriaCruce.SelectedItem.ToString(), true);
                generarCruce.ChildFormClosed += ChildFormClosedHandlerTabla;
                generarCruce.Show();
            }
            else
            {
                GenerarCruce generarCruce = new GenerarCruce(comboCategoriaCruce.SelectedItem.ToString(), false);
                generarCruce.ChildFormClosed += ChildFormClosedHandlerTabla;
                generarCruce.Show();
            }

        }

        private void ChildFormClosedHandlerTabla(object sender, EventArgs e)
        {
            comboCategoriaCruce_SelectedIndexChanged(null, null);
        }

        private void ChildFormClosedHandlerPlantilla(object sender, EventArgs e)
        {
            comboCategoriaCruce_SelectedIndexChanged(null, null);
        }

        private void ChildFormClosedHandlerEditar(object sender, EventArgs e)
        {
            comboCategoriaCruce_SelectedIndexChanged(null, null);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            tbNombreClub2.Focus();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            label4.Visible = false;
            tbNombreClub1.Focus();
        }

        private void tbNombreClub1_Leave(object sender, EventArgs e)
        {
            if (tbNombreClub1.Text == "")
            {
                label4.Visible = true;
            }
        }

        private void tbNombreClub2_Leave(object sender, EventArgs e)
        {
            if (tbNombreClub2.Text == "")
            {
                label2.BackColor = Color.White;
                label2.Visible = true;

            }
        }

        private void tbNombreClub1_Enter(object sender, EventArgs e)
        {
            label4.Visible = false;
        }

        private void tbNombreClub2_Enter(object sender, EventArgs e)
        {
            label2.Visible = false;
        }

        private void tbNombreClubFiltro_TextChanged(object sender, EventArgs e)
        {
            var filasFiltradas = dtFiltro.Clone();

            string busqueda = tbNombreClubFiltro.Text.ToUpper();

            if (!string.IsNullOrEmpty(busqueda))
            {
                var filasCoincidentes = dtFiltro.AsEnumerable().Where(row => row.Field<string>(1).Contains(busqueda)).ToList();

                if (filasCoincidentes.Count > 0)
                {
                    filasCoincidentes.ForEach(row => filasFiltradas.ImportRow(row));

                    dgvClub.Rows.Clear();
                    foreach (DataRow row in filasFiltradas.Rows)
                    {
                        dgvClub.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                    }

                }
                else
                {
                    dgvClub.DataSource = null;
                }
            }
            else
            {
                dgvClub.Rows.Clear();
                foreach (DataRow row in dtFiltro.Rows)
                {
                    dgvClub.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                }
            }
        }

        private void btLimpiarCanchas_Click_1(object sender, EventArgs e)
        {
            tbNombreClub1.Text = "";
            label4.Visible = true;
            pbCancha1.Image = Resources.nada;
        }

        private void dgvClub_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label2.Visible = false;
            tbNombreClub2.Text = dgvClub.SelectedRows[0].Cells[1].Value.ToString();
            lbAdvertencia.Visible = false;
            //PONER FOTO DEL CLUB QUE SELECCIONO

            string imageUrl = conexionFoto + "/apis_sarmientito/ESCUDOS/" + dgvClub.SelectedRows[0].Cells[2].Value.ToString(); // Reemplaza con la URL de tu imagen remota
            MessageBox.Show("esta conectado a sarmiento");
            // Descargar la imagen desde la URL
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    byte[] imageBytes = webClient.DownloadData(imageUrl);
                    using (var stream = new System.IO.MemoryStream(imageBytes))
                    {
                        Image image = Image.FromStream(stream);
                        pbCancha2.Image = image;
                        pbCancha2Control.Image = image;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al descargar la imagen: " + ex.Message);
                }
            }
        }

        private void tbNombreClub2_TextChanged(object sender, EventArgs e)
        {
            label2.Visible = false;
            tbNombreClub2.Enabled = true;
            btModificarClub.Enabled = true;

        }

        private void dgvCategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbModificarCategoria.Enabled = true;
            btModificarCategoria.Enabled = true;
            tbModificarCategoria.Text = dgvCategoria.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void dgvCategoria_Leave(object sender, EventArgs e)
        {

        }

        private void dgvZona_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvZona_Leave(object sender, EventArgs e)
        {

        }

        private void btEliminarCategoria_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea eliminar la categoría seleccionada?", "Torneo Guillermito", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Querys q = new Querys();
                q.EliminarCategoria(dgvCategoria.SelectedRows[0].Cells[0].Value.ToString());
                tbModificarCategoria.Text = "";

                DataTable dt = q.LlenarTablaCategoria();

                dgvCategoria.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dgvCategoria.Rows.Add(new string[] { row[0].ToString() });
                }


                List<string> zonas = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q" };
                List<String> categorias2 = new List<String>();
                categorias2.Clear();

                dtFiltro = q.LlenarTablaZona();

                dgvZona.Rows.Clear();
                foreach (DataRow row in dtFiltro.Rows)
                {
                    dgvZona.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                }

                foreach (DataGridViewRow fila in dgvCategoria.Rows)
                { if (fila.Cells[0].Value != null) { string valor = fila.Cells[0].Value.ToString(); categorias2.Add(valor); } }

                comboZona1.DataSource = categorias2;
                comboZona2.DataSource = zonas;

            }
        }

        private void btEliminarZona_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea eliminar la zona seleccionada?", "Torneo Guillermito", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Querys q = new Querys();
                q.EliminarZona(dgvZona.SelectedRows[0].Cells[0].Value.ToString());

                dtFiltro = q.LlenarTablaZona();
                dgvZona.Rows.Clear();
                foreach (DataRow row in dtFiltro.Rows)
                {
                    dgvZona.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                }

            }
        }

        private void btAgregarCategoria_Click(object sender, EventArgs e)
        {
            if (tbAgregarCategoria.Text != "")
            {
                Querys q = new Querys();
                q.InsertarCategoria(tbAgregarCategoria.Text);
                tbAgregarCategoria.Text = "";


                DataTable dt = q.LlenarTablaCategoria();

                dgvCategoria.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dgvCategoria.Rows.Add(new string[] { row[0].ToString() });
                }


                List<string> zonas = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q" };
                List<String> categorias2 = new List<String>();
                categorias2.Clear();

                dtFiltro = q.LlenarTablaZona();

                dgvZona.Rows.Clear();
                foreach (DataRow row in dtFiltro.Rows)
                {
                    dgvZona.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                }

                foreach (DataGridViewRow fila in dgvCategoria.Rows)
                { if (fila.Cells[0].Value != null) { string valor = fila.Cells[0].Value.ToString(); categorias2.Add(valor); } }

                comboZona1.DataSource = categorias2;
                comboZona2.DataSource = zonas;

            }
            else
            {
                MessageBox.Show("Error al insertar la categoría, verifique que los datos esten completos y correctos.", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btAgregarZona_Click(object sender, EventArgs e)
        {
            Querys q = new Querys();
            q.InsertarZona(comboZona1.SelectedItem.ToString(), comboZona2.SelectedItem.ToString());

            dtFiltro = q.LlenarTablaZona();
            dgvZona.Rows.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                dgvZona.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
            }

        }

        private void btModificarCategoria_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea modificar la categoria seleccionada?", "Toreno Guillermito", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (tbModificarCategoria.Text != "")
                {
                    Querys q = new Querys();
                    q.actualizarCategoria(tbModificarCategoria.Text, dgvCategoria.SelectedRows[0].Cells[0].Value.ToString());
                    DataTable dt = q.LlenarTablaCategoria();

                    dgvCategoria.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dgvCategoria.Rows.Add(new string[] { row[0].ToString() });
                    }
                    tbModificarCategoria.Text = "";
                    tbModificarCategoria.Enabled = false;
                    btModificarCategoria.Enabled = false;


                    List<string> zonas = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q" };
                    List<String> categorias2 = new List<String>();
                    categorias2.Clear();

                    dtFiltro = q.LlenarTablaZona();

                    dgvZona.Rows.Clear();
                    foreach (DataRow row in dtFiltro.Rows)
                    {
                        dgvZona.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                    }

                    foreach (DataGridViewRow fila in dgvCategoria.Rows)
                    { if (fila.Cells[0].Value != null) { string valor = fila.Cells[0].Value.ToString(); categorias2.Add(valor); } }

                    comboZona1.DataSource = categorias2;

                    comboZona2.DataSource = zonas;

                }
                else
                {

                }
            }
        }

        private void btMoficiarZona_Click(object sender, EventArgs e)
        {
            //FALTA
        }

        private void tbEquipos1_TextChanged(object sender, EventArgs e)
        {

            var filasFiltradas = dtFiltro.Clone();

            string busqueda = tbEquipos1.Text.ToUpper();
            if (!string.IsNullOrEmpty(busqueda))
            {
                var filasCoincidentes = dtFiltro.AsEnumerable().Where(row => row.Field<string>(1).Contains(busqueda)).ToList();

                if (filasCoincidentes.Count > 0)
                {
                    filasCoincidentes.ForEach(row => filasFiltradas.ImportRow(row));

                    dgvEquipo1.Rows.Clear();
                    foreach (DataRow row in filasFiltradas.Rows)
                    {
                        dgvEquipo1.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                    }

                }
                else
                {
                    dgvEquipo1.DataSource = null;
                }
            }
            else
            {
                dgvEquipo1.Rows.Clear();
                foreach (DataRow row in dtFiltro.Rows)
                {
                    dgvEquipo1.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                }
            }
        }

        private void dgvEquipo1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbEquipos2.Text = dgvEquipo1.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void comboEquipo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Querys q = new Querys();

            DataTable dt = new DataTable();
            dt = q.LlenarTablaZonaFiltrado(comboEquipo1.Text);

            comboEquipo2.Items.Clear();

            foreach (DataRow fila in dt.Rows)
            { comboEquipo2.Items.Add(fila[0].ToString()); }
            if (comboEquipo2.Items.Count > 0) comboEquipo2.SelectedIndex = 0;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var filasFiltradas = dtFiltro2.Clone();

            string busqueda = textBox2.Text.ToUpper();

            if (!string.IsNullOrEmpty(busqueda))
            {
                var filasCoincidentes = dtFiltro2.AsEnumerable().Where(row => row.Field<string>(1).Contains(busqueda)).ToList();

                if (filasCoincidentes.Count > 0)
                {
                    filasCoincidentes.ForEach(row => filasFiltradas.ImportRow(row));

                    dgvEquipo2.Rows.Clear();
                    foreach (DataRow row in filasFiltradas.Rows)
                    {
                        dgvEquipo2.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString() });
                    }

                }
                else
                {
                    dgvEquipo2.DataSource = null;
                }
            }
            else
            {
                dgvEquipo2.Rows.Clear();
                foreach (DataRow row in dtFiltro2.Rows)
                {
                    dgvEquipo2.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString() });
                }
            }
        }

        private void btEquipos1_Click(object sender, EventArgs e)
        {
            Querys q = new Querys();

            if (dgvEquipo1.SelectedRows.Count == 1 && comboEquipo1.Text != "" && comboEquipo2.Text != "")
            {
                q.InsertarEquipo(dgvEquipo1.SelectedRows[0].Cells[0].Value.ToString(), comboEquipo1.Text, comboEquipo2.Text);

                dtFiltro2 = q.LlenarTablaEquipo();
                dgvEquipo2.Rows.Clear();
                foreach (DataRow row in dtFiltro2.Rows)
                {
                    dgvEquipo2.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString() });
                }

            }
            else
            {
                MessageBox.Show("Error al insertar el equipo, asegúrese de que todos los datos estén completos.", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvEquipo1_Leave(object sender, EventArgs e)
        {

        }

        private void btEliminarEquipo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea eliminar el equipo seleccionado?", "Torneo Guillermito", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Querys q = new Querys();
                q.EliminarEquipo(dgvEquipo2.SelectedRows[0].Cells[0].Value.ToString());

                dtFiltro2 = q.LlenarTablaEquipo();
                dgvEquipo2.Rows.Clear();
                foreach (DataRow row in dtFiltro2.Rows)
                {
                    dgvEquipo2.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString() });
                }

            }
        }

        private void dgvEquipo2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btEliminarEquipo.Enabled = true;
        }

        private void dgvEquipo2_Leave(object sender, EventArgs e)
        {
            //btEliminarEquipo.Enabled = false;
        }

        private void btEliminarCancha_Click(object sender, EventArgs e)
        {
            if (dgvCanchas.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("¿Está seguro que desea eliminar la cancha seleccionada?", "Torneo Guillermito", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Querys q = new Querys();
                    q.EliminarCancha(dgvCanchas.SelectedRows[0].Cells[0].Value.ToString());

                    dtFiltro = q.LlenarTablaCanchas();
                    dgvCanchas.Rows.Clear();
                    foreach (DataRow row in dtFiltro.Rows)
                    {
                        dgvCanchas.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                    }

                    tbNumero.Text = "";
                    tbLatitud.Text = "";
                    tbLongitud.Text = "";
                }
            }
        }

        private void btLimpiarCancha_Click(object sender, EventArgs e)
        {
            tbNumero.Text = "";
            tbLongitud.Text = "";
            tbLatitud.Text = "";
        }

        private void btAgregarCancha1_Click(object sender, EventArgs e)
        {
            if (tbNumero.Text != "" && tbLongitud.Text != "" && tbLatitud.Text != "")
            {
                Querys q = new Querys();
                q.InsertarCancha(tbNumero.Text, tbLatitud.Text, tbLatitud.Text);

                dtFiltro = q.LlenarTablaCanchas();
                dgvCanchas.Rows.Clear();
                foreach (DataRow row in dtFiltro.Rows)
                {
                    dgvCanchas.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                }
                tbNumero.Text = "";
                tbLatitud.Text = "";
                tbLongitud.Text = "";
            }
            else
            {
                MessageBox.Show("Complete todos los campos para agregar la cancha correspondiente o asegurese que el numero de cancha no sea uno existente.", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gbPartidos_Enter(object sender, EventArgs e)
        {

        }

        private void comboCategoriaEncuentro_SelectedIndexChanged(object sender, EventArgs e)
        {

            Querys q = new Querys();
            List<String> lista = new List<String>();
            dtFiltro = q.LlenarTablaZonaFiltrado(comboCategoriaEncuentro.SelectedItem.ToString());

            foreach (DataRow row in dtFiltro.Rows)
            {
                lista.Add(row[0].ToString());
            }
            lista.Add("INTERZONAL");
            comboZonaEncuentro.DataSource = lista;
        }

        private void comboZonaEncuentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Querys q = new Querys();

            List<String> listaNombre = new List<String>();
            List<String> listaID = new List<String>();

            if (comboZonaEncuentro.Text == "INTERZONAL")
            {
                dtFiltro = q.LlenarTablaEquipoFiltradoCategoriaZona(comboCategoriaEncuentro.Text, "%");
            }
            else
            {
                dtFiltro = q.LlenarTablaEquipoFiltradoCategoriaZona(comboCategoriaEncuentro.Text, comboZonaEncuentro.Text);
            }
            comboIDvisita.Items.Clear();
            comboIDlocal.Items.Clear();
            comboVisitaEncuentro1.Items.Clear();
            comboLocalEncuentro1.Items.Clear();

            foreach (DataRow row in dtFiltro.Rows)
            {
                comboIDlocal.Items.Add(row[0].ToString());
                comboIDvisita.Items.Add(row[0].ToString());
                comboLocalEncuentro1.Items.Add(row[1].ToString());
                comboVisitaEncuentro1.Items.Add(row[1].ToString());
            }
        }

        private void btAgregarEncuentro_Click(object sender, EventArgs e)
        {
            Querys q = new Querys();
            if (comboCanchaEncuentro1.Text != "" && comboCategoriaEncuentro.Text != "" && comboZonaEncuentro.Text != "" && tbHoraEncuentro1.Text != "" && comboLocalEncuentro1.Text != "" && comboVisitaEncuentro1.Text != "" && comboFechaEncuentro1.Text != "" && comboLocalEncuentro1.SelectedIndex != comboVisitaEncuentro1.SelectedIndex)
            {
                q.InsertarEncuentro(comboIDlocal.Items[comboLocalEncuentro1.SelectedIndex].ToString(), comboIDvisita.Items[comboVisitaEncuentro1.SelectedIndex].ToString(), tbHoraEncuentro1.Text, comboCanchaEncuentro1.Text, comboFechaEncuentro1.Text);


                DataTable dt = new DataTable();
                dt = q.LlenarTablaEncuentroFiltrado(!cbPartidos.Checked, comboFiltroEncuentros1.Text, comboFiltroEncuentros2.Text, comboFiltroEncuentros3.Text);
                dgvEncuentros.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dgvEncuentros.Rows.Add(new string[] {
                    row[0].ToString(), // ID
                    row[1].ToString(), // Nombre Local
                    row[2].ToString(), // Resultado Local
                    "-",
                    row[3].ToString(), // Resultado Visita
                    row[4].ToString(), // Nombre Visita
                    row[5].ToString(), // Categoria
                    row[6].ToString(), // Zona Local
                    row[7].ToString(), // Zona Visita
                    row[8].ToString(), // Fecha
                    row[9].ToString(), // Hora
                    row[10].ToString(), // Cancha
                });
                }

            }
            else
            {
                MessageBox.Show("Complete todos los campos para agregar el encuentro o asegurece que los equipos sean distintos", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvEncuentros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var fila = dgvEncuentros.SelectedRows[0];

            tbLocalEncuentro2.Text = fila.Cells[1].Value.ToString();
            tbGolesLocal.Text = fila.Cells[2].Value.ToString();
            tbGolesVisita.Text = fila.Cells[4].Value.ToString();
            tbVisitaEncuentro2.Text = fila.Cells[5].Value.ToString();
            tbCategoriaEncuentro2.Text = "Categoría " + fila.Cells[6].Value.ToString();
            comboFechaEncuentro2.SelectedItem = fila.Cells[9].Value.ToString();
            tbHoraEncuentro2.Text = fila.Cells[10].Value.ToString();
            comboCanchaEncuentro2.SelectedItem = fila.Cells[11].Value.ToString();

            if (fila.Cells[7].Value.ToString() == fila.Cells[8].Value.ToString())
            {
                tbZonaEncuentro2.Text = "Zona " + fila.Cells[7].Value.ToString();
            }
            else tbZonaEncuentro2.Text = "Zona INTERZONAL";
        }

        private void comboLocalEncuentro1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbPartidos_CheckedChanged(object sender, EventArgs e)
        {
            Querys q = new Querys();
            DataTable dt = new DataTable();
            dt = q.LlenarTablaEncuentroFiltrado(!cbPartidos.Checked, comboFiltroEncuentros1.Text, comboFiltroEncuentros2.Text, comboFiltroEncuentros3.Text);
            dgvEncuentros.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dgvEncuentros.Rows.Add(new string[] {
                    row[0].ToString(), // ID
                    row[1].ToString(), // Nombre Local
                    row[2].ToString(), // Resultado Local
                    "-",
                    row[3].ToString(), // Resultado Visita
                    row[4].ToString(), // Nombre Visita
                    row[5].ToString(), // Categoria
                    row[6].ToString(), // Zona Local
                    row[7].ToString(), // Zona Visita
                    row[8].ToString(), // Fecha
                    row[9].ToString(), // Hora
                    row[10].ToString(), // Cancha
                });
            }
        }

        private void comboFiltroEncuentros1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Querys q = new Querys();
            List<String> lista = new List<String>();
            DataTable dt = new DataTable();
            dt = q.LlenarTablaZonaFiltrado(comboFiltroEncuentros1.SelectedItem.ToString());

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(row[0].ToString());
            }
            lista.Add("TODAS");
            comboFiltroEncuentros2.DataSource = lista;


            DataTable dt2 = new DataTable();
            dt2 = q.LlenarTablaEncuentroFiltrado(!cbPartidos.Checked, comboFiltroEncuentros1.Text, comboFiltroEncuentros2.Text, comboFiltroEncuentros3.Text);
            dgvEncuentros.Rows.Clear();
            foreach (DataRow row in dt2.Rows)
            {
                dgvEncuentros.Rows.Add(new string[] {
                    row[0].ToString(), // ID
                    row[1].ToString(), // Nombre Local
                    row[2].ToString(), // Resultado Local
                    "-",
                    row[3].ToString(), // Resultado Visita
                    row[4].ToString(), // Nombre Visita
                    row[5].ToString(), // Categoria
                    row[6].ToString(), // Zona Local
                    row[7].ToString(), // Zona Visita
                    row[8].ToString(), // Fecha
                    row[9].ToString(), // Hora
                    row[10].ToString(), // Cancha
                });
            }
        }

        private void comboVisitaEncuentro1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btModificarPartido_Click(object sender, EventArgs e)
        {
            if (comboCanchaEncuentro2.Text != "" && comboFechaEncuentro2.Text != "" && tbHoraEncuentro2.Text != "" && ((tbGolesLocal.Text != "" && tbGolesVisita.Text != "") || (tbGolesLocal.Text == "" && tbGolesVisita.Text == "")))
            {
                if (MessageBox.Show("¿Seguro que desea modificar el Encuentro?", "Torneo Guillermito", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Querys q = new Querys();
                    q.actualizarEncuentro(dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString(), tbGolesLocal.Text, tbGolesVisita.Text, comboFechaEncuentro2.Text, tbHoraEncuentro2.Text, comboCanchaEncuentro2.Text);


                    DataTable dt = new DataTable();
                    dt = q.LlenarTablaEncuentroFiltrado(!cbPartidos.Checked, comboFiltroEncuentros1.Text, comboFiltroEncuentros2.Text, comboFiltroEncuentros3.Text);
                    dgvEncuentros.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dgvEncuentros.Rows.Add(new string[] {
                    row[0].ToString(), // ID
                    row[1].ToString(), // Nombre Local
                    row[2].ToString(), // Resultado Local
                    "-",
                    row[3].ToString(), // Resultado Visita
                    row[4].ToString(), // Nombre Visita
                    row[5].ToString(), // Categoria
                    row[6].ToString(), // Zona Local
                    row[7].ToString(), // Zona Visita
                    row[8].ToString(), // Fecha
                    row[9].ToString(), // Hora
                    row[10].ToString(), // Cancha
                });
                    }


                    if (dgvEncuentros.Rows.Count > 0)
                    {
                        dgvEncuentros.CurrentCell = dgvEncuentros.Rows[0].Cells[1];
                        dgvEncuentros.Rows[0].Selected = true;
                        tbGolesLocal.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("Verifique que todos los datos esten completos y correctos", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btModificarClub_Click(object sender, EventArgs e)
        {
            if (tbNombreClub2.Text != "") //NUEVO
            {
                byte[] imageBytes1;
                byte[] imageBytes2;

                MemoryStream ms1 = new MemoryStream();
                MemoryStream ms2 = new MemoryStream();

                pbCancha2.Image.Save(ms1, ImageFormat.Png);
                pbCancha2Control.Image.Save(ms2, ImageFormat.Png);

                imageBytes1 = ms1.ToArray();
                imageBytes2 = ms2.ToArray();

                if (!imageBytes1.SequenceEqual(imageBytes2))
                {
                    string nombreArchivo = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";

                    Querys q = new Querys();
                    q.actualizarClub(dgvClub.SelectedRows[0].Cells[0].Value.ToString(), tbNombreClub2.Text.ToUpper(), nombreArchivo);
                    subirFoto2(nombreArchivo);


                    DataTable dt = new DataTable();
                    dt = q.LlenarTablaClub();
                    dgvClub.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dgvClub.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                    }
                    lbAdvertencia.Visible = true;
                    tbNombreClub1.Text = "";
                    pbCancha1.Image = Resources.nada;
                }
                else
                {
                    string nombreArchivo = dgvClub.SelectedRows[0].Cells[2].Value.ToString();
                    Querys q = new Querys();
                    q.actualizarClub(dgvClub.SelectedRows[0].Cells[0].Value.ToString(), tbNombreClub2.Text.ToUpper(), nombreArchivo);


                    DataTable dt = new DataTable();
                    dt = q.LlenarTablaClub();
                    dgvClub.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dgvClub.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                    }
                    lbAdvertencia.Visible = true;
                    tbNombreClub1.Text = "";
                    pbCancha1.Image = Resources.nada;
                }

            }
            else
            {
                MessageBox.Show("Debe completar todos los datos obligatorios para insertar un club", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboFiltroEncuentros2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Querys q = new Querys();
            DataTable dt = new DataTable();
            dt = q.LlenarTablaEncuentroFiltrado(!cbPartidos.Checked, comboFiltroEncuentros1.Text, comboFiltroEncuentros2.Text, comboFiltroEncuentros3.Text);
            dgvEncuentros.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dgvEncuentros.Rows.Add(new string[] {
                    row[0].ToString(), // ID
                    row[1].ToString(), // Nombre Local
                    row[2].ToString(), // Resultado Local
                    "-",
                    row[3].ToString(), // Resultado Visita
                    row[4].ToString(), // Nombre Visita
                    row[5].ToString(), // Categoria
                    row[6].ToString(), // Zona Local
                    row[7].ToString(), // Zona Visita
                    row[8].ToString(), // Fecha
                    row[9].ToString(), // Hora
                    row[10].ToString(), // Cancha
                });
            }
        }

        private void comboFiltroEncuentros3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Querys q = new Querys();
            DataTable dt = new DataTable();
            dt = q.LlenarTablaEncuentroFiltrado(!cbPartidos.Checked, comboFiltroEncuentros1.Text, comboFiltroEncuentros2.Text, comboFiltroEncuentros3.Text);
            dgvEncuentros.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dgvEncuentros.Rows.Add(new string[] {
                    row[0].ToString(), // ID
                    row[1].ToString(), // Nombre Local
                    row[2].ToString(), // Resultado Local
                    "-",
                    row[3].ToString(), // Resultado Visita
                    row[4].ToString(), // Nombre Visita
                    row[5].ToString(), // Categoria
                    row[6].ToString(), // Zona Local
                    row[7].ToString(), // Zona Visita
                    row[8].ToString(), // Fecha
                    row[9].ToString(), // Hora
                    row[10].ToString(), // Cancha
                });
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboFiltroEncuentros1.Items.Clear();
            comboFiltroEncuentros3.Items.Clear();

            List<String> list = new List<String>();
            Querys query = new Querys();

            dtFiltro = query.LlenarTablaCategoria();

            comboCategoriaEncuentro.Items.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                comboCategoriaEncuentro.Items.Add(row[0].ToString());
                comboFiltroEncuentros1.Items.Add(row[0].ToString());
            }

            dtFiltro = query.LlenarTablaCanchas();

            comboCanchaEncuentro1.Items.Clear();
            comboCanchaEncuentro2.Items.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                comboCanchaEncuentro1.Items.Add(row[0].ToString());
                comboCanchaEncuentro2.Items.Add(row[0].ToString());

            }
            comboCanchaEncuentro2.SelectedItem = null;

            dtFiltro = query.LlenarTablaFechas();

            comboFechaEncuentro1.Items.Clear();
            comboFechaEncuentro2.Items.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                comboFechaEncuentro1.Items.Add(row[0].ToString());
                comboFechaEncuentro2.Items.Add(row[0].ToString());
                comboFiltroEncuentros3.Items.Add(row[0].ToString());
            }
            comboFechaEncuentro2.SelectedItem = null;

            dtFiltro = query.LlenarTablaEncuentro();
            dgvEncuentros.Rows.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                dgvEncuentros.Rows.Add(new string[] {
                    row[0].ToString(), // ID
                    row[1].ToString(), // Nombre Local
                    row[2].ToString(), // Resultado Local
                    "-",
                    row[3].ToString(), // Resultado Visita
                    row[4].ToString(), // Nombre Visita
                    row[5].ToString(), // Categoria
                    row[6].ToString(), // Zona Local
                    row[7].ToString(), // Zona Visita
                    row[8].ToString(), // Fecha
                    row[9].ToString(), // Hora
                    row[10].ToString(), // Cancha
                });
            }
            comboFiltroEncuentros1.SelectedIndex = -1;
            comboFiltroEncuentros2.SelectedIndex = -1;
            comboFiltroEncuentros3.SelectedIndex = -1;
            comboFiltroEncuentros1.Text = "";
            comboFiltroEncuentros3.Text = "";
        }

        private void comboCategoriaCruce_SelectedIndexChanged(object sender, EventArgs e)
        {
            Querys q = new Querys();
            DataTable dt = new DataTable();
            dt = q.ConsultarCruces(comboCategoriaCruce.Text);
            dgvCruces.DataSource = dt;
            dgv16vosOro.Rows.Clear();
            dgv16vosPlata.Rows.Clear();
            dgv8vosOro.Rows.Clear();
            dgv8vosPlata.Rows.Clear();
            dgv4tosOro.Rows.Clear();
            dgv4tosPlata.Rows.Clear();
            dgvFinalesOro.Rows.Clear();
            dgvFinalesPlata.Rows.Clear();

            if (dgvCruces.Rows.Count > 0)
            {
                labelNingunCruce.Visible = false;
                gbCruce.Visible = true;
            }
            else
            {
                labelNingunCruce.Visible = true;
                gbCruce.Visible = false;
            }
            DataGridView tabla = new DataGridView();
            foreach (DataGridViewRow row in dgvCruces.Rows)
            {

                if (Int32.Parse(row.Cells[12].Value.ToString()) < 5)
                {
                    tabla = dgvFinalesOro;
                }
                else if (Int32.Parse(row.Cells[12].Value.ToString()) >= 5 && Int32.Parse(row.Cells[12].Value.ToString()) < 9)
                {
                    tabla = dgv4tosOro;
                }
                else if (Int32.Parse(row.Cells[12].Value.ToString()) >= 9 && Int32.Parse(row.Cells[12].Value.ToString()) < 17)
                {
                    tabla = dgv8vosOro;
                }
                else if (Int32.Parse(row.Cells[12].Value.ToString()) >= 17 && Int32.Parse(row.Cells[12].Value.ToString()) < 33)
                {
                    tabla = dgv16vosOro;
                }
                else if (Int32.Parse(row.Cells[12].Value.ToString()) >= 33 && Int32.Parse(row.Cells[12].Value.ToString()) < 37)
                {
                    tabla = dgvFinalesPlata;
                }
                else if (Int32.Parse(row.Cells[12].Value.ToString()) >= 37 && Int32.Parse(row.Cells[12].Value.ToString()) < 41)
                {
                    tabla = dgv4tosPlata;
                }
                else if (Int32.Parse(row.Cells[12].Value.ToString()) >= 41 && Int32.Parse(row.Cells[12].Value.ToString()) < 49)
                {
                    tabla = dgv8vosPlata;
                }
                else if (Int32.Parse(row.Cells[12].Value.ToString()) >= 49 && Int32.Parse(row.Cells[12].Value.ToString()) < 65)
                {
                    tabla = dgv16vosPlata;
                }

                tabla.Rows.Add(new string[] { row.Cells[12].Value.ToString(), row.Cells[3].Value.ToString(), "VS", row.Cells[4].Value.ToString(), row.Cells[10].Value.ToString(), row.Cells[11].Value.ToString(), row.Cells[0].Value.ToString() });
            }
        }

        private void btEliminarEncuentro_Click(object sender, EventArgs e)
        {
            if (dgvEncuentros.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("¿Está seguro que desea eliminar el encuentro seleccionado?", "Torneo Guillermito", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Querys q = new Querys();
                    q.EliminarEncuentro(dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString());


                    DataTable dt2 = new DataTable();
                    dt2 = q.LlenarTablaEncuentroFiltrado(!cbPartidos.Checked, comboFiltroEncuentros1.Text, comboFiltroEncuentros2.Text, comboFiltroEncuentros3.Text);
                    dgvEncuentros.Rows.Clear();
                    foreach (DataRow row in dt2.Rows)
                    {
                        dgvEncuentros.Rows.Add(new string[] {
                    row[0].ToString(), // ID
                    row[1].ToString(), // Nombre Local
                    row[2].ToString(), // Resultado Local
                    "-",
                    row[3].ToString(), // Resultado Visita
                    row[4].ToString(), // Nombre Visita
                    row[5].ToString(), // Categoria
                    row[6].ToString(), // Zona Local
                    row[7].ToString(), // Zona Visita
                    row[8].ToString(), // Fecha
                    row[9].ToString(), // Hora
                    row[10].ToString(), // Cancha
                });
                    }

                }

            }
        }

        private void dgvFinalesOro_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow datos = dgvCruces.Rows.Cast<DataGridViewRow>()
                             .Where(row => row.Cells[12].Value.ToString() == dgvFinalesOro.SelectedRows[0].Cells[0].Value.ToString()).FirstOrDefault();

            string tipo;

            if (dgvFinalesOro.SelectedRows[0].Cells[0].Value.ToString() == "1") tipo = "FINAL - COPA ORO";
            else if (dgvFinalesOro.SelectedRows[0].Cells[0].Value.ToString() == "2") tipo = "3ER Y 4TO PUESTO - COPA ORO";
            else tipo = "SEMIFINAL - COPA ORO";



            DetalleCruce pantalla = new DetalleCruce(
                comboCategoriaCruce.Text, tipo,
                datos.Cells[3].Value.ToString(),
                datos.Cells[4].Value.ToString(),
                datos.Cells[10].Value.ToString(),
                datos.Cells[9].Value.ToString(),
                datos.Cells[11].Value.ToString(),
                datos.Cells[5].Value.ToString(),
                datos.Cells[6].Value.ToString(),
                datos.Cells[7].Value.ToString(),
                datos.Cells[8].Value.ToString(),
                datos.Cells[0].Value.ToString(),
                datos.Cells[12].Value.ToString(),
                datos.Cells[1].Value.ToString(),
                datos.Cells[2].Value.ToString());

            pantalla.ChildFormClosed += ChildFormClosedHandlerEditar;
            pantalla.Show();
        }

        private void dgv4tosOro_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow datos = dgvCruces.Rows.Cast<DataGridViewRow>()
                             .Where(row => row.Cells[12].Value.ToString() == dgv4tosOro.SelectedRows[0].Cells[0].Value.ToString()).FirstOrDefault();

            string tipo = "4TO DE FINAL - COPA ORO";

            DetalleCruce pantalla = new DetalleCruce(
                comboCategoriaCruce.Text, tipo, //categoria 1  //tipo decorativo 2
                datos.Cells[3].Value.ToString(), //Nombre local 3
                datos.Cells[4].Value.ToString(),  //Nombre visita 4
                datos.Cells[10].Value.ToString(), //hora 5
                datos.Cells[9].Value.ToString(),   //fecha 6
                datos.Cells[11].Value.ToString(),   //cancha 7
                datos.Cells[5].Value.ToString(),   //gl 8
                datos.Cells[6].Value.ToString(),    //gv 9
                datos.Cells[7].Value.ToString(),    //pl 10
                datos.Cells[8].Value.ToString(),    // pv 11
                datos.Cells[0].Value.ToString(),    //id 12
                datos.Cells[12].Value.ToString(),   //tipo 13
                datos.Cells[1].Value.ToString(),    //idlocal 14
                datos.Cells[2].Value.ToString());   //idvisita 15

            pantalla.ChildFormClosed += ChildFormClosedHandlerEditar;
            pantalla.Show();

            Querys q = new Querys();
            DataTable dt = new DataTable();
            dt = q.ConsultarCruces(comboCategoriaCruce.Text);
            dgvCruces.DataSource = dt;
            dgv16vosOro.Rows.Clear();
            dgv16vosPlata.Rows.Clear();
            dgv8vosOro.Rows.Clear();
            dgv8vosPlata.Rows.Clear();
            dgv4tosOro.Rows.Clear();
            dgv4tosPlata.Rows.Clear();
            dgvFinalesOro.Rows.Clear();
            dgvFinalesPlata.Rows.Clear();

            if (dgvCruces.Rows.Count > 0)
            {
                labelNingunCruce.Visible = false;
                gbCruce.Visible = true;
            }
            else
            {
                labelNingunCruce.Visible = true;
                gbCruce.Visible = false;
            }
            DataGridView tabla = new DataGridView();
            foreach (DataGridViewRow row in dgvCruces.Rows)
            {

                if (Int32.Parse(row.Cells[12].Value.ToString()) < 5)
                {
                    tabla = dgvFinalesOro;
                }
                else if (Int32.Parse(row.Cells[12].Value.ToString()) >= 5 && Int32.Parse(row.Cells[12].Value.ToString()) < 9)
                {
                    tabla = dgv4tosOro;
                }
                else if (Int32.Parse(row.Cells[12].Value.ToString()) >= 9 && Int32.Parse(row.Cells[12].Value.ToString()) < 17)
                {
                    tabla = dgv8vosOro;
                }
                else if (Int32.Parse(row.Cells[12].Value.ToString()) >= 17 && Int32.Parse(row.Cells[12].Value.ToString()) < 33)
                {
                    tabla = dgv16vosOro;
                }
                else if (Int32.Parse(row.Cells[12].Value.ToString()) >= 33 && Int32.Parse(row.Cells[12].Value.ToString()) < 37)
                {
                    tabla = dgvFinalesPlata;
                }
                else if (Int32.Parse(row.Cells[12].Value.ToString()) >= 37 && Int32.Parse(row.Cells[12].Value.ToString()) < 41)
                {
                    tabla = dgv4tosPlata;
                }
                else if (Int32.Parse(row.Cells[12].Value.ToString()) >= 41 && Int32.Parse(row.Cells[12].Value.ToString()) < 49)
                {
                    tabla = dgv8vosPlata;
                }
                else if (Int32.Parse(row.Cells[12].Value.ToString()) >= 49 && Int32.Parse(row.Cells[12].Value.ToString()) < 65)
                {
                    tabla = dgv16vosPlata;
                }

                tabla.Rows.Add(new string[] { row.Cells[12].Value.ToString(), row.Cells[3].Value.ToString(), "VS", row.Cells[4].Value.ToString(), row.Cells[10].Value.ToString(), row.Cells[11].Value.ToString(), row.Cells[0].Value.ToString() });
            }
        }

        private void dgv8vosOro_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow datos = dgvCruces.Rows.Cast<DataGridViewRow>()
                             .Where(row => row.Cells[12].Value.ToString() == dgv8vosOro.SelectedRows[0].Cells[0].Value.ToString()).FirstOrDefault();

            string tipo = "8VOS DE FINAL - COPA ORO";

            DetalleCruce pantalla = new DetalleCruce(
                comboCategoriaCruce.Text, tipo,
                datos.Cells[3].Value.ToString(),
                datos.Cells[4].Value.ToString(),
                datos.Cells[10].Value.ToString(),
                datos.Cells[9].Value.ToString(),
                datos.Cells[11].Value.ToString(),
                datos.Cells[5].Value.ToString(),
                datos.Cells[6].Value.ToString(),
                datos.Cells[7].Value.ToString(),
                datos.Cells[8].Value.ToString(),
                datos.Cells[0].Value.ToString(),
                datos.Cells[12].Value.ToString(),
                datos.Cells[1].Value.ToString(),
                datos.Cells[2].Value.ToString());

            pantalla.ChildFormClosed += ChildFormClosedHandlerEditar;
            pantalla.Show();
        }

        private void dgv16vosOro_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow datos = dgvCruces.Rows.Cast<DataGridViewRow>()
                             .Where(row => row.Cells[12].Value.ToString() == dgv16vosOro.SelectedRows[0].Cells[0].Value.ToString()).FirstOrDefault();

            string tipo = "16VOS DE FINAL - COPA ORO";

            DetalleCruce pantalla = new DetalleCruce(
                comboCategoriaCruce.Text, tipo,
                datos.Cells[3].Value.ToString(),
                datos.Cells[4].Value.ToString(),
                datos.Cells[10].Value.ToString(),
                datos.Cells[9].Value.ToString(),
                datos.Cells[11].Value.ToString(),
                datos.Cells[5].Value.ToString(),
                datos.Cells[6].Value.ToString(),
                datos.Cells[7].Value.ToString(),
                datos.Cells[8].Value.ToString(),
                datos.Cells[0].Value.ToString(),
                datos.Cells[12].Value.ToString(),
                datos.Cells[1].Value.ToString(),
                datos.Cells[2].Value.ToString());

            pantalla.ChildFormClosed += ChildFormClosedHandlerEditar;
            pantalla.Show();
        }

        private void dgvFinalesPlata_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow datos = dgvCruces.Rows.Cast<DataGridViewRow>()
                             .Where(row => row.Cells[12].Value.ToString() == dgvFinalesPlata.SelectedRows[0].Cells[0].Value.ToString()).FirstOrDefault();

            string tipo;

            if (dgvFinalesOro.SelectedRows[0].Cells[0].Value.ToString() == "1") tipo = "FINAL - COPA PLATA";
            else if (dgvFinalesOro.SelectedRows[0].Cells[0].Value.ToString() == "2") tipo = "3ER Y 4TO PUESTO - COPA PLATA";
            else tipo = "SEMIFINAL - COPA PLATA";

            DetalleCruce pantalla = new DetalleCruce(
                comboCategoriaCruce.Text, tipo,
                datos.Cells[3].Value.ToString(),
                datos.Cells[4].Value.ToString(),
                datos.Cells[10].Value.ToString(),
                datos.Cells[9].Value.ToString(),
                datos.Cells[11].Value.ToString(),
                datos.Cells[5].Value.ToString(),
                datos.Cells[6].Value.ToString(),
                datos.Cells[7].Value.ToString(),
                datos.Cells[8].Value.ToString(),
                datos.Cells[0].Value.ToString(),
                datos.Cells[12].Value.ToString(),
                datos.Cells[1].Value.ToString(),
                datos.Cells[2].Value.ToString());

            pantalla.ChildFormClosed += ChildFormClosedHandlerEditar;
            pantalla.Show();
        }

        private void dgv4tosPlata_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow datos = dgvCruces.Rows.Cast<DataGridViewRow>()
                             .Where(row => row.Cells[12].Value.ToString() == dgv4tosPlata.SelectedRows[0].Cells[0].Value.ToString()).FirstOrDefault();

            string tipo = "4TOS DE FINAL - COPA PLATA";

            DetalleCruce pantalla = new DetalleCruce(
                comboCategoriaCruce.Text, tipo,
                datos.Cells[3].Value.ToString(),
                datos.Cells[4].Value.ToString(),
                datos.Cells[10].Value.ToString(),
                datos.Cells[9].Value.ToString(),
                datos.Cells[11].Value.ToString(),
                datos.Cells[5].Value.ToString(),
                datos.Cells[6].Value.ToString(),
                datos.Cells[7].Value.ToString(),
                datos.Cells[8].Value.ToString(),
                datos.Cells[0].Value.ToString(),
                datos.Cells[12].Value.ToString(),
                datos.Cells[1].Value.ToString(),
                datos.Cells[2].Value.ToString());

            pantalla.ChildFormClosed += ChildFormClosedHandlerEditar;
            pantalla.Show();
        }

        private void dgv8vosPlata_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow datos = dgvCruces.Rows.Cast<DataGridViewRow>()
                             .Where(row => row.Cells[12].Value.ToString() == dgv8vosPlata.SelectedRows[0].Cells[0].Value.ToString()).FirstOrDefault();

            string tipo = "8VOS DE FINAL - COPA PLATA";

            DetalleCruce pantalla = new DetalleCruce(
                comboCategoriaCruce.Text, tipo,
                datos.Cells[3].Value.ToString(),
                datos.Cells[4].Value.ToString(),
                datos.Cells[10].Value.ToString(),
                datos.Cells[9].Value.ToString(),
                datos.Cells[11].Value.ToString(),
                datos.Cells[5].Value.ToString(),
                datos.Cells[6].Value.ToString(),
                datos.Cells[7].Value.ToString(),
                datos.Cells[8].Value.ToString(),
                datos.Cells[0].Value.ToString(),
                datos.Cells[12].Value.ToString(),
                datos.Cells[1].Value.ToString(),
                datos.Cells[2].Value.ToString());

            pantalla.ChildFormClosed += ChildFormClosedHandlerEditar;
            pantalla.Show();
        }

        private void dgv16vosPlata_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow datos = dgvCruces.Rows.Cast<DataGridViewRow>()
                             .Where(row => row.Cells[12].Value.ToString() == dgv16vosPlata.SelectedRows[0].Cells[0].Value.ToString()).FirstOrDefault();

            string tipo = "16VOS DE FINAL - COPA PLATA";

            DetalleCruce pantalla = new DetalleCruce(
                comboCategoriaCruce.Text, tipo,
                datos.Cells[3].Value.ToString(),
                datos.Cells[4].Value.ToString(),
                datos.Cells[10].Value.ToString(),
                datos.Cells[9].Value.ToString(),
                datos.Cells[11].Value.ToString(),
                datos.Cells[5].Value.ToString(),
                datos.Cells[6].Value.ToString(),
                datos.Cells[7].Value.ToString(),
                datos.Cells[8].Value.ToString(),
                datos.Cells[0].Value.ToString(),
                datos.Cells[12].Value.ToString(),
                datos.Cells[1].Value.ToString(),
                datos.Cells[2].Value.ToString());

            pantalla.ChildFormClosed += ChildFormClosedHandlerEditar;
            pantalla.Show();
        }

        private void btClubes_Enter(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void btClubes_Leave(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.FloralWhite;
        }

        private void btCyZ_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void btCyZ_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.FloralWhite;
        }

        private void btEquipos_MouseEnter(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void btEquipos_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.FloralWhite;
        }

        private void btCanchas_MouseEnter(object sender, EventArgs e)
        {
            pictureBox9.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void btCanchas_MouseLeave(object sender, EventArgs e)
        {
            pictureBox9.BackColor = Color.FloralWhite;
        }

        private void btEncuentros_MouseEnter(object sender, EventArgs e)
        {
            pictureBox8.BackColor = Color.FromArgb(229, 224, 215);

        }

        private void btEncuentros_MouseLeave(object sender, EventArgs e)
        {
            pictureBox8.BackColor = Color.FloralWhite;
        }

        private void btCruces_MouseEnter(object sender, EventArgs e)
        {
            pictureBox7.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void btCruces_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.BackColor = Color.FloralWhite;
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.FromArgb(229, 224, 215);
            btClubes.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            btClubes.BackColor = Color.FloralWhite;
            pictureBox4.BackColor = Color.FloralWhite;

        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.FromArgb(229, 224, 215);
            btCyZ.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            btCyZ.BackColor = Color.FloralWhite;
            pictureBox5.BackColor = Color.FloralWhite;

        }

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.FromArgb(229, 224, 215);
            btEquipos.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            btEquipos.BackColor = Color.FloralWhite;
            pictureBox6.BackColor = Color.FloralWhite;

        }

        private void pictureBox9_MouseEnter(object sender, EventArgs e)
        {
            pictureBox9.BackColor = Color.FromArgb(229, 224, 215);
            btCanchas.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void pictureBox9_MouseLeave(object sender, EventArgs e)
        {
            btCanchas.BackColor = Color.FloralWhite;
            pictureBox9.BackColor = Color.FloralWhite;

        }

        private void pictureBox8_MouseEnter(object sender, EventArgs e)
        {
            pictureBox8.BackColor = Color.FromArgb(229, 224, 215);
            btEncuentros.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            btEncuentros.BackColor = Color.FloralWhite;
            pictureBox8.BackColor = Color.FloralWhite;

        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            pictureBox7.BackColor = Color.FromArgb(229, 224, 215);
            btCruces.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            btCruces.BackColor = Color.FloralWhite;
            pictureBox7.BackColor = Color.FloralWhite;
        }

        private void pbCancha2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbCancha2.Image = new Bitmap(openFileDialog.FileName);
            }
        }
    }
}
