using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIntensity2 : StateIntensityBase
{
    //activate rain

    void GameFocus()
    {
        //play lightning particles
        GameObject.Find("Shower").SetActive(true);
        GameObject.Find("Shower (1)").SetActive(true);
        GameObject.Find("Shower (2)").SetActive(true);
        GameObject.Find("Shower (3)").SetActive(true);
        GameObject.Find("Shower (4)").SetActive(true);
        GameObject.Find("Shower (5)").SetActive(true);

    }

    void LoseFocus()
    {
        //disable lightning particles
        GameObject.Find("Shower").SetActive(false);
        GameObject.Find("Shower (1)").SetActive(false);
        GameObject.Find("Shower (2)").SetActive(false);
        GameObject.Find("Shower (3)").SetActive(false);
        GameObject.Find("Shower (4)").SetActive(false);
        GameObject.Find("Shower (5)").SetActive(false);

    }

}