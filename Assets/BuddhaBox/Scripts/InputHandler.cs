using UnityEngine;

public class InputHandler : ModuleBase
{
    GameManager gm;


    public float intensityClock = 0;
    public float decisionClock = 0;

    private BreathDetector breathDetector;

    public float playTimeClock = 0;

    private void Start()
    {
        gm = GameManager.instance;
        breathDetector = gm.modules.Get<BreathDetector>();

    }


    private int CheckBreathingRate()
    {
        return 0;
    }

    public override void DoUpdate()
    {
        if(gm.currentState == gm.finishing)
        {
            return;
        }
        if (gm.currentState != gm.introduction)
        {
            intensityClock += Time.deltaTime;
            decisionClock += Time.deltaTime;
            playTimeClock += Time.deltaTime;
        }
       

        switch (gm.settings.mode)
        {
            case Settings.MODE.AUTO_PLAY:
                if (playTimeClock >= gm.settings.TotalSecondsBeforeFinishingExperience)
                {
                    gm.SetCurrentState(gm.finishing);
                    return;
                }
                if (intensityClock > gm.settings.SecondsBetweenStatesInAutoplay)
                    {
                        intensityClock = 0;
                        gm.SetCurrentState((gm.currentState as StateIntensityBase).nextState);
                    }
                break;
            case Settings.MODE.BREATH_DETECTION:
                if (playTimeClock >= gm.settings.TotalSecondsBeforeFinishingExperience)
                {
                    gm.SetCurrentState(gm.finishing);
                    return;
                }
                if (decisionClock > gm.settings.SecondsBetweenRecheckingBreathsPerMinute)
                {
                    float secondsPerBreath = breathDetector.GetSecondsBetweenBreaths();
                    Debug.Log("Making mood decision based on seconds per breath: " + secondsPerBreath);
                    decisionClock = 0;

                    if (secondsPerBreath > gm.settings.SecondsPerBreathForIntensity3)
                    {
                        gm.SetCurrentState(gm.intenstity3);

                    }
                    else if (secondsPerBreath > gm.settings.SecondsPerBreathForIntensity2)
                    {
                        gm.SetCurrentState(gm.intenstity2);

                    }
                    else
                    {
                        gm.SetCurrentState(gm.intenstity1);

                    }
                }
                break;
        }
      
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            gm.SetCurrentState(gm.introduction);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gm.SetCurrentState(gm.intenstity1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gm.SetCurrentState(gm.intenstity2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gm.SetCurrentState(gm.intenstity3);
        }

        base.DoUpdate();
    }
}
