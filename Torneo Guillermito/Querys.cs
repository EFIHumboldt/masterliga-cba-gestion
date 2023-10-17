using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;


namespace Torneo_Guillermito
{
    internal class Querys
    {
        conexion cn = new conexion();

        public DataTable LlenarTablaCanchas()
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }


            string cadena = "SELECT id_cancha as Numero, latitud as Latitud, longitud as Longitud FROM cancha";
           
            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_canchas");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_canchas"];

        }

        public DataTable LlenarTablaCategoria()
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }


            string cadena = "SELECT anio as AÑO FROM categoria";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_canchas");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_canchas"];

        }

        public DataTable LlenarTablaEquipo()
        {
            
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }

            string cadena = "SELECT e.id_equipo as ID, c.nombre as Nombre, e.categoria as Categoria, z.id_zona as Zona FROM equipo as e INNER JOIN club as c ON e.club = c.id_club INNER JOIN zona as z ON e.zona = z.id WHERE z.id_zona <> 'Z';";
            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_canchas");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_canchas"];
            
        }

        public void InsertarCancha(string numero, string latitud, string longitud)
        {
            String cadena = "INSERT INTO cancha Values ('" + numero + "', '" + latitud + "', '" + longitud + "');";

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Cancha '" + numero + "' insertado correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e) {
                //MessageBox.Show(e.Message);
                MessageBox.Show("Error al insertar la cancha, verifique que los datos esten completos y correctos. Asegúrese además que el número de cancha que intenta ingresar no sea uno existente.", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            cn.cerrarconexion();
        }

        public void InsertarCategoria(string numero)
        {
            String cadena = "INSERT INTO categoria Values ('" + numero + "');";

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Categoría '" + numero + "' insertada correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                MessageBox.Show("Error al insertar la categoría, verifique que los datos esten completos y correctos. Asegúrese además que el número de cancha que intenta ingresar no sea uno existente.", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.cerrarconexion();
        }

        public void EliminarCancha(String codigo)
        {

            String cadena = "DELETE FROM cancha where id =" + codigo;
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Cancha eliminada correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e) { MessageBox.Show(e.Message); }

            cn.cerrarconexion();
        }

        public void EliminarClub(String codigo)
        {

            String cadena = "DELETE FROM club where id_club =" + codigo;
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Club eliminado correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e) { MessageBox.Show(e.Message); }

            cn.cerrarconexion();
        }

        public void EliminarEquipo(String codigo)
        {

            String cadena = "DELETE FROM equipo where id_equipo =" + codigo;
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Equipo eliminado correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e) { MessageBox.Show(e.Message); }

            cn.cerrarconexion();
        }

        public void InsertarClub(string nombre, string foto)
        {
            String cadena = "INSERT INTO club (nombre, escudo) Values ('" + nombre + "', '" + foto + "');";

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Club '" + nombre + "' insertado correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                MessageBox.Show("Error al insertar el club, verifique que los datos esten completos y correctos. Asegúrese además que el número de cancha que intenta ingresar no sea uno existente.", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.cerrarconexion();
        }

        public void InsertarZona(string categoria, string zona)
        {
            String cadena = "INSERT INTO zona (id_zona, categoria) Values ('" + zona + "', '" + categoria + "');";

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Zona '" + categoria + " - " +zona+ "' insertada correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {     
                MessageBox.Show("Error al insertar la zona, verifique que los datos esten completos y correctos. Asegúrese además que el número de cancha que intenta ingresar no sea uno existente.", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.cerrarconexion();
        }

        public void InsertarEquipo(string club, string categoria, string zona)
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            string cadena = "SELECT id FROM zona WHERE id_zona = '" + zona + "' and categoria = '" +  categoria + "'";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_zona");
            DataTable dt = new DataTable();
            dt = ds.Tables["tabla_resultados_zona"];
            DataRow dr = dt.Rows[0];
            cadena = "INSERT INTO equipo (club, categoria, zona) Values ('" + club + "', '" + categoria + "', '" + dr[0].ToString() + "');";
  
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Zona '" + categoria + " - " + zona + "' insertada correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                MessageBox.Show("Error al insertar la zona, verifique que los datos esten completos y correctos. Asegúrese además que el número de cancha que intenta ingresar no sea uno existente.", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.cerrarconexion();
        }

        public DataTable LlenarTablaClub()
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }


            string cadena = "SELECT id_club, nombre as 'Nombre Club', escudo FROM club WHERE id_club>192";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_club");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_club"];

        }
     
        public DataTable LlenarTablaClubFiltrado(string nombre)
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }


            string cadena = "SELECT id_club, nombre as 'Nombre Club', escudo FROM club WHERE id_club>192 and nombre like '%" + nombre + "%'";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_club");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_club"];

        }

        public DataTable LlenarTablaZona()
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }


            string cadena = "SELECT id as ID, categoria as Categoria, id_zona as Zona FROM zona WHERE id_zona <> 'Z' ORDER BY categoria ASC";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_club");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_club"];

        }

        public DataTable LlenarTablaZonaFiltrado(string categoria)
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }


            string cadena = "SELECT id_zona as Zona FROM zona WHERE id_zona <> 'Z' and categoria = '" + categoria + "' ORDER BY categoria ASC";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_club");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_club"];

        }

        public void actualizarClub(string id, string nombre, string foto)
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }

            string cadena = "UPDATE club SET nombre='"+nombre+"', escudo='"+foto+"' WHERE id_club="+id;
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Club modificado correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception e) { MessageBox.Show(e.Message); }

            cn.cerrarconexion();
        }

        public void actualizarCategoria(string id1, string id2)
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message);}

            string cadena = "UPDATE categoria SET anio='" + id1 + "' WHERE anio=" + id2;
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Categoría modificada correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e) { MessageBox.Show(e.Message); }

            cn.cerrarconexion();
        }
    }
}
