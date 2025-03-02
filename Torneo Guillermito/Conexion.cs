 using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIPa
{
    internal class conexion
    {



        //string cadena = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        string cadena = "Server=vps-4677860-x.dattaweb.com;Database=vps4_lipa_test;Uid=vps4_admin;Pwd=root;";
        //contraseña del ssh: DA5wjdo56MVN%I
        public MySqlConnection conectarbd = new MySqlConnection();

        public conexion()
        {
            Console.WriteLine(cadena); 
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
