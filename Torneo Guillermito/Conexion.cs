using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torneo_Guillermito
{
    internal class conexion
    {



        string cadena = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        public MySqlConnection conectarbd = new MySqlConnection();

        public conexion()
        {

            conectarbd.ConnectionString = cadena;

        }

        public void abrirconexion()
        {
            try
            {
                conectarbd.Open();
                Console.WriteLine("Conexion abierta");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void cerrarconexion()
        {
            conectarbd.Close();
        }
    }
}
