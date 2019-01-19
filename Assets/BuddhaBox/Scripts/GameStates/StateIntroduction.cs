using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIntroduction : GameStateBase
{
    public TMPro.TextMeshProUGUI text;

    float runClock = 0;

    public Transform startPosition;

    public override void GainFocus()
    {
        base.GainFocus();
        StartCoroutine(IntroRoutine());
    }

    IEnumerator IntroRoutine()
    {
        gm.player.transform.position = startPosition.position;
        runClock = 0;
        text.gameObject.SetActive(true);
        while(runClock < gm.settings.introductionDuration)
        {
            runClock += Time.deltaTime;
            text.text = "Introduction: " + (gm.settings.introductionDuration - runClock);
            yield return new WaitForEndOfFrame();
        }
        text.gameObject.SetActive(false);
        gm.SetCurrentState(gm.playing);
    }

}
