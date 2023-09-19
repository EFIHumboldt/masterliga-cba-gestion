package com.example.aplicacionsarmientitoii


import android.content.Context
import android.content.Intent
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.aplicacionsarmientitoii.CosasDetalleZona.DetallesZonaActivity
import com.example.aplicacionsarmientitoii.CosasDetalleZona.DetallesZonaCruceActivity


class AdaptadorZona(): RecyclerView.Adapter<AdaptadorZona.FavoritosViewHolder>(),
    View.OnClickListener {

    override fun onCreateViewHolder (parent: ViewGroup, viewType: Int): FavoritosViewHolder {
        val itemViewFavoritos = LayoutInflater.from(parent.context).inflate(R.layout.card_layout_zona, parent,false)
        return FavoritosViewHolder(itemViewFavoritos)
    }

    override fun onBindViewHolder(holder: FavoritosViewHolder, position: Int) {

        val currentItem = MainActivity.GlobalVars.arrayZonas[position]

            holder.tvtitulozona.text = currentItem
        holder.setOnClickListener()
    }

    override fun getItemCount(): Int {
        return MainActivity.GlobalVars.arrayZonas.size
    }

    class FavoritosViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView),
        View.OnClickListener {
        //agregado para apretar la estrella

        fun setOnClickListener() {
            itemView.setOnClickListener(this)
        }

        override fun onClick(view: View) {

            if (tvtitulozona.text == "Cruces")
            {
                val intent: Intent = Intent(context, DetallesZonaCruceActivity::class.java)
                MainActivity.GlobalVars.zonaG =  tvtitulozona.text.toString()
                context.startActivity(intent)
            }
            else
            {
                val intent: Intent = Intent(context, DetallesZonaActivity::class.java)
                MainActivity.GlobalVars.zonaG =  tvtitulozona.text.toString()
                context.startActivity(intent)
            }



        }

        val tvtitulozona : TextView = itemView.findViewById(R.id.titulozona)

        //agregado para apretar la estrella
        val context : Context = itemView.context

    }

    override fun onClick(p0: View?) {
        TODO("Not yet implemented")
    }
}
