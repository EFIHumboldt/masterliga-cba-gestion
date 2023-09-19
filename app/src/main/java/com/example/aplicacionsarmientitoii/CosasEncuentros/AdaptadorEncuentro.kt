package com.example.aplicacionsarmientitoii

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
import com.example.aplicacionsarmientitoii.MainActivity
import com.example.aplicacionsarmientitoii.R

class AdaptadorEncuentro(): RecyclerView.Adapter<AdaptadorEncuentro.EncuentroViewHolder>(),
    View.OnClickListener {


    override fun onCreateViewHolder (parent: ViewGroup, viewType: Int): EncuentroViewHolder {
        val itemViewEncuentro = LayoutInflater.from(parent.context).inflate(R.layout.card_layout_encuentro, parent, false)
        return  EncuentroViewHolder(itemViewEncuentro)
    }

    override fun onBindViewHolder (holder: EncuentroViewHolder, position: Int) {
        val currentItem = MainActivity.GlobalVars.encuentrosArrayList[position]
        Glide.with(holder.imgEquipoLocal.context).load("${MainActivity.GlobalVars.url}ESCUDOS/${currentItem.fotoEquipoLocal}").into(holder.imgEquipoLocal)
        holder.n1 = currentItem.fotoEquipoLocal
        holder.tvNombreLocal.text = currentItem.nombreLocal
        holder.resultadoLocal.text = currentItem.resultadoLocal
        Glide.with(holder.imgEquipoVisita.context).load("${MainActivity.GlobalVars.url}ESCUDOS/${currentItem.fotoEquipoVisita}").into(holder.imgEquipoVisita)
        holder.n2 = currentItem.fotoEquipoVisita
        holder.tvNombreVisita.text = currentItem.nombreVisita
        holder.resultadoVisita.text = currentItem.resultadoVisita
        holder.tvDatosEncuento.text = currentItem.DatosEncuentro
        holder.tvid.text = currentItem.idEquipoVisita.toString()
        holder.cancha.text  = currentItem.idEquipoLocal.toString()
        if(currentItem.resultadoLocal == null)
        {
            holder.finalizado.text = ""
        }else
        holder.finalizado.text = "finalizado"

        holder.setOnClickListener()

    }

    override fun getItemCount(): Int {
        return MainActivity.GlobalVars.encuentrosArrayList.size
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
            val intent: Intent = Intent(context, AhoraSiModificarActivity::class.java)
            intent.putExtra("ID", tvid.text)
            intent.putExtra("equipo1", tvNombreLocal.text)
            intent.putExtra("equipo2", tvNombreVisita.text)
            intent.putExtra("resultadolocal", resultadoLocal.text)
            intent.putExtra("resultadovisita", resultadoVisita.text)
            intent.putExtra("fecha", finalizado.text)
            intent.putExtra("hora", tvDatosEncuento.text)
            intent.putExtra("cancha", cancha.text)
            context.startActivity(intent)
        }
        val tvid : TextView = itemView.findViewById(R.id.id2)
        val cancha: TextView = itemView.findViewById(R.id.tvidequipo)
        val imgEquipoLocal : ImageView = itemView.findViewById(R.id.imgEquipoL)
        val tvNombreLocal : TextView = itemView.findViewById(R.id.tvEquipoL)
        val resultadoLocal: TextView = itemView.findViewById(R.id.tvResultadoL)
        val imgEquipoVisita : ImageView = itemView.findViewById(R.id.imgEquipoV)
        val tvNombreVisita : TextView = itemView.findViewById(R.id.tvEquipov)
        val resultadoVisita: TextView = itemView.findViewById(R.id.tvResultadoV)
        val tvDatosEncuento: TextView = itemView.findViewById(R.id.tvDatosEncuentro)
        val finalizado: TextView = itemView.findViewById(R.id.finalizado)
        var n1 : String = "hola"
        var n2: String = "hola"

        //agregado para apretar la estrella
        val context : Context = itemView.context



    }



    override fun onClick(p0: View?) {
        TODO("Not yet implemented")
    }


}
