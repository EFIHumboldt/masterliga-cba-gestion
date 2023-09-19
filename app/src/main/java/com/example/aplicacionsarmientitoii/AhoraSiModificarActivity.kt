package com.example.aplicacionsarmientitoii

import android.os.Bundle
import android.widget.*
import androidx.appcompat.app.AlertDialog
import androidx.appcompat.app.AppCompatActivity
import com.android.volley.Request
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley

class AhoraSiModificarActivity: AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_modificar_encuentro2)
        val tvcategoria = findViewById<EditText>(R.id.tvcategoria)
        val tvzona = findViewById<EditText>(R.id.tvzona)
        val goles = findViewById<EditText>(R.id.goleslocal)
        val goles1 = findViewById<EditText>(R.id.golesVisita)
        val idpartido = findViewById<TextView>(R.id.idequipo)
        var nombre1 = findViewById<EditText>(R.id.tvEquipoL)
        var nombre2 = findViewById<EditText>(R.id.tvEquipoV)
        var finalizado = findViewById<CheckBox>(R.id.finalizado)
        var tvhora1 = findViewById<EditText>(R.id.horaedit)
        var fecha = findViewById<EditText>(R.id.tvfecha)
        var tvcancha = findViewById<EditText>(R.id.tvcancha)

        val bundle = intent.extras

        var id = bundle?.getString("ID")
        var equipo1 = bundle?.getString("equipo1")
        var equipo2 = bundle?.getString("equipo2")
        var resultadoLocal = bundle?.getString("resultadolocal")
        var resultadoVisita = bundle?.getString("resultadovisita")
        var hora = bundle?.getString("hora")
        var fechatv = bundle?.getString("fecha")
        var cancha = bundle?.getString("cancha")
        var categoria = MainActivity.GlobalVars.categoriaZona
        var zona = MainActivity.GlobalVars.zonaG

        finalizado.isChecked = resultadoLocal.toString() != ""
        goles.setText(resultadoLocal.toString())
        goles1.setText(resultadoVisita.toString())
        idpartido.text = id
        nombre1.setText(equipo1)
        nombre2.setText(equipo2)
        tvhora1.setText(hora)
        fecha.setText(fechatv)
        tvcancha.setText(cancha)
        tvcategoria.setText(categoria)
        tvzona.setText(zona)



        finalizado.setOnClickListener {
            if(finalizado.isChecked == false)
            {
                goles1.setEnabled(false)
                goles.setEnabled(false)
            }else
                goles.setEnabled(true)
            goles1.setEnabled(true)
        }




        val botonCrearEncuentro = findViewById<Button>(R.id.modificar)
        botonCrearEncuentro.setOnClickListener(){

            val builder = AlertDialog.Builder(this)
            builder.setTitle("Alerta")
            builder.setMessage("¿Esta seguro que desea modificar el encuentro?")
            builder.setPositiveButton("Aceptar"){

                    dialog, which -> modificarEncuentro(goles,goles1, tvhora1, idpartido, tvcancha)
            }
            builder.setNegativeButton("Cancelar", null)
            builder.show()
        }
    }

    fun modificarEncuentro(golesl: EditText, golesv: EditText, hora: EditText, id: TextView, cancha: EditText)
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

        if(cancha.text.isEmpty()){
            Toast.makeText(this, "La cancha no puede estar vacía", Toast.LENGTH_SHORT).show()
            return
        }
        if(hora.text.isEmpty()){
            Toast.makeText(this, "La hora no puede estar vacía", Toast.LENGTH_SHORT).show()
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
        url="${MainActivity.GlobalVars.url}api_update_partido.php?id=${id.text}" +
                "&goles1=$gl" +
                "&goles2=$gv" +
                "&hora=${hora.text}" +
                "&cancha=${cancha.text}"
        var stringRequest = StringRequest(Request.Method.GET, url, { response -> if (response == "0") Toast.makeText(this, "verifique que los datos estan escritos en su formato correcto, goles (solo números), hora (00:00 a 23:59)", Toast.LENGTH_SHORT).show()
        else {Toast.makeText(this, "El encuentro se modificó correctamente", Toast.LENGTH_SHORT).show()
            finish()
        }
        }, {
            Toast.makeText(this, "verifique que los datos estan escritos en su formato correcto, goles (solo números), hora (00:00 a 23:59) o su conexion a internet", Toast.LENGTH_SHORT).show()
        })
        queue.add(stringRequest)

    }

    private fun limpiar(hora: EditText, golesl: EditText, golesv: EditText){

    }

}