using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Torneo_Guillermito.Properties;

namespace Torneo_Guillermito
{
    public partial class Club : Form
    {
        public Club()
        {
            InitializeComponent();
        }

        private async void Club_Load(object sender, EventArgs e)
        {
            Querys q = new Querys();
            //MessageBox.Show(q.LlenarTablaClub().ToString());
            DataTable dataTable = new DataTable();
            dataTable = q.LlenarTablaClub();
            dgvClub.DataSource = dataTable;
            lbAdvertencia.Visible = true;
        }

        private void btLimpiarCanchas_Click(object sender, EventArgs e)
        {
            tbNombreClub1.Text = "";
            pbCancha1.Image = Resources.nada;
           
        }

        private void DescargarImagenExistenteRemota(string url)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    // Descarga la imagen desde la URL remota
                    byte[] data = webClient.DownloadData(url);

                    // Convierte los datos descargados en una imagen
                    using (var ms = new System.IO.MemoryStream(data))
                    {
                        Image imagen = Image.FromStream(ms);

                        // Asigna la imagen a la PictureBox
                        pbCancha2.Image = imagen;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show("Error al descargar la imagen: " + ex.Message);
            }
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

        private async void btAgregarCancha_Click(object sender, EventArgs e)
        {
            if(tbNombreClub1 != null)
            if (pbCancha1.Image != null)
            {
                    string nombreArchivo = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";

                    Querys q = new Querys(); 
                    q.InsertarClub(tbNombreClub1.Text, nombreArchivo);

                    // Abre el cuadro de diálogo para seleccionar la carpeta de destino
                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();


                    /* FALLA ALGO DEL FORMATO O NO SE QUE, LA DEJO AHI
                     
                    string rutaCompleta = Path.Combine("http://vps-3666997-x.dattaweb.com/ESCUDOS/", nombreArchivo);
                    pbCancha1.Image.Save(rutaCompleta, ImageFormat.Jpeg);
                    MessageBox.Show("Imagen guardada exitosamente.");

                    */
                }
                else
            {
                MessageBox.Show("No se ha seleccionado una imagen para guardar.");
            }
        }

        private void dgvClub_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lbAdvertencia.Visible = false;
            // Especifica la ruta completa de la imagen, por ejemplo:
            string rutaImagen = Resources.carpetaFotosServer + dgvClub.SelectedRows[0].Cells[2].Value.ToString();

            try
            {
                // Carga la imagen en el PictureBox
             
                DescargarImagenExistenteRemota(rutaImagen);
                tbNombreClub2.Text = dgvClub.SelectedRows[0].Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            tbNombreClub2.Enabled = true;
            btModificarClub.Enabled = true;


        }

        // HAY QUE PROBAR SI SE LIBERA EL IF, SINO NI BORRAR LA FOTO NO CREO QUE ESTEN AHI CAMBIANDO FOTOS A DOS MANOS.

        private void btModificarClub_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea modificar el club seleccionado?", "Torneo Guillermito", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Querys q = new Querys();
                string nombreFoto = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
                q.actualizarClub(dgvClub.SelectedRows[0].Cells[0].Value.ToString(), tbNombreClub2.Text, nombreFoto);

                string rutaCompleta = Path.Combine("D:\\xampp\\htdocs\\ESCUDOS", nombreFoto);
                pbCancha2.Image.Save(rutaCompleta, ImageFormat.Png);

                string rutaArchivo = Path.Combine("D:\\xampp\\htdocs\\ESCUDOS", dgvClub.SelectedRows[0].Cells[2].Value.ToString());

                if (File.Exists(rutaArchivo))
                {
                    pbCancha2.Image.Dispose();
                    pbCancha2.Image = null;
                    pbCancha2.Image = Resources.nada;


                }
                else
                {
                    MessageBox.Show("El archivo no existe en la ruta especificada.");
                }

                Club_Load(null, null);
            }
            
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

        private void btEliminarClub_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea eliminar el club seleccionado?", "Toreno Guillermito", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Querys q = new Querys();
                q.EliminarClub(dgvClub.SelectedRows[0].Cells[0].Value.ToString());

                tbNombreClub2.Text = "";
                pbCancha2.Image = Resources.nada;
                Club_Load(null, null);
            }
        }
    }
}
