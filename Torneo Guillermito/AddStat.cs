﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIPa
{
    public partial class AddStat : Form
    {
        public string value = string.Empty;
        public string id_partido = string.Empty;
        Querys q = new Querys();
        DataTable dt1, dt2 = new DataTable();
        Action refresh;
        TextBox gl, gv, yl, yv, rl, rv;

        private void checkEncuentroComenzado_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkEncuentroComenzado_Click(object sender, EventArgs e)
        {
            if (checkEncuentroComenzado.Checked == true)
            {
                if (MessageBox.Show("¿Está seguro que desea comenzar el partido?", "LIPa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Querys q = new Querys();
                    q.ComenzarEncuentro(id_partido);
                    menuPrincipal.refreshEncuentros(true);
                }
                else
                {
                    checkEncuentroComenzado.Checked = false;
                }
            }
            else if(checkEncuentroComenzado.Checked == false)
            {
                if (MessageBox.Show("Si lo desactiva, borrará todas las acciones relacionadas a este partido, ¿está seguro?", "LIPa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Querys q = new Querys();
                    q.ReiniciarEncuentro(id_partido);
                    menuPrincipal.refreshEncuentros(false);

                    dt2 = q.LlenarTablaStats(id_partido, value);
                    dgvAcciones.Rows.Clear();

                    foreach (DataRow row in dt2.Rows)
                    {
                        dgvAcciones.Rows.Add(new string[]
                        {
                    row[0].ToString(), //ID partido
                    row[1].ToString(), //ID jugador
                    row[2].ToString(), //Nombre Completo
                    row[3].ToString(), //Dorsal

                        });
                    }

                }
                else
                {
                    checkEncuentroComenzado.Checked = true;
                }
            }
        }

        private gbEncuentros menuPrincipal;
        public AddStat(string id_partido, string value, Action refresh, TextBox glocal, TextBox gvisit, TextBox ylocal, TextBox yvisit, TextBox rlocal, TextBox rvisit)
        {
            this.value = value;
            this.id_partido = id_partido;
            this.refresh = refresh;
            this.gl = glocal;
            this.gv = gvisit;
            this.yl = ylocal;
            yv = yvisit;
            rl = rlocal;
            rv = rvisit;
            InitializeComponent();
        }
        public AddStat(string id_partido, string value, gbEncuentros menu, bool comenzado)
        {
            this.value = value;
            this.id_partido = id_partido;
            this.menuPrincipal = menu;
            InitializeComponent();
            this.checkEncuentroComenzado.Checked = comenzado;
        }

        private void AddStat_Load(object sender, EventArgs e)
        {
            dt2 = q.LlenarTablaStats(id_partido, value);
            dgvAcciones.Rows.Clear();

            foreach (DataRow row in dt2.Rows)
            {
                dgvAcciones.Rows.Add(new string[]
                {
                    row[0].ToString(), //ID partido
                    row[1].ToString(), //ID jugador
                    row[2].ToString(), //Nombre Completo
                    row[3].ToString(), //Dorsal

                });
            }

            string argument = String.Empty;

            switch (value)
            {
                case "add-goal-local":
                    label2.Text = "Agregar gol de local";
                    argument = "local";
                    break;
                case "add-goal-visit":
                    label2.Text = "Agregar gol de visita";
                    argument = "visit";
                    break;
                case "rest-goal-local":
                    label2.Text = "Quitar gol de local";
                    dgvJugadoresPartido.Enabled = false;
                    plusStat.Enabled = false;
                    argument = "local";
                    break;
                case "rest-goal-visit":
                    label2.Text = "Quitar gol de visita";
                    dgvJugadoresPartido.Enabled = false;
                    plusStat.Enabled = false;
                    argument = "visit";
                    break;
                case "add-yc-local":
                    label2.Text = "Agregar tarjeta amarilla de local";
                    argument = "local";
                    break;
                case "add-yc-visit":
                    label2.Text = "Agregar tarjeta amarilla de visita";
                    argument = "visit";
                    break;
                case "rest-yc-local":
                    label2.Text = "Quitar tarjeta amarilla de local";
                    dgvJugadoresPartido.Enabled = false;
                    plusStat.Enabled = false;
                    argument = "local";
                    break;
                case "rest-yc-visit":
                    label2.Text = "Quitar tarjeta amarilla de visita";
                    dgvJugadoresPartido.Enabled = false;
                    plusStat.Enabled = false;
                    argument = "visit";
                    break;
                case "add-rc-local":
                    label2.Text = "Agregar tarjeta roja de local";
                    argument = "local";
                    break;
                case "add-rc-visit":
                    label2.Text = "Agregar tarjeta roja de visita";
                    argument = "visit";
                    break;
                case "rest-rc-local":
                    label2.Text = "Quitar tarjeta roja de local";
                    dgvJugadoresPartido.Enabled = false;
                    plusStat.Enabled = false;
                    argument = "local";
                    break;
                case "rest-rc-visit":
                    label2.Text = "Quitar tarjeta roja de visita";
                    dgvJugadoresPartido.Enabled = false;
                    plusStat.Enabled = false;
                    argument = "visit";
                    break;

            }

            dt1 = q.LlenarTablaJugadores(id_partido, argument);
            dgvJugadoresPartido.Rows.Clear();

            foreach (DataRow row in dt1.Rows)
            {
                dgvJugadoresPartido.Rows.Add(new string[]
                {
                    row[0].ToString(), //ID jugador
                    row[1].ToString(), //Nombre Completo
                    row[2].ToString(), //Dorsal

                });
            }
        }

        private void plusStat_Click(object sender, EventArgs e)
        {
            //TODO: Si bien se actualiza en la tabla el partido, se tiene que ver el cambio en la pantalla con el partido abierto
            //TODO: Ver que si son nulos los goles es pq va a ser el primero, y no se puede hacer X+=1

            if (dgvJugadoresPartido.SelectedRows.Count != 0)
            {
                if (checkEncuentroComenzado.Checked == false)
                {
                    MessageBox.Show("Para añadir una acción, debe tener el partido comenzado", "LIPa", MessageBoxButtons.OK);
                    return;
                }
                string id_jugador = dgvJugadoresPartido.SelectedRows[0].Cells[0].Value.ToString();
                q.InsertarAccion(id_partido, id_jugador, value);

                dt2 = q.LlenarTablaStats(id_partido, value);
                dgvAcciones.Rows.Clear();

                foreach (DataRow row in dt2.Rows)
                {
                    dgvAcciones.Rows.Add(new string[]
                    {
                    row[0].ToString(), //ID partido
                    row[1].ToString(), //ID jugador
                    row[2].ToString(), //Nombre Completo
                    row[3].ToString(), //Dorsal

                    });
                }
                menuPrincipal.refreshEncuentros(false);
                
            }
        }

        private void btEliminarEncuentro_Click(object sender, EventArgs e)
        {
               //TODO: Si bien se actualiza en la tabla el partido, se tiene que ver el cambio en la pantalla con el partido abierto

            if (dgvAcciones.SelectedRows.Count != 0)
            {
                string id_accion = dgvAcciones.SelectedRows[0].Cells[0].Value.ToString();
                string id_jugador = dgvAcciones.SelectedRows[0].Cells[1].Value.ToString();
                if (MessageBox.Show("¿Está seguro que desea quitar esa accion?", "LiPA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    q.EliminarAccion(id_partido, id_accion, id_jugador, value);

                    dt2 = q.LlenarTablaStats(id_partido, value);
                    dgvAcciones.Rows.Clear();

                    foreach (DataRow row in dt2.Rows)
                    {
                        dgvAcciones.Rows.Add(new string[]
                        {
                            row[0].ToString(), //ID partido
                            row[1].ToString(), //ID jugador
                            row[2].ToString(), //Nombre Completo
                            row[3].ToString(), //Dorsal

                        });
                    }

                }
                menuPrincipal.refreshEncuentros(false);
            }
        }
    }
}
