using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFinishing : GameStateBase
{
    public TMPro.TextMeshProUGUI text;

    float runClock = 0;

    public override void GainFocus()
    {
        base.GainFocus();
        StartCoroutine(IntroRoutine());
    }

    IEnumerator IntroRoutine()
    {
        runClock = 0;
        text.gameObject.SetActive(true);
        while (runClock < gm.settings.finishingDuration)
        {
            runClock += Time.deltaTime;
           // text.text = "Finishing: " + (gm.settings.finishingDuration - runClock);
            yield return new WaitForEndOfFrame();
        }
        text.gameObject.SetActive(false);
        Application.Quit();
        Debug.Log("quit!");
      //  gm.SetCurrentState(gm.introduction);
    }


}
