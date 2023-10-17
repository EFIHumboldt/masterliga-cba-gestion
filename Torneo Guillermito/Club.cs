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
using System.Net.Http.Headers;
using System.Security.Policy;
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

                    string rutaCompleta = Path.Combine(Resources.carpetaFotosServer, nombreArchivo);

                    SubirImagenAServidorRemoto();



                    //pbCancha1.Image.Save(rutaCompleta, ImageFormat.Png);
                    //MessageBox.Show("Imagen guardada exitosamente.");

                    
                }
                else
            {
                MessageBox.Show("No se ha seleccionado una imagen para guardar.");
            }
        }

        private async void SubirImagenAServidorRemoto()
        {
            try
            {
                // URL del servidor donde deseas cargar la imagen
                string urlServidor = "https://vps-3666997-x.dattaweb.com/ESCUDOS";

                // Ruta local del archivo que deseas cargar
                string rutaLocalArchivo = @"D:\xampp\htdocs\ESCUDOS\20231010030129.png";

                using (HttpClient cliente = new HttpClient())
                using (var formData = new MultipartFormDataContent())
                using (var archivoStream = new FileStream(rutaLocalArchivo, FileMode.Open, FileAccess.Read))
                {
                    // Crea un contenido de archivo y agrega el archivo al formulario
                    var archivoContent = new StreamContent(archivoStream);
                    archivoContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "archivo", // Nombre del campo en el formulario
                        FileName = Path.GetFileName(rutaLocalArchivo) // Nombre del archivo en el servidor
                    };

                    formData.Add(archivoContent);

                    // Realiza la solicitud POST al servidor
                    HttpResponseMessage respuesta = await cliente.PostAsync(urlServidor, formData);

                    // Verifica si la carga fue exitosa
                    if (respuesta.IsSuccessStatusCode)
                    {
                        Console.WriteLine("La imagen se cargó con éxito en el servidor.");
                    }
                    else
                    {
                        Console.WriteLine("Error al cargar la imagen: " + respuesta.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        // Método para convertir una imagen en un arreglo de bytes
        private byte[] ImageToByteArray(Image imagen, System.Drawing.Imaging.ImageFormat formato)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imagen.Save(ms, formato);
                return ms.ToArray();
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
