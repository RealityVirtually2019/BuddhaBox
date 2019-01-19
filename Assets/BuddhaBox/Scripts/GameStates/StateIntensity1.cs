using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIntensity1 : StateIntensityBase
{
    //activate lightning

    void GameFocus()
    {
        //play lightning particles
        GameObject.Find("Ligtning").SetActive(true);
        GameObject.Find("Ligtning (1)").SetActive(true);
        GameObject.Find("Ligtning (2)").SetActive(true);
        GameObject.Find("Ligtning (3)").SetActive(true);
        GameObject.Find("Ligtning (4)").SetActive(true);
        GameObject.Find("Ligtning (5)").SetActive(true);

    }

    void LoseFocus()
    {
        //disable lightning particles
        GameObject.Find("Ligtning").SetActive(false);
        GameObject.Find("Ligtning (1)").SetActive(false);
        GameObject.Find("Ligtning (2)").SetActive(false);
        GameObject.Find("Ligtning (3)").SetActive(false);
        GameObject.Find("Ligtning (4)").SetActive(false);
        GameObject.Find("Ligtning (5)").SetActive(false);

    }

}