package com.example.aplicacionsarmientitoii.CosasCruce

import android.os.Bundle
import android.widget.*
import androidx.appcompat.app.AlertDialog
import androidx.appcompat.app.AppCompatActivity
import com.android.volley.Request
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley
import com.example.aplicacionsarmientitoii.CosasEquipo.Equipos
import com.example.aplicacionsarmientitoii.MainActivity
import com.example.aplicacionsarmientitoii.R
import org.json.JSONArray
import org.json.JSONObject
import java.util.*

class AhoraSiModificarCruceActivity: AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_modificar_encuentro3)

        val Spinner1 = findViewById<Spinner>(R.id.tvEquipoL)
        val Spinner2 = findViewById<Spinner>(R.id.tvEquipoV)
        val eq = mutableListOf<Equipos>()



        val goles = findViewById<EditText>(R.id.goleslocal)
        val goles1 = findViewById<EditText>(R.id.golesVisita)
        val penales = findViewById<EditText>(R.id.goleslocal2)
        val penales1 = findViewById<EditText>(R.id.golesVisita2)
        var tvhora1 = findViewById<EditText>(R.id.horaedit)
        var tvcancha = findViewById<EditText>(R.id.tvcancha)
        val tvcategoria = findViewById<EditText>(R.id.tvcategoria)
        val idpartido = findViewById<TextView>(R.id.idequipo)

        var finalizado = findViewById<CheckBox>(R.id.finalizado)

        var fecha = findViewById<EditText>(R.id.tvfecha)


        val bundle = intent.extras

        var idp = bundle?.getString("ID").toString()
        var idLocal = bundle?.getString("id1").toString()
        var idVisita = bundle?.getString("id2").toString()
        var equipo1 = bundle?.getString("e1").toString()
        var equipo2 = bundle?.getString("e2").toString()
        var golesLocal = bundle?.getString("g1")
        var golesVisita = bundle?.getString("g2")
        var penalesLocal = bundle?.getString("p1")
        var penalesVisita = bundle?.getString("p2")
        var hora = bundle?.getString("hora").toString()
        hora = hora.substring(0, 5)
        var cancha = bundle?.getString("cancha")
        var tipoCruce = bundle?.getString("tipo").toString()

        var categoria = MainActivity.GlobalVars.categoriaZona
        var zona = MainActivity.GlobalVars.zonaG

        datainitialice(Spinner1, Spinner2, MainActivity.GlobalVars.categoriaZona, eq, equipo1, equipo2)


        finalizado.isChecked = golesLocal.toString() != ""


        goles.setText(golesLocal.toString())
        goles1.setText(golesVisita.toString())
        penales.setText(penalesLocal.toString())
        penales1.setText(penalesVisita.toString())
        tvhora1.setText(hora)
        tvcancha.setText(cancha)

        finalizado.setOnClickListener {
            if (finalizado.isChecked == false) {
                goles1.setEnabled(false)
                goles.setEnabled(false)
                penales.setEnabled(false)
                penales1.setEnabled(false)
            } else
                goles.setEnabled(true)
            goles1.setEnabled(true)
            penales.setEnabled(true)
            penales1.setEnabled(true)
        }


        val botonCrearEncuentro = findViewById<Button>(R.id.modificar)
        botonCrearEncuentro.setOnClickListener() {

            val builder = AlertDialog.Builder(this)
            builder.setTitle("Alerta")
            builder.setMessage("¿Esta seguro que desea modificar el encuentro?")
            builder.setPositiveButton("Aceptar") {

                    dialog, which ->
                modificarEncuentro(goles, goles1, penales, penales1, tvhora1, idp, tvcancha, finalizado, tipoCruce, idLocal, idVisita, categoria, eq, Spinner1, Spinner2)
            }
            builder.setNegativeButton("Cancelar", null)
            builder.show()
        }
    }

    fun modificarEncuentro(
        golesl: EditText,
        golesv: EditText,
        penalesl: EditText,
        penalesv: EditText,
        hora: EditText,
        idp: String,
        cancha: EditText,
        finalizado: CheckBox,
        tipoCruce: String,
        idLocal: String,
        idVisita: String,
        categoria: String,
    eq: MutableList<Equipos>,
        sp1: Spinner,
        sp2: Spinner
    ) {

        var queue = Volley.newRequestQueue(this)
        var url: String
        var gl: String = ""
        var gv: String = ""
        var pl: String = ""
        var pv: String = ""


        if (cancha.text.isEmpty()) {
            Toast.makeText(this, "La cancha no puede estar vacía", Toast.LENGTH_SHORT).show()
            return
        }
        if (hora.text.isEmpty()) {
            Toast.makeText(this, "La hora no puede estar vacía", Toast.LENGTH_SHORT).show()
            return
        }
        if (finalizado.isChecked == false) {
            gl = "null"
            gv = "null"
            pl="null"
            pv="null"
        } else
        {
            if (golesl.text.isEmpty() || golesv.text.isEmpty()) {
                Toast.makeText(this, "Los goles no pueden estar vacios", Toast.LENGTH_SHORT).show()
                return
            }else if(golesl.text.toString().toInt() == golesv.text.toString().toInt()){
                if(penalesv.text.isEmpty() || penalesl.text.isEmpty()) {
                    Toast.makeText(
                        this,
                        "Los penales no pueden estar vacios en caso de empate",
                        Toast.LENGTH_SHORT
                    ).show()
                    return
                }else {pl = penalesl.text.toString(); pv = penalesv.text.toString();gl = golesl.text.toString()
                    gv = golesv.text.toString()}
            }else{
                pl="null"
                pv="null"
                gl = golesl.text.toString()
                gv = golesv.text.toString()
            }
        }
        var e1: Int =0
        var e2: Int =0
        for(i in 0 until eq.size){
            if(eq[i].tvNombre == sp1.selectedItem.toString())
                e1 = eq[i].idEquipo
            else if(eq[i].tvNombre == sp2.selectedItem.toString())
                e2 = eq[i].idEquipo
        }
        url = "${MainActivity.GlobalVars.url}api_modificar_cruce.php?id=$idp" +
                "&e1=$e1" +
                "&e2=$e2" +
                "&gl=$gl" +
                "&gv=$gv" +
                "&pl=$pl" +
                "&pv=$pv" +
                "&hora='${hora.text}'" +
                "&can=${cancha.text}"



        var stringRequest = StringRequest(Request.Method.GET, url, { response ->
            if (response == "0") Toast.makeText(
                this,
                "verifique que los datos estan escritos en su formato correcto, goles (solo números), hora (00:00 a 23:59)",
                Toast.LENGTH_SHORT
            ).show()
            else {
                Toast.makeText(this, "El encuentro se modificó correctamente", Toast.LENGTH_SHORT)
                    .show()


                        //ESTABLEZCO UN GANADOR Y UN PERDEDOR

                        var ganador : Int
                        var perdedor : Int


                        if (gl != gv)
                        {
                            if (gl > gv)
                            {
                                ganador = e1
                                perdedor = e2
                            }
                            else
                            {
                                ganador = e2
                                perdedor = e1
                            }
                        }
                        else
                        {
                            if (pl > pv)
                            {
                                ganador = e1
                                perdedor = e2
                            }
                            else
                            {
                                ganador = e2
                                perdedor = e1
                            }
                        }

                        val url1 = "${MainActivity.GlobalVars.url}api_modificar_cruce_auto.php?id=$ganador&id1=$perdedor&id2=$tipoCruce"

                        var stringRequest1 = StringRequest(Request.Method.GET, url1, { response ->
                            if (response == "0") Toast.makeText(
                                this,
                                "verifique que los datos estan escritos en su formato correcto, goles (solo números), hora (00:00 a 23:59)",
                                Toast.LENGTH_SHORT
                            ).show()
                            else {
                                Toast.makeText(this, "El encuentro se modificó correctamente", Toast.LENGTH_SHORT)
                                    .show()
                            }
                        }, {
                            Toast.makeText(
                                this,
                                "verifique que los datos estan escritos en su formato correcto, goles (solo números), hora (00:00 a 23:59) o su conexion a internet",
                                Toast.LENGTH_SHORT
                            ).show()
                        })
                        queue.add(stringRequest1)
            }

        }, {
            Toast.makeText(
                this,
                "verifique que los datos estan escritos en su formato correcto, goles (solo números), hora (00:00 a 23:59) o su conexion a internet",
                Toast.LENGTH_SHORT
            ).show()
        })
        queue.add(stringRequest)




    }

    private fun datainitialice(equipolocal : Spinner, equipovisitante : Spinner, categoria: String, eq: MutableList<Equipos>, equipo1 : String, equipo2 : String){
        //Equipo
        val queue = Volley.newRequestQueue(this)
        val url = "${MainActivity.GlobalVars.url}api_equipos_cruces.php?id=$categoria"

        val stringRequest = StringRequest(
            Request.Method.GET, url, { response ->

                if(response.toString().substring(response.toString().length - 5,response.toString().length) != "error"){



                    val jsonArray = JSONArray(response)

                    if (jsonArray.length() > 0) {

                        var nombre = mutableListOf<String>()

                        for (i in 0 until jsonArray.length()) {

                            val jsonObjet = JSONObject(jsonArray.getString(i))



                            val text1 = jsonObjet.get("id_equipo")
                            val text2 = jsonObjet.get("nombre").toString()
                            val equipo = Equipos(text1.toString().toInt(), "",text2, "", "")

                            eq.add(equipo)
                            nombre.add(equipo.tvNombre)

                        }

                        val adaptador = ArrayAdapter(this,android.R.layout.simple_spinner_item, nombre)
                        equipolocal.adapter = adaptador
                        equipovisitante.adapter = adaptador

                        var posicionL : Int = 0
                        var posicionV : Int = 0

                        for(i in 0 until eq.size){
                            if(eq[i].tvNombre == equipo1)
                                posicionL = i
                            else if(eq[i].tvNombre == equipo2)
                                posicionV = i
                        }

                        equipolocal.setSelection(posicionL)
                        equipovisitante.setSelection(posicionV)

                    }
                }
            },
            {
                Toast.makeText(this, "Se ha producido un error", Toast.LENGTH_SHORT).show()
            })
        queue.add(stringRequest)
    }
}

