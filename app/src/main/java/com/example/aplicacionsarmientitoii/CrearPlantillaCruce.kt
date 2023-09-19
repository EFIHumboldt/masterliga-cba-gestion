package com.example.aplicacionsarmientitoii

import android.content.Intent
import android.os.Bundle
import android.text.Editable
import android.widget.*
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.ItemTouchHelper
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import androidx.viewpager2.widget.ViewPager2
import com.android.volley.Request
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley
import com.efihumboldt.sarmientito.CosasEncuentros.CruceOroFragment
import com.efihumboldt.sarmientito.CosasEncuentros.CrucePlataFragment
import com.example.aplicacionsarmientitoii.CosasDetalleZona.Posicion
import com.example.aplicacionsarmientitoii.CosasEncuentros.Encuentro
import com.example.aplicacionsarmientitoii.CosasEncuentros.TabLayoutCruceAdapter
import com.google.android.material.tabs.TabLayout
import com.google.android.material.tabs.TabLayoutMediator

import org.json.JSONArray
import org.json.JSONObject
import java.util.*
import java.util.concurrent.CountDownLatch
import kotlin.concurrent.thread

class CrearPlantillaCruce : AppCompatActivity() {

    //lateinit var adapter: PosicionesZonaAdapter
    val layoutManager = LinearLayoutManager(this)






    override fun onCreate(savedInstanceState: Bundle?) {


        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_cruce_general)
        supportActionBar?.setDisplayHomeAsUpEnabled(false)


        val bundle = intent.extras

        val tabLayout = findViewById<TabLayout>(R.id.tablayoutencuentros)
        val viewPager2 = findViewById<ViewPager2>(R.id.viewpagerencuentros)
        val adapter = TabLayoutCruceAdapter(supportFragmentManager, lifecycle)


        viewPager2.adapter=adapter

        TabLayoutMediator(tabLayout,viewPager2){tab,position->
            when(position){
                0->{
                    tab.text="COPA ORO"
                }
                1->{
                    tab.text="COPA PLATA"
                }
            }
        }.attach()

        val ButtonOK: Button = findViewById(R.id.button_volver_fragment_encuentro)




        val suggestions = mutableListOf<String>()
        for (i in 1..64) {  suggestions.add("GANADOR ${i}") }
        for (i in 1..64) {  suggestions.add("${i}º TABLA GENERAL")}
        for (i in 1..64) {  suggestions.add("PERDEDOR ${i}")}



        val spinner : Spinner = findViewById(R.id.SpinnerCategoria)

        val items = listOf("2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017")
        val adapterspinner = ArrayAdapter(this, android.R.layout.simple_spinner_item, items)
        adapterspinner.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        spinner.adapter = adapterspinner


        ButtonOK.setOnClickListener{
                val queue = Volley.newRequestQueue(this)
                val url3 = "${MainActivity.GlobalVars.url}api_eliminar_cruce.php?id=${spinner.selectedItem.toString()}"

                var stringRequest = StringRequest(Request.Method.GET, url3, { response ->

                    Toast.makeText(applicationContext, "Eliminado", Toast.LENGTH_SHORT).show()
                    datainitiliace(spinner, suggestions)


                }, {
                    Toast.makeText(this, "verifique que los datos estan escritos en su formato correcto, goles (solo números), hora (00:00 a 23:59) o su conexion a internet", Toast.LENGTH_SHORT).show()
                })
                queue.add(stringRequest)


            Toast.makeText(this, "Cruce creado correctamente", Toast.LENGTH_SHORT).show()
            }
        }

    private fun datainitiliace(spinner : Spinner, suggestions : MutableList<String>) {
        val viewOro = MainActivity.GlobalVars.viewOro
        val viewPlata = MainActivity.GlobalVars.viewPlata
        var error = false;
        var anclaOro = 0
        var anclaPlata = 0
        val id_categoria = spinner.selectedItem.toString()



        for (i in 1..32)
        {
            val id_izquierdo_oro = resources.getIdentifier("label$i" + "l", "id", packageName)
            val id_derecho_oro = resources.getIdentifier("label$i" + "r", "id", packageName)
            val id_hora_oro = resources.getIdentifier("hora$i" , "id", packageName)
            val id_cancha_oro = resources.getIdentifier("cancha$i" , "id", packageName)
            val p_izquierda_oro = viewOro.findViewById<AutoCompleteTextView>(id_izquierdo_oro)
            val p_derecha_oro = viewOro.findViewById<AutoCompleteTextView>(id_derecho_oro)
            val p_hora_oro = viewOro.findViewById<EditText>(id_hora_oro)
            val p_cancha_oro = viewOro.findViewById<EditText>(id_cancha_oro)


            if ((p_izquierda_oro.text.toString() == "") && (p_derecha_oro.text.toString() == "") && (p_cancha_oro.text.toString() == "") && (p_hora_oro.text.toString() == ""))
            {anclaOro = i
                break}else if (!((p_izquierda_oro.text.toString() != "") && (suggestions.contains(p_izquierda_oro.text.toString())) && (p_derecha_oro.text.toString() != "") &&  (suggestions.contains(p_derecha_oro.text.toString())) && (p_cancha_oro.text.toString() != "") && (p_hora_oro.text.toString() != "")))
            {error=true
                Toast.makeText(this, "errorOro", Toast.LENGTH_SHORT).show()
                break}
            if(i==32)
                anclaOro=33
        }
        if (!error){
            for (i in 1..32){
                val id_izquierdo_plata = resources.getIdentifier("labelp$i" + "l", "id", packageName)
                val id_derecho_plata = resources.getIdentifier("labelp$i" + "r", "id", packageName)
                val id_hora_plata = resources.getIdentifier("horap$i", "id", packageName)
                val id_cancha_plata = resources.getIdentifier("canchap$i", "id", packageName)
                val p_izquierda_plata = viewPlata.findViewById<AutoCompleteTextView>(id_izquierdo_plata)
                val p_derecha_plata = viewPlata.findViewById<AutoCompleteTextView>(id_derecho_plata)
                val p_hora_plata = viewPlata.findViewById<EditText>(id_hora_plata)
                val p_cancha_plata = viewPlata.findViewById<EditText>(id_cancha_plata)

                if ((p_izquierda_plata.text.toString() == "") && (p_derecha_plata.text.toString() == "") && (p_cancha_plata.text.toString() == "") && (p_hora_plata.text.toString() == ""))
                {anclaPlata = i
                    break}else if (!((p_izquierda_plata.text.toString() != "") && (suggestions.contains(p_izquierda_plata.text.toString())) && (p_derecha_plata.text.toString() != "") &&  (suggestions.contains(p_derecha_plata.text.toString())) && (p_cancha_plata.text.toString() != "") && (p_hora_plata.text.toString() != "")))
                {error=true
                    Toast.makeText(this, "errorPlata", Toast.LENGTH_SHORT).show()
                    break}
                if(i==32)
                    anclaPlata=33
            }
        }

        if (error)
        {
            Toast.makeText(this, "Verifique que no haya partidos incompletos y/o nombres correctos", Toast.LENGTH_SHORT).show()
            error = false
        }
        else
        {
            for (i in 1..anclaOro-1)
            {
                val id_izquierdo_oro = resources.getIdentifier("label$i" + "l", "id", packageName)
                val id_derecho_oro = resources.getIdentifier("label$i" + "r", "id", packageName)
                val id_hora_oro = resources.getIdentifier("hora$i" , "id", packageName)
                val id_cancha_oro = resources.getIdentifier("cancha$i" , "id", packageName)
                val p_izquierda_oro = viewOro.findViewById<AutoCompleteTextView>(id_izquierdo_oro).text.toString()
                val p_derecha_oro = viewOro.findViewById<AutoCompleteTextView>(id_derecho_oro).text.toString()
                val p_hora_oro = viewOro.findViewById<EditText>(id_hora_oro).text.toString()
                val p_cancha_oro = viewOro.findViewById<EditText>(id_cancha_oro).text.toString()


                val queue = Volley.newRequestQueue(this)
                val url = "${MainActivity.GlobalVars.url}api_select_ideq_cruce.php?id=$id_categoria&id1=$p_izquierda_oro&id2=$p_derecha_oro"+
                        "&fecha=2023-11-12" +
                        "&hora=${p_hora_oro}" +
                        "&can=${p_cancha_oro}" +
                        "&tipo=${i}"

                var stringRequest = StringRequest(Request.Method.GET, url, { response -> if (response == "0") runOnUiThread {Toast.makeText(this, "verifique que los datos estan escritos en su formato correcto, goles (solo números), hora (00:00 a 23:59)", Toast.LENGTH_SHORT).show()}
                else {
                }
                }, {
                    runOnUiThread {
                        Toast.makeText(
                            this,
                            "verifique que los datos estan escritos en su formato correcto, goles (solo números), hora (00:00 a 23:59) o su conexion a internet",
                            Toast.LENGTH_SHORT
                        ).show()
                    }
                    })
                queue.add(stringRequest)
                //Toast.makeText(this, "$ideq1 $ideq2", Toast.LENGTH_SHORT).show()

            }

            for (i in 1..anclaPlata-1)
            {
                val id_izquierdo_plata = resources.getIdentifier("labelp$i" + "l", "id", packageName)
                val id_derecho_plata = resources.getIdentifier("labelp$i" + "r", "id", packageName)
                val id_hora_plata = resources.getIdentifier("horap$i", "id", packageName)
                val id_cancha_plata = resources.getIdentifier("canchap$i", "id", packageName)
                val p_izquierda_plata = viewPlata.findViewById<AutoCompleteTextView>(id_izquierdo_plata).text.toString()
                val p_derecha_plata = viewPlata.findViewById<AutoCompleteTextView>(id_derecho_plata).text.toString()
                val p_hora_plata = viewPlata.findViewById<EditText>(id_hora_plata).text.toString()
                val p_cancha_plata = viewPlata.findViewById<EditText>(id_cancha_plata).text.toString()


                val queue = Volley.newRequestQueue(this)
                val url = "${MainActivity.GlobalVars.url}api_select_ideq_cruce.php?id=$id_categoria&id1=$p_izquierda_plata&id2=$p_derecha_plata"+
                        "&fecha=2023-11-12" +
                        "&hora=${p_hora_plata}" +
                        "&can=${p_cancha_plata}" +
                        "&tipo=${i+32}"

                var stringRequest = StringRequest(Request.Method.GET, url, { response -> if (response == "0") Toast.makeText(this, "verifique que los datos estan escritos en su formato correcto, goles (solo números), hora (00:00 a 23:59)", Toast.LENGTH_SHORT).show()
                else {
                    runOnUiThread {
                        Toast.makeText(applicationContext, "Agregado Plata", Toast.LENGTH_SHORT).show()
                    }
                }
                }, {
                    Toast.makeText(this, "verifique que los datos estan escritos en su formato correcto, goles (solo números), hora (00:00 a 23:59) o su conexion a internet", Toast.LENGTH_SHORT).show()
                })
                queue.add(stringRequest)

            }
        }
    }


}
