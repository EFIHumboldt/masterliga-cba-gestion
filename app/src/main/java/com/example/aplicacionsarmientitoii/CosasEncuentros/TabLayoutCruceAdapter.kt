package com.example.aplicacionsarmientitoii.CosasEncuentros

import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.lifecycle.Lifecycle
import androidx.viewpager2.adapter.FragmentStateAdapter
import com.efihumboldt.sarmientito.CosasEncuentros.CruceOroFragment
import com.efihumboldt.sarmientito.CosasEncuentros.CrucePlataFragment

class TabLayoutCruceAdapter(fragmentManager: FragmentManager, lifecycle: Lifecycle) : FragmentStateAdapter(fragmentManager, lifecycle){


    override fun getItemCount(): Int {
        return 2
    }

    override fun createFragment(position: Int): Fragment {


        return when (position) {
            0 -> {
                CruceOroFragment()
            }
            1-> {
                CrucePlataFragment()
            }
            else -> {
                Fragment()
            }
        }

    }
}