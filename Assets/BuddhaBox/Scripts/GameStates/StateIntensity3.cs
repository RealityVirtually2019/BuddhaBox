using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIntensity3 : StateIntensityBase
{
    //activate rain
    public GameObject[] softClouds;
    public GameObject[] hardClouds;


    public override void GainFocus()
    {
        //play softCloud particles
        for (int i = 0; i < 6; i++)
        {
            hardClouds[i].SetActive(false);

            softClouds[i].SetActive(true);
        }
        base.GainFocus();

    }

    public override void LoseFocus()
    {
        //disable softCloud particles
        for (int i = 0; i < 6; i++)
        {
            hardClouds[i].SetActive(true);

            softClouds[i].SetActive(false);
        }

    }
}