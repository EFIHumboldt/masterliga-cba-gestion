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



namespace Torneo_Guillermito
{

    
    public partial class gbEncuentros : Form
    {

        public string rutaLocalArchivo = "";

        public gbEncuentros()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var zona = new Zona();
            zona.Show();
        }

        private void Cancha_Click(object sender, EventArgs e)
        {
            var cancha = new Cancha();
            cancha.Show();
        }

        private void Encuentro_Click(object sender, EventArgs e)
        {
            var encuentro = new Encuentro(); 
            encuentro.Show();
        }

        private async void Club_Click(object sender, EventArgs e)
        {
           
            var club = new Club(); 
            club.Show();
        }

        private void Equipo_Click(object sender, EventArgs e)
        {
            var equipo = new Equipo(); 
            equipo.Show();
        }

        private async void Menu_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(@"C:\Users\Usuario\Pictures\foto.png"))
            {
                await SubirImagenAServidorRemoto();
            }
        }

        private async Task SubirImagenAServidorRemoto()
        {

            // Configura la URL del servidor HTTPS
            string serverUrl = "http://vps-3666997-x.dattaweb.com/ESCUDOS";

            // Ruta completa del archivo local que deseas subir
            string localFilePath = @"C:\Users\Usuario\Pictures\foto.png";

            // Crea un cliente HTTP
            using (HttpClient httpClient = new HttpClient())
            {
                using (MultipartFormDataContent form = new MultipartFormDataContent())
                {
                    // Lee el archivo local
                    byte[] fileContents = System.IO.File.ReadAllBytes(localFilePath);

                    // Crea un contenido de archivo
                    ByteArrayContent fileContent = new ByteArrayContent(fileContents);
                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "file",
                        FileName = "foto.png"
                    };

                    // Agrega el contenido del archivo al formulario
                    form.Add(fileContent);

                    // Realiza la solicitud POST
                    HttpResponseMessage response = await httpClient.PostAsync(serverUrl, form);

                    // Verifica la respuesta
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("File upload successful!");
                    }
                    else
                    {
                        MessageBox.Show($"File upload failed. Status Code: {response.StatusCode}");
                    }
                }
            }
        }
    }
}
