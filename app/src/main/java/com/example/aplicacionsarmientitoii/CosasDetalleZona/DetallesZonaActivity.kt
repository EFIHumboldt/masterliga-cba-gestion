package com.example.aplicacionsarmientitoii.CosasDetalleZona

import android.os.Bundle
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.android.volley.Request
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley
import com.example.aplicacionsarmientitoii.AdaptadorEncuentro
import com.example.aplicacionsarmientitoii.CosasEncuentros.Encuentro
import com.example.aplicacionsarmientitoii.MainActivity
import com.example.aplicacionsarmientitoii.R
import org.json.JSONArray
import org.json.JSONObject

class DetallesZonaActivity: AppCompatActivity() {

    private lateinit var adapter : AdaptadorEncuentro
    private lateinit var recyclerViewEncuentros: RecyclerView
    val layoutManager = LinearLayoutManager(this)

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_elegir_encuentro)


        val zona = MainActivity.GlobalVars.zonaG
        val categoria = MainActivity.GlobalVars.categoriaZona.toInt()


        recyclerViewEncuentros = findViewById(R.id.recycler_view_encuentros)
        recyclerViewEncuentros.layoutManager = layoutManager
        recyclerViewEncuentros.setHasFixedSize(true)

        dataInitialize(zona, categoria)

        }

    private fun dataInitialize(zona: String, categoria: Int) {

        var enc = mutableListOf<Encuentro>()

        var queue1 = Volley.newRequestQueue(this)

        //var url1 = "${MainActivity.GlobalVars.url}api_encuentros_todos.php?id1=$categoria"

        var url1 = "${MainActivity.GlobalVars.url}api_encuentros_zona.php?id=$categoria"

        var stringRequest1 = StringRequest(Request.Method.GET, url1, { response ->
            if(response.toString().substring(response.toString().length - 5,response.toString().length) != "error"){
                val jsonArray = JSONArray(response)

                for (i in 0 until jsonArray.length()) {

                    val jsonObjet = JSONObject(jsonArray.getString(i))

                    var id1 = jsonObjet.get("cancha")
                    var id2 = jsonObjet.get("id_partido")

                    var tipozona = jsonObjet.get("id_zona")
                    var tipozona2 = jsonObjet.get("id_zona2")

                    if (tipozona != tipozona2) {tipozona = "INTERZONAL"}
                    else {tipozona = zona}

                    val text1 = jsonObjet.get("nombre")
                    var text2 = jsonObjet.get("goles1").toString()
                    val text3 = jsonObjet.get("nombre2")
                    var text4 = jsonObjet.get("goles2").toString()
                    val text5: String = jsonObjet.get("hora").toString()
                    val text6 = jsonObjet.get("fecha")
                    val esc1 = jsonObjet.get("escudo1")
                    val esc2 = jsonObjet.get("escudo2")

                    if (text2 == "null" ) text2 = ""
                    if (text4 == "null") text4 = ""

                    val encuentro = Encuentro(id1.toString().toInt(), id2.toString().toInt(),  esc1.toString(), text1.toString(), text2, esc2.toString(), text3.toString(), text4, text5, text6.toString())
                    enc.add(encuentro)
                }

                MainActivity.GlobalVars.encuentrosArrayList = enc

                adapter = AdaptadorEncuentro()
                recyclerViewEncuentros.adapter = adapter

            }
        }, {
            Toast.makeText(this, "No se pudo conectar con el Servidor", Toast.LENGTH_SHORT).show()
        })
        queue1.add(stringRequest1)
    }

}