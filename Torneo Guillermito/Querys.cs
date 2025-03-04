﻿using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Policy;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Utilities.Collections;


namespace LIPa
{
    internal class Querys
    {
        conexion cn = new conexion();

        public DataTable LlenarTablaCanchas()
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }


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

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }


            string cadena = "SELECT ID, nombre as DIVISION FROM division";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_canchas");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_canchas"];

        }

        public DataTable LlenarTablaJugador()
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            string cadena = "SELECT j.ID, j.nombreCompleto, j.dorsal, c.nombrecompleto, j.foto FROM jugador as j " +
                "INNER JOIN equipo as e ON e.ID = j.equipoID " +
                "INNER JOIN club as c ON e.club = c.id " +
                "INNER JOIN zona as z ON e.zona = z.nombre ";
            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_canchas");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_canchas"];

        }

        public DataTable LlenarTablaEquipo()
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            string cadena = "SELECT e.ID, c.nombrecompleto, d.nombre, z.nombre as Zona FROM equipo as e " +
                "INNER JOIN club as c ON e.club = c.id " +
                "INNER JOIN zona as z ON e.zona = z.nombre " +
                "INNER JOIN division as d ON d.ID = e.division;";
            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_canchas");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_canchas"];

        }

        public DataTable LlenarTablaEncuentro(string filtro)
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            string cadena = "SELECT en.ID, c1.nombrecompleto as 'nombre1', en.golesLocal, en.golesVisita,  c2.nombrecompleto  as 'nombre2', d.nombre, fl.numero, " +
                "DATE_FORMAT(en.fecha, '%Y-%m-%d') as dia, TIME_FORMAT(en.hora, '%H:%i') as hora, en.url_transmicion, en.url_imagenes, en.terminado FROM partido as en" +
                " JOIN equipo as eq1 ON en.equipoLocal = eq1.ID" +
                " JOIN equipo as eq2 ON en.equipoVisita = eq2.ID" +
                " JOIN club as c1 ON eq1.club = c1.ID" +
                " JOIN club as c2 ON eq2.club = c2.ID " +
                " JOIN fecha as fl ON fl.ID = en.fechaLiga " +
                " JOIN division as d ON d.ID = eq1.division " +
                filtro + " and en.golesLocal is null" +
                " and eq1.division=1 and en.fechaLiga = '-1'" +
                " ORDER BY en.fecha, en.hora;";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_canchas");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_canchas"];

        }

        public DataTable LlenarTablaEncuentroFiltrado(bool todos, string categoria, string zona, string fecha, string filtro)
        {
            filtro = " " + filtro;
            string all = "";
            if (todos)
            {
                all = " and en.golesLocal is null ";
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
                fecha = " and en.fechaLiga = (SELECT ID from fecha where numero= '" + fecha + "' and division=1) ";
            }
            else
            {
                fecha = " and en.fechaLiga = -1";
            }
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            string cadena = "SELECT en.ID, c1.nombrecompleto as 'nombre1', en.golesLocal, en.golesVisita,  c2.nombrecompleto  as 'nombre2', d.nombre, fl.numero, " +
                "DATE_FORMAT(en.fecha, '%Y-%m-%d') as dia, TIME_FORMAT(en.hora, '%H:%i') as hora, en.url_transmicion, en.url_imagenes, en.terminado from partido as en" +
                " JOIN equipo as eq1 ON en.equipoLocal = eq1.ID" +
                " JOIN equipo as eq2 ON en.equipoVisita = eq2.ID" +
                " JOIN club as c1 ON eq1.club = c1.ID" +
                " JOIN club as c2 ON eq2.club = c2.ID " +
                " JOIN fecha as fl ON fl.ID = en.fechaLiga " +
                " JOIN division as d ON d.ID = eq1.division " +
                filtro + all +/* categoria + zona + */ fecha +
                " and eq1.division=1 " +
                " ORDER BY en.fecha, en.hora;";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_canchas");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_canchas"];

        }

        public DataTable LlenarTablaJugadores(string id_partido, string value)
        {
            string argument = "";
            if (value == "local") { argument = "(SELECT equipoLocal FROM partido where ID = '" + id_partido + "')"; }
            else if (value == "visit") { argument = "(SELECT equipoVisita FROM partido where ID = " + id_partido + ")";  }
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            string cadena = "SELECT j.ID, j.nombreCompleto, j.dorsal FROM jugador as j" +
                " WHERE equipoID = " + argument + " ORDER BY j.dorsal";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_canchas");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_canchas"];

        }

        public DataTable LlenarTablaStats(string id_partido, string value)
        {
            string cadena = "";
            switch(value)
            {
                case "add-goal-local":
                    cadena = "SELECT g.ID, j.ID, j.nombreCompleto, j.dorsal FROM gol as g INNER JOIN jugador as j ON g.jugadorID = j.ID WHERE g.partido = '" +
                    id_partido + "' and g.lado = 'L'";
                    break;
                case "add-goal-visit":
                    cadena = "SELECT g.ID, j.ID, j.nombreCompleto, j.dorsal FROM gol as g INNER JOIN jugador as j ON g.jugadorID = j.ID WHERE g.partido = '" +
                    id_partido + "' and g.lado = 'V'";
                    break;
                case "rest-goal-local":
                    cadena = "SELECT g.ID, j.ID, j.nombreCompleto, j.dorsal FROM gol as g INNER JOIN jugador as j ON g.jugadorID = j.ID WHERE g.partido = '" +
                    id_partido + "' and g.lado = 'L'";
                    break;
                case "rest-goal-visit":
                    cadena = "SELECT g.ID, j.ID, j.nombreCompleto, j.dorsal FROM gol as g INNER JOIN jugador as j ON g.jugadorID = j.ID WHERE g.partido = '" +
                    id_partido + "' and g.lado = 'V'";
                    break;
                case "add-yc-local":
                    cadena = "SELECT t.ID, j.ID, j.nombreCompleto, j.dorsal FROM tarjeta as t INNER JOIN jugador as j ON t.jugadorID = j.ID WHERE t.partido = '" +
                    id_partido + "' and t.lado = 'L' and t.tipo = 'Y'";
                    break;
                case "add-yc-visit":
                    cadena = "SELECT t.ID, j.ID, j.nombreCompleto, j.dorsal FROM tarjeta as t INNER JOIN jugador as j ON t.jugadorID = j.ID WHERE t.partido = '" +
                    id_partido + "' and t.lado = 'V' and t.tipo = 'Y'";
                    break;
                case "rest-yc-local":
                    cadena = "SELECT t.ID, j.ID, j.nombreCompleto, j.dorsal FROM tarjeta as t INNER JOIN jugador as j ON t.jugadorID = j.ID WHERE t.partido = '" +
                    id_partido + "' and t.lado = 'L' and t.tipo = 'Y'";
                    break;
                case "rest-yc-visit":
                    cadena = "SELECT t.ID, j.ID, j.nombreCompleto, j.dorsal FROM tarjeta as t INNER JOIN jugador as j ON t.jugadorID = j.ID WHERE t.partido = '" +
                    id_partido + "' and t.lado = 'V' and t.tipo = 'Y'";
                    break;
                case "add-rc-local":
                    cadena = "SELECT t.ID, j.ID, j.nombreCompleto, j.dorsal FROM tarjeta as t INNER JOIN jugador as j ON t.jugadorID = j.ID WHERE t.partido = '" +
                    id_partido + "' and t.lado = 'L' and t.tipo = 'R'";
                    break;
                case "add-rc-visit":
                    cadena = "SELECT t.ID, j.ID, j.nombreCompleto, j.dorsal FROM tarjeta as t INNER JOIN jugador as j ON t.jugadorID = j.ID WHERE t.partido = '" +
                    id_partido + "' and t.lado = 'V' and t.tipo = 'R'";
                    break;
                case "rest-rc-local":
                    cadena = "SELECT t.ID, j.ID, j.nombreCompleto, j.dorsal FROM tarjeta as t INNER JOIN jugador as j ON t.jugadorID = j.ID WHERE t.partido = '" +
                    id_partido + "' and t.lado = 'L' and t.tipo = 'R'";
                    break;
                case "rest-rc-visit":
                    cadena = "SELECT t.ID, j.ID, j.nombreCompleto, j.dorsal FROM tarjeta as t INNER JOIN jugador as j ON t.jugadorID = j.ID WHERE t.partido = '" +
                    id_partido + "' and t.lado = 'V' and t.tipo = 'R'";
                    break;
            }
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_canchas");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_canchas"];

        }

        public DataTable LlenarTablaFechas()
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            string cadena = "SELECT numero FROM fecha where division=1;";
            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_canchas");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_canchas"];

        }

        public DataTable LlenarTablaEquipoFiltradoCategoriaZona(string a, string b)
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            string cadena = "SELECT e.ID as ID, c.nombrecompleto as Nombre FROM equipo as e INNER JOIN club as c ON e.club = c.ID WHERE e.zona = '" + b + "' and e.division = " +
                " (SELECT id FROM division where nombre = '" + a + "');";
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

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Cancha '" + numero + "' insertado correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                MessageBox.Show("Error al insertar la cancha, verifique que los datos esten completos y correctos. Asegúrese además que el número de cancha que intenta ingresar no sea uno existente.", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.cerrarconexion();
        }

        public void cambiarEstadoPartido(bool estado, string id_partido, int gl, int gv)
        {
            string cadena1 = "";
            string cadena2 = "";
            string cadena3 = "";

            cadena1 = "UPDATE partido SET terminado = " + estado + " where ID ='" + id_partido + "';";

            if (estado)
            {
               if (gl > gv)
               {
                    cadena2 = "UPDATE posicion SET partidosGanados = partidosGanados+1 where id_equipo = " +
                        "(SELECT equipoLocal FROM partido where ID='" + id_partido + "');";
                    cadena3 = "UPDATE posicion SET partidosPerdidos = partidosPerdidos+1 where id_equipo = " +
                        "(SELECT equipoVisita FROM partido where ID='" + id_partido + "');";
               }
               else if (gl == gv)
               {
                    cadena2 = "UPDATE posicion SET partidosEmpatados = partidosEmpatados+1 where id_equipo = " +
                                            "(SELECT equipoLocal FROM partido where ID='" + id_partido + "');";
                    cadena3 = "UPDATE posicion SET partidosEmpatados = partidosEmpatados+1 where id_equipo = " +
                        "(SELECT equipoVisita FROM partido where ID='" + id_partido + "');";
               }
               else if (gl < gv)
               {
                    cadena2 = "UPDATE posicion SET partidosPerdidos = partidosPerdidos+1 where id_equipo = " +
                                           "(SELECT equipoLocal FROM partido where ID='" + id_partido + "');";
                    cadena3 = "UPDATE posicion SET partidosGanados = partidosGanados+1 where id_equipo = " +
                        "(SELECT equipoVisita FROM partido where ID='" + id_partido + "');";
               }
            }
            else
            {
                if (gl > gv)
                {
                    cadena2 = "UPDATE posicion SET partidosGanados = partidosGanados-1 where id_equipo = " +
                        "(SELECT equipoLocal FROM partido where ID='" + id_partido + "');";
                    cadena3 = "UPDATE posicion SET partidosPerdidos = partidosPerdidos-1 where id_equipo = " +
                        "(SELECT equipoVisita FROM partido where ID='" + id_partido + "');";
                }
                else if (gl == gv)
                {
                    cadena2 = "UPDATE posicion SET partidosEmpatados = partidosEmpatados-1 where id_equipo = " +
                                            "(SELECT equipoLocal FROM partido where ID='" + id_partido + "');";
                    cadena3 = "UPDATE posicion SET partidosEmpatados = partidosEmpatados-1 where id_equipo = " +
                        "(SELECT equipoVisita FROM partido where ID='" + id_partido + "');";
                }
                else if (gl < gv)
                {
                    cadena2 = "UPDATE posicion SET partidosPerdidos = partidosPerdidos-1 where id_equipo = " +
                                           "(SELECT equipoLocal FROM partido where ID='" + id_partido + "');";
                    cadena3 = "UPDATE posicion SET partidosGanados = partidosGanados-1 where id_equipo = " +
                        "(SELECT equipoVisita FROM partido where ID='" + id_partido + "');";
                }
            }

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            try
            {
                MySqlCommand command1 = new MySqlCommand(cadena1, cn.conectarbd);
                MySqlCommand command2 = new MySqlCommand(cadena2, cn.conectarbd);
                MySqlCommand command3 = new MySqlCommand(cadena3, cn.conectarbd);
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                command3.ExecuteNonQuery();
                
                MessageBox.Show("Encuentro modificado correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al modificar el encuentro, verifique que los datos esten completos y correctos: "+ e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.cerrarconexion();

        }

        public void InsertarAccion(string id_partido, string id_jugador, string value)
        {
            string cadena1 = "";
            string cadena2 = "";
            string cadena3 = "";
            string cadena4 = "";
            string cadena5 = "";
            switch (value)
            {
                case "add-goal-local":
                    cadena1 = "INSERT INTO gol(partido, lado, jugadorID) Values ('" + id_partido + "', 'L', '" + id_jugador + "');";
                    cadena2 = "UPDATE jugador set goles = goles + 1 where ID = '" + id_jugador + "'";
                    cadena3 = "UPDATE partido set golesLocal = golesLocal + 1 where ID = '" + id_partido + "'";
                    cadena4 = "UPDATE posicion set golesFavor = golesFavor+1 where id_equipo=(SELECT equipoLocal from partido where ID='"+id_partido+"')";
                    cadena5 = "UPDATE posicion set golesContra = golesContra+1 where id_equipo=(SELECT equipoVisita from partido where ID='" + id_partido + "')";
                    break;
                case "add-goal-visit":
                    cadena1 = "INSERT INTO gol(partido, lado, jugadorID) Values ('" + id_partido + "', 'V', '" + id_jugador + "');";
                    cadena2 = "UPDATE jugador set goles = goles + 1 where ID = '" + id_jugador + "'";
                    cadena3 = "UPDATE partido set golesVisita = golesVisita + 1 where ID = '" + id_partido + "'";
                    cadena4 = "UPDATE posicion set golesFavor = golesFavor+1 where id_equipo=(SELECT equipoVisita from partido where ID='" + id_partido + "')";
                    cadena5 = "UPDATE posicion set golesContra = golesContra+1 where id_equipo=(SELECT equipoLocal from partido where ID='" + id_partido + "')";
                    break;
                case "add-yc-local":
                    cadena1 = "INSERT INTO tarjeta(partido, lado, tipo, jugadorID) Values ('" + id_partido + "', 'L', 'Y', '" + id_jugador + "');";
                    cadena2 = "UPDATE jugador set tarjetasAmarillas = tarjetasAmarillas + 1 where ID = '" + id_jugador + "'";
                    break;
                case "add-yc-visit":
                    cadena1 = "INSERT INTO tarjeta(partido, lado, tipo, jugadorID) Values ('" + id_partido + "', 'V', 'Y', '" + id_jugador + "');";
                    cadena2 = "UPDATE jugador set tarjetasAmarillas = tarjetasAmarillas + 1 where ID = '" + id_jugador + "'";
                    break;
                case "add-rc-local":
                    cadena1 = "INSERT INTO tarjeta(partido, lado, tipo, jugadorID) Values ('" + id_partido + "', 'L', 'R', '" + id_jugador + "');";
                    cadena2 = "UPDATE jugador set tarjetasRojas = tarjetasRojas + 1 where ID = '" + id_jugador + "'";
                    break;
                case "add-rc-visit":
                    cadena1 = "INSERT INTO tarjeta(partido, lado, tipo, jugadorID) Values ('" + id_partido + "', 'V', 'R', '" + id_jugador + "');";
                    cadena2 = "UPDATE jugador set tarjetasRojas = tarjetasRojas + 1 where ID = '" + id_jugador + "'";
                    break;
            }
            Console.WriteLine(cadena1);
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            try
            {
                MySqlCommand command1 = new MySqlCommand(cadena1, cn.conectarbd);
                command1.ExecuteNonQuery();
                MySqlCommand command2 = new MySqlCommand(cadena2, cn.conectarbd);
                command2.ExecuteNonQuery();
                
                if (cadena3 != "")
                {
                    MySqlCommand command3 = new MySqlCommand(cadena3, cn.conectarbd);
                    command3.ExecuteNonQuery();
                }
                if (cadena4 != "")
                {
                    MySqlCommand command4 = new MySqlCommand(cadena4, cn.conectarbd);
                    command4.ExecuteNonQuery();
                }
                if (cadena5 != "")
                {
                    MySqlCommand command5 = new MySqlCommand(cadena5, cn.conectarbd);
                    command5.ExecuteNonQuery();
                }

                MessageBox.Show("Acción insertada correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                MessageBox.Show("Error al insertar la acción: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.cerrarconexion();
        }

        public void InsertarCategoria(string numero)
        {
            String cadena = "INSERT INTO division (nombre, torneo, duracionPartido, orden, tablaGeneral) Values ('" + numero + "', 1, 45, 1, 0);";

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Categoría '" + numero + "' insertada correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                MessageBox.Show("Error al insertar la categoría, verifique que los datos esten completos y correctos. La categoria no debe contener mas de 5 caracteres (inlcuyendo espacios)", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.cerrarconexion();
        }

        public void EliminarCancha(String codigo)
        {

            String cadena = "DELETE FROM cancha where id_cancha =" + codigo;
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Cancha eliminada correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e) { MessageBox.Show("Se ha producido un error al intentar eliminar la cancha seleccionada, este error se puede deber a que existen partidos asociados a la misma.", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            cn.cerrarconexion();
        }


        public void EliminarAccion(string id_partido, string id_accion, string id_jugador, string value)
        {
            string cadena1 = "";
            string cadena2 = "";
            string cadena3 = "";
            string cadena4 = "";
            string cadena5 = "";
            switch (value)
            {
                case "add-goal-local":
                    cadena1 = "DELETE FROM gol WHERE ID = '"+id_accion+"';";
                    cadena2 = "UPDATE jugador SET goles=goles-1 WHERE ID = '" + id_jugador + "'";
                    cadena3 = "UPDATE partido set golesLocal=golesLocal-1 where ID = '" + id_partido + "'";
                    cadena4 = "UPDATE posicion set golesFavor = golesFavor-1 where id_equipo=(SELECT equipoLocal from partido where ID='" + id_partido + "')";
                    cadena5 = "UPDATE posicion set golesContra = golesContra-1 where id_equipo=(SELECT equipoVisita from partido where ID='" + id_partido + "')";
                    break;
                case "add-goal-visit":
                    cadena1 = "DELETE FROM gol WHERE ID = '" + id_accion + "';";
                    cadena2 = "UPDATE jugador SET goles=goles-1 WHERE ID = '" + id_jugador + "'";
                    cadena3 = "UPDATE partido set golesVisita=golesVisita-1 where ID = '" + id_partido + "'";
                    cadena4 = "UPDATE posicion set golesFavor = golesFavor-1 where id_equipo=(SELECT equipoVisita from partido where ID='" + id_partido + "')";
                    cadena5 = "UPDATE posicion set golesContra = golesContra-1 where id_equipo=(SELECT equipoLocal from partido where ID='" + id_partido + "')";
                    break;
                case "add-yc-local":
                    cadena1 = "DELETE FROM tarjeta WHERE ID = '" + id_accion + "';";
                    cadena2 = "UPDATE jugador SET tarjetasAmarillas=tarjetasamarillas-1 WHERE ID = '" + id_jugador + "'";
                    break;
                case "add-yc-visit":
                    cadena1 = "DELETE FROM tarjeta WHERE ID = '" + id_accion + "';";
                    cadena2 = "UPDATE jugador SET tarjetasAmarillas=tarjetasamarillas-1 WHERE ID = '" + id_jugador + "'";
                    break;
                case "add-rc-local":
                    cadena1 = "DELETE FROM tarjeta WHERE ID = '" + id_accion + "';";
                    cadena2 = "UPDATE jugador SET tarjetasRojas=tarjetasRojas-1 WHERE ID = '" + id_jugador + "'";
                    break;
                case "add-rc-visit":
                    cadena1 = "DELETE FROM tarjeta WHERE ID = '" + id_accion + "';";
                    cadena2 = "UPDATE jugador SET tarjetasRojas=tarjetasRojas-1 WHERE ID = '" + id_jugador + "'";
                    break;
                case "rest-goal-local":
                    cadena1 = "DELETE FROM gol WHERE ID = '" + id_accion + "';";
                    cadena2 = "UPDATE jugador SET goles=goles-1 WHERE ID = '" + id_jugador + "'";
                    cadena3 = "UPDATE partido set golesLocal=golesLocal-1 where ID = '" + id_partido + "'";
                    cadena4 = "UPDATE posicion set golesFavor = golesFavor-1 where id_equipo=(SELECT equipoLocal from partido where ID='" + id_partido + "')";
                    cadena5 = "UPDATE posicion set golesContra = golesContra-1 where id_equipo=(SELECT equipoVisita from partido where ID='" + id_partido + "')";
                    break;
                case "rest-goal-visit":
                    cadena1 = "DELETE FROM gol WHERE ID = '" + id_accion + "';";
                    cadena2 = "UPDATE jugador SET goles=goles-1 WHERE ID = '" + id_jugador + "'";
                    cadena3 = "UPDATE partido set golesVisita=golesVisita-1 where ID = '" + id_partido + "'";
                    cadena4 = "UPDATE posicion set golesFavor = golesFavor-1 where id_equipo=(SELECT equipoVisita from partido where ID='" + id_partido + "')";
                    cadena5 = "UPDATE posicion set golesContra = golesContra-1 where id_equipo=(SELECT equipoLocal from partido where ID='" + id_partido + "')";
                    break;
                case "rest-yc-local":
                    cadena1 = "DELETE FROM tarjeta WHERE ID = '" + id_accion + "';";
                    cadena2 = "UPDATE jugador SET tarjetasAmarillas=tarjetasamarillas-1 WHERE ID = '" + id_jugador + "'";
                    break;
                case "rest-yc-visit":
                    cadena1 = "DELETE FROM tarjeta WHERE ID = '" + id_accion + "';";
                    cadena2 = "UPDATE jugador SET tarjetasAmarillas=tarjetasamarillas-1 WHERE ID = '" + id_jugador + "'";
                    break;
                case "rest-rc-local":
                    cadena1 = "DELETE FROM tarjeta WHERE ID = '" + id_accion + "';";
                    cadena2 = "UPDATE jugador SET tarjetasRojas=tarjetasRojas-1 WHERE ID = '" + id_jugador + "'";
                    break;
                case "rest-rc-visit":
                    cadena1 = "DELETE FROM tarjeta WHERE ID = '" + id_accion + "';";
                    cadena2 = "UPDATE jugador SET tarjetasRojas=tarjetasRojas-1 WHERE ID = '" + id_jugador + "'";
                    break;
            }

            Console.WriteLine(cadena1);
            Console.WriteLine(cadena2);
            Console.WriteLine(cadena3);

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show(e.Message); }
            try
            {
                MySqlCommand command1 = new MySqlCommand(cadena1, cn.conectarbd);
                command1.ExecuteNonQuery();
                MySqlCommand command2 = new MySqlCommand(cadena2, cn.conectarbd);
                command2.ExecuteNonQuery();
                if (cadena3 != "") { 
                    MySqlCommand command3 = new MySqlCommand(cadena3, cn.conectarbd);
                    command3.ExecuteNonQuery();
                }
                if (cadena4 != "")
                {
                    MySqlCommand command4 = new MySqlCommand(cadena4, cn.conectarbd);
                    command4.ExecuteNonQuery();
                }
                if (cadena5 != "")
                {
                    MySqlCommand command5 = new MySqlCommand(cadena5, cn.conectarbd);
                    command5.ExecuteNonQuery();
                }
                MessageBox.Show("Acción eliminada correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                catch (Exception e) { MessageBox.Show("Se ha producido un error al intentar eliminar la cancha seleccionada, este error se puede deber a que existen partidos asociados a la misma.", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

               cn.cerrarconexion();
        }

        public void EliminarZona(String codigo)
        {

            String cadena = "DELETE FROM zona where id =" + codigo;
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Zona eliminada correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e) { MessageBox.Show("Error al eliminar una zona: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            cn.cerrarconexion();
        }

        public void EliminarClub(String codigo)
        {

            String cadena = "DELETE FROM club where ID =" + codigo;
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Club eliminado correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e) { MessageBox.Show("Error al eliminar el club: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            cn.cerrarconexion();
        }

        public void EliminarJugador(string ID)
        {
            String cadena1 = "DELETE FROM gol where jugadorID =" + ID;
            String cadena2 = "DELETE FROM tarjeta where jugadorID =" + ID;
            String cadena3 = "DELETE FROM jugador where ID =" + ID;
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            try
            {
                MySqlCommand command1 = new MySqlCommand(cadena1, cn.conectarbd);
                MySqlCommand command2 = new MySqlCommand(cadena2, cn.conectarbd);
                MySqlCommand command3 = new MySqlCommand(cadena3, cn.conectarbd);
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                command3.ExecuteNonQuery();
                MessageBox.Show("Jugador y acciones eliminados correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e) { MessageBox.Show("Error al eliminar el jugador: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            cn.cerrarconexion();
        }

        public void EliminarEncuentro(String codigo)
        {

            String cadena = "DELETE FROM partido where id =" + codigo;
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Encuentro eliminado correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e) { MessageBox.Show("Se ha producido un error al intentar eliminar el encuentro, este error se puede deber a que existen acciones (goles, tarjetas) asociados a él. Eliminalos primero si quiere borrar el partido", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            cn.cerrarconexion();
        }

        public void EliminarCategoria(String codigo)
        {

            String cadena = "DELETE FROM division where ID = '" + codigo + "'";
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Categoría eliminada correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                        catch (Exception e) { MessageBox.Show("Se ha producido un error al intentar eliminar la categoría, este error se puede deber a que existen equipos asociados a él.", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }


            cn.cerrarconexion();
        }

        public void FletearPlantilla(String codigo)
        {

            String cadena = "DELETE p FROM partido as p " +
                    "INNER JOIN equipo as eq ON p.equipo1 = eq.id_equipo WHERE eq.categoria = '" + codigo + "' and p.tipo <> 0";
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Se ha eliminado correctamente la plantilla anterior", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e) { MessageBox.Show(e.Message); }

            cn.cerrarconexion();
        }

        public void EliminarEquipo(String codigo)
        {

            String cadena = "DELETE FROM equipo where ID =" + codigo;
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Equipo eliminado correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e) { MessageBox.Show("Se ha producido un error al intentar eliminar el equipo, este error se puede deber a que existen partidos asociados a él.", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            cn.cerrarconexion();
        }

        public void InsertarClub(string nombrecompleto, string nombrecorto, string foto)
        {
            String cadena = "INSERT INTO club (nombrecompleto, nombrecorto, escudo) Values ('" + nombrecompleto + "', '" + nombrecorto + "', 'ESCUDOS/" + foto + "');";

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Club '" + nombrecompleto + "' insertado correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al insertar el club, verifique que los datos esten completos y correctos. Asegúrese además que el número de cancha que intenta ingresar no sea uno existente." + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.cerrarconexion();
        }

        public void InsertarZona(string division, string zona)
        {
            String cadena = "INSERT INTO zona (division, nombre) Values ((" +
                "SELECT ID from division where nombre= '" + division + "'), '" + zona + "');";

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Zona '" + division + " - " + zona + "' insertada correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al insertar la zona, verifique que los datos esten completos y correctos.", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.cerrarconexion();
        }

        public void InsertarEquipo(string club, string division, string zona)
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            string cadena1 = "INSERT INTO equipo (club, division, zona) Values ('" + club + "', " +
                "(SELECT id from division where nombre = '" + division + "'), '" + zona + "');";
            string cadena2 = "INSERT INTO posicion (id_equipo) SELECT ID from equipo where club='" + club + "' " +
                    "and division=" +
                        "(SELECT id from division where nombre = '" + division + "') " +
                    "and zona='" + zona + "';";
            try
            {
                MySqlCommand command1 = new MySqlCommand(cadena1, cn.conectarbd);
                MySqlCommand command2 = new MySqlCommand(cadena2, cn.conectarbd);
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                MessageBox.Show("Equipo insertado correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al insertar el equipo, verifique que los datos esten completos y correctos.", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.cerrarconexion();
        }

        public void InsertarJugador(string id, string nombre, string dorsal, string id_equipo, string foto)
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            string cadena = "INSERT INTO jugador (ID, nombreCompleto, dorsal, equipoID, foto) Values " +
                "('" + id + "', '" + nombre + "', '" + dorsal + "',  '" + id_equipo + "','jugadores/" + foto + "');";
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Jugador '"+nombre+"' insertado correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al insertar el jugador, verifique que los datos esten completos y correctos.", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.cerrarconexion();
        }

        public void InsertarEncuentro(string equipo1, string equipo2, string dia, string fecha, string hora, string url_transmicion, string url_imagenes)
        {
            string cadena = "";
            if (dia != "")
            {
             cadena = "INSERT INTO partido (equipoLocal, equipoVisita, fecha, fechaLiga, hora, url_transmicion, url_imagenes) VALUES " +
                "(" + equipo1 + ", " + equipo2 + ", '" + dia + "', (SELECT ID from fecha where numero = '" + fecha + "' and division=1), '" + hora + "', '" + url_transmicion + "', '" + url_imagenes + "')";
            }
            else
            {
                cadena = "INSERT INTO partido (equipoLocal, equipoVisita, fechaLiga, hora, url_transmicion, url_imagenes) VALUES " +
                   "(" + equipo1 + ", " + equipo2 + ", (SELECT ID from fecha where numero = '" + fecha + "' and division=1), '" + hora + "', '" + url_transmicion + "', '" + url_imagenes + "')";
            }
            Console.WriteLine(cadena);
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Encuentro insertado correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al insertar el Encuentro, verifique que los datos esten completos y correctos. " + e.Message.ToString(), "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.cerrarconexion();
        }

        public DataTable LlenarTablaClub()
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }


            string cadena = "SELECT ID, nombrecompleto as 'Nombre Club', nombrecorto as 'Nombre Corto', escudo FROM club";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_club");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_club"];

        }

        public DataTable ConsultarPlantillaCategoria(string parametro)
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }


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

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                //MessageBox.Show("Encuentro insertado correctamente", "Torneo Guillermito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al insertar el Encuentro, verifique que los datos esten completos y correctos: "+e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.cerrarconexion();
        }

        public DataTable LlenarTablaClubFiltrado(string nombre)
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }


            string cadena = "SELECT ID, nombrecompleto as 'Nombre Club', escudo FROM club WHERE nombrecompleto like '%" + nombre + "%'";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_club");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_club"];

        }

        public DataTable LlenarTablaEquipoJugadorFiltrado(string nombre)
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }


            string cadena = "SELECT e.ID, c.nombrecompleto as 'Nombre Club', d.nombre FROM club as c " +
                "INNER JOIN equipo as e ON c.ID = e.club " +
                "INNER JOIN division as d ON e.division = d.ID " +
                "WHERE c.nombrecompleto like '%" + nombre + "%'";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_club");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_club"];

        }

        public DataTable LlenarTablaZona()
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }


            string cadena = "SELECT z.id as ID, d.nombre as DIVISION, z.nombre as Zona " +
                "FROM zona as z INNER JOIN division as d " +
                "ON z.division = d.id ORDER BY division ASC, z.nombre ASC";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_club");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_club"];

        }

        public DataTable ConsultarCruces(string categoria)
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }


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


            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

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

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }


            string cadena = "SELECT z.nombre FROM zona as z " +
                            "INNER JOIN division as d ON z.division = d.id WHERE d.nombre = '" + categoria + "' ORDER BY z.nombre ASC";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_club");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_club"];

        }

        public DataTable ObtenerStats(string id)
        {

            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }


            string cadena = "SELECT " +
                "COALESCE(SUM(CASE WHEN lado = 'L' AND tipo = 'Y' THEN 1 ELSE 0 END), 0) as amarillasLocal, " +
                "COALESCE(SUM(CASE WHEN lado = 'V' AND tipo = 'Y' THEN 1 ELSE 0 END), 0) as amarillasVisita, " +
                "COALESCE(SUM(CASE WHEN lado = 'L' AND tipo = 'R' THEN 1 ELSE 0 END), 0) as rojasLocal, " +
                "COALESCE(SUM(CASE WHEN lado = 'V' AND tipo = 'R' THEN 1 ELSE 0 END), 0) as rojasVisita, " +
                "'" + id + "' as partido " +
                "FROM tarjeta WHERE partido = '" + id + "'";

            MySqlCommand comando = new MySqlCommand(cadena, cn.conectarbd);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla_resultados_club");
            cn.cerrarconexion();
            return ds.Tables["tabla_resultados_club"];

        }

        public void actualizarClub(string id, string nombrecompleto, string nombrecorto, string foto)
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            string cadena = "UPDATE club SET nombrecompleto='" + nombrecompleto + "', nombrecorto='" + nombrecorto + "', escudo='" + foto + "' WHERE ID=" + id;
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Club modificado correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e) { MessageBox.Show("Error al modificar el club: "+e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            cn.cerrarconexion();
        }
        public void actualizarJugador(string id, string nombrecompleto, string dorsal, string equipo, string foto)
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            string cadena = "";
            if(equipo == "") { cadena = "UPDATE jugador SET nombreCompleto='" + nombrecompleto + "', dorsal='" + dorsal + "', foto='" + foto + "' WHERE ID=" + id; }
            else { cadena = "UPDATE jugador SET nombreCompleto='" + nombrecompleto + "', dorsal='" + dorsal + "', foto='" + foto + "', equipoID='" + equipo + "' WHERE ID=" + id; }

            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Jugador modificado correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e) { MessageBox.Show("Error al modificar el jugador: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            cn.cerrarconexion();
        }

        public void ComenzarEncuentro(string id)
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            string cadena = "UPDATE partido SET golesLocal = '0', golesVisita = '0' WHERE ID=" + id;
  
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Encuentro modificado correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e) { MessageBox.Show("Error al modificar el encuentro: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            cn.cerrarconexion();
        }

        public void ReiniciarEncuentro(string id)
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            string cadena1 = "UPDATE partido SET golesLocal = null, golesVisita = null WHERE ID=" + id;
            string cadena2 = "DELETE FROM gol WHERE partido=" + id;
            string cadena3 = "DELETE FROM tarjeta WHERE partido=" + id;
            try
            {
                MySqlCommand command1 = new MySqlCommand(cadena1, cn.conectarbd);
                MySqlCommand command2 = new MySqlCommand(cadena2, cn.conectarbd);
                MySqlCommand command3 = new MySqlCommand(cadena3, cn.conectarbd);
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                command3.ExecuteNonQuery();
                MessageBox.Show("Encuentro modificado correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e) { MessageBox.Show("Error al modificar el club: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            cn.cerrarconexion();
        }

        public void actualizarEncuentro(string id, string gol1, string gol2, string fecha, string hora, string cancha, string dia, string youtube, string imagenes)
        {
            string cadena = "";
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            if (gol1 == "")
            {
                gol1 = "null";
                gol2 = "null";
            }
            if (dia != "")
            {
            cadena = "UPDATE partido SET golesLocal = " + gol1 + ",golesVisita = " + gol2 + ", hora = '" + hora + "'" +
                ", fecha = '" + dia + "' , fechaLiga =  (SELECT ID from fecha where numero = '" + fecha + "' and division=1), url_transmicion = '" + youtube + "', url_imagenes = '" + imagenes + "' WHERE ID=" + id;
            }
            else
            {
             cadena = "UPDATE partido SET golesLocal = " + gol1 + ",golesVisita = " + gol2 + ", hora = '" + hora + "'" +
                 ", fechaLiga =  (SELECT ID from fecha where numero = '" + fecha + "' and division=1), url_transmicion = '" + youtube + "', url_imagenes = '" + imagenes + "' WHERE ID=" + id;
            }
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Encuentro modificado correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e) { MessageBox.Show("Error al modificar el encuentro: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            cn.cerrarconexion();
        }

        public void rellenarCruce(string equipo, string categoria, string posicion)
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
            catch (Exception e) { MessageBox.Show("Error al modificar el encuentro: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            cn.cerrarconexion();
        }

        public void actualizarCategoria(string id1, string id2)
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            string cadena = "UPDATE division SET nombre='" + id1 + "' WHERE id='" + id2 + "'";
            try
            {
                MySqlCommand command = new MySqlCommand(cadena, cn.conectarbd);
                command.ExecuteNonQuery();
                MessageBox.Show("Division modificada correctamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e) { MessageBox.Show("Error al modificar la división: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            cn.cerrarconexion();
        }

        public void ModificarYRellenarCruce(string idPartido, string tipo, string hora, string fecha, string cancha, string gl, string gv, string pl, string pv, string idLocal, string idVisita, string categoria)
        {
            try { cn.abrirconexion(); } catch (Exception e) { MessageBox.Show("Error al conectarse con la base de datos: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

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
                MessageBox.Show("Se ha modificado el cruce exitosamente", "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e) { MessageBox.Show("Error al modificar el cruce: " + e.Message, "Liga Infantil del Paraná", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            cn.cerrarconexion();

        }
    }
}
