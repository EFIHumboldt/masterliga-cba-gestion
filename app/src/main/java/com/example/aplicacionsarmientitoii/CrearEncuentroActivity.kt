package com.example.aplicacionsarmientitoii

import android.os.Bundle
import android.text.TextWatcher
import android.view.View
import android.widget.*
import androidx.appcompat.app.AlertDialog
import androidx.appcompat.app.AppCompatActivity
import com.android.volley.Request
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley
import com.example.aplicacionsarmientitoii.CosasDetalleZona.Posicion
import com.example.aplicacionsarmientitoii.CosasEncuentros.Encuentro
import com.example.aplicacionsarmientitoii.CosasEquipo.Equipos
import org.json.JSONArray
import org.json.JSONObject
import kotlin.system.exitProcess

class CrearEncuentroActivity: AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_crear_encuentro)
        val goles = findViewById<EditText>(R.id.goleslocal)
        val goles1 = findViewById<EditText>(R.id.golesVisita)
        val hora = findViewById<EditText>(R.id.horaedit)
        var eq = mutableListOf<Equipos>()

        val categoria = findViewById<Spinner>(R.id.spncategoria)
        val zona = findViewById<Spinner>(R.id.spnzona)
        val lista = MainActivity.GlobalVars.anioCategoria;
        var adaptador = ArrayAdapter(this,android.R.layout.simple_spinner_item, lista)
        categoria.adapter = adaptador

        val fecha = findViewById<Spinner>(R.id.spnfecha)
        //LAS FECHAS NO SON LAS MISMAS
        val listaf = listOf("2023-11-10", "2023-11-11", "2023-11-12")
        var adaptador1 = ArrayAdapter(this,android.R.layout.simple_spinner_item, listaf)
        fecha.adapter = adaptador1

        val cancha = findViewById<Spinner>(R.id.spncancha)
        datainitialiceC(cancha)
        var finalizado = findViewById<CheckBox>(R.id.finalizado)


        finalizado.setOnClickListener {
        if(finalizado.isChecked == false)
        {
            goles1.setEnabled(false)
            goles.setEnabled(false)
        }else
            goles.setEnabled(true)
            goles1.setEnabled(true)
        }

        categoria.onItemSelectedListener = object:
        AdapterView.OnItemSelectedListener{
            override fun onItemSelected(p0: AdapterView<*>?, p1: View?, p2: Int, p3: Long) {
                datainitialice1(categoria, zona)
            }

            override fun onNothingSelected(p0: AdapterView<*>?) {
                TODO("Not yet implemented")
            }

        }
        zona.onItemSelectedListener = object:
        AdapterView.OnItemSelectedListener{
            override fun onItemSelected(p0: AdapterView<*>?, p1: View?, p2: Int, p3: Long) {
                datainitialice(categoria, zona, eq)
            }

            override fun onNothingSelected(p0: AdapterView<*>?) {
                TODO("Not yet implemented")
            }

        }

val botonCrearEncuentro = findViewById<Button>(R.id.agregar)
        botonCrearEncuentro.setOnClickListener(){

            val builder = AlertDialog.Builder(this)
            builder.setTitle("Alerta")
            builder.setMessage("¿Esta seguro que desea agregar el encuentro?")
            builder.setPositiveButton("Aceptar"){

                    dialog, which -> crearCruce(goles,goles1, fecha, hora, cancha, eq)
            }
            builder.setNegativeButton("Cancelar", null)
            builder.show()
        }
    }

    fun crearCruce(golesl: EditText, golesv: EditText, fecha: Spinner, hora: EditText, cancha: Spinner, eq : MutableList<Equipos>)
    {
        var queue = Volley.newRequestQueue(this)
        var url : String
        var equipoLocal = findViewById<Spinner>(R.id.spnequipolocal)
        var equipoVisitante = findViewById<Spinner>(R.id.spnequipovisitante)
        var finalizado = findViewById<CheckBox>(R.id.finalizado)
        var gl : String
        var gv : String
        var e1 : Int = 0
        var e2 : Int = 0
        if(hora.text.isEmpty()){
            Toast.makeText(this, "La hora no puede estar vacía", Toast.LENGTH_SHORT).show()
            return
        }
        if(equipoLocal.selectedItem == equipoVisitante.selectedItem){
            Toast.makeText(this, "Los equipos no pueden ser iguales", Toast.LENGTH_SHORT).show()
            return
        }
        if(finalizado.isChecked == false){
            gl = "null"
            gv="null"
        }else if(golesl.text.isEmpty() || golesv.text.isEmpty()){
            Toast.makeText(this, "Los goles no pueden estar vacios", Toast.LENGTH_SHORT).show()
            return
        }else
        {
            gl = golesl.text.toString()
            gv = golesv.text.toString()
        }
        for(i in 0 until eq.size){
            if(eq[i].tvNombre == equipoLocal.selectedItem.toString())
                e1 = eq[i].idEquipo
            else if(eq[i].tvNombre == equipoVisitante.selectedItem.toString())
                e2 = eq[i].idEquipo
        }
                url="${MainActivity.GlobalVars.url}api_insertar_encuentro.php?equipo1=$e1" +
                        "&equipo2=$e2" +
                        "&goles1=$gl&goles2=$gv" +
                        "&fecha='${fecha.selectedItem}'" +
                        "&hora='${hora.text}'" +
                        "&cancha=${cancha.selectedItem}" +
                        "&tipo=0"

        Toast.makeText(this, url, Toast.LENGTH_SHORT).show()
        var stringRequest = StringRequest(Request.Method.GET, url, { response -> if (response == "0") Toast.makeText(this, "verifique que los datos estan escritos en su formato correcto, goles (solo números), hora (00:00 a 23:59)", Toast.LENGTH_SHORT).show()
            else {Toast.makeText(this, "El encuentro se agregó correctamente", Toast.LENGTH_SHORT).show()
            limpiar(hora, golesl, golesv)}
        }, {
        Toast.makeText(this, "verifique que los datos estan escritos en su formato correcto, goles (solo números), hora (00:00 a 23:59) o su conexion a internet", Toast.LENGTH_SHORT).show()
    })
        queue.add(stringRequest)
    }

    private fun limpiar(hora: EditText, golesl: EditText, golesv: EditText){

    }

    private fun datainitialiceC(cancha: Spinner){
        //Zonas
        val queue1 = Volley.newRequestQueue(this)
        val url1 = "${MainActivity.GlobalVars.url}api_canchas.php"
        val stringRequest1 = StringRequest(
            Request.Method.GET, url1, { response ->

                if(response.toString().substring(response.toString().length - 5,response.toString().length) != "error"){

                    val jsonArray = JSONArray(response)

                    if (jsonArray.length() > 0) {
                        var canchal = mutableListOf<String>()

                        for (i in 0 until jsonArray.length()) {

                            val jsonObjet = JSONObject(jsonArray.getString(i))

                            val text1 = jsonObjet.get("id_cancha")
                            canchal.add(text1.toString())

                        }
                        val adaptador1 = ArrayAdapter(this,android.R.layout.simple_spinner_item, canchal)
                        cancha.adapter = adaptador1

                    }
                }
            },
            {
                Toast.makeText(this, "No se pudo conectar con el Servidor (1)", Toast.LENGTH_SHORT).show()
            })
        queue1.add(stringRequest1)
    }

    private fun datainitialice1(categoria: Spinner, zona: Spinner){
        //Zonas
        val queue1 = Volley.newRequestQueue(this)
        val url1 = "${MainActivity.GlobalVars.url}api_cantidad_zonas.php?id=${categoria.selectedItem}"
        val stringRequest1 = StringRequest(
            Request.Method.GET, url1, { response ->

                if(response.toString().substring(response.toString().length - 5,response.toString().length) != "error"){



                    val jsonArray = JSONArray(response)

                    if (jsonArray.length() > 0) {
                        var zonalist = mutableListOf<String>()


                        for (i in 0 until jsonArray.length()) {

                            val jsonObjet = JSONObject(jsonArray.getString(i))



                            val text1 = jsonObjet.get("id_zona")
                            zonalist.add(text1.toString())

                        }
                        zonalist.add("INTERZONAL")
                        val adaptador = ArrayAdapter(this,android.R.layout.simple_spinner_item, zonalist)
                        zona.adapter = adaptador

                    }
                }
            },
            {
                Toast.makeText(this, "No se pudo conectar con el Servidor (2)", Toast.LENGTH_SHORT).show()
            })
        queue1.add(stringRequest1)
    }
    private fun datainitialice(categoria : Spinner, zona : Spinner, eq : MutableList<Equipos>){
        //Equipos
        val queue = Volley.newRequestQueue(this)
        val zonaD : String
        if(zona.selectedItem == "INTERZONAL")
            zonaD = "%"
        else
            zonaD = zona.selectedItem.toString()

        val url = "${MainActivity.GlobalVars.url}api_equipos_spinner.php?id=${categoria.selectedItem}&id1=$zonaD"
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
                        val equipolocal = findViewById<Spinner>(R.id.spnequipolocal)
                        val equipovisitante = findViewById<Spinner>(R.id.spnequipovisitante)
                        val adaptador = ArrayAdapter(this,android.R.layout.simple_spinner_item, nombre)
                        equipolocal.adapter = adaptador
                        equipovisitante.adapter = adaptador

                    }
                }
            },
            {
                Toast.makeText(this, "No se pudo conectar con el Servidor (3)", Toast.LENGTH_SHORT).show()
            })
        queue.add(stringRequest)
    }
}