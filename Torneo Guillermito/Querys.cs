using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Policy;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Utilities.Collections;


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

        public DataTable LlenarTablaEncuentro()
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }

            string cadena = "SELECT en.id_partido, c1.nombre as 'nombre1', en.goles1, en.goles2,  c2.nombre  as 'nombre2', eq1.categoria, z.id_zona, z2.id_zona as id_zona2, DATE_FORMAT(en.fecha, '%Y-%m-%d') as fecha, TIME_FORMAT(en.hora, '%H:%i') as hora, en.cancha FROM partido as en" +
                " JOIN equipo as eq1 ON en.equipo1 = eq1.id_equipo" +
                " JOIN equipo as eq2 ON en.equipo2 = eq2.id_equipo" +
                " JOIN club as c1 ON eq1.club = c1.id_club" +
                " JOIN club as c2 ON eq2.club = c2.id_club" +
                " JOIN zona as z ON eq1.zona = z.id" +
                " JOIN zona as z2 ON eq2.zona = z2.id" +
                " WHERE z.id_zona<> 'Z' and z2.id_zona<> 'Z' and en.tipo = 0 and en.goles1 is null" +
                " ORDER BY id_zona, hora;";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_canchas");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_canchas"];

        }

        public DataTable LlenarTablaEncuentroFiltrado(bool todos, string categoria, string zona, string fecha)
        {
            string all = "";
            if (todos)
            {
                all = " and en.goles1 is null";
            }
            if (!string.IsNullOrEmpty(categoria))
            {
                categoria = " and eq1.categoria = '" + categoria + "'";
            }
            if (!string.IsNullOrEmpty(zona) && zona != "TODAS")
            {
                zona = " and (z.id_zona = '" + zona + "' or z2.id_zona = '" + zona + "')";
            }
            else
            {
                zona = "";
            }
            if (!string.IsNullOrEmpty(fecha))
            {
                fecha = " and en.fecha = '" + fecha + "'";
            }
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }

            string cadena = "SELECT en.id_partido, c1.nombre as 'nombre1', en.goles1, en.goles2,  c2.nombre  as 'nombre2', eq1.categoria, z.id_zona, z2.id_zona as id_zona2, DATE_FORMAT(en.fecha, '%Y-%m-%d') as fecha, TIME_FORMAT(en.hora, '%H:%i') as hora, en.cancha FROM partido as en" +
                " JOIN equipo as eq1 ON en.equipo1 = eq1.id_equipo" +
                " JOIN equipo as eq2 ON en.equipo2 = eq2.id_equipo" +
                " JOIN club as c1 ON eq1.club = c1.id_club" +
                " JOIN club as c2 ON eq2.club = c2.id_club" +
                " JOIN zona as z ON eq1.zona = z.id" +
                " JOIN zona as z2 ON eq2.zona = z2.id" +
                " WHERE en.tipo = 0 and z.id_zona<> 'Z' and z2.id_zona<> 'Z'" + all + categoria + zona + fecha +
                " ORDER BY id_zona, hora;";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_canchas");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_canchas"];

        }

        public DataTable LlenarTablaFechas()
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }

            string cadena = "SELECT DATE_FORMAT(fecha, '%Y-%m-%d') FROM fecha;";
            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_canchas");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_canchas"];

        }

        public DataTable LlenarTablaEquipoFiltradoCategoriaZona(string a, string b)
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }

            string cadena = "SELECT e.id_equipo as ID, c.nombre as Nombre FROM equipo as e INNER JOIN club as c ON e.club = c.id_club INNER JOIN zona as z ON e.zona = z.id WHERE z.id_zona LIKE '" + b + "' and z.id_zona <> 'Z' and e.categoria = '" + a + "'";
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
            catch (Exception e)
            {
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
                MessageBox.Show("Error al insertar la categoría, verifique que los datos esten completos y correctos. La categoria no debe contener mas de 5 caracteres (inlcuyendo espacios)", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.cerrarconexion();
        }

        public void EliminarCancha(String codigo)
        {

            String cadena = "DELETE FROM cancha where id_cancha =" + codigo;
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Cancha eliminada correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e) { MessageBox.Show("Se ha producido un error al intentar eliminar la cancha seleccionada, este error se puede deber a que existen partidos asociados a la misma.", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            cn.cerrarconexion();
        }

        public void EliminarZona(String codigo)
        {

            String cadena = "DELETE FROM zona where id =" + codigo;
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Zona eliminada correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public void EliminarEncuentro(String codigo)
        {

            String cadena = "DELETE FROM partido where id_partido =" + codigo;
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Encuentro eliminado correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e) { MessageBox.Show(e.Message); }

            cn.cerrarconexion();
        }

        public void EliminarCategoria(String codigo)
        {

            String cadena = "DELETE FROM categoria where anio = '" + codigo + "'";
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Categoría eliminada correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e) { MessageBox.Show(e.Message); }

            cn.cerrarconexion();
        }

        public void FletearPlantilla(String codigo)
        {

            String cadena = "DELETE p FROM partido as p " +
                    "INNER JOIN equipo as eq ON p.equipo1 = eq.id_equipo WHERE eq.categoria = '" + codigo + "' and p.tipo <> 0";
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Se ha eliminado correctamente la plantilla anterior", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            catch (Exception e) { MessageBox.Show("Se ha producido un error al intentar eliminar el equipo, este error se puede deber a que existen partidos asociados a él.", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error); }

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
                MessageBox.Show("Zona '" + categoria + " - " + zona + "' insertada correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al insertar la zona, verifique que los datos esten completos y correctos.", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.cerrarconexion();
        }

        public void InsertarEquipo(string club, string categoria, string zona)
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            string cadena = "SELECT id FROM zona WHERE id_zona = '" + zona + "' and categoria = '" + categoria + "'";

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
                MessageBox.Show("Equipo insertado correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al insertar el equipo, verifique que los datos esten completos y correctos.", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.cerrarconexion();
        }

        public void InsertarEncuentro(string equipo1, string equipo2, string hora, string cancha, string fecha)
        {
            String cadena = "INSERT INTO partido (equipo1, equipo2, fecha, hora, cancha, tipo) VALUES (" + equipo1 + ", " + equipo2 + ", '" + fecha + "', '" + hora + "', '" + cancha + "', 0)";
            //MessageBox.Show(cadena);
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Encuentro insertado correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al insertar el Encuentro, verifique que los datos esten completos y correctos.", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public DataTable ConsultarPlantillaCategoria(string parametro)
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }


            string cadena = "SELECT eq.id_equipo, c.nombre FROM equipo as eq " +
                            "JOIN club as c on eq.club = c.id_club WHERE eq.categoria = '" + parametro + "'";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_club");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_club"];

        }

        public void InsertarEncuentroPlantilla(string equipo1, string equipo2, string hora, string cancha, string tipo)
        {
            String cadena = "INSERT INTO partido (equipo1, equipo2, fecha, hora, cancha, tipo) VALUES (" + equipo1 + ", " + equipo2 + ", '2023-11-05', '" + hora + "', '" + cancha + "', '" + tipo + "')";

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                //MessageBox.Show("Encuentro insertado correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al insertar el Encuentro, verifique que los datos esten completos y correctos.", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.cerrarconexion();
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

        public DataTable ConsultarCruces(string categoria)
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }


            string cadena2 = "SELECT en.id_partido, eq1.id_equipo as id1eq, eq2.id_equipo as id2eq,  c1.nombre, c2.nombre as 'nombre2', en.goles1, en.goles2, en.penales1, en.penales2, DATE_FORMAT(en.fecha, '%Y-%m-%d') as fecha, TIME_FORMAT(en.hora, '%H:%i') as hora, en.cancha, en.tipo, c1.escudo as esc1, c2.escudo as esc2 FROM partido as en " +
                "JOIN equipo as eq1 ON en.equipo1 = eq1.id_equipo " +
                "JOIN equipo as eq2 ON en.equipo2 = eq2.id_equipo " +
                "JOIN club as c1 ON eq1.club = c1.id_club " +
                "JOIN club as c2 ON eq2.club = c2.id_club " +
                "JOIN zona as z ON eq1.zona = z.id " +
                "WHERE eq1.categoria = '" + categoria + "' and en.tipo <> 0 " +
                "ORDER BY en.tipo ASC";

            MySqlCommand comando = new MySqlCommand(cadena2, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_canchas");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_canchas"];

        }

        public DataTable ConsultarEncuentrosPosiciones(string categoria)
        {


            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }

            string cadena = "SELECT eq1.id_equipo as ideq1,  eq2.id_equipo as ideq2, c1.nombre, c2.nombre as 'nombre2', en.goles1, en.goles2, en.tipo, c1.escudo as escudo1, c2.escudo as escudo2 FROM partido as en" +
                " JOIN equipo as eq1 ON en.equipo1 = eq1.id_equipo" +
                " JOIN equipo as eq2 ON en.equipo2 = eq2.id_equipo" +
                " JOIN club as c1 ON eq1.club = c1.id_club" +
                " JOIN club as c2 ON eq2.club = c2.id_club" +
                " JOIN zona as z ON eq1.zona = z.id" +
                " JOIN zona as z2 ON eq2.zona = z2.id" +
                " WHERE eq1.categoria = '" + categoria + "' and en.tipo = 0 and en.goles1 is not null" +
                " ORDER BY fecha, hora;";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_canchas");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_canchas"];

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

            string cadena = "UPDATE club SET nombre='" + nombre + "', escudo='" + foto + "' WHERE id_club=" + id;
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Club modificado correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e) { MessageBox.Show(e.Message); }

            cn.cerrarconexion();
        }

        public void actualizarEncuentro(string id, string gol1, string gol2, string fecha, string hora, string cancha)
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            if (gol1 == "")
            {
                gol1 = "null";
                gol2 = "null";
            }
            string cadena = "UPDATE partido SET goles1 = " + gol1 + ",goles2 = " + gol2 + ", hora = '" + hora + "' ,cancha = " + cancha + ", fecha = '" + fecha + "' WHERE id_partido=" + id;
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Encuentro modificado correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e) { MessageBox.Show(e.Message); }

            cn.cerrarconexion();
        }

        public void rellenarCruce(string equipo, string categoria, string posicion)
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            string cadena = "UPDATE partido as p INNER JOIN equipo as eq ON p.equipo1 = eq.id_equipo INNER JOIN club as c ON eq.club = c.id_club SET p.equipo1 = " + equipo + " WHERE p.tipo <> 0 and eq.categoria='" + categoria + "' and c.nombre = '" + posicion + "º TABLA GENERAL'";
            //MessageBox.Show(cadena);
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                //MessageBox.Show("Encuentro modificado correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e) { MessageBox.Show(e.Message); }

            cadena = "UPDATE partido as p INNER JOIN equipo as eq ON p.equipo2 = eq.id_equipo INNER JOIN club as c ON eq.club = c.id_club SET p.equipo2 = " + equipo + " WHERE p.tipo <> 0 and eq.categoria='" + categoria + "' and c.nombre = '" + posicion + "º TABLA GENERAL'";
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();

            }
            catch (Exception e) { MessageBox.Show(e.Message); }

            cn.cerrarconexion();
        }

        public void actualizarCategoria(string id1, string id2)
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }

            string cadena = "UPDATE categoria SET anio='" + id1 + "' WHERE anio='" + id2 + "'";
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Categoría modificada correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e) { MessageBox.Show(e.Message); }

            cn.cerrarconexion();
        }

        public void ModificarYRellenarCruce(string idPartido, string tipo, string hora, string fecha, string cancha, string gl, string gv, string pl, string pv, string idLocal, string idVisita, string categoria)
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }

            string plm;
            string pvm;
            if (gl == "")
            {
                gl = "null";
                gv = "null";
            }

            if (pl == "")
            {
                plm = "null";
                pvm = "null";
                pl = "0";
                pv = "0";
            }
            else
            {
                plm = pl;
                pvm = pv;
            }

            //MODIFICA EL ENCUENTRO SELECCIONADO
            string cadena1 = "UPDATE partido SET hora = '" + hora + "', fecha = '" + fecha + "', cancha = '" + cancha + "'" +
                             ", goles1 = " + gl + ", goles2 = " + gv + ", penales1 = " + plm + ", penales2 = " + pvm + " WHERE id_partido = '" + idPartido + "'";

            string cadena2 = "";
            string cadena3 = "";
            string cadena4 = "";
            string cadena5 = "";
            if (gl != "null")
            {
                if (Int32.Parse(gl) > Int32.Parse(gv) || (Int32.Parse(gl) == Int32.Parse(gv) && Int32.Parse(pl) > Int32.Parse(pv)))
                {


                    cadena2 = "UPDATE partido AS p " +
                        "INNER JOIN equipo AS e ON e.id_equipo = p.equipo1 " +
                        "INNER JOIN club AS c ON e.club = c.id_club " +
                        "SET p.equipo1 = '" + idLocal + "' WHERE c.nombre = 'GANADOR " + tipo + "' and e.categoria = '" + categoria + "'";

                    cadena3 = "UPDATE partido AS p " +
                        "INNER JOIN equipo AS e ON e.id_equipo = p.equipo2 " +
                        "INNER JOIN club AS c ON e.club = c.id_club " +
                        "SET p.equipo2 = '" + idLocal + "' WHERE c.nombre = 'GANADOR " + tipo + "' and e.categoria = '" + categoria + "'";

                    cadena4 = "UPDATE partido AS p " +
                        "INNER JOIN equipo AS e ON e.id_equipo = p.equipo1 " +
                        "INNER JOIN club AS c ON e.club = c.id_club " +
                        "SET p.equipo1 = '" + idVisita + "' WHERE c.nombre = 'PERDEDOR " + tipo + "' and e.categoria = '" + categoria + "'";

                    cadena5 = "UPDATE partido AS p " +
                        "INNER JOIN equipo AS e ON e.id_equipo = p.equipo2 " +
                        "INNER JOIN club AS c ON e.club = c.id_club " +
                        "SET p.equipo2 = '" + idVisita + "' WHERE c.nombre = 'PERDEDOR " + tipo + "' and e.categoria = '" + categoria + "'";
                }
                else
                {
                    cadena2 = "UPDATE partido AS p " +
                        "INNER JOIN equipo AS e ON e.id_equipo = p.equipo1 " +
                        "INNER JOIN club AS c ON e.club = c.id_club " +
                        "SET p.equipo1 = '" + idVisita + "' WHERE c.nombre = 'GANADOR " + tipo + "' and e.categoria = '" + categoria + "'";

                    cadena3 = "UPDATE partido AS p " +
                        "INNER JOIN equipo AS e ON e.id_equipo = p.equipo2 " +
                        "INNER JOIN club AS c ON e.club = c.id_club " +
                        "SET p.equipo2 = '" + idVisita + "' WHERE c.nombre = 'GANADOR " + tipo + "' and e.categoria = '" + categoria + "'";

                    cadena4 = "UPDATE partido AS p " +
                        "INNER JOIN equipo AS e ON e.id_equipo = p.equipo1 " +
                        "INNER JOIN club AS c ON e.club = c.id_club " +
                        "SET p.equipo1 = '" + idLocal + "' WHERE c.nombre = 'PERDEDOR " + tipo + "' and e.categoria = '" + categoria + "'";

                    cadena5 = "UPDATE partido AS p " +
                        "INNER JOIN equipo AS e ON e.id_equipo = p.equipo2 " +
                        "INNER JOIN club AS c ON e.club = c.id_club " +
                        "SET p.equipo2 = '" + idLocal + "' WHERE c.nombre = 'PERDEDOR " + tipo + "' and e.categoria = '" + categoria + "'";
                }
            }
            try
            {
                //MessageBox.Show(cadena2);
                MySqlCommand command = new MySqlCommand(cadena1, cn.conectarbd);
                command.ExecuteNonQuery();
                if (gl != "null")
                {
                    command = new MySqlCommand(cadena2, cn.conectarbd);
                    command.ExecuteNonQuery();
                    command = new MySqlCommand(cadena3, cn.conectarbd);
                    command.ExecuteNonQuery();
                    command = new MySqlCommand(cadena4, cn.conectarbd);
                    command.ExecuteNonQuery();
                    command = new MySqlCommand(cadena5, cn.conectarbd);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Se ha modificado el cruce exitosamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e) { MessageBox.Show(e.Message); }

            cn.cerrarconexion();

        }
    }
}
