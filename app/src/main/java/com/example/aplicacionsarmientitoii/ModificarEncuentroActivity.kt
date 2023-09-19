package com.example.aplicacionsarmientitoii

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import androidx.appcompat.app.AppCompatActivity

class ModificarEncuentroActivity: AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_modificar_encuentro)

        val b2009 = findViewById<Button>(R.id.button0)
        val b2010 = findViewById<Button>(R.id.button1)
        val b2011 = findViewById<Button>(R.id.button2)
        val b2012 = findViewById<Button>(R.id.button3)
        val b2013 = findViewById<Button>(R.id.button4)
        val b2014 = findViewById<Button>(R.id.button5)
        val b2015 = findViewById<Button>(R.id.button6)
        val b2016 = findViewById<Button>(R.id.button7)

        b2009.text = MainActivity.GlobalVars.anioCategoria[0]
        b2010.text = MainActivity.GlobalVars.anioCategoria[1]
        b2011.text = MainActivity.GlobalVars.anioCategoria[2]
        b2012.text = MainActivity.GlobalVars.anioCategoria[3]
        b2013.text = MainActivity.GlobalVars.anioCategoria[4]
        b2014.text = MainActivity.GlobalVars.anioCategoria[5]
        b2015.text = MainActivity.GlobalVars.anioCategoria[6]
        b2016.text = MainActivity.GlobalVars.anioCategoria[7]




        b2009.setOnClickListener{
            val intent = Intent (this, ZonasActivity::class.java)
            MainActivity.GlobalVars.categoriaZona = MainActivity.GlobalVars.anioCategoria[0]
            startActivity(intent)
        }
        b2010.setOnClickListener{
            val intent = Intent (this, ZonasActivity::class.java)
            MainActivity.GlobalVars.categoriaZona = MainActivity.GlobalVars.anioCategoria[1]
            startActivity(intent)
        }
        b2011.setOnClickListener{
            val intent = Intent (this, ZonasActivity::class.java)
            MainActivity.GlobalVars.categoriaZona = MainActivity.GlobalVars.anioCategoria[2]
            startActivity(intent)
        }
        b2012.setOnClickListener{
            val intent = Intent (this, ZonasActivity::class.java)
            MainActivity.GlobalVars.categoriaZona = MainActivity.GlobalVars.anioCategoria[3]
            startActivity(intent)
        }
        b2013.setOnClickListener{
            val intent = Intent (this, ZonasActivity::class.java)
            MainActivity.GlobalVars.categoriaZona = MainActivity.GlobalVars.anioCategoria[4]
            startActivity(intent)
        }
        b2014.setOnClickListener{
            val intent = Intent (this, ZonasActivity::class.java)
            MainActivity.GlobalVars.categoriaZona = MainActivity.GlobalVars.anioCategoria[5]
            startActivity(intent)
        }
        b2015.setOnClickListener{
            val intent = Intent (this, ZonasActivity::class.java)
            MainActivity.GlobalVars.categoriaZona = MainActivity.GlobalVars.anioCategoria[6]
            startActivity(intent)
        }
        b2016.setOnClickListener{
            val intent = Intent (this, ZonasActivity::class.java)
            MainActivity.GlobalVars.categoriaZona = MainActivity.GlobalVars.anioCategoria[7]
            startActivity(intent)
        }
    }

}