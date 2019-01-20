using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIntensity1 : StateIntensityBase
{

    //activate lightning
    public Material cloudMaterial1;

    public GameObject[] clouds1;

    public GameObject[] lightningParticles;

    public override void GainFocus()
    {
  

        //activate lightning particles
        for (int i = 0; i < 6; i++)
        {
           lightningParticles[i].SetActive(true);
           clouds1[i].GetComponent<ParticleSystemRenderer>().material = cloudMaterial1;

        }
        base.GainFocus();

    }

    public override void LoseFocus()
    {
 
        for (int i = 0; i < 6; i++)
        {
            lightningParticles[i].SetActive(false);
        }

    }

}