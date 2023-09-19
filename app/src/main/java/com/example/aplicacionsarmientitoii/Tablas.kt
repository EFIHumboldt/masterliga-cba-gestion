package com.example.aplicacionsarmientitoii

import android.content.Intent
import android.os.Bundle
import android.os.Handler
import android.widget.ArrayAdapter
import android.widget.Button
import android.widget.Spinner
import android.widget.Toast
import androidx.appcompat.app.AlertDialog
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.isVisible
import androidx.recyclerview.widget.ItemTouchHelper
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.android.volley.Request
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley
import com.example.aplicacionsarmientitoii.CosasDetalleZona.Posicion
import com.example.aplicacionsarmientitoii.CosasEncuentros.Encuentro
import com.example.aplicacionsarmientitoii.CosasEquipo.Equipos

import org.json.JSONArray
import org.json.JSONObject
import java.text.SimpleDateFormat
import java.util.*
import kotlin.collections.HashMap

class Tablas: AppCompatActivity() {

    lateinit var adapter: PosicionesZonaAdapter
    lateinit var recyclerviewposiciones: RecyclerView
    val layoutManager = LinearLayoutManager(this)

    lateinit var botonCrearCruce: Button


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_tablas)


        recyclerviewposiciones = findViewById(R.id.recycler_view_equipos)
        recyclerviewposiciones.layoutManager = layoutManager
        recyclerviewposiciones.setHasFixedSize(true)

        adapter = PosicionesZonaAdapter()
        recyclerviewposiciones.adapter = adapter

        botonCrearCruce = findViewById(R.id.btnCrearCruce1)

        val item : ItemTouchHelper
        item = ItemTouchHelper(object: ItemTouchHelper.SimpleCallback(ItemTouchHelper.DOWN or ItemTouchHelper.UP,0){
            override fun onMove(
                recyclerView: RecyclerView,
                viewHolder: RecyclerView.ViewHolder,
                target: RecyclerView.ViewHolder
            ): Boolean {
                val inicial = viewHolder.adapterPosition
                val final=target.adapterPosition

                MainActivity.GlobalVars.posicionesOrdenadosZonaArrayList[inicial].puesto = final + 1
                MainActivity.GlobalVars.posicionesOrdenadosZonaArrayList[final].puesto = inicial + 1
                Collections.swap(MainActivity.GlobalVars.posicionesOrdenadosZonaArrayList, inicial, final)
                adapter.notifyItemMoved(inicial,final)
                return true
            }

            override fun onSwiped(viewHolder: RecyclerView.ViewHolder, direction: Int) {
                TODO("Not yet implemented")
            }
        })

        item.attachToRecyclerView(recyclerviewposiciones)

        botonCrearCruce.setOnClickListener(){

            val builder = AlertDialog.Builder(this)
            builder.setTitle("Alerta")
            builder.setMessage("¿Esta seguro que desea crear los cruces?")
            builder.setPositiveButton("Aceptar"){

                    dialog, which -> crearCruce();
            }
            builder.setNegativeButton("Cancelar", null)
            builder.show()
        }

    }

    fun crearCruce() {
        val bundle = intent.extras
        var categoria = bundle?.getString("ID")

        for(i in 1..MainActivity.GlobalVars.posicionesOrdenadosZonaArrayList.size)
        {
            val queue = Volley.newRequestQueue(this)
            val url = "${MainActivity.GlobalVars.url}api_rellenar_cruce.php?id=${MainActivity.GlobalVars.posicionesOrdenadosZonaArrayList[i-1].id_equipo}&id1=${i}"

            var stringRequest = StringRequest(Request.Method.GET, url, { response -> if (response == "0") Toast.makeText(this, "Verifique que los datos estan escritos en su formato correcto, goles (solo números), hora (00:00 a 23:59)", Toast.LENGTH_SHORT).show()
            }, {
                Toast.makeText(this, "verifique que los datos estan escritos en su formato correcto, goles (solo números), hora (00:00 a 23:59) o su conexion a internet", Toast.LENGTH_SHORT).show()
            })
            queue.add(stringRequest)
        }

        Toast.makeText(this, "El cruce se creó correctamente", Toast.LENGTH_SHORT).show()
    }
}
