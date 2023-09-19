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
import com.example.aplicacionsarmientitoii.CosasCruce.AdaptadorCruce
import com.example.aplicacionsarmientitoii.CosasCruce.Cruce
import com.example.aplicacionsarmientitoii.CosasEncuentros.Encuentro
import com.example.aplicacionsarmientitoii.MainActivity
import com.example.aplicacionsarmientitoii.R
import org.json.JSONArray
import org.json.JSONObject

class DetallesZonaCruceActivity: AppCompatActivity() {

    private lateinit var adapter : AdaptadorCruce
    private lateinit var recyclerViewEncuentros: RecyclerView
    val layoutManager = LinearLayoutManager(this)

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_elegir_encuentro)



        val categoria = MainActivity.GlobalVars.categoriaZona.toInt()

        setTitle("Cruces Categoria " + categoria.toString())

        recyclerViewEncuentros = findViewById(R.id.recycler_view_encuentros)
        recyclerViewEncuentros.layoutManager = layoutManager
        recyclerViewEncuentros.setHasFixedSize(true)

        dataInitialize(categoria)

    }

    private fun dataInitialize(categoria: Int) {

        var enc = mutableListOf<Cruce>()

        var queue1 = Volley.newRequestQueue(this)

        var url1 = "${MainActivity.GlobalVars.url}api_encuentros_cruces.php?id=$categoria"

        var stringRequest1 = StringRequest(Request.Method.GET, url1, { response ->
            if(response.toString().substring(response.toString().length - 5,response.toString().length) != "error"){
                val jsonArray = JSONArray(response)

                for (i in 0 until jsonArray.length()) {

                    val jsonObjet = JSONObject(jsonArray.getString(i))

                    var text0 = jsonObjet.get("id_partido").toString()
                    var text1 = jsonObjet.get("id1eq").toString()
                    var text2 = jsonObjet.get("id2eq").toString()
                    var text3 = jsonObjet.get("nombre").toString()
                    var text4 = jsonObjet.get("nombre2").toString()
                    var text5 = jsonObjet.get("goles1").toString()
                    var text6 = jsonObjet.get("goles2").toString()
                    var text7 = jsonObjet.get("penales1").toString()
                    var text8 = jsonObjet.get("penales2").toString()
                    var text9 = jsonObjet.get("hora").toString()
                    var text10 = jsonObjet.get("cancha").toString()
                    var text11 = jsonObjet.get("tipo").toString()
                    var text12 = jsonObjet.get("esc1").toString()
                    var text13 = jsonObjet.get("esc2").toString()

                    if (text5 == "null") text5 = ""
                    if (text6 == "null") text6 = ""
                    if (text7 == "null") text7 = ""
                    if (text8 == "null") text8 = ""

                    var cruce = Cruce(text0, text1, text2, text3,text4,text5,text6,text7,text8,text9,text10,text11, text12, text13)
                    enc.add(cruce)
                }

                MainActivity.GlobalVars.crucesArrayList = enc

                adapter = AdaptadorCruce()
                recyclerViewEncuentros.adapter = adapter

            }
        }, {
            Toast.makeText(this, "No se conecto con la BD", Toast.LENGTH_SHORT).show()
        })
        queue1.add(stringRequest1)
    }

}