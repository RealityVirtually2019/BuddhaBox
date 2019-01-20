using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIntensity3 : StateIntensityBase
{
    //activate rain
    public GameObject[] softClouds;
    bool isFocused;
    public GameObject elephantCloud;
    float counter;
    void Start()
    {
        isFocused = false;
        counter = 0;
    }
    void Update()
    {
        if (isFocused)
        {
           
            counter += Time.deltaTime;
           
        }
        if (counter > 60.0f)
        {
            Debug.Log("It has been 1 min");
            //play elephant animation
            elephantCloud.SetActive(true);
        }
    }

    public override void GainFocus()
    {
        //play softCloud particles
        for (int i = 0; i < 6; i++)
        {
            isFocused = true;
            softClouds[i].SetActive(true);
        }
        base.GainFocus();

    }

    public override void LoseFocus()
    {
        //disable softCloud particles
        counter = 0;
        isFocused = false;
        for (int i = 0; i < 6; i++)
        {

            softClouds[i].SetActive(false);
        }

    }
}