package com.example.aplicacionsarmientitoii

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.Spinner
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.ItemTouchHelper
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.android.volley.Request
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley
import com.example.aplicacionsarmientitoii.CosasDetalleZona.Posicion
import com.example.aplicacionsarmientitoii.CosasEncuentros.Encuentro

import org.json.JSONArray
import org.json.JSONObject
import java.util.*

class VerTablaGeneral: AppCompatActivity() {

    lateinit var adapter : PosicionesZonaAdapter
    lateinit var recyclerviewposiciones: RecyclerView
    val layoutManager = LinearLayoutManager(this)

    var puntos : Int = 0
    var pj : Int  = 0
    var pg : Int  = 0
    var pe : Int  = 0
    var pp : Int  = 0
    var gf : Int  = 0
    var gc : Int  = 0
    var puesto: Int = 0

    var v1 : Int = 0


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_ver_tabla_general)


        val button0 : Button = findViewById(R.id.button0)
        val button1 : Button = findViewById(R.id.button1)
        val button2 : Button = findViewById(R.id.button2)
        val button3 : Button = findViewById(R.id.button3)
        val button4 : Button = findViewById(R.id.button4)
        val button5 : Button = findViewById(R.id.button5)
        val button6 : Button = findViewById(R.id.button6)
        val button7 : Button = findViewById(R.id.button7)

        button0.text = MainActivity.GlobalVars.anioCategoria[0]
        button1.text = MainActivity.GlobalVars.anioCategoria[1]
        button2.text = MainActivity.GlobalVars.anioCategoria[2]
        button3.text = MainActivity.GlobalVars.anioCategoria[3]
        button4.text = MainActivity.GlobalVars.anioCategoria[4]
        button5.text = MainActivity.GlobalVars.anioCategoria[5]
        button6.text = MainActivity.GlobalVars.anioCategoria[6]
        button7.text = MainActivity.GlobalVars.anioCategoria[7]






        button0.setOnClickListener() {
            v1 = MainActivity.GlobalVars.anioCategoria[0].toInt()
            dataInitialize2(v1)
            //restaurarArray(v1)
        }
        button1.setOnClickListener() {
            v1 = MainActivity.GlobalVars.anioCategoria[1].toInt()
            dataInitialize2(v1)
            //restaurarArray(v1)
        }
        button2.setOnClickListener() {
            v1 = MainActivity.GlobalVars.anioCategoria[2].toInt()
            dataInitialize2(v1)
            //restaurarArray(v1)
        }
        button3.setOnClickListener() {
            v1 = MainActivity.GlobalVars.anioCategoria[3].toInt()
            dataInitialize2(v1)
            //restaurarArray(v1)
        }
        button4.setOnClickListener() {
            v1 = MainActivity.GlobalVars.anioCategoria[4].toInt()
            dataInitialize2(v1)
            //restaurarArray(v1)
        }
        button5.setOnClickListener() {
            v1 = MainActivity.GlobalVars.anioCategoria[5].toInt()
            dataInitialize2(v1)
            //restaurarArray(v1)
        }
        button6.setOnClickListener() {
            v1 = MainActivity.GlobalVars.anioCategoria[6].toInt()
            dataInitialize2(v1)
            //restaurarArray(v1)
        }
        button7.setOnClickListener() {
            v1 = MainActivity.GlobalVars.anioCategoria[7].toInt()
            dataInitialize2(v1)
            //restaurarArray(v1)
        }
    }
    private fun dataInitialize2(v1: Int) {
        MainActivity.GlobalVars.posicionesDesordenadosZonaArrayList = mutableListOf<Posicion>()
        MainActivity.GlobalVars.posicionesOrdenadosZonaArrayList = mutableListOf<Posicion>()
        var enc = mutableListOf<Encuentro>()

        val queue = Volley.newRequestQueue(this)

        val url = "${MainActivity.GlobalVars.url}api_posiciones_categoria.php?id=$v1"


        val stringRequest = StringRequest(
            Request.Method.GET, url, { response ->

                if(response.toString().substring(response.toString().length - 5,response.toString().length) != "error"){



                    val jsonArray = JSONArray(response)

                    if (jsonArray.length() > 0) {


                        for (i in 0 until jsonArray.length()) {

                            val jsonObjet = JSONObject(jsonArray.getString(i))

                            val ideq1 = jsonObjet.get("ideq1")
                            val ideq2 = jsonObjet.get("ideq2")
                            val text1 = jsonObjet.get("nombre")
                            val text2 = jsonObjet.get("goles1").toString()
                            val text3 = jsonObjet.get("nombre2")
                            val text4: String = jsonObjet.get("goles2").toString()
                            val text6 = jsonObjet.get("tipo")
                            val esc1 = jsonObjet.get("escudo1")
                            val esc2 = jsonObjet.get("escudo2")
                            val encuentro = Encuentro(ideq1.toString().toInt(), ideq2.toString().toInt(), esc1.toString(), text1.toString(), text2, esc2.toString(), text3.toString(), text4, "", text6.toString())
                            enc.add(encuentro)
                        }
                        var idequiposzonas: MutableList<Int> = mutableListOf()
                        var equiposZona: MutableList<String> = mutableListOf()
                        var escudoZonas: MutableList<String> = mutableListOf()
                        var puntosEquipos: MutableList<Int> = mutableListOf()
                        var partidosZona = enc

                        for (i in 0 until partidosZona.size){

                            if (!(equiposZona.contains(partidosZona[i].nombreLocal))) {

                                idequiposzonas.add(partidosZona[i].idEquipoLocal)
                                equiposZona.add(partidosZona[i].nombreLocal)
                                escudoZonas.add(partidosZona[i].fotoEquipoLocal)
                            }
                            if (!(equiposZona.contains(partidosZona[i].nombreVisita))) {

                                idequiposzonas.add(partidosZona[i].idEquipoVisita)
                                equiposZona.add(partidosZona[i].nombreVisita)
                                escudoZonas.add(partidosZona[i].fotoEquipoVisita)
                            }
                        }


                        for (i in 0 until equiposZona.size) {

                            puntos = 0
                            pj = 0
                            pg = 0
                            pe = 0
                            pp = 0
                            gf = 0
                            gc = 0
                            puesto = 0


                            for (j in 0 until partidosZona.size) {


                                var aux = 0

                                if (equiposZona[i] == partidosZona[j].nombreLocal && partidosZona[j].resultadoLocal != "null"){


                                    if (partidosZona[j].resultadoLocal.toInt() > partidosZona[j].resultadoVisita.toInt()) { aux +=3}
                                    else if (partidosZona[j].resultadoLocal.toInt() == partidosZona[j].resultadoVisita.toInt()) {
                                        aux +=1
                                    }

                                    //PJ
                                    pj++;

                                    //PG, PE, PP
                                    if (aux == 3) pg++
                                    else if (aux == 1) pe++
                                    else pp++

                                    //GF Y GC

                                    gf += partidosZona[j].resultadoLocal.toInt()
                                    gc += partidosZona[j].resultadoVisita.toInt()

                                }
                                else if (equiposZona[i] == partidosZona[j].nombreVisita && partidosZona[j].resultadoVisita != "null"){


                                    //PUNTOS (ARREGLAR QUE LOS EMPATES EXISTEN EN PARTIDOS DE ZONA, OJO)

                                    if (partidosZona[j].resultadoVisita.toInt() > partidosZona[j].resultadoLocal.toInt()) { aux +=3}
                                    else if (partidosZona[j].resultadoVisita.toInt() == partidosZona[j].resultadoLocal.toInt()) {

                                        aux +=1
                                    }

                                    //PJ
                                    pj++;

                                    //PG, PE, PP
                                    if (aux == 3) pg++
                                    else if (aux == 1) pe++
                                    else pp++

                                    //GF Y GC

                                    gf += partidosZona[j].resultadoVisita.toInt()
                                    gc += partidosZona[j].resultadoLocal.toInt()


                                }
                                puntos += aux

                            }
                            puntosEquipos.add(puntos)
                            puesto = i+1
                            val posicion = Posicion(idequiposzonas[i], puesto, escudoZonas[i], equiposZona[i], puntos.toInt(), pj.toInt(), pg.toInt(), pe.toInt(), pp.toInt(), gf.toInt(), gc.toInt())
                            MainActivity.GlobalVars.posicionesDesordenadosZonaArrayList.add(posicion)
                            val a = MainActivity.GlobalVars.posicionesDesordenadosZonaArrayList

                            if(puesto > 1)
                            {

                                for (e in i downTo 1)
                                {

                                    if(a[e].puntos > a[e-1].puntos || (a[e].puntos == a[e-1].puntos && a[e].gf - a[e].gc > a[e-1].gf - a[e-1].gc) || (a[e].puntos == a[e-1].puntos && a[e].gf - a[e].gc == a[e-1].gf - a[e-1].gc && a[e].gf > a[e-1].gf) || (a[e].puntos == a[e-1].puntos && a[e].gf - a[e].gc == a[e-1].gf - a[e-1].gc && a[e].gf == a[e-1].gf && a[e].gc < a[e-1].gc))
                                    {

                                        var aux = 0
                                        aux = a[e].puesto
                                        a[e].puesto = a[e-1].puesto
                                        a[e-1].puesto = aux
                                        var aux1 = a[e]
                                        a.removeAt(e)
                                        a.add(e, a[e-1])
                                        a.removeAt(e-1)
                                        a.add(e-1, aux1)
                                    }
                                }
                            }
                        }

                        for (i in 0 until equiposZona.size) {
                            for (j in 0 until MainActivity.GlobalVars.posicionesDesordenadosZonaArrayList.size)
                                if (i+1 == MainActivity.GlobalVars.posicionesDesordenadosZonaArrayList[j].puesto){
                                    MainActivity.GlobalVars.posicionesOrdenadosZonaArrayList.add(MainActivity.GlobalVars.posicionesDesordenadosZonaArrayList[j])
                                }
                        }

                        MainActivity.GlobalVars.encuentrosArrayList = enc
                    }

                    val intent = Intent (this, Tablas::class.java)
                    intent.putExtra("ID", v1.toString())
                    startActivity(intent)

                }
            },
            {
                Toast.makeText(this, "No se pudo conectar con el Servidor", Toast.LENGTH_SHORT).show()
            })
        queue.add(stringRequest)
    }

   fun refresh(hola: Int)
    {


    }
}