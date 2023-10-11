using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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

        private void Club_Load(object sender, EventArgs e)
        {
            Querys q = new Querys();
            dgvClub.DataSource = q.LlenarTablaClub();
        }

        private void btLimpiarCanchas_Click(object sender, EventArgs e)
        {
            tbNombreClub1.Text = "";
            pbCancha1.Image = Resources.nada;
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
                    

               
                    // Obtiene la ruta seleccionada

                    // Genera un nombre de archivo único (por ejemplo, usando la fecha y hora actual)
                    

                    // Combina la ruta de la carpeta de destino con el nombre del archivo
                    string rutaCompleta = Path.Combine("D:\\xampp\\htdocs\\ESCUDOS", nombreArchivo);

                    // Guarda la imagen en la carpeta de destino
                    pbCancha1.Image.Save(rutaCompleta, ImageFormat.Jpeg);

                    MessageBox.Show("Imagen guardada exitosamente.");
                
            }
            else
            {
                MessageBox.Show("No se ha seleccionado una imagen para guardar.");
            }
        }

        private void tbNombreClub1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvClub_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Especifica la ruta completa de la imagen, por ejemplo:
            string rutaImagen = @"D:\xampp\htdocs\ESCUDOS\" + dgvClub.SelectedRows[0].Cells[2].Value.ToString();

            try
            {
                // Carga la imagen en el PictureBox
                pbCancha2.Image = System.Drawing.Image.FromFile(rutaImagen);
                tbNombreClub2.Text = dgvClub.SelectedRows[0].Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            tbNombreClub2.Enabled = true;
            btModificarClub.Enabled = true;


        }

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

                    // Elimina el archivo (momentaneamente no util)

                    //File.Delete(rutaArchivo);
                    //MessageBox.Show("Archivo eliminado exitosamente.");


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
