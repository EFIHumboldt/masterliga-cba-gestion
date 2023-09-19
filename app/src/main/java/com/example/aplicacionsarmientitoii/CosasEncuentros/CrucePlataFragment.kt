package com.efihumboldt.sarmientito.CosasEncuentros

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.AutoCompleteTextView
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.aplicacionsarmientitoii.MainActivity
import com.example.aplicacionsarmientitoii.R




class CrucePlataFragment() : Fragment() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

    }
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        return inflater.inflate(R.layout.fragment_cruce_plata, container, false)
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        val layoutManager = LinearLayoutManager(view.context)
        MainActivity.GlobalVars.viewPlata = view

        val suggestions = mutableListOf<String>()
        for (i in 1..64) {  suggestions.add("GANADOR ${i}") }
        for (i in 1..64) {  suggestions.add("${i}º TABLA GENERAL")}
        for (i in 1..64) {  suggestions.add("PERDEDOR ${i}")}

        for (i in 1..32) { // Cambia 100 al número total de AutoCompleteTextView que tienes

            val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_dropdown_item_1line, suggestions)

            /*
            var autoCompleteTextViewId = resources.getIdentifier("label$i" + "l", "id", requireActivity().packageName)
            var opcion1 = view.findViewById<AutoCompleteTextView>(autoCompleteTextViewId)
            opcion1.setAdapter(adapter)

            autoCompleteTextViewId= resources.getIdentifier("label$i" + "r", "id", requireActivity().packageName)
            opcion1 = view.findViewById<AutoCompleteTextView>(autoCompleteTextViewId)
            opcion1.setAdapter(adapter)

            */
            var autoCompleteTextViewId= resources.getIdentifier("labelp$i" + "l", "id", requireActivity().packageName)
            var opcion1 = view.findViewById<AutoCompleteTextView>(autoCompleteTextViewId)
            opcion1.setAdapter(adapter)

            autoCompleteTextViewId= resources.getIdentifier("labelp$i" + "r", "id", requireActivity().packageName)
            opcion1 = view.findViewById<AutoCompleteTextView>(autoCompleteTextViewId)
            opcion1.setAdapter(adapter)

        }
    }
    override fun onDestroyView() {
        super.onDestroyView()
        var binding = null
    }

}
