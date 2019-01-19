using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlaying : GameStateBase
{
    public TMPro.TextMeshProUGUI text;

    float runClock = 0;

    public Transform endPosition;


    public override void GainFocus()
    {
        base.GainFocus();
        StartCoroutine(IntroRoutine());
    }

    IEnumerator IntroRoutine()
    {
        runClock = 0;
        text.gameObject.SetActive(true);
        while (runClock < gm.settings.playingDuration)
        {
            runClock += Time.deltaTime;
            text.text = "Playing: " + (int)(gm.settings.playingDuration - runClock);
            yield return new WaitForEndOfFrame();
        }
        text.gameObject.SetActive(false);
        gm.SetCurrentState(gm.finishing);
    }

    public override void DoUpdate()
    {
       // gm.player.transform.position = Vector3.Lerp(gm.introduction.startPosition.position, endPosition.position, runClock / gm.settings.playingDuration);  
    }

}