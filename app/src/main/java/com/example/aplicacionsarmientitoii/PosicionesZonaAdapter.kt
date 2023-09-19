package com.example.aplicacionsarmientitoii

import android.app.AlertDialog
import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.ImageView
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.core.content.contentValuesOf
import androidx.recyclerview.widget.RecyclerView
import com.bumptech.glide.Glide
import com.example.aplicacionsarmientitoii.CosasDetalleZona.Posicion
import kotlinx.coroutines.NonDisposableHandle.parent


class PosicionesZonaAdapter(): RecyclerView.Adapter<PosicionesZonaAdapter.PosicionesZonaViewHolder>(),
View.OnClickListener{

    override fun onCreateViewHolder (parent: ViewGroup, viewType: Int): PosicionesZonaAdapter.PosicionesZonaViewHolder {

        val itemViewPosicionesZona = LayoutInflater.from(parent.context).inflate(R.layout.card_layout_tabla, parent, false)

        return PosicionesZonaViewHolder(itemViewPosicionesZona)

    }

    override fun onBindViewHolder (holder: PosicionesZonaViewHolder, position: Int) {
        val currentItem = MainActivity.GlobalVars.posicionesOrdenadosZonaArrayList[position]
        holder.puesto.text = currentItem.puesto.toString()
        //holder.escudo.setImageResource(currentItem.escudo)
        Glide.with(holder.escudo.context).load("${MainActivity.GlobalVars.url}ESCUDOS/${currentItem.escudo}").into(holder.escudo)
        holder.n = currentItem.escudo
        holder.nombre.text = currentItem.nombre
        holder.puntos.text = currentItem.puntos.toString()
        holder.pg.text = currentItem.pg.toString()
        holder.pj.text = currentItem.pj.toString()
        holder.pe.text = currentItem.pe.toString()
        holder.pp.text = currentItem.pp.toString()
        holder.ga.text = currentItem.gf.toString()
        holder.gc.text = currentItem.gc.toString()
        holder.id_equipo.text = currentItem.id_equipo.toString()


    }
    class PosicionesZonaViewHolder(itemView: View): RecyclerView.ViewHolder(itemView), View.OnClickListener {

        val puesto: TextView = itemView.findViewById(R.id.posicion)
        val escudo: ImageView = itemView.findViewById(R.id.imgEquipo)
        val nombre: TextView = itemView.findViewById(R.id.item_nombre)
        val puntos: TextView = itemView.findViewById(R.id.item_nombre2)
        val id_equipo: TextView = itemView.findViewById(R.id.idEquipoText)


        val pj: TextView = itemView.findViewById(R.id.item_nombre3)
        val pg: TextView = itemView.findViewById(R.id.item_nombre4)
        val pe: TextView = itemView.findViewById(R.id.item_nombre5)
        val pp: TextView = itemView.findViewById(R.id.item_nombre6)
        val ga: TextView = itemView.findViewById(R.id.item_nombre7)
        val gc: TextView = itemView.findViewById(R.id.item_nombre8)
        var n: String = "hola"



        val context : Context = itemView.context

        override fun onClick(view: View) {

          /*
        if (view == abajo)
        {

            var equipo = MainActivity.GlobalVars.posicionesOrdenadosZonaArrayList
            var aux = equipo[puesto.text.toString().toInt() -1]
            equipo[puesto.text.toString().toInt()-1] = equipo[puesto.text.toString().toInt()]
            equipo[puesto.text.toString().toInt()] = aux

            equipo[puesto.text.toString().toInt() -1].puesto = puesto.text.toString().toInt()
            equipo[puesto.text.toString().toInt()].puesto = puesto.text.toString().toInt() + 1


            VerTablaGeneral().refresh(puesto.text.toString().toInt()-1)

        }


           */
        }


    }
    override fun getItemCount(): Int {
        return MainActivity.GlobalVars.posicionesOrdenadosZonaArrayList.size
    }
    override fun onClick(p0: View?) {
        TODO("Not yet implemented")
    }

    fun notifyItemMoved(hola: Int) {

    }
}