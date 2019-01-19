using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIntensityBase : GameStateBase
{

    public Clouds clouds;
    public Color skyColor;
    public GameStateBase nextState;

    float runClock = 0;


    public override void GainFocus()
    {
        base.GainFocus();
        GameManager.instance.modules.Get<Sky>().ChangeColor(skyColor);
        StartCoroutine(IntensityRoutine());
    }
    
    IEnumerator IntensityRoutine()
    {
        runClock = 0;
        //text.gameObject.SetActive(true);
        while (runClock < 3)
        {
            runClock += Time.deltaTime;
           // text.text = "Playing: " + (int)(gm.settings.playingDuration - runClock);
            yield return new WaitForEndOfFrame();
        }
      //  text.gameObject.SetActive(false);
        gm.SetCurrentState(nextState);
    }

    public override void DoUpdate()
    {
        // gm.player.transform.position = Vector3.Lerp(gm.introduction.startPosition.position, endPosition.position, runClock / gm.settings.playingDuration);  
    }

}