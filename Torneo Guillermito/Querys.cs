using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Torneo_Guillermito
{
    internal class Querys
    {
        conexion cn = new conexion();

        public DataTable LlenarTablaCanchas()
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }


            string cadena = "SELECT id as Numero, latitud as Latitud, longitud as Longitud FROM cancha";
           
            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_canchas");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_canchas"];

        }


    }
}
