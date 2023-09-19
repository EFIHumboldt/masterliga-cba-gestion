package com.example.aplicacionsarmientitoii.CosasCruce

import android.content.Context
import android.content.Intent
import android.os.Parcel
import android.os.Parcelable
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.ViewParent
import android.widget.ImageButton
import android.widget.ImageView
import android.widget.TextView
import androidx.core.view.isVisible
import androidx.recyclerview.widget.RecyclerView
import com.bumptech.glide.Glide
import com.example.aplicacionsarmientitoii.AhoraSiModificarActivity
import com.example.aplicacionsarmientitoii.MainActivity
import com.example.aplicacionsarmientitoii.R

class AdaptadorCruce(): RecyclerView.Adapter<AdaptadorCruce.EncuentroViewHolder>(),
    View.OnClickListener {


    override fun onCreateViewHolder (parent: ViewGroup, viewType: Int): EncuentroViewHolder {
        val itemViewEncuentro = LayoutInflater.from(parent.context).inflate(R.layout.card_layout_encuentro, parent, false)
        return  EncuentroViewHolder(itemViewEncuentro)
    }

    override fun onBindViewHolder (holder: EncuentroViewHolder, position: Int) {

        var zona : String = ""
        val currentItem = MainActivity.GlobalVars.crucesArrayList[position]

        holder.tvidPartido.text = currentItem.idPartido.toString()
        holder.idEquipo1.text = currentItem.idEquipoLocal.toString()
        holder.idEquipo2.text = currentItem.idEquipoVisita.toString()
        holder.tvNombreLocal.text = currentItem.nombreLocal
        holder.tvNombreVisita.text = currentItem.nombreVisita
        holder.golesLocal.text = currentItem.golesLocal.toString()
        holder.golesVisita.text = currentItem.golesVisita.toString()
        holder.penalesLocal.text = currentItem.penalesLocal.toString()
        holder.penalesVisita.text = currentItem.penalesVisita.toString()
        holder.tipoCruce.text = currentItem.tipoCruce.toString()

        Glide.with(holder.imgEquipoLocal.context).load("${MainActivity.GlobalVars.url}ESCUDOS/${currentItem.esc1}").into(holder.imgEquipoLocal)
        Glide.with(holder.imgEquipoVisita.context).load("${MainActivity.GlobalVars.url}ESCUDOS/${currentItem.esc2}").into(holder.imgEquipoVisita)

        holder.cancha.text = currentItem.canchaCruce.toString()
        holder.tipoCruce.text = currentItem.tipoCruce.toString()

        if(currentItem.golesLocal == "")
        {
            holder.finalizado.text = ""
        }else
            holder.finalizado.text = "finalizado"

        if (currentItem.tipoCruce.toString().toInt() >= 1 && currentItem.tipoCruce.toString().toInt() <= 1) { zona = "FINAL COPA ORO"}
        else if (currentItem.tipoCruce.toString().toInt() >= 2 && currentItem.tipoCruce.toString().toInt() <= 2) { zona = "3ER Y 4TO PUESTO COPA ORO"}
        else if (currentItem.tipoCruce.toString().toInt() >= 3 && currentItem.tipoCruce.toString().toInt() <= 4) { zona = "SEMIFINALES COPA ORO"}
        else if (currentItem.tipoCruce.toString().toInt() >= 5 && currentItem.tipoCruce.toString().toInt() <= 8) { zona = "4TOS COPA ORO"}
        else if (currentItem.tipoCruce.toString().toInt() >= 9 && currentItem.tipoCruce.toString().toInt() <= 16) { zona = "8VOS COPA ORO"}
        else if (currentItem.tipoCruce.toString().toInt() >= 17 && currentItem.tipoCruce.toString().toInt() <= 32) { zona = "16VOS COPA ORO"}
        else if (currentItem.tipoCruce.toString().toInt() >= 33 && currentItem.tipoCruce.toString().toInt() <= 33) { zona = "FINAL COPA PLATA"}
        else if (currentItem.tipoCruce.toString().toInt() >= 34 && currentItem.tipoCruce.toString().toInt() <= 34) { zona = "3ER Y 4TO PUESTO COPA PLATA"}
        else if (currentItem.tipoCruce.toString().toInt() >= 35 && currentItem.tipoCruce.toString().toInt() <= 36) { zona = "SEMIFINALES COPA PLATA"}
        else if (currentItem.tipoCruce.toString().toInt() >= 37 && currentItem.tipoCruce.toString().toInt() <= 40) { zona = "4TOS COPA PLATA"}
        else if (currentItem.tipoCruce.toString().toInt() >= 41 && currentItem.tipoCruce.toString().toInt() <= 48) { zona = "8VOS COPA PLATA"}
        else if (currentItem.tipoCruce.toString().toInt() >= 49 && currentItem.tipoCruce.toString().toInt() <= 64) { zona = "16VOS COPA PLATA"}

        holder.horaCruce.text = currentItem.hora + " - " + zona

        holder.setOnClickListener()

    }

    override fun getItemCount(): Int {
        return MainActivity.GlobalVars.crucesArrayList.size
    }

    inner class EncuentroViewHolder(itemView: View): RecyclerView.ViewHolder(itemView), View.OnClickListener {


        /* POR SI QUEREMOS IMPLEMENTAR UNA FUNCION DE CLICK EN EL RECYLCER VIEW
        fun setOnClickListener() {
            imgEstrella.setOnClickListener(this)
            itemView.setOnClickListener(this)
        }


         */

        fun setOnClickListener() {

            itemView.setOnClickListener(this)
        }

        override fun onClick(view: View) {


            val intent: Intent = Intent(context, AhoraSiModificarCruceActivity::class.java)


            intent.putExtra("ID", tvidPartido.text)
            intent.putExtra("id1", idEquipo1.text)
            intent.putExtra("id2", idEquipo2.text)
            intent.putExtra("e1", tvNombreLocal.text)
            intent.putExtra("e2", tvNombreVisita.text)
            intent.putExtra("g1", golesLocal.text)
            intent.putExtra("g2", golesVisita.text)
            intent.putExtra("p1", penalesLocal.text)
            intent.putExtra("p2", penalesVisita.text)
            intent.putExtra("hora", horaCruce.text)
            intent.putExtra("cancha", cancha.text)
            intent.putExtra("tipo", tipoCruce.text)

            context.startActivity(intent)

        }


        val tvidPartido : TextView = itemView.findViewById(R.id.id2)
        val idEquipo1: TextView = itemView.findViewById(R.id.eq1)
        val idEquipo2: TextView = itemView.findViewById(R.id.eq2)
        val tvNombreLocal : TextView = itemView.findViewById(R.id.tvEquipoL)
        val tvNombreVisita : TextView = itemView.findViewById(R.id.tvEquipov)
        val golesLocal: TextView = itemView.findViewById(R.id.tvResultadoL)
        val golesVisita: TextView = itemView.findViewById(R.id.tvResultadoV)
        val penalesLocal: TextView = itemView.findViewById(R.id.tvPenalesL)
        val penalesVisita: TextView = itemView.findViewById(R.id.tvPenalesV)
        val horaCruce: TextView = itemView.findViewById(R.id.tvDatosEncuentro)
        val cancha: TextView = itemView.findViewById(R.id.tvidequipo)
        val finalizado: TextView = itemView.findViewById(R.id.finalizado)
        val tipoCruce: TextView = itemView.findViewById(R.id.nroTipo)
        val imgEquipoLocal: ImageView = itemView.findViewById(R.id.imgEquipoL)
        val imgEquipoVisita: ImageView = itemView.findViewById(R.id.imgEquipoV)
        //agregado para apretar la estrella
        val context : Context = itemView.context



    }



    override fun onClick(p0: View?) {
        TODO("Not yet implemented")
    }


}
