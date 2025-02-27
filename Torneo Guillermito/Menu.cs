﻿using MySql.Data.MySqlClient;
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
using Torneo_Guillermito.Properties;
using System.Drawing.Imaging;
using System.Net.Sockets;
using System.Drawing.Printing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics.Eventing.Reader;

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
            this.pictureBox1.BackColor = System.Drawing.ColorTranslator.FromHtml("#110f15");
            this.pictureBox3.BackColor = System.Drawing.ColorTranslator.FromHtml("#110f15");

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
                MessageBox.Show("Seleccione partidos para imprimir", "Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Error al subir la imagen al servidor", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //

            Querys q = new Querys();

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

            Querys q = new Querys();
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

            tbEquipos1.Text = "";
            textBox2.Text = "";


            Querys q = new Querys();
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

            Querys q = new Querys();
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

            dtFiltro = query.LlenarTablaEncuentro("WHERE en.tipo = 1");
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

                    Querys q = new Querys();
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
                    if (MessageBox.Show("No ha insertado una foto para el escudo, ¿Desea agregar el club sin escudo? Se pondra un escudo por defecto", "Torneo Guillermito", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string nombreArchivo = "0.png";

                        Querys q = new Querys();
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
            if (MessageBox.Show("¿Está seguro que desea eliminar la categoría seleccionada?", "Torneo Guillermito", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Querys q = new Querys();
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
            if (comboCategoriaEncuentro.Text != "" && comboZonaEncuentro.Text != "" && comboLocalEncuentro1.Text != "" && comboVisitaEncuentro1.Text != "" && comboFechaEncuentro1.Text != "" && comboLocalEncuentro1.SelectedIndex != comboVisitaEncuentro1.SelectedIndex)
            {
                q.InsertarEncuentro(comboIDlocal.Items[comboLocalEncuentro1.SelectedIndex].ToString(), comboIDvisita.Items[comboVisitaEncuentro1.SelectedIndex].ToString(), datePickerEnc1.Value.ToString("yyyy-MM-dd"), comboFechaEncuentro1.Text, tbHoraEncuentro1.Text, tbYoutubeEncuentro1.Text, tbDriveEncuentro1.Text);

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
            tbCategoriaEncuentro2.Text = "División " + fila.Cells[6].Value.ToString();
            comboFechaEncuentro2.SelectedItem = fila.Cells[7].Value.ToString();
            if(fila.Cells[8].Value != "")
            {
                datePickerEn2.Value = Convert.ToDateTime(fila.Cells[8].Value);
            }
           
            tbHoraEncuentro2.Text = fila.Cells[9].Value.ToString();
            tbYoutubeEncuentro2.Text = fila.Cells[10].Value.ToString();
            tbDriveEncuentro2.Text = fila.Cells[11].Value.ToString();
            //comboCanchaEncuentro2.SelectedItem = fila.Cells[11].Value.ToString();
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

            Querys q = new Querys();
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
                if (MessageBox.Show("¿Seguro que desea modificar el Encuentro?", "Torneo Guillermito", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Querys q = new Querys();
                    q.actualizarEncuentro(dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString(), tbGolesLocal.Text, tbGolesVisita.Text, comboFechaEncuentro2.Text, tbHoraEncuentro2.Text, comboCanchaEncuentro2.Text);

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

                    Querys q = new Querys();
                    q.actualizarClub(dgvClub.SelectedRows[0].Cells[0].Value.ToString(), tbNombreClub2.Text, tbNombreCortoClub2.Text, "ESCUDOS/"+nombreArchivo);
                    subirFoto2(nombreArchivo);


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
                    tbNombreClub2.Text = "";
                    tbNombreCortoClub2.Text = "";
                    pbCancha1.Image = Resources.nada;
                    pbCancha2.Image = Resources.nada;
                }
                else
                {
                    string nombreArchivo = dgvClub.SelectedRows[0].Cells[3].Value.ToString();
                    Querys q = new Querys();
                    q.actualizarClub(dgvClub.SelectedRows[0].Cells[0].Value.ToString(), tbNombreClub2.Text, tbNombreCortoClub2.Text, nombreArchivo);


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
                MessageBox.Show("Debe completar todos los datos obligatorios para insertar un club", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboFiltroEncuentros2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtro;
            if (cbCruces.Checked)
                filtro = "WHERE z.id_zona<> 'Z' and z2.id_zona<> 'Z' and en.tipo <> 1";
            else
                filtro = "WHERE z.id_zona<> 'Z' and z2.id_zona<> 'Z' and en.tipo = 1";
            Querys q = new Querys();
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

            Querys q = new Querys();
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

            //dtFiltro = query.LlenarTablaCanchas();

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

            dtFiltro = query.LlenarTablaEncuentro("WHERE en.tipo = 1");
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

            Querys q = new Querys();
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
            AddStat addStat = new AddStat(id_partido, "add-goal-local");
            addStat.Show();
        }

        private void dgvEncuentros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "rest-goal-local");
            addStat.Show();
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "add-goal-visit");
            addStat.Show();
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "rest-goal-visit");
            addStat.Show();
        }

        private void pictureBox14_Click_1(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "add-yc-local");
            addStat.Show();
        }

        private void pictureBox13_Click_1(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "rest-yc-local");
            addStat.Show();
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "add-yc-visit");
            addStat.Show();
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "rest-yc-visit");
            addStat.Show();
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "add-rc-local");
            addStat.Show();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "rest-rc-local");
            addStat.Show();
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "add-rc-visit");
            addStat.Show();
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            string id_partido = dgvEncuentros.SelectedRows[0].Cells[0].Value.ToString();
            AddStat addStat = new AddStat(id_partido, "rest-rc-visit");
            addStat.Show();
        }
    }
}
