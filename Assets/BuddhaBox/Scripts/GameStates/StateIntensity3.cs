using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIntensity3 : StateIntensityBase
{
    //activate rain
    public GameObject[] softClouds;
    public GameObject[] hardClouds;
    bool isFocused;
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
        if (counter > 4.0f)
        {
            Debug.Log("It has been 4 seconds");
        }
    }

    public override void GainFocus()
    {
        //play softCloud particles
        for (int i = 0; i < 6; i++)
        {
            isFocused = true;
            //hardClouds[i].SetActive(false);
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
           // hardClouds[i].SetActive(true);

            softClouds[i].SetActive(false);
        }

    }
}