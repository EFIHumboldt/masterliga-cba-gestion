package com.example.aplicacionsarmientitoii

import android.os.Bundle
import android.view.View
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.android.volley.Request
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley
import org.json.JSONArray
import org.json.JSONObject

class ZonasActivity : AppCompatActivity(), View.OnClickListener {

    private lateinit var adapterZonas : AdaptadorZona
    private lateinit var recyclerViewZonas: RecyclerView


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_fase_de_grupos)


        lateinit var zona: String
        val dato = MainActivity.GlobalVars.categoriaZona
        val boton = findViewById<TextView>(R.id.button)
        boton.text = dato
        setTitle(dato)

        val queue = Volley.newRequestQueue(this)

        //val url = "${MainActivity.GlobalVars.url}api_cantidad_zonas.php?id=${dato}"

        supportActionBar?.setDisplayHomeAsUpEnabled(false)

        val ButtonOpen : Button = findViewById(R.id.button_volver_fragment)

        ButtonOpen.setOnClickListener { finish() }

        val layoutManager = LinearLayoutManager(this)
        recyclerViewZonas = findViewById(R.id.recycler_view_zonas)
        recyclerViewZonas.layoutManager = layoutManager
        recyclerViewZonas.setHasFixedSize(true)

        MainActivity.GlobalVars.arrayZonas = mutableListOf()

        /*

        val stringRequest = StringRequest(Request.Method.GET, url, { response ->
            if(response.toString().substring(response.toString().length - 5,response.toString().length) != "error"){
                val jsonArray = JSONArray(response)
                for (i in 0 until jsonArray.length()) {
                    val jsonObjet = JSONObject(jsonArray.getString(i))

                    zona = jsonObjet.get("id_zona").toString()

                    //100 zona

                    MainActivity.GlobalVars.arrayZonas.add(zona)
                }

         */
                MainActivity.GlobalVars.arrayZonas.add("Zonas")
                MainActivity.GlobalVars.arrayZonas.add("Cruces")
                adapterZonas = AdaptadorZona()
                recyclerViewZonas.adapter = adapterZonas
        /*
            }
        }, {
            Toast.makeText(this, "No se conecto con la BD", Toast.LENGTH_SHORT).show()
        })
        queue.add(stringRequest)

*/
    }

    override fun onClick(p0: View?) {

    }



}