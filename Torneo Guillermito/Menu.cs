using MySql.Data.MySqlClient;
using System;
using System.Net;
using System.Collections;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
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
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using LIPa.Properties;
using System.Drawing.Imaging;
using System.Net.Sockets;
using System.Drawing.Printing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics.Eventing.Reader;
using Google.Protobuf.WellKnownTypes;

namespace LIPa
{


    public partial class gbEncuentros : Form
    {

        public string rutaLocalArchivo = "";
        private DataTable dtFiltro;
        private DataTable dtFiltro2; //solo para el de equipos por ahora, que tiene dos datagrid al mismo tiempo
        private String conexionFoto;
        private String partidoSeleccionado;
        private bool comenzado;
        Querys q = new Querys();
        


        public gbEncuentros()
        {
            InitializeComponent();
        }

        private async void Menu_Load(object sender, EventArgs e)
        {
            this.pictureBox1.BackColor = System.Drawing.ColorTranslator.FromHtml("#110f15");
            this.pictureBox3.BackColor = System.Drawing.ColorTranslator.FromHtml("#110f15");

            string filePath = "conexionFoto.txt";
            Screen screen = Screen.PrimaryScreen;

            //MessageBox.Show((screen.Bounds.Width * 9 / 10).ToString() + " " + screen.Bounds.Height.ToString());
            if (screen.Bounds.Width * 9 / 10 < 1400)
            {
                this.Width = screen.Bounds.Width * 9 / 10;
            }
            else
            {
                this.Width = 1399;
            }
            if (screen.Bounds.Height * 9 / 10 < 990)
            {
                this.Height = screen.Bounds.Height * 9 / 10;
            }
            else
            {
                this.Height = 980;
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
                MessageBox.Show("Errror al leer el archivo de conexion para las fotos: "+ ef.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            //Querys q = new Querys();
            //dtFiltro = new DataTable();

            //List<String> list = new List<String>();
            //DataTable dt = q.LlenarTablaCategoria();
            

            //foreach (DataRow row in dt.Rows)
            //{
                //list.Add(row[0].ToString());
            //}
            //comboCategoriaCruce.DataSource = list;

            label2.BackColor = Color.FromArgb(240, 240, 240);
            label49.BackColor = Color.FromArgb(240, 240, 240);
        }

        public void GenerarVistaPreviaPDF()
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "Ficha de Partido Detallada";

            int N = dgvEncuentros.SelectedRows.Count; // Número de fichas a generar
            //int N = 1;

            if (N != 0)
            {

                int currentFicha = 0; // Contador de fichas impresas

                string[] categoria = new string[N];
                string[] elocal = new string[N];
                string[] evisita = new string[N];
                string[] cancha = new string[N];
                string[] hora = new string[N];

                int i = -1;

                foreach (DataGridViewRow row in dgvEncuentros.SelectedRows)
                {
                    i++;
                    categoria[i] = row.Cells["categoria_encuentro"].Value.ToString();
                    elocal[i] = row.Cells["local"].Value.ToString().ToLower();
                    evisita[i] = row.Cells["visita"].Value.ToString().ToLower();
                    cancha[i] = row.Cells["cancha_encuentro"].Value.ToString();
                    hora[i] = row.Cells["hora_encuentro"].Value.ToString();
                }

                // Configurar el contenido de la página de impresión
                printDoc.PrintPage += (sender, e) =>
                {
                    // Establecer fuentes y pinceles
                    Font tituloFont = new Font("Arial", 15, FontStyle.Bold);
                    Font textoFont = new Font("Arial", 13);
                    Font subTextoFont = new Font("Arial", 12, FontStyle.Bold);
                    Pen linePen = new Pen(Color.Black, 1);
                    Brush textBrush = Brushes.Black;

                    // Obtener ancho y alto de página
                    float pageWidth = e.PageBounds.Width;
                    float pageHeight = e.PageBounds.Height;
                    float margin = 40;
                    float yPosition = margin; // Inicio desde el margen superior

                    // Mientras haya fichas para imprimir y espacio en la página
                    while (currentFicha < N && yPosition < pageHeight - margin - 100)
                    {
                        // Información de Cancha y Hora en la esquina superior derecha
                        string canchaTexto = "Cancha: " + cancha[currentFicha];
                        string horaTexto = "Hora: " + hora[currentFicha];
                        e.Graphics.DrawString(canchaTexto, subTextoFont, textBrush, pageWidth - margin - 150, yPosition);
                        e.Graphics.DrawString(horaTexto, subTextoFont, textBrush, pageWidth - margin - 150, yPosition + 20);

                        // Dibuja el título centrado en la página
                        string titulo = "Categoría " + categoria[currentFicha];
                        SizeF tituloSize = e.Graphics.MeasureString(titulo, tituloFont);
                        e.Graphics.DrawString(titulo, tituloFont, textBrush, (pageWidth - tituloSize.Width) / 2, yPosition);
                        yPosition += 50;

                        // Información de Equipo Local
                        float localX = margin; // Posición X para equipo local
                        float visitanteX = pageWidth / 2 + 20; // Posición X para equipo visitante

                        // Equipo Local
                        e.Graphics.DrawString("Equipo Local:", textoFont, textBrush, localX, yPosition);
                        e.Graphics.DrawString(elocal[currentFicha], textoFont, textBrush, localX, yPosition + 25);
                        e.Graphics.DrawRectangle(Pens.Black, localX + 250, yPosition - 5, 50, 50); // Espacio para goles

                        // Equipo Visitante
                        e.Graphics.DrawString("Equipo Visitante:", textoFont, textBrush, visitanteX, yPosition);
                        e.Graphics.DrawString(evisita[currentFicha], textoFont, textBrush, visitanteX, yPosition + 25);
                        e.Graphics.DrawRectangle(Pens.Black, visitanteX + 275, yPosition - 5, 50, 50); // Espacio para goles

                        yPosition += 50;

                        // Texto "VS" centrado entre los equipos
                        string vsText = "VS";
                        SizeF vsTextSize = e.Graphics.MeasureString(vsText, textoFont);
                        e.Graphics.DrawString(vsText, textoFont, textBrush, (pageWidth - vsTextSize.Width) / 2 - 20, yPosition - 45);

                        yPosition += 5;

                        // Firmas de Delegados
                        e.Graphics.DrawString("Firma del Delegado:", subTextoFont, textBrush, localX, yPosition);
                        e.Graphics.DrawLine(linePen, localX + 180, yPosition + 25, localX + 300, yPosition + 25); // Línea de firma
                        e.Graphics.DrawString("Firma del Delegado:", subTextoFont, textBrush, visitanteX, yPosition);
                        e.Graphics.DrawLine(linePen, visitanteX + 180, yPosition + 25, visitanteX + 300, yPosition + 25); // Línea de firma

                        yPosition += 30;

                        // Firma del Árbitro
                        string arbitroTexto = "Firma del Árbitro:";
                        SizeF arbitroTextoSize = e.Graphics.MeasureString(arbitroTexto, subTextoFont);
                        e.Graphics.DrawString(arbitroTexto, subTextoFont, textBrush, (pageWidth - arbitroTextoSize.Width) / 2 - 130, yPosition);
                        e.Graphics.DrawLine(linePen, (pageWidth / 2) - 60, yPosition + 20, (pageWidth / 2) + 70, yPosition + 20);
                        e.Graphics.DrawLine(linePen, 30, yPosition + 35, pageWidth - 30, yPosition + 35);
                        e.Graphics.DrawLine(linePen, 30, yPosition + -150, 30, yPosition + 35);
                        e.Graphics.DrawLine(linePen, pageWidth - 30, yPosition + -150, pageWidth - 30, yPosition + 35);

                        yPosition += 45; // Aumentar el espacio para la próxima ficha

                        // Incrementar el contador de fichas
                        currentFicha++;
                    }

                    // Controlar el salto de página
                    if (currentFicha < N)
                    {
                        e.HasMorePages = true; // Indica que hay más páginas
                    }
                    else
                    {
                        e.HasMorePages = false; // Fin de las páginas
                    }
                };

                // Abrir el cuadro de diálogo de impresión
                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = printDoc;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDoc.PrinterSettings = printDialog.PrinterSettings;
                    printDoc.Print();
                }
            }else
                MessageBox.Show("Seleccione partidos para imprimir", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private async void subirFoto1(string nombre)
        {
            string serverUrl = conexionFoto + "ESCUDOS/imagen.php"; // Reemplaza con la URL de tu API
            string fieldName = "archivo"; // Nombre del campo de entrada del archivo en el formulario HTML

            System.Drawing.Image image = pbCancha1.Image;
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
                    MessageBox.Show("Error al subir la imagen al servidor, intente modificar el club, caso contrario comuuniquese con el administrador", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void subirFoto2(string nombre)
        {
            string serverUrl = conexionFoto + "ESCUDOS/imagen.php"; // Reemplaza con la URL de tu API
            string fieldName = "archivo"; // Nombre del campo de entrada del archivo en el formulario HTML

            System.Drawing.Image image = pbCancha2.Image;
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
                    MessageBox.Show("Error al subir la imagen al servidor, intente modificar el club, caso contrario comuuniquese con el administrador", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void subirFotoAgregarJugador(string nombre)
        {
            string serverUrl = conexionFoto + "jugadores/imagen.php"; // Reemplaza con la URL de tu API
            string fieldName = "archivo"; // Nombre del campo de entrada del archivo en el formulario HTML

            System.Drawing.Image image = pbJugador1.Image;
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
                    MessageBox.Show("Error al subir la imagen al servidor, intente modificar el jugador, caso contrario comuuniquese con el administrador", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private async void subirFotoModificarJugador(string nombre)
        {
            string serverUrl = conexionFoto + "jugadores/imagen.php"; // Reemplaza con la URL de tu API
            string fieldName = "archivo"; // Nombre del campo de entrada del archivo en el formulario HTML

            System.Drawing.Image image = pbJugador2.Image;
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
                    MessageBox.Show("Error al subir la imagen al servidor, intente modificar el jugador, caso contrario comuuniquese con el administrador", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            tbNombreCortoClub2.Text = "";
            tbNombreCortoClub2.Enabled = false;
            label4.Visible = true;
            label48.Visible = true;
            label2.Visible = true;
            label2.BackColor = Color.FromArgb(240, 240, 240);
            label49.Visible = true;
            label49.BackColor = Color.FromArgb(240, 240, 240);
            lbAdvertencia.Visible = true;
            //
            dgvClub.Rows.Clear();
            gbClubes.Visible = true;
            gbCanchas.Visible = false;
            gbCyZ.Visible = false;
            gbEquipos.Visible = false;
            gbPartidos.Visible = false;
            gbCruces.Visible = false;
            gbJugadores.Visible = false;
            //

            dtFiltro = q.LlenarTablaClub();

            foreach (DataRow row in dtFiltro.Rows)
            {
                dgvClub.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString() });
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
            gbJugadores.Visible=false;

            List<string> zonas = new List<string> { "General", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q" };
            List<String> categorias2 = new List<String>();
            categorias2.Clear();

            dtFiltro = q.LlenarTablaCategoria();

            dgvCategoria.Rows.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                dgvCategoria.Rows.Add(new string[] { row[0].ToString(), row[1].ToString() });
            }

            dtFiltro = q.LlenarTablaZona();

            dgvZona.Rows.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                //dgvZona.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                dgvZona.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
            }

            foreach (DataGridViewRow fila in dgvCategoria.Rows)
            { if (fila.Cells[1].Value != null) { string valor = fila.Cells[1].Value.ToString(); categorias2.Add(valor); } }

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
            gbJugadores.Visible = false;

            tbEquipos1.Text = "";
            tbNombreJugador3.Text = "";


            DataTable dt = new DataTable();

            comboEquipo1.Items.Clear();
            comboEquipo2.Items.Clear();
            dt = q.LlenarTablaCategoria();
            foreach (DataRow fila in dt.Rows)
            { comboEquipo1.Items.Add(fila[1].ToString()); }
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
            gbJugadores.Visible = false;

            //dtFiltro = q.LlenarTablaCanchas();

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
        private void limpiarFichaEncuentros()
        {
            tbLocalEncuentro2.Text = string.Empty;
            tbVisitaEncuentro2.Text = string.Empty;
            tbGolesLocal.Text = string.Empty;
            tbGolesVisita.Text = string.Empty;
            tbAmarillasLocal.Text = string.Empty;
            tbAmarillasVisita.Text = string.Empty;
            tbRojasLocal.Text = string.Empty;
            tbRojasVisita.Text = string.Empty;
            tbYoutubeEncuentro2.Text = string.Empty;
            tbDriveEncuentro2.Text = string.Empty;
            comboFechaEncuentro2.Text = string.Empty;
            tbHoraEncuentro2.Text = string.Empty;
            datePickerEn2.Text = string.Empty;
            checkFechaEncuentro.Checked = false;
            datePickerEn2.Enabled = false;
            checkFechaNuevoEncuentro.Checked = false;
            datePickerEnc1.Enabled = false;

            comboCategoriaEncuentro.Text = string.Empty;
            comboLocalEncuentro1.Text = string.Empty;
            comboVisitaEncuentro1.Text = string.Empty;
            tbHoraEncuentro1.Text = string.Empty;
            comboFechaEncuentro1.Text = string.Empty;
            tbDriveEncuentro1.Text = string.Empty;
            tbYoutubeEncuentro1.Text = string.Empty;
        }

        private void btEncuentros_Click(object sender, EventArgs e)
        {
            gbClubes.Visible = false;
            gbCanchas.Visible = false;
            gbCyZ.Visible = false;
            gbEquipos.Visible = false;
            gbPartidos.Visible = true;
            gbCruces.Visible = false;
            gbJugadores.Visible=false;

            limpiarFichaEncuentros();
            comboFiltroEncuentros1.Items.Clear();
            comboFiltroEncuentros3.Items.Clear();

            List<String> list = new List<String>();

            dtFiltro = q.LlenarTablaCategoria();

            comboCategoriaEncuentro.Items.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                comboCategoriaEncuentro.Items.Add(row[1].ToString());
                comboFiltroEncuentros1.Items.Add(row[1].ToString());
            }

            //dtFiltro = query.LlenarTablaCanchas();

            //comboCanchaEncuentro1.Items.Clear();
            //comboCanchaEncuentro2.Items.Clear();
            /*
            foreach (DataRow row in dtFiltro.Rows)
            {
                comboCanchaEncuentro1.Items.Add(row[0].ToString());
                comboCanchaEncuentro2.Items.Add(row[0].ToString());

            }
            */
            //comboCanchaEncuentro2.SelectedItem = null;

            dtFiltro = q.LlenarTablaFechas();

            comboFechaEncuentro1.Items.Clear();
            comboFechaEncuentro2.Items.Clear();
            
            foreach (DataRow row in dtFiltro.Rows)
            {
                comboFechaEncuentro1.Items.Add(row[0].ToString());
                comboFechaEncuentro2.Items.Add(row[0].ToString());
                comboFiltroEncuentros3.Items.Add(row[0].ToString());
            }
            
            comboFechaEncuentro2.SelectedItem = null;

            dtFiltro = q.LlenarTablaEncuentro("WHERE en.tipo = 1");
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
                    row[5].ToString(), // Division
                    row[6].ToString(), // Fecha
                    row[7].ToString(), // Dia
                    row[8].ToString(), // Hora
                     row[9].ToString(), // Youtube
                    row[10].ToString(), // Imagenes
                    row[11].ToString() //terminado
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
            if (tbNombreClub1.Text != "" && tbNombreCortoClub1.Text != "")
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
                    q.InsertarClub(tbNombreClub1.Text, tbNombreCortoClub1.Text, nombreArchivo);

                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

                    string rutaCompleta = Path.Combine(Resources.carpetaFotosServer, nombreArchivo);

                    subirFoto1(nombreArchivo);


                    DataTable dt = new DataTable();
                    dt = q.LlenarTablaClub();
                    dgvClub.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dgvClub.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString() });
                    }
                    lbAdvertencia.Visible = true;
                    tbNombreClub1.Text = "";
                    tbNombreCortoClub1.Text = "";
                    pbCancha1.Image = Resources.nada;
                }
                else
                {
                    if (MessageBox.Show("No ha insertado una foto para el escudo, ¿Desea agregar el club sin escudo? Se pondra un escudo por defecto", "Liga Infantil del Paraná", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string nombreArchivo = "0.png";

                        q.InsertarClub(tbNombreClub1.Text, tbNombreCortoClub1.Text, nombreArchivo);


                        DataTable dt = new DataTable();
                        dt = q.LlenarTablaClub();
                        dgvClub.Rows.Clear();
                        foreach (DataRow row in dt.Rows)
                        {
                            dgvClub.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString() });
                        }
                        lbAdvertencia.Visible = true;
                        tbNombreClub1.Text = "";
                        tbNombreCortoClub1.Text = "";
                        pbCancha1.Image = Resources.nada;
                    }

                }




            }
            else
            {
                MessageBox.Show("Debe completar todos los datos obligatorios para insertar un club", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        // HAY QUE PROBAR SI SE LIBERA EL IF, SINO NI BORRAR LA FOTO NO CREO QUE ESTEN AHI CAMBIANDO FOTOS A DOS MANOS.

        private void btEliminarClub_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea eliminar el club seleccionado?", "Liga Infantil del Paraná", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
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

        private void label49_Click(object sender, EventArgs e)
        {
            label49.Visible = false;
            tbNombreCortoClub2.Focus();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            label4.Visible = false;
            tbNombreClub1.Focus();
        }

        private void label48_Click(object sender, EventArgs e)
        {
            label48.Visible = false;
            tbNombreCortoClub1.Focus();
        }

        private void tbNombreClub1_Leave(object sender, EventArgs e)
        {
            if (tbNombreClub1.Text == "")
            {
                label4.Visible = true;
            }
        }

        private void tbNombreCortoClub1_Leave(object sender, EventArgs e)
        {
            if (tbNombreCortoClub1.Text == "")
            {
                label48.Visible = true;
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

        private void tbNombreCortoClub2_Leave(object sender, EventArgs e)
        {
            if (tbNombreCortoClub2.Text == "")
            {
                label49.BackColor = Color.White;
                label49.Visible = true;

            }
        }

        private void tbNombreClub1_Enter(object sender, EventArgs e)
        {
            label4.Visible = false;
        }

        private void tbNombreCortoClub1_Enter(object sender, EventArgs e)
        {
            label48.Visible = false;
        }

        private void tbNombreCortoClub2_Enter(object sender, EventArgs e)
        {
            label49.Visible = false;
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
                var filasCoincidentes = dtFiltro.AsEnumerable().Where(row => row.Field<string>(1).ToUpper().Contains(busqueda)).ToList();

                if (filasCoincidentes.Count > 0)
                {
                    filasCoincidentes.ForEach(row => filasFiltradas.ImportRow(row));

                    dgvClub.Rows.Clear();
                    foreach (DataRow row in filasFiltradas.Rows)
                    {
                        dgvClub.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString() });
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
                    dgvClub.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString() });
                }
            }
        }

        private void btLimpiarCanchas_Click_1(object sender, EventArgs e)
        {
            tbNombreClub1.Text = "";
            tbNombreCortoClub1.Text = "";
            label4.Visible = true;
            label48.Visible = true;
            pbCancha1.Image = Resources.nada;
        }

        private void dgvClub_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label2.Visible = false;
            label49.Visible = false;
            tbNombreClub2.Text = dgvClub.SelectedRows[0].Cells[1].Value.ToString();
            tbNombreCortoClub2.Text = dgvClub.SelectedRows[0].Cells[2].Value.ToString();
            lbAdvertencia.Visible = false;
            //PONER FOTO DEL CLUB QUE SELECCIONO

            string imageUrl = conexionFoto + dgvClub.SelectedRows[0].Cells[3].Value.ToString(); // Reemplaza con la URL de tu imagen remota
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    byte[] imageBytes = webClient.DownloadData(imageUrl);
                    using (var stream = new System.IO.MemoryStream(imageBytes))
                    {
                        System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                        pbCancha2.Image = image;
                        pbCancha2Control.Image = image;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al descargar la imagen: " + ex.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tbNombreClub2_TextChanged(object sender, EventArgs e)
        {
            label2.Visible = false;
            tbNombreClub2.Enabled = true;
            btModificarClub.Enabled = true;

        }
        private void tbNombreCortoClub2_TextChanged(object sender, EventArgs e)
        {
            label49.Visible = false;
            tbNombreCortoClub2.Enabled = true;
            btModificarClub.Enabled = true;

        }

        private void dgvCategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbModificarCategoria.Enabled = true;
            btModificarCategoria.Enabled = true;
            tbModificarCategoria.Text = dgvCategoria.SelectedRows[0].Cells[1].Value.ToString();
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
            if (MessageBox.Show("¿Está seguro que desea eliminar la categoría seleccionada?", "Liga Infantil del Paraná", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                q.EliminarCategoria(dgvCategoria.SelectedRows[0].Cells[0].Value.ToString());
                tbModificarCategoria.Text = "";

                DataTable dt = q.LlenarTablaCategoria();

                dgvCategoria.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dgvCategoria.Rows.Add(new string[] { row[0].ToString(), row[1].ToString() });
                }


                List<string> zonas = new List<string> { "General", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q" };
                List<String> categorias2 = new List<String>();
                categorias2.Clear();

                dtFiltro = q.LlenarTablaZona();

                dgvZona.Rows.Clear();
                foreach (DataRow row in dtFiltro.Rows)
                {
                    dgvZona.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                }

                foreach (DataGridViewRow fila in dgvCategoria.Rows)
                { if (fila.Cells[1].Value != null) { string valor = fila.Cells[1].Value.ToString(); categorias2.Add(valor); } }

                comboZona1.DataSource = categorias2;
                comboZona2.DataSource = zonas;

            }
        }

        private void btEliminarZona_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea eliminar la zona seleccionada?", "Liga Infantil del Paraná", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
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
                q.InsertarCategoria(tbAgregarCategoria.Text);
                tbAgregarCategoria.Text = "";


                DataTable dt = q.LlenarTablaCategoria();

                dgvCategoria.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dgvCategoria.Rows.Add(new string[] { row[0].ToString(), row[1].ToString() });
                }


                List<string> zonas = new List<string> { "General", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q" };
                List<String> categorias2 = new List<String>();
                categorias2.Clear();

                dtFiltro = q.LlenarTablaZona();

                dgvZona.Rows.Clear();
                foreach (DataRow row in dtFiltro.Rows)
                {
                    dgvZona.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                }

                foreach (DataGridViewRow fila in dgvCategoria.Rows)
                { if (fila.Cells[1].Value != null) { string valor = fila.Cells[1].Value.ToString(); categorias2.Add(valor); } }

                comboZona1.DataSource = categorias2;
                comboZona2.DataSource = zonas;

            }
            else
            {
                MessageBox.Show("Error al insertar la categoría, verifique que los datos esten completos y correctos.", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btAgregarZona_Click(object sender, EventArgs e)
        {
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
            if (MessageBox.Show("¿Está seguro que desea modificar la categoria seleccionada?", "Liga Infantil del Paraná", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (tbModificarCategoria.Text != "")
                {
                    q.actualizarCategoria(tbModificarCategoria.Text, dgvCategoria.SelectedRows[0].Cells[0].Value.ToString());
                    DataTable dt = q.LlenarTablaCategoria();

                    dgvCategoria.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dgvCategoria.Rows.Add(new string[] { row[0].ToString(), row[1].ToString() });
                    }
                    tbModificarCategoria.Text = "";
                    tbModificarCategoria.Enabled = false;
                    btModificarCategoria.Enabled = false;


                    List<string> zonas = new List<string> { "General", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q" };
                    List<String> categorias2 = new List<String>();
                    categorias2.Clear();

                    dtFiltro = q.LlenarTablaZona();

                    dgvZona.Rows.Clear();
                    foreach (DataRow row in dtFiltro.Rows)
                    {
                        dgvZona.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                    }

                    foreach (DataGridViewRow fila in dgvCategoria.Rows)
                    { if (fila.Cells[1].Value != null) { string valor = fila.Cells[1].Value.ToString(); categorias2.Add(valor); } }

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
                var filasCoincidentes = dtFiltro.AsEnumerable().Where(row => row.Field<string>(1).ToUpper().Contains(busqueda)).ToList();

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

            string busqueda = tbEquipos3.Text.ToUpper();

            if (!string.IsNullOrEmpty(busqueda))
            {
                var filasCoincidentes = dtFiltro2.AsEnumerable().Where(row => row.Field<string>(1).ToUpper().Contains(busqueda)).ToList();

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
                MessageBox.Show("Error al insertar el equipo, asegúrese de que todos los datos estén completos.", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvEquipo1_Leave(object sender, EventArgs e)
        {

        }

        private void btEliminarEquipo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea eliminar el equipo seleccionado?", "Liga Infantil del Paraná", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                
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
                if (MessageBox.Show("¿Está seguro que desea eliminar la cancha seleccionada?", "Liga Infantil del Paraná", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    
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
                MessageBox.Show("Complete todos los campos para agregar la cancha correspondiente o asegurese que el numero de cancha no sea uno existente.", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gbPartidos_Enter(object sender, EventArgs e)
        {

        }

        private void comboCategoriaEncuentro_SelectedIndexChanged(object sender, EventArgs e)
        {

            
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
            
            if (comboCategoriaEncuentro.Text != "" && comboZonaEncuentro.Text != "" && comboLocalEncuentro1.Text != "" && comboVisitaEncuentro1.Text != "" && comboFechaEncuentro1.Text != "" && comboLocalEncuentro1.SelectedIndex != comboVisitaEncuentro1.SelectedIndex)
            {
                if(checkFechaNuevoEncuentro.Checked == true)
                {
                    q.InsertarEncuentro(comboIDlocal.Items[comboLocalEncuentro1.SelectedIndex].ToString(), comboIDvisita.Items[comboVisitaEncuentro1.SelectedIndex].ToString(), datePickerEnc1.Value.ToString("yyyy-MM-dd"), comboFechaEncuentro1.Text, tbHoraEncuentro1.Text, tbYoutubeEncuentro1.Text, tbDriveEncuentro1.Text);
                }
                else
                {
                    q.InsertarEncuentro(comboIDlocal.Items[comboLocalEncuentro1.SelectedIndex].ToString(), comboIDvisita.Items[comboVisitaEncuentro1.SelectedIndex].ToString(), "", comboFechaEncuentro1.Text, tbHoraEncuentro1.Text, tbYoutubeEncuentro1.Text, tbDriveEncuentro1.Text);
                }

                string filtro;
                if (cbCruces.Checked)
                    filtro = "WHERE and en.tipo <> 1";
                else
                    filtro = "WHERE en.tipo = 1";

                DataTable dt = new DataTable();
                dt = q.LlenarTablaEncuentroFiltrado(!cbPartidos.Checked, comboFiltroEncuentros1.Text, comboFiltroEncuentros2.Text, comboFiltroEncuentros3.Text, filtro);
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
                    row[5].ToString(), // Division
                    row[6].ToString(), // Fecha
                    row[7].ToString(), // Dia
                    row[8].ToString(), // Hora
                    row[9].ToString(), // Youtube
                    row[10].ToString(), // Imagenes
                    row[11].ToString() //terminado
                });
                }

            }
            else
            {
                MessageBox.Show("Complete todos los campos para agregar el encuentro o asegurece que los equipos sean distintos", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvEncuentros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var fila = dgvEncuentros.SelectedRows[0];
            partidoSeleccionado = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString(); ;

            tbLocalEncuentro2.Text = fila.Cells[1].Value.ToString();
            tbGolesLocal.Text = fila.Cells[2].Value.ToString();
            tbGolesVisita.Text = fila.Cells[4].Value.ToString();
            if (fila.Cells[2].Value.ToString() == "" || fila.Cells[4].Value.ToString() == "") { comenzado = false; } else { comenzado = true; }
            tbVisitaEncuentro2.Text = fila.Cells[5].Value.ToString();
            tbCategoriaEncuentro2.Text = "División " + fila.Cells[6].Value.ToString();
            comboFechaEncuentro2.SelectedItem = fila.Cells[7].Value.ToString();
            if(fila.Cells[8].Value != "")
            {
                datePickerEn2.Value = Convert.ToDateTime(fila.Cells[8].Value);
            }
           
            tbHoraEncuentro2.Text = fila.Cells[9].Value.ToString();
            tbYoutubeEncuentro2.Text = fila.Cells[10].Value.ToString();
            tbDriveEncuentro2.Text = fila.Cells[11].Value.ToString();

            if (fila.Cells[12].Value.ToString() == "True")
            {
                checkPartidoTerminado.Checked = true;
            }
            else
            {
                checkPartidoTerminado.Checked = false;
            }
            //comboCanchaEncuentro2.SelectedItem = fila.Cells[11].Value.ToString();

            //Ahora llenar lo de las tarjetas
            if (fila.Cells[2].Value.ToString() != "" && fila.Cells[4].Value.ToString() != "")
            {
                DataTable dt = new DataTable();
                dt = q.ObtenerStats(partidoSeleccionado);
                foreach (DataRow row in dt.Rows)
                {
                    tbAmarillasLocal.Text = row[0].ToString();
                    tbAmarillasVisita.Text = row[1].ToString();
                    tbRojasLocal.Text = row[2].ToString();
                    tbRojasVisita.Text = row[3].ToString();
                }
            }
            else
            {
                tbAmarillasLocal.Text = "";
                tbAmarillasVisita.Text = "";
                tbRojasLocal.Text = "";
                tbRojasVisita.Text = "";
            }
        }

        private void comboLocalEncuentro1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbPartidos_CheckedChanged(object sender, EventArgs e)
        {
            string filtro;
            if (cbCruces.Checked)
                filtro = "WHERE en.tipo <> 1";
            else
                filtro = "WHERE en.tipo = 1";

            
            DataTable dt = new DataTable();
            dt = q.LlenarTablaEncuentroFiltrado(!cbPartidos.Checked, comboFiltroEncuentros1.Text, comboFiltroEncuentros2.Text, comboFiltroEncuentros3.Text, filtro);
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
                    row[5].ToString(), // Division
                    row[6].ToString(), // Fecha
                    row[7].ToString(), // Dia
                    row[8].ToString(), // Hora
                     row[9].ToString(), // Youtube
                    row[10].ToString(), // Imagenes
                    row[11].ToString() //terminado
                });
            }
        }

        private void comboFiltroEncuentros1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            List<String> lista = new List<String>();
            DataTable dt = new DataTable();
            dt = q.LlenarTablaZonaFiltrado(comboFiltroEncuentros1.SelectedItem.ToString());

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(row[0].ToString());
            }
            lista.Add("TODAS");
            comboFiltroEncuentros2.DataSource = lista;
            string filtro;
            if (cbCruces.Checked)
                filtro = "WHERE z.id_zona<> 'Z' and z2.id_zona<> 'Z' and en.tipo <> 1";
            else
                filtro = "WHERE z.id_zona<> 'Z' and z2.id_zona<> 'Z' and en.tipo = 1";

            DataTable dt2 = new DataTable();
            dt2 = q.LlenarTablaEncuentroFiltrado(!cbPartidos.Checked, comboFiltroEncuentros1.Text, comboFiltroEncuentros2.Text, comboFiltroEncuentros3.Text, filtro);
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
                    row[5].ToString(), // Division
                    row[6].ToString(), // Fecha
                    row[7].ToString(), // Dia
                    row[8].ToString(), // Hora
                     row[9].ToString(), // Youtube
                    row[10].ToString(), // Imagenes
                    row[11].ToString() //terminado
                });
            }
        }

        private void comboVisitaEncuentro1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btModificarPartido_Click(object sender, EventArgs e)
        {
            if (comboFechaEncuentro2.Text != "" && tbHoraEncuentro2.Text != "" && ((tbGolesLocal.Text != "" && tbGolesVisita.Text != "") || (tbGolesLocal.Text == "" && tbGolesVisita.Text == "")))
            {
                if (MessageBox.Show("¿Seguro que desea modificar el Encuentro?", "Liga Infantil del Paraná", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (checkFechaEncuentro.Checked == true)
                    {
                    q.actualizarEncuentro(dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString(), tbGolesLocal.Text, tbGolesVisita.Text, comboFechaEncuentro2.Text, tbHoraEncuentro2.Text, comboCanchaEncuentro2.Text
                        , datePickerEn2.Value.ToString("yyyy-MM-dd"), tbYoutubeEncuentro2.Text, tbDriveEncuentro2.Text) ;
                    }
                    else
                    {
                        q.actualizarEncuentro(dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString(), tbGolesLocal.Text, tbGolesVisita.Text, comboFechaEncuentro2.Text, tbHoraEncuentro2.Text, comboCanchaEncuentro2.Text
                         , "", tbYoutubeEncuentro2.Text, tbDriveEncuentro2.Text);
                    }
                    string filtro;
                    if (cbCruces.Checked)
                        filtro = "WHERE en.tipo <> 1";
                    else
                        filtro = "WHERE en.tipo = 1";
                    DataTable dt = new DataTable();
                    dt = q.LlenarTablaEncuentroFiltrado(!cbPartidos.Checked, comboFiltroEncuentros1.Text, comboFiltroEncuentros2.Text, comboFiltroEncuentros3.Text, filtro);
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
                    row[5].ToString(), // Division
                    row[6].ToString(), // Fecha
                    row[7].ToString(), // Dia
                    row[8].ToString(), // Hora
                     row[9].ToString(), // Youtube
                    row[10].ToString(), // Imagenes
                    row[11].ToString() //terminado
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
                MessageBox.Show("Verifique que todos los datos esten completos y correctos", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btModificarClub_Click(object sender, EventArgs e)
        {
            if (tbNombreClub2.Text != "" && tbNombreCortoClub2.Text != "") //NUEVO
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

                    
                    q.actualizarClub(dgvClub.SelectedRows[0].Cells[0].Value.ToString(), tbNombreClub2.Text, tbNombreCortoClub2.Text, "ESCUDOS/"+nombreArchivo);
                    subirFoto2(nombreArchivo);

                    dgvClub.Rows.Clear();
                    DataTable dt = new DataTable();
                    dt = q.LlenarTablaClub();
                    
                    foreach (DataRow row in dt.Rows)
                    {
                        dgvClub.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString() });
                    }
                    lbAdvertencia.Visible = true;
                    tbNombreClub1.Text = "";
                    tbNombreCortoClub1.Text = "";
                    tbNombreClub2.Text = "";
                    tbNombreCortoClub2.Text = "";
                    pbCancha1.Image = Resources.nada;
                    pbCancha2.Image = Resources.nada;
                }
                else
                {
                    string nombreArchivo = dgvClub.SelectedRows[0].Cells[3].Value.ToString();
                    
                    q.actualizarClub(dgvClub.SelectedRows[0].Cells[0].Value.ToString(), tbNombreClub2.Text, tbNombreCortoClub2.Text, nombreArchivo);

                    dgvClub.Rows.Clear();
                    DataTable dt = new DataTable();
                    dt = q.LlenarTablaClub();
                    
                    foreach (DataRow row in dt.Rows)
                    {
                        dgvClub.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString() });
                    }
                    lbAdvertencia.Visible = true;
                    tbNombreClub1.Text = "";
                    tbNombreCortoClub1.Text = "";
                    tbNombreClub2.Text = "";
                    tbNombreCortoClub2.Text = "";
                    pbCancha1.Image = Resources.nada;
                    pbCancha2.Image = Resources.nada;
                    label2.Visible = true;
                    label49.Visible = true;
                    tbNombreClub2.Enabled = false;
                    tbNombreCortoClub2.Enabled = false;
                }

            }
            else
            {
                MessageBox.Show("Debe completar todos los datos obligatorios para insertar un club", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboFiltroEncuentros2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtro;
            if (cbCruces.Checked)
                filtro = "WHERE z.id_zona<> 'Z' and z2.id_zona<> 'Z' and en.tipo <> 1";
            else
                filtro = "WHERE z.id_zona<> 'Z' and z2.id_zona<> 'Z' and en.tipo = 1";
            
            DataTable dt = new DataTable();
            dt = q.LlenarTablaEncuentroFiltrado(!cbPartidos.Checked, comboFiltroEncuentros1.Text, comboFiltroEncuentros2.Text, comboFiltroEncuentros3.Text, filtro);
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
                    row[5].ToString(), // Division
                    row[6].ToString(), // Fecha
                    row[7].ToString(), // Dia
                    row[8].ToString(), // Hora
                     row[9].ToString(), // Youtube
                    row[10].ToString(), // Imagenes
                    row[11].ToString() //terminado
                });
            }
        }

        private void comboFiltroEncuentros3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtro;
            if (cbCruces.Checked)
                filtro = "WHERE en.tipo <> 1";
            else
                filtro = "WHERE en.tipo = 1";

            
            DataTable dt = new DataTable();
            dt = q.LlenarTablaEncuentroFiltrado(!cbPartidos.Checked, comboFiltroEncuentros1.Text, comboFiltroEncuentros2.Text, comboFiltroEncuentros3.Text, filtro);
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
                    row[5].ToString(), // Division
                    row[6].ToString(), // Fecha
                    row[7].ToString(), // Dia
                    row[8].ToString(), // Hora
                    row[9].ToString(), // Youtube
                    row[10].ToString(), // Imagenes
                    row[11].ToString() //terminado
                });
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboFiltroEncuentros1.Items.Clear();
            comboFiltroEncuentros3.Items.Clear();

            List<String> list = new List<String>();

            dtFiltro = q.LlenarTablaCategoria();

            comboCategoriaEncuentro.Items.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                comboCategoriaEncuentro.Items.Add(row[0].ToString());
                comboFiltroEncuentros1.Items.Add(row[0].ToString());
            }

            //dtFiltro = query.LlenarTablaCanchas();

            comboCanchaEncuentro1.Items.Clear();
            comboCanchaEncuentro2.Items.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                comboCanchaEncuentro1.Items.Add(row[0].ToString());
                comboCanchaEncuentro2.Items.Add(row[0].ToString());

            }
            comboCanchaEncuentro2.SelectedItem = null;

            dtFiltro = q.LlenarTablaFechas();

            comboFechaEncuentro1.Items.Clear();
            comboFechaEncuentro2.Items.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                comboFechaEncuentro1.Items.Add(row[0].ToString());
                comboFechaEncuentro2.Items.Add(row[0].ToString());
                comboFiltroEncuentros3.Items.Add(row[0].ToString());
            }
            comboFechaEncuentro2.SelectedItem = null;

            dtFiltro = q.LlenarTablaEncuentro("WHERE en.tipo = 1");
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
                    row[5].ToString(), // Division
                    row[6].ToString(), // Fecha
                    row[7].ToString(), // Dia
                    row[8].ToString(), // Hora
                     row[9].ToString(), // Youtube
                    row[10].ToString(), // Imagenes
                    row[11].ToString() //terminado
                });
            }
            comboFiltroEncuentros1.SelectedIndex = -1;
            comboFiltroEncuentros2.SelectedIndex = -1;
            comboFiltroEncuentros3.SelectedIndex = -1;
            comboFiltroEncuentros1.Text = "";
            comboFiltroEncuentros3.Text = "";
            cbCruces.Checked = false;
        }

        private void comboCategoriaCruce_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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
                if (MessageBox.Show("¿Está seguro que desea eliminar el encuentro seleccionado?", "Liga Infantil del Paraná", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    
                    q.EliminarEncuentro(dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString());

                    string filtro;
                    if (cbCruces.Checked)
                        filtro = "WHERE en.tipo <> 1";
                    else
                        filtro = "WHERE en.tipo = 1";

                    DataTable dt2 = new DataTable();
                    dt2 = q.LlenarTablaEncuentroFiltrado(!cbPartidos.Checked, comboFiltroEncuentros1.Text, comboFiltroEncuentros2.Text, comboFiltroEncuentros3.Text, filtro);
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
                    row[5].ToString(), // Division
                    row[6].ToString(), // Fecha
                    row[7].ToString(), // Dia
                    row[8].ToString(), // Hora
                    row[9].ToString(), // Youtube
                    row[10].ToString(), // Imagenes
                    row[11].ToString() //terminado
                });
                    }

                    foreach (DataGridViewRow row in dgvEncuentros.Rows)
                    {
                        if (row.Cells[0].Value.ToString() == partidoSeleccionado) { row.Selected = true; }
                        else { row.Selected = false; }
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
            //pictureBox4.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void btClubes_Leave(object sender, EventArgs e)
        {
            //pictureBox4.BackColor = Color.FromArgb(17, 15, 21);
        }

        private void btCyZ_MouseEnter(object sender, EventArgs e)
        {
            //pictureBox5.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void btCyZ_MouseLeave(object sender, EventArgs e)
        {
            //pictureBox5.BackColor = Color.FromArgb(17, 15, 21);
        }

        private void btEquipos_MouseEnter(object sender, EventArgs e)
        {
            //pictureBox6.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void btEquipos_MouseLeave(object sender, EventArgs e)
        {
            //pictureBox6.BackColor = Color.FromArgb(17, 15, 21);
        }

        private void btCanchas_MouseEnter(object sender, EventArgs e)
        {
            //pictureBox9.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void btCanchas_MouseLeave(object sender, EventArgs e)
        {
            //pictureBox9.BackColor = Color.FromArgb(17, 15, 21);
        }

        private void btEncuentros_MouseEnter(object sender, EventArgs e)
        {
            //pictureBox8.BackColor = Color.FromArgb(229, 224, 215);

        }

        private void btEncuentros_MouseLeave(object sender, EventArgs e)
        {
            //pictureBox8.BackColor = Color.FromArgb(17, 15, 21);
        }

        private void btCruces_MouseEnter(object sender, EventArgs e)
        {
            //pictureBox7.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void btCruces_MouseLeave(object sender, EventArgs e)
        {
            //pictureBox7.BackColor = Color.FromArgb(17, 15, 21);
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            //pictureBox4.BackColor = Color.FromArgb(229, 224, 215);
            //btClubes.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            //btClubes.BackColor = Color.FromArgb(17, 15, 21);
            //pictureBox4.BackColor = Color.FromArgb(17, 15, 21);

        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            //pictureBox5.BackColor = Color.FromArgb(229, 224, 215);
            //btCyZ.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            //btCyZ.BackColor = Color.FromArgb(17, 15, 21);
            //pictureBox5.BackColor = Color.FromArgb(17, 15, 21);

        }

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            //pictureBox6.BackColor = Color.FromArgb(229, 224, 215);
            //btEquipos.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            //btEquipos.BackColor = Color.FromArgb(17, 15, 21);
            //pictureBox6.BackColor = Color.FromArgb(17, 15, 21);

        }

        private void pictureBox9_MouseEnter(object sender, EventArgs e)
        {
            //pictureBox9.BackColor = Color.FromArgb(229, 224, 215);
            //btCanchas.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void pictureBox9_MouseLeave(object sender, EventArgs e)
        {
            //btCanchas.BackColor = Color.FromArgb(17, 15, 21);
            //pictureBox9.BackColor = Color.FromArgb(17, 15, 21);

        }

        private void pictureBox8_MouseEnter(object sender, EventArgs e)
        {
            //pictureBox8.BackColor = Color.FromArgb(229, 224, 215);
            //btEncuentros.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            //btEncuentros.BackColor = Color.FromArgb(17, 15, 21);
            //pictureBox8.BackColor = Color.FromArgb(17, 15, 21);

        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            //pictureBox7.BackColor = Color.FromArgb(229, 224, 215);
            //btCruces.BackColor = Color.FromArgb(229, 224, 215);
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            //btCruces.BackColor = Color.FromArgb(17, 15, 21);
            //pictureBox7.BackColor = Color.FromArgb(17, 15, 21);
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

        private void btPDF_Click(object sender, EventArgs e)
        {
            GenerarVistaPreviaPDF();
        }

        private void btPDF_Click_1(object sender, EventArgs e)
        {
            GenerarVistaPreviaPDF();
        }

        private void cbCruces_CheckedChanged(object sender, EventArgs e)
        {
            string filtro;
            if (cbCruces.Checked)
                filtro = "WHERE z.id_zona<> 'Z' and z2.id_zona<> 'Z' and en.tipo <> 1";
            else
                filtro = "WHERE z.id_zona<> 'Z' and z2.id_zona<> 'Z' and en.tipo = 1";

            
            DataTable dt = new DataTable();
            dt = q.LlenarTablaEncuentroFiltrado(!cbPartidos.Checked, comboFiltroEncuentros1.Text, comboFiltroEncuentros2.Text, comboFiltroEncuentros3.Text, filtro);
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
                    row[5].ToString(), // Division
                    row[6].ToString(), // Fecha
                    row[7].ToString(), // Dia
                    row[8].ToString(), // Hora
                     row[9].ToString(), // Youtube
                    row[10].ToString(), // Imagenes
                    row[11].ToString() //terminado
                });
            }
        }

        private void btSeleccionar_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvEncuentros.Rows)
            {
                row.Selected = true;
            }
        }

        private void dgvClub_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tbNombreCortoClub1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbNombreClub1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvCategoria_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox10_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "add-goal-local", this, comenzado);
            addStat.Show();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "rest-goal-local", this, comenzado);
            addStat.Show();
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "add-goal-visit", this, comenzado);
            addStat.Show();
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "rest-goal-visit", this, comenzado);
            addStat.Show();
        }

        private void pictureBox14_Click_1(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "add-yc-local", this, comenzado);
            addStat.Show();
        }

        private void pictureBox13_Click_1(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "rest-yc-local", this, comenzado);
            addStat.Show();
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "add-yc-visit", this, comenzado);
            addStat.Show();
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "rest-yc-visit", this, comenzado);
            addStat.Show();
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "add-rc-local", this, comenzado);
            addStat.Show();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "rest-rc-local", this, comenzado);
            addStat.Show();
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "add-rc-visit", this, comenzado);
            addStat.Show();
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "rest-rc-visit", this, comenzado);
            addStat.Show();
        }


        public void refreshEncuentros(bool comenzar)
        {
            
            string filtro;
            if (cbCruces.Checked)
                filtro = "WHERE en.tipo <> 1";
            else
                filtro = "WHERE en.tipo = 1";
            DataTable dt = new DataTable();
            if (comenzar) { cbPartidos.Checked = true; }
            dt = q.LlenarTablaEncuentroFiltrado(!cbPartidos.Checked, comboFiltroEncuentros1.Text, comboFiltroEncuentros2.Text, comboFiltroEncuentros3.Text, filtro);
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
                    row[5].ToString(), // Division
                    row[6].ToString(), // Fecha
                    row[7].ToString(), // Dia
                    row[8].ToString(), // Hora
                     row[9].ToString(), // Youtube
                    row[10].ToString(), // Imagenes
                    row[11].ToString() //terminado
                });
            }

            foreach (DataGridViewRow row in dgvEncuentros.Rows)
            {
                if (row.Cells[0].Value.ToString() == partidoSeleccionado)
                {
                    row.Selected = true;
                }
                else
                {
                    row.Selected = false;
                }
            }
            /*
            if (dgvEncuentros.Rows.Count > 0)
            {
                dgvEncuentros.CurrentCell = dgvEncuentros.Rows[0].Cells[1];
                dgvEncuentros.Rows[0].Selected = true;
            }
            */
            dgvEncuentros_CellClick(null, null);
        }

        private void limpiarFichaJugadores()
        {
            tbNombreJugador1.Text = string.Empty;
            tbDNIJugador1.Text = string.Empty;
            tbDorsalJugador1.Text = string.Empty;
            tbEquipoJugador1.Text = string.Empty;
            tbFiltrarEquipoJugador1.Text = string.Empty;
            pbJugador1.Image = Resources.nada;


            tbNombreJugador2.Text = string.Empty;
            tbDNIJugador2.Text = string.Empty;
            tbDorsalJugador2.Text = string.Empty;
            tbEquipoJugador2.Text = string.Empty;
            tbFiltroEquipoJugador2.Text = string.Empty;
            pbJugador2.Image = Resources.nada;

            btEliminarJugador.Enabled = false;
            cbCambiarEquipoJugador.Checked = false;
            tbFiltroEquipoJugador2.Text = "";
            tbFiltroEquipoJugador2.Enabled = false;
        }
        private void btJugadores_Click(object sender, EventArgs e)
        {
            gbClubes.Visible = false;
            gbCanchas.Visible = false;
            gbCyZ.Visible = false;
            gbEquipos.Visible = false;
            gbJugadores.Visible = true;
            gbCruces.Visible = false;
            gbPartidos.Visible = false;

            pbJugador1.Image = Resources.nada;
            pbJugador2.Image = Resources.nada;

            limpiarFichaJugadores();


            DataTable dt = new DataTable();

            dtFiltro = q.LlenarTablaEquipoJugadorFiltrado("");

            dgvJugador1.Rows.Clear();
            foreach (DataRow row in dtFiltro.Rows)
            {
                dgvJugador1.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
            }

            dtFiltro2 = q.LlenarTablaJugador();

            dgvJugador2.Rows.Clear();
            foreach (DataRow row in dtFiltro2.Rows)
            {
                dgvJugador2.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString() });
            }
        }

        private void tbFiltrarEquipoJugador1_TextChanged(object sender, EventArgs e)
        {
            var filasFiltradas = dtFiltro.Clone();

            string busqueda = tbFiltrarEquipoJugador1.Text.ToUpper();

            if (!string.IsNullOrEmpty(busqueda))
            {
                var filasCoincidentes = dtFiltro.AsEnumerable().Where(row => row.Field<string>(1).ToUpper().Contains(busqueda)).ToList();

                if (filasCoincidentes.Count > 0)
                {
                    filasCoincidentes.ForEach(row => filasFiltradas.ImportRow(row));

                    dgvJugador1.Rows.Clear();
                    foreach (DataRow row in filasFiltradas.Rows)
                    {
                        dgvJugador1.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                    }

                }
                else
                {
                    dgvJugador1.DataSource = null;
                }
            }
            else
            {
                dgvJugador1.Rows.Clear();
                foreach (DataRow row in dtFiltro.Rows)
                {
                    dgvJugador1.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                }
            }
        }

        private void tbFiltroEquipoJugador2_TextChanged(object sender, EventArgs e)
        {
            if (tbFiltroEquipoJugador2.Text == "") { dgvJugador3.Rows.Clear(); }
            var filasFiltradas = dtFiltro.Clone();

            string busqueda = tbFiltroEquipoJugador2.Text.ToUpper();

            if (!string.IsNullOrEmpty(busqueda))
            {
                var filasCoincidentes = dtFiltro.AsEnumerable().Where(row => row.Field<string>(1).ToUpper().Contains(busqueda)).ToList();

                if (filasCoincidentes.Count > 0)
                {
                    filasCoincidentes.ForEach(row => filasFiltradas.ImportRow(row));

                    dgvJugador3.Rows.Clear();
                    foreach (DataRow row in filasFiltradas.Rows)
                    {
                        dgvJugador3.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                    }

                }
                else
                {
                    dgvJugador3.DataSource = null;
                }
            }
            else
            {
                dgvJugador3.Rows.Clear();
                foreach (DataRow row in dtFiltro.Rows)
                {
                    dgvJugador3.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                }
            }
        }

        private void tbNombreJugador3_TextChanged(object sender, EventArgs e)
        {
            var filasFiltradas = dtFiltro2.Clone();

            string busqueda1 = tbNombreJugador3.Text.ToUpper();
            string busqueda2 = tbEquipoJugador3.Text.ToUpper();

            if (!string.IsNullOrEmpty(busqueda1))
            {
                if (!string.IsNullOrEmpty(busqueda2))
                {
                    var filasCoincidentes = dtFiltro2.AsEnumerable().Where(row => row.Field<string>(1).ToUpper().Contains(busqueda1) && row.Field<string>(3).ToUpper().Contains(busqueda2)).ToList();

                    if (filasCoincidentes.Count > 0)
                    {
                        filasCoincidentes.ForEach(row => filasFiltradas.ImportRow(row));

                        dgvJugador2.Rows.Clear();
                        foreach (DataRow row in filasFiltradas.Rows)
                        {
                            dgvJugador2.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString() });
                        }

                    }
                    else
                    {
                        dgvJugador2.DataSource = null;
                    }
                }
                else
                {
                    var filasCoincidentes = dtFiltro2.AsEnumerable().Where(row => row.Field<string>(1).ToUpper().Contains(busqueda1)).ToList();

                    if (filasCoincidentes.Count > 0)
                    {
                        filasCoincidentes.ForEach(row => filasFiltradas.ImportRow(row));

                        dgvJugador2.Rows.Clear(); 
                        foreach (DataRow row in filasFiltradas.Rows)
                        {
                            dgvJugador2.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString() , row[4].ToString() });
                        }

                    }
                    else
                    {
                        dgvJugador2.DataSource = null;
                    }
                }
            }
            else
            {
                dgvJugador2.Rows.Clear();
                foreach (DataRow row in dtFiltro2.Rows)
                {
                    dgvJugador2.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString() , row[4].ToString() });
                }
            }
        }

        private void dgvJugador1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbEquipoJugador1.Text = dgvJugador1.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void btAgregarJugador_Click(object sender, EventArgs e)
        {
            if (tbNombreJugador1.Text == string.Empty || tbDNIJugador1.Text == string.Empty || tbDorsalJugador1.Text == string.Empty || tbEquipoJugador1.Text == string.Empty)
            {
                MessageBox.Show("Error al insertar el jugador, verifique que los campos esten completos y correctos", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            byte[] imageBytes1;
            byte[] imageBytes2;

            MemoryStream ms1 = new MemoryStream();
            MemoryStream ms2 = new MemoryStream();
            pbJugador1.Image.Save(ms1, ImageFormat.Png);
            Resources.nada.Save(ms2, ImageFormat.Png);

            imageBytes1 = ms1.ToArray();
            imageBytes2 = ms2.ToArray();

            if (!imageBytes1.SequenceEqual(imageBytes2))
            {
                string nombreArchivo = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
                q.InsertarJugador(tbDNIJugador1.Text, tbNombreJugador1.Text, tbDorsalJugador1.Text, dgvJugador1.SelectedRows[0].Cells[0].Value.ToString(), nombreArchivo);
                

                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

                string rutaCompleta = Path.Combine(Resources.carpetaFotosServer, nombreArchivo);

                subirFotoAgregarJugador(nombreArchivo);

                DataTable dt = new DataTable();
                dt = q.LlenarTablaJugador();
                dgvJugador2.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dgvJugador2.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString() });
                }
                limpiarFichaJugadores();
            }
            else
            {
                if (MessageBox.Show("No ha insertado una foto para el jugador, ¿Desea agregar el jugador sin foto? Se pondra una por defecto", "Liga Infantil del Paraná", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string nombreArchivo = "0.png";

                    q.InsertarJugador(tbDNIJugador1.Text, tbNombreJugador1.Text, tbDorsalJugador1.Text, dgvJugador1.SelectedRows[0].Cells[0].Value.ToString(), nombreArchivo);


                    DataTable dt = new DataTable();
                    dt = q.LlenarTablaJugador();
                    dgvJugador2.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dgvJugador2.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString() });
                    }
                    limpiarFichaJugadores();
                }

            }
        }

        private void pbJugador1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbJugador1.Image = new Bitmap(openFileDialog.FileName);
            }
        }


        private void dgvJugador2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btEliminarJugador.Enabled = true;
            tbDNIJugador2.Text = dgvJugador2.SelectedRows[0].Cells[0].Value.ToString();
            tbNombreJugador2.Text = dgvJugador2.SelectedRows[0].Cells[1].Value.ToString();
            tbDorsalJugador2.Text = dgvJugador2.SelectedRows[0].Cells[2].Value.ToString();
            tbEquipoJugador2.Text = dgvJugador2.SelectedRows[0].Cells[3].Value.ToString();

            string imageUrl = conexionFoto + dgvJugador2.SelectedRows[0].Cells[4].Value.ToString();
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    byte[] imageBytes = webClient.DownloadData(imageUrl);
                    using (var stream = new System.IO.MemoryStream(imageBytes))
                    {
                        System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                        pbJugador2.Image = image;
                        pbJugador2Control.Image = image;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al descargar la imagen: " + ex.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tbEquipoJugador3_TextChanged(object sender, EventArgs e)
        {
            var filasFiltradas = dtFiltro2.Clone();

            string busqueda1 = tbNombreJugador3.Text.ToUpper();
            string busqueda2 = tbEquipoJugador3.Text.ToUpper();

            if (!string.IsNullOrEmpty(busqueda1))
            {
                if (!string.IsNullOrEmpty(busqueda2))
                {
                    var filasCoincidentes = dtFiltro2.AsEnumerable().Where(row => row.Field<string>(1).ToUpper().Contains(busqueda1) && row.Field<string>(3).ToUpper().Contains(busqueda2)).ToList();

                    if (filasCoincidentes.Count > 0)
                    {
                        filasCoincidentes.ForEach(row => filasFiltradas.ImportRow(row));

                        dgvJugador2.Rows.Clear();
                        foreach (DataRow row in filasFiltradas.Rows)
                        {
                            dgvJugador2.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString() });
                        }

                    }
                    else
                    {
                        dgvJugador2.DataSource = null;
                    }
                }
                else
                {
                    var filasCoincidentes = dtFiltro2.AsEnumerable().Where(row => row.Field<string>(1).ToUpper().Contains(busqueda1)).ToList();

                    if (filasCoincidentes.Count > 0)
                    {
                        filasCoincidentes.ForEach(row => filasFiltradas.ImportRow(row));

                        dgvJugador2.Rows.Clear();
                        foreach (DataRow row in filasFiltradas.Rows)
                        {
                            dgvJugador2.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString() });
                        }

                    }
                    else
                    {
                        dgvJugador2.DataSource = null;
                    }
                }
            }
            else
            {
                dgvJugador2.Rows.Clear();
                foreach (DataRow row in dtFiltro2.Rows)
                {
                    dgvJugador2.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString() });
                }
            }
        }

        private void cbCambiarEquipoJugador_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCambiarEquipoJugador.Checked == true)
            {
                tbFiltroEquipoJugador2.Enabled = true;
            }
            else if (cbCambiarEquipoJugador.Checked == false)
            {
                tbFiltroEquipoJugador2.Enabled = false;
                tbFiltroEquipoJugador2.Text = "";
                dgvJugador3.Rows.Clear();
            }
        }

        private void pbJugador2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbJugador2.Image = new Bitmap(openFileDialog.FileName);
            }
        }

        private void btModificarJugador_Click(object sender, EventArgs e)
        {
            if (tbNombreJugador2.Text == string.Empty || tbDNIJugador2.Text == string.Empty || tbDorsalJugador2.Text == string.Empty || tbEquipoJugador2.Text == string.Empty)
            {
                MessageBox.Show("Error al insertar el jugador, verifique que los campos esten completos y correctos", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            byte[] imageBytes1;
            byte[] imageBytes2;

            MemoryStream ms1 = new MemoryStream();
            MemoryStream ms2 = new MemoryStream();

            pbJugador2.Image.Save(ms1, ImageFormat.Png);
            pbJugador2Control.Image.Save(ms2, ImageFormat.Png);

            imageBytes1 = ms1.ToArray();
            imageBytes2 = ms2.ToArray();

                if (!imageBytes1.SequenceEqual(imageBytes2))
                {
                    string nombreArchivo = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";

                    if (cbCambiarEquipoJugador.Checked == false)
                    {
                        q.actualizarJugador(tbDNIJugador2.Text, tbNombreJugador2.Text, tbDorsalJugador2.Text, "", "jugadores/"+nombreArchivo);
                    }
                    else
                    {
                        q.actualizarJugador(tbDNIJugador2.Text, tbNombreJugador2.Text, tbDorsalJugador2.Text, dgvJugador3.SelectedRows[0].Cells[0].Value.ToString(), "jugadores/"+nombreArchivo);
                    }
                    subirFotoModificarJugador(nombreArchivo);


                    DataTable dt = new DataTable();
                    dt = q.LlenarTablaJugador();
                    dgvJugador2.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                    dgvJugador2.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString() });
                    }
                limpiarFichaJugadores();
                }
                else
                {
                    string nombreArchivo = dgvJugador2.SelectedRows[0].Cells[4].Value.ToString();

                    if (cbCambiarEquipoJugador.Checked == false)
                    {
                        q.actualizarJugador(tbDNIJugador2.Text, tbNombreJugador2.Text, tbDorsalJugador2.Text, "", nombreArchivo);
                    }
                    else
                    {
                        q.actualizarJugador(tbDNIJugador2.Text, tbNombreJugador2.Text, tbDorsalJugador2.Text, dgvJugador3.SelectedRows[0].Cells[0].Value.ToString(), nombreArchivo);
                    }

                     DataTable dt = new DataTable();
                    dt = q.LlenarTablaJugador();
                    dgvJugador2.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dgvJugador2.Rows.Add(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString() });
                    }
                    limpiarFichaJugadores();
                }
        }

        private void btEliminarJugador_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea eliminar el jugador seleccionado? Se quitará cualquier acción relacionada al mismo", "Liga Infantil del Paraná", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                q.EliminarJugador(dgvJugador2.SelectedRows[0].Cells[0].Value.ToString());
                btJugadores_Click(null, null);
            }
        }

        private void checkFechaEncuentro_CheckedChanged(object sender, EventArgs e)
        {
            if(checkFechaEncuentro.Checked == true)
            {
                datePickerEn2.Enabled = true;
            }
            else
            {
                datePickerEn2.Enabled=false;
            }
        }

        private void checkFechaNuevoEncuentro_CheckedChanged(object sender, EventArgs e)
        {
            if (checkFechaNuevoEncuentro.Checked == true)
            {
                datePickerEnc1.Enabled = true;
            }
            else
            {
                datePickerEnc1.Enabled = false;
            }
        }

        private void checkPartidoTerminado_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPartidoTerminado.Checked == true)
            {
                if (MessageBox.Show("¿Está seguro que desea finalizar el partido?", "Liga Infantil del Paraná", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    q.cambiarEstadoPartido(checkPartidoTerminado.Checked, partidoSeleccionado, Int32.Parse(tbGolesLocal.Text), Int32.Parse(tbGolesVisita.Text));
                }
                else
                {
                    checkPartidoTerminado.Checked = false;
                }
            }
            else if (checkPartidoTerminado.Checked == false)
            {
                if (MessageBox.Show("¿Está seguro que desea reaundar el partido?", "Liga Infantil del Paraná", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    q.cambiarEstadoPartido(checkPartidoTerminado.Checked, partidoSeleccionado, Int32.Parse(tbGolesLocal.Text), Int32.Parse(tbGolesVisita.Text));

                }
                else
                {
                    checkPartidoTerminado.Checked = true;
                }
            }
        }
    }
}
