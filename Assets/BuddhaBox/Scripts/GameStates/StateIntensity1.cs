using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIntensity1 : StateIntensityBase
{

    //activate lightning

    public GameObject[] clouds1;

    public GameObject[] lightningParticles;

    public override void GainFocus()
    {
  

        //activate lightning particles
        for (int i = 0; i < 6; i++)
        {
           lightningParticles[i].SetActive(true);
            clouds1[i].SetActive(true);

        }
        base.GainFocus();

    }

    public override void LoseFocus()
    {
 
        for (int i = 0; i < 6; i++)
        {
            lightningParticles[i].SetActive(false);
            clouds1[i].SetActive(false);

        }

    }

}