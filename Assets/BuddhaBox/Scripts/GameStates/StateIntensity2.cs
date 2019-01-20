using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIntensity2 : StateIntensityBase
{
    //activate rain
    public GameObject[] rainParticles;
    public GameObject[] clouds2;
    public Material cloudMaterial2;

    public override void GainFocus()
    {
        //play lightning particles
        for (int i = 0; i < 6; i++)
        {
            rainParticles[i].SetActive(true);
            clouds2[i].GetComponent<ParticleSystemRenderer>().material = cloudMaterial2;
        }
        base.GainFocus();

    }

    public override void LoseFocus()
    {
        //disable lightning particles
        for (int i = 0; i < 6; i++)
        {
            rainParticles[i].SetActive(false);
        }

    }

}