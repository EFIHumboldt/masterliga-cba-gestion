package com.example.aplicacionsarmientitoii

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.Button
import android.widget.Toast
import com.android.volley.Request
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley
import com.example.aplicacionsarmientitoii.CosasCruce.Cruce
import com.example.aplicacionsarmientitoii.CosasDetalleZona.Posicion
import com.example.aplicacionsarmientitoii.CosasEncuentros.Encuentro
import org.json.JSONArray
import org.json.JSONObject

class MainActivity : AppCompatActivity() {

    object GlobalVars {

        lateinit var viewOro : View
        lateinit var viewPlata : View

        //val url: String = "https://vps-3147586-x.dattaweb.com/"
        lateinit var zonaG : String
        var url: String = "http://192.168.100.129/"

        //var equiposArrayList: MutableList<Equipos> = mutableListOf()
        //var favoritosArrayList: MutableList<Equipos> = mutableListOf()
        var encuentrosArrayList: MutableList<Encuentro> = mutableListOf()
        //var encuentrosHoyArrayList: MutableList<Encuentro> = mutableListOf()
        //var encuentrosManianaArrayList: MutableList<Encuentro> = mutableListOf()
        var categoriaZona: String = ""
        var arrayZonas: MutableList<String> = mutableListOf()
        var zonasArrayList: MutableList<String> = mutableListOf("A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N")
        var posicionesDesordenadosZonaArrayList: MutableList<Posicion> = mutableListOf()
        var posicionesOrdenadosZonaArrayList: MutableList<Posicion> = mutableListOf()
        var anioCategoria: MutableList<String> = mutableListOf("2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017")

        var crucesArrayList: MutableList<Cruce> = mutableListOf()
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val view = layoutInflater

        val btnCrearEnc : Button = findViewById(R.id.btnCrearEnc)
        val btnModificarEnc : Button = findViewById(R.id.btnModificarEnc)
        val btnVerTabla : Button = findViewById(R.id.btnTablaGeneral)
        val btnCrearPlantilla : Button = findViewById(R.id.btnCrearPlantillaCruce)


        btnCrearEnc.setOnClickListener()
        {
            val intent = Intent (this, CrearEncuentroActivity::class.java)
            startActivity(intent)
        }
        btnModificarEnc.setOnClickListener()
        {
            val intent = Intent (this, ModificarEncuentroActivity::class.java)
            startActivity(intent)
        }
        btnVerTabla.setOnClickListener()
        {
            val intent = Intent (this, VerTablaGeneral::class.java)
            startActivity(intent)
        }
        btnCrearPlantilla.setOnClickListener()
        {
            val intent = Intent (this, CrearPlantillaCruce::class.java)
            startActivity(intent)
        }
    }

}